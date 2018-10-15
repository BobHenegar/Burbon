using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.ViewModels
{
    public class CredentialProcessViewModel
    {
        public CredentialProcess CredentialProcess { get; set; }
        private readonly ApplicationDbContext _context;

        public SelectList Cr2Ct2KeySelectList { get; set; }
        public SelectList Cr2Cr1KeySelectList { get; set; }
        public SelectList Cr2Ct6KeySelectList { get; set; }

        public CredentialProcessViewModel(ApplicationDbContext context, CredentialProcess credentialProcess = null)
        {
            _context = context;
            CredentialProcess = credentialProcess ?? new CredentialProcess();

            Cr2Ct2KeySelectList = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", CredentialProcess.Cr2Ct2Key);
            //Cr2Cr1KeySelectList = new SelectList(_context.Credentials, "Cr1Key", "Cr1Credential", CredentialProcess.Cr2Cr1Key);
            Cr2Ct6KeySelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialProcess.Cr2Ct6Key);
        }
    }
}
