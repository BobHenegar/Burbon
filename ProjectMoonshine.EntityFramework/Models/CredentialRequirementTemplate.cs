using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
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
                if (!requirement.Cr3ExsKey.IsGuidValid()) return false;
                if (requirement.CredentialRequirementType == null)
                    requirement.CredentialRequirementType = await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);

                if (requirement.CredentialRequirementType == null) return false;
                requirement.ExamScore = await context.ExamScores.FindAsync(requirement.Cr3ExsKey);
                if (requirement.ExamScore == null) return false;
                if (requirement.ExamScore.ExsSuccessFlag) return true;
                if (CommonUtils.IsIntValidAndNotZero(requirement.CredentialRequirementType.Ct3MinExamScore) 
                    && requirement.ExamScore.ExsTotalReceived >= requirement.CredentialRequirementType.Ct3MinExamScore)
                    return true;

                if (!CommonUtils.IsIntValidAndNotZero(requirement.CredentialRequirementType.Ct3MinExamScore) 
                    || !(requirement.ExamScore.ExsTotalReceived < requirement.CredentialRequirementType.Ct3MinExamScore) 
                    || !requirement.CredentialRequirementType.Ct3DeniedCt6Key.IsGuidValid())
                    return false;

                if (!CommonUtils.IsIntValidAndNotZero(requirement.CredentialRequirementType.Ct3ExamMaxAttempts))
                    return false;

                if (!(await context.ExamScores
                          .AsNoTracking()
                          .Where(e => e.ExsCusKey == requirement.ExamScore.ExsCusKey
                                      && e.ExsExmKey == requirement.ExamScore.ExsExmKey)
                          .CountAsync() >=
                      requirement.CredentialRequirementType.Ct3ExamMaxAttempts)) return false;

                requirement.Cr3Ct6Key = requirement.CredentialRequirementType.Ct3DeniedCt6Key;
                context.Update(requirement);
                await context.SaveChangesAsync();
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
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
                Guid customerKey =
                    await CredentialingUtils.GetCustomerKeyFromProcessKeyAsync(context, requirement.Cr3Cr2Key);
                if (!CommonUtils.IsGuidValid(customerKey)) return false;

                License license = await FindLicenseDataAsync(context, customerKey);

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

        private static async Task<License> FindLicenseDataAsync(ApplicationDbContext context, Guid customerKey)
        {
            //QueryManager qm = new QueryManager(context);
            //var expression = await qm.GetExpressionByKey<License>(Guid.Parse("0ECB62E9-89DA-4806-AFF8-0431F2C7BA48"));
            Expression<Func<License, bool>> isLicenseValid = l => l.IxlCusKey == customerKey
                                         && (!CommonUtils.IsDateTimeOffsetValid(l.IxlIssueDate) ||
                                             l.IxlIssueDate <= DateTimeOffset.Now)
                                         && (!CommonUtils.IsDateTimeOffsetValid(l.IxlExpireDate) ||
                                             l.IxlExpireDate > DateTimeOffset.Now);
            return await context.Licenses
                .AsNoTracking()
                .Where(isLicenseValid )
                .OrderByDescending(l => !CommonUtils.IsDateTimeOffsetValid(l.IxlExpireDate) ? DateTimeOffset.MinValue : l.IxlExpireDate)
                    .ThenByDescending(l => l.IxlAddDate)
                .FirstOrDefaultAsync();
        }

        private static async Task<Degree> FindDegreeAsync(ApplicationDbContext context, CredentialRequirement requirement, Guid customerKey)
        {
            if (requirement.CredentialRequirementType == null) 
                requirement.CredentialRequirementType = await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);
            if (requirement.CredentialRequirementType == null) return null;
            Expression<Func<Degree, bool>> isDegreeValid = d =>
                        d.DegDetKey == requirement.CredentialRequirementType.Ct3DetKey
                        && customerKey == d.DegCusKey
                        && CommonUtils.IsDateTimeOffsetValid(d.DegDate)
                        && d.DegDate <= DateTimeOffset.Now;
            return await context.Degrees
                .AsNoTracking()
                .Where(isDegreeValid)
                .OrderByDescending(d => CommonUtils.IsDateTimeOffsetValid(d.DegDate) ? DateTimeOffset.MinValue : d.DegDate )
                .ThenByDescending(d => d.DegAddDate)
                .FirstOrDefaultAsync();
        }

        private static async Task<FileUpload> FindDocumentAsync(ApplicationDbContext context, CredentialRequirement requirement, Guid customerKey)
        {
            if (requirement.CredentialRequirementType == null)
                requirement.CredentialRequirementType = await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);
            if (requirement.CredentialRequirementType == null) return null;

            Expression<Func<FileUpload, bool>> isFileUploadValid = f =>
                f.Person.PerCusKey == customerKey
                && f.FupDtpKey == requirement.CredentialRequirementType.Ct3DtpKey;


            return await context.FileUploads
                .Include(f => f.Person)
                .AsNoTracking()
                .Where(isFileUploadValid)
                .OrderByDescending(f => f.FupAddDate)
                .FirstOrDefaultAsync();
        }

        //private async Task<bool> FindLicenseDataAsync(Context)

        private async Task<bool> EvalDocumentComplete(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                if (requirement.Cr3FupKey.IsGuidValid()) return true;
                Guid customerKey =
                    await CredentialingUtils.GetCustomerKeyFromProcessKeyAsync(context, requirement.Cr3Cr2Key);
                if (!CommonUtils.IsGuidValid(customerKey)) return false;

                FileUpload fileUpload = await FindDocumentAsync(context, requirement, customerKey);
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
                Guid customerKey =
                    await CredentialingUtils.GetCustomerKeyFromProcessKeyAsync(context, requirement.Cr3Cr2Key);
                if (!CommonUtils.IsGuidValid(customerKey)) return false;

                Degree degree = await FindDegreeAsync(context, requirement, customerKey);

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
                        //return await InitDegreeAsync(context, requirement);
                        break;
                        
                    case GlobalConstants.RequirementTemplateCode.Designation:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Disclosure:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Document:
                        //return await InitDocumentAsync(context, requirement);
                        break;
                    case GlobalConstants.RequirementTemplateCode.DocumentRequest:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Ethics:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Event:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Exam:
                        return await InitExamAsync(context, requirement);
                        
                    case GlobalConstants.RequirementTemplateCode.Experience:
                        break;
                    case GlobalConstants.RequirementTemplateCode.Fee:
                        break;
                    case GlobalConstants.RequirementTemplateCode.License:
                        //return await InitLicenseAsync(context, requirement);
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

        private async Task<bool> InitLicenseAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                Customer customer = await CredentialingUtils.GetCustomerFromProcessKey(context, requirement.Cr3Cr2Key);
                if (customer == null) return false;
                List<License> licenses = await context.Licenses
                    .AsNoTracking()
                    .Where(l => l.IxlCusKey == customer.CusKey)
                    .OrderByDescending(l => l.IxlIssueDate)
                    .ToListAsync();

                if (licenses == null || licenses.Count <= 0) return true;
                foreach (License license in licenses)
                {
                    if (license.IxlExpireDate <= DateTimeOffset.Now) return true;
                    if (!CommonUtils.IsDateTimeOffsetValid(license.IxlIssueDate)) continue;

                    requirement.Cr3IxlKey = license.IxlKey;
                    context.Update(requirement);
                    await context.SaveChangesAsync();
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


        private async Task<bool> InitDocumentAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                Customer customer = await CredentialingUtils.GetCustomerFromProcessKey(context, requirement.Cr3Cr2Key);

                if (customer == null) return false;

                if (requirement.CredentialRequirementType == null)
                    requirement.CredentialRequirementType =
                        await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);

                if (requirement.CredentialRequirementType == null) return false;

                if (!requirement.CredentialRequirementType.Ct3DtpKey.IsGuidValid()) return true;

                FileUpload fileUpload = await context.FileUploads
                    .Include(f => f.Person)
                    .Where(f => f.Person.PerCusKey == customer.CusKey 
                                && f.FupDtpKey == requirement.CredentialRequirementType.Ct3DtpKey)
                    .OrderByDescending(f => f.FupAddDate)
                    .FirstOrDefaultAsync();

                if (fileUpload == null) return true;

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

        private async Task<bool> InitExamAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                Customer customer = await CredentialingUtils.GetCustomerFromProcessKey(context, requirement.Cr3Cr2Key);

                if (customer == null) return false;

                if (requirement.CredentialRequirementType == null)
                    requirement.CredentialRequirementType =
                        await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);

                if (requirement.CredentialRequirementType == null) return false;

                if (!requirement.CredentialRequirementType.Ct3PriorExamScoreEligibleFlag) return true;

                DateTimeOffset? minDate = null;
                if (requirement.CredentialRequirementType.Ct3PriorExamScoreEligibleDateNumber != null)
                {
                    switch (requirement.CredentialRequirementType.Ct3PriorExamScoreEligibleDateUnit)
                    {
                        case GlobalConstants.DueDateUnit.None:
                            break;
                        case GlobalConstants.DueDateUnit.Day:
                            minDate = DateTimeOffset.Now.AddDays((short) -requirement.CredentialRequirementType.Ct3PriorExamScoreEligibleDateNumber);
                            break;
                        case GlobalConstants.DueDateUnit.Month:
                            minDate = DateTimeOffset.Now.AddMonths((short)-requirement.CredentialRequirementType.Ct3PriorExamScoreEligibleDateNumber);
                            break;
                        case GlobalConstants.DueDateUnit.Year:
                            minDate = DateTimeOffset.Now.AddYears((short)-requirement.CredentialRequirementType.Ct3PriorExamScoreEligibleDateNumber);
                            break;
                        default:
                            break;
                    }
                    
                }
                List<ExamScore> examScores = await context.ExamScores
                    .AsNoTracking()
                    .Where(e => e.ExsCusKey == customer.CusKey
                                && e.ExsExmKey == requirement.CredentialRequirementType.Ct3ExmKey)
                    .OrderByDescending(e => e.ExsDate)
                    .ToListAsync();

                if (examScores == null || examScores.Count <= 0) return true;

                foreach (ExamScore examScore in examScores)
                {
                    if ((minDate == null || (minDate.IsDateTimeOffsetValid() && examScore.ExsDate >= minDate)) 
                        && (examScore.ExsSuccessFlag 
                            || (requirement.CredentialRequirementType.Ct3MinExamScore != null 
                                && examScore.ExsTotalReceived >= requirement.CredentialRequirementType.Ct3MinExamScore)))
                    {
                        requirement.Cr3ExsKey = examScore.ExsKey;
                        context.Update(requirement);
                        await context.SaveChangesAsync();
                        break;
                    }
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

        private async Task<bool> InitDegreeAsync(ApplicationDbContext context, CredentialRequirement requirement)
        {
            try
            {
                Customer customer = await CredentialingUtils.GetCustomerFromProcessKey(context, requirement.Cr3Cr2Key);

                if (customer == null) return false;

                if (requirement.CredentialRequirementType == null)
                    requirement.CredentialRequirementType = await CredentialingUtils.GetCredentialRequirementTypeAsync(context, requirement.Cr3Ct3Key);

                if (requirement.CredentialRequirementType == null) return false;

                Degree degree = await context.Degrees
                    .AsNoTracking()
                    .Where(d => d.DegCusKey == customer.CusKey 
                                && d.DegDetKey == requirement.CredentialRequirementType.Ct3DetKey)
                    .OrderByDescending( d => d.DegDate)
                    .FirstOrDefaultAsync();

                if (degree == null) return true;

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

                //if (cePeriod.Ce2BalanceUnits <= 0 && cePeriod.Ce2RequiredUnits > 0) return true;

                //if (! await cePeriod.CalculateAsync(context)) return false;

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

                //if (cePeriod.Ce2ReportedUnits >= cePeriod.Ce2RequiredUnits && cePeriod.Ce2RequiredUnits > 0) return true;

                //if (!await cePeriod.CalculateAsync(context)) return false;

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
