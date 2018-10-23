using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.ViewModels
{
    public class CredentialRequirementTypeViewModel
    {
        public CredentialRequirementType CredentialRequirementType { get; set; }

        private readonly ApplicationDbContext _context;

        public SelectList Ct3CompleteCt6KeySelectList { get; set; }

        public SelectList Ct3MetCt6KeySelectList { get; set; }

        public SelectList Ct3StartCt6KeySelectList { get; set; }

        public SelectList Ct3SubmitCt6KeySelectList { get; set; }

        public SelectList Ct3Ct8KeySelectList { get; set; }

        public SelectList Ct3DependentCt6KeySelectList { get; set; }

        public SelectList Ct3StartCr2Ct6KeySelectList { get; set; }

        public SelectList Ct3MetCr2Ct6KeySelectList { get; set; }

        public SelectList Ct3CetKeySelectList { get; set; }

        public CredentialRequirementTypeViewModel(ApplicationDbContext context, CredentialRequirementType credentialRequirementType = null)
        {
            _context = context;
            CredentialRequirementType = credentialRequirementType ?? new CredentialRequirementType();

            Ct3CompleteCt6KeySelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3CompleteCt6Key);
            Ct3MetCt6KeySelectList =  new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3MetCt6Key);
            Ct3StartCt6KeySelectList =  new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3StartCt6Key);
            Ct3SubmitCt6KeySelectList =  new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3SubmitCt6Key);
            Ct3Ct8KeySelectList =  new SelectList(_context.CredentialRequirementTemplates, "Ct8Key", "Ct8Template", CredentialRequirementType.Ct3Ct8Key);
            Ct3DependentCt6KeySelectList =  new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3DependentCt6Key);
            Ct3StartCr2Ct6KeySelectList =  new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3StartCr2Ct6Key);
            Ct3MetCr2Ct6KeySelectList =  new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3MetCr2Ct6Key);
            Ct3CetKeySelectList = new SelectList(_context.CEType, "CetKey", "CetCode", CredentialRequirementType.Ct3CetKey);

        }
    }
}
