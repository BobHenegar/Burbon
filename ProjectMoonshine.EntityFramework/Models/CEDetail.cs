using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [DisplayName("Activity Date")]
        public DateTimeOffset Ce1ActivityDate { get; set; }

        [Required]
        [DisplayName("Activity Description")]
        public Guid Ce1CetKey { get; set; }

        [Required]
        [DisplayName("Activity Description")]
        public string Ce1ActivityDescription { get; set; }

        [DisplayName("Units")]
        public float Ce1Units { get; set; }

        [DisplayName("Verified Flag")]
        public bool Ce1VerifiedFlag { get; set; }


        public DateTimeOffset Ce1AddDate { get; set; }

        public DateTimeOffset? Ce1ChangeDate { get; set; }

        public string Ce1AddUser { get; set; }

        public string Ce1ChangeUser { get; set; }

        [ForeignKey("Ce1CusKey")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("Ce1CetKey")]
        public virtual CEType CeType { get; set; }
    }
}
