using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.TAMS.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.TAMS.Data.Entity;
using OPUSERP.TAMS.Service.Interface;
using OPUSERP.Workflow.Service.Interface;
using System;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Controllers
{
    [Area("TAMS")]
    public class TaskManagemnetController : Controller
    {
        private readonly IUserInfoes userInfoes;
        private readonly IDocService docService;
        private readonly ITaskManagementService taskManagementService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IEmailSenderService emailSenderService;
        private readonly IERPCompanyService iERPCompanyService;
        private readonly IHostingEnvironment _hostingEnvironment;


        private readonly IActivityStatusService activityStatusService;
        private readonly IActivitySessionService activitySessionService;
        private readonly IActivityTypeService activityTypeService;
        private readonly IActivityCategoryService activityCategoryService;
        private readonly IPhotographService photographService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public TaskManagemnetController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IERPCompanyService iERPCompanyService ,IEmailSenderService emailSenderService, IActivityStatusService activityStatusService, IActivityCategoryService activityCategoryService, IActivityTypeService activityTypeService, IActivitySessionService activitySessionService, IConverter converter, IUserInfoes userInfoes, IPersonalInfoService personalInfoService, IDocService docService, ITaskManagementService taskManagementService)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.userInfoes = userInfoes;
            this.docService = docService;
            this.personalInfoService = personalInfoService;
            this.taskManagementService = taskManagementService;

            this.activityStatusService = activityStatusService;
            this.activitySessionService = activitySessionService;
            this.activityTypeService = activityTypeService;
            this.activityCategoryService = activityCategoryService;
            this.emailSenderService = emailSenderService;
            this.iERPCompanyService = iERPCompanyService;
            this.photographService = photographService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: VisitorEntry
        public async Task<IActionResult> Index()
        {
            TaskManagemnetViewModel model = new TaskManagemnetViewModel
            {
                taskProjects = await taskManagementService.GetAllTaskProject()
            };
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] TaskManagemnetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.taskProjects = await taskManagementService.GetAllTaskProject();
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            TaskProject data = new TaskProject
            {
                Id = model.projectId,
                name = model.name,
                date = DateTime.Now,
                satrtDate = model.satrtDate,
                approxEndDate = model.approxEndDate,
                shortDescription = model.shortDescription,
                description = model.description,
            };

            int lstId = await taskManagementService.SaveTaskProject(data);


            //return RedirectToAction("DocPreView", "Document", new
            //{
            //    id = lstId
            //});

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Section()
        {
            TaskManagemnetViewModel model = new TaskManagemnetViewModel
            {
                taskProjects = await taskManagementService.GetAllTaskProject(),
                taskSections = await taskManagementService.GetAllTaskSection(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Section([FromForm] TaskManagemnetViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.taskProjects = await taskManagementService.GetAllTaskProject();
                model.taskSections = await taskManagementService.GetAllTaskSection();
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            TaskSection data = new TaskSection
            {
                Id = model.projectId,
                name = model.name,
                date = DateTime.Now,
                taskProjectId = model.taskProjectId,
            };

            int lstId = await taskManagementService.SaveTaskSection(data);

            if (model.fromModal == 1)
            {
                return RedirectToAction("ProjectView", "TaskManagemnet", new
                {
                    id = model.taskProjectId
                });
            }

            return RedirectToAction(nameof(Section));
        }

        public async Task<IActionResult> ProjectView(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;

            }

            var photo = await photographService.GetPhotographByEmpIdAndType(empId, "profile");

            TaskManagemnetViewModel model = new TaskManagemnetViewModel
            {
                projectViewModel = await taskManagementService.GetAllProjectViewModel(id),
                taskSections = await taskManagementService.GetAllTaskSectionByProjectId(id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                empId = empId,
                empname = EmpInfo.nameEnglish,
                empPhoto = photo?.url,
            };
            return View(model);
        }


        public async Task<IActionResult> MyTask(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;

            }

            TaskManagemnetViewModel model = new TaskManagemnetViewModel
            {
                projectViewModel = await taskManagementService.GetAllProjectViewModel(id),
                taskSections = await taskManagementService.GetAllTaskSectionByProjectId(id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                taskProjects = await taskManagementService.GetAllTaskProject(),
                empId = empId,
                empname = EmpInfo.nameEnglish,
                taskInformationHistories = await taskManagementService.GetAllTaskProjectByEmp(empId)
            };
            return View(model);
        }

        public async Task<IActionResult> FollowUpTask(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;

            }

            TaskManagemnetViewModel model = new TaskManagemnetViewModel
            {
                projectViewModel = await taskManagementService.GetAllProjectViewModel(id),
                taskSections = await taskManagementService.GetAllTaskSectionByProjectId(id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                taskProjects = await taskManagementService.GetAllTaskProject(),
                empId = empId,
                empname = EmpInfo.nameEnglish,
                taskInformationHistories = await taskManagementService.GetAllTaskProjectByEmpFollowup(empId)
            };
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> TaskSave([FromForm] TaskSaveViewModel model)
        {

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            TaskInformation data = new TaskInformation
            {
                name = model.name,
                taskSectionId = model.taskSectionId,
                activitySessionId = model.activitySessionId,
                activityCategoryId = model.activityCategoryId,
                activityStatusId = model.activityStatusId,
                activityTypeId = model.activityTypeId,
                satrtDate = model.satrtDate,
                endDate = model.endDate,
                estimatedHours = model.estimatedHours,
                description = model.description,
                employeeId = empId,
                employeeAssignId = model.employeeAssignId,
            };

            int lstId = await taskManagementService.SaveTaskInformation(data);
            var employee = await personalInfoService.GetEmployeeInfoById((int)model.employeeAssignId);

            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string baseUrl = $"" + scheme + "://" + host + "/TAMS/TaskManagemnet/ProjectView/" + model.taskProjectId;

            string html = "<div><strong>Task Assign.</strong></div>"
                    + " <br/>"
                    + "<p>Dear Sir,</p>"
                    + " <br/>"
                    + " This is to inform you that one Task Assigned you Just Now"
                    + "<br/>"
                    + "<div><a href='" + baseUrl + "'><button>Login</button></a></div>"
                    + " <br/>"
                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > Human Resource Department.</p></div>"
                    + " <br/>";

            if (model.isreminder == 1)
            {
                if (employee.emailAddress != null)
                {
                    await emailSenderService.SendEmailWithFrom(employee.emailAddress, employee.nameEnglish, "Task Assign", html);
                }
            }

            if (model.myTask == 1)
            {
                return RedirectToAction(nameof(MyTask));
            }

            return RedirectToAction("ProjectView", "TaskManagemnet", new
            {
                id = model.taskProjectId
            });
        }

        public async Task<IActionResult> TaskProjectList()
        {
            TaskManagemnetViewModel model = new TaskManagemnetViewModel
            {
                taskProjects = await taskManagementService.GetAllTaskProject()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> Comments([FromForm] TaskCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            TaskComment data = new TaskComment
            {
                taskInformationId = model.taskIdForComment,
                comments = model.Comment,
                employeeId = empId,
            };

            int lstId = await taskManagementService.SaveTaskComment(data);

            if (model.TaskModalId == 1)
            {
                return Json(true);
            }

            if (model.projectIdForComment == 0)
            {
                return RedirectToAction(nameof(MyTask));
            }

            return RedirectToAction("ProjectView", "TaskManagemnet", new
            {
                id = model.projectIdForComment
            });
        }

        [HttpPost]
        public async Task<ActionResult> Follower([FromForm] TaskCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);
            int lstId = 0;
            if (model.ids != null)
            {
                foreach (var follower in model.ids)
                {

                    TaskFollower data = new TaskFollower
                    {
                        taskInformationId = model.taskIdForFollower,
                        employeeId = follower,
                    };

                    lstId = await taskManagementService.SaveTaskFollower(data);
                }
            }

            if (model.TaskModalId == 1)
            {
                return Json(lstId);
            }

            if (model.projectIdForFollower == 0)
            {
                return RedirectToAction(nameof(MyTask));
            }


            return RedirectToAction("ProjectView", "TaskManagemnet", new
            {
                id = model.projectIdForFollower
            });
        }

        [HttpPost]
        public async Task<ActionResult> Tags([FromForm] TaskCommentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);
            int lstId = 0;
            if (model.Tags != null)
            {
                foreach (var tag in model.Tags)
                {

                    TaskTag data = new TaskTag
                    {
                        taskInformationId = model.taskIdForTags,
                        tag = tag,
                    };

                    lstId = await taskManagementService.SaveTaskTag(data);
                }
            }

            if (model.TaskModalId == 1)
            {
                return Json(lstId);
            }

            if (model.projectIdForTags == 0)
            {
                return RedirectToAction(nameof(MyTask));
            }
            
            return RedirectToAction("ProjectView", "TaskManagemnet", new
            {
                id = model.projectIdForTags
            });
        }

        [HttpPost]
        public async Task<IActionResult> File([FromForm] TaskCommentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "NDADocument";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    TaskAttachment data = new TaskAttachment
                    {
                        url = fileLocation,
                        fileTitle = model.title,
                        taskInformationId = model.taskIdForFile
                    };
                    await taskManagementService.SaveTaskAttachment(data);
                }

                if (model.projectIdForFile == 0)
                {
                    return RedirectToAction(nameof(MyTask));
                }

                return RedirectToAction("ProjectView", "TaskManagemnet", new
                {
                    id = model.projectIdForFile
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> Subtask([FromForm] SubtaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            TaskSubtask data = new TaskSubtask
            {
                taskInformationId = model.taskIdForFollower,
                name = model.SubTaskName,
                satrtDate = model.starttimeSubtask,
                endDate = model.endtimeSubtask,
                remarks = model.remarksSubtask
            };

            int lstId = await taskManagementService.SaveTaskSubtask(data);

            return Json(lstId);
        }


        [HttpPost]
        public async Task<ActionResult> UpdateSubtask([FromForm] UpdateSubTaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);
            int status = 0;
            if (model.satusSubtask == "Completed")
            {
                status = 1;
            }

            TaskSubtask data = new TaskSubtask
            {
                Id = model.Id,
                taskInformationId = model.taskIdForFollower,
                name = model.SubTaskName,
                satrtDate = model.starttimeSubtask,
                endDate = model.endtimeSubtask,
                remarks = model.remarksSubtask,
                details = model.commentsSubtask,
                statusDetails = model.satusSubtask,
                status = status
            };

            int lstId = await taskManagementService.SaveTaskSubtask(data);

            return Json(lstId);
        }



        [HttpPost]
        public async Task<ActionResult> FollowUp([FromForm] SubtaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            TaskFollowUp data = new TaskFollowUp
            {
                taskInformationId = model.taskIdForFollower,
                name = model.followupname,
                description = model.followupRemarks,
                activityCategoryId = model.followupactivityCategoryId,
                activitySessionId = model.followupactivitySessionId,
                activityTypeId = model.followupactivityTypeId,
                activityStatusId = model.followupactivityStatusId,
                targetDate = model.targetDate,
                executionDate = model.executionDate,
                remainderStatus = model.remainderStatus,
                reminderDate = model.reminderDate
            };

            int lstId = await taskManagementService.SaveTaskFollowUp(data);

            return Json(lstId);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateFollowUp([FromForm] UpdateFollowUpViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            TaskFollowUp data = new TaskFollowUp
            {
                Id= model.Id,
                taskInformationId = model.taskIdForFollower,
                name = model.followupname,
                description = model.followupRemarks,
                activityCategoryId = model.followupactivityCategoryId,
                activitySessionId = model.followupactivitySessionId,
                activityStatusId = model.followupactivityStatusId,
                targetDate = model.targetDate,
                executionDate = model.executionDate,
                remainderStatus = model.remainderStatus,
                reminderDate = model.reminderDate,
                note =model.followupNotes,
                status = model.satusfollowup
            };

            int lstId = await taskManagementService.SaveTaskFollowUp(data);

            return Json(lstId);
        }

        [HttpPost]
        public async Task<ActionResult> TaskHours([FromForm] SubtaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            TaskHours data = new TaskHours
            {
                taskInformationId = model.taskIdForFollower,
                hours = model.TaskHourName,
                date = model.TaskHourDate,
            };

            int lstId = await taskManagementService.SaveTaskHours(data);

            return Json(lstId);
        }

        [HttpPost]
        public async Task<ActionResult> TaskStatus([FromForm] SubtaskViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfoes.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            //return Json(model);

            TaskStatusList data = new TaskStatusList
            {
                taskInformationId = model.taskIdForFollower,
                status = model.TaskStatusName,
                date = model.TaskStatusDate,
                remarks = model.TaskStatusNameremarks
            };

            int lstId = await taskManagementService.SaveTaskStatusList(data);

            return Json(lstId);
        }

        [AllowAnonymous]
        public async Task<IActionResult> TaskPriview(int id)
        {
            TaskManagemnetViewModel model = new TaskManagemnetViewModel
            {
                singleTaskViewModel = await taskManagementService.GetSingleTaskViewModelById(id),
                companies = await iERPCompanyService.GetAllCompany()
            };
            return View(model);
        }

        [AllowAnonymous]
        public IActionResult TaskPriviewPdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/TAMS/TaskManagemnet/TaskPriview?id=" + id;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public void UpdateTaskDone(int Id)
        {
            taskManagementService.UpdateSubtask(Id, 1);
        }

        public void UpdateTaskUnDone(int Id)
        {
            taskManagementService.UpdateSubtask(Id, 0);
        }

        public async Task<IActionResult> DeleteSubtask(int Id)
        {
            return Json(await taskManagementService.DeleteTaskSubtaskById(Id));
        }

        public async Task<IActionResult> DeleteTaskHour(int Id)
        {
            return Json(await taskManagementService.DeleteTaskHoursById(Id));
        }

        public async Task<IActionResult> DeleteTaskFollower(int Id)
        {
            return Json(await taskManagementService.DeleteTaskFollowerById(Id));
        }

        public async Task<IActionResult> DeleteTaskTags(int Id)
        {
            return Json(await taskManagementService.DeleteTaskTagById(Id));
        }

        public async Task<IActionResult> DeleteTaskFollowup(int Id)
        {
            return Json(await taskManagementService.DeleteTaskFollowUpById(Id));
        }

        public async Task<IActionResult> DeleteTaskStatusListById(int Id)
        {
            return Json(await taskManagementService.DeleteTaskStatusListById(Id));
        }


        [Route("global/api/GetSingleTaskViewModelById/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSingleTaskViewModelById(int Id)
        {
            return Json(await taskManagementService.GetSingleTaskViewModelById(Id));
        }

        [Route("global/api/GetTaskSubtaskById/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTaskSubtaskById(int Id)
        {
            return Json(await taskManagementService.GetTaskSubtaskById(Id));
        }

        [Route("global/api/GetTaskFollowUpById/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetTaskFollowUpById(int Id)
        {
            return Json(await taskManagementService.GetTaskFollowUpById(Id));
        }

        [Route("global/api/GetAllTaskSectionByProjectId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllTaskSectionByProjectId(int Id)
        {
            return Json(await taskManagementService.GetAllTaskSectionByProjectId(Id));
        }

    }
}