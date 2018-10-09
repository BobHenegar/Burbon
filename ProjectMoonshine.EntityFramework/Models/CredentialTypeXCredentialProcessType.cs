using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class CredentialTypeXCredentialProcessType
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ct4_key { get; set; }
        public Guid ct4_ct1_key { get; set; }
        public Guid ct4_ct2_key { get; set; }
        public DateTimeOffset ct4_start_date { get; set; }
        public DateTimeOffset ct4_end_date { get; set; }
        public Int16 ct4_order { get; set; }
        public String ct4AddUser { get; set; }
        public DateTimeOffset ct4AddDate { get; set; }
        public String ct4ChangeUser { get; set; }
        public DateTimeOffset ct4ChangeDate { get; set; }
    }
}
