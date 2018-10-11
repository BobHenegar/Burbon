using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        [Required]
        [DisplayName("State Code")]
        public string Cu6Code { get; set; }

        [DisplayName("Add Date")]
        public DateTimeOffset Cu6AddDate { get; set; }

        [DisplayName("Change Date")]
        public DateTimeOffset? Cu6ChangeDate { get; set; }

        [DisplayName("Add User")]
        public string Cu6AddUser { get; set; }

        [DisplayName("Change User")]
        public string Cu6ChangeUser { get; set; }

    }
}
