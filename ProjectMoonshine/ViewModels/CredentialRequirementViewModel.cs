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
    public class CredentialRequirementViewModel
    {
        public CredentialRequirement CredentialRequirement { get; set; }
        private readonly ApplicationDbContext _context;

        public SelectList Cr3Ct3KeySelectList { get; set; }

        public SelectList Cr3Ct6KeySelectList { get; set; }

        public SelectList Cr3Cr2KeySelectList { get; set; }

        public CredentialRequirementViewModel(ApplicationDbContext context, CredentialRequirement credentialRequirement = null)
        {
            _context = context;
            CredentialRequirement = credentialRequirement ?? new CredentialRequirement();

            var processDropDownList = _context.CredentialProcesses
                .Include(p => p.Credential)
                    .ThenInclude(c => c.Customer)
                    .ThenInclude(d => d.Person)
                .Include(p => p.Credential)
                    .ThenInclude(c => c.CredentialType)
                .Include(p => p.CredentialProcessType)
                .Select(p => new { p.Cr2Key, Text = BuildProcessListString(p)});

            Cr3Cr2KeySelectList = new SelectList(processDropDownList, "Cr2Key", "Text", CredentialRequirement.Cr3Cr2Key);
            Cr3Ct3KeySelectList = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Requirement", CredentialRequirement.Cr3Ct3Key);
            Cr3Ct6KeySelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialRequirement.Cr3Ct6Key);
        }

        private static string BuildProcessListString(CredentialProcess credentialProcess)
        {
            return string.Format("{0}, {1}: {2} - {3}"
                            , credentialProcess.Credential.Customer.Person.PerLastName
                                    , credentialProcess.Credential.Customer.Person.PerFirstName
                                    , credentialProcess.Credential.CredentialType.Ct1Credential
                                    , credentialProcess.CredentialProcessType.Ct2Process);
        }
    }
}
