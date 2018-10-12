using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialProcess")]
    public class CredentialProcess : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Cr2";
        [Key]
        public Guid Cr2Key { get; set; }

        public Guid Cr2Ct2Key { get; set; }

        public Guid Cr2Cr1Key { get; set; }

        //public Guid Cr2PerKey { get; set; }

        //public Guid Cr2Cr2Key { get; set; }

        [DisplayName("Status")]
        public Guid Cr2Ct6Key { get; set; }


        //public DateTimeOffset Cr2StatusChangeDate { get; set; }

        //public string Cr2StatusChangeUser { get; set; }

        [DisplayName("Start Date")]
        public DateTimeOffset? Cr2StartDate { get; set; }

        //public DateTimeOffset Cr2DueDate { get; set; }

        //public DateTimeOffset Cr2SubmitDate { get; set; }

            [DisplayName("Cr2CompletionDate")]
        public DateTimeOffset? Cr2CompletionDate { get; set; }

        [DisplayName("Termination Date")]
        public DateTimeOffset? Cr2TerminationDate { get; set; }

        //public DateTimeOffset Cr2ExtensionDate { get; set; }

        //public DateTimeOffset Cr2ExtensionGrantedDate { get; set; }

        //public string Cr2ExtensionGrantedUser { get; set; }

        //public string Cr2ExtensionGrantedReason { get; set; }

        //public string Cr2Note { get; set; }

        //public string Cr2Owner { get; set; }

        //public DateTimeOffset Cr2Due2Date { get; set; }

        //public DateTimeOffset Cr2Extension2Date { get; set; }

        //public DateTimeOffset Cr2Extension2GrantedDate { get; set; }

        //public DateTimeOffset Cr2Extension2GrantedUser { get; set; }

        //public string Cr2Extension2GrantedReason { get; set; }

        //public DateTimeOffset Cr2InactiveStartDate { get; set; }

        //public DateTimeOffset Cr2InactiveEndDate { get; set; }

        //public DateTimeOffset Cr2FulfillmentDate { get; set; }

        //public string Cr2EthicsDisclosure { get; set; }

        //public string Cr2EthicsSummary { get; set; }

        //public DateTimeOffset Cr2EthicsClearedDate { get; set; }

        //public string Cr2Agreement { get; set; }

        //public DateTimeOffset Cr2AgreementDate { get; set; }

        //public DateTimeOffset Cr2AuditSelectDate { get; set; }

        //public DateTimeOffset Cr2AuditCompleteDate { get; set; }

        //public DateTimeOffset Cr2AuditCompleteUser { get; set; }

        //public Guid Cr2DataKey { get; set; }

        //public string Cr2ProcessNumber { get; set; }


        public string Cr2AddUser { get; set; }

        public DateTimeOffset Cr2AddDate { get; set; }

        public string Cr2ChangeUser { get; set; }

        public DateTimeOffset? Cr2ChangeDate { get; set; }

        //public virtual List<CredentialRequirement>  CredentialRequirements { get; set; }
        //[ForeignKey("Cr2Cr1Key")]
        public virtual Credential Credential { get; set; }
        //public virtual Person Person { get; set; }

        public virtual List<CredentialRequirement> CredentialRequirements { get; set; }

        [ForeignKey("Cr2Ct6Key")]
        public CredentialStatusType StatusType { get; set; }

        [ForeignKey("Cr2Ct2Key")]
        public virtual CredentialProcessType CredentialProcessType { get; set; }

        public async Task<bool> Add(ApplicationDbContext context)
        {
            try
            {
                if (Cr2Cr1Key == Guid.Empty) return false;
                if (Cr2Ct2Key == Guid.Empty) return false;
                if (Cr2Key == Guid.Empty) Cr2Key = Guid.NewGuid();
                CredentialProcessType = context.CredentialProcessTypes.FirstOrDefault(pt => pt.Ct2Key == Cr2Ct2Key);

                
                if (Cr2Ct6Key == Guid.Empty) Cr2Ct6Key =   Guid.Parse(CredentialProcessType.Ct2StartCt6Key.ToString()) ;
// TODO: set credential status based on ct2cr1startct6key

                context.Add(this);

                Credential = context.Credentials.Where(c => Cr2Cr1Key == c.Cr1Key).Include(c=> c.CredentialType).FirstOrDefault();

                Init();

                if (Cr2StartDate == null || Cr2StartDate == DateTimeOffset.MinValue)
                {
                    if (!string.IsNullOrWhiteSpace(CredentialProcessType.Ct2CalculateStartDateSp))
                    {
                        Cr2StartDate = CalculateDateFromSp(context, CredentialProcessType.Ct2CalculateStartDateSp,
                            Cr2Key.ToString());

                    }

                    if (Cr2StartDate == null || Cr2StartDate == DateTimeOffset.MinValue)
                    {
                        DateTimeOffset? cr2startdatetemp = context.CredentialProcesses
                            .Include(p => p.Credential)
                            .Where(p => p.Cr2Key != Cr2Key 
                                        && p.Cr2Ct2Key == CredentialProcessType.Ct2Key 
                                        && p.Credential.Cr1CusKey == Credential.Cr1CusKey)
                            .OrderByDescending(p => p.Cr2StartDate)
                            .FirstOrDefault()
                            .Cr2StartDate;

                        if (cr2startdatetemp != null && cr2startdatetemp != DateTimeOffset.MinValue)
                            Cr2StartDate = ((DateTimeOffset) cr2startdatetemp).AddDays(1);
                    }
                    if (Cr2StartDate == null || Cr2StartDate == DateTimeOffset.MinValue) Cr2StartDate = DateTimeOffset.Now;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return false;
        }

        private DateTimeOffset CalculateDateFromSp(DbContext context, string procedure, string parameterValue)
        {
            DbConnection connection = context.Database.GetDbConnection();
            connection.Open();
            DbCommand command = connection.CreateCommand();
            command.CommandText = string.Format("EXEC dbo.{0} {1}", procedure, parameterValue);
            return command.ExecuteScalar() is DateTimeOffset rawStartDate ? rawStartDate : DateTimeOffset.MinValue;
        }


        public virtual void Init()
        {

        }


    }
}
