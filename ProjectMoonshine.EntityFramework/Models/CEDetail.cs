using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CEDetail")]
    public class CEDetail: EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ce1";
        [Key]
        public Guid Ce1Key { get; set; }

        public Guid Ce1CusKey { get; set; }

        public DateTimeOffset Ce1ActivityDate { get; set; }

        [Required]
        [DisplayName("Activity Description")]
        public string Ce1ActivityDescription { get; set; }

        public float Ce1Units { get; set; }

        public DateTimeOffset Ce1AddDate { get; set; }

        public DateTimeOffset? Ce1ChangeDate { get; set; }

        public string Ce1AddUser { get; set; }

        public string Ce1ChangeUser { get; set; }

        [ForeignKey("Ce1CusKey")]
        public virtual Customer Customer { get; set; }
    }
}
