using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Classes.QueryClasses;
using ProjectMoonshine.EntityFramework.Helpers;
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
                        return await EvalApplicationMetAsync(context, requirement);
                    case GlobalConstants.RequirementTemplateCode.Attestment:
                        return EvalAttestmentMet(context, requirement);
                    case GlobalConstants.RequirementTemplateCode.Audit:
                        return await EvalAuditMetAsync(context, requirement);
                    case GlobalConstants.RequirementTemplateCode.CE:
                        return await EvalCEMetAsync(context, requirement);
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
                        return await EvalExamMetAsync(context, requirement);
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
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private async Task<bool> EvalExamMetAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (requirement.Cr3ExsKey.IsGuidValid()) return true;

                ExamScore examScore = await FindRequirementDataAsync(FindExamScoreAsync, context, requirement);
                if (examScore == null)
                    return await UpdateExamRequirementStatusIfMaxAttemptsExceeded(context, requirement);

                requirement.Cr3ExsKey = examScore.ExsKey;
                context.Update(requirement);
                await context.SaveChangesAsync();
                return true;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private static async Task<bool> UpdateExamRequirementStatusIfMaxAttemptsExceeded(ApplicationDbContext context, CredentialRequirement requirement)
        {
            if (requirement.CredentialRequirementType == null) return false;
            if (!requirement.CredentialRequirementType.Ct3DeniedCt6Key.IsGuidValid()) return false;
            {
                if (!CommonUtils.IsIntValidAndNotZero(requirement.CredentialRequirementType.Ct3ExamMaxAttempts))
                    return false;

                Guid customerKey = await CredentialingUtils.GetCustomerKeyFromProcessKeyAsync(context, requirement.Cr3Cr2Key);
                if (!CommonUtils.IsGuidValid(customerKey)) return false;

                DateTimeOffset? minDate = GetPriorExamScoreEligibilityMinDate(requirement.CredentialRequirementType);

                if (!(await context.ExamScores
                          .AsNoTracking()
                          .Where(e => e.ExsCusKey == customerKey
                                      && e.ExsExmKey == requirement.CredentialRequirementType.Ct3ExmKey
                                      && (!minDate.IsDateTimeOffsetValid() || minDate < e.ExsDate))
                          .CountAsync() >=
                      requirement.CredentialRequirementType.Ct3ExamMaxAttempts)) return false;

                requirement.Cr3Ct6Key = requirement.CredentialRequirementType.Ct3DeniedCt6Key;
                context.Update(requirement);
                await context.SaveChangesAsync();
                return false;
            }
        }

        private static DateTimeOffset? GetPriorExamScoreEligibilityMinDate(CredentialRequirementType requirementType)
        {
            DateTimeOffset? minDate = null;
            if (!requirementType.Ct3PriorExamScoreEligibleFlag ||
                requirementType.Ct3PriorExamScoreEligibleDateNumber == null) return null;
            switch (requirementType.Ct3PriorExamScoreEligibleDateUnit)
            {
                case GlobalConstants.DueDateUnit.None:
                    break;
                case GlobalConstants.DueDateUnit.Day:
                    minDate = DateTimeOffset.Now.AddDays((short)-requirementType.Ct3PriorExamScoreEligibleDateNumber);
                    break;
                case GlobalConstants.DueDateUnit.Month:
                    minDate = DateTimeOffset.Now.AddMonths((short)-requirementType.Ct3PriorExamScoreEligibleDateNumber);
                    break;
                case GlobalConstants.DueDateUnit.Year:
                    minDate = DateTimeOffset.Now.AddYears((short)-requirementType.Ct3PriorExamScoreEligibleDateNumber);
                    break;
                default:
                    break;
            }
            return minDate;
        }

        private async Task<ExamScore> FindExamScoreAsync(ApplicationDbContext context, Guid customerKey, CredentialRequirementType requirementType)
        {
            Expression<Func<ExamScore, bool>> rule = await GetRuleExpression<ExamScore>(context, requirementType);

            Expression<Func<ExamScore, bool>> defaultRule = e => e.ExsExmKey == requirementType.Ct3ExmKey 
                                              && (e.ExsSuccessFlag 
                                                  || (CommonUtils.IsIntValidAndNotZero(requirementType.Ct3MinExamScore)
                                                      && e.ExsTotalReceived >= requirementType.Ct3MinExamScore)); 

            if (rule == null) rule = defaultRule;
            DateTimeOffset? minDate = GetPriorExamScoreEligibilityMinDate(requirementType);
            if (requirementType.Ct3PriorExamScoreEligibleFlag && requirementType.Ct3PriorExamScoreEligibleDateNumber != null)
            {

                switch (requirementType.Ct3PriorExamScoreEligibleDateUnit)
                {
                    case GlobalConstants.DueDateUnit.None:
                        break;
                    case GlobalConstants.DueDateUnit.Day:
                        minDate = DateTimeOffset.Now.AddDays((short)-requirementType.Ct3PriorExamScoreEligibleDateNumber);
                        break;
                    case GlobalConstants.DueDateUnit.Month:
                        minDate = DateTimeOffset.Now.AddMonths((short)-requirementType.Ct3PriorExamScoreEligibleDateNumber);
                        break;
                    case GlobalConstants.DueDateUnit.Year:
                        minDate = DateTimeOffset.Now.AddYears((short)-requirementType.Ct3PriorExamScoreEligibleDateNumber);
                        break;
                    default:
                        break;
                }
            };

            return await context.ExamScores
                .AsNoTracking()
                .Where(rule)
                .Where(e => e.ExsCusKey == customerKey 
                            && (!minDate.IsDateTimeOffsetValid() || minDate <= e.ExsDate))
                .OrderByDescending(l => !CommonUtils.IsDateTimeOffsetValid(l.ExsDate) ? DateTimeOffset.MinValue : l.ExsDate)
                .ThenByDescending(l => l.ExsAddDate)
                .FirstOrDefaultAsync();
        }

        private async Task<bool> EvalAuditMetAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (requirement.Cr3MetDate.IsDateTimeOffsetValid()) return true;

                requirement.CredentialProcess =
                    await CredentialingUtils.GetCredentialProcessAsync(context, requirement.Cr3Cr2Key);

                if (requirement.CredentialProcess == null || requirement.CredentialProcess.Cr2AuditSelectDate.IsDateTimeOffsetValid()) return false;

                if (requirement.CredentialRequirementType == null)
                requirement.CredentialRequirementType =
                    await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);

                if (requirement.CredentialRequirementType == null) return false;

                if (string.IsNullOrWhiteSpace(requirement.CredentialRequirementType.Ct3EvalStoredProcedure))
                    return false;

                if (await CredentialingUtils.GetStringFromSp(context,
                        requirement.CredentialRequirementType.Ct3EvalStoredProcedure, requirement.Cr3Key.ToString()) !=
                    "true") return true;

                requirement.CredentialProcess.Cr2AuditSelectDate = DateTimeOffset.Now;

                context.Update(requirement.CredentialProcess);

                if (!requirement.CredentialRequirementType.Ct3StartCt6Key.IsGuidValid()) return false;

                requirement.Cr3Ct6Key = requirement.CredentialRequirementType.Ct3StartCt6Key;
                context.Update(requirement);
                return false;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private bool EvalAttestmentMet(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                return requirement.Cr3AttestmentFlag;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
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
                        return await EvalApplicationMetAsync(context, requirement);
                    case GlobalConstants.RequirementTemplateCode.Attestment:
                        return EvalAttestmentMet(context, requirement);
                    case GlobalConstants.RequirementTemplateCode.Audit:
                        return await EvalAuditMetAsync(context, requirement);
                    case GlobalConstants.RequirementTemplateCode.CE:

                        return await EvalCECompleteAsync(context, requirement);
                    case GlobalConstants.RequirementTemplateCode.Checklist:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Course:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Custom:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Degree:
                        return await EvalDegreeComplete(context, requirement);
                    case GlobalConstants.RequirementTemplateCode.Designation:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Disclosure:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Document:
                        return await EvalDocumentComplete(context, requirement);
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
                        return await EvalLicenseCompleteAsync(context, requirement);
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
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private async Task<bool> EvalLicenseCompleteAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (requirement.Cr3IxlKey.IsGuidValid()) return true;
                
                License license = await FindRequirementDataAsync(FindLicenseAsync, context, requirement);
                    
                if (license == null) return false;

                requirement.Cr3IxlKey = license.IxlKey;
                context.Update(requirement);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        public async Task<T> FindRequirementDataAsync<T>(Func<ApplicationDbContext, Guid, CredentialRequirementType, Task<T>> findMethod, ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                Guid customerKey =
                    await CredentialingUtils.GetCustomerKeyFromProcessKeyAsync(context, requirement.Cr3Cr2Key);
                if (!CommonUtils.IsGuidValid(customerKey)) return default(T);

                if (requirement.CredentialRequirementType == null)
                    requirement.CredentialRequirementType = await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);

                if (requirement.CredentialRequirementType == null) return default(T);

                return await findMethod(context, customerKey, requirement.CredentialRequirementType);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }

                return default(T);
            }
        }


        private static async Task<License> FindLicenseAsync(ApplicationDbContext context, Guid customerKey, CredentialRequirementType requirementType)
        {
            Expression<Func<License, bool>> rule = await GetRuleExpression<License>(context, requirementType);

            Expression<Func<License, bool>> defaultRule = l => (!CommonUtils.IsDateTimeOffsetValid(l.IxlIssueDate) ||
                                             l.IxlIssueDate <= DateTimeOffset.Now)
                                         && (!CommonUtils.IsDateTimeOffsetValid(l.IxlExpireDate) ||
                                             l.IxlExpireDate > DateTimeOffset.Now);

            if (rule == null) rule = defaultRule;
            return await context.Licenses
                .AsNoTracking()
                .Where(rule)
                .Where(l => l.IxlCusKey == customerKey)
                .OrderByDescending(l => !CommonUtils.IsDateTimeOffsetValid(l.IxlExpireDate) ? DateTimeOffset.MinValue : l.IxlExpireDate)
                    .ThenByDescending(l => l.IxlAddDate)
                .FirstOrDefaultAsync();
        }

        private static async Task<Expression<Func<T, bool>>> GetRuleExpression<T>(ApplicationDbContext context, CredentialRequirementType requirementType) where T : EntityBase
        {
            if (!requirementType.Ct3QryKey.IsGuidValid()) return null;
            try
            {
                QueryManager qm = new QueryManager(context);
                return await qm.GetExpressionByKey<T>((Guid)requirementType.Ct3QryKey);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }

                return null;
            }
        }

        private static async Task<Degree> FindDegreeAsync(ApplicationDbContext context, Guid customerKey, CredentialRequirementType requirementType )
        {
            Expression<Func<Degree, bool>> rule = await GetRuleExpression<Degree>(context, requirementType);
            Expression<Func<Degree, bool>> defaultRule = d =>
                        d.DegDetKey == requirementType.Ct3DetKey
                        && CommonUtils.IsDateTimeOffsetValid(d.DegDate)
                        && d.DegDate <= DateTimeOffset.Now;

            if (rule == null) rule = defaultRule;

            return await context.Degrees
                .AsNoTracking()
                .Where(rule)
                .Where(d => customerKey == d.DegCusKey)
                .OrderByDescending(d => CommonUtils.IsDateTimeOffsetValid(d.DegDate) ? DateTimeOffset.MinValue : d.DegDate )
                .ThenByDescending(d => d.DegAddDate)
                .FirstOrDefaultAsync();
        }

        private static async Task<FileUpload> FindDocumentAsync(ApplicationDbContext context, Guid customerKey, CredentialRequirementType requirementType)
        {

            Expression<Func<FileUpload, bool>> rule = await GetRuleExpression<FileUpload>(context, requirementType);
            Expression<Func<FileUpload, bool>> defaultRule = f => f.FupDtpKey == requirementType.Ct3DtpKey;

            if (rule == null) rule = defaultRule;
            return await context.FileUploads
                .Include(f => f.Person)
                .AsNoTracking()
                .Where(rule)
                .Where(f => f.Person.PerCusKey == customerKey)
                .OrderByDescending(f => f.FupAddDate)
                .FirstOrDefaultAsync();
        }

        private async Task<bool> EvalDocumentComplete(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (requirement.Cr3FupKey.IsGuidValid()) return true;

                FileUpload fileUpload = await FindRequirementDataAsync(FindDocumentAsync, context, requirement);
                
                if (fileUpload == null) return false;

                requirement.Cr3FupKey = fileUpload.FupKey;
                context.Update(requirement);
                await context.SaveChangesAsync();

                return true;

            }
            catch (Exception e)
            {

                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private async Task<bool> EvalDegreeComplete(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {

                if (requirement.Cr3DegKey.IsGuidValid()) return true;

                Degree degree = await FindRequirementDataAsync(FindDegreeAsync, context, requirement);
                
                if (degree == null) return false;

                requirement.Cr3DegKey = degree.DegKey;
                context.Update(requirement);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private async Task<bool> EvalApplicationMetAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                DateTimeOffset? submitDate = await context.CredentialProcesses
                    .AsNoTracking()
                    .Where(p => p.Cr2Key == requirement.Cr3Cr2Key)
                    .Select(p => p.Cr2SubmitDate)
                    .FirstOrDefaultAsync();

                return (submitDate.IsDateTimeOffsetValid());
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
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
                        return await InitAuditAsync(context, requirement);
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
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        

        private async Task<bool> InitAuditAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (requirement.CredentialRequirementType == null)
                requirement.CredentialRequirementType =
                    await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);

                if (requirement.CredentialRequirementType == null) return false;

                if (!requirement.CredentialRequirementType.Ct3DependentCt6Key.IsGuidValid()) return true;
                requirement.Cr3Ct6Key = requirement.CredentialRequirementType.Ct3DependentCt6Key;
                context.Update(requirement);

                return true;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private async Task<bool> InitCEAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (requirement.CredentialRequirementType == null)
                requirement.CredentialRequirementType = 
                    await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);

                //if (requirement.CredentialRequirementType == null 
                //    || !requirement.CredentialRequirementType.Ct3CetKey.IsGuidValid()) return false;

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
                    , Ce2CetKey =   requirement.CredentialRequirementType.Ct3CetKey.IsGuidValid() ? requirement.CredentialRequirementType.Ct3CetKey : null
                    , Ce2RequiredUnits = requirement.CredentialRequirementType.Ct3CEUnitsRequired ?? 0
                    , Ce2BalanceUnits = requirement.CredentialRequirementType.Ct3CEUnitsRequired ?? 0
                    , Ce2ReportedUnits = 0
                    , Ce2AcceptedUnits = 0
                };

                if (!await cePeriod.AddAsync(context)) return false;

                requirement.Cr3Ce2Key = cePeriod.Ce2Key;
                context.Update(requirement);
                return true;

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private async Task<bool> EvalCEMetAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (!requirement.Cr3Ce2Key.IsGuidValid()) return false;
                CEPeriod cePeriod = await context.CEPeriods
                    .FindAsync(requirement.Cr3Ce2Key);
                
                if (cePeriod == null) return false;

                return cePeriod.Ce2BalanceUnits <= 0 && cePeriod.Ce2RequiredUnits > 0;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }

        private async Task<bool> EvalCECompleteAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (!requirement.Cr3Ce2Key.IsGuidValid()) return false;
                CEPeriod cePeriod = await context.CEPeriods
                    .FindAsync(requirement.Cr3Ce2Key);

                if (cePeriod == null) return false;

                return cePeriod.Ce2ReportedUnits >= cePeriod.Ce2RequiredUnits && cePeriod.Ce2RequiredUnits > 0;

               }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                return false;
            }
        }
    }


}
