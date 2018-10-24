using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProjectMoonshine.EntityFramework;

namespace ProjectMoonshine.Classes.QueryClasses
{
    public class DatabaseMetadata
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly TableNameLookup _tableNameLookup;
        private string _databaseName;
        private IEnumerable<TableMetadata> _tables;

        public DatabaseMetadata(ApplicationDbContext dbContext, TableNameLookup tableNameLookup)
        {
            _dbContext = dbContext;
            _tableNameLookup = tableNameLookup;
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
            return _tables.First(x => x.Columns.Any(a => a.ColumnName == columnName));
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
                var column = _tables.First(x => x.Columns.Any(a => a.ColumnName == columnName)).Columns.First(x => x.ColumnName == columnName);
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
                    AssemblyFullName = entityType.ClrType.FullName,
                    Columns = GetColumnsMetadata(entityType)
                });
                _tableNameLookup.InsertKeyName(tableName);
            }

            return metaTables;
        }
        private IReadOnlyList<ColumnMetadata> GetColumnsMetadata(IEntityType entityType)
        {
            var tableColumns = new List<ColumnMetadata>();
            foreach (var propertyType in entityType.GetProperties())
            {
                var relational = propertyType.Relational();
                tableColumns.Add(new ColumnMetadata
                {
                    ColumnName = relational.ColumnName,
                    DataType = relational.ColumnType,
                    TableEntity = entityType
                });
            }
            return tableColumns;
        }
    }
}
