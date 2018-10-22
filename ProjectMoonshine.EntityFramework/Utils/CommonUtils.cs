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
        /// <summary>
        /// Check if Guid? is not null and is not default Guid value / empty.
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public static bool IsGuidValid(this Guid? guid)
        {
            return guid.HasValue && guid != Guid.Empty;
        }

        /// <summary>
        /// Check if dateTimeOffset is not null and not default.
        /// </summary>
        /// <param name="dateTimeOffset"></param>
        /// <returns></returns>
        public static bool IsDateTimeOffsetValid( this DateTimeOffset? dateTimeOffset)
        {
            return dateTimeOffset.HasValue && dateTimeOffset != DateTimeOffset.MinValue;
        }

        /// <summary>
        /// Check if int is not null and not default value (0)
        /// </summary>
        /// <param name="integer"></param>
        /// <returns>Returns true if int is not null and not 0.</returns>
        public static bool IsIntValidAndNotZero(this int? integer)
        {
            return integer.HasValue && integer != 0;
        }
    }
}
