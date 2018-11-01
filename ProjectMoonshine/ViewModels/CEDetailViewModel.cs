using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Models;
using SQLitePCL;

namespace ProjectMoonshine.ViewModels
{
    public class CEDetailViewModel
    {
        public CEDetail CeDetail { get; set; }

        private readonly ApplicationDbContext _context;

        public SelectList Ce1CetKeySelectList { get; set; }

        //public SelectList Ce1CusKeySelectList { get; set; }

        public CEDetailViewModel(ApplicationDbContext context, CEDetail ceDetail = null)
        {
            _context = context;
            CeDetail = ceDetail ?? new CEDetail();

            Ce1CetKeySelectList = new SelectList(_context.CEType, "CetKey", "CetCode", CeDetail.Ce1CetKey);
            //Ce1CusKeySelectList = new SelectList(_context
            //        .People
            //        .Select(s => new { s.PerCusKey, Text = s.PerLastName + ", " + s.PerFirstName })
            //    , "PerCusKey", "Text", Ce1CusKeySelectList);

        }
    }
}
