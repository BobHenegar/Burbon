using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectMoonshine.ViewModels
{
    public class PersonViewModel : BaseViewModel
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

        [DisplayName("Mailing")]
        [DefaultValue(false)]
        public bool AdrIsMailing { get; set; }


        [DisplayName("Work")]
        [DefaultValue(false)]
        public bool AdrIsWork { get; set; }


        [DisplayName("Home")]
        [DefaultValue(false)]
        public bool AdrIsHome { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoNumber { get; set; }

        [DisplayName("Extension")]
        public string PhoExtention { get; set; }

        [DisplayName("Is Primary Phone Number?")]
        [DefaultValue(true)]
        public bool PhoIsPrimary { get; set; }


        [DisplayName("Home Phone")]
        [DefaultValue(false)]
        public bool PhoIsHome { get; set; }

        [DisplayName("Mobile Phone")]
        [DefaultValue(false)]
        public bool PhoIsMobile { get; set; }

        [DisplayName("Work Phone")]
        [DefaultValue(false)]
        public bool PhoIsWork { get; set; }

        [DisplayName("Email Address")]
        public string EmlAddress { get; set; }

        [DisplayName("Work")]
        [DefaultValue(false)]
        public bool EmlIsWork { get; set; }

        [DisplayName("Personal")]
        [DefaultValue(false)]
        public bool EmlIsPersonal { get; set; }
    }
}
