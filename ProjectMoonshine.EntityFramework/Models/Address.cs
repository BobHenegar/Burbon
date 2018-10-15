using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Address")]
    public class Address : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Adr";

        [Key]
        public Guid AdrKey { get; set; }

        [DisplayName("Person")]
        public Guid AdrPerKey { get; set; }

        [DisplayName("City")]
        [StringLength(50, MinimumLength = 0)]
        public string AdrCity { get; set; }

        [DisplayName("State")]
        [StringLength(75, MinimumLength = 0)]
        public string AdrState { get; set; }

        [DisplayName("Zip")]
        [StringLength(10, MinimumLength = 1)]
        public string AdrZip { get; set; }

        [DisplayName("Address1")]
        [StringLength(500, MinimumLength = 1)]
        public string AdrAddress1 { get; set; }

        [DisplayName("Address2")]
        [StringLength(maximumLength: 500)]
        public string AdrAddress2 { get; set; }

        [DisplayName("Address3")]
        [StringLength(maximumLength: 500)]
        public string AdrAddress3 { get; set; }

        [DisplayName("Is Primary Address?")]
        [DefaultValue(false)]
        public bool AdrIsPrimaryAddress { get; set; }

        [DisplayName("Is Mailing Address?")]
        [DefaultValue(false)]
        public bool AdrIsMailingAddress { get; set; }


        [DisplayName("Is Work Address?")]
        [DefaultValue(false)]
        public bool AdrIsWorkAddress { get; set; }


        [DisplayName("Is Home Address?")]
        [DefaultValue(false)]
        public bool AdrIsHomeAddress { get; set; }
        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset AdrAddDate { get; set; }
        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? AdrChangeDate { get; set; }
        [DisplayName("Added By")]
        public string AdrAddUser { get; set; }
        [DisplayName("Changed By")]
        public string AdrChangeUser { get; set; }


        #region Navigation Properties
        [ForeignKey("AdrPerKey")]
        public virtual Person Person {get;set; }
        #endregion Navigation Properties
    }
}
