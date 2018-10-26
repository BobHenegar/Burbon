using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProjectMoonshine.Classes.QueryClasses;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Enums;
using ProjectMoonshine.ViewModels;

namespace ProjectMoonshine.Managers
{
    public class QueryManager
    {
        private ApplicationDbContext _context { get; set; }
        private DatabaseMetadata _databaseMetadata { get; set; }
        private Dictionary<Guid, Expression> _knownExpressions { get; set; }
        private Dictionary<string, LambdaExpression> _tableRoutes { get; set; }
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
            _tableRoutes = new Dictionary<string, LambdaExpression>();
            _tableRoutes.Add(CredentialProcess.Prefix, CreateExpression(typeof(CredentialRequirement), "CredentialProcess"));
            _tableRoutes.Add(CredentialRequirementType.Prefix, CreateExpression(typeof(CredentialRequirement), "CredentialRequirementType"));
            _tableRoutes.Add("Cr3Ct6Key", CreateExpression(typeof(CredentialRequirement), "Status"));
            _tableRoutes.Add("Cr3Cr3Key", CreateExpression(typeof(CredentialRequirement), "ParentRequirement"));
            _tableRoutes.Add(CredentialRequirement.Prefix, CreateExpression(typeof(CredentialRequirement), "DependentRequirements"));
            _tableRoutes.Add("Cr1CurrentCr2Key", CreateExpression(typeof(CredentialRequirement), "CredentialProcess.Credential.CurrentCredentialProcess"));
            _tableRoutes.Add("Cr1Ct2Key", CreateExpression(typeof(CredentialRequirement), "CredentialProcess.Credential.CurrentCredentialProcessType"));
            _tableRoutes.Add(ExamScore.Prefix, CreateExpression(typeof(CredentialRequirement), "CredentialProcess.Credential.Customer.ExamScores"));
            _tableRoutes.Add(EventRegistration.Prefix, CreateExpression(typeof(CredentialRequirement), "CredentialProcess.Credential.Customer.EventRegistrations"));
            _tableRoutes.Add(Email.Prefix, CreateExpression(typeof(CredentialRequirement), "CredentialProcess.Credential.Customer.Person.Emails"));
            _tableRoutes.Add(FileUpload.Prefix, CreateExpression(typeof(CredentialRequirement), "CredentialProcess.Credential.Customer.Person.FileUploads"));
            _tableRoutes.Add(Phone.Prefix, CreateExpression(typeof(CredentialRequirement), "CredentialProcess.Credential.Customer.Person.Phones"));
            _tableRoutes.Add(Address.Prefix, CreateExpression(typeof(CredentialRequirement), "CredentialProcess.Credential.Customer.Person.Addresses"));
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
                            TablePrefix = cd.TablePrefix,
                            // String type is inherintly nullable we only care about <object>? types
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
            foreach(var comparison in comparisonList)
            {
                model.EqualityComparisonsList.Add(comparison.Value, comparison.Name);
            }
            return model;
        }
        //public IQueryable<T> GetStoredExpression<T>(IQueryable<T> context, Guid Id)
        //{ }
        public IQueryable<Credential> ParseCredentialQuery(string json)
        {
            JToken nodeList = JToken.Parse(json);
            var wrappingLogic = nodeList["logic"] != null && nodeList["logic"].Value<int>() == 1 ? LogicTypes.And : LogicTypes.Or;
            var includes = nodeList["includes"].Values<string>().ToList();
            var baseType = nodeList["baseType"].Value<string>();

            var queryableType = _context
                .CredentialRequirements
                // CredentialProcess
                .Include(i => i.CredentialProcess)
                // CredentialRequirementType
                .Include(i => i.CredentialRequirementType)
                // Status
                .Include(i => i.Status)
                // ParentRequirement
                .Include(i => i.ParentRequirement)
                // DependentRequirements
                .Include(i => i.DependentRequirements)
                // CredentialProcess.Credential.CurrentCredentialProcess
                .Include(i => "CredentialProcess.Credential.CurrentCredentialProcess")
                // CredentialProcess.Credential.CurrentCredentialProcessType
                .Include(i => "CredentialProcess.Credential.CurrentCredentialProcessType")
                // CredentialProcess.Credential.Customer.ExamScores
                .Include(i => "CredentialProcess.Credential.ExamScores")
                // CredentialProcess.Credential.Customer.EventRegistrations
                .Include(i => "CredentialProcess.Credential.EventRegistrations")
                // CredentialProcess.Credential.Customer.Person.Emails
                .Include(i => "CredentialProcess.Credential.Customer.Person.Emails")
                // CredentialProcess.Credential.Customer.Person.FileUploads
                .Include(i => "CredentialProcess.Credential.Customer.Person.FileUploads")
                // CredentialProcess.Credential.Customer.Person.Phones
                .Include(i => "CredentialProcess.Credential.Customer.Person.Phones")
                // CredentialProcess.Credential.Customer.Person.Addresses
                .Include(i => "CredentialProcess.Credential.Customer.Person.Addresses")
                // AsQueryable
                .AsQueryable();

            // GetAllIncludes

            // Set Parameter for the query
            var result = WalkNode(nodeList["logicList"], paramExpression, wrappingLogic);

            return result;
        }
        static LambdaExpression CreateExpression(Type type, string propertyName)
        {
            var param = Expression.Parameter(type, "x");
            Expression body = param;
            foreach (var member in propertyName.Split('.'))
            {
                body = Expression.PropertyOrField(body, member);
            }
            return Expression.Lambda(body, param);
        }

