using System;
using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;

namespace ProjectMoonshine.Classes.QueryClasses
{
    public static class QueryExtensions
    {
        static readonly MethodInfo SetMethod = typeof(ApplicationDbContext).GetMethod(nameof(ApplicationDbContext.Set));

        /// <summary>
        /// Allows a database tables to be queries by type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entityType"></param>
        /// <returns></returns>
        public static IQueryable Query(this ApplicationDbContext context, Type entityType)
        {
            return (IQueryable)SetMethod.MakeGenericMethod(entityType).Invoke(context, null);
        }

        /// <summary>
        /// Allows a database tables to be queries by table name
        /// </summary>
        /// <param name="context"></param>
        /// <param name="entityName"></param>
        /// <returns></returns>
        public static IQueryable Query(this ApplicationDbContext context, string entityName)
        {
            return context.Query(context.Model.FindEntityType(entityName).ClrType);
        }
    }
}
