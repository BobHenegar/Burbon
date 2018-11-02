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
    public class CredentialRequirementTypeViewModel
    {
        public CredentialRequirementType CredentialRequirementType { get; set; }

        private readonly ApplicationDbContext _context;

        public SelectList Ct3CompleteCt6KeySelectList { get; set; }

        public SelectList Ct3MetCt6KeySelectList { get; set; }

        public SelectList Ct3StartCt6KeySelectList { get; set; }

        public SelectList Ct3SubmitCt6KeySelectList { get; set; }
        public SelectList Ct3DeniedCt6KeySelectList { get; set; }

        public SelectList Ct3Ct8KeySelectList { get; set; }

        public SelectList Ct3DependentCt6KeySelectList { get; set; }

        public SelectList Ct3StartCr2Ct6KeySelectList { get; set; }

        public SelectList Ct3MetCr2Ct6KeySelectList { get; set; }

        public SelectList Ct3CetKeySelectList { get; set; }

        public SelectList Ct3Cu6KeySelectList { get; set; }

        public SelectList Ct3QryKeySelectList { get; set; }
        public SelectList Ct3ExmKeySelectList { get; set; }
        public SelectList Ct3DetKeySelectList { get; set; }
        public SelectList Ct3DtpKeySelectList { get; set; }

        public CredentialRequirementTypeViewModel(ApplicationDbContext context, CredentialRequirementType credentialRequirementType = null)
        {
            _context = context;
            CredentialRequirementType = credentialRequirementType ?? new CredentialRequirementType();

            Ct3CompleteCt6KeySelectList = new SelectList(_context.CredentialStatusTypes.AsNoTracking(), "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3CompleteCt6Key);
            Ct3MetCt6KeySelectList =  new SelectList(_context.CredentialStatusTypes.AsNoTracking(), "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3MetCt6Key);
            Ct3StartCt6KeySelectList =  new SelectList(_context.CredentialStatusTypes.AsNoTracking(), "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3StartCt6Key);
            Ct3SubmitCt6KeySelectList =  new SelectList(_context.CredentialStatusTypes.AsNoTracking(), "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3SubmitCt6Key);
            Ct3DeniedCt6KeySelectList = new SelectList(_context.CredentialStatusTypes.AsNoTracking(), "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3DeniedCt6Key);
            Ct3Ct8KeySelectList =  new SelectList(_context.CredentialRequirementTemplates.AsNoTracking(), "Ct8Key", "Ct8Template", CredentialRequirementType.Ct3Ct8Key);
            Ct3DependentCt6KeySelectList =  new SelectList(_context.CredentialStatusTypes.AsNoTracking(), "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3DependentCt6Key);
            Ct3StartCr2Ct6KeySelectList =  new SelectList(_context.CredentialStatusTypes.AsNoTracking(), "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3StartCr2Ct6Key);
            Ct3MetCr2Ct6KeySelectList =  new SelectList(_context.CredentialStatusTypes.AsNoTracking(), "Ct6Key", "Ct6Code", CredentialRequirementType.Ct3MetCr2Ct6Key);

            Ct3CetKeySelectList = new SelectList(_context.CEType.AsNoTracking(), "CetKey", "CetCode", CredentialRequirementType.Ct3CetKey);
            Ct3Cu6KeySelectList = new SelectList(_context.StatusStateTypes.AsNoTracking(), "Cu6Key","Cu6Code");
            Ct3QryKeySelectList = new SelectList(_context.Queries.AsNoTracking(), "QryKey", "QryName", CredentialRequirementType.Ct3QryKey);
            Ct3ExmKeySelectList = new SelectList(_context.Exams.AsNoTracking().OrderBy(e => e.ExmName), "ExmKey","ExmName", CredentialRequirementType.Ct3ExmKey);
            Ct3DetKeySelectList = new SelectList(_context.DegreeTypes.AsNoTracking().OrderBy(e => e.DetCode), "DetKey", "DetCode", CredentialRequirementType.Ct3DetKey);
            Ct3DtpKeySelectList = new SelectList(_context.DocumentTypes.AsNoTracking().OrderBy(e => e.DtpCode), nameof(CredentialRequirementType.DocumentType.DtpKey), nameof(CredentialRequirementType.DocumentType.DtpCode), CredentialRequirementType.Ct3DtpKey);
        }
    }
}
