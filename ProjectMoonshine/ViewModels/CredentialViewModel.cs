using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.ViewModels
{
    public class CredentialViewModel
    {
        public Credential Credential { get; set; }

        private readonly ApplicationDbContext _context;

        public SelectList Cr1Ct1KeySelectList { get; set; }

        public SelectList Cr1CusKeySelectList { get; set; }

        public SelectList Cr1Ct2KeySelectList { get; set; }

        public SelectList Cr1Ct6KeySelectList { get; set; }

        public CredentialViewModel(ApplicationDbContext context, Credential credential = null)
        {
            _context = context;
            Credential = credential ?? new Credential();
            Cr1CusKeySelectList = new SelectList(_context
                .People
                .Select(s => new { s.PerCusKey, Text = s.PerLastName + ", " + s.PerFirstName })
                , "PerCusKey", "Text");

            Cr1Ct2KeySelectList = new SelectList(_context.CredentialProcessTypes, "Ct2Key", "Ct2Process", Credential.Cr1Ct2Key);
            Cr1Ct6KeySelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", Credential.Cr1Ct6Key);
            Cr1Ct1KeySelectList = new SelectList(_context.CredentialTypes, "Ct1Key", "Ct1Credential", Credential.Cr1Ct1Key);

        }
    }
}
