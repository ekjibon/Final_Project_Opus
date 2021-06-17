using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.ProjectStatus.Interface;

namespace OPUSERP.Areas.SCMMasterData.Controllers
{
    [Area("SCMMasterData")]
    public class ProjectController : Controller
    {
        private readonly IProjectService projectService;
        private readonly IProjectStatusService projectStatusService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IItemsService itemsService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public ProjectController(IItemsService itemsService, IConverter converter, IHostingEnvironment hostingEnvironment, IProjectService projectService, IProjectStatusService projectStatusService, ISpecialBranchUnitService specialBranchUnitService)
        {
            this.projectService = projectService;
            this.projectStatusService = projectStatusService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.itemsService = itemsService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ProjectViewModel model = new ProjectViewModel
            {
                projects = await projectService.GetProjectList(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] ProjectViewModel model)
        {
            Project project = new Project
            {
                Id = Convert.ToInt32(model.projectId),
                projectName = model.projectName,
                projectNameBN = model.projectNameBN,
                projectShortName = model.projectShortName,
                projectLocation = model.projectLocation,
                inChargeEmpCode = model.inChargeEmpCode,
                projectStatus = model.projectStatus,
                specialBranchUnitId = model.sbuId,
                startDate = model.startDate,
                endDate = model.endDate,
                isdefault = model.isdefault
            };
            await projectService.SaveProject(project);
            return RedirectToAction(nameof(Index));
        }
        //GET: api/ProjectStatus/GetAllocatedProjectNameByUser/{userName}
        //[Route("api/Project/GetAllocatedProjectNameByUser/{userName}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllocatedProjectNameByUser(string userName)
        {
            var result = await projectStatusService.GetAllocatedProjectNameByUser(userName);
            return Json(result); ;
        }
        public async Task<IActionResult> ProjectLocation()
        {
            ProjectLocationViewModel model = new ProjectLocationViewModel
            {
                projectConstructionLocations = await projectStatusService.GetAllProjectLocation(),
                projects = await projectService.GetProjectList(),
                workProgressActivityTypes = await projectStatusService.GetWorkProgressActivityType()
            };
            return View(model);
        }

        public async Task<IActionResult> ProjectActivityByLocationList()
        {
            string userName = HttpContext.User.Identity.Name;
            ProjectLocationViewModel model = new ProjectLocationViewModel
            {
                activityWiseProjectLocations = await projectStatusService.GetAllLocationWithActivityDetails(userName),
                projects = await projectService.GetProjectList(),
                workProgressActivityTypes = await projectStatusService.GetWorkProgressActivityType(),
                units = await itemsService.GetAllUnit()
            };
            return View(model);
        }

        public async Task<IActionResult> ProjectActivityByLocation()
        {
            ProjectLocationViewModel model = new ProjectLocationViewModel
            {
                projectConstructionLocations = await projectStatusService.GetAllProjectLocation(),
                projects = await projectService.GetProjectList(),
                workProgressActivityTypes = await projectStatusService.GetWorkProgressActivityType(),
                units = await itemsService.GetAllUnit()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ProjectActivityByLocation(ProjectActivityByLocationViewModel model)
        {

            string msg = "error";
            if(model.projectId > 0 && !string.IsNullOrEmpty(model.applicationUserId) && !string.IsNullOrEmpty(model.location[0]))
            {
                for (int i = 0; i < model.levelLocation.Length; i++)
                {
                    ProjectConstructionLocation data = new ProjectConstructionLocation
                    {
                        Id = Convert.ToInt32(model.projectLocationId),
                        projectId = model.projectId,
                        locationPosition = model.levelLocation[i],
                        //floorNo = model.location[i],
                        remarks = model.remarks,
                        ApplicationUserId = model.applicationUserId
                    };

                    int masterId = await projectStatusService.SaveProjectLocation(data);

                    for (int h = 0; h < model.location.Length; h++)
                    {
                        if (model.levelIndexForGrid[h] == i)
                        {
                            ProjectGridLocation gridLocation = new ProjectGridLocation
                            {
                                projectConstructionLocationId = masterId,
                                gridLocation = model.location[h]
                            };
                            int gridId = await projectStatusService.SaveProjectGridLocation(gridLocation);

                            for (int j = 0; j < model.activity.Length; j++)
                            {
                                if (model.locationIndexForActivity[j] == h)
                                {
                                    ProjectLocationActivityDetails locationDetails = new ProjectLocationActivityDetails
                                    {
                                        projectGridLocationId = gridId,
                                        progressActivityTypeId = model.activity[j],
                                        unitId = model.activityUnit[j],
                                        qty = model.activityQty[j]
                                    };

                                    int activitydetailsid = await projectStatusService.SaveProjectLocationActivityDetails(locationDetails);

                                    for (int k = 0; k < model.item.Length; k++)
                                    {
                                        if (model.activityIndexForItem[k] == j)
                                        {
                                            ActivityWiseItemDetial activityDetail = new ActivityWiseItemDetial
                                            {
                                                activityDetailsId = activitydetailsid,
                                                itemSpecificationId = model.item[k],
                                                unitId = model.itemUnit[k],
                                                qty = model.itemQty[k]
                                            };

                                            await projectStatusService.SaveActivityItemDetails(activityDetail);
                                        }
                                    }
                                }
                            }
                        }
                        
                    }
                }
                msg = "success";
            }

            return Json(msg);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectLocation([FromForm] ProjectLocationViewModel model)
        {
            ProjectConstructionLocation data = new ProjectConstructionLocation
            {
                Id = Convert.ToInt32(model.projectLocationId),
                projectId = model.projectId,
                locationPosition = model.locationPosition,
                floorNo = model.floorNo,
                remarks = model.remarks,
                ApplicationUserId=model.applicationUserId
            };
            //await projectStatusService.SaveProjectLocation(data);
            int masterId = await projectStatusService.SaveProjectLocation(data);
            int activitydetailsid = 0;
            if (model.headlineAll != null)
            {
                for (int i = 0; i < model.headlineAll.Length; i++)
                {
                    ProjectLocationActivityDetails locationDetails = new ProjectLocationActivityDetails
                    {
                        projectGridLocationId = masterId,
                        progressActivityTypeId = model.headlineAll[i]
                    };

                    activitydetailsid = await projectStatusService.SaveProjectLocationActivityDetails(locationDetails);
                }
            }

            if (model.itemSpecificationName != null)
            {
                for (int i = 0; i < model.itemSpecificationName.Length; i++)
                {
                    ActivityWiseItemDetial activityDetail = new ActivityWiseItemDetial
                    {
                        activityDetailsId = activitydetailsid,
                        itemSpecificationId = model.itemSpecificationId[i]
                    };

                    await projectStatusService.SaveActivityItemDetails(activityDetail);
                }
            }
            
            return RedirectToAction(nameof(ProjectLocation));
        }

        public async Task<IActionResult> LocationActivity()
        {
            LocationActivityViewModel model = new LocationActivityViewModel
            {
                workProgressActivityTypes = await projectStatusService.GetWorkProgressActivityType()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LocationActivity([FromForm] LocationActivityViewModel model)
        {
            WorkProgressActivityType data = new WorkProgressActivityType
            {
                Id = Convert.ToInt32(model.activityId),
                activityName = model.activityName
            };
            await projectStatusService.SaveWorkProgressActivityType(data);
            return RedirectToAction(nameof(LocationActivity));
        }

        public JsonResult UpdateProjectStatus(int projectId,string status)
        {
            try
            {
                projectService.UpdateProjectStatusById(projectId, status);
                return Json(true);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ProjectEquipment()
        {

            ProjectEquipmentModel model = new ProjectEquipmentModel
            {
                Projects = await projectService.GetProjectList(),
                ProjectWiseEquipment= await projectStatusService.GetAllProjectEquipment()
                //itemSpecifications = await itemsService.GetAllItemSpecification()
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProjectEquipment([FromForm] ProjectEquipmentModel model)
        {
            ProjectWiseEquipment data = new ProjectWiseEquipment
            {
                Id = Convert.ToInt32(model.projectEquipmentId),
                projectId = model.projectId,
                itemSpecificationId=model.itemspecificationId
            };
            await projectStatusService.SaveSingleProjectEquipment(data);
            return RedirectToAction(nameof(ProjectEquipment));
        }

        public async Task<IActionResult> DailyProgressReportList()
        {
            string username = HttpContext.User.Identity.Name;
            ProjectEquipmentModel model = new ProjectEquipmentModel
            {
                Projects = await projectService.GetProjectList()
            };
            return View(model);
        }
        [Route("api/SCMMasterData/Project/DailyProgressReportByFiltering/{fromDate}/{toDate}/{projectId}")]
        public async Task<IActionResult> DailyProgressReportByFiltering(DateTime? fromDate,DateTime? toDate,int projectId)
        {
            var result = await projectStatusService.GetDailyProgressReportByFiltering(fromDate,toDate, projectId);
            return Json(result);
        }

        [Route("SCMMasterData/Project/ProgressReportPdf/{date}/{userName}")]
        [AllowAnonymous]
        public IActionResult ProgressReportPdf(string date, string userName)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            url = scheme + "://" + host + "/SCMMasterData/Project/ProgressReportPrintLView?date=" + date+"&userName="+userName;

            string status = myPDF.GeneratePDFProjectStatus(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgressReportPrintLView(string date, string userName)
        {
            try
            {
                var report = await projectStatusService.GetDailyProgressReportByDay(date, userName);
                IEnumerable<WorkProgressActivityDescription> progressActivityDescription = new List<WorkProgressActivityDescription>();
                IEnumerable<ActivityWiseDailyProgressDetail> activityWiseDailyProgressDetails = new List<ActivityWiseDailyProgressDetail>();
                IEnumerable<SiteVisitor> SiteVisitor = new List<SiteVisitor>();
                IEnumerable<SiteEquipment> SiteEquipment = new List<SiteEquipment>();
                IEnumerable<SiteConstraint> siteConstraints = new List<SiteConstraint>();
                IEnumerable<SiteManpower> siteManpower = new List<SiteManpower>();
                if (report != null)
                {
                    progressActivityDescription = await projectStatusService.GetWorkProgressActivityTypeByReportId(report.Id);
                    activityWiseDailyProgressDetails = await projectStatusService.GetActivityProgressItemDetailsByReportId(report.Id);
                    SiteVisitor = await projectStatusService.GetSiteVisitorByProgressId(report.Id);
                    SiteEquipment = await projectStatusService.GetSiteEquipmentByReportId(report.Id);
                    siteConstraints = await projectStatusService.GetSiteConstraintByProgressId(report.Id);
                    siteManpower = await projectStatusService.GetSiteManpowerByProgressId(report.Id);
                }
                else
                {
                    progressActivityDescription = new List<WorkProgressActivityDescription>();
                    SiteVisitor = new List<SiteVisitor>();
                    SiteEquipment = new List<SiteEquipment>();
                    siteConstraints = new List<SiteConstraint>();
                    siteManpower = new List<SiteManpower>();
                    activityWiseDailyProgressDetails = new List<ActivityWiseDailyProgressDetail>();
                }

                var model = new ProjectLocationViewModel
                {

                    dailyProgressReport = report,
                    workProgressActivityDescriptions = progressActivityDescription,
                    siteVisitors = SiteVisitor,
                    siteEquipment = SiteEquipment,
                    siteConstraints = siteConstraints,
                    siteManpowers = siteManpower,
                    activityWiseDailyProgressDetails = activityWiseDailyProgressDetails
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Route("SCMMasterData/Project/ProgressReportPdfById/{id}")]
        [AllowAnonymous]
        public IActionResult ProgressReportPdfById(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            url = scheme + "://" + host + "/SCMMasterData/Project/ProgressReportPrintViewById?id=" + id;

            string status = myPDF.GeneratePDFProjectStatus(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgressReportPrintViewById(int id)
        {
            try
            {
                var report = await projectStatusService.GetDailyProgressReportsById(id);
                IEnumerable<WorkProgressActivityDescription> progressActivityDescription = new List<WorkProgressActivityDescription>();
                IEnumerable<ActivityWiseDailyProgressDetail> activityWiseDailyProgressDetails = new List<ActivityWiseDailyProgressDetail>();
                IEnumerable<SiteVisitor> SiteVisitor = new List<SiteVisitor>();
                IEnumerable<SiteEquipment> SiteEquipment = new List<SiteEquipment>();
                IEnumerable<SiteConstraint> siteConstraints = new List<SiteConstraint>();
                IEnumerable<SiteManpower> siteManpower = new List<SiteManpower>();
                if (report != null)
                {
                    progressActivityDescription = await projectStatusService.GetWorkProgressActivityTypeByReportId(report.Id);
                    activityWiseDailyProgressDetails = await projectStatusService.GetActivityProgressItemDetailsByReportId(report.Id);
                    SiteVisitor = await projectStatusService.GetSiteVisitorByProgressId(report.Id);
                    SiteEquipment = await projectStatusService.GetSiteEquipmentByReportId(report.Id);
                    siteConstraints = await projectStatusService.GetSiteConstraintByProgressId(report.Id);
                    siteManpower = await projectStatusService.GetSiteManpowerByProgressId(report.Id);
                }
                else
                {
                    progressActivityDescription = new List<WorkProgressActivityDescription>();
                    SiteVisitor = new List<SiteVisitor>();
                    SiteEquipment = new List<SiteEquipment>();
                    siteConstraints = new List<SiteConstraint>();
                    siteManpower = new List<SiteManpower>();
                    activityWiseDailyProgressDetails = new List<ActivityWiseDailyProgressDetail>();
                }

                var model = new ProjectLocationViewModel
                {

                    dailyProgressReport = report,
                    workProgressActivityDescriptions = progressActivityDescription,
                    siteVisitors = SiteVisitor,
                    siteEquipment = SiteEquipment,
                    siteConstraints = siteConstraints,
                    siteManpowers = siteManpower,
                    activityWiseDailyProgressDetails = activityWiseDailyProgressDetails
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgressReportPrintview(string date,string userName)
        {
            try
            {
                var report = await projectStatusService.GetDailyProgressReportByDay(date, userName);
                IEnumerable<WorkProgressActivityDescription> progressActivityDescription = new List<WorkProgressActivityDescription>();
                IEnumerable<ActivityWiseDailyProgressDetail> activityWiseDailyProgressDetails = new List<ActivityWiseDailyProgressDetail>();
                IEnumerable<SiteVisitor> SiteVisitor = new List<SiteVisitor>();
                IEnumerable<SiteEquipment> SiteEquipment = new List<SiteEquipment>();
                IEnumerable<SiteConstraint> siteConstraints = new List<SiteConstraint>();
                IEnumerable<SiteManpower> siteManpower = new List<SiteManpower>();
                if (report != null)
                {
                    progressActivityDescription = await projectStatusService.GetWorkProgressActivityTypeByReportId(report.Id);
                    activityWiseDailyProgressDetails = await projectStatusService.GetActivityProgressItemDetailsByReportId(report.Id);
                    SiteVisitor = await projectStatusService.GetSiteVisitorByProgressId(report.Id);
                    SiteEquipment = await projectStatusService.GetSiteEquipmentByReportId(report.Id);
                    siteConstraints = await projectStatusService.GetSiteConstraintByProgressId(report.Id);
                    siteManpower = await projectStatusService.GetSiteManpowerByProgressId(report.Id);
                }
                else
                {
                    progressActivityDescription = new List<WorkProgressActivityDescription>();
                    SiteVisitor = new List<SiteVisitor>();
                    SiteEquipment = new List<SiteEquipment>();
                    siteConstraints = new List<SiteConstraint>();
                    siteManpower = new List<SiteManpower>();
                    activityWiseDailyProgressDetails = new List<ActivityWiseDailyProgressDetail>();
                }

                var model = new ProjectLocationViewModel
                {
                    
                    dailyProgressReport=report,
                    workProgressActivityDescriptions = progressActivityDescription,
                    siteVisitors=SiteVisitor,
                    siteEquipment=SiteEquipment,
                    siteConstraints=siteConstraints,
                    siteManpowers=siteManpower,
                    activityWiseDailyProgressDetails= activityWiseDailyProgressDetails
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        [HttpPost]
        public async Task<JsonResult> DeleteProjectById(int Id)
        {
            await projectService.DeleteProjectById(Id);
            return Json(true);
        }

        [Route("global/api/Project/GetAllUnits")]
        [HttpGet]
        public async Task<IEnumerable<Unit>> GetAllUnits()
        {
            return await itemsService.GetAllUnit();
        }
    }
}