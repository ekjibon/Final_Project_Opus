using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using OPUSERP.Areas.MasterData.Models;
using DinkToPdf.Contracts;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class LedgerOpeningBalanceController : Controller
    {
        private readonly ILedgerService ledgerService;
        private readonly IAccountGroupService accountGroupService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOpeningBalanceService openingBalanceService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public LedgerOpeningBalanceController(ILedgerService ledgerService, IAccountGroupService accountGroupService, IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService, IConverter converter, IOpeningBalanceService openingBalanceService)
        {
            this.ledgerService = ledgerService;
            this.accountGroupService = accountGroupService;
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.openingBalanceService = openingBalanceService;

            myPDF = new MyPDF(hostingEnvironment, converter);

            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: Ledger
        public async Task<IActionResult> Index()
        {
            LedgerOpeningBalanceViewModel model = new LedgerOpeningBalanceViewModel
            {
                openingBalances = await openingBalanceService.GetOpeningBalance(),
                
            };
         
            return View(model);
        }

        // POST: Ledger/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<JsonResult> Index([FromForm] LedgerOpeningBalanceViewModel model)
        {
            int? RelationId = 0;
            if(model.subledgerRelationId>0)
            {
                RelationId = model.subledgerRelationId;
            }
            else
            {
                RelationId = model.ledgerRelationId;
            }
            OpeningBalance data = new OpeningBalance
            {
                Id = model.ledgerOpeningBalanceId,
                ledgerRelationId = RelationId,
                amount = model.amount,
                transectionModeId = model.transectionModeId,
                balanceUpTo = model.balanceUpTo
            };

           int ledgerOpeningBalanceId=  await openingBalanceService.SaveopeningBalance(data);

            return Json(ledgerOpeningBalanceId);
            //return RedirectToAction(nameof(Index));
            //return RedirectToAction("LedgerDetails", "Ledger", new { id = ledgerId, Area = "Accounting" });
        }

        [HttpPost]
        public async Task<JsonResult> DeleteopeningBalanceById(int Id)
        {
            await openingBalanceService.DeleteopeningBalanceById(Id);
            return Json(true);
        }

    }
}