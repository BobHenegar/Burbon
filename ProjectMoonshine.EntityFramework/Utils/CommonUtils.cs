using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework.Utils
{
    public static class CommonUtils
    {
        public static bool IsGuidValid(this Guid? guid)
        {
            return guid.HasValue && guid != Guid.Empty;
        }

        public static bool IsDateTimeOffsetValid( this DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset.HasValue && dateTimeOffset != DateTimeOffset.MinValue;
        }

        public static bool IsIntValid(this int? integer)
        {
            return integer.HasValue && integer != 0;
        }
    }
}
