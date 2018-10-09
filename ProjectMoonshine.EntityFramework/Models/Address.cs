using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Address")]
    public class Address : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Adr";

        [Key]
        public Guid AdrKey { get; set; }


        public Guid AdrPerKey { get; set; }

        [StringLength(50, MinimumLength = 0)]
        public string AdrCity { get; set; }

        [StringLength(75, MinimumLength = 0)]
        public string AdrState { get; set; }

        [StringLength(10, MinimumLength = 1)]
        public string AdrZip { get; set; }

        [StringLength(500, MinimumLength = 1)]
        public string AdrAddress1 { get; set; }

        [StringLength(maximumLength: 500)]
        public string AdrAddress2 { get; set; }

        [StringLength(maximumLength: 500)]
        public string AdrAddress3 { get; set; }

        [DefaultValue(false)]
        public bool AdrIsPrimaryAddress { get; set; }

        [DefaultValue(false)]
        public bool AdrIsMailingAddress { get; set; }

        [DefaultValue(false)]
        public bool AdrIsWorkAddress { get; set; }

        [DefaultValue(false)]
        public bool AdrIsHomeAddress { get; set; }

        public DateTimeOffset AdrAddDate { get; set; }
        public DateTimeOffset? AdrChangeDate { get; set; }
        public string AdrAddUser { get; set; }
        public string AdrChangeUser { get; set; }


        #region Navigation Properties
        [ForeignKey("AdrPerKey")]
        public virtual Person AdrPerson {get;set; }
        #endregion Navigation Properties
    }
}
