using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ProjectMoonshine.EntityFramework.Models
{
    public class Credential
    {
        [Key]
        public Guid Cr1Key { get; set; }

        [DisplayName("Credential Type")]
        public string Cr1Ct1Credential { get; set; }

        public String Cr1CredentialNumber { get; set; }

        public Guid Cr1PerKey { get; set; }

        public String Cr1Status { get; set; }

        public DateTimeOffset Cr1StatusChangeDate { get; set; }

        public String Cr1StatusChangeUser { get; set; }

        public DateTimeOffset Cr1CredentialDate { get; set; }

        public DateTimeOffset Cr1EffectiveDate { get; set; }

        public DateTimeOffset Cr1ValidThruDate { get; set; }

        public Guid Cr1Ct2CurrentProcess { get; set; }

        public Guid Cr1CurrentCr2Key { get; set; }

        public Guid Cr1NextCr2Key { get; set; }

        public Guid Cr1ApplicationCr2Key { get; set; }

        public Boolean Cr1ExtensionUsedFlag { get; set; }

        public String Cr1Note { get; set; }

        public String Cr1Owner { get; set; }

        public Guid Cr1DataKey { get; set; }

        public String Cr1AddUser { get; set; }

        public DateTimeOffset Cr1AddDate { get; set; }

        public String Cr1ChangeUser { get; set; }

        public DateTimeOffset Cr1ChangeDate { get; set; }

        public String Cr1LegalFirstName { get; set; }

        public String Cr1LegalLastName { get; set; }


        public virtual List<CredentialProcess> CredentialProcesses { get; set; }
        public virtual Person Person { get; set; }
        }
    }



