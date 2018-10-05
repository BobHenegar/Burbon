using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public abstract class EntityBase
    {
        public string Name { get; set; }
        public DateTimeOffset AddDate { get; set; }
        public DateTimeOffset? ModifiedDate { get; set; }
        public DateTimeOffset? DeletedDate { get; set; }
    }
}
