using System.Collections.Generic;

namespace ProjectMoonshine.ViewModels
{
    public class QueryViewModel
    {
        public QueryViewModel()
        {
            TableDetails = new Dictionary<string, List<ColumnDefinition>>();
        }
        public Dictionary<string, List<ColumnDefinition>> TableDetails { get; set; }
    }
    public class ColumnDefinition
    {
        public string ColumnName { get; set; }
        public string ColumnType { get; set; }
    }
}
