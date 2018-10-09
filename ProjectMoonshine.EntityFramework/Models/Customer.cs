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
        public Guid CusPerKey { get; set; }
        public DateTimeOffset CstAddDate { get; set; }
        public DateTimeOffset? CstChangeDate { get; set; }
        public string CstAddUser { get; set; }
        public string CstChangeUser { get; set; }

        #region Navigation Properties
        [ForeignKey("CusPerKey")]
        public virtual Person Person { get; set; }
        #endregion Navigation Properties
    }
}
