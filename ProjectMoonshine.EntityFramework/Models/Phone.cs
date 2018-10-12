using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Phone")]
    public class Phone : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Pho";
        [Key]
        public Guid PhoKey { get; set; }

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

        [DisplayName("Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset PhoAddDate { get; set; }
        public DateTimeOffset? PhoChangeDate { get; set; }
        public string PhoAddUser { get; set; }
        public string PhoChangeUser { get; set; }

        #region Navigation Properties
        [ForeignKey("PhoPerKey")]
        public virtual Person Person { get; set; }
        #endregion Navigation Properties
    }
}
