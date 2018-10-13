using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using Microsoft.EntityFrameworkCore;

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
                return DateTimeOffset.MinValue;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
                return DateTimeOffset.MinValue;
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
    }
}
