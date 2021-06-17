using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.FAMSMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSMasterData.Controllers
{
    [Area("FAMSMasterData")]
    public class AssetRetirementTypeController : Controller
    {
        private readonly IAssetAssignService assetAssignService;

        public AssetRetirementTypeController(IAssetAssignService assetAssignService)
        {
            this.assetAssignService = assetAssignService;          
        }

        public async Task<IActionResult> Index()
        {
            AssetRetirementTypeViewModel model = new AssetRetirementTypeViewModel()
            {
                assetRetirementTypes = await assetAssignService.GetAllAssetRetirementType(),
            };
            return View(model);
        }       
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AssetRetirementTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.assetRetirementTypes = await assetAssignService.GetAllAssetRetirementType();
                return View(model);
            }

            AssetRetirementType data = new AssetRetirementType
            {
                Id = model.assetRetirementTypeId,
                assetRetirementTypeName = model.assetRetirementTypeName
            };

            await assetAssignService.SaveAssetRetirementType(data);

            return RedirectToAction(nameof(Index));
        }      

        public async Task<IActionResult> DeleteAssetRetirementTypeById(int id)
        {
            try
            {
                await assetAssignService.DeleteAssetRetirementTypeById(id);
                return RedirectToAction("Index", "AssetRetirementType", new { Area = "FAMSMasterData" });
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
                //return RedirectToAction("Index", "AssetRetirementType", new { Area = "FAMSMasterData" });
            }
           
        }

     

    }
}