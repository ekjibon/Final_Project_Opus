using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.POS.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
namespace OPUSERP.Areas.OtherSales.Controllers
{
    [Authorize]
    [Area("OtherSales")]
    public class ItemPriceFixingController : Controller
    {
        private readonly IItemPriceFixingService itemPriceFixingService;
        private readonly ICustomerService customerService;
        //private readonly IPurchaseService purchaseService;
        private readonly IERPCompanyService companyService;
        private readonly string rootPath;
        private readonly barcodecs barcodecs;
        private readonly MyPDF myPDF;

        public ItemPriceFixingController(IHostingEnvironment hostingEnvironment, IERPCompanyService companyService, IItemPriceFixingService itemPriceFixingService, ICustomerService customerService,  IConverter converter)
        {
            this.itemPriceFixingService = itemPriceFixingService;
            this.customerService = customerService;
            this.companyService = companyService;
            //this.purchaseService = purchaseService;
            barcodecs = new barcodecs(hostingEnvironment);
            myPDF = new MyPDF(hostingEnvironment, converter);
            this.rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: ItemPriceFixing
        public async Task<IActionResult> Index()
        {
            
            ItemPriceFixingViewModel model = new ItemPriceFixingViewModel
            {
                itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixingWithBarCode(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ItemPriceFixingViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {                
                return View(model);
            }

            await itemPriceFixingService.UpdateLastItemPriceFixing(Convert.ToInt32(model.itemSpecificationId));

            string barcode = model.itemSpecificationId+"-"+barcodecs.generateBarcode();
            ItemPriceFixing data = new ItemPriceFixing
            {
                Id = 0,
                itemSpecificationId = model.itemSpecificationId,
                price = model.price,
                discountPersent = model.discount,
                VAT = model.vat,
                barCode = barcode,
                barCodeImage = barcodecs.getBarcodeImage(barcode, string.Empty),
                isActive = 1
            };
            await itemPriceFixingService.SaveItemPriceFixing(data);
            
            return RedirectToAction(nameof(Index));
        }


        #region API Section

        [Route("global/api/getAllActiveItemFromItemPrice/")]
        [HttpGet]
        public async Task<IActionResult> GetAllActiveItemFromItemPrice()
        {
            return Json(await itemPriceFixingService.GetAllActiveItemFromItemPrice());
        }

        [Route("global/api/getAllPriceFixedItemSpacification")]
        [HttpGet]
        public async Task<IActionResult> GetAllPriceFixedItemSpacification()
        {
            return Json(await itemPriceFixingService.GetAllItemSpacification());
        }

        [Route("global/api/getAllPriceFixedItemSpacificationByItemId/{itemId}")]
        [HttpGet]
        public async Task<IActionResult> GetAllPriceFixedItemSpacificationByItemId(int itemId)
        {
            var company = await companyService.GetAllCompany();
            var compName = company.FirstOrDefault().companyName;
            if (compName == "Priyojon Healthcare Ltd")
            {
                return Json(await itemPriceFixingService.GetAllItemSpacificationByItemIdForPriyo(itemId));
            }
            else
            {
                return Json(await itemPriceFixingService.GetAllItemSpacificationByItemId(itemId));
            }
            //return Json(await itemPriceFixingService.GetAllPriceFixedItemSpacificationByItemId(itemId));
            //return Json(await itemPriceFixingService.GetAllItemSpacificationByItemId(itemId));
        }

        //[Route("global/api/GetAllPriceFixedItemSpacByItemId/{itemId}")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllPriceFixedItemSpacByItemId(int itemId)
        //{
        //    var result = await itemPriceFixingService.GetAllItemSpacificationByItemId(itemId);
        //    return Json(result.FirstOrDefault());
        //}

        [Route("global/api/GetItemPriceFixingBySpecId/{itemId}")]
        [HttpGet]
        public async Task<IActionResult> GetItemPriceFixingBySpecId(int itemId)
        {
            return Json(await itemPriceFixingService.GetItemPriceFixingBySpecId(itemId));
        }

        [Route("global/api/getAllPriceFixedItemSpacificationByItemOffId/{itemId}")]
        [HttpGet]
        public async Task<IActionResult> GetAllPriceFixedItemSpacificationByItemOffId(int itemId)
        {
            return Json(await itemPriceFixingService.GetAllPriceFixedItemSpacificationByItemOffId(itemId));
        }

        [Route("global/api/getAllORelSupplierCustomerResourseByRoleId/")]
        [HttpGet]
        public async Task<IActionResult> GetAllRelSupplierCustomerResourseByRoleId()
        {
            var roledata =await customerService.GetAllRoleType();

            return Json(await customerService.GetRelSupplierCustomerResourseByRoleId(roledata.Where(x => x.name == "Leads").Select(x => x.Id).FirstOrDefault()));
            //return Json(await customerService.GetAllRelSupplierCustomerResourseByRoleId(roledata.Where(x => x.name == "Leads").Select(x => x.Id).FirstOrDefault()));
        }

        [Route("global/api/GetCustomerInfoByRoleId/")]
        [HttpGet]
        public async Task<IActionResult> GetCustomerInfoByRoleId()
        {
            var roledata = await customerService.GetAllRoleType();
            return Json(await customerService.GetCustomerInfoByRoleId(0));
        }

        [Route("global/api/GetAllRelSupplierCustomerResourseForSales/")]
        [HttpGet]
        public async Task<IActionResult> GetAllRelSupplierCustomerResourse()
        {
            return Json(await customerService.GetAllRelSupplierCustomerResourse());
        }

        [Route("global/api/GetAllRelSupplierCustomerResourseBySupplierRoleId/")]
        [HttpGet]
        public async Task<IActionResult> GetAllRelSupplierCustomerResourseBySupplierRoleId()
        {
            return Json(await customerService.GetAllRelSupplierCustomerResourseByRoleId(1));
        }

        //[Route("global/api/GetratebyspecId/{Id}")]
        //[HttpGet]
        //public async Task<IActionResult> GetratebyspecId(int Id)
        //{
        //    return Json(await purchaseService.GetPurchaseOrderDetailBySpecId(Id));
        //}

        //[Route("api/itemPriceFixing/GetBoMPriceBySpecId/{Id}")]
        //[HttpGet]
        //public async Task<IActionResult> GetBoMPriceBySpecId(int Id)
        //{
        //    return Json(await purchaseService.GetPurchasePriceFromBoMBySpecId(Id));
        //}

        //[Route("api/itemPriceFixing/GetPurchaseProfitFromBoMBySpecId/{Id}")]
        //[HttpGet]
        //public async Task<IActionResult> GetPurchaseProfitFromBoMBySpecId(int Id)
        //{
        //    return Json(await purchaseService.GetPurchaseProfitFromBoMBySpecId(Id));
        //}

        [Route("global/api/GetItemDetailsInfoByBarCode/{barCode}")]
        [HttpGet]
        public async Task<IActionResult> GetItemDetailsInfoByBarCode(string barCode)
        {
            return Json(await itemPriceFixingService.GetActiveItemPriceFixingWithBarCodeNo(barCode));
        }

        #endregion

        #region Report
        [AllowAnonymous]
        public IActionResult BarCodePrintOption(int itemPriceId, int printCount)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/Sales/ItemPriceFixing/BarCodePrintOptionPreview?itemPriceId=" + itemPriceId + "&printCount=" + printCount;
            
            string fileName;
            string status = myPDF.GeneratePOSPDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<ActionResult> BarCodePrintOptionPreview(int itemPriceId,int printCount)
        {

            string userName = User.Identity.Name;
            ItemPriceFixing itemPrice = await itemPriceFixingService.GetActiveItemPriceFixingWithBarCodeById(itemPriceId);
            List<ItemPriceFixing> lstItemPrice = new List<ItemPriceFixing>();
            ViewBag.CodeInfo = itemPrice.barCode;
            for (int i = 1; i <= printCount; i++)
            {
                lstItemPrice.Add(itemPrice);
            }
            ItemPriceFixingViewModel model = new ItemPriceFixingViewModel
            {
                itemPriceFixings = lstItemPrice
            };
            
            return View(model);
        }

      

        #endregion

        //xxxxxxxxxxxxxx
    }
}