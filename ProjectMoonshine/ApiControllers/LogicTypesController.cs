using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProjectMoonshine.EntityFramework.Classes.QueryClasses;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectMoonshine.ApiControllers
{
    [Route("api/[controller]")]
    public class LogicTypesController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public JsonResult Get()
        {
            var result = new List<KeyValuePair<string, int>>();
            foreach (var item in Enum.GetValues(typeof(LogicTypes)))
            {
                result.Add(new KeyValuePair<string, int>(Enum.GetName(typeof(LogicTypes), item), (int)(LogicTypes)item));
            }
            return Json(result);
        }
    }
}
