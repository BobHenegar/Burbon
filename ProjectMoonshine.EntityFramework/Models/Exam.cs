using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class Exam : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Exm";


        [Key]
        public Guid ExmKey { get; set; }

        [DisplayName("Event")]
        public Guid? ExmEvtKey { get; set; }


        [DisplayName("Exam Seating Limit")]
        public int? ExmSeatCount { get; set; }

        [DisplayName("Exam Name")]
        public string ExmName{ get; set; }

        [DisplayName("Allow Retakes")]
        [DefaultValue(true)]
        public bool ExmCanRetake { get; set; }



        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset ExmAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? ExmChangeDate { get; set; }

        [DisplayName("Added By")]
        public string ExmAddUser { get; set; }


        [DisplayName("Changed By")]
        public string ExmChangeUser { get; set; }


        #region Navigation Properties
        public virtual IList<ExamScore> ExamScores { get; set; }
        [ForeignKey("ExmEvtKey")]
        public virtual Event Event { get; set; }
        #endregion Navigation Properties
    }
}
