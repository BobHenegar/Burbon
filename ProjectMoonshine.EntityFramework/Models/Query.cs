using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class Query : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Qry";

        [Key]
        [DisplayName("Query Key")]
        public Guid QryKey { get; set; }

        [Required]
        public string QryJsonBody { get; set; }

        [DisplayName("Query Name")]
        [Required]
        public string QryName { get; set; }

        [DisplayName("For Table")]
        [Required]
        public string QryTable { get; set; }

        [DisplayName("Date Added")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset QryAddDate { get; set; }

        [DisplayName("Date Changed")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTimeOffset? QryChangeDate { get; set; }

        [DisplayName("Added By")]
        public string QryAddUser { get; set; }


        [DisplayName("Changed By")]
        public string QryChangeUser { get; set; }
    }
}
