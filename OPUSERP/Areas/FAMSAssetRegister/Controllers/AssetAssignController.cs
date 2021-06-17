using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;

namespace OPUSERP.Areas.FAMSAssetRegister.Controllers
{
    [Area("FAMSAssetRegister")]
    public class AssetAssignController : Controller
    {
        private readonly IAssetAssignService assetAssignService;
        private readonly IAssetRegistrationService assetRegistrationService;
        private readonly ERPServices.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService;
        private readonly IPurchaseOrderService purchaseOrderService;

        public AssetAssignController(IAssetAssignService assetAssignService, IAssetRegistrationService assetRegistrationService, ERPServices.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService, IPurchaseOrderService purchaseOrderService)
        {
            this.assetAssignService = assetAssignService;
            this.assetRegistrationService = assetRegistrationService;
            this.designationDepartmentService = designationDepartmentService;
            this.purchaseOrderService = purchaseOrderService;
        }

        #region Asset Assign List

        public async Task<IActionResult> AssetAssignList()
        {
            AssetAssignViewModel model = new AssetAssignViewModel
            {
                assetAssigns = await assetAssignService.GetAllAssetAssign(),

            };
            return View(model);

        }
        #endregion

        #region Asset Assign

        public async Task<IActionResult> AssetAssign(int? id, int? assetId)
        {
            ViewBag.AssignId = id != null ? (int)id : 0;
            ViewBag.AssetId = assetId;
            var assetRegistrations = await assetRegistrationService.GetAssetRegistrationById(Convert.ToInt32(assetId));
            AssetAssignViewModel model = new AssetAssignViewModel
            {
                departments= await designationDepartmentService.GetDepartment(),
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
        public async Task<IActionResult> AssetAssign([FromForm] AssetAssignViewModel model)
        {
            var masterId = 0;
            
            AssetAssign data = new AssetAssign
            {
                Id =Convert.ToInt32(model.assetAssignId),
                assetRegistrationId = model.assetRegistrationId,
                assignDate = model.assignDate,
                empId = model.empId,
                coordinatorempId = model.coordinatorempId,
                departmentId = model.departmentId,
                deliveryLocationId = model.deliveryLocationId,
                remarks = model.remarks
            };

            masterId = await assetAssignService.SaveAssetAssign(data);            

            return RedirectToAction(nameof(AssetAssignList));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssetAssignO([FromForm] AssetAssignViewModel model)
        {
            var masterId = 0;

            AssetAssign data = new AssetAssign
            {
                Id = Convert.ToInt32(model.assetAssignId),
                assetRegistrationId = model.assetRegistrationId,
                assignDate = model.assignDate,
                empId = model.empId,
                coordinatorempId = model.coordinatorempId,
                departmentId = model.departmentId,
                deliveryLocationId = model.deliveryLocationId,
                remarks = model.remarks
            };

            masterId = await assetAssignService.SaveAssetAssign(data);

            return RedirectToAction(nameof(AssetUnAssignedListO));
        }

        [HttpGet]
        public async Task<IActionResult> GetAssetAssignById(int id)
        {
            return Json(await assetAssignService.GetAssetAssignById(id));
        }


        #endregion
        

        #region Asset Un-Assign List

        public async Task<IActionResult> AssetUnAssignedList()
        {
            AssetAssignViewModel model = new AssetAssignViewModel
            {
                assetRegistrations = await assetAssignService.GetAllUnassignedList(),

            };
            return View(model);

        }
        public async Task<IActionResult> AssetUnAssignedListO()
        {
            AssetAssignViewModel model = new AssetAssignViewModel
            {
                assetRegistrations = await assetAssignService.GetAllUnassignedList(),
                departments = await designationDepartmentService.GetDepartment(),
                deliveryLocations = await purchaseOrderService.GetDeliveryLocation(),
                employeeInfoViewModels = await assetAssignService.GetAllEmployeesList(),
                assetAssigns = await assetAssignService.GetAllAssetAssign(),

            };
            return View(model);

        }

        [HttpGet]
        [Route("/global/api/getiteminfo/{id}")]
        public async Task<JsonResult> getiteminfo(int id)
        {

            var result = await assetRegistrationService.GetAssetRegistrationById((int)id);
            return Json(result);
        }
        #endregion
    }
}