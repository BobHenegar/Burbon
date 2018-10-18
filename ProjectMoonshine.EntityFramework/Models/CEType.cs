using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class CEType
    {

        [NotMapped]
        public const string Prefix = "Cet";


        [Key]
        public Guid CetKey { get; set; }

        [Required]
        public string CetCode { get; set; }

        public string CetDescription { get; set; }

        public DateTimeOffset CetAddDate { get; set; }

        public DateTimeOffset? CetChangeDate { get; set; }

        public string CetAddUser { get; set; }

        public string CetChangeUser { get; set; }
    }
}
