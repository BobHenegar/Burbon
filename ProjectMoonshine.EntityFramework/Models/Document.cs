using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class Document: EntityBase
    {

        [NotMapped]
        public const string Prefix = "Doc";


        [Key]
        public Guid DocKey { get; set; }

        public Guid DocCusKey { get; set; }
        [DisplayName("Document Type")]
        public Guid DocDtpKey { get; set; }

        [DisplayName("Document Number")]
        public string DocNumber { get; set; }

        [DisplayName("Description")]
        public string DocDescription { get; set; }

        [DisplayName("URL")]
        public string DocUrl { get; set; }

        [DisplayName("File Name")]
        public string DocFileName { get; set; }

        [DisplayName("Add User")]
        public string DocAddUser { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset DocAddDate { get; set; }

        [DisplayName("Change User")]
        public string DocChangeUser { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset DocChangeDate { get; set; }
    }
}
