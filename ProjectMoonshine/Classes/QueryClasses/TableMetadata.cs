using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectMoonshine.Classes.QueryClasses
{
    public class TableMetadata
    {
        public string TableName { get; set; }
        public IEntityType TableEntity { get; set; }
        public string TableAssemblyFullName { get; set; }
        public IEnumerable<ColumnMetadata> TableColumns { get; set; }
    }
}
