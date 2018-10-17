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


        [DisplayName("Exam")]
        public Guid ExsExmKey { get; set; }


        [DisplayName("Customer")]
        public Guid ExsCusKey { get; set; }


        [DisplayName("Total Possible")]
        public int ExsTotalPossible { get; set; }
        [DisplayName("Total Received")]
        public int ExsTotalReceived { get; set; }



        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset ExsAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? ExsChangeDate { get; set; }

        [DisplayName("Added By")]
        public string ExsAddUser { get; set; }


        [DisplayName("Changed By")]
        public string ExsChangeUser { get; set; }


        #region Navigation Properties
        [ForeignKey("ExsCusKey")]
        public virtual Customer Customer { get; set; }
        [ForeignKey("ExsExmKey")]
        public virtual Exam Exam { get; set; }
        #endregion Navigation Properties
    }
}
