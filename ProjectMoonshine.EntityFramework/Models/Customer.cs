using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public string CusAddUser { get; set; }
        public string CusChangeUser { get; set; }

        public virtual Person Person { get; set; }
        public virtual List<Credential> Credentials { get; set; }
    }
}
