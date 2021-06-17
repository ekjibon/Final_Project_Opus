using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.Inventory;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.CarManagement.Interfaces;
using OPUSERP.VMS.Services.Inventory.interfaces;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class PartsIssueController : Controller
    {
        private readonly IPartsIssueService partsIssueService;
        private readonly IPurchasePartsService purchasePartsService;
        private readonly IVMSVehicleInfoService vehicleInfoService;
        private readonly ICarInfo carInfo;
        //private readonly LangGenerate<PartsIssueLN> _lang;

        public PartsIssueController(IHostingEnvironment hostingEnvironment, IPartsIssueService partsIssueService, IPurchasePartsService purchasePartsService, IVMSVehicleInfoService vehicleInfoService, ICarInfo carInfo)
        {
            this.partsIssueService = partsIssueService;
            this.purchasePartsService = purchasePartsService;
            this.vehicleInfoService = vehicleInfoService;
            this.carInfo = carInfo;
            //_lang = new LangGenerate<PartsIssueLN>(hostingEnvironment.ContentRootPath);
        }

        public async Task<IActionResult> Index(int purchasePartsId)
        {
            string userName = HttpContext.User.Identity.Name;
            var lstVehicle = await vehicleInfoService.GetVehicleInformation();
            if (lstVehicle == null)
            {
                lstVehicle = new List<VehicleInformation>();
            }

            var lstPurchasePartsDetails = purchasePartsService.GetPurchasePartsDetailsByMasterId(purchasePartsId);
            
            PartsIssueMasterViewModel model = new PartsIssueMasterViewModel
            {
                vehicleInformation = lstVehicle,
                purchasePartsDetails=lstPurchasePartsDetails,
                partsMaster=await purchasePartsService.GetPurchasePartsMasterById(purchasePartsId)
            };
            return View(model);
        }


        public async Task<IActionResult> PartsIssueList()
        {
            string userName = HttpContext.User.Identity.Name;
            var lstPurchasePartsMaster = await purchasePartsService.GetAllPurchasePartsMaster();
            if (lstPurchasePartsMaster == null)
            {
                lstPurchasePartsMaster = new List<PurchasePartsMaster>();
            }

            PurchasePartsMasterViewModel model = new PurchasePartsMasterViewModel
            {
                purchasePartsMasters = lstPurchasePartsMaster
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] PartsIssueMasterViewModel model)
        {
            try
            {
                string userName = User.Identity.Name;
                PartsIssueMaster partsIssueMaster = new PartsIssueMaster
                {
                    Id = Convert.ToInt32(model.partsIssueMasterId),
                    vehicleId = model.vehicleId,
                    issueNo = model.issueNo,
                    issueDate = model.issueDate,
                    purchasePartsId = model.purchasePartsMasterId,
                    quantity = Convert.ToDecimal(model.quantity),
                    createdBy = userName,
                };
                int masterId = await partsIssueService.SavePartsIssueMaster(partsIssueMaster);
                //if (model.purchasePartsMasterId > 0)
                //{
                //    await purchasePartsService.DeletePurchasePartsDetailsByMasterId(masterId);
                //}
                //string currentDate = DateTime.Now.ToString("yyyyMMdd");
                //string barcode = "PP/" + currentDate + masterId;
                //for (int i = 1; i <= model.quantity; i++)
                //{
                //    PurchasePartsDetail partsDetail = new PurchasePartsDetail
                //    {
                //        Id = 0,
                //        purchasePartsMasterId = masterId,
                //        quantity = 1,
                //        price = model.price / model.quantity,
                //        isUse = 0,
                //        barCode = barcode + i,
                //        createdBy = userName
                //    };
                //    await purchasePartsService.SavePurchasePartsDetails(partsDetail);
                //}

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult GetIssueNo(int id,string date)
        {
            int purchaseParts = purchasePartsService.GetPurchasePartsDetailsByMasterId(id).Count();
            string idate = Convert.ToDateTime(date).ToString("yyyyMMdd");
            string issueNo = idate + id + purchaseParts;
            return Json(issueNo);
        }
    }
}