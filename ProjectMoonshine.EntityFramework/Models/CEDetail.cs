using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CEDetail")]
    public class CEDetail: EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ce1";
        [Key]
        public Guid Ce1Key { get; set; }

        [DisplayName("Customer")]
        public Guid Ce1CusKey { get; set; }

        [DisplayName("Activity Date")]
        public DateTimeOffset Ce1ActivityDate { get; set; }

        [Required]
        [DisplayName("CE Type")]
        public Guid Ce1CetKey { get; set; }

        [Required]
        [DisplayName("Activity Description")]
        public string Ce1ActivityDescription { get; set; }

        [DisplayName("Units")]
        public float Ce1Units { get; set; }

        [DisplayName("Verified Flag")]
        public bool Ce1VerifiedFlag { get; set; }


        public DateTimeOffset Ce1AddDate { get; set; }

        public DateTimeOffset? Ce1ChangeDate { get; set; }

        public string Ce1AddUser { get; set; }

        public string Ce1ChangeUser { get; set; }

        [ForeignKey("Ce1CusKey")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("Ce1CetKey")]
        public virtual CEType CeType { get; set; }

        public async Task<bool> AddAsync(ApplicationDbContext context)
        {
            try
            {
                
                if (!CommonUtils.IsGuidValid(Ce1CetKey)) return false;
                if (!CommonUtils.IsGuidValid(Ce1CusKey)) return false;
                if (!CommonUtils.IsDateTimeOffsetValid(Ce1ActivityDate)) return false;
                if (string.IsNullOrWhiteSpace(Ce1ActivityDescription)) return false;

                if (!CommonUtils.IsGuidValid(Ce1Key)) Ce1Key = new Guid();

                await context.AddAsync(this);
                await context.SaveChangesAsync();

                if (!await UpdateCePeriodsAsync(context, Ce1CusKey)) return false;

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

        public async Task<bool> UpdateAsync(ApplicationDbContext context)
        {
            try
            {
                if (!CommonUtils.IsGuidValid(Ce1CetKey)) return false;
                if (!CommonUtils.IsGuidValid(Ce1CusKey)) return false;
                if (!CommonUtils.IsDateTimeOffsetValid(Ce1ActivityDate)) return false;
                if (!CommonUtils.IsGuidValid(Ce1Key)) return false;
                if (string.IsNullOrWhiteSpace(Ce1ActivityDescription)) return false;

                Guid originalCe1CusKey = await context.CEDetails
                    .AsNoTracking()
                    .Where(d => d.Ce1Key == Ce1Key)
                    .Select(d => d.Ce1CusKey)
                    .FirstOrDefaultAsync();

                context.Update(this);
                await context.SaveChangesAsync();

                if (!await UpdateCePeriodsAsync(context, Ce1CusKey)) return false;

                if (CommonUtils.IsGuidValid(originalCe1CusKey) && Ce1CusKey != originalCe1CusKey)
                {
                    if (!await UpdateCePeriodsAsync(context, originalCe1CusKey)) return false;
                }

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

        public async Task<bool> UpdateCePeriodsAsync(ApplicationDbContext context, Guid cusKey)
        {
            try
            {
                List<CEPeriod> cePeriods = await context.CEPeriods
                    .Where(p => p.Ce2CusKey == cusKey)
                    .ToListAsync();

                if (cePeriods == null || cePeriods.Count <= 0) return true;

                foreach (CEPeriod cePeriod in cePeriods)
                {
                    if (!await cePeriod.UpdateAsync(context)) return false;
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
    }
}
