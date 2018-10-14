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

            if (CredentialType.CredentialTypeXCredentialProcessTypes == null)
                CredentialType.CredentialTypeXCredentialProcessTypes =
                    _context.CredentialTypeXCredentialProcessTypes.Where(cxp => cxp.Ct4Ct1Key == CredentialType.Ct1Key).ToList();

            IQueryable<CredentialProcessType> connectedCredentialProcessTypes = _context.CredentialProcessTypes
                .Where( pt => CredentialType.CredentialTypeXCredentialProcessTypes
                    .Where(t => (t.Ct4EndDate == null || t.Ct4EndDate >= DateTimeOffset.Now) 
                                && (t.Ct4StartDate == null || t.Ct4StartDate <= DateTimeOffset.Now))
                    .Select(i => i.Ct4Ct2Key)
                    .Contains(pt.Ct2Key));

            InitProcessTypeSelectList = new SelectList(connectedCredentialProcessTypes, "Ct2Key"
                , "Ct2Process", CredentialType.Ct1InitCt2Key);
            InitStatusTypeSelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code",
                CredentialType.Ct1InitCt6Key);
            RenewProcessTypeSelectList = new SelectList(connectedCredentialProcessTypes, "Ct2Key", "Ct2Process", CredentialType.Ct1RenewCt2Key);
            RenewStatusTypeSelectList =  new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialType.Ct1RenewCt6Key);
        }
    }
}
