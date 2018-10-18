using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class License
    {
        [NotMapped]
        public const string Prefix = "Ixl";

        [Key]
        public Guid IxlKey { get; set; }

        public Guid IxlIndCusKey { get; set; }

        [DisplayName("State")]
        public string IxlState { get; set; }

        [DisplayName("License No")]
        public string IxlLicenseNo { get; set; }

        [DisplayName("License Name")]
        public string IxlLicenseName { get; set; }

        [DisplayName("Issue Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset IxlIssueDate { get; set; }

        [DisplayName("Expire Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset IxlEpireDate { get; set; }

        [DisplayName("Add User")]
        public string IxlAddUser { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset IxlAddDate { get; set; }

        [DisplayName("Change User")]
        public string IxlChangeUser { get; set; }

        [DisplayName("Change Date")]

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset IxlChangeDate { get; set; }
    }
}
