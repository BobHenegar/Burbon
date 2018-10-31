using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CEPeriod")]
    public class CEPeriod : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ce2";
        [Key]
        public Guid Ce2Key { get; set; }

        public Guid Ce2CusKey { get; set; }

        [DisplayName("CE Type")]
        public Guid? Ce2CetKey { get; set; }

        [DisplayName("Start Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTimeOffset Ce2StartDate { get; set; }

        [DisplayName("End Date")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTimeOffset Ce2EndDate { get; set; }

        [DisplayName("Required Units")]
        public float Ce2RequiredUnits { get; set; }

        [DisplayName("Reported Units")]
        public float Ce2ReportedUnits { get; set; }

        [DisplayName("Accepted Units")]
        public float Ce2AcceptedUnits { get; set; }

        [DisplayName("Balance Units")]
        public float Ce2BalanceUnits { get; set; }

        public DateTimeOffset Ce2AddDate { get; set; }

        public DateTimeOffset? Ce2ChangeDate { get; set; }

        public string Ce2AddUser { get; set; }

        public string Ce2ChangeUser { get; set; }

        [ForeignKey("Ce2CusKey")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("Ce2CetKey")]
        public CEType CeType { get; set; }

        public async Task<bool> UpdateAsync(ApplicationDbContext context)
        {
            try
            {
                if (!await CalculateAsync(context)) return false;

                bool isMet = Ce2RequiredUnits > 0 && Ce2BalanceUnits <= 0;

                bool isComplete = Ce2RequiredUnits > 0 && Ce2ReportedUnits >= Ce2RequiredUnits;

                if (isMet || isComplete  )
                {
                    CredentialRequirement credentialRequirement = await context.CredentialRequirements
                        .Include(r => r.Status)
                        .ThenInclude(s => s.State)
                        .Where(r => r.Cr3Ce2Key == Ce2Key)
                        .FirstOrDefaultAsync();

                    if (credentialRequirement != null 
                        && isMet 
                        && !credentialRequirement.Cr3MetDate.IsDateTimeOffsetValid() 
                        && !await credentialRequirement.UpdateAsync(context))
                        return false;

                    if (credentialRequirement != null
                        && isComplete && credentialRequirement.Status?.State != null 
                        && CredentialingUtils.IsUnlocked(credentialRequirement.Status.State)
                        && !await credentialRequirement.UpdateAsync(context))
                        return false;
                        
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

        public async Task<bool> CalculateAsync(ApplicationDbContext context)
        {
            try
            {
                Ce2AcceptedUnits = await context.CEDetails
                    .Where(d => d.Ce1ActivityDate >= Ce2StartDate
                                && d.Ce1ActivityDate <= Ce2EndDate
                                && (!Ce2CetKey.IsGuidValid() || d.Ce1CetKey == Ce2CetKey)
                                && d.Ce1CusKey == Ce2CusKey
                                && d.Ce1VerifiedFlag)
                    .SumAsync(d => d.Ce1Units);

                Ce2ReportedUnits = await context.CEDetails
                    .Where(d => d.Ce1ActivityDate >= Ce2StartDate
                                && d.Ce1ActivityDate <= Ce2EndDate
                                && (!Ce2CetKey.IsGuidValid() || d.Ce1CetKey == Ce2CetKey)
                                && d.Ce1CusKey == Ce2CusKey
                                )
                    .SumAsync(d => d.Ce1Units);

                Ce2BalanceUnits = Ce2RequiredUnits - Ce2AcceptedUnits > 0 ? Ce2RequiredUnits - Ce2AcceptedUnits : 0;

                context.Update(this);

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

        public async Task<bool> AddAsync(ApplicationDbContext context)
        {
            try
            {
                await context.AddAsync(this);
                await context.SaveChangesAsync();
                if (! await CalculateAsync(context)) return false;

                context.Update(this);

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
