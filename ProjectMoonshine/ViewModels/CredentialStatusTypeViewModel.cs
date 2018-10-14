using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;

namespace ProjectMoonshine.ViewModels
{
    public class CredentialStatusTypeViewModel
    {
        public CredentialStatusType CredentialStatusType { get; set; }
        public SelectList Ct6Cu6KeySelectList { get; set; }
        private readonly ApplicationDbContext _context;

        public CredentialStatusTypeViewModel(ApplicationDbContext context, CredentialStatusType credentialStatusType = null)
        {
            _context = context;
            CredentialStatusType = credentialStatusType ?? new CredentialStatusType();
            Ct6Cu6KeySelectList = new SelectList(_context.StatusStateTypes, "Cu6Key", "Cu6Code", CredentialStatusType.Ct6Cu6Key);
        }
    }
}
