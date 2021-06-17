using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Area("HRPMSEmployee")]
    public class EmployeeAttachmentController : Controller
    {
        private readonly IHRPMSAttachmentService hRPMSAttachmentService;
        private readonly IAttachmentCategoryService attachmentCategoryService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;

        public EmployeeAttachmentController(IHRPMSAttachmentService hRPMSAttachmentService, IPhotographService photographService, IAttachmentCategoryService attachmentCategoryService, IPersonalInfoService personalInfoService)
        {
            this.hRPMSAttachmentService = hRPMSAttachmentService;
            this.attachmentCategoryService = attachmentCategoryService;
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
        }


        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            HRPMSAttachmentViewModel model = new HRPMSAttachmentViewModel
            {
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                atttachmentCategory = await attachmentCategoryService.GetAllAttachmentCategory(),
                hRPMSAttachments = await hRPMSAttachmentService.GetHRPMSAttachmentByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                atttachmentGroups= await attachmentCategoryService.GetAllAtttachmentGroup()
            };
            return View(model);
        }

        // POST: Language/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] HRPMSAttachmentViewModel  model)
        {
            if (!ModelState.IsValid)
            {
                model.atttachmentCategory = await attachmentCategoryService.GetAllAttachmentCategory();
                model.hRPMSAttachments = await hRPMSAttachmentService.GetHRPMSAttachmentByEmpId(model.employeeId);
                model.atttachmentGroups = await attachmentCategoryService.GetAllAtttachmentGroup();
                return View(model);
            }

            string fileName = String.Empty;
            string fileNameMain = String.Empty;
            string message = FileSave.SaveEmpAttachment(out fileName, model.fileUrl);

            if (message == "success")
            {
                fileNameMain = fileName;
            }

            HRPMSAttachment data = new HRPMSAttachment
            {
                employeeId = model.employeeId,
                atttachmentCategoryId = model.atttachmentCategoryId,
                fileTitle = model.fileTitle,
                fileUrl = fileNameMain,
                remarks = model.remarks,
                attachmentDate=model.attachmentDate

            };

            await hRPMSAttachmentService.SaveHRPMSAttachment(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await hRPMSAttachmentService.DeleteHRPMSAttachmentById(id);
            return RedirectToAction("Index", "EmployeeAttachment", new
            {
                id = empId
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAttachmentCategoryByGroupId(int masterId)
        {
            return Json(await attachmentCategoryService.GetAllAttachmentCategoryByGroupId(masterId));
        }
    }
}