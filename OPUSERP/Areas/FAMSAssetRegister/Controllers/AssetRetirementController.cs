using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;

namespace OPUSERP.Areas.FAMSAssetRegister.Controllers
{
    [Area("FAMSAssetRegister")]
    public class AssetRetirementController : Controller
    {
        private readonly IAssetAssignService assetAssignService;
        private readonly IAssetRegistrationService assetRegistrationService;

        public AssetRetirementController(IAssetAssignService assetAssignService, IAssetRegistrationService assetRegistrationService)
        {
            this.assetAssignService = assetAssignService;
            this.assetRegistrationService = assetRegistrationService;
        }

        #region Asset Un-Assign List

        public async Task<IActionResult> AssetNonRetirementList()
        {
            AssetRetirementViewModel model = new AssetRetirementViewModel
            {
                assetRegistrations = await assetAssignService.GetNonRetirementAssetList(),

            };
            return View(model);

        }
        public async Task<IActionResult> AssetNonRetirementListO()
        {
            AssetRetirementViewModel model = new AssetRetirementViewModel
            {
                assetRegistrations = await assetAssignService.GetNonRetirementAssetList(),
                assetRetirementTypes = await assetAssignService.GetAllAssetRetirementType(),
                assetRetirements = await assetAssignService.GetAllAssetRetirement(),

            };
            return View(model);

        }
        #endregion


        #region Asset Retirement List

        public async Task<IActionResult> AssetRetirementList()
        {
            AssetRetirementViewModel model = new AssetRetirementViewModel
            {
                assetRetirements = await assetAssignService.GetAllAssetRetirement(),

            };
            return View(model);

        }

        [HttpPost]
        public async Task<JsonResult> DeleteAssetRetirementById(int Id)
        {
            await assetAssignService.DeleteAssetRetirementById(Id);
            return Json(true);
        }
        #endregion

        #region Asset Retirement

        public async Task<IActionResult> AssetRetirement(int? id, int? assetId)
        {
            ViewBag.RetirementId = id != null ? (int)id : 0;
            ViewBag.AssetId = assetId;
            var assetRegistrations = await assetRegistrationService.GetAssetRegistrationById(Convert.ToInt32(assetId));
            AssetRetirementViewModel model = new AssetRetirementViewModel
            {
                assetRetirementTypes = await assetAssignService.GetAllAssetRetirementType(),
            };
            
            ViewBag.AssetValue = assetRegistrations.assetValue;
            ViewBag.AssetNo = assetRegistrations.assetNo;
            ViewBag.ItemName = assetRegistrations.itemSpecification.Item.itemName;
            ViewBag.YearAccumulatedDep= await assetAssignService.GetAssetTotalDepreciationByAssetId(Convert.ToInt32(assetId));

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetRetirement([FromForm] AssetRetirementViewModel model)
        {
            var masterId = 0;

            AssetRetirement data = new AssetRetirement
            {
                Id =Convert.ToInt32(model.assetRetirementId),
                assetRegistrationId = model.assetRegistrationId,
                assetRetirementTypeId = model.assetRetirementTypeId,
                retirementDate = model.retirementDate,
                causeOfRetirement = model.causeOfRetirement,
                scrapValue = model.scrapValue
            };

            masterId = await assetAssignService.SaveAssetRetirement(data);            

            return RedirectToAction(nameof(AssetRetirementList));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetRetirementO([FromForm] AssetRetirementViewModel model)
        {
            var masterId = 0;

            AssetRetirement data = new AssetRetirement
            {
                Id = Convert.ToInt32(model.assetRetirementId),
                assetRegistrationId = model.assetRegistrationId,
                assetRetirementTypeId = model.assetRetirementTypeId,
                retirementDate = model.retirementDate,
                causeOfRetirement = model.causeOfRetirement,
                scrapValue = model.scrapValue
            };

            masterId = await assetAssignService.SaveAssetRetirement(data);

            return RedirectToAction(nameof(AssetNonRetirementListO));
        }

        [HttpGet]
        public async Task<IActionResult> GetAssetRetirementById(int id)
        {
            return Json(await assetAssignService.GetAssetRetirementById(id));
        }

        [HttpGet]
        [Route("/global/api/getitemdepinfo/{id}")]
        public async Task<JsonResult> getiteminfo(int id)
        {

            var result = await assetAssignService.GetAssetTotalDepreciationByAssetId(Convert.ToInt32(id));
            return Json(result);
        }


        #endregion


    }
}