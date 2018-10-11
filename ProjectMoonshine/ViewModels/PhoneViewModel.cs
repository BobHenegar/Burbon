using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectMoonshine.ViewModels
{
    public class PhoneViewModel
    {
        [Required]
        public Guid PhoKey { get; set; }
        [Required]
        public Guid PhoPerKey { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoNumber { get; set; }

        [DisplayName("Extension")]
        public string PhoExtention { get; set; }

        [DisplayName("Is Primary Phone Number?")]
        [DefaultValue(false)]
        public bool PhoIsPrimary { get; set; }

        [DisplayName("Is Home Phone?")]
        [DefaultValue(false)]
        public bool PhoIsHome { get; set; }

        [DisplayName("Is Mobile Phone?")]
        [DefaultValue(false)]
        public bool PhoIsMobile { get; set; }

        [DisplayName("Is Work Phone?")]
        [DefaultValue(false)]
        public bool PhoIsWork { get; set; }
    }
}
