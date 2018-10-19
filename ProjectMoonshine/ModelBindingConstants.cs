using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine
{
    public class ModelBindingConstants
    {
        public const string CredentialProcessAdd = "Cr2Key,Cr2Ct2Key,Cr2Cr1Key,Cr2Ct6Key,Cr2TerminationDate,Cr2CompletionDate,Cr2DueDate,Cr2StartDate";
        public const string CredentialProcessEdit = "Cr2Key,Cr2Ct6Key,Cr2TerminationDate,Cr2CompletionDate,Cr2DueDate,Cr2StartDate,";

        public const string CredentialProcessType =
            "Ct2Key,Ct2Process,Ct2Description,Ct2StartCt6Key,Ct2CompletedCt6Key,Ct2SubmittedCt6Key,Ct2ProcessType,Ct2NextCt2Key,Ct2CalculateDueDateSp,Ct2CalculateStartDateSp,Ct2DueDateNumber,Ct2DueDateRounding,Ct2DueDateUnit,Ct2ProcessType,Ct2CompletedCr1Ct6Key";

        public const string CredentialTypeXCredentialProcessType = "Ct4Key,Ct4Key,Ct4Ct1Key,Ct4Ct2Key,Ct4Order,Ct4EndDate,Ct4StartDate";

        public const string CredentialAdd =
            "Cr1Key,Cr1Ct1Key,Cr1CusKey,Cr1Ct6Key,Cr1Ct2Key,Cr1StartDate,Cr1CredentialDate,Cr1CredentialNumber,Cr1EffectiveDate,Cr1ValidThruDate";

        public const string CredentialEdit =
            "Cr1Key,Cr1Ct6Key,Cr1StartDate,Cr1CredentialDate,Cr1CredentialNumber,Cr1EffectiveDate,Cr1ValidThruDate";

        public const string CredentialType = "Ct1Key,Ct1Credential,Ct1Description,Ct1StartDate,Ct1EndDate,Ct1InitCt2Key,Ct1InitCt6Key,Ct1RenewCt2Key,Ct1RenewCt6Key,Ct1Order,Ct1SyncFlag";
    }
}
