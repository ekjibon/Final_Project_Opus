using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Distribution.Models;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.Distribution.Controllers
{
    [Authorize]
    [Area("Distribution")]
    public class SalesLevelController : Controller
    {
        // GET: /<controller>/
        private readonly ISalesLevelService salesLevelService;
        public SalesLevelController(IHostingEnvironment hostingEnvironment, ISalesLevelService salesLevelService)
        {
            
            this.salesLevelService = salesLevelService;
        }
        public async Task<IActionResult> Index()
        {
            SalesLevelViewModel model = new SalesLevelViewModel
            {
               
               
                salesLevels = await salesLevelService.GetAllSalesLevel()
            };
            return View(model);
        }
        // POST: Degree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SalesLevelViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                
                model.salesLevels = await salesLevelService.GetAllSalesLevel();
                return View(model);
            }

            SalesLevel data = new SalesLevel
            {
                Id = (int)model.Id,
                name = model.name,
                code = model.code,
                salesLevelId = model.salesLevelId
               
            };

            await salesLevelService.SaveSalesLevel(data);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await salesLevelService.DeleteSalesLevelById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
