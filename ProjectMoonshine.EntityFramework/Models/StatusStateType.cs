using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("StatusStateType")]
    public class StatusStateType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Cu6";
        [Key]
        public Guid Cu6Key { get; set; }

        public string Cu6Code { get; set; }

        public DateTimeOffset Cu6AddDate { get; set; }

        public DateTimeOffset? Cu6ChangeDate { get; set; }

        public string Cu6AddUser { get; set; }

        public string Cu6ChangeUser { get; set; }

    }
}
