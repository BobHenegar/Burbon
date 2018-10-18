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

        [DisplayName("Person")]
        public Guid PhoPerKey { get; set; }

        [Required]
        [DisplayName("Phone Number")]
        public string PhoNumber { get; set; }

        [DisplayName("Extension")]
        public string PhoExtention { get; set; }

        [DisplayName("Primary")]
        [DefaultValue(false)]
        public bool PhoIsPrimary { get; set; }


        [DisplayName("Home")]
        [DefaultValue(false)]
        public bool PhoIsHome { get; set; }

        [DisplayName("Mobile")]
        [DefaultValue(false)]
        public bool PhoIsMobile { get; set; }

        [DisplayName("Work")]
        [DefaultValue(false)]
        public bool PhoIsWork { get; set; }


        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset PhoAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? PhoChangeDate { get; set; }

        [DisplayName("Added By")]
        public string PhoAddUser { get; set; }


        [DisplayName("Changed By")]
        public string PhoChangeUser { get; set; }

        #region Navigation Properties
        [ForeignKey("PhoPerKey")]
        public virtual Person Person { get; set; }
        #endregion Navigation Properties
    }
}
