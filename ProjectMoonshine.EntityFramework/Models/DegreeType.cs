using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using ProjectMoonshine.EntityFramework.Attributes;

namespace ProjectMoonshine.EntityFramework.Models
{

    [CanBeSelectList(true)]
    public class DegreeType : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Det";


        [Key]
        public Guid DetKey { get; set; }

        [Required]
        [CanBeDescription(true)]
        [DisplayName("Degree Code")]
        public string DetCode { get; set; }

        [DisplayName("Description")]
        public string DetDescription { get; set; }

        [DisplayName("Order")]
        public short? DetOrder { get; set; }

        public DateTimeOffset DetAddDate { get; set; }

        public DateTimeOffset? DetChangeDate { get; set; }

        public string DetAddUser { get; set; }

        public string DetChangeUser { get; set; }
    }
}
