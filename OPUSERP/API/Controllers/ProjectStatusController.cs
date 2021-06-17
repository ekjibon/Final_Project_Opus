using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.API.Models;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.ProjectStatus.Interface;
using ZXing.QrCode.Internal;

namespace OPUSERP.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectStatusController : ControllerBase
    {
        private readonly IProjectStatusService projectStatusService;
        private readonly IItemsService itemsService;
        private readonly IUserInfoes userInfoes;

        public ProjectStatusController(IProjectStatusService projectStatusService, IItemsService itemsService, IUserInfoes userInfoes)
        {
            this.projectStatusService = projectStatusService;
            this.itemsService = itemsService;
            this.userInfoes = userInfoes;
        }
        
        //POST: api/ProjectStatus/SaveDailyProjectStatus
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> SaveDailyProjectStatus(ProjectStatusModel model)
        {
            string returnResult = "success";
            string imagePath = string.Empty;
            try
            {
                var userInfo = await userInfoes.GetUserInfoByUser(model.userName);
                int totalCnt =await projectStatusService.GetTotalReport((int)userInfo.projId);
                DailyProgressReport dailyProgress = new DailyProgressReport
                {
                    ApplicationUserId = userInfo.aspnetId,
                    projectId = userInfo.projId,
                    remarks = "test",//model.remarks,
                    siteCondition = model.siteCondition,
                    siteWeather = model.siteWeather,
                    reportDate = DateTime.Now,
                    reportNo = "DPS/" + DateTime.Now.Month + "/" + DateTime.Now.Day+"/"+userInfo.projId+"/"+totalCnt,
                    statusId = 1
                };
                int progressId = await projectStatusService.SaveDailyProgressReport(dailyProgress);

                if (model.workProgressActivityDescriptions?.Count() > 0)
                {
                    List<WorkProgressActivityDescription> lstProgressActivity = new List<WorkProgressActivityDescription>();
                    foreach (var data in model.workProgressActivityDescriptions)
                    {
                        WorkProgressActivityDescription workProgress = new WorkProgressActivityDescription
                        {
                            progressReportId = progressId,
                            locationName = data.locationName,
                            gridName=data.gridName,
                            projectLoacationId=data.projectConstructionLocationId,
                            projectGridLocationId=data.projectGridLocationId,
                            forDayPlanned = data.forDayPlanned,
                            forDayAchived = data.forDayAchived,
                            totalQty = data.totalQty,
                            nextDayPlanned = data.nextDayPlanned,
                            unit = data.unitName,
                            progressActivityTypeId=data.progressActivityTypeId
                        };
                        int jobprogressID= await projectStatusService.SaveWorkProgressActivityDescriptionSingle(workProgress);
                        if (data.activityWiseDailyProgressDetails.Count() > 0)
                        {
                            List<ActivityWiseDailyProgressDetail> lstProgressItemDetails = new List<ActivityWiseDailyProgressDetail>();
                            foreach (var item in data.activityWiseDailyProgressDetails)
                            {
                                ActivityWiseDailyProgressDetail dailyProgressDetail = new ActivityWiseDailyProgressDetail
                                {
                                    workProgressActivityId= jobprogressID,
                                    itemDetialId =item.itemDetialId,
                                    materialConsumtion=item.materialConsumtion,
                                    materialReceived=item.materialReceived
                                };
                                lstProgressItemDetails.Add(dailyProgressDetail);
                            }
                            await projectStatusService.SaveActivityProgressItemDetails(lstProgressItemDetails);
                        }
                        
                    }

                }

                if (model.siteEquipmentVM?.Count() > 0)
                {
                    List<SiteEquipment> lstEquipment = new List<SiteEquipment>();
                    foreach (var data in model.siteEquipmentVM)
                    {
                        SiteEquipment siteEquipment = new SiteEquipment
                        {
                            categoryName = data.categoryName,
                            equipmentHours = data.equipmentHours,
                            equipmentNo = data.equipmentNo,
                            progressReportId = progressId,
                            itemSpecificationId = data.itemSpecificationId,
                            status=data.status
                        };
                        lstEquipment.Add(siteEquipment);
                    }
                    await projectStatusService.SaveSiteEquipment(lstEquipment);
                }

                if (model.workProgressActivityDescriptions?.Count() > 0)
                {
                    List<SiteManpower> lstManpower = new List<SiteManpower>();
                    foreach (var data in model.workProgressActivityDescriptions)
                    {
                        SiteManpower siteManpower = new SiteManpower
                        {
                            actual = data.actual,
                            description = data.description,
                            name = data.name,
                            unitName = data.unitName,
                            progressReportId = progressId,
                            planned = data.planned,

                        };
                        lstManpower.Add(siteManpower);
                    }
                    await projectStatusService.SaveSiteManpower(lstManpower);
                }

                if (model.workProgressActivityDescriptions?.Count() > 0)
                {
                    List<SiteMaterial> lstMaterial = new List<SiteMaterial>();
                    foreach (var data in model.workProgressActivityDescriptions)
                    {
                        SiteMaterial siteMaterial = new SiteMaterial
                        {
                            forDay = data.forDay,
                            progressReportId = progressId,
                            itemSpecificationId = data.itemSpecificationId,
                            tillDate = data.tillDate,
                            
                        };
                        lstMaterial.Add(siteMaterial);
                    }
                    await projectStatusService.SaveSiteMaterial(lstMaterial);
                }

                if (model.siteVisitorsVM?.Count() > 0)
                {
                    List<SiteVisitor> lstVisitor = new List<SiteVisitor>();
                    foreach (var data in model.siteVisitorsVM)
                    {
                        SiteVisitor siteVisitor = new SiteVisitor
                        {
                            progressReportId = progressId,
                            visitorName = data.visitorName,
                            visitorOrganization = data.visitorOrganization,

                        };
                        lstVisitor.Add(siteVisitor);

                    }
                    await projectStatusService.SaveSiteVisitor(lstVisitor);
                }
                

                if (model.siteConstraintsVM?.Count() > 0)
                {
                    List<SiteConstraint> lstConstraint = new List<SiteConstraint>();
                    foreach (var data in model.siteConstraintsVM)
                    {
                        SiteConstraint siteConstraint = new SiteConstraint
                        {
                            name = data.name,
                            progressReportId = progressId,
                            statusId = 1
                        };
                        lstConstraint.Add(siteConstraint);
                    }
                    await projectStatusService.SaveSiteConstraints(lstConstraint);
                }

                return Ok(progressId.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //POST: api/ProjectStatus/UploadMultipleImage
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> UploadMultipleImage([FromForm]IFormFile[] formFile,int id)
        {
            try
            {
                
                var siteVisitors = await projectStatusService.GetSiteVisitorByProgressId(id);
                string returnResult = "success";
                string imagePath = string.Empty;
                //string totalImage = "";
                if (formFile.Length>0 )
                {
                    for (int i = 0; i < formFile.Length; i++)
                    {
                        //totalImage += formFile[i].FileName + ", ";
                        string fileName;
                        string message = FileSave.SaveImageWithLoacation(out fileName, "Upload/VisitorImage", formFile[i]);

                        if (message == "success")
                        {
                            imagePath = fileName;
                        }
                        projectStatusService.UpdateVisitorById(siteVisitors.ToArray()[i].Id,imagePath);
                    }
                }
                
                return Ok(returnResult);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public class MultipleImageInfo
        {
            public IFormFile formFile { get; set; }
            public int? id { get; set; }
        }

        // GET: api/ProjectStatus/GetProjectConstructionLocations/{projectId}/{userName}
        [HttpGet("{projectId}/{userName}")]
        public async Task<IEnumerable<ProjectConstructionLocation>> GetProjectConstructionLocations(int projectId,string userName)
        {
            var projectLocation = await projectStatusService.GetAllProjectLocationByProjectId(projectId,userName);
            return projectLocation;
        }

        //GET: api/ProjectStatus/GetGridLocationByLevelId/{levelId}
        [HttpGet("{levelId}")]
        [AllowAnonymous]
        public async Task<IEnumerable<ProjectGridLocation>> GetGridLocationByLevelId(int levelId)
        {
            var result = await projectStatusService.GetAllProjectGridLocationsByProjectId(levelId);
            return result;
        }

        //GET: api/ProjectStatus/GetProjectGridActivityByGridId/{gridId}
        [HttpGet("{gridId}")]
        [AllowAnonymous]
        public async Task<IEnumerable<ProjectLocationActivityDetails>> GetProjectGridActivityByGridId(int gridId)
        {
            var result = await projectStatusService.GetAllProjectLocationActivityBygridId(gridId);
            return result;
        }

        // GET: api/ProjectStatus/GetItemSpecifications
        [HttpGet]
        public async Task<IEnumerable<ItemSpecification>> GetItemSpecifications()
        {
            var itemSpecification = await itemsService.GetAllItemSpecification();
            return itemSpecification;
        }

        // GET: api/ProjectStatus/GetItemSpecificationsByProject/{projectId}
        [HttpGet]
        public async Task<IEnumerable<ItemSpecification>> GetItemSpecificationsByProject(int projectId)
        {
            var itemSpecification = await itemsService.GetAllItemSpecificationByProjectId(projectId);
            return itemSpecification;
        }

        // GET: api/ProjectStatus/GetWorkProgressActivityTypes
        [HttpGet]
        public async Task<IEnumerable<WorkProgressActivityType>> GetWorkProgressActivityTypes()
        {
            var activityType = await projectStatusService.GetWorkProgressActivityType();
            return activityType;
        }

        // GET: api/ProjectStatus/GetAllUnit
        [HttpGet]
        public async Task<IEnumerable<Unit>> GetAllUnit()
        {
            var units = await itemsService.GetAllUnit();
            return units;
        }

        // GET: api/ProjectStatus/GetActivityWiseMaterialByActivityId/{activityId}/{locationId}
        [HttpGet("{activityId}/{locationId}")]
        public async Task<IEnumerable<ActivityWiseMaterialModel>> GetActivityWiseMaterialByActivityId(int activityId, int locationId)
        {
            var activityType = await projectStatusService.GetActivityWiseItemDetialByActivityId(activityId, locationId);
            return activityType;
        }

        [HttpGet("{locationId}")]
        [AllowAnonymous]
        public async Task<IEnumerable<ProjectLocationActivityDetails>> GetActivityTypeByLocation(int locationId)
        {
            var result = await projectStatusService.GetAllProjectLocationActivityByLocationId(locationId);
            return result;
        }

        
    }
}
