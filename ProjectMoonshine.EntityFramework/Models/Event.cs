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
        public DateTimeOffset? EvtStartDate { get; set; }

        [DisplayName("Event End Date")]
        public DateTimeOffset? EvtEndDate { get; set; }
        [DisplayName("Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]

        public DateTimeOffset EvtAddDate { get; set; }
        public DateTimeOffset? EvtChangeDate { get; set; }
        public string EvtAddUser { get; set; }
        public string EvtChangeUser { get; set; }

        #region Navigation Properties
        public virtual IList<EventRegistration> EventRegistrations { get; set; }
        #endregion Navigation Properties
    }
}
