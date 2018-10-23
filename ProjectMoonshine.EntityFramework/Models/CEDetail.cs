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

                List<CEPeriod> CEPeriods = await context.CEPeriods
                    .Where(p => p.Ce2CusKey == Ce1CusKey
                                && p.Ce2StartDate <= Ce1ActivityDate 
                                && p.Ce2EndDate >= Ce1ActivityDate)
                    .ToListAsync();

                if (CEPeriods != null && CEPeriods.Count > 0)
                {
                    foreach (CEPeriod cePeriod in CEPeriods)
                    {
                        if (!await cePeriod.UpdateAsync(context)) return false;
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
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

                context.Update(this);

                List<CEPeriod> CEPeriods = await context.CEPeriods
                    .Where(p => p.Ce2CusKey == Ce1CusKey
                                && p.Ce2StartDate <= Ce1ActivityDate
                                && p.Ce2EndDate >= Ce1ActivityDate)
                    .ToListAsync();

                if (CEPeriods != null && CEPeriods.Count > 0)
                {
                    foreach (CEPeriod cePeriod in CEPeriods)
                    {
                        if (!await cePeriod.UpdateAsync(context)) return false;
                    }
                }

                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
