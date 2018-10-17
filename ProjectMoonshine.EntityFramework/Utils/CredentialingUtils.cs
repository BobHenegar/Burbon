using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
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

        public static DateTimeOffset CalculateDateFromSp(DbContext context, string procedure, string parameterValue)
        {
            DbConnection connection = context.Database.GetDbConnection();
            connection.Open();
            DbCommand command = connection.CreateCommand();
            command.CommandText = string.Format("EXEC dbo.{0} {1}", procedure, parameterValue);
            return command.ExecuteScalar() is DateTimeOffset rawStartDate ? rawStartDate : DateTimeOffset.MinValue;
        }

        public static short? GetNextCredentialNumber(ApplicationDbContext context, Credential credential)
        {
            if (credential == null) return null;

            //TODO: add sproc capability
            //
            /// //if (credential.CredentialType == null) credential.CredentialType = 
            //    context.CredentialTypes
            //        .Where(ct => ct.Ct1Key == credential.Cr1Ct1Key)
            //        .FirstOrDefault();

            short? nextCredentialNumber = context.Credentials
                .Where(c => c.Cr1CredentialNumber != null)
                .DefaultIfEmpty()
                .Max(c => c.Cr1CredentialNumber);

            if (nextCredentialNumber != null && nextCredentialNumber == 0)
                return nextCredentialNumber;

            return null;
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
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
