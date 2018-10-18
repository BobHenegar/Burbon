using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    class DegreeType
    {
        [NotMapped]
        public const string Prefix = "Det";


        [Key]
        public Guid DetKey { get; set; }

        [Required]
        public string DetCode { get; set; }

        public string DetDescription { get; set; }

        public DateTimeOffset DCetAddDate { get; set; }

        public DateTimeOffset? DetChangeDate { get; set; }

        public string DetAddUser { get; set; }

        public string DetChangeUser { get; set; }
    }
}
