using System;
using Microsoft.AspNetCore.Mvc;
using ProjectMoonshine.Helpers;

namespace ProjectMoonshine.Controllers
{
    public class BaseController : Controller
    {

        public void SetRefererInfo(string controllerName)
        {
            var refererHeader = Request.Headers[GlobalConstants.RefererHeader];
            if (!string.IsNullOrEmpty(refererHeader))
            {
                var localPath = new Uri(refererHeader).LocalPath;
                ViewBag.RefAction = localPath.GetActionFromUrl();
                ViewBag.RefController = localPath.GetControllerFromUrl();
                ViewBag.RefId = GlobalConstants.IsValidGuidRegex.GetRegexMatch(localPath);
            }
            else
            {
                ViewBag.RefController = controllerName;
                ViewBag.RefId = "";
                ViewBag.RefText = "List";
                ViewBag.RefAction = "Index";

            }
        }
    }

}
