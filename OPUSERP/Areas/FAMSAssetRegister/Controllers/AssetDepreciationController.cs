using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using OPUSERP.FixedAsset.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.FAMSAssetRegister.Controllers
{
    [Area("FAMSAssetRegister")]
    public class AssetDepreciationController : Controller
    {
      
        private readonly IDepriciationPeriodService depriciationPeriodService;
        private readonly IDepriciationRateService depriciationRateService;
        private readonly IAssetDepreciationService assetDepreciationService; 
        private readonly IAssetRegistrationService assetRegistrationService;

        public AssetDepreciationController(IDepriciationPeriodService depriciationPeriodService,IDepriciationRateService depriciationRateService, IAssetDepreciationService assetDepreciationService, IAssetRegistrationService assetRegistrationService)
        {
            this.depriciationPeriodService = depriciationPeriodService;
            this.depriciationRateService = depriciationRateService;
            this.assetDepreciationService = assetDepreciationService;
            this.assetRegistrationService = assetRegistrationService;
        }
        #region Asset Depreciation

        public async Task<IActionResult> AssetDepreciation(int periodId,int itemcatId)
        {
            AssetDepreciationViewModel model = new AssetDepreciationViewModel()
            {
                depriciationPeriods = await depriciationPeriodService.GetAllDepriciationPeriod(),
                depriciationRates=await depriciationRateService.GetAllDepriciationRate(),
                assetDepreciations=await assetDepreciationService.GetAssetDepreciationByPeriodId(periodId, itemcatId)

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetDepreciation([FromForm] AssetDepreciationViewModel model)
        {
            //IEnumerable<AssetRegistration> lstasset = await assetRegistrationService.GetAllAssetRegistrationbycatIdForDepreciation(Convert.ToInt32(model.itemCategoryId));
            //var Rate = await depriciationRateService.GetDepriciationRateByCatId(Convert.ToInt32(model.itemCategoryId));
            //var dep = await assetDepreciationService.GetAssetDepreciationByPeriodId(Convert.ToInt32(model.depriciationPeriodId), Convert.ToInt32(model.itemCategoryId));
            //if(dep.Count()>0)
            //{
            //    await assetDepreciationService.DeleteSAssetDepreciationByPeriodId(Convert.ToInt32(model.depriciationPeriodId), Convert.ToInt32(model.itemCategoryId));
            //}
            //foreach (AssetRegistration Assetdata in lstasset)
            //{
            //    AssetDepreciation data = new AssetDepreciation
            //    {
            //        Id = 0,
            //        assetRegistrationId = Assetdata.Id,
            //        depriciationPeriodId = model.depriciationPeriodId,
            //        depriciationRate = Rate.rate,
            //        depriciationValue = Assetdata.assetValue / Rate.rate
            //    };
            //    await assetDepreciationService.SaveAssetDepreciation(data);
            //}

            string userName = HttpContext.User.Identity.Name;
            await assetDepreciationService.ProcessAssetDepreciation(Convert.ToInt32(model.depriciationPeriodId), Convert.ToInt32(model.itemCategoryId), userName);

            return RedirectToAction("AssetDepreciation", "AssetDepreciation", new { periodId = model.depriciationPeriodId, itemcatId=model.itemCategoryId, Area = "FAMSAssetRegister" });
        }

        [HttpGet]
        public async Task<IActionResult> GetAssetDepreciationByPeriodId(int periodId,int itemcatId)
        {
            return Json( await assetDepreciationService.GetAssetDepreciationByPeriodId(periodId, itemcatId));
        }

        [HttpPost]
        public async Task<JsonResult> UnProcessAssetDepreciation(int depPeriodId, int depRateId)
        {
            await assetDepreciationService.UnProcessAssetDepreciation(depPeriodId, depRateId, HttpContext.User.Identity.Name);
            return Json(true);
        }

        #endregion


        #region API

        [HttpGet]
        [Route("/global/api/AssetDepreciationController/GetAllDepriciationPeriod/")]
        public async Task<JsonResult> GetAllDepriciationPeriod()
        {
            var result = await depriciationPeriodService.GetAllDepriciationPeriod();
            return Json(result);
        }

        [HttpGet]
        [Route("/global/api/AssetDepreciationController/GetAllDepriciationRate/")]
        public async Task<JsonResult> GetAllDepriciationRate()
        {
            var result = await depriciationRateService.GetAllDepriciationRate();
            return Json(result);
        }

        #endregion
    }
}