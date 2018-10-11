using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.EntityFramework.Models
{
    [Table("CredentialRequirementTemplate")]
    public class CredentialRequirementTemplate : EntityBase
    {
        [NotMapped]
        public const string Prefix = "Ct8";
        [Key]
        public Guid Ct8Key { get; set; }

        public string Ct8Template { get; set; }

        public string Ct8Description { get; set; }

        //public string Ct8CheckProcedure { get; set; }

        //public string Ct8FindProcedure { get; set; }

        //public bool Ct8SetDataKeyFlag { get; set; }

        //public string Ct8InitMethodBehavior { get; set; }

        //public string Ct8EvalMethodBehavior { get; set; }

        public DateTimeOffset Ct8AddDate { get; set; }

        public DateTimeOffset? Ct8ChangeDate { get; set; }

        public string Ct8AddUser { get; set; }

        public string Ct8ChangeUser { get; set; }

        public List<CredentialRequirementType> CredentialRequirementTypes { get; set; }

    }
}
