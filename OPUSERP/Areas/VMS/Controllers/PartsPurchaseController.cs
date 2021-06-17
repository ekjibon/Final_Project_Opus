using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.Inventory;
using OPUSERP.VMS.Services.CarManagement.Interfaces;
using OPUSERP.VMS.Services.Inventory.interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class PartsPurchaseController : Controller
    {
        private readonly IPurchasePartsService purchasePartsService;
        private readonly ICarInfo carInfo;
        //private readonly LangGenerate<PurchasePartsMasterLN> _lang;

        public PartsPurchaseController(IHostingEnvironment hostingEnvironment,IPurchasePartsService purchasePartsService, ICarInfo carInfo)
        {
            this.purchasePartsService = purchasePartsService;
            this.carInfo = carInfo;
            //_lang = new LangGenerate<PurchasePartsMasterLN>(hostingEnvironment.ContentRootPath);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var lstPurchasePartsMaster = await purchasePartsService.GetAllPurchasePartsMaster();
            if (lstPurchasePartsMaster == null)
            {
                lstPurchasePartsMaster = new List<PurchasePartsMaster>();
            }

            PurchasePartsMasterViewModel model = new PurchasePartsMasterViewModel
            {
                purchasePartsMasters= lstPurchasePartsMaster,
                spareParts=await carInfo.GetSpareParts()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] PurchasePartsMasterViewModel model)
        {
            try
            {
                string userName = User.Identity.Name;
                PurchasePartsMaster partsMaster = new PurchasePartsMaster
                {
                    Id = Convert.ToInt32(model.purchasePartsMasterId),
                    sparePartsId = model.sparePartsId,
                    purchaseDate = model.purchaseDate,
                    purchaseBy = model.purchaseBy,
                    quantity=model.quantity,
                    price=model.price,
                    challanNo=model.challanNo,
                    createdBy = userName,
                };
                int masterId = await purchasePartsService.SavePurchasePartsMaster(partsMaster);
                if (model.purchasePartsMasterId > 0)
                {
                    await purchasePartsService.DeletePurchasePartsDetailsByMasterId(masterId);
                }
                string currentDate = DateTime.Now.ToString("yyyyMMdd");
                string barcode = "PP/"+currentDate+masterId;
                for (int i = 1; i <= model.quantity; i++)
                {
                    PurchasePartsDetail partsDetail = new PurchasePartsDetail
                    {
                        Id=0,
                        purchasePartsMasterId=masterId,
                        quantity=1,
                        price=model.price/model.quantity,
                        isUse=0,
                        barCode= barcode+i,
                        createdBy=userName
                    };
                    await purchasePartsService.SavePurchasePartsDetails(partsDetail);
                }

                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        [Route("/api/purchase/partsDetailsList/{masterId}")]
        public JsonResult GetPurchasePartsDetailList(int masterId)
        {
            var result = purchasePartsService.GetPurchasePartsDetailsByMasterId(masterId);
            return Json(result);
        }
    }
}