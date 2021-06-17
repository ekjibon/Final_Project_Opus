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
    public class AssetTransferController : Controller
    {
        private readonly IAssetAssignService assetAssignService;
        private readonly IAssetRegistrationService assetRegistrationService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IDesignationDepartmentService designationDepartmentService;

        public AssetTransferController(IAssetAssignService assetAssignService, IDesignationDepartmentService designationDepartmentService, IAssetRegistrationService assetRegistrationService, IPurchaseOrderService purchaseOrderService)
        {
            this.assetAssignService = assetAssignService;
            this.assetRegistrationService = assetRegistrationService;          
            this.purchaseOrderService = purchaseOrderService;
            this.designationDepartmentService = designationDepartmentService;
        }

        #region Asset Transfer List

        public async Task<IActionResult> AssetTransferList()
        {
            AssetTransferViewModel model = new AssetTransferViewModel
            {
                assetTransfers = await assetAssignService.GetAllAssetTransfer(),

            };
            return View(model);

        }
        #endregion

        #region Asset Assign

        public async Task<IActionResult> AssetTransfer(int? id, int? assetId)
        {
            ViewBag.TransferId = id != null ? (int)id : 0;
            ViewBag.AssetId = assetId;
            var assetRegistrations = await assetRegistrationService.GetAssetRegistrationById(Convert.ToInt32(assetId));
            AssetTransferViewModel model = new AssetTransferViewModel
            {
                deliveryLocations = await purchaseOrderService.GetDeliveryLocation(),
                employeeInfoViewModels = await assetAssignService.GetAllEmployeesList(),
            };
            ViewBag.PurchaseNo = assetRegistrations.purchaseInfo.purchaseNo;
            ViewBag.AssetNo = assetRegistrations.assetNo;
            ViewBag.ItemName = assetRegistrations.itemSpecification.Item.itemName;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetTransfer([FromForm] AssetTransferViewModel model)
        {
            var masterId = 0;

            AssetTransfer data = new AssetTransfer
            {
                Id =Convert.ToInt32(model.assetTransferId),
                assetRegistrationId = model.assetRegistrationId,
                transferDate = model.transferDate,
                currentEmpId = model.currentEmpId,
                currentCoordinatorId = model.currentCoordinatorId,               
                deliveryLocationId = model.deliveryLocationId
            };

            masterId = await assetAssignService.SaveAssetTransfer(data);            

            return RedirectToAction(nameof(AssetTransferList));
        }

        [HttpGet]
        public async Task<IActionResult> GetAssetTransferById(int id)
        {
            return Json(await assetAssignService.GetAssetTransferById(id));
        }


        #endregion

        #region Asset Not Transfer List

        public async Task<IActionResult> AssetNotTransferList()
        {
            AssetTransferViewModel model = new AssetTransferViewModel
            {
                assetRegistrations = await assetAssignService.GetNotTransferList(),
            };
            return View(model);
        }
        public async Task<IActionResult> AssetNotTransferListO()
        {
            AssetTransferViewModel model = new AssetTransferViewModel
            {
                assetRegistrations = await assetAssignService.GetNotTransferList(),
                deliveryLocations = await purchaseOrderService.GetDeliveryLocation(),
                employeeInfoViewModels = await assetAssignService.GetAllEmployeesList(),
                departments = await designationDepartmentService.GetDepartment(),
                assetTransfers = await assetAssignService.GetAllAssetTransfer(),
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetTransferO([FromForm] AssetTransferViewModel model)
        {
            var masterId = 0;

            AssetTransfer data = new AssetTransfer
            {
                Id = Convert.ToInt32(model.assetTransferId),
                assetRegistrationId = model.assetRegistrationId,
                transferDate = model.transferDate,
                currentEmpId = model.currentEmpId,
                currentCoordinatorId = model.currentCoordinatorId,
                deliveryLocationId = model.deliveryLocationId,
                departmentId=model.departmentId
            };

            masterId = await assetAssignService.SaveAssetTransfer(data);

            return RedirectToAction(nameof(AssetNotTransferListO));
        }
        #endregion
    }
}