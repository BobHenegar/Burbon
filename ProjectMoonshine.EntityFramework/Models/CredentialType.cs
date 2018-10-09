using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectMoonshine.Models
{
    public class CredentialType
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Ct1Key { get; set; }

        public string Ct1Credential { get; set; }

        public string Ct1Description { get; set; }

        public string Ct1LongDescription { get; set; }

        public string ct1Designation { get; set; }

        public string ct1DesignationHtml { get; set; }

        public DateTimeOffset ct1StartDate { get; set; }

        public DateTimeOffset ct1EndDate { get; set; }

        public Guid ct1InitCt2Key { get; set; }

        public string ct1InitStatus { get; set; }

        public Guid ct1RenewCt2Key { get; set; }

        public string ct1RenewStatus { get; set; }

        public Guid ct1ReinstateCt2Key { get; set; }

        public string ct1ReinstateStatus { get; set; }

        public string ct1CstType { get; set; }

        public string ct1AddUser { get; set; }

        public DateTimeOffset ct1AddDate { get; set; }

        public string ct1ChangeUser { get; set; }

        public DateTimeOffset ct1ChangeDate { get; set; }

    }

}

