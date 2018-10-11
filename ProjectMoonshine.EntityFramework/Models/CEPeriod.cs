using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CEPeriod")]
    public class CEPeriod : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ce2";
        [Key]
        public Guid Ce2Key { get; set; }

        public Guid Ce2CusKey { get; set; }

        public DateTimeOffset Ce2StartDate { get; set; }

        public DateTimeOffset Ce2EndDate { get; set; }

        public DateTimeOffset Ce2AddDate { get; set; }

        public DateTimeOffset? Ce2ChangeDate { get; set; }

        public string Ce2AddUser { get; set; }

        public string Ce2ChangeUser { get; set; }

        [ForeignKey("Ce2CusKey")]
        public virtual Customer Customer { get; set; }
    }
}
