using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class ExamScore : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Exs";

        [Key]
        public Guid ExsKey { get; set; }
        public Guid ExsExmKey { get; set; }
        public Guid ExsCusKey { get; set; }


        [DisplayName("Total Possible")]
        public int ExsTotalPossible { get; set; }
        [DisplayName("Total Received")]
        public int ExsTotalReceived { get; set; }


        public DateTimeOffset ExsAddDate { get; set; }
        public DateTimeOffset? ExsChangeDate { get; set; }
        public string ExsAddUser { get; set; }
        public string ExsChangeUser { get; set; }


        #region Navigation Properties
        [ForeignKey("ExsCusKey")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("ExsExmKey")]
        public virtual Exam Exam { get; set; }
        #endregion Navigation Properties
    }
}
