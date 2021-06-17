using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Helpers;
using OPUSERP.POS.Data.Entity;
using OPUSERP.POS.Services.POS.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.POS.Controllers
{
    [Authorize]
    [Area("POS")]
    public class OfferController : Controller
    {
        private readonly IItemPriceFixingService itemPriceFixingService;
        private readonly IOfferService offerService;
        private readonly IItemsService itemsService;

        private readonly string rootPath;
        private readonly barcodecs barcodecs;
        private readonly MyPDF myPDF;

        public OfferController(IHostingEnvironment hostingEnvironment, IItemsService itemsService, IItemPriceFixingService itemPriceFixingService,   IConverter converter, IOfferService offerService)
        {
            this.itemPriceFixingService = itemPriceFixingService;
            this.offerService = offerService;
            this.itemsService = itemsService;
            barcodecs = new barcodecs(hostingEnvironment);
            myPDF = new MyPDF(hostingEnvironment, converter);
            this.rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: Offer/Details/5
        public async Task<IActionResult> Index()
        {
            OfferViewModel model = new OfferViewModel
            {
                itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing(),
                offerMasters = await offerService.GetOfferMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OfferViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.itemPriceFixings = await itemPriceFixingService.GetActiveItemPriceFixing();
                return View(model);
            }

            if (model.offerId > 0)
            {
                await offerService.DeleteOfferDetailsByMasterId(model.offerId);
            }

            string barcode = "P-" + barcodecs.generateBarcode();

            OfferMaster data = new OfferMaster
            {
                Id = model.offerId,
                offerName = model.offerName,
                endDate = model.offerDate,
                price = model.price,
                discountPersent = model.discount,
                VAT = model.vat,
                barCode = barcode,
                barCodeImage = barcodecs.getBarcodeImage(barcode, string.Empty),
            };
            int id = await offerService.SaveOfferMaster(data);

            if (model.itemIdall != null)
            {
                for (int i = 0; i < model.itemIdall.Length; i++)
                {
                    OfferDetails offerDetails = new OfferDetails
                    {
                        offerMasterId = id,
                        quantity = model.qntall[i],
                        itemPriceFixingId = model.itemIdall[i]
                    };
                    await offerService.SaveOfferDetails(offerDetails);
                }
            }
            //Item
            string itemCode = "";
            itemCode = await itemsService.GetItemCode();
            int itemId = 0;
            if (model.offerId > 0)
            {

                var itemobj = await itemsService.GetItemByName(model.offerName);
                itemId = itemobj.Id;
                itemCode = itemobj.itemCode;
            }
            var itemcat = await itemsService.GetItemCategoryByName("Offer&Package");
            Item item = new Item
            {
                Id = itemId,
                parentId = itemcat.Id,
                unitId = 52,
                itemName = model.offerName,
                itemCode = itemCode,
                itemDescription = "Offer&Package",
                isDelete = 0,
                //  accountLedgerId = model.accountLedgerId,
                // reOrderLevel = model.reOrderLevel,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            int instId = await itemsService.SaveItem(item);
            int itemspecid = 0;
            var specdata = await itemsService.GetAllItemSpecificationByitemIdandspec(instId, model.offerName);
            if (specdata.Count() != 0)
            {
                itemspecid = specdata.FirstOrDefault().Id;
            }
            ItemSpecification itemspec = new ItemSpecification
            {
                Id = itemspecid,
                itemId = Convert.ToInt32(instId),
                specificationName = model.offerName,
                isDelete = 0,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };
            itemspecid = await itemsService.SaveItemSpecification(itemspec);

            await itemPriceFixingService.UpdateLastItemPriceFixing(Convert.ToInt32(itemspecid));
            ItemPriceFixing dataprice = new ItemPriceFixing
            {
                Id = 0,
                itemSpecificationId = itemspecid,
                price = model.price,
                discountPersent = model.discount,
                VAT = model.vat,
                barCode = barcode,
                barCodeImage = barcodecs.getBarcodeImage(barcode, string.Empty),
                isActive = 1
            };
            await itemPriceFixingService.SaveItemPriceFixing(dataprice);
            return RedirectToAction(nameof(Index));
        }

        #region Report
        [AllowAnonymous]
        public IActionResult BarCodePrintOption(int itemPriceId, int printCount)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/POS/Offer/BarCodePrintOptionPreview?itemPriceId=" + itemPriceId + "&printCount=" + printCount;

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
        public async Task<ActionResult> BarCodePrintOptionPreview(int itemPriceId, int printCount)
        {

            string userName = User.Identity.Name;
            OfferMaster offerMaster = await offerService.GetOfferMasterById(itemPriceId);
            List<OfferMaster> lstItemPrice = new List<OfferMaster>();
            ViewBag.CodeInfo = offerMaster.barCode;
            for (int i = 1; i <= printCount; i++)
            {
                lstItemPrice.Add(offerMaster);
            }
            OfferViewModel model = new OfferViewModel
            {
                offerMasters = lstItemPrice
            };

            return View(model);
        }
        #endregion


        #region API section
        [Route("global/api/GetOfferDetailsByMasterId/{id}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetOfferDetailsByMasterId(int id)
        {
            return Json(await offerService.GetOfferDetailsByMasterId(id));
        }
        #endregion
    }
}