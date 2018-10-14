using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.ViewModels
{
    public class CredentialTypeXCredentialProcessTypeViewModel
    {
        public CredentialTypeXCredentialProcessType CredentialTypeXCredentialProcessType { get; set; }
        private readonly ApplicationDbContext _context;

        public SelectList Ct4Ct1KeySelectList { get; set; }
        public SelectList Ct4Ct2KeySelectList { get; set; }

        public CredentialTypeXCredentialProcessTypeViewModel(ApplicationDbContext context, CredentialTypeXCredentialProcessType credentialTypeXCredentialProcessType = null)
        {
            _context = context;
            CredentialTypeXCredentialProcessType =
                credentialTypeXCredentialProcessType ?? new CredentialTypeXCredentialProcessType();
            Ct4Ct2KeySelectList = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", CredentialTypeXCredentialProcessType.Ct4Ct2Key);
            Ct4Ct1KeySelectList = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Credential", CredentialTypeXCredentialProcessType.Ct4Ct1Key);
        }

    }
}
