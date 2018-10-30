using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Classes.QueryClasses;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Enums;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers
{
    public class QueryManager
    {
        private ApplicationDbContext _context { get; set; }
        private DatabaseMetadata _databaseMetadata { get; set; }

        /// <summary>
        /// public constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="databaseMetadata"></param>
        public QueryManager(ApplicationDbContext context)
        {
            _context = context;
            _databaseMetadata = new DatabaseMetadata(context);
        }

        public QueryViewModel BuildQueryViewModel()
        {
            var model = new QueryViewModel();
            var objects = _databaseMetadata
                .GetTableMetadata()
                .Select(s =>
                new KeyValuePair<string, List<ColumnDefinition>>(
                    s.TableName,
                    s.TableColumns.Select(cd =>
                        new ColumnDefinition()
                        {
                            ColumnName = cd.ColumnName,
                            ColumnDisplayName = cd.ColumnDisplayName,
                            TablePrefix = cd.TablePrefix,
                            TableName = nameof(cd.TableEntity),
                            // String type is inherintly nullable we only care about Nullable<T> types
                            ColumnType = cd.DataType
                        }
                    )
                    .ToList()));
            foreach (var obj in objects)
            {
                model.TableDetails.Add(obj.Key, obj.Value);
            }
            var comparisonList = Enum.GetValues(typeof(EqualityComparisons))
                .Cast<EqualityComparisons>()
                .Select(v => new { Name = v.ToString(), Value = ((int)v).ToString() })
                .ToList();
            foreach (var comparison in comparisonList)
            {
                model.EqualityComparisonsList.Add(comparison.Value, comparison.Name);
            }
            return model;
        }

        public async Task<Expression<Func<T, bool>>> GetExpressionByKey<T>(Guid key) where T : EntityBase
        {
            var query = await _context.Queries.AsNoTracking().Where(x => x.QryKey == key).FirstOrDefaultAsync();
            return GetCompiledExpression<T>(query.QryJsonBody);
        }
        public async Task<Query> GetExpressionByKey(Guid key)
        {
            return await _context.Queries.AsNoTracking().Where(x => x.QryKey == key).FirstOrDefaultAsync();
        }

        public async Task<Expression> GetUncompiledQuery(Guid key)
        {
            var result = await _context.Queries.FirstOrDefaultAsync();
            var json = result.QryJsonBody;
            var parameter = GetExpressionType(json);
            var body = ParseQuery(json, parameter);
            return body;
        }

        private Expression<Func<T, bool>> GetCompiledExpression<T>(string json) where T : EntityBase
        {
            var parameter = GetExpressionType(json);
            var body = ParseQuery(json, parameter);
            return Expression.Lambda<Func<T, bool>>(body, parameter);
        }

        public void DeleteQueryById(Guid id)
        {
            _context.Queries.RemoveRange(_context.Queries.Where(x => x.QryKey == id));
        }

        public ParameterExpression GetExpressionType(JToken json)
        {
            var wrappingLogic = json["logic"] != null && json["logic"].Value<int>() == 1 ? LogicTypes.And : LogicTypes.Or;
            var baseType = json["baseType"].Value<string>();
            var type = _databaseMetadata.GetTable(baseType).TableEntity.ClrType;
            return Expression.Parameter(type, "x");
        }

        public async Task<List<Query>> GetAllQueriesAsync()
        {
            return await _context.Queries.AsNoTracking().ToListAsync();
        }

        public Expression ParseQuery(JToken json, ParameterExpression param)
        {
            var wrappingLogic = json["logic"] != null && json["logic"].Value<int>() == 1 ? LogicTypes.And : LogicTypes.Or;
            var baseType = json["baseType"].Value<string>();


            // Set Parameter for the query
            var result = WalkNode(json["logicList"], param, wrappingLogic);
            return result;
        }
        public void AddQuery(string jsonBody, Query query)
        {
            _context.Queries.Add(new Query()
            {
                QryJsonBody = jsonBody,
                QryName = query.QryName,
                QryTable = query.QryTable
            });
        }

        private Expression WalkNode(JToken node, ParameterExpression baseParameter, LogicTypes outerLogic, Expression lhs = null)
        {
            if (node.Type == JTokenType.Array)
            {
                var last = node.Children().Last();
                Expression rhs = Expression.Constant(true);
                foreach (var obj in node.Children())
                {
                    rhs = WalkNode(obj, baseParameter, outerLogic, null);
                    if (lhs == null)
                    {
                        lhs = rhs;
                    }
                    else
                    {
                        lhs = CombineExpressions(lhs, outerLogic, rhs);
                    }
                }
                return lhs;
            }
            else if (node.Type == JTokenType.Object)
            {
                if (node["logicGroup"] != null)
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
                else if (node["lhs"] != null
                   && node["rhs"] != null)
                {
                    var obj = node.ToObject<LogicExpression>();
                    return BuildExpression(baseParameter, obj.Lhs.Value, obj.Lhs.Type, obj.Logic, obj.Rhs);
                }
            }
            return lhs;
        }

        private LambdaExpression GetPropertyAccessors(ParameterExpression parameter, string propertyName)
        {
            try
            {
                var prefix = propertyName.Substring(0, 3);
                var prefixRouteName = string.Empty;
                if (propertyName.Contains("."))
                {
                    Expression body = parameter;
                    foreach (var prop in propertyName.Split("."))
                    {
                        if (prop != "." && !string.IsNullOrEmpty(prop))
                        {
                            body = Expression.PropertyOrField(body, prop);
                        }
                    }
                    return Expression.Lambda(body, parameter);

                }
                return Expression.Lambda(Expression.PropertyOrField(parameter, propertyName), parameter);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.RecursivelyFormatException());
                Debugger.Break();
                return null;
            }
        }

        private Expression BuildExpression(ParameterExpression expr, string propName, string propType, EqualityComparisons equ, string rhsValue)
        {
            try
            {
                Expression member;
                Expression constant;
                member = Expression.Property(expr, propName);
                constant = Expression.Constant(CreateExpressionConstant(rhsValue, propType));

                Expression expression = null;
                switch (equ)
                {
                    case EqualityComparisons.GreaterThen:
                        {
                            expression = Expression.GreaterThan(member, constant);
                            break;
                        }
                    case EqualityComparisons.GreaterThenOrEqualsTo:
                        {
                            expression = Expression.GreaterThanOrEqual(member, constant);
                            break;
                        }
                    case EqualityComparisons.LessThen:
                        {
                            expression = Expression.LessThan(member, constant);
                            break;
                        }
                    case EqualityComparisons.LessThenOrEqualsTo:
                        {
                            expression = Expression.LessThanOrEqual(member, constant);
                            break;
                        }
                    case EqualityComparisons.IsNotNull:
                        {
                            expression = Expression.NotEqual(member, constant);
                            break;
                        }
                    case EqualityComparisons.Like:
                    case EqualityComparisons.Contains:
                        {
                            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                            expression = Expression.Call(member, containsMethod, constant);
                            break;
                        }
                    case EqualityComparisons.IsNull:
                    case EqualityComparisons.Equals:
                    default:
                        {
                            expression = Expression.Equal(member, constant);
                            break;
                        }
                }
                return expression;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.RecursivelyFormatException());
                Debugger.Break();
                return null;
            }
        }

        private Expression CombineExpressions(Expression lhs, LogicTypes logicType, Expression rhs)
        {
            try
            {
                switch (logicType)
                {
                    case LogicTypes.And:
                        {
                            return Expression.AndAlso(lhs, rhs);
                        }
                    default:
                        {
                            return Expression.OrElse(lhs, rhs);
                        }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.RecursivelyFormatException());
                Debugger.Break();
                return null;
            }
        }

        private object CreateExpressionConstant(string value, string type)
        {
            try
            {
                switch (type)
                {
                    case nameof(Boolean):
                        {
                            var cleanedBool = (value == "1" || value.ToLower() == "true") ? bool.TrueString : bool.FalseString ;
                            return System.Convert.ToBoolean(cleanedBool);
                        }
                    case nameof(Int16):
                        {
                            return System.Convert.ToInt16(value);
                        }
                    case nameof(Int32):
                        {
                            return System.Convert.ToInt32(value);
                        }
                    case nameof(Int64):
                        {
                            return System.Convert.ToInt64(value);
                        }
                    case nameof(DateTimeOffset):
                        {
                            if (string.IsNullOrEmpty(value))
                            {
                                return DateTimeOffset.Now;
                            }
                            else
                            {
                                var newVal = System.Convert.ToDateTime(value);
                                return new DateTimeOffset(newVal);
                            }
                        }
                    case nameof(Guid):
                        {
                            if (value == string.Empty)
                            {
                                return new Guid?();
                            }
                            else
                            {
                                return new Guid?(new Guid(value));
                            }
                        }
                    default:
                        {
                            return value;
                        }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.RecursivelyFormatException());
                Debugger.Break();
                return null;
            }
        }

        private Type Convert(string type)
        {
            try
            {
                switch (type)
                {
                    case "Nullable<Boolean>":
                        {
                            return typeof(bool?);
                        }
                    case nameof(Boolean):
                        {
                            return typeof(bool);
                        }
                    case "Nullable<int>":
                        {
                            return typeof(int?);
                        }
                    case nameof(Int16):
                        {
                            return typeof(int);
                        }
                    case "Nullable<DateTimeOffset>":
                        {
                            return typeof(DateTimeOffset?);
                        }
                    case nameof(DateTimeOffset):
                        {
                            return typeof(DateTimeOffset);
                        }
                    case "Nullable<Guid>":
                        {
                            return typeof(Guid?);
                        }
                    case nameof(Guid):
                        {
                            return typeof(Guid);
                        }
                    default:
                        {
                            return typeof(string);
                        }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.RecursivelyFormatException());
                Debugger.Break();
                return null;
            }
        }
    }

}