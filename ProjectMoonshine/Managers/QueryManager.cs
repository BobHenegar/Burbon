using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Newtonsoft.Json.Linq;
using ProjectMoonshine.Classes.QueryClasses;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Enums;
using ProjectMoonshine.Helpers;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers
{
    public class QueryManager
    {
        private ApplicationDbContext _context { get; set; }
        private DatabaseMetadata _databaseMetadata { get; set; }
        private Dictionary<Guid, Expression> _knownExpressions { get; set; }
        public QueryManager(ApplicationDbContext context,
            DatabaseMetadata databaseMetadata)
        {
            _context = context;
            _databaseMetadata = databaseMetadata;
            PrepareExpressions();
        }
        private void PrepareExpressions()
        {
            _knownExpressions = new Dictionary<Guid, Expression>();
        }
        public QueryViewModel BuildQueryViewModel()
        {
            var model = new QueryViewModel();
            var objects = _context
                .Model
                .GetEntityTypes()
                .Select(s =>
                new KeyValuePair<string, List<ColumnDefinition>>(s.ClrType.Name,
                    s.GetProperties().Select(cd =>
                        new ColumnDefinition()
                        {
                            ColumnName = cd.Name,
                            // String type is inherintly nullable we only care about <object>? types
                            ColumnType = cd.IsNullable
                                && Nullable.GetUnderlyingType(cd.ClrType) != null ?
                                $"Nullable<{Nullable.GetUnderlyingType(cd.ClrType).Name}>" : cd.ClrType.Name
                        }
                    )
                    .ToList()));
            foreach (var obj in objects)
            {
                model.TableDetails.Add(obj.Key, obj.Value);
            }
            return model;
        }
        //public IQueryable<T> GetStoredExpression<T>(IQueryable<T> context, Guid Id)
        //{ }
        public void ParseQuery(string json)
        {
            JToken nodeList = JToken.Parse(json);
            var wrappingLogic = nodeList["logic"] != null && nodeList["logic"].Value<int>() == 1 ? LogicTypes.And : LogicTypes.Or;
            var includes = nodeList["includes"].Values<string>().ToList();
            var baseType = nodeList["baseType"].Value<string>();
            var querySet = GetQueryableEntity(baseType);
            var paramExpression = Expression.Parameter(querySet.ElementType, baseType);
            var exppressionResult = WalkNode(nodeList["logicList"], paramExpression, wrappingLogic);
            CallRuleset<Address>(querySet, exppressionResult, paramExpression);
            Debugger.Break();
        }
        private Expression WalkNode(JToken node, ParameterExpression baseParameter, LogicTypes outerLogic, Expression lhs = null)
        {
            if (node.Type == JTokenType.Array)
            {
                var last = node.Children().Last();

                foreach (var obj in node.Children())
                {
                    lhs = WalkNode(obj, baseParameter, outerLogic, lhs);
                    //if (obj != last)
                    //{
                    //    //Debug.WriteLine($"{new string(' ', depth + 1)}{outerLogic}");
                    //}
                    //else
                    //{
                    //    //Debug.WriteLine($"{new string(' ', depth)})");
                    //}
                }
                return lhs;
            }
            else if (node.Type == JTokenType.Object)
            {
                if (node["lhs"] != null
                    && node["rhs"] != null)
                {
                    var obj = node.ToObject<LogicExpression>();
                    var convertedObject = Convert(obj.Rhs, obj.Lhs.Type);
                    return BuildConstantExpression(baseParameter, obj.Lhs.Value, obj.Rhs, obj.Logic, obj.Lhs.Type);
                }
                else if (node["logicGroup"] != null)
                {
                    if (lhs == null)
                    {
                        lhs = WalkNode(node["logicGroup"], baseParameter, (LogicTypes)node["logic"].Value<int>());
                    }
                    else
                    {
                        var rhs = WalkNode(node["logicGroup"], baseParameter, (LogicTypes)node["logic"].Value<int>(), lhs);
                        return CombineExpressions(lhs, outerLogic, rhs);
                    }
                }
            }
            return lhs;
        }
        public IEnumerable<T> CallRuleset<T>(IQueryable queryableData, Expression predicateBody, ParameterExpression param)
        {
            var propertyAccessExpression = Expression.Lambda(predicateBody, param);

            MethodCallExpression whereCallExpression = Expression.Call(
                typeof(Queryable),
                "Where",
                new Type[] { queryableData.ElementType },
                queryableData.Expression,
                Expression.Lambda<Func<T, bool>>(predicateBody, new ParameterExpression[] { param }));
            var resultList = queryableData.Provider.Execute<IEnumerable<T>>(whereCallExpression);
            return resultList;
        }
        private object Convert(string value, string type)
        {
            switch (type)
            {
                case nameof(Boolean):
                    {
                        var cleanedBool = FormatBoolString(value);
                        return System.Convert.ToBoolean(cleanedBool);
                    }
                case nameof(Int16):
                    {
                        return System.Convert.ToInt16(value);
                    }
                case nameof(DateTimeOffset):
                    {
                        var newVal = System.Convert.ToDateTime(value);
                        return new DateTimeOffset(newVal);
                    }
                case nameof(DateTime):
                    {
                        return System.Convert.ToDateTime(value);
                    }
                case nameof(Guid):
                    {
                        return new Guid(value);
                    }
                default:
                    {
                        return value;
                    }
            }
        }

        private string FormatBoolString(string boolValue)
        {
            var passedValue = boolValue.ToLower();
            switch (passedValue)
            {
                case "1":
                case "yes":
                case "is":
                case "true":
                    {
                        return bool.TrueString;
                    }
                default:
                    {
                        return bool.FalseString;
                    }
            }
        }
        private IQueryable GetQueryableEntity(string baseSet)
        {
            var entitySet = _databaseMetadata.GetTable(baseSet);
            return _context.Query(entitySet.AssemblyFullName);
        }
        private Expression BuildConstantExpression(ParameterExpression expr, string propertyName, string constantValue, EqualityComparisons equality, string propertyType)
        {
            var valueOject = Convert(constantValue, propertyType);
            var castType = Type.GetType("System." + propertyType);
            var left = Expression.Property(expr, propertyName);
            var right = Expression.Constant(valueOject, castType);
            //build the expression
            return CombineExpressions(left, equality, right);
        }
        private Expression CombineExpressions(Expression lhs, EqualityComparisons equality, Expression rhs)
        {
            switch (equality)
            {
                case EqualityComparisons.Equals:
                    {
                        return Expression.Equal(lhs, rhs);
                    }
                case EqualityComparisons.GreaterThan:
                    {
                        return Expression.GreaterThan(lhs, rhs);
                    }
                case EqualityComparisons.GreaterThanOrEqualsTo:
                    {
                        return Expression.GreaterThanOrEqual(lhs, rhs);
                    }
                case EqualityComparisons.LessThan:
                    {
                        return Expression.LessThan(lhs, rhs);
                    }
                default:
                    {
                        return Expression.LessThanOrEqual(lhs, rhs);
                    }
            }
        }
        private Expression CombineExpressions(Expression lhs, LogicTypes logicType, Expression rhs)
        {
            switch (logicType)
            {
                case LogicTypes.And:
                    {
                        return Expression.And(lhs, rhs);
                    }
                default:
                    {
                        return Expression.Or(lhs, rhs);
                    }
            }
        }

    }
    public class LogicExpression
    {
        public QueryProperty Lhs { get; set; }
        public EqualityComparisons Logic { get; set; }
        public string Rhs { get; set; }
    }

    public class QueryProperty
    {
        public string Value { get; set; }
        public string Type { get; set; }

        public Type GetQueryType()
        {
            if (Value.Contains("ChangeDate"))
            {
                return typeof(DateTimeOffset?);
            }
            else if (Value.Contains("Key") && Type.Contains("Nullable"))
            {
                return typeof(Guid?);
            }
            else if (Value.Contains("AddDate"))
            {
                return typeof(DateTimeOffset);
            }
            else if (Type == "String")
            {
                return typeof(string);
            }
            else if (Value.Contains("Int"))
            {
                return typeof(int);
            }
            return typeof(object);
        }
    }
}