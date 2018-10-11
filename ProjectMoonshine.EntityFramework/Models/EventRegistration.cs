using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class EventRegistration : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Evr";

        [Key]
        public Guid EvrKey { get; set; }
        public Guid EvrEvtKey { get; set; }
        public Guid EvrCusKey { get; set; }

        [DisplayName("Drop Date")]
        public DateTimeOffset? EvrDropDate { get; set; }

        public DateTimeOffset EvrAddDate { get; set; }
        public DateTimeOffset? EvrChangeDate { get; set; }
        public string EvrAddUser { get; set; }
        public string EvrChangeUser { get; set; }

        #region Navigation Properties
        [ForeignKey("EvrEvtKey")]
        public virtual Event Event { get; set; }
        public virtual Customer Customer { get; set; }
        #endregion Navigation Properties
    }
}
