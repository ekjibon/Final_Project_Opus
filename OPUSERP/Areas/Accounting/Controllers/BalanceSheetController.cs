using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;


namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class BalanceSheetController : Controller
    {
        private readonly IVoucherService VoucherService;
        private readonly ILedgerService ledgerService;
        private readonly IPurchaseOrderService purchaseOrderService;
        private readonly IGroupNatureService groupNatureService;
        private readonly IAccountGroupService accountGroupService;
        private readonly ICostCentreService costCentreService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;
        public BalanceSheetController(IVoucherService VoucherService, ILedgerService ledgerService, IPurchaseOrderService purchaseOrderService, IGroupNatureService groupNatureService, IAccountGroupService accountGroupService, ICostCentreService costCentreService, IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService, IConverter converter)
        {
            this.VoucherService = VoucherService;
            this.ledgerService = ledgerService;
            this.purchaseOrderService = purchaseOrderService;
            this.groupNatureService = groupNatureService;
            this.accountGroupService = accountGroupService;
            this.costCentreService = costCentreService;
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;

            myPDF = new MyPDF(hostingEnvironment, converter);

            rootPath = hostingEnvironment.ContentRootPath;
        }

        #region Balance Sheet Master

        public async Task<IActionResult> Index()
        {
            BalanceSheetMasterViewModel model = new BalanceSheetMasterViewModel
            {
                groupNatures = await groupNatureService.GetgroupNature(),
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BalanceSheetMasterViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster();
               
            }

            BalanceSheetMaster data = new BalanceSheetMaster
            {
                Id = model.balanceSheetMasterId,
                accountGroupId = model.accountGroupId,
                fsLineName = model.fsLineName,
                noteNo = model.noteNo,
                serialNo = model.serialNo,
                fsLineFor = "BSS"

            };

            await accountGroupService.SaveBalanceSheetMaster(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteBalanceSheetMaster(int Id)
        {
            await accountGroupService.DeleteBalanceSheetMasterById(Id);
            return Json(true);
        }
        

        [HttpGet]
        public async Task<IActionResult> GetLedgerCountByGorupId(int Id)
        {
            return Json(await ledgerService.GetLedgerCountByGorupId(Id));
        }

        [HttpGet]
        public async Task<IActionResult> GetAccountGroupByNatureId(int? natureId)
        {
            return Json(await accountGroupService.GetAccountGroupByNatureId(natureId));
        }

        #endregion

        #region Balance Sheet Note Master

        public async Task<IActionResult> NoteMaster()
        {
            NoteMasterViewModel model = new NoteMasterViewModel
            {
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NoteMaster([FromForm] NoteMasterViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.noteMasters = await accountGroupService.GetNoteMaster();

            //}

            NoteMaster data = new NoteMaster
            {
                Id = model.noteMasterId,
                noteName = model.noteName,
                noteHead = model.noteHead,
                nmSerialNo=model.nmSerialNo,
                balanceSheetMasterId = model.balanceSheetMasterId
            };

            await accountGroupService.SavenoteMaster(data);

            return RedirectToAction(nameof(NoteMaster));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteNoteMaster(int Id)
        {
            await accountGroupService.DeleteNoteMasterById(Id);
            return Json(true);
        }
        [HttpPost]
        public async Task<JsonResult> DeleteBalanceSheetDetail(int Id)
        {
            await accountGroupService.DeleteBalanceSheetDetailsById(Id);
            return Json(true);
        }




        #endregion

        #region Balance Sheet Detail

        public async Task<IActionResult> BalanceSheetDetail()
        {
            BalanceSheetDetailViewModel model = new BalanceSheetDetailViewModel
            {
                balanceSheetMasters=await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster(),
                balanceSheetDetails=await accountGroupService.GetBalanceSheetDetails(),
                ledgers=await ledgerService.GetLedger()

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BalanceSheetDetail([FromForm] BalanceSheetDetailViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.noteMasters = await accountGroupService.GetNoteMaster();

            }

            BalanceSheetDetails data = new BalanceSheetDetails
            {
                Id = model.balanceSheetDetailId,
                noteMasterId = model.noteMasterId,
                ledgerId = model.ledgerId,
              
            };

            await accountGroupService.SaveBalanceSheetDetails(data);

            return RedirectToAction(nameof(BalanceSheetDetail));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteBalanceSheetDetails(int Id)
        {
            await accountGroupService.DeleteBalanceSheetDetailsById(Id);
            return Json(true);
        }

        [Route("/api/global/GetNoteMasterbyMasterId/{id}")]
        public async Task<JsonResult> GetNoteMasterbyMasterId(int id)
        {
            var subLedgers = await accountGroupService.GetNoteMasterByMasterId(id);
            return Json(subLedgers);
        }




        #endregion

        #region Profit Loss Account Master

        public async Task<IActionResult> ProfitLossMaster()
        {
            BalanceSheetMasterViewModel model = new BalanceSheetMasterViewModel
            {
                groupNatures = await groupNatureService.GetgroupNature(),
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfitLossMaster([FromForm] BalanceSheetMasterViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster();

            }

            BalanceSheetMaster data = new BalanceSheetMaster
            {
                Id = model.balanceSheetMasterId,
                accountGroupId = model.accountGroupId,
                fsLineName = model.fsLineName,
                noteNo = model.noteNo,
                serialNo = model.serialNo,
                fsLineFor = "PLA"

            };

            await accountGroupService.SaveBalanceSheetMaster(data);

            return RedirectToAction(nameof(ProfitLossMaster));
        }
        #endregion

        #region Profit Loss Note Master

        public async Task<IActionResult> NoteMasterProfitLoss()
        {
            NoteMasterViewModel model = new NoteMasterViewModel
            {
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NoteMasterProfitLoss([FromForm] NoteMasterViewModel model)
        {           
            NoteMaster data = new NoteMaster
            {
                Id = model.noteMasterId,
                noteName = model.noteName,
                noteHead = model.noteHead,
                balanceSheetMasterId = model.balanceSheetMasterId
            };

            await accountGroupService.SavenoteMaster(data);

            return RedirectToAction(nameof(NoteMasterProfitLoss));
        }

        #endregion

        #region Profit & Loss Detail

        public async Task<IActionResult> ProfitLossDetail()
        {
            BalanceSheetDetailViewModel model = new BalanceSheetDetailViewModel
            {
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster(),
                balanceSheetDetails = await accountGroupService.GetBalanceSheetDetails(),
                ledgers = await ledgerService.GetLedger()

            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfitLossDetail([FromForm] BalanceSheetDetailViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.noteMasters = await accountGroupService.GetNoteMaster();

            }

            BalanceSheetDetails data = new BalanceSheetDetails
            {
                Id = model.balanceSheetDetailId,
                noteMasterId = model.noteMasterId,
                ledgerId = model.ledgerId,

            };

            await accountGroupService.SaveBalanceSheetDetails(data);

            return RedirectToAction(nameof(ProfitLossDetail));
        }

        #endregion

        #region CFS Account Master

        public async Task<IActionResult> CFSMaster()
        {
            BalanceSheetMasterViewModel model = new BalanceSheetMasterViewModel
            {
                groupNatures = await groupNatureService.GetgroupNature(),
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CFSMaster([FromForm] BalanceSheetMasterViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster();

            }

            BalanceSheetMaster data = new BalanceSheetMaster
            {
                Id = model.balanceSheetMasterId,
                accountGroupId = model.accountGroupId,
                fsLineName = model.fsLineName,
                noteNo = model.noteNo,
                serialNo = model.serialNo,
                fsLineFor = "CFS"

            };

            await accountGroupService.SaveBalanceSheetMaster(data);

            return RedirectToAction(nameof(CFSMaster));
        }
        #endregion

        #region CFS Indirect Account Master 

        public async Task<IActionResult> CFSMasterIND()
        {
            BalanceSheetMasterViewModel model = new BalanceSheetMasterViewModel
            {
                groupNatures = await groupNatureService.GetgroupNature(),
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CFSMasterIND([FromForm] BalanceSheetMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.groupNatures = await groupNatureService.GetgroupNature();
                model.balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster();
            }

            BalanceSheetMaster data = new BalanceSheetMaster
            {
                Id = model.balanceSheetMasterId,
                accountGroupId = model.accountGroupId,
                fsLineName = model.fsLineName,
                noteNo = model.noteNo,
                serialNo = model.serialNo,
                fsLineFor = "CFSIND"
            };
            await accountGroupService.SaveBalanceSheetMaster(data);
            return RedirectToAction(nameof(CFSMasterIND));
        }
        #endregion

        #region CFS Note Master

        public async Task<IActionResult> NoteMasterCFS()
        {
            NoteMasterViewModel model = new NoteMasterViewModel
            {
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NoteMasterCFS([FromForm] NoteMasterViewModel model)
        {
            NoteMaster data = new NoteMaster
            {
                Id = model.noteMasterId,
                noteName = model.noteName,
                noteHead = model.noteHead,
                balanceSheetMasterId = model.balanceSheetMasterId
            };

            await accountGroupService.SavenoteMaster(data);

            return RedirectToAction(nameof(NoteMasterCFS));
        }
        #endregion

        #region CFS Indirect Note Master

        public async Task<IActionResult> NoteMasterCFSIND()
        {
            NoteMasterViewModel model = new NoteMasterViewModel
            {
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NoteMasterCFSIND([FromForm] NoteMasterViewModel model)
        {
            NoteMaster data = new NoteMaster
            {
                Id = model.noteMasterId,
                noteName = model.noteName,
                noteHead = model.noteHead,
                balanceSheetMasterId = model.balanceSheetMasterId,
                nmSerialNo = model.nmSerialNo
            };
            await accountGroupService.SavenoteMaster(data);
            return RedirectToAction(nameof(NoteMasterCFSIND));
        }
        #endregion
        
        #region CFS Note Details

        public async Task<IActionResult> CFSDetail()
        {
            BalanceSheetDetailViewModel model = new BalanceSheetDetailViewModel
            {
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster(),
                balanceSheetDetails = await accountGroupService.GetBalanceSheetDetails(),
                ledgers = await ledgerService.GetLedger()

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CFSDetail([FromForm] BalanceSheetDetailViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.noteMasters = await accountGroupService.GetNoteMaster();

            }

            BalanceSheetDetails data = new BalanceSheetDetails
            {
                Id = model.balanceSheetDetailId,
                noteMasterId = model.noteMasterId,
                ledgerId = model.ledgerId,

            };

            await accountGroupService.SaveBalanceSheetDetails(data);

            return RedirectToAction(nameof(CFSDetail));
        }
        #endregion

        #region CFS Indirect Note Details

        public async Task<IActionResult> CFSDetailIND()
        {
            BalanceSheetDetailViewModel model = new BalanceSheetDetailViewModel
            {
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster(),
                balanceSheetDetails = await accountGroupService.GetBalanceSheetDetails(),
                ledgers = await ledgerService.GetLedger()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CFSDetailIND([FromForm] BalanceSheetDetailViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.noteMasters = await accountGroupService.GetNoteMaster();
                model.balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster();
                model.balanceSheetDetails = await accountGroupService.GetBalanceSheetDetails();
                model.ledgers = await ledgerService.GetLedger();
            }

            BalanceSheetDetails data = new BalanceSheetDetails
            {
                Id = model.balanceSheetDetailId,
                noteMasterId = model.noteMasterId,
                ledgerId = model.ledgerId,
            };
            await accountGroupService.SaveBalanceSheetDetails(data);
            return RedirectToAction(nameof(CFSDetailIND));
        }
        #endregion

        #region RV Account Master

        public async Task<IActionResult> RVMaster()
        {
            BalanceSheetMasterViewModel model = new BalanceSheetMasterViewModel
            {
                groupNatures = await groupNatureService.GetgroupNature(),
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RVMaster([FromForm] BalanceSheetMasterViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster();

            }

            BalanceSheetMaster data = new BalanceSheetMaster
            {
                Id = model.balanceSheetMasterId,
                accountGroupId = model.accountGroupId,
                fsLineName = model.fsLineName,
                noteNo = model.noteNo,
                serialNo = model.serialNo,
                fsLineFor = "RV"

            };

            await accountGroupService.SaveBalanceSheetMaster(data);

            return RedirectToAction(nameof(RVMaster));
        }
        #endregion

        #region RV Note Master

        public async Task<IActionResult> NoteMasterRV()
        {
            NoteMasterViewModel model = new NoteMasterViewModel
            {
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NoteMasterRV([FromForm] NoteMasterViewModel model)
        {
            NoteMaster data = new NoteMaster
            {
                Id = model.noteMasterId,
                noteName = model.noteName,
                noteHead = model.noteHead,
                balanceSheetMasterId = model.balanceSheetMasterId
            };

            await accountGroupService.SavenoteMaster(data);

            return RedirectToAction(nameof(NoteMasterRV));
        }
        #endregion

        #region RV Details Detail

        public async Task<IActionResult> RVDetail()
        {
            BalanceSheetDetailViewModel model = new BalanceSheetDetailViewModel
            {
                balanceSheetMasters = await accountGroupService.GetBalanceSheetMaster(),
                noteMasters = await accountGroupService.GetNoteMaster(),
                balanceSheetDetails = await accountGroupService.GetBalanceSheetDetails(),
                ledgers = await ledgerService.GetLedger()

            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RVDetail([FromForm] BalanceSheetDetailViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.noteMasters = await accountGroupService.GetNoteMaster();

            }

            BalanceSheetDetails data = new BalanceSheetDetails
            {
                Id = model.balanceSheetDetailId,
                noteMasterId = model.noteMasterId,
                ledgerId = model.ledgerId,

            };

            await accountGroupService.SaveBalanceSheetDetails(data);

            return RedirectToAction(nameof(RVDetail));
        }
        #endregion
    }
}