using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.Rental.Services.Sales.Interfaces;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.OtherSales.Controllers
{
    [Area("OtherSales")]
    public class SalesMenuController : Controller
    {

        private readonly IERPModuleService eRPModuleService;
        private readonly IItemsService itemsService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IPaymentModeService paymentModeService;

        private readonly IHostingEnvironment _hostingEnvironment;     
       
       


        public SalesMenuController(IHostingEnvironment hostingEnvironment, IERPModuleService eRPModuleService, IItemsService itemsService, IAttachmentCommentService attachmentCommentService, IPaymentModeService paymentModeService)
        {           
            this.itemsService = itemsService;
            this.attachmentCommentService = attachmentCommentService; 
            this.eRPModuleService = eRPModuleService;
            this.paymentModeService = paymentModeService;

            this._hostingEnvironment = hostingEnvironment;

        }

        #region SalesMenuCategory

        public async Task<IActionResult> SalesMenuCategory()
        {
            SalesMenuCategoryViewModel model = new SalesMenuCategoryViewModel()
            {
                salesMenuCategories = await paymentModeService.GetAllSalesMenuCategory(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalesMenuCategory([FromForm] SalesMenuCategoryViewModel model)
        {
            SalesMenuCategory data = new SalesMenuCategory
            {
                Id = model.masterId,
                categoryName = model.categoryName,
                activeStatus = model.activeStatus
            };

            await paymentModeService.SaveSalesMenuCategory(data);
            return RedirectToAction(nameof(SalesMenuCategory));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSalesMenuCategoryById(int Id)
        {
            await paymentModeService.DeleteSalesMenuCategoryById(Id);
            return Json(true);
        }

        #endregion

        #region Sales Menu       

        public async Task<IActionResult> SalesMenu()
        {
            SalesMenuViewModel model = new SalesMenuViewModel()
            {
                salesMenus = await paymentModeService.GetAllSalesMenu(),
                salesMenuCategories= await paymentModeService.GetAllSalesMenuCategory(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SalesMenu([FromForm] SalesMenuViewModel model)
        {
            SalesMenu opening = new SalesMenu
            {
                Id = model.masterId,
                itemSpecificationId = model.itemSpecificationId,
                salesMenuCategoryId=model.salesMenuCategoryId,
                activeStatus = model.activeStatus
            };
            int stockmasterId = await paymentModeService.SaveSalesMenu(opening);

            return RedirectToAction(nameof(SalesMenu));
        }
       

        [HttpPost]
        public async Task<JsonResult> DeleteSalesMenuById(int Id)
        {
            await paymentModeService.DeleteSalesMenuById(Id);
            return Json(true);
        }


        #endregion


        #region Menu Opening Balance

        public ActionResult MenuOpening(int Id)
        {
            ViewBag.OpeningStockId = Id;
            return View();
        }

        public async Task<ActionResult> MenuOpeningList()
        {
            var opening = await paymentModeService.GetAllSalesMenuOpeningBalance();
            SalesMenuOpeningBalanceViewModel model = new SalesMenuOpeningBalanceViewModel
            {
                salesMenuOpeningBalances = opening
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MenuOpening([FromForm] SalesMenuOpeningBalanceViewModel model)
        {
            SalesMenuOpeningBalance opening = new SalesMenuOpeningBalance
            {
                Id = model.opeiningId,
                itemSpecificationId = model.itemSpecificationId,
                openingDate = model.openingDate,
                openingQty = model.openingQty
            };
            int stockmasterId = await paymentModeService.SaveSalesMenuOpeningBalance(opening);          

            return RedirectToAction(nameof(MenuOpeningList));
        }

        [HttpGet]
        public async Task<IActionResult> GetSalesMenuOpeningBalanceById(int id)
        {
            return Json(await paymentModeService.GetSalesMenuOpeningBalanceById(id));
        }        

        [HttpPost]
        public async Task<JsonResult> DeleteSalesMenuOpeningBalanceById(int Id)
        {
            await paymentModeService.DeleteSalesMenuOpeningBalanceById(Id);
            return Json(true);
        }
        

        #endregion





    }
}