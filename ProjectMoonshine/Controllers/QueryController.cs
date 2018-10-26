using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using ProjectMoonshine.Enums;
using ProjectMoonshine.Managers;
using ProjectMoonshine.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectMoonshine.Controllers
{
    public class QueryController : Controller
    {
        private readonly QueryManager _queryManager;
        public QueryController(QueryManager queryManager)
        {
            _queryManager = queryManager;
        }


        [HttpGet]
        public IActionResult List()
        {
            return View();
        }

        // GET: /Query/Create
        public IActionResult Index()
        {
            var model = _queryManager.BuildQueryViewModel();
            ViewData["FromTable"] = model.TableDetails.Select(s => new SelectListItem(s.Key, s.Key)).ToList();
            var objects = model.TableDetails.SelectMany(s => s.Value.Select(sv => new { name = sv.ColumnName, prefixs = sv.TablePrefix, type = sv.ColumnType, table = s.Key })).ToList();

            ViewData["SelectList"] = JsonConvert.SerializeObject(objects);
            ViewData["SelectIncludes"] = JsonConvert.SerializeObject(objects);
            return View(model);
        }

        [HttpPost]
        [Route("Query/Create")]
        public async Task<IActionResult> Create()
        {
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
            }
            _queryManager.ParseQuery(body);
            return View();
        }
    }
}
