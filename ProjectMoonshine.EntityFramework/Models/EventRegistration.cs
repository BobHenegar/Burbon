using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class EventRegistration : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Evr";


        [Key]
        public Guid EvrKey { get; set; }


        [DisplayName("Event")]
        public Guid EvrEvtKey { get; set; }

        [DisplayName("Customer")]
        public Guid EvrCusKey { get; set; }

        [DisplayName("Drop Date")]
        public DateTimeOffset? EvrDropDate { get; set; }


        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset EvrAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? EvrChangeDate { get; set; }

        [DisplayName("Added By")]
        public string EvrAddUser { get; set; }


        [DisplayName("Changed By")]
        public string EvrChangeUser { get; set; }

        #region Navigation Properties
        [ForeignKey("EvrEvtKey")]
        public virtual Event Event { get; set; }
        [ForeignKey("EvrCusKey")]
        public virtual Customer Customer { get; set; }
        #endregion Navigation Properties
    }
}
