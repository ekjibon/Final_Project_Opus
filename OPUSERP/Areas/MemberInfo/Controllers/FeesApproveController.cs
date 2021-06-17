using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Auth.Interfaces;
using OPUSERP.CLUB.Services.Finance.Interface;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.Areas.MemberInfo.Models.Lang;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Area("MemberInfo")]
    public class FeesApproveController : Controller
    {
        // GET: FeesApprove
        private readonly LangGenerate<FeeLn> _lang;
        private readonly IMemberFeesService memberFeesService;
        private readonly UserManager<ApplicationUser> _userManage;
        private readonly ILoggedinUser loggedinUser;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IBalanceService balanceService;
        private readonly ITrMasterService trMasterService;

        public FeesApproveController(IHostingEnvironment hostingEnvironment, IMemberFeesService memberFeesService, UserManager<ApplicationUser> userManage, ILoggedinUser loggedinUser, IPersonalInfoService personalInfoService, IBalanceService balanceService, ITrMasterService trMasterService)
        {
            _lang = new LangGenerate<FeeLn>(hostingEnvironment.ContentRootPath);
            this.memberFeesService = memberFeesService;
            this.personalInfoService = personalInfoService;
            this.balanceService = balanceService;
            this.trMasterService = trMasterService;
            _userManage = userManage;
            this.loggedinUser = loggedinUser;
        }


        // GET: EmployeeType
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManage.GetUserAsync(HttpContext.User);
            int employeeId = loggedinUser.UserAuthId(user.Id);

            FeesViewModel model = new FeesViewModel
            {
                fLang = _lang.PerseLang("Fee/FeeEN.json", "Fee/FeeBN.json", Request.Cookies["lang"]),
                paymentLogs = await memberFeesService.GetAllPendingPayment(),
                trMasters = await trMasterService.GetPendingTrMaster(),
            };
            //return Json(model);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] FeesViewModel model)
        {
            //return Json(model);
            model.fLang = _lang.PerseLang("Fee/FeeEN.json", "Fee/FeeBN.json", Request.Cookies["lang"]);
            await memberFeesService.UpdatePaymentStatus(Convert.ToInt32(model.Id), Convert.ToInt32(model.actionType), model.remarks);
            return RedirectToAction(nameof(Index));
        }
    }
}