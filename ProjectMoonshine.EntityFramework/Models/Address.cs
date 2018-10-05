using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("Address")]
    public class Address : EntityBase
    {
        public Address()
        {
            Prefix = "Adr";
        }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("AdrKey")]
        public Guid AddressKey { get; set; }
        public Guid AdrPerKey { get; set; }
        public string AdrCity { get; set; }
        public string AdrState { get; set; }
        public string AdrZip { get; set; }
        public string AdrAddress1 { get; set; }
        public string AdrAddress2 { get; set; }
        public string AdrAddress3 { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset AdrAddDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTimeOffset? AdrModifiedDate { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AdrAddUser { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string AdrModifiedUser { get; set; }

        public virtual AddressType AdtAddressType { get; set; }


        [ForeignKey("AdrPerKey")]
        public virtual Person AdrPerson {get;set;}
    }
}
