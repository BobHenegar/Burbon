using System.Collections.Generic;

namespace ProjectMoonshine.EntityFramework.Classes.QueryClasses
{
    public class QueryViewModel
    {
        public QueryViewModel()
        {
            TableDetails = new Dictionary<string, List<ColumnDefinition>>();
            EqualityComparisonsList = new Dictionary<string, string>();
        }
        public Dictionary<string, List<ColumnDefinition>> TableDetails { get; set; }
        public Dictionary<string, string> EqualityComparisonsList { get; set; }
    }
    public class ColumnDefinition
    {
        public string ColumnName { get; set; }
        public string ColumnDisplayName { get; set; }
        public string TablePrefix { get; set; }
        public string TableName { get; set; }
        public string ColumnType { get; set; }
    }
}
