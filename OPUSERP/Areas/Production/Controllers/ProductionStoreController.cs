using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Inventory.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Production.Services.Interfaces;
using OPUSERP.Purchase.Service.Interfaces;
using OPUSERP.SCM.Data.Entity.Stock;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Stock.Interface;

namespace OPUSERP.Areas.Production.Controllers
{
    [Authorize]
    [Area("Production")]
    public class ProductionStoreController : Controller
    {
        private readonly IProductionRequisitionService productionRequisitionService;
        private readonly IStoreService storeService;
        private readonly IItemsService itemsService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public string FileName;
        private readonly IStockService stockService;
        private readonly IUserInfoes userInfoes;

        public ProductionStoreController(IProductionRequisitionService productionRequisitionService, IStoreService storeService, IItemsService itemsService, IStockService stockService, IUserInfoes userInfoes)
        {
            this.productionRequisitionService = productionRequisitionService;
            this.storeService = storeService;
            this.itemsService = itemsService;
            this.stockService = stockService;
            this.userInfoes = userInfoes;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> ProdRawMatReceiveDetails(int id)
        {
            ViewBag.masterId = id;
            var store = await storeService.GetStoreByStoreName("Production");
            var sale = await stockService.GetStockMasterbyTypeForProd(1, store.Id);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "ProdRNO" + '-' + idate + '-' + (Cpurchase + 1);
            ViewBag.MRNO = issueNo;
            var model = new StockInViewModel
            {
                stockMasterById = await stockService.GetStockOutForProductionMasterById(id),
                GetStocksbyMasterId = await stockService.GetAllStockForProductionReqByMasterId(id),
            };
            if (model.stockMasterById.productionRequsitionId != null)
            {
                ViewBag.invoiceNumber = model.GetStocksbyMasterId.FirstOrDefault().productionRequsitionDetails.productionRequsitionMaster.requsitionNumber;
                ViewBag.invoiceDate = model.GetStocksbyMasterId.FirstOrDefault().productionRequsitionDetails.productionRequsitionMaster.date;
                ViewBag.nameEnglish = model.GetStocksbyMasterId.FirstOrDefault().productionRequsitionDetails.productionRequsitionMaster.createdBy;
                ViewBag.totalAmount = model.GetStocksbyMasterId.FirstOrDefault().productionRequsitionDetails.productionRequsitionMaster.requisitionQty;
            }
            else
            {
                //ViewBag.invoiceNumber = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.invoiceNumber;
                //ViewBag.invoiceDate = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.invoiceDate;
                //ViewBag.nameEnglish = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.posCustomer.name;
                //ViewBag.totalAmount = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.totalAmount;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ProdStockIn([FromForm] StockInViewModel model)
        {
            var store = await storeService.GetStoreByStoreName("Production");
            var PurchasedetailsId = 0;
            var sale = await stockService.GetStockMasterbyTypeForProd(1,store.Id);
            int Cpurchase = 0;
            Cpurchase = sale.Where(x => Convert.ToDateTime(x.StockDate).ToString("yyyyMMdd") == Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd")).Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string issueNo = "ProdRNO" + '-' + idate + '-' + (Cpurchase + 1);
            int masterId = 0;
            if (model.detailids != null)
            {
                StockMaster data = new StockMaster
                {
                    Id = Convert.ToInt32(model.stockMasterId),
                    //companyId = 1,
                    productionRequsitionId = model.productionId,
                    storeId= store.Id,
                    remarks = model.remarks,
                    MRNO = issueNo,//model.MRNO,
                    receiveNo=issueNo,
                    receiveDate=model.stockDate,
                    StockDate = model.stockDate,
                    stockStatusId = 1,
                    //storeId = 1

                };
                masterId = await stockService.SaveStockMaster(data);
                await stockService.DeleteStockMasterById(masterId);
                for (var i = 0; i < model.detailids.Length; i++)
                {
                    PurchasedetailsId = (int)model.detailids[i];
                    StockDetails data1 = new StockDetails
                    {
                        stockMasterId = masterId,
                        productionRequsitionDetailsId = model.detailids[i],
                        purchaseRate = model.rates[i],
                        rate = model.rates[i],
                        qty = model.quantitys[i],
                        itemSpecificationId = model.specids[i]
                    };
                    await stockService.SaveStock(data1);
                }
            }
            //await productionRequisitionService.UpdateProductionRequsitionMasterStockCloseById((int)purchasemasterId);
            return RedirectToAction("ReceivedGoodsForProduction", "ProductionStore", new { Area = "Production" });
        }

        public async Task<IActionResult> ReceivedGoodsForProduction()
        {
            var store = await storeService.GetStoreByStoreName("Production");
            StockInViewModel model = new StockInViewModel
            {
                stockMasters = await stockService.GetStockMasterbyTypeForProd(1, store.Id)

            };
            return View(model);
        }

        public async Task<IActionResult> ProdStockInDetails(int id)
        {
            try
            {
                ViewBag.masterId = id;
                var model = new StockInViewModel
                {
                    stockMasterById = await stockService.GetStockOutMasterById(id),
                    GetStocksbyMasterId = await stockService.GetAllStockForProductionReqByMasterId(id),
                };
                if (model.stockMasterById.productionRequsitionId != null)
                {
                    ViewBag.invoiceNumber = model.GetStocksbyMasterId.FirstOrDefault().productionRequsitionDetails.productionRequsitionMaster.requsitionNumber;
                    ViewBag.invoiceDate = model.GetStocksbyMasterId.FirstOrDefault().productionRequsitionDetails.productionRequsitionMaster.date;
                    ViewBag.nameEnglish = model.GetStocksbyMasterId.FirstOrDefault().productionRequsitionDetails.productionRequsitionMaster.createdBy;
                    ViewBag.totalAmount = model.GetStocksbyMasterId.FirstOrDefault().productionRequsitionDetails.productionRequsitionMaster.requisitionQty;
                }
                else
                {
                    //ViewBag.invoiceNumber = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.invoiceNumber;
                    //ViewBag.invoiceDate = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.invoiceDate;
                    //ViewBag.nameEnglish = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.posCustomer.name;
                    //ViewBag.totalAmount = model.GetStocksbyMasterId.FirstOrDefault().posInvoiceDetail.posInvoiceMaster.totalAmount;
                }

                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
