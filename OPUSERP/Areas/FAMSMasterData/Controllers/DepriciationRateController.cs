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
    public class DepriciationRateController : Controller
    {
        private readonly IDepriciationRateService depriciationRateService;
        private readonly IAssetRegistrationService assetRegistrationService;
        public DepriciationRateController(IDepriciationRateService depriciationRateService, IAssetRegistrationService assetRegistrationService)
        {
            this.depriciationRateService = depriciationRateService;
            this.assetRegistrationService = assetRegistrationService;
        }

        public async Task<IActionResult> Index()
        {
            DepriciationRateViewModel model = new DepriciationRateViewModel()
            {
                depriciationRates = await depriciationRateService.GetAllDepriciationRate(),
                //itemCategories = await depriciationRateService.GetItemCategoryForDepRate()
            };
            return View(model);
        }       
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] DepriciationRateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.depriciationRates = await depriciationRateService.GetAllDepriciationRate();
                return View(model);
            }           

            DepriciationRate data = new DepriciationRate
            {
                Id = model.depriciationRateId,
                depreciationName = model.depreciationName,
                rate=model.rate,
                usefulLife = model.usefulLife,
                formulaType = model.formulaType
            };

            await depriciationRateService.SaveDepriciationRate(data);

            return RedirectToAction(nameof(Index));
        }      
               
        [HttpPost]
        public async Task<JsonResult> DeleteDepriciationRateById(int Id)
        {
            await depriciationRateService.DeleteDepriciationRateById(Id);
            return Json(true);
        }


    }
}