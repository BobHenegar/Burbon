using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public const string StatusStateType =
            "Cu6Key,Cu6Code,Cu6LockedFlag,Cu6MetFlag,Cu6TerminateProcessFlag,Cu6CompletedFlag,Cu6UnderReviewFlag";

        public const string CredentialRequirementTemplate =
            "Ct8Key,Ct8Template,Ct8Description,Ct8Code,Ct8SetDataKeyFlag";

        public const string CredentialRequirementType =
            "Ct3Key," +
            "Ct3Requirement," +
            "Ct3Description," +
            "Ct3Ct8Key," +
            "Ct3StartCt6Key," +
            "Ct3CompleteCt6Key," +
            "Ct3SubmitCt6Key," +
            "Ct3MetCt6Key," +
            "Ct3DeniedCt6Key," +
            "Ct3LongDescription," +
            "Ct3SubmitProcessFlag," +
            "Ct3CalculateDueDateSp," +
            "Ct3CalculateStartDateSp," +
            "Ct3DependentCt6Key," +
            "Ct3DueDateNumber," +
            "Ct3DueDateRounding," +
            "Ct3DueDateUnit," +
            "Ct3HasRequiredParentsFlag," +
            "Ct3StartCr2Ct6Key," +
            "Ct3SyncToProcessDatesFlag," +
            "Ct3MetCr2Ct6Key," +
            "Ct3SubmitProcessFlag," +
            "Ct3CEUnitsRequired," +
            "Ct3CetKey" +
            ",Ct3QryKey," +
            "Ct3ExmKey," +
            "Ct3MinExamScore," +
            "Ct3DetKey," +
            "Ct3DtpKey," +
            "Ct3ExamMaxAttempts," +
            "Ct3PriorExamScoreEligibleFlag," +
            "Ct3PriorExamScoreEligibleDateUnit," +
            "Ct3PriorExamScoreEligibleDateNumber";
    }
}
