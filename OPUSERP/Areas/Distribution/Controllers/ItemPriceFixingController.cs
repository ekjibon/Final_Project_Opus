using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Distribution.Models;
//using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.Helpers;
//using OPUSERP.OtherSales.Data.Entity.Sales;
//using OPUSERP.OtherSales.Services.Sales.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
namespace OPUSERP.Areas.Distribution.Controllers
{
    [Authorize]
    [Area("Distribution")]
    public class ItemPriceFixingController : Controller
    {
        private readonly IDisItemPriceFixingService itemPriceFixingService;
        private readonly IDistributorTypeService distributorTypeService;
        //private readonly ICustomerService customerService;
        //private readonly IPurchaseService purchaseService;
        private readonly string rootPath;
        private readonly barcodecs barcodecs;
        private readonly MyPDF myPDF;

        public ItemPriceFixingController(IHostingEnvironment hostingEnvironment, IDistributorTypeService distributorTypeService, IDisItemPriceFixingService itemPriceFixingService,   IConverter converter)
        {
            this.itemPriceFixingService = itemPriceFixingService;
            this.distributorTypeService = distributorTypeService;
           // this.customerService = customerService;
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
                itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                distributorTypes=await distributorTypeService.GetAllDistributorType()
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
            DisItemPriceFixing data = new DisItemPriceFixing
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
          int priceid=  await itemPriceFixingService.SaveDisItemPriceFixing(data);
            if(model.distributortypeIdall.Length>0)
            {
                await itemPriceFixingService.DeletePriceDetailByMasterId(priceid);
                for (int i = 0; i < model.distributortypeIdall.Length; i++)
                {
                    DisItemPriceFixingDetails data1 = new DisItemPriceFixingDetails
                    {
                        Id = 0,
                        distributorTypeId = model.distributortypeIdall[i],
                        price = model.priceall[i],
                        VAT = model.vatall[i],
                        discountPersent=model.discountall[i],
                        disItemPriceFixingId=priceid

                    };
                    await itemPriceFixingService.SavePriceDetail(data1);
                }

            }
           

            return RedirectToAction(nameof(Index));
        }


        #region API Section
        //[Route("global/api/GetItemForPrice")]
        //[AllowAnonymous]
        //[HttpGet]
        //public async Task<IActionResult> GetItemForRequisition()
        //{
        //    var Re_DetailList = await itemsService.GetAllItemForRequisition();
        //    return Json(Re_DetailList);
        //}
        [AllowAnonymous]
        [Route("global/api/getdetailprice/{Id}")]
        [HttpGet]
        public async Task<IActionResult> getdetailprice(int Id)
        {
            IEnumerable<ItemPriceFixingDetailViewModel> model = await itemPriceFixingService.ItemPriceFixingDetailViewModels(Id);
            return Json(model);
        }

        [Route("global/api/getDisAllActiveItemFromItemPrice/")]
        [HttpGet]
        public async Task<IActionResult> GetDisAllActiveItemFromItemPrice()
        {
            return Json(await itemPriceFixingService.GetAllActiveItemFromItemPrice());
        }

        [Route("global/api/getDisAllPriceFixedItemSpacificationByItemId/{itemId}")]
        [HttpGet]
        public async Task<IActionResult> GetDisAllPriceFixedItemSpacificationByItemId(int itemId)
        {
            return Json(await itemPriceFixingService.GetAllPriceFixedItemSpacificationByItemId(itemId));
        }
        [Route("global/api/getDisAllPriceFixedItemSpacificationByItemTypeId/{itemId}/{distype}")]
        [HttpGet]
        public async Task<IActionResult> getDisAllPriceFixedItemSpacificationByItemTypeId(int itemId,int distype)
        {
            return Json(await itemPriceFixingService.GetAllPriceFixedItemSpacificationByItemtypeId(itemId,distype));
        }

        //[Route("global/api/getAllPriceFixedItemSpacificationByItemOffId/{itemId}")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllPriceFixedItemSpacificationByItemOffId(int itemId)
        //{
        //    return Json(await itemPriceFixingService.GetAllPriceFixedItemSpacificationByItemOffId(itemId));
        //}





        [Route("global/api/GetDisItemDetailsInfoByBarCode/{barCode}")]
        [HttpGet]
        public async Task<IActionResult> GetDisItemDetailsInfoByBarCode(string barCode)
        {
            return Json(await itemPriceFixingService.GetActiveItemPriceFixingWithBarCodeNo(barCode));
        }

        #endregion

        #region Report
        //[AllowAnonymous]
        //public IActionResult BarCodePrintOption(int itemPriceId, int printCount)
        //{
        //    string scheme = Request.Scheme;
        //    var host = Request.Host;

        //    string url = scheme + "://" + host + "/Sales/ItemPriceFixing/BarCodePrintOptionPreview?itemPriceId=" + itemPriceId + "&printCount=" + printCount;
            
        //    string fileName;
        //    string status = myPDF.GeneratePOSPDF(out fileName, url);

        //    if (status != "done")
        //    {
        //        return Content("<h1>Something Went Wrong</h1>");
        //    }

        //    var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
        //    return new FileStreamResult(stream, "application/pdf");
        //}

        //[AllowAnonymous]
        //public async Task<ActionResult> BarCodePrintOptionPreview(int itemPriceId,int printCount)
        //{

        //    string userName = User.Identity.Name;
        //    ItemPriceFixing itemPrice = await itemPriceFixingService.GetActiveItemPriceFixingWithBarCodeById(itemPriceId);
        //    List<ItemPriceFixing> lstItemPrice = new List<ItemPriceFixing>();
        //    ViewBag.CodeInfo = itemPrice.barCode;
        //    for (int i = 1; i <= printCount; i++)
        //    {
        //        lstItemPrice.Add(itemPrice);
        //    }
        //    ItemPriceFixingViewModel model = new ItemPriceFixingViewModel
        //    {
        //        itemPriceFixings = lstItemPrice
        //    };
            
        //    return View(model);
        //}

      

        #endregion

        //xxxxxxxxxxxxxx
    }
}