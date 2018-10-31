using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.ViewModels
{
    public class CredentialRequirementViewModel
    {
        public CredentialRequirement CredentialRequirement { get; set; }
        private readonly ApplicationDbContext _context;

        public SelectList Cr3Ct3KeySelectList { get; set; }

        public SelectList Cr3Ct6KeySelectList { get; set; }

        public SelectList Cr3Cr2KeySelectList { get; set; }

        public SelectList Cr3IxlKeySelectList { get; set; }
        public SelectList Cr3FupKeySelectList { get; set; }

        public SelectList Cr3DegKeySelectList { get; set; }

        public SelectList Cr3ExsSelectList { get; set; }


        public CredentialRequirementViewModel(ApplicationDbContext context, CredentialRequirement credentialRequirement = null)
        {
            _context = context;
            CredentialRequirement = credentialRequirement ?? new CredentialRequirement();

            BuildProcessSelectList();
            Cr3Ct3KeySelectList = new SelectList(_context.CredentialRequirementTypes, "Ct3Key", "Ct3Requirement", CredentialRequirement.Cr3Ct3Key);
            Cr3Ct6KeySelectList = new SelectList(_context.CredentialStatusTypes, "Ct6Key", "Ct6Code", CredentialRequirement.Cr3Ct6Key);


            if (!CommonUtils.IsGuidValid(CredentialRequirement.Cr3Cr2Key)) return;
            Guid customerKey =
                CredentialingUtils.GetCustomerKeyFromProcessKey(_context, CredentialRequirement.Cr3Cr2Key);

            if (!CommonUtils.IsGuidValid(customerKey)) return;

            BuildDegreeSelectList(customerKey);

            BuildLicenseSelectList(customerKey);

            BuildExamScoreSelectList(customerKey);

            BuildFileUploadSelectList(customerKey);

        }

        private void BuildProcessSelectList()
        {
            var processDropDownList = _context.CredentialProcesses
                            .Include(p => p.Credential)
                                .ThenInclude(c => c.Customer)
                                .ThenInclude(d => d.Person)
                            .Include(p => p.Credential)
                                .ThenInclude(c => c.CredentialType)
                            .Include(p => p.CredentialProcessType)
                            .AsNoTracking()
                            .Select(p => new { p.Cr2Key, Text = BuildProcessListString(p) });

            Cr3Cr2KeySelectList = new SelectList(processDropDownList, "Cr2Key", "Text", CredentialRequirement.Cr3Cr2Key);
        }

        private void BuildDegreeSelectList(Guid customerKey)
        {
            var degreeDropdownList = _context.Degrees
                                                    .Include(d => d.DegreeType)
                                                    .AsNoTracking()
                                                    .Where(d => d.DegCusKey == customerKey)
                                                    .OrderByDescending(d => d.DegDate)
                                                    .Select(d => new
                                                    {
                                                        d.DegKey,
                                                        Text = string.Format("{0} {1}, {2}", d.DegDate.Year, d.DegreeType.DetCode, d.DegInstitution)
                                                    });
            Cr3DegKeySelectList = new SelectList(degreeDropdownList, "DegKey", "Text", CredentialRequirement.Cr3DegKey);
        }

        private void BuildLicenseSelectList(Guid customerKey)
        {
            var licenseDropdownList = _context.Licenses
                                .AsNoTracking()
                                .Where(l => l.IxlCusKey == customerKey).OrderByDescending(i => i.IxlExpireDate)
                                .Select(l => new
                                {
                                    l.IxlKey,
                                    Text = string.Format("{0} - {1}, Exp {2}", l.IxlLicenseName, l.IxlLicenseNo,
                                        l.IxlExpireDate.Year)
                                });
            Cr3IxlKeySelectList = new SelectList(licenseDropdownList, "IxlKey", "Text", CredentialRequirement.Cr3IxlKey);
        }

        private void BuildFileUploadSelectList(Guid customerKey)
        {
            var fileUploadDropdownList = _context.FileUploads.Include(f => f.Person).Include(f => f.DocumentType)
                .AsNoTracking().Where(f => f.Person.PerCusKey == customerKey).OrderByDescending(f => f.FupAddDate)
                .Select(f => new
                {
                    f.FupKey,
                    Text = string.Format("{0}{1}"
                        , f.DocumentType != null ? f.DocumentType.DtpCode + " - " : ""
                        , f.FupFileName)
                });
            Cr3FupKeySelectList = new SelectList(fileUploadDropdownList, "FupKey", "Text", CredentialRequirement.Cr3FupKey);
        }

        private void BuildExamScoreSelectList(Guid customerKey)
        {
            var examScoreDropdownList = _context.ExamScores
                .AsNoTracking()
                .Include(s => s.Exam)
                .Where(s => s.ExsCusKey == customerKey)
                .OrderByDescending(s => s.ExsDate)
                .Select(s => new {s.ExsKey
                    , Text = string.Format("{0:d} - {1}" 
                        , s.ExsDate
                        , s.Exam.ExmName)});

            Cr3ExsSelectList = new SelectList(examScoreDropdownList, "ExsKey", "Text", CredentialRequirement.Cr3ExsKey);

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
