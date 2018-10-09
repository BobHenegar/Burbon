using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Customer")]
    public class Customer : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Cus";
        [Key]
        public Guid CusKey { get; set; }
        public DateTimeOffset CusAddDate { get; set; }
        public DateTimeOffset? CusChangeDate { get; set; }
        public string CusAddUser { get; set; }
        public string CusChangeUser { get; set; }
    }
}
