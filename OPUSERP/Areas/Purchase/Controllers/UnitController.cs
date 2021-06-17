using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Purchase.Models;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.Purchase.Controllers
{
    [Authorize]
    [Area("Purchase")]
    public class UnitController : Controller
    {
        private readonly IItemsService itemsService;

        public UnitController(IItemsService itemsService)
        {
            this.itemsService = itemsService;
        }

        // GET: EventCategory
        public async Task<IActionResult> Index()
        {
            UnitViewModel model = new UnitViewModel
            {
                units = await itemsService.GetAllUnit(),
            };
            return View(model);
        }

        // POST: EventCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] UnitViewModel model)
        {
            var validation = await itemsService.GetUnitbyName(model.name);
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.units = await itemsService.GetAllUnit();
               
                return View(model);
            }
            if (validation!=null&&model.UnitId == 0)
            {
                model.units = await itemsService.GetAllUnit();
                if (validation.Id != 0)
                {
                    ModelState.AddModelError(string.Empty, "Unit Name '" + model.name + "' is already exists.");
                }
                return View(model);
            }

            Unit data = new Unit
            {
                Id = model.UnitId,
                unitName = model.name,
            };

            await itemsService.SaveUnit(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await itemsService.DeleteUnitById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}