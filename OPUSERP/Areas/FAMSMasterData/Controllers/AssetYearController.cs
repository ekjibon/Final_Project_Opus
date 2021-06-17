using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.FAMSMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using OPUSERP.FixedAsset.Services.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSMasterData.Controllers
{
    [Area("FAMSMasterData")]
    public class AssetYearController : Controller
    {
        private readonly IDepriciationPeriodService  depriciationPeriodService;

        public AssetYearController(IDepriciationPeriodService depriciationPeriodService)
        {
            this.depriciationPeriodService = depriciationPeriodService;          
        }

        public async Task<IActionResult> Index(int? Id)
        {
            AssetYearViewModel model = new AssetYearViewModel()
            {
                assetYears = await depriciationPeriodService.GetAllAssetYear(),
            };
            //if (Id > 0)
            //{
            //    ViewBag.messageShow = "Data saved successfully";
            //}
            ViewBag.masterId = Id;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AssetYearViewModel model)
        //[HttpPost]
        //public async Task<JsonResult> Index([FromForm] AssetYearViewModel model)
        {
            var yearId = 0;
            AssetYear data = new AssetYear
            {
                Id = model.assetYearId,
                AssetYearName = model.AssetYearName
            };

            yearId = await depriciationPeriodService.SaveAssetYear(data);

            //return Json(true);
            return RedirectToAction(nameof(Index), new { Id = yearId });
        }    

        
        [HttpPost]
        public async Task<JsonResult> DeleteAssetYearById(int Id)
        {
            await depriciationPeriodService.DeleteAssetYearById(Id);
            return Json(true);
        }


    }
}