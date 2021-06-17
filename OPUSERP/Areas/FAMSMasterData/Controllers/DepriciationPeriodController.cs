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
    public class DepriciationPeriodController : Controller
    {
        private readonly IDepriciationPeriodService  depriciationPeriodService;

        public DepriciationPeriodController(IDepriciationPeriodService depriciationPeriodService)
        {
            this.depriciationPeriodService = depriciationPeriodService;          
        }

        public async Task<IActionResult> Index()
        {
            DepriciationPeriodViewModel model = new DepriciationPeriodViewModel()
            {
                depriciationPeriods = await depriciationPeriodService.GetAllDepriciationPeriod(),
                assetYears=await depriciationPeriodService.GetAllAssetYear()
            };
            return View(model);
        }       
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DepriciationPeriodViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.depriciationPeriods = await depriciationPeriodService.GetAllDepriciationPeriod();
                return View(model);
            }

            DepriciationPeriod data = new DepriciationPeriod
            {
                Id = model.depriciationPeriodId,
                 PeriodName = model.PeriodName,
                MonthName = model.MonthName,
                YearID = model.YearID,
                DaysWorking = model.DaysWorking,
                SDate = model.SDate,
                EDate = model.EDate,
                islock = model.islock
            };

            await depriciationPeriodService.SaveDepriciationPeriod(data);

            return RedirectToAction(nameof(Index));
        }     
               

        [HttpPost]
        public async Task<JsonResult> DeleteDepriciationPeriodById(int Id)
        {
            await depriciationPeriodService.DeleteDepriciationPeriodById(Id);
            return Json(true);
        }

    }
}