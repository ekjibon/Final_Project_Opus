using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class ApprovalController : Controller
    {
       
        private readonly IApprovalService approvalService;
        private readonly IPersonalInfoService personalInfoService;

        public ApprovalController(IHostingEnvironment hostingEnvironment, IApprovalService approvalService, IPersonalInfoService personalInfoService)
        {
            this.approvalService = approvalService;
            this.personalInfoService = personalInfoService;
        }
      
        public async Task<IActionResult> Index()
        {
            ApprovalViewModel model = new ApprovalViewModel
            {
                approvalMasters = await approvalService.GetAllApprovalMaster(),
                approvalTypes = await approvalService.GetAllApprovalType(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        // POST: Approval
        [HttpPost]
        public async Task<JsonResult> Index([FromForm] ApprovalViewModel model)
        {
            ApprovalMaster master = new ApprovalMaster
            {
                Id = Convert.ToInt32(model.approvalMasterId),
                employeeInfoId = model.employeeInfoId,
                approvalTypeId = model.approvalTypeId,
               
            };
            int MasterId = await approvalService.SaveApprovalMaster(master);
            
            if (model.approvalMasterId > 0)
            {
                await approvalService.DeleteapprovalDetailsByApprovalMasterId(Convert.ToInt32(MasterId));
            }
            if (model.approverId != null)
            {
                if (model.approverId.Length > 0)
                {

                    for (int i = 0; i < model.approverId.Length; i++)
                    {
                        ApprovalDetail detail = new ApprovalDetail();

                        detail.Id = 0;
                        detail.approvalMasterId = MasterId;
                        detail.approverId = Convert.ToInt32(model.approverId[i]);
                        detail.sortOrder = model.sortOrder[i];
                        detail.status = model.status[i];

                        await approvalService.SaveApprovalDetail(detail);
                        detail = new ApprovalDetail();
                    }

                }
            }

            return Json(MasterId);
        }

        [HttpGet]
        public async Task<IActionResult> GetApprovalMasterByApprovalMasterId(int ApprovalMasterId)
        {
            return Json(await approvalService.GetApprovalMasterByApprovalMasterId(ApprovalMasterId));
        }
        [HttpGet]
        public async Task<IActionResult> GetApprovalDetailByApprovalMasterId(int ApprovalMasterId)
        {
            return Json(await approvalService.GetApprovalDetailByApprovalMasterId(ApprovalMasterId));
        }
        public async Task<IActionResult> ApprovalType()
        {
            ApprovalTypeViewModel model = new ApprovalTypeViewModel
            {
                approvalTypes = await approvalService.GetAllApprovalType(),
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApprovalType([FromForm] ApprovalTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.approvalTypes = await approvalService.GetAllApprovalType();
                return View(model);
            }

            ApprovalType data = new ApprovalType
            {
                Id = (int)model.approvalTypeId,
                approvalTypeName = model.approvalTypeName
            };

            await approvalService.SaveApprovalType(data);

            return RedirectToAction(nameof(ApprovalType));
        }
    }
}