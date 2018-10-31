using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectMoonshine.EntityFramework.Classes.QueryClasses
{
    public class ColumnMetadata
    {
        public string ColumnName { get; set; }
        public bool IsForeignKey { get; set; }
        public string ColumnDisplayName { get; set; }
        public string TablePrefix { get; set; }
        public string DataType { get; set; }
        public IEntityType TableEntity { get; set; }
        public IEntityType ForeinKeyTableEntity { get; set; }
    }
}
