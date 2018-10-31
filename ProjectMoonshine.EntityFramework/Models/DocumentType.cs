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
    public class DocumentType : EntityBase
    {

        [NotMapped]
        public const string prefix = "Dtp";

        [Key]
        public Guid DtpKey { get; set; }

        [Required]
        [CanBeDescription(true)]
        [DisplayName("Code")]
        public string DtpCode { get; set; }

        [DisplayName("Description")]
        public string DtpDescription { get; set; }

        [DisplayName("Order")]
        public short? DtpOrder { get; set; }


        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset DtpAddDate { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? DtpChangeDate { get; set; }

        [DisplayName("Add User")]
        public string DtpAddUser { get; set; }

        [DisplayName("Change User")]
        public string DtpChangeUser { get; set; }

    }
}
