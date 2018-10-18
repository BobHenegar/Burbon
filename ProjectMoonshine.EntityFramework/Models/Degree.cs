using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class Degree
    {
        [NotMapped]
        public const string Prefix = "Deg";
        [Key]
        public Guid DegKey { get; set; }

        public Guid DegCusKey { get; set; }

        [DisplayName("Institution")]
        public string DegInstitution { get; set; }

        [DisplayName("Graduation Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public string DegDate { get; set; }

        [DisplayName("Add User")]
        public string DegAddUser { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset DegAddDate { get; set; }

        [DisplayName("Change User")]
        public string DegChangeUser { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset DegChangeDate { get; set; }

        [DisplayName("Degree Type")]
        public Guid DegDetKey { get; set; }
    }
}
