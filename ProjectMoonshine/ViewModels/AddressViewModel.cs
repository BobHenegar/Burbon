using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectMoonshine.ViewModels
{
    public class AddressViewModel
    {
        public Guid AdrKey { get; set; }

        [Required]
        public Guid AdrPerKey { get; set; }

        [Required]
        [StringLength(maximumLength: 50)]
        public string AdrCity { get; set; }

        [Required]
        [StringLength(75, MinimumLength = 0)]
        public string AdrState { get; set; }

        [Required]
        [StringLength(10)]
        public string AdrZip { get; set; }

        [Required]
        [StringLength(maximumLength: 500)]
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
    }
}
