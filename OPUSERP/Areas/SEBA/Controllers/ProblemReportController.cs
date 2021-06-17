using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SEBA.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.SEBA.Data.Entity;
using OPUSERP.SEBA.Services.Interfaces;
using OPUSERP.TAMS.Data.Entity;
using OPUSERP.TAMS.Service.Interface;

namespace OPUSERP.Areas.SEBA.Controllers
{
    [Authorize]
    [Area("SEBA")]
    public class ProblemReportController : Controller
    {
        private readonly ISebaService sebaService;
        public readonly IERPModuleService iERPModuleService;
        public readonly ITaskManagementService taskManagementService;
        private readonly IActivityStatusService activityStatusService;
        private readonly IActivitySessionService activitySessionService;
        private readonly IActivityTypeService activityTypeService;
        private readonly IActivityCategoryService activityCategoryService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IUserInfoes userInfo;
        public readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProblemReportController(ISebaService sebaService, IERPModuleService iERPModuleService, ITaskManagementService taskManagementService, IActivitySessionService activitySessionService, IActivityTypeService activityTypeService, IActivityCategoryService activityCategoryService, IActivityStatusService activityStatusService, IUserInfoes userInfo, IPersonalInfoService personalInfoService, IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService)
        {
            this.sebaService = sebaService;
            this.iERPModuleService = iERPModuleService;
            this.taskManagementService = taskManagementService;
            this.activityCategoryService = activityCategoryService;            
            this.activityStatusService = activityStatusService;
            this.activitySessionService = activitySessionService;
            this.activityTypeService = activityTypeService;
            this.personalInfoService = personalInfoService;
            this.userInfo = userInfo;
            this.attachmentCommentService = attachmentCommentService;
            this._hostingEnvironment = hostingEnvironment;
        }

        #region Customer Product

        public async Task<IActionResult> CustomerProduct()
        {
            CustomerProductWarrantyViewModel model = new CustomerProductWarrantyViewModel
            {
                customerProductWarranties = await sebaService.GetAllCustomerProductWarranty()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CustomerProduct([FromForm] CustomerProductWarrantyViewModel model)
        {
            CustomerProductWarranty data = new CustomerProductWarranty
            {
                Id = model.warrantyId,
                leadsId = model.leadsId,
                itemSpecificationId = model.itemSpecificationId,
                salesDate = model.salesDate,
                warrantyStartDate = model.warrantyStartDate,
                warrantyEndDate = model.warrantyEndDate,
                warrantyDescription = model.warrantyDescription,
                invoiceNo = model.invoiceNo,
                serialNo = model.serialNo,
                modelNo = model.modelNo

            };

            await sebaService.SaveCustomerProductWarranty(data);
            return RedirectToAction(nameof(CustomerProduct));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteCustomerProductWarrantyById(int Id)
        {
            await sebaService.DeleteCustomerProductWarrantyById(Id);
            return Json(true);
        }

        #endregion

        #region Issue Problem Report

        public IActionResult Index(int? Id, int? leadId)
        {
            ProblemMasterViewModel model = new ProblemMasterViewModel
            {

            };
            ViewBag.problemId = Id;
            ViewBag.leadId = leadId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ProblemMasterViewModel model)
        {
            var problemMasterId = 0;

            if (model.headIdAll == null)
            {
                ModelState.AddModelError(string.Empty, "Have to Add minimum 1 Head Disburse");
                model.problemMasterId = 0;
                return View(model);
            }

            ProblemMaster data = new ProblemMaster
            {
                Id = model.problemMasterId,                
                tokenNo = model.tokenNo,
                priority = model.priority,
                targetDate = model.targetDate
            };

            problemMasterId = await sebaService.SaveProblemMaster(data);

            if (model.problemMasterId > 0)
            {
                await sebaService.DeleteProblemDetailById(Convert.ToInt32(model.problemMasterId));
            }
            for (int i = 0; i < model.headIdAll.Length; i++)
            {
                ProblemDetail details = new ProblemDetail
                {
                    Id = 0,
                    problemMasterId = problemMasterId,
                    customerProductWarrantyId = model.headIdAll[i],
                    problemTitle = model.problemTitleAll[i],
                    description = model.descriptionAll[i]
                };
                await sebaService.SaveProblemDetail(details);
            }

            #region Attachment

            var module = await iERPModuleService.GetERPModuleByModuleName("SEBA");
            int moduleId = module.Id;

            if (model.imagePath_Challan != null)
            {

                string userName = User.Identity.Name;
                string documentType = "photo";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Challan.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = problemMasterId + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await model.imagePath_Challan.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataF = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "Seba Problem",
                    actionTypeId = problemMasterId,
                    subject = "",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "",
                    documentType = documentType,
                    moduleId = moduleId,
                    createdBy = userName
                };
                await attachmentCommentService.SaveDocumentAttachment(dataF);

            }
           
            #endregion


            return RedirectToAction(nameof(ProblemList));
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerProductListByLeadId(int leadId)
        {
            return Json(await sebaService.GetCustomerProductListByLeadId(leadId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProblemMasterById(int id)
        {
            return Json(await sebaService.GetProblemMasterById(id));
        }

        [HttpGet]
        public async Task<IActionResult> GetProblemphotoByProblemId(int Id)
        {
            var module = await iERPModuleService.GetERPModuleByModuleName("SEBA");
            int moduleId = module.Id;

            return Json(await attachmentCommentService.GetDocumentAttachmentByActionIdContact(Id, "Seba Problem", "photo", moduleId));
        }

       
        #endregion

        #region Problem List

        public IActionResult ProblemList()
        {
            ProblemMasterViewModel model = new ProblemMasterViewModel
            {
                //patientInfos = await homeCareService.GetAllPatientInfo()
            };
            return View(model);

        }

        [HttpGet]
        public async Task<IActionResult> GetProblemsList()
        {
            return Json(await sebaService.GetProblemsList());
        }

        #endregion

        #region Task Assign

        public async Task<IActionResult> TaskAssign(int? Id, int? problemMasterId, int? leadId)
        {
            TaskInformationViewModel model = new TaskInformationViewModel
            {
                taskProjects = await taskManagementService.GetAllTaskProject(),
                activityCategories = await activityCategoryService.GetAllActivityCategory(),
                activitySessions = await activitySessionService.GetAllActivitySession(),
                activityTypes = await activityTypeService.GetAllActivityType(),               
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
            };
            ViewBag.problemId = problemMasterId;
            ViewBag.taskId = Id;
            ViewBag.leadId = leadId;
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaskAssign([FromForm] TaskInformationViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            TaskInformation data = new TaskInformation
            {
                Id = model.taskInformationId,
                problemMasterId=model.problemMasterId,
                name = model.name,
                description = model.description,
                employeeId = empId,
                employeeAssignId = model.employeeAssignId,
                satrtDate = model.satrtDate,
                endDate = model.endDate,
                estimatedHours = model.estimatedHours,
                taskSectionId = model.taskSectionId,
                activityStatusId = model.activityStatusId,
                activityCategoryId = model.activityCategoryId,
                activitySessionId = model.activitySessionId,
                activityTypeId = model.activityTypeId

            };

            await taskManagementService.SaveTaskInformation(data);
            return RedirectToAction(nameof(TaskAssignList));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTaskSectionByProjectId(int masterId)
        {
            return Json(await taskManagementService.GetAllTaskSectionByProjectId(masterId));
        }

        

        #endregion

        #region Problem List

        public IActionResult TaskAssignList()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskInformationList()
        {
            return Json(await taskManagementService.GetTaskInformationList());
        }

        [HttpGet]
        public async Task<IActionResult> GetTaskInformationById(int id)
        {
            return Json(await taskManagementService.GetTaskInformationById(id));
        }

        #endregion


        #region API

        [Route("global/api/ProblemReportController/GetProblemDetailByMasterId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetProblemDetailByMasterId(int id)
        {
            return Json(await sebaService.GetProblemDetailByMasterId(id));
        }

        #endregion
    }
}