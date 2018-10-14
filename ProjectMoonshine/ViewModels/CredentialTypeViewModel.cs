using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.ViewModels
{
    public class CredentialTypeViewModel 
    {

        public CredentialType CredentialType { get; set; }
        private ApplicationDbContext _context;
        public SelectList InitProcessTypeSelectList { get; set; }
        public SelectList InitStatusTypeSelectList { get; set; }

        public SelectList RenewProcessTypeSelectList { get; set; }

        public SelectList RenewStatusTypeSelectList { get; set; }

        public CredentialTypeViewModel(ApplicationDbContext context, CredentialType credentialType = null)
        {
            _context = context;
            CredentialType = credentialType ?? new CredentialType();
            InitProcessTypeSelectList = new SelectList(context.CredentialProcessTypes, "Ct2Key", "Ct2Process", CredentialType.Ct1InitCt2Key);
            InitStatusTypeSelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code",
                CredentialType.Ct1InitCt6Key);
            RenewProcessTypeSelectList = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", CredentialType.Ct1RenewCt2Key);
            RenewStatusTypeSelectList =  new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialType.Ct1RenewCt6Key);
        }
    }
}
