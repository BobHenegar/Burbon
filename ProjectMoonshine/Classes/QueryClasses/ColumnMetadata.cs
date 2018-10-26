using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ProjectMoonshine.Classes.QueryClasses
{
    public class ColumnMetadata
    {
        public string ColumnName { get; set; }
        public string TablePrefix { get; set; }
        public string DataType { get; set; }
        public IEntityType TableEntity { get; set; }
    }
}
