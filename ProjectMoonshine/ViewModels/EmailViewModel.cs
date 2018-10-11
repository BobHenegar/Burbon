using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectMoonshine.ViewModels
{
    public class EmailViewModel
    {
        public Guid EmlKey { get; set; }
        [Required]
        public Guid EmlPerKey { get; set; }
        [Required]
        [DisplayName("Email Address")]
        public string EmlAddress { get; set; }
        [Required]
        [DisplayName("Is Primary Email?")]
        [DefaultValue(false)]
        public bool EmlIsPrimaryEmail { get; set; }
        [Required]
        [DisplayName("Is Work Email?")]
        [DefaultValue(false)]
        public bool EmlIsWorkEmail { get; set; }
        [Required]
        [DisplayName("Is Personal Email?")]
        [DefaultValue(false)]
        public bool EmlIsPersonalEmail { get; set; }
    }
}
