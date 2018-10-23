using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework.Utils
{
    public class CredentialingUtils
    {
        public static DateTimeOffset CalculateDueDate(DateTimeOffset? floorDate, short? dueDateNumber, GlobalConstants.DueDateUnit dueDateUnit, GlobalConstants.DueDateRounding dueDateRounding)
        {
            try
            {
                if (floorDate == null || floorDate == DateTimeOffset.MinValue || dueDateNumber == null ||
                                dueDateNumber < 1) return DateTimeOffset.MinValue;

                DateTimeOffset dueDate;

                switch (dueDateUnit)
                {
                    case GlobalConstants.DueDateUnit.Day:
                        dueDate = ((DateTimeOffset)floorDate).AddDays((double)dueDateNumber);
                        break;

                    case GlobalConstants.DueDateUnit.Month:
                        dueDate = ((DateTimeOffset)floorDate).AddMonths((int)dueDateNumber);
                        break;

                    case GlobalConstants.DueDateUnit.Year:
                        dueDate = ((DateTimeOffset)floorDate).AddYears((int)dueDateNumber);
                        break;

                    case GlobalConstants.DueDateUnit.None:
                        return DateTimeOffset.MinValue;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(dueDateUnit), dueDateUnit, null);
                }

                switch (dueDateRounding)
                {
                    case GlobalConstants.DueDateRounding.None:
                        return dueDate;

                    case GlobalConstants.DueDateRounding.FirstOfMonth:
                        return new DateTime(dueDate.Year, dueDate.Month, 1);

                    case GlobalConstants.DueDateRounding.EndOfMonth:
                        return new DateTime(dueDate.Year, dueDate.Month, 1).AddMonths(1).AddDays(-1);

                    default:
                        throw new ArgumentOutOfRangeException(nameof(dueDateRounding), dueDateRounding, null);
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.RecursivelyFormatException());
                if (Environment.UserInteractive)
                {
                    Debugger.Break();
                }
                throw;
            }
        }

        public static async Task<DateTimeOffset> CalculateDateFromSp(DbContext context, string procedure, string parameterValue)
        {
            DbConnection connection = context.Database.GetDbConnection();
            await connection.OpenAsync();
            DbCommand command = connection.CreateCommand();
            command.CommandText = string.Format("EXEC dbo.{0} {1}", procedure, parameterValue);
            return await command.ExecuteScalarAsync() is DateTimeOffset rawStartDate ? rawStartDate : DateTimeOffset.MinValue;
        }

        public static async Task<short?> GetNextCredentialNumber(ApplicationDbContext context, Credential credential)
        {
            if (credential == null) return null;

            //TODO: add sproc capability
            //
            /// //if (credential.CredentialType == null) credential.CredentialType =
            //    context.CredentialTypes
            //        .Where(ct => ct.Ct1Key == credential.Cr1Ct1Key)
            //        .FirstOrDefault();

            short? nextCredentialNumber = await context.Credentials
                .Where(c => c.Cr1CredentialNumber != null)
                .DefaultIfEmpty()
                .MaxAsync(c => c.Cr1CredentialNumber);

            return ((int?) nextCredentialNumber).IsIntValidAndNotZero() ? nextCredentialNumber : (short?) 1;

            
        }

        public static bool SendMail(ApplicationDbContext context, Guid PerKey, Guid TemKey)
        {
            try
            {

                var mailMessage = new MailMessage
                {
                    From = new MailAddress("support@credengnext.net"),
                    Subject = context.CommunicationTemplates
                                .Where(t => t.TemKey.ToString() == TemKey.ToString())
                                .Select(t => t.TemSubject)
                                .FirstOrDefault(),
                    Body = context.CommunicationTemplates
                               .Where(t => t.TemKey.ToString() == TemKey.ToString())
                               .Select(t => t.TemHtml)
                               .FirstOrDefault() ?? throw new InvalidOperationException()
            };

                mailMessage.To.Add(context.Emails
                                    .Where(e => e.EmlPerKey.ToString() == PerKey.ToString() && e.EmlIsPrimary == true)
                                    .Select(e => e.EmlAddress)
                                    .FirstOrDefault());

                var smtpClient = new SmtpClient
                {
                    Credentials = new NetworkCredential("azure_62dfa7dda67490fad6f34fdee29cbab6@azure.com", "1425Market"),
                    Host = "smtp.sendgrid.net",
                    Port = 587
                };

                smtpClient.Send(mailMessage);

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

        /// <summary>
        /// Get Credential Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="processKey"></param>
        /// <returns>Returns process if successful, null if failure.</returns>
        public static async Task<CredentialProcess> GetCredentialProcessAsync(ApplicationDbContext context, Guid processKey, bool includeProcessType = false)
        {
            try
            {
                if (includeProcessType)
                    return await context.CredentialProcesses
                        .Include(p => p.CredentialProcessType)
                        .Where(p => processKey == p.Cr2Key)
                        .FirstOrDefaultAsync();

                return await context.CredentialProcesses
                    .FindAsync(processKey);
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

        public static async Task<CredentialType> GetCredentialTypeAsync(ApplicationDbContext context,
            Guid credentialTypeKey)
        {
            try
            {
                return await context.CredentialTypes.FindAsync(credentialTypeKey);
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


        /// <summary>
        /// Get Credential Requirement Type
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirementTypeKey"></param>
        /// <returns>Returns Requirement Type if successful, null on failure.</returns>
        public static async Task<CredentialRequirementType> GetCredentialRequirementTypeAsync(ApplicationDbContext context, Guid requirementTypeKey, bool includeTemplate = false)
        {
            try
            {
                if (includeTemplate)
                    return await context.CredentialRequirementTypes
                        .Include(rt => rt.Template)
                        .Where(rt => rt.Ct3Key == requirementTypeKey)
                        .FirstOrDefaultAsync();

                return await context.CredentialRequirementTypes
                    .FindAsync(requirementTypeKey);
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

        public static async Task<Credential> GetCredentialAsync(ApplicationDbContext context, Guid credentialKey, bool includeCredentialType = false)
        {
            try
            {
                if (includeCredentialType)
                    return await context.Credentials
                        .Include(c => c.CredentialType)
                        .Where(c => credentialKey == c.Cr1Key)
                        .FirstOrDefaultAsync();

                return await context.Credentials.FindAsync(credentialKey);
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

        /// <summary>
        /// Get Credential Status type, and include Status.State if specified.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="statusKey"></param>
        /// <param name="includeState"></param>
        /// <returns>Returns status type if successful, null on failure.</returns>
        public static async Task<CredentialStatusType> GetCredentialStatusTypeAsync(ApplicationDbContext context,
            Guid statusKey, bool includeState = false)
        {
            try
            {
                if (includeState)
                    return await context.CredentialStatusTypes
                        .Include(st => st.State)
                        .Where(st => st.Ct6Key == statusKey)
                        .FirstOrDefaultAsync();

                return await context.CredentialStatusTypes
                    .FindAsync(statusKey);
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

        public static async Task<CredentialProcessType> GetCredentialProcessTypeAsync(ApplicationDbContext context, Guid ct2Key)
        {
            try
            {
                return await context.CredentialProcessTypes.FindAsync(ct2Key);
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

        public static async Task<List<CredentialProcessTypeXCredentialRequirementType>> GetCredentialProcessTypeXCredentialRequirementTypesForProcessTypeAsync(ApplicationDbContext context, Guid processTypeKey)
        {
            try
            {
                return await context
                    .CredentialProcessTypeXCredentialRequirementTypes
                    .Where(pxr => pxr.Ct5Ct2Key == processTypeKey)
                    .ToListAsync();
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

        /// <summary>
        /// Get Credential Requirements related to a Credential Process
        /// </summary>
        /// <param name="context"></param>
        /// <param name="processKey"></param>
        /// <param name="includeDependentRequirements"></param>
        /// <returns>Returns List of Requirements if successful, or null if there are no requirements or it fails.</returns>
        public static async Task<List<CredentialRequirement>> GetCredentialRequirementsForProcessAsync(
            ApplicationDbContext context, Guid processKey
            , bool includeDependentRequirements = false)
        {
            try
            {
                if (includeDependentRequirements)
                {
                    return await context.CredentialRequirements
                        .Include(r => r.DependentRequirements)
                        .Include(r => r.CredentialRequirementType)
                        .Include(r => r.Status)
                        .ThenInclude(s => s.State)
                        .Where(r => r.Cr3Cr2Key == processKey)
                        .ToListAsync();
                }

                return await context.CredentialRequirements
                    .Include(r => r.CredentialRequirementType)
                    .Include(r => r.Status)
                    .ThenInclude(s => s.State)
                    .Where(r => r.Cr3Cr2Key == processKey)
                    .ToListAsync();
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
    }
}
