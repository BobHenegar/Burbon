using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class CredentialProcessType
    {
        [Key]
        public Guid ct2Key { get; set; }

        public String ct2Process { get; set; }

        public String ct2Description { get; set; }

        public String ct2TrocessType { get; set; }

        public DateTimeOffset ct2StartSate { get; set; }

        public DateTimeOffset ct2EndDate { get; set; }

        public String ct2Cr2StartStatus { get; set; }

        public String ct2Cr2_submittedStatus { get; set; }

        public String ct2Cr2CompletionStatus { get; set; }

        public String ct2Cr1StartStatus { get; set; }

        public String ct2Cr1CompletionStatus { get; set; }

        public Guid ct2NextCt2Key { get; set; }

        public Int16 ct2DueDateNumber { get; set; }

        public String ct2DueDateUnit { get; set; }

        public String ct2DueDateRounding { get; set; }

        public String ct2LongDescription { get; set; }

        public String ct2AddUser { get; set; }

        public DateTimeOffset ct2AddDate { get; set; }

        public String ct2ChangeUser { get; set; }

        public DateTimeOffset ct2ChangeDate { get; set; }

    }

}
