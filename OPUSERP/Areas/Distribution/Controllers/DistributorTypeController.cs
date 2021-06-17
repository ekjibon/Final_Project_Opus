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
    public class DistributorTypeController : Controller
    {
        // GET: /<controller>/
        private readonly IDistributorTypeService distributorTypeService;
        public DistributorTypeController(IHostingEnvironment hostingEnvironment, IDistributorTypeService distributorTypeService)
        {
            
            this.distributorTypeService = distributorTypeService;
        }
        public async Task<IActionResult> Index()
        {
            DistributorTypeViewModel model = new DistributorTypeViewModel
            {
               
               
                distributorTypes = await distributorTypeService.GetAllDistributorType()
            };
            return View(model);
        }
        // POST: Degree/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DistributorTypeViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                
                model.distributorTypes = await distributorTypeService.GetAllDistributorType();
                return View(model);
            }

            DistributorType data = new DistributorType
            {
                Id = (int)model.Id,
                name = model.name,
              
               
            };

            await distributorTypeService.SaveDistributorType(data);

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await distributorTypeService.DeleteDistributorTypeById(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