        public Expression GetQueryById(Guid key)
        {
            var result = _context.Queries.FirstOrDefault(x => x.QryKey == key);
            return ParseQuery(result.QryJsonBody);
        }

        public void CreateQuery(string json)
        {
            _context.Queries.Add(new Query() { QryJsonBody =  json});
            _context.SaveChanges();
        }

        private Expression WalkNode(JToken node, ParameterExpression baseParameter, LogicTypes outerLogic, Expression lhs = null)
        {
            if (node.Type == JTokenType.Array)
            {
                var last = node.Children().Last();

                foreach (var obj in node.Children())
                {
                    lhs = WalkNode(obj, baseParameter, outerLogic, lhs);
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
                case nameof(Int32):
                    {
                        return System.Convert.ToInt32(value);
                    }
                case nameof(Int64):
                    {
                        return System.Convert.ToInt64(value);
                    }
                case nameof(DateTime):
                case nameof(DateTimeOffset):
                    {
                        var newVal = System.Convert.ToDateTime(value);
                        return new DateTimeOffset(newVal);
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
        private Type Convert(string type)
        {
            switch (type)
            {
                case "Nullable<Boolean>":
                case nameof(Boolean):
                    {
                        return typeof(bool);
                    }
                case "Nullable<int>":
                case nameof(Int16):
                    {
                        return typeof(int);
                    }
                case "Nullable<DateTimeOffset>":
                case "Nullable<DateTime>":
                case nameof(DateTimeOffset):
                case nameof(DateTime):
                    {
                        return typeof(DateTimeOffset);
                    }
                case "Nullable<Guid>":
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
        private Expression BuildConstantExpression(ParameterExpression expr, string propertyName, string constantValue, EqualityComparisons equality, string propertyType)
        {

            var valueOject = Convert(constantValue, propertyType);
            var castType = Convert(propertyType);
            if (equality == EqualityComparisons.IsNotNull
                || equality == EqualityComparisons.IsNull)
            {
                var left = Expression.Property(expr, propertyName);
                var right = Expression.Convert(Expression.Constant(null), castType);
                //build the expression
                return CombineExpressions(left, equality, right);
            }
            if (equality == EqualityComparisons.Like)
            {
                var left = Expression.Property(expr, propertyName);
                //if null ignore on like
                if (valueOject == null)
                {
                    return Expression.Equal(Expression.Constant(true), Expression.Constant(true));
                }
                var right = Expression.Constant(valueOject ?? "", castType);
                //build the expression
                var lh = Expression.Call(left, typeof(string).GetMethod("StartsWith", new[] { typeof(string) }), right);
                var rh = Expression.Call(left, typeof(string).GetMethod("EndsWith", new[] { typeof(string) }), right);
                return Expression.Or(lh, right);
            }
            if(equality == EqualityComparisons.Contains)
            {
                var left = Expression.Property(expr, propertyName);
                var right = Expression.Constant(valueOject ?? "", castType);
                //build the expression
                return Expression.Call(left, typeof(string).GetMethod("Contains", new[] { typeof(string) }), right);
            }
            else
            {
                var left = Expression.Property(expr, propertyName);
                var right = Expression.Constant(valueOject, castType);
                //build the expression
                return CombineExpressions(left, equality, right);
            }
        }
        private Expression CombineExpressions(Expression lhs, EqualityComparisons equality, Expression rhs)
        {
            switch (equality)
            {
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
                case EqualityComparisons.LessThanOrEqualsTo:
                    {
                        return Expression.LessThanOrEqual(lhs, rhs);
                    }
                case EqualityComparisons.IsNotNull:
                    {
                        return Expression.NotEqual(lhs, rhs);
                    }
                case EqualityComparisons.IsNull:
                case EqualityComparisons.Equals:
                default:
                    {
                        return Expression.Equal(lhs, rhs);
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