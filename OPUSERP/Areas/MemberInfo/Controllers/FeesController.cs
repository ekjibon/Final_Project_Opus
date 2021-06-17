using System.IO;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Member.Interfaces;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Services.Auth.Interfaces;
using OPUSERP.CLUB.Services.Finance.Interface;
using OPUSERP.CLUB.Services.Event.Interface;
using OPUSERP.CLUB.Data.Finance;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.Areas.MemberInfo.Models.Lang;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Area("MemberInfo")]
    public class FeesController : Controller
    {
        private readonly LangGenerate<FeeLn> _lang;
        private readonly IMemberFeesService memberFeesService;
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly ILoggedinUser loggedinUser;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IBalanceService balanceService;
        private readonly IPaymentHeadService paymentHeadService;
        private readonly ICollectionService collectionService;
        private readonly ITrMasterService trMasterService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public FeesController(IHostingEnvironment hostingEnvironment, IMemberFeesService memberFeesService, UserManager<ApplicationUser> userManage, ILoggedinUser loggedinUser, IPersonalInfoService personalInfoService, IBalanceService balanceService, IConverter converter, IPaymentHeadService paymentHeadService, ICollectionService collectionService, ITrMasterService trMasterService)
        {
            _lang = new LangGenerate<FeeLn>(hostingEnvironment.ContentRootPath);
            this.memberFeesService = memberFeesService;
            this.personalInfoService = personalInfoService;
            this.balanceService = balanceService;
            this.paymentHeadService = paymentHeadService;
            this.collectionService = collectionService;
            this.trMasterService = trMasterService;
            _userManage = userManage;
            this.loggedinUser = loggedinUser;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        
        // GET: EmployeeType
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);
            int employeeId = loggedinUser.UserAuthId(user.Id);

            FeesViewModel model = new FeesViewModel
            {
                fLang = _lang.PerseLang("Fee/FeeEN.json", "Fee/FeeBN.json", Request.Cookies["lang"]),
                singleFees = await memberFeesService.GetSingleFeesById(employeeId),
                singleFeeses = await memberFeesService.GetAllEmployeePaymentSummery(),
                trMasters = await trMasterService.GetTrMasterByEmpId(employeeId)
            };
            //return Json(model);
            return View(model);
        }

        // GET: Fees/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            FeesViewModel model = new FeesViewModel
            {
                //paymentLog = await memberFeesService.GetPaymentById(id),
                trMaster = await trMasterService.GetTrMasterById(id),
                collections = await collectionService.GetCollectionByTrMasterId(id),
            };
            //return Json(model);
            return View(model);
        }

        // GET: Fees/Details/5
        public async Task<IActionResult> Payment()
        {
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);
            int employeeId = loggedinUser.UserAuthId(user.Id);

            FeesViewModel model = new FeesViewModel
            {
                singleFeeses = await memberFeesService.GetSingleFees(),
                monthlyPaymentHeadWithDues = await paymentHeadService.GetMonthlyPaymentHeadByMemberId(employeeId),
                onetimePaymentHeadWithDues = await paymentHeadService.GetOnetimePaymentHeadByMemberId(employeeId),
            };
            //return Json(model);
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult pdspdf(int id)
        {
            string fileName;
            string status = myPDF.GeneratePDF(out fileName, $"Employee/Fees/Details/{id}");

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        // GET: Fees/Create
        public async Task<IActionResult> OpeningBalance()
        {
            FeesViewModel model = new FeesViewModel
            {
                fLang = _lang.PerseLang("Fee/FeeEN.json", "Fee/FeeBN.json", Request.Cookies["lang"]),
                employeeInfos = await personalInfoService.GetEmployeeInfoByType(),
                balances = await balanceService.GetBalance(),
                paymentHeads = await paymentHeadService.GetAllMonthlyPaymentHead(),
            };
            //return Json(model);
            return View(model);
        }
        
        // POST: EventInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> OpeningBalance([FromForm] FeesViewModel model)
        {
            //return Json(model);
            Balance data = new Balance
            {
                Id = (int)model.Id,
                employeeId = model.employeeId,
                paymentHeadId = model.paymentHead,
                date = model.date,
                paid = model.amount,
                due = model.due,
            };
            //return Json(model);
            model.fLang = _lang.PerseLang("Fee/FeeEN.json", "Fee/FeeBN.json", Request.Cookies["lang"]);
            await balanceService.SaveBalance(data);
            return RedirectToAction(nameof(OpeningBalance));
        }

        // POST: EventInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Payment([FromForm] FeesViewModel model)
        {
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);
            int employeeId = loggedinUser.UserAuthId(user.Id);

            if (model.total < 1)
            {
                model.monthlyPaymentHeadWithDues = await paymentHeadService.GetMonthlyPaymentHeadByMemberId(employeeId);
                model.onetimePaymentHeadWithDues = await paymentHeadService.GetOnetimePaymentHeadByMemberId(employeeId);
                ModelState.AddModelError(string.Empty, "You have to pay minimum 1 taka.");
                return View(model);
            }

            string fileFullName;

            string fileName = string.Empty;
            string message = "No Image";
            if (model.bankReceipt != null)
            {
                message = FileSave.SaveImage(out fileName, model.bankReceipt);
            }

            if (message == "success")
            {
                fileFullName = fileName;
            }
            else
            {
                fileFullName = model.hiddenFile;
            }

            TrMaster data = new TrMaster
            {
                employeeId = employeeId,
                date = model.date,
                amount = model.total,
                url = fileFullName,
                paymentType = model.type,
                remarks = model.remarks,
                trNumber = model.transectionId,
                status = 0
            };

            //return Json(data);

            int trMasterId = await trMasterService.SaveTrMaster(data);

            if (model.selectPaymentHeadIds!=null)
            {
                for (var i=0;i<model.selectPaymentHeadIds.Length;i++)
                {
                    Collection data1 = new Collection
                    {
                        employeeId = employeeId,
                        paymentHeadId = model.selectPaymentHeadIds[i],
                        amount = model.selectPaymentHeadAmounts[i],
                        trMasterId = trMasterId,
                        paymentDate = model.date,
                        remarks = model.remarks,
                        status = "pending"
                    };
                    await collectionService.SaveCollection(data1);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Fees/Details/5
        public async Task<IActionResult> PaymentAll(int id)
        {
            FeesViewModel model = new FeesViewModel
            {
                employeeId = id,
                singleFeeses = await memberFeesService.GetSingleFees(),
                monthlyPaymentHeadWithDues = await paymentHeadService.GetMonthlyPaymentHeadByMemberId(id),
                onetimePaymentHeadWithDues = await paymentHeadService.GetOnetimePaymentHeadByMemberId(id),
            };
            //return Json(model);
            return View(model);
        }

        // POST: EventInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PaymentAll([FromForm] FeesViewModel model)
        {
            //return Json(model); 
            TrMaster data = new TrMaster
            {
                employeeId = model.employeeId,
                date = model.date,
                amount = model.total,
                paymentType = "Cash",
                adminFeedBack = model.remarks,
                status = 1
            };

            //return Json(data);

            int trMasterId = await trMasterService.SaveTrMaster(data);

            if (model.selectPaymentHeadIds != null)
            {
                for (var i = 0; i < model.selectPaymentHeadIds.Length; i++)
                {
                    Collection data1 = new Collection
                    {
                        employeeId = model.employeeId,
                        paymentHeadId = model.selectPaymentHeadIds[i],
                        amount = model.selectPaymentHeadAmounts[i],
                        trMasterId = trMasterId,
                        paymentDate = model.date,
                        remarks = model.remarks,
                        status = "Approve"
                    };
                    await collectionService.SaveCollection(data1);
                }
            }

            //return Json(data);

            return RedirectToAction(nameof(Index));
        }

        // GET: Fees/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> PaymentDetails(int id)
        {
            FeesViewModel model = new FeesViewModel
            {
                paymentReports = await memberFeesService.GetEmployeePaymentSummeryByEmpId(id)
            };
            //return Json(model);
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult PaymentDetailsPdf(int id)
        {
            string fileName;
            string status = myPDF.GeneratePDF(out fileName, $"Employee/Fees/PaymentDetails/{id}");

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}