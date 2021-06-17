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
    public class AssetReceiveController : Controller
    {
        private readonly IAssetAssignService assetAssignService;
        private readonly IAssetRegistrationService assetRegistrationService;        

        public AssetReceiveController(IAssetAssignService assetAssignService, IAssetRegistrationService assetRegistrationService)
        {
            this.assetAssignService = assetAssignService;
            this.assetRegistrationService = assetRegistrationService; 
        }

        #region Asset Not Receive List

        public async Task<IActionResult> AssetNotReceiveList()
        {
            AssetReceiveViewModel model = new AssetReceiveViewModel
            {
                assetRegistrations = await assetAssignService.GetNotAssetReceiveList(),
                assetReceives = await assetAssignService.GetAllAssetReceive(),
                assetRejects = await assetAssignService.GetAllAssetReject(),
            };
            return View(model);
        }
        #endregion


        #region Asset Receive List

        public async Task<IActionResult> AssetReceiveList()
        {
            AssetReceiveViewModel model = new AssetReceiveViewModel
            {
                assetReceives = await assetAssignService.GetAllAssetReceive(),

            };
            return View(model);

        }
        #endregion

        #region Asset Receive
        
        public async Task<IActionResult> SaveAssetReceive(int? id)
        {
            var masterId = 0;
            AssetReceive data = new AssetReceive
            {
                Id = 0,
                assetRegistrationId = id,
                receiveDate = DateTime.Now,
                receiveBy = HttpContext.User.Identity.Name
            };

            masterId = await assetAssignService.SaveAssetReceive(data);   

            return RedirectToAction(nameof(AssetNotReceiveList));
        }

        #endregion

        #region Asset Reject Save

        public async Task<IActionResult> SaveAssetReject(int? id)
        {
            var masterId = 0;
            AssetReject data = new AssetReject
            {
                Id = 0,
                assetRegistrationId = id,
                rejectDate = DateTime.Now,
                rejectBy = HttpContext.User.Identity.Name
            };

            masterId = await assetAssignService.SaveAssetReject(data);

            return RedirectToAction(nameof(AssetNotReceiveList));
        }

        #endregion

        #region Asset Reject List

        public async Task<IActionResult> AssetRejectList()
        {
            AssetReceiveViewModel model = new AssetReceiveViewModel
            {
                assetRejects = await assetAssignService.GetAllAssetReject(),
            };
            return View(model);

        }
        #endregion



    }
}