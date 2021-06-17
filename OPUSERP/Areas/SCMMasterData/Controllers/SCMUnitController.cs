using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.SCMMasterData.Controllers
{
    [Area("SCMMasterData")]
    public class SCMUnitController : Controller
    {
        private readonly IItemsService ItemsService;

        public SCMUnitController(IItemsService ItemsService)
        {
            this.ItemsService = ItemsService;
        }

        #region Unit

        public async Task<IActionResult>  Index()
        {
            UnitViewModel model = new UnitViewModel
            { 
                units = await ItemsService.GetAllUnit()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] UnitViewModel model)
        {
            Unit data = new Unit
            {
                Id = Convert.ToInt32(model.unitId),
                unitName = model.unitName,
                description = model.description
            };
            await ItemsService.SaveUnit(data);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteUnitById(int Id)
        {
            await ItemsService.DeleteUnitById(Id);
            return Json(true);
        }

        #endregion

        #region Buyer Item Mapping

        public async Task<IActionResult> BuyerItem()
        {
            BuyerItemMappingViewModel model = new BuyerItemMappingViewModel
            {
                itemCategories = await ItemsService.GetAllItemCategory(),
                buyerItemMappings = await ItemsService.GetAllBuyerItemMapping()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyerItem([FromForm] BuyerItemMappingViewModel model)
        {
            BuyerItemMapping data = new BuyerItemMapping
            {
                Id = model.mappingId,
                itemCategoryId = model.itemCategoryId,
                employeeInfoId = model.employeeInfoId
            };
            await ItemsService.SaveBuyerItemMapping(data);
            return RedirectToAction(nameof(BuyerItem));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteBuyerItemMappingById(int Id)
        {
            await ItemsService.DeleteBuyerItemMappingById(Id);
            return Json(true);
        }

        #endregion

    }
}