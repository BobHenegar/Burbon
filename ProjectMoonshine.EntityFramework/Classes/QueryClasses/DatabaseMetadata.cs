using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectMoonshine.EntityFramework;

namespace ProjectMoonshine.EntityFramework.Classes.QueryClasses
{
    public class DatabaseMetadata
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TableNameLookup _tableNameLookup;
        private string _databaseName;
        private IEnumerable<TableMetadata> _tables;

        public DatabaseMetadata(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _tableNameLookup = new TableNameLookup();
            _databaseName = _dbContext.Database.GetDbConnection().Database;
            if (_tables == null)
            {
                ReloadMetatdata();
            }
        }

        /// <summary>
        /// Gets the table metatadata for the applications DbContext
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TableMetadata> GetTableMetadata()
        {
            if (_tables == null)
            {
                return new List<TableMetadata>();
            }
            return _tables;
        }
        public void ReloadMetatdata()
        {
            _tables = FetchTablesMetaData();
        }

        /// <summary>
        /// Gets a table Entity Type by table name
        /// </summary>
        /// <param name="tableName"></param>
        /// <example>"" or ""</example>
        /// <returns></returns>
        public TableMetadata GetTable(string tableName)
        {
            return _tables.First(x => x.TableName == tableName);
        }

        /// <summary>
        /// Gets a tables property by type
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public object TableProperty(string columnName)
        {
            return _tables.First(x => x.TableColumns.Any(a => a.ColumnName == columnName));
        }

        /// <summary>
        /// Gets a columns type by string
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public Type GetPropertyType(string columnName)
        {
            try
            {
                var column = _tables.First(x => x.TableColumns.Any(a => a.ColumnName == columnName)).TableColumns.First(x => x.ColumnName == columnName);
                Type result = Type.GetType(column.DataType);
                return result;
            }
            catch(Exception)
            {
                return null;
            }
        }

        private IReadOnlyList<TableMetadata> FetchTablesMetaData()
        {
            var metaTables = new List<TableMetadata>();
            foreach (var entityType in _dbContext.Model.GetEntityTypes())
            {
                var tableName = entityType.Relational().TableName;
                metaTables.Add(new TableMetadata
                {
                    TableName = tableName,
                    TableEntity = entityType,
                    TableAssemblyFullName = entityType.ClrType.FullName,
                    TableColumns = GetColumnsMetadata(entityType, tableName)
                });
                _tableNameLookup.InsertKeyName(tableName);
            }

            return metaTables;
        }
        private IReadOnlyList<ColumnMetadata> GetColumnsMetadata(IEntityType entityType, string tableName)
        {
            var tableColumns = new List<ColumnMetadata>();
            foreach (var propertyType in entityType.GetProperties())
            {
                var relational = propertyType.Relational();
                tableColumns.Add(new ColumnMetadata
                {
                    ColumnName = propertyType.Name,
                    ColumnDisplayName = GetPropertyDisplayName(entityType.ClrType, relational.ColumnName, tableName),
                    TablePrefix = GetTablePrefix(entityType),
                    DataType = propertyType.IsNullable
                                && Nullable.GetUnderlyingType(propertyType.ClrType) != null ?
                                $"Nullable<{Nullable.GetUnderlyingType(propertyType.ClrType).Name}>" : propertyType.ClrType.Name,
                    TableEntity = entityType
                });
            }
            return tableColumns;

        }
        private static string GetPropertyDisplayName(Type baseType, string propertyName, string tableName)
        {
            var displayName = string.Empty;
            var prop = baseType
                .GetProperty(propertyName);
            var displayNameAttribute = prop
                .GetCustomAttributes(false)
                .FirstOrDefault(x => x.GetType() == typeof(DisplayNameAttribute));
            if (displayNameAttribute == null)
            {
                if (propertyName.Contains("Key"))
                {
                    displayName = $"{tableName} Primary Key";
                }
                else if (propertyName.Contains("AddDate"))
                {
                    displayName = $"{tableName} Add Date";
                }
                else if (propertyName.Contains("AddUser"))
                {
                    displayName = $"{tableName} Added By";
                }
                else if (propertyName.Contains("ChangeDate"))
                {
                    displayName = $"{tableName} Change Date";
                }
                else if (propertyName.Contains("ChangeUser"))
                {
                    displayName = $"{tableName} Changed By";
                }
            }
            else
            {
                displayName = ((DisplayNameAttribute)displayNameAttribute).DisplayName;

                if (!displayName.ToLower().Contains(tableName.ToLower()))
                {
                    displayName = $"{tableName} {displayName}";
                }
            }
            return displayName;
        }
        private static string GetTablePrefix(IEntityType src)
        {
            return src.ClrType.UnderlyingSystemType.GetProperties().First().Name.Substring(0, 3);
        }
    }
}
