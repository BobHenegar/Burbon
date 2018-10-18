using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework.Utils
{
    public class CommonUtils
    {
        public static bool IsGuidValid(Guid? guid)
        {
            return guid != null && guid != Guid.Empty;
        }

        public static bool IsDateTimeOffsetValid(DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset != null && dateTimeOffset != DateTimeOffset.MinValue;
        }

        public static bool IsIntValid(int? integer)
        {
            return integer != null && integer != 0;
        }
    }
}
