using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class BankController : Controller
    {
        private readonly IBankBranchService bankBranchService;
        private readonly IAddressService addressService;

        public BankController(IBankBranchService bankBranchService, IAddressService addressService)
        {
            this.bankBranchService = bankBranchService;
            this.addressService = addressService;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region FI Type

        public async Task<IActionResult> FIType()
        {
            BankBranchViewModel model = new BankBranchViewModel()
            {
                fITypes = await bankBranchService.GetAllFIType()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveFIType([FromForm] BankBranchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fITypes = await bankBranchService.GetAllFIType();
                return View(model);
            }

            FIType data = new FIType
            {
                Id = Convert.ToInt32(model.fiTypeId),
                fiTypeName = model.fiTypeName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await bankBranchService.SaveFIType(data);

            return RedirectToAction(nameof(FIType));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteFITypeById(int Id)
        {
            await bankBranchService.DeleteFITypeById(Id);
            return Json(true);
        }

        #endregion

        #region Bank Info

        public async Task<IActionResult> BankInfo()
        {
            BankBranchViewModel model = new BankBranchViewModel()
            {
                banks = await bankBranchService.GetAllBank(),
                fITypes = await bankBranchService.GetAllFIType()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BankInfo([FromForm] BankBranchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.banks = await bankBranchService.GetAllBank();
                return View(model);
            }

            Bank data = new Bank
            {
                Id = model.bankId,
                bankName = model.bankName,
                fiTypeId = model.fiTypeId,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await bankBranchService.SaveBank(data);

            return RedirectToAction(nameof(BankInfo));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteBankById(int Id)
        {
            await bankBranchService.DeleteBankById(Id);
            return Json(true);
        }

        #endregion

        #region Branch Info

        public async Task<IActionResult> BranchInfo()
        {
            BankBranchViewModel model = new BankBranchViewModel()
            {
                bankBranches = await bankBranchService.GetAllBankBranch()
            };
            return View(model);
        }

        // POST: Activity Status/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BranchInfo([FromForm] BankBranchViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.bankBranches = await bankBranchService.GetAllBankBranch();
                return View(model);
            }

            BankBranch data = new BankBranch
            {
                Id = model.branchId,
                branchName = model.branchName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await bankBranchService.SaveBankBranch(data);

            return RedirectToAction(nameof(BranchInfo));
        }


        #endregion

        #region Bank Branch Info

        public async Task<IActionResult> BankBranchInfo()
        {
            BranchDetailsViewModel model = new BranchDetailsViewModel()
            {
                banks = await bankBranchService.GetAllBank(),
                bankBranches = await bankBranchService.GetAllBankBranch(),
                thanas = await addressService.GetAllThana(),
                bankBranchDetails = await bankBranchService.GetAllBankBranchDetails()
            };
            return View(model);
        }

        // POST: Activity Status/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BankBranchInfo([FromForm] BranchDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.banks = await bankBranchService.GetAllBank();
                model.bankBranches = await bankBranchService.GetAllBankBranch();
                model.thanas = await addressService.GetAllThana();
                return View(model);
            }

            BankBranchDetails data = new BankBranchDetails
            {
                Id = (int)model.branchDetailsId,
                bankId = model.bankId,
                bankBranchId = model.branchId,
                thanaId = model.thanaId,
                accountNo = model.accountNo,
                maillingAddress = model.mailingAddress,
                email = model.email,
                fax = model.fax,
                website = model.website,
                phone = model.phone,
                mobile = model.mobile,
                collectionType = model.collectionType,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await bankBranchService.SaveBankBranchDetails(data);

            return RedirectToAction(nameof(BankBranchInfo));
        }
        #endregion

    }
}