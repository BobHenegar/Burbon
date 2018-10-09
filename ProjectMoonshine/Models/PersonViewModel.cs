using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.Models
{
    public class PersonViewModel
    {
        public Guid PerKey { get; set; }
        public Guid CusPerKey { get; set; }

        public string PerFirstName { get; set; }
        public string PerMiddleName { get; set; }

        public string PerLastName { get; set; }

        public DateTimeOffset PerDateOfBirth { get; set; }

        public string PerGender { get; set; }
        public DateTimeOffset PerAddDate { get; set; }
        public DateTimeOffset? PerChangeDate { get; set; }
        public string PerAddUser { get; set; }
        public string PerChangeUser { get; set; }

        #region Navigation Properties
        public virtual Customer Customer { get; set; }
        public virtual List<Address> Addresses { get; set; }
        public virtual List<Email> Emails { get; set; }
        public virtual List<Phone> Phones { get; set; }
    }
}
