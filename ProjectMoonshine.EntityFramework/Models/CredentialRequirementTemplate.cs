using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialRequirementTemplate")]
    public class CredentialRequirementTemplate : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct8";
        [Key]
        public Guid Ct8Key { get; set; }

        [DisplayName("Template")]
        public string Ct8Template { get; set; }

        [DisplayName("Code")]
        public GlobalConstants.RequirementTemplateCode Ct8Code { get; set; }

        [DisplayName("Description")]
        public string Ct8Description { get; set; }

        //public string Ct8CheckProcedure { get; set; }

        //public string Ct8FindProcedure { get; set; }

        [DisplayName("Set Data Key Flag")]
        public bool Ct8SetDataKeyFlag { get; set; }

        //public string Ct8InitMethodBehavior { get; set; }

        //public string Ct8EvalMethodBehavior { get; set; }

        [DisplayName("Add Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset Ct8AddDate { get; set; }

        [DisplayName("Change Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTimeOffset? Ct8ChangeDate { get; set; }

        [DisplayName("Add User")]
        public string Ct8AddUser { get; set; }

        [DisplayName("Change User")]
        public string Ct8ChangeUser { get; set; }

        public List<CredentialRequirementType> CredentialRequirementTypes { get; set; }

        public async Task<bool> EvalMetAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (requirement.Cr3MetDate.IsDateTimeOffsetValid()) return true;
                switch (Ct8Code)
                {
                    case GlobalConstants.RequirementTemplateCode.None:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Application:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Attestment:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Audit:
                        break;
                    case GlobalConstants.RequirementTemplateCode.CE:
                        return await EvalCEMetAsync(context, requirement);
                        break;
                    case GlobalConstants.RequirementTemplateCode.Checklist:
                        
                        break;
                    case GlobalConstants.RequirementTemplateCode.Course:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Custom:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Degree:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Designation:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Disclosure:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Document:
                        break;
                    case GlobalConstants.RequirementTemplateCode.DocumentRequest:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Ethics:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Event:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Exam:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Experience:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Fee:
                        break;
                    case GlobalConstants.RequirementTemplateCode.License:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Membership:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Relationship:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Signature:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Simple:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Sql:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Switch:
                        break;
                    default:
                        return false;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
                return false;
            }
        }

        public async Task<bool> EvaluateCompleteAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (requirement.Cr3MetDate.IsDateTimeOffsetValid()) return true;
                if (requirement.Cr3Ct6Key.IsGuidValid())
                requirement.Status = await
                    CredentialingUtils.GetCredentialStatusTypeAsync(context, (Guid) requirement.Cr3Ct6Key, true);

                if (requirement.Status?.State != null && ( requirement.Status.State.Cu6CompletedFlag 
                                                           || requirement.Status.State.Cu6UnderReviewFlag 
                                                           || requirement.Status.State.Cu6MetFlag)) return true;

                switch (Ct8Code)
                {
                    case GlobalConstants.RequirementTemplateCode.None:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Application:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Attestment:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Audit:
                        break;
                    case GlobalConstants.RequirementTemplateCode.CE:

                        return await EvalCECompleteAsync(context, requirement);
                        break;
                    case GlobalConstants.RequirementTemplateCode.Checklist:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Course:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Custom:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Degree:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Designation:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Disclosure:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Document:
                        break;
                    case GlobalConstants.RequirementTemplateCode.DocumentRequest:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Ethics:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Event:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Exam:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Experience:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Fee:
                        break;
                    case GlobalConstants.RequirementTemplateCode.License:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Membership:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Relationship:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Signature:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Simple:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Sql:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Switch:
                        break;
                    default:
                        return false;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<bool> InitAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                switch (Ct8Code)
                {
                    case GlobalConstants.RequirementTemplateCode.None:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Application:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Attestment:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Audit:
                        break;
                    case GlobalConstants.RequirementTemplateCode.CE:
                        return await InitCEAsync(context, requirement);
                    case GlobalConstants.RequirementTemplateCode.Checklist:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Course:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Custom:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Degree:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Designation:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Disclosure:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Document:
                        break;
                    case GlobalConstants.RequirementTemplateCode.DocumentRequest:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Ethics:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Event:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Exam:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Experience:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Fee:
                        break;
                    case GlobalConstants.RequirementTemplateCode.License:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Membership:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Relationship:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Signature:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Simple:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Sql:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Switch:
                        break;
                    default:
                        return true;
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<bool> InitCEAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                requirement.CredentialRequirementType = 
                    await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);

                if (requirement.CredentialRequirementType == null 
                    || !requirement.CredentialRequirementType.Ct3CetKey.IsGuidValid()) return false;

                requirement.CredentialProcess =
                    await CredentialingUtils.GetCredentialProcessAsync(context, requirement.Cr3Cr2Key);

                if (requirement.CredentialProcess == null) return false;

                requirement.CredentialProcess.Credential =
                    await CredentialingUtils.GetCredentialAsync(context, requirement.CredentialProcess.Cr2Cr1Key);

                if (requirement.CredentialProcess.Credential == null) return false;
                CEPeriod cePeriod = new CEPeriod()
                {
                    Ce2Key = Guid.NewGuid()
                    , Ce2CusKey = requirement.CredentialProcess.Credential.Cr1CusKey
                    , Ce2StartDate = requirement.Cr3StartDate.IsDateTimeOffsetValid() ? (DateTimeOffset) requirement.Cr3StartDate : DateTimeOffset.Now
                    , Ce2EndDate =  requirement.Cr3DueDate.IsDateTimeOffsetValid() ? (DateTimeOffset) requirement.Cr3DueDate : DateTimeOffset.Now
                    , Ce2CetKey =  (Guid) requirement.CredentialRequirementType.Ct3CetKey
                    , Ce2RequiredUnits = requirement.CredentialRequirementType.Ct3CEUnitsRequired ?? 0
                    , Ce2BalanceUnits = requirement.CredentialRequirementType.Ct3CEUnitsRequired ?? 0
                    , Ce2ReportedUnits = 0
                    , Ce2AcceptedUnits = 0
                };

                if (!await cePeriod.AddAsync(context)) return false;

                requirement.Cr3DataKey = cePeriod.Ce2Key;
                context.Update(requirement);
                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<bool> EvalCEMetAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (!requirement.Cr3DataKey.IsGuidValid()) return false;
                CEPeriod cePeriod = await context.CEPeriods
                    .FindAsync(requirement.Cr3DataKey);

                //if (cePeriod.Ce2BalanceUnits <= 0 && cePeriod.Ce2RequiredUnits > 0) return true;

                //if (! await cePeriod.CalculateAsync(context)) return false;

                if (cePeriod == null) return false;

                return cePeriod.Ce2BalanceUnits <= 0 && cePeriod.Ce2RequiredUnits > 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<bool> EvalCECompleteAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (!requirement.Cr3DataKey.IsGuidValid()) return false;
                CEPeriod cePeriod = await context.CEPeriods
                    .FindAsync(requirement.Cr3DataKey);

                //if (cePeriod.Ce2ReportedUnits >= cePeriod.Ce2RequiredUnits && cePeriod.Ce2RequiredUnits > 0) return true;

                //if (!await cePeriod.CalculateAsync(context)) return false;

                if (cePeriod == null) return false;

                return cePeriod.Ce2ReportedUnits >= cePeriod.Ce2RequiredUnits && cePeriod.Ce2RequiredUnits > 0;

               }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }


}
