using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.ViewModels
{
    public class CredentialProcessTypeViewModel
    {
        public CredentialProcessType CredentialProcessType { get; set; }
        private readonly ApplicationDbContext _context;

        public SelectList Ct2StartCt6KeySelectList { get; set; }
        public SelectList Ct2CompletedCt6KeySelectList { get; set; }

        public SelectList Ct2SubmittedCt6KeySelectList { get; set; }

        public SelectList Ct2NextCt2KeySelectList { get; set; }

        public CredentialProcessTypeViewModel(ApplicationDbContext context, CredentialProcessType credentialProcessType = null)
        {
            _context = context;
            CredentialProcessType = credentialProcessType ?? new CredentialProcessType();
            Ct2StartCt6KeySelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialProcessType.Ct2StartCt6Key);
            Ct2SubmittedCt6KeySelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialProcessType.Ct2SubmittedCt6Key);
            Ct2CompletedCt6KeySelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialProcessType.Ct2CompletedCt6Key);
            Ct2NextCt2KeySelectList = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", CredentialProcessType.Ct2NextCt2Key);
        }
    }
}
