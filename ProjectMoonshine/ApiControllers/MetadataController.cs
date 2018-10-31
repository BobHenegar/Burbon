using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Attributes;
using ProjectMoonshine.EntityFramework.Classes.QueryClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectMoonshine.ApiControllers
{
    [Route("api/[controller]")]
    public class MetadataController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly DatabaseMetadata _databaseMetadata;
        public MetadataController(ApplicationDbContext context)
        {
            _context = context;
            _databaseMetadata = new DatabaseMetadata(context);
        }
        // GET: api/<controller>
        [HttpGet("Tables")]
        public JsonResult GetTables([FromQuery] string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                var result = _databaseMetadata
                    .GetTableMetadata()
                    .Select(td =>
                        new
                        {
                            td.TableName,
                            td.DbSetName,
                        }
                    ).ToList();
                return Json(result);
            }
            else
            {
                var columnDetails = _databaseMetadata
                    .GetTableMetadata()
                    .Where(x => x.TableColumns.Any(a => a.ColumnName == columnName))
                    .FirstOrDefault();

                // If we have column details get the table and process its columns looking for a key and Description attribute
                if (columnDetails != null)
                {
                    var column = columnDetails
                        .TableColumns
                        .Where(x => x.ColumnName == columnName)
                        .FirstOrDefault();
                    if (column.IsForeignKey)
                    {
                        var type = column.ForeinKeyTableEntity.ClrType;
                        var isSelectList = type.GetCustomAttributes(typeof(CanBeSelectListAttribute), false).FirstOrDefault() as CanBeSelectListAttribute;
                        if (isSelectList != null
                                && isSelectList.CanBeSelectList)
                        {
                            PropertyInfo keyProp = type.GetProperties().First(e => e.GetCustomAttributes(false).Any(a => a.GetType() == typeof(KeyAttribute)));
                            PropertyInfo valueProp = type.GetProperties().First(e => e.GetCustomAttributes(false).Any(a => a.GetType() == typeof(CanBeDescriptionAttribute)));
                            // Wizard stuff yo
                            var dbSetMethodInfo = typeof(ApplicationDbContext).GetMethod("Set");
                            var dbSet = dbSetMethodInfo.MakeGenericMethod(type).Invoke(_context, null);
                            var toListMethodInfo = typeof(Enumerable).GetMethod("ToList").MakeGenericMethod(type);
                            var toListMethod = toListMethodInfo.Invoke(dbSet, new[] { dbSet });
                            var results = (dynamic)toListMethod;
                            List<object> response = new List<object>();
                            foreach (var result in results)
                            {
                                var key = type.GetProperty(keyProp.Name).GetValue(result).ToString();
                                var value = type.GetProperty(valueProp.Name).GetValue(result).ToString();
                                response.Add(new
                                {
                                    key,
                                    value
                                });
                            }
                            return Json(response);
                        }
                    }
                }
                return Json("");
            }
        }

        // GET: api/<controller>
        [HttpGet("Columns")]
        public JsonResult GetColumns([FromQuery]string tableName)
        {
            var columns = _databaseMetadata
                .GetTableMetadata()
                .SelectMany(s => s.TableColumns)
                .Select(cd =>
                    new ColumnDefinition()
                    {
                        ColumnName = cd.ColumnName,
                        ColumnDisplayName = cd.ColumnDisplayName,
                        TablePrefix = cd.TablePrefix,
                        TableName = cd.TableEntity.ClrType.Name,
                        ColumnType = cd.DataType
                    }
                ).AsQueryable();
            if (!string.IsNullOrEmpty(tableName))
            {
                columns = columns.Where(x => x.TableName == tableName);
            }
            var result = columns.ToList();
            return Json(result);
        }
    }
}
