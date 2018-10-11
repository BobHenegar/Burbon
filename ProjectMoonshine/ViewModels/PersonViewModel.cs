using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
