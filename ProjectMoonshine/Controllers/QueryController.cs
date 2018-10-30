using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ProjectMoonshine.EntityFramework;
using ProjectMoonshine.EntityFramework.Helpers;
using ProjectMoonshine.EntityFramework.Models;
using ProjectMoonshine.Enums;
using ProjectMoonshine.Managers;
using ProjectMoonshine.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjectMoonshine.Controllers
{
    public class QueryController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly QueryManager _queryManager;
        public QueryController(ApplicationDbContext context)
        {
            _queryManager = new QueryManager(context);
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _queryManager.GetAllQueriesAsync());
        }

        // GET: /Query
        [HttpGet]
        [Route("Query/Create")]
        public IActionResult Create()
        {
            var model = _queryManager.BuildQueryViewModel();
            ViewData["FromTable"] = model.TableDetails.Select(s => new SelectListItem(s.Key, s.Key)).ToList();
            var objects = model.TableDetails.SelectMany(s => s.Value.Select(sv => new { name = sv.ColumnName, prefixs = sv.TablePrefix, type = sv.ColumnType, table = s.Key })).ToList();
            ViewData["EqualityComparisonsList"] = model.EqualityComparisonsList;
            ViewData["SelectList"] = JsonConvert.SerializeObject(objects);
            ViewData["SelectIncludes"] = JsonConvert.SerializeObject(objects);
            return View();
        }

        [HttpPost]
        [Route("Query/Create")]
        public async Task<IActionResult> Create(Query query)
        {
            try
            {
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
                {
                    body = await reader.ReadToEndAsync();
                }
                JToken json = JToken.Parse(body);

                query.QryName = json["queryName"].Value<string>();
                query.QryTable = json["baseType"].Value<string>();
                var parameter = _queryManager.GetExpressionType(json);
                var res = _queryManager.ParseQuery(json, parameter);
                _queryManager.AddQuery(body, query);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                return View();
            }
        }

        // GET: Query/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var query = await _queryManager.GetExpressionByKey(id.Value);
            if (query == null)
            {
                return NotFound();
            }

            return View(query);
        }

        // POST: Phones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            _queryManager.DeleteQueryById(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
