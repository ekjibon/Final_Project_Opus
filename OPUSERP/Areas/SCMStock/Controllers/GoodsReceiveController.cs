using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Areas.SCMStock.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.Stock;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.IOU.Interface;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.Stock.Interface;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMStock.Controllers
{
    [Area("SCMStock")]
    public class GoodsReceiveController : Controller
    {
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IIOUService iOUService;
        private readonly IStockService stockService;
        private readonly RequisitionStatusHistory requisitionStatusHistory;

        private readonly IUserInfoes userInfo;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public GoodsReceiveController(IHostingEnvironment hostingEnvironment, RequisitionStatusHistory requisitionStatusHistory, IIOUService iOUService, IConverter converter, IUserInfoes userInfo, IPurchaseOrderService purchaseOrderService, IStockService stockService)
        {
            this.purchaseOrderService = purchaseOrderService;
            this.requisitionStatusHistory = requisitionStatusHistory;
            this.stockService = stockService;
            this.iOUService = iOUService;
            this.userInfo = userInfo;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region GoodsReceive
        public async Task<ActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            PurchaseOrderViewModel model = new PurchaseOrderViewModel
            {
                purchaseOrderMasters = await purchaseOrderService.GetPurchaseOrderMasterForGR(userName),
                stockMasters = await stockService.GetStockMasterByReceiveType("PO", userName)
            };
            return View(model);
        }

        // GET: GoodsReceive/Details/5
        public async Task<ActionResult> StockIn(int id)
        {
            var GrNumber = await stockService.GetGRNumber();
            ViewBag.MRNO = GrNumber;
            StockInViewModel model = new StockInViewModel
            {
                PurchaseOrderMaster = await purchaseOrderService.GetPurchaseOrderMasterById(id),
                stockItemViewModels = await stockService.GetDueStockPurchaseDetailsInvoiceList(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] StockInViewModel model)
        {
            AspNetUsersViewModel user = await userInfo.GetUserInfoByUser(HttpContext.User.Identity.Name);
            var GRNumber = await stockService.GetGRNumber();
            if (!ModelState.IsValid || model.detailids == null)
            {
                model.PurchaseOrderMaster = await purchaseOrderService.GetPurchaseOrderMasterById((int)model.purchaseId);
                model.stockItemViewModels = await stockService.GetDueStockPurchaseDetailsInvoiceList((int)model.purchaseId);
                if (model.detailids == null)
                {
                    ModelState.AddModelError(string.Empty, "You have to Add minimum 1 Item");
                }
                return View(model);
            }

            //return Json(model);

            if (model.detailids != null)
            {
                StockMaster data = new StockMaster
                {
                    Id = Convert.ToInt32(model.stockMasterId),
                    receiveNo = GRNumber, //model.MRNO,
                    receiveDate = model.stockDate,
                    StockDate = model.stockDate,
                    receiveType = "PO",
                    purchaseId = model.purchaseId,
                    receiveBy = user.EmpName,
                    userId = user.UserId,
                    stockStatusId = 1
                };
                var masterId = await stockService.SaveStockMaster(data);

                for (var i = 0; i < model.detailids.Length; i++)
                {
                    //PurchasedetailsId = (int)model.detailids[i];
                    StockDetails data1 = new StockDetails
                    {
                        stockMasterId = masterId,
                        orderDetailsId = model.detailids[i],
                        purchaseRate = model.rates[i],
                        grQty = model.quantitys[i],
                        poQty = model.duequantitys[i],
                        itemSpecificationId = model.specids[i]
                    };
                    await stockService.SaveStock(data1);
                }

                var stockMaster = await stockService.GetStockMasterById(masterId);
                string empNameCode = user.EmpCode + "-" + user.EmpName;
                await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(stockMaster.purchase.cSMaster.requisitionId), 4, Convert.ToInt32(user.UserTypeId), user.UserId, empNameCode, model.remarks, "", 20, "GRPO", masterId, GRNumber);

            }
            return RedirectToAction(nameof(Index));
        }

        #endregion

        #region IOU Stock In
        public async Task<ActionResult> GRIOUList()
        {
            //AspNetUsersViewModel user = await userInfo.GetUserInfoByUser(HttpContext.User.Identity.Name);
            string userName = HttpContext.User.Identity.Name;
            PurchaseOrderViewModel model = new PurchaseOrderViewModel
            {
                iOUMasters = await iOUService.GetIOUMaster(userName),
                stockMasters = await stockService.GetStockMasterByReceiveType("IOU", userName)
            };
            return View(model);
        }

        public async Task<ActionResult> IOUStockIn(int id)
        {
            var GrNumber = await stockService.GetGRNumber();
            ViewBag.MRNO = GrNumber;
            StockInViewModel model = new StockInViewModel
            {
                iOUMaster = await iOUService.GetIOUMasterById(id),
                stockItemViewModels = await stockService.GetDueStockIOUDetailsInvoiceList(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> IOUStockIn([FromForm] StockInViewModel model)
        {
            AspNetUsersViewModel user = await userInfo.GetUserInfoByUser(HttpContext.User.Identity.Name);
            var GRNumber = await stockService.GetGRNumber();
            if (!ModelState.IsValid || model.detailids == null)
            {
                model.iOUMaster = await iOUService.GetIOUMasterById((int)model.iouId);
                model.stockItemViewModels = await stockService.GetDueStockIOUDetailsInvoiceList((int)model.iouId);
                if (model.detailids == null)
                {
                    ModelState.AddModelError(string.Empty, "You have to Add minimum 1 Item");
                }
                return View(model);
            }

            //return Json(model);

            if (model.detailids != null)
            {
                StockMaster data = new StockMaster
                {
                    Id = Convert.ToInt32(model.stockMasterId),
                    receiveNo = GRNumber, //model.MRNO,
                    receiveDate = model.stockDate,
                    StockDate = model.stockDate,
                    receiveType = "IOU",
                    IOUId = model.iouId,
                    receiveBy = user.EmpName,
                    userId = user.UserId
                };
                var masterId = await stockService.SaveStockMaster(data);
                int? requisitionId = 0;
                for (var i = 0; i < model.detailids.Length; i++)
                {
                    //PurchasedetailsId = (int)model.detailids[i];
                    StockDetails data1 = new StockDetails
                    {
                        stockMasterId = masterId,
                        iOUDetailsId = model.detailids[i],
                        purchaseRate = model.rates[i],
                        grQty = model.quantitys[i],
                        poQty = model.duequantitys[i],
                        itemSpecificationId = model.specids[i]
                    };
                    await stockService.SaveStock(data1);
                    IOUDetails details = await iOUService.GetIOUDetailsById(Convert.ToInt32(model.detailids[i]));
                    requisitionId = details.requisitionId;
                }

                var stockMaster = await stockService.GetStockMasterById(masterId);
                string empNameCode = user.EmpCode + "-" + user.EmpName;

                await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(requisitionId), 4, Convert.ToInt32(user.UserTypeId), user.UserId, empNameCode, model.remarks, "", 20, "GRIOU", masterId, GRNumber);

            }
            return RedirectToAction(nameof(GRIOUList));
        }

        #endregion

        #region Opening Stock

        public ActionResult OpeningStock(int Id)
        {
            ViewBag.OpeningStockId = Id;
            return View();
        }

        public async Task<ActionResult> OpeningStockList()
        {
            var stock = await stockService.GetStockDetails();
            OpeningStockViewModel model = new OpeningStockViewModel
            {
                //openingStocks = await stockService.GetOpeningStockAll(),
                stockDetails = stock.Where(a => a.stockMaster.receiveType == "Opening")
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OpeningStock([FromForm] OpeningStockViewModel model)
        {
            AspNetUsersViewModel user = await userInfo.GetUserInfoByUser(HttpContext.User.Identity.Name);
            var GRNumber = await stockService.GetGRNumber();

            //OpeningStock data = new OpeningStock
            //{
            //    Id = model.opeiningId,
            //    itemSpecificationId = model.itemSpecificationId,
            //    openingQty = model.openingQty,
            //    openingValue = model.openingValue,
            //    balanceUpTo = model.balanceUpTo
            //};
            //var masterId = await stockService.SaveOpeningStock(data);

            #region Stock Master
            StockMaster stockMaster = new StockMaster
            {
                Id = model.opeiningId,
                receiveNo = GRNumber,
                receiveDate = model.balanceUpTo,
                StockDate = model.balanceUpTo,
                receiveType = "Opening",
                receiveBy = user.EmpName,
                userId = user.UserId,
                stockStatusId = model.stockStatusId
            };
            int stockmasterId = await stockService.SaveStockMaster(stockMaster);

            //var stockData= await stockService.GetAllStockByMasterId(stockmasterId);
            //if (stockData.Count() > 0)
            //{
            //    await stockService.DeleteStockMasterById(stockmasterId);
            //}

            StockDetails data1 = new StockDetails
            {
                Id = model.stockDetailId,
                stockMasterId = stockmasterId,
                purchaseRate = model.openingRate,
                grQty = model.openingQty,
                poQty = model.openingQty,
                itemSpecificationId = model.itemSpecificationId
            };
            await stockService.SaveStock(data1);

            #endregion

            return RedirectToAction(nameof(OpeningStockList));
        }

        [HttpGet]
        public async Task<IActionResult> GetOpeningStockById(int id)
        {
            return Json(await stockService.GetOpeningStockById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetStockBymasterId(int id)
        {
            return Json(await stockService.GetStockBymasterId(id));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteOpeningStockById(int Id)
        {
            await stockService.DeleteOpeningStockById(Id);
            return Json(true);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteStockMasterDetailsByMasterId(int Id)
        {
            await stockService.DeleteStockMasterById(Id);
            await stockService.DeleteStockMasterByMasterId(Id);
            return Json(true);
        }

        #endregion
    }
}