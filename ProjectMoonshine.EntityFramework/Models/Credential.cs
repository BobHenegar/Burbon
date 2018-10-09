using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class Credential: EntityBase
    {
        [NotMapped]
        public const string Prefix = "cr1";

        [Key]
        public Guid cr1Key { get; set; }

        [DisplayName("Credential Type")]
        public string cr1Ct1Credential { get; set; }

        public string cr1CredentialNumber { get; set; }

        public Guid cr1CstKey { get; set; }

        public string cr1Status { get; set; }

        public DateTimeOffset cr1StatusChangeDate { get; set; }

        public string cr1StatusChangeUser { get; set; }

        public DateTimeOffset cr1CredentialDate { get; set; }

        public DateTimeOffset cr1EffectiveDate { get; set; }

        public DateTimeOffset cr1ValidThruDate { get; set; }

        public Guid cr1Ct2CurrentProcess { get; set; }

        public Guid cr1CurrentCr2Key { get; set; }

        public Guid cr1NextCr2Key { get; set; }

        public Guid cr1ApplicationCr2Key { get; set; }

        public bool cr1ExtensionUsedFlag { get; set; }

        public string cr1Note { get; set; }

        public string cr1Owner { get; set; }

        public Guid cr1DataKey { get; set; }

        public DateTimeOffset cr1AddDate { get; set; }
        public DateTimeOffset? cr1ChangeDate { get; set; }
        public string cr1AddUser { get; set; }
        public string cr1ChangeUser { get; set; }

        public string cr1LegalFirstName { get; set; }

        public string cr1LegalLastName { get; set; }
    }

}
