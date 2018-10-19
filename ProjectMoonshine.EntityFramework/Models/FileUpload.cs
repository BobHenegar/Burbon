using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class FileUpload : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Fup";

        [Key]
        public Guid FupKey { get; set; }

        [DisplayName("Person")]
        public Guid FupPerKey { get; set; }

        [DisplayName("File")]
        public byte[] FupFileData { get; set; }

        [DisplayName("File Type")]
        public string FupFileType { get; set; }

        [DisplayName("Added By")]
        public string FupAddUser { get; set; }

        [DisplayName("Changed By")]
        public string FupChangeUser { get; set; }
        [DisplayName("Add Date")]
        public DateTimeOffset FupAddDate { get; set; }

        [DisplayName("Change Date")]
        public DateTimeOffset FupChangeDate { get; set; }


        [ForeignKey("FupPerKey")]
        public virtual Person Person { get; set; }
    }
}
