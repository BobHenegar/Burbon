using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectMoonshine.Controllers;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Utils;

namespace ProjectMoonshine.Helpers
{
    public class ControllerUtils
    {
        public static async Task<IActionResult> RedirectFromRequirementDataAsync(Controller controller,
            ApplicationDbContext context)
        {
            if (!controller.Request.HasFormContentType) return controller.RedirectToAction("Index");

            Guid requirementKey = Guid.Empty;
            if (controller.Request.Form["source"].ToString().Equals("Requirement") 
                && !string.IsNullOrWhiteSpace(controller.Request.Form["requirementKey"].ToString()))
            {
                Guid.TryParse(controller.Request.Form["requirementKey"].ToString(), out requirementKey);
            }

            else if (!controller.Request.Form["source"].ToString().Equals("Person"))
                return controller.RedirectToAction("Index");

            Guid customerKey = Guid.Empty;
            if (!string.IsNullOrWhiteSpace(controller.Request.Form["customerKey"].ToString()))
            {
                Guid.TryParse(controller.Request.Form["customerKey"].ToString(), out customerKey);
            }

            return await RedirectToRequirementCustomerOrDefault(controller, context,
                requirementKey != Guid.Empty ? (Guid?) requirementKey : null,
                customerKey != Guid.Empty ? (Guid?) customerKey : null);

        }

        public static async Task SetRequirementDataCustomerDropdownAndRedirectFieldsAsync(Controller controller, ApplicationDbContext context, string customerFieldName, Guid? requirementKey = null, Guid? customerKey = null, Guid? selectedValue = null)
        {
            if (controller.Request.HasFormContentType)
            {
                if (customerKey.IsGuidValid())
                {
                    if (Guid.TryParse(controller.Request.Form["customerKey"].ToString(), out Guid customerKeyParsed))
                        customerKey = customerKeyParsed;
                }

                if (requirementKey.IsGuidValid())
                {
                    if (Guid.TryParse(controller.Request.Form["requirementKey"].ToString(),
                        out Guid requirementKeyParsed))
                        requirementKey = requirementKeyParsed;
                }
            }
            
            if (selectedValue.IsGuidValid() )
            {
                controller.ViewData[customerFieldName] = new SelectList(await context.People
                                .Select(s => new { s.PerCusKey, Text = s.PerLastName + ", " + s.PerFirstName }).ToListAsync(), "PerCusKey", "Text", selectedValue);
            }

            else
            {
                controller.ViewData[customerFieldName] = new SelectList(await context.People
                    .Select(s => new { s.PerCusKey, Text = s.PerLastName + ", " + s.PerFirstName }).ToListAsync(), "PerCusKey", "Text");
            }
            

            if (requirementKey.IsGuidValid()) controller.ViewData["requirementKey"] = requirementKey;
            if (!customerKey.IsGuidValid()) return;

            controller.ViewData["customerKey"] = customerKey;
            if (selectedValue.IsGuidValid()) return;

            SelectListItem selected =
                ((SelectList) controller.ViewData[customerFieldName])?.FirstOrDefault(x =>
                    x.Value == customerKey.ToString());
            if (selected != null) selected.Selected = true;

        }

        public static async Task<IActionResult> RedirectToRequirementCustomerOrDefault(Controller controller, ApplicationDbContext context, Guid? requirementKey = null, Guid? customerKey = null)
        {
            if (requirementKey.IsGuidValid()) return controller.RedirectToAction("Edit", "CredentialRequirements", new { id = requirementKey
            });
            if (!customerKey.IsGuidValid()) return controller.RedirectToAction("Index");
            Guid? personKey = await context.People
                .AsNoTracking()
                .Where(p => p.PerCusKey == customerKey)
                .Select(p => p.PerKey)
                .FirstOrDefaultAsync();
            return personKey.IsGuidValid()? controller.RedirectToAction("Details", "People", new { id = personKey}) : controller.RedirectToAction("Index");
        }

        //public static async Task SetRequirementRedirectFieldsAsync(Controller controller, ApplicationDbContext context, string customerFieldName, Guid? processKey = null, Guid? customerKey = null, object selectedValue = null)
        //{

        //    //if (customerKey == null)
        //    //{
        //    //    if (Guid.TryParse(controller.Request.Form["customerKey"].ToString(), out Guid customerKeyParsed)) customerKey = customerKeyParsed;
        //    //}

        //    if (requirementKey == null)
        //    {
        //        if (Guid.TryParse(controller.Request.Form["processKey"].ToString(), out Guid requirementKeyParsed))
        //            requirementKey = requirementKeyParsed;
        //    }
        //    //if (selectedValue != null)
        //    //{
        //    //    controller.ViewData[customerFieldName] = new SelectList(await context.People
        //    //        .Select(s => new { s.PerCusKey, Text = s.PerLastName + ", " + s.PerFirstName }).ToListAsync(), "PerCusKey", "Text", selectedValue);
        //    //}

        //    //else
        //    //{
        //    //    controller.ViewData[customerFieldName] = new SelectList(await context.People
        //    //        .Select(s => new { s.PerCusKey, Text = s.PerLastName + ", " + s.PerFirstName }).ToListAsync(), "PerCusKey", "Text");
        //    //}


        //    if (processKey.IsGuidValid()) controller.ViewData["processKey"] = requirementKey;
        //    if (!customerKey.IsGuidValid()) return;

        //    controller.ViewData["customerKey"] = customerKey;
        //    if (selectedValue != null) return;

        //    SelectListItem selected =
        //        ((SelectList)controller.ViewData[customerFieldName])?.FirstOrDefault(x =>
        //            x.Value == customerKey.ToString());
        //    if (selected != null) selected.Selected = true;

        //}
    }
}
