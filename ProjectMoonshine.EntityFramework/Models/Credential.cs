using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class Credential
    {
        [NotMapped]
        public const string Prefix = "cr1";

        [Key]
        public Guid Cr1Key { get; set; }

        [DisplayName("Credential Type")]
        public string Cr1Ct1Credential { get; set; }

        public string Cr1CredentialNumber { get; set; }

        public Guid Cr1CstKey { get; set; }

        public string Cr1Status { get; set; }

        public DateTimeOffset Cr1StatusChangeDate { get; set; }

        public string Cr1StatusChangeUser { get; set; }

        public DateTimeOffset cr1CredentialDate { get; set; }

        public DateTimeOffset cr1EffectiveDate { get; set; }

        public DateTimeOffset cr1ValidThruDate { get; set; }

        public Guid cr1Ct2CurrentProcess { get; set; }

        public Guid cr1CurrentCr2Key { get; set; }

        public Guid cr1NextCr2Key { get; set; }

        public Guid cr1ApplicationCr2Key { get; set; }

        public bool Cr1ExtensionUsedFlag { get; set; }

        public string Cr1Note { get; set; }

        public string Cr1Owner { get; set; }

        public Guid cr1DataKey { get; set; }

        public DateTimeOffset Cr1AddDate { get; set; }
        public DateTimeOffset? Cr1ChangeDate { get; set; }
        public string Cr1AddUser { get; set; }
        public string Cr1ChangeUser { get; set; }

        public string Cr1LegalFirstName { get; set; }

        public string Cr1LegalLastName { get; set; }
    }



