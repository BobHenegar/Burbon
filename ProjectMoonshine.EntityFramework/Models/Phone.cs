using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Phone")]
    public class Phone : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Pho";
        [Key]
        public Guid PhoKey { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PhoPerKey { get; set; }
        public string PhoNumber { get; set; }
        public string PhoExtention { get; set; }
        [DefaultValue(false)]
        public bool PhoIsPrimary { get; set; }
        [DefaultValue(false)]
        public bool PhoIsHome { get; set; }
        [DefaultValue(false)]
        public bool PhoIsMobile { get; set; }
        [DefaultValue(false)]
        public bool PhoIsWork { get; set; }

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
