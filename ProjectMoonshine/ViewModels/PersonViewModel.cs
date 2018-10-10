using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.ViewModels
{
    public class PersonViewModel
    {
        public Guid PerKey { get; set; }

        [Required]
        public Guid PerCusKey { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string PerFirstName { get; set; }

        [DisplayName("Middle Name")]
        public string PerMiddleName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        public string PerLastName { get; set; }

        [Required]
        [DisplayName("Date of Birth")]
        public DateTimeOffset PerDateOfBirth { get; set; }

        [Required]
        [DisplayName("Gender")]
        public string PerGender { get; set; }
    }
}
