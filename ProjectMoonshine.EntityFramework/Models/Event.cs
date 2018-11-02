using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class Event : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Evt";
        [Key]
        public Guid EvtKey { get; set; }

        [DisplayName("Event Description")]
        [Required]
        [MinLength(10)]
        [MaxLength(500)]
        public string EvtDescription { get; set; }

        [DisplayName("Event Name")]
        [Required]
        [MinLength(1)]
        [MaxLength(500)]
        public string EvtName { get; set; }

        [DisplayName("Event Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTimeOffset? EvtStartDate { get; set; }

        [DisplayName("Event End Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTimeOffset? EvtEndDate { get; set; }

        [DisplayName("Exam")]
        public Guid? EvtExmKey { get; set; }


        [DisplayName("Registrant Limit")]
        public int? EvtRegistrantLimit { get; set; }


        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset EvtAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? EvtChangeDate { get; set; }

        [DisplayName("Added By")]
        public string EvtAddUser { get; set; }


        [DisplayName("Changed By")]
        public string EvtChangeUser { get; set; }

        #region Navigation Properties
        public virtual IList<EventRegistration> EventRegistrations { get; set; }

        [ForeignKey("EvtExmKey")]
        public virtual Exam Exam { get; set; }
        #endregion Navigation Properties
    }
}
