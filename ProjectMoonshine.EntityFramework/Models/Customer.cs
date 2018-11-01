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


        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset CusAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? CusChangeDate { get; set; }

        [DisplayName("Added By")]
        public string CusAddUser { get; set; }

        [DisplayName("Changed By")]
        public string CusChangeUser { get; set; }

        public virtual Person Person { get; set; }
        public virtual IList<Credential> Credentials { get; set; }
        public virtual IList<ExamScore> ExamScores { get; set; }
        public virtual IList<EventRegistration> EventRegistrations { get; set; }

        public virtual IList<License> Licenses { get; set; }

        public virtual IList<Degree> Degrees { get; set; }

        public virtual IList<CEDetail> CEDetails { get; set; }

        
    }
}
