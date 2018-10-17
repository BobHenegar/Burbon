using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Person")]
    public class Person : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Per";
        [Key]
        public Guid PerKey { get; set; }


        [DisplayName("Customer")]
        public Guid PerCusKey { get; set; }

        [Required, DisplayName("First Name")]
        public string PerFirstName { get; set; }

        [DisplayName("Middle Name")]
        public string PerMiddleName { get; set; }

        [Required, DisplayName("Last Name")]
        public string PerLastName { get; set; }

        [Required, DisplayName("Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset PerDateOfBirth { get; set; }

        [Required, DisplayName("Gender")]
        public string PerGender { get; set; }


        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset PerAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? PerChangeDate { get; set; }

        [DisplayName("Added By")]
        public string PerAddUser { get; set; }


        [DisplayName("Changed By")]
        public string PerChangeUser { get; set; }

        [NotMapped]
        [DisplayName("First Middle Last")]
        public string PerFirstMiddleLast { get => $"{PerFirstName} {PerMiddleName} {PerLastName}"; }

        #region Navigation Properties

        [ForeignKey("PerCusKey")]
        public virtual Customer Customer { get; set; }
        public virtual IList<Address> Addresses { get; set; }
        public virtual IList<Email> Emails { get; set; }
        public virtual IList<Phone> Phones { get; set; }


        #endregion Navigation Properties
    }
}

