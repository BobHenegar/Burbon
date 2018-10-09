using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Person")]
    public class Person : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Per";
        [Key]
        public Guid PerKey { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CusPerKey { get; set; }

        public string PerFirstName { get; set; }
        public string PerMiddleName { get; set; }

        public string PerLastName { get; set; }

        public DateTimeOffset PerDateOfBirth { get; set; }

        public string PerGender { get; set; }
        public DateTimeOffset PerAddDate { get; set; }
        public DateTimeOffset? PerChangeDate { get; set; }
        public string PerAddUser { get; set; }
        public string PerChangeUser { get; set; }

        #region Navigation Properties

        [ForeignKey("CusPerKey")]
        public virtual Customer Customer { get; set; }
        public virtual IList<Address> Addresses { get; set; }
        public virtual IList<Email> Emails { get; set; }
        public virtual IList<Phone> Phones { get; set; }


        #endregion Navigation Properties
    }
}

