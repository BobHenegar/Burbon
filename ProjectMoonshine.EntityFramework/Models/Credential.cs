using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    class Credential
    {
        [Key]
        public Guid cr1Key { get; set; }

        public String cr1Ct1Credential { get; set; }

        public String cr1CredentialNumber { get; set; }

        public Guid cr1CstKey { get; set; }

        public String cr1Status { get; set; }

        public DateTimeOffset cr1StatusChangeDate { get; set; }

        public String cr1StatusChangeUser { get; set; }

        public DateTimeOffset cr1CredentialDate { get; set; }

        public DateTimeOffset cr1EffectiveDate { get; set; }

        public DateTimeOffset cr1ValidThruDate { get; set; }

        public Guid cr1Ct2CurrentProcess { get; set; }

        public Guid cr1CurrentCr2Key { get; set; }

        public Guid cr1NextCr2Key { get; set; }

        public Guid cr1ApplicationCr2Key { get; set; }

        public Boolean cr1ExtensionUsedFlag { get; set; }

        public String cr1Note { get; set; }

        public String cr1Owner { get; set; }

        public Guid cr1DataKey { get; set; }

        public String cr1AddUser { get; set; }

        public DateTimeOffset cr1AddDate { get; set; }

        public String cr1ChangeUser { get; set; }

        public DateTimeOffset cr1ChangeDate { get; set; }

        public String cr1LegalFirstName { get; set; }

        public String cr1LegalLastName { get; set; }
    }

}
