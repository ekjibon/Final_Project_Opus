using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.HRPMS.Data.Entity.Gratuity;
using OPUSERP.HRPMS.Services.Gratuity.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class GratuityController : Controller
    {
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IUserInfoes userInfo;
        private readonly IGratuityPolicyService gratuityPolicyService;

        public GratuityController(ISpecialBranchUnitService specialBranchUnitService, IUserInfoes userInfo, IGratuityPolicyService gratuityPolicyService)
        {
            this.specialBranchUnitService = specialBranchUnitService;
            this.userInfo = userInfo;
            this.gratuityPolicyService = gratuityPolicyService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> GratuityPolicy()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            int? branchId = userInfos.projectId;
            GratuityViewModel model = new GratuityViewModel
            {
                branches = await specialBranchUnitService.GetSpecialBranchUnit(),
                gratuityPolicyDetails = await gratuityPolicyService.GetGratuityPolicyDetail()
                //gratuityPolicyDetails = await gratuityPolicyService.GetGratuityPolicyDetailByBranchId(branchId)
            };
            return View(model);
            
        }

        [HttpPost]
        public async Task<IActionResult> GratuityPolicy([FromForm] GratuityViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            int id = 0;
            var ploicy = await gratuityPolicyService.GetGratuityPolicyMasterBybranch(model.sbuId);
            if (ploicy != null)
            {
                id = ploicy.Id;
            }

            GratuityPolicyMaster master = new GratuityPolicyMaster
            {
                Id = id,
                sbuId = model.sbuId,
                daysDIv = model.daysDIv,
                isJoiningDate = model.isJoiningDate,
                roundMode = model.roundMode
            };
            int masterId = await gratuityPolicyService.SaveGratuityPolicyMaster(master);

            if (model.Id > 0)
            {
                await gratuityPolicyService.DeleteGratuityPolicyDetailByMasterId(Convert.ToInt32(id));
            }
            for (int i = 0; i < model.FromYear.Length; i++)
            {
                GratuityPolicyDetail details = new GratuityPolicyDetail
                {
                    Id = 0,
                    gratuityPolicyId = masterId,
                    fromYear = model.FromYear[i],
                    toYear = model.ToYear[i],
                    Times=model.Times[i]
                };
                await gratuityPolicyService.SaveGratuityPolicyDetail(details);
            }

            return RedirectToAction(nameof(GratuityPolicy));
        }

        public async Task<IActionResult> GratuityProcess()
        {
            GratuityViewModel model = new GratuityViewModel
            {
                branches = await specialBranchUnitService.GetSpecialBranchUnit()
            };
            return View(model);
        }
    }
}