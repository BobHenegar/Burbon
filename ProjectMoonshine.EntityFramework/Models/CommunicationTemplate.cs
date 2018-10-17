using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CommunicationTemplate")]
    public class CommunicationTemplate: EntityBase
    {
        [NotMapped]
        public const string Prefix = "Tem";

        [Key]
        public Guid TemKey { get; set; }

        [DisplayName("Template Code")]
        public string TemCode { get; set; }

        [DisplayName("Template Description")]
        public string TemDescription { get; set; }

        [DisplayName("Subject Line")]
        public string TemSubject { get; set; }


        [DisplayName("Template HTML")]
        public string TemHtml { get; set; }

        [DisplayName("Template Text")]
        public string TemText { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset TemAddDate { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? TemChangeDate { get; set; }

        [DisplayName("Add User")]
        public string TemAddUser { get; set; }

        [DisplayName("Change User")]
        public string TemChangeUser { get; set; }
    }
}
