using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectMoonshine.Models
{
    public class CredentialTypeXCredentialProcessType
    {
        public Guid ct4_key { get; set; }
        public Guid ct4_ct1_key { get; set; }
        public Guid ct4_ct2_key { get; set; }
        public DateTimeOffset ct4_start_date { get; set; }
        public DateTimeOffset ct4_end_date { get; set; }
        public Int16 ct4_order { get; set; }
        public String ct4_add_user { get; set; }
        public DateTimeOffset ct4_add_date { get; set; }
        public String ct4_change_user { get; set; }
        public DateTimeOffset ct4_change_date { get; set; }
    }
}
