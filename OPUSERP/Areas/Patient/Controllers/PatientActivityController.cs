using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Patient.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPService.MasterData.Interfaces;
using OPUSERP.Patient.Services.Interfaces;
using OPUSERP.TAMS.Data.Entity;
using OPUSERP.TAMS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Patient.Controllers
{
    [Authorize]
    [Area("Patient")]
    public class PatientActivityController : Controller
    {
        private readonly IHomeCareService homeCareService;
        private readonly IUserInfoes userInfo;
        private readonly ITeamService teamService;
        private readonly IActivityMasterService activityMasterService;
        private readonly IContactsService contactsService;
        private readonly IActivityCategoryService activityCategoryService;
        private readonly IActivityDetailsService activityDetailsService;
        private readonly IActivityTeamService activityTeamService;
        private readonly IActivityStatusService activityStatusService;
        private readonly IActivitySessionService activitySessionService;
        private readonly IActivityTypeService activityTypeService;
        private readonly ITaskManagementService taskManagementService;
        public readonly IActivityStatusProgressService activityStatusProgressService;
        private readonly ILeadsService leadsService;

        public PatientActivityController(IHomeCareService homeCareService, IUserInfoes userInfo, ITeamService teamService, IActivityMasterService activityMasterService, IContactsService contactsService, IActivityCategoryService activityCategoryService, IActivityDetailsService activityDetailsService, IActivityTeamService activityTeamService, IActivityStatusService activityStatusService, IActivitySessionService activitySessionService, IActivityTypeService activityTypeService, ITaskManagementService taskManagementService, IActivityStatusProgressService activityStatusProgressService, ILeadsService leadsService)
        {
            this.homeCareService = homeCareService;
            this.userInfo = userInfo;
            this.teamService = teamService;
            this.activityMasterService = activityMasterService;
            this.contactsService = contactsService;
            this.activityCategoryService = activityCategoryService;
            this.activityDetailsService = activityDetailsService;
            this.activityTeamService = activityTeamService;
            this.activityStatusService = activityStatusService;
            this.activitySessionService = activitySessionService;
            this.activityTypeService = activityTypeService;
            this.taskManagementService = taskManagementService;
            this.activityStatusProgressService = activityStatusProgressService;
            this.leadsService = leadsService;
        }

        #region Patient Activity Master

        public async Task<IActionResult> ActivityMaster(int Id, string leadName, int? masterId)
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = new Team();
            if (userinfo != null)
            {

                team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            }

            var contacts = await contactsService.GetAllContactsbyLeadId(Id);
            var master = new ActivityMaster();
            if (masterId != null)
            {
                master = await activityMasterService.GetActivityMasterById((int)masterId);
            }

            if (contacts == null)
            {
                contacts = new List<Contacts>();
            }
            int teamId = 0;
            if (team != null)
            {
                teamId = team.Id;
            }


            PatientActivityMasterViewModel model = new PatientActivityMasterViewModel()
            {
                activityMasters = await activityMasterService.GetActivityMasterByLeadId(Id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                contacts = contacts,
                teams = await teamService.GetTeamByParrentId(teamId),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                activityMasterCViewModels = await activityMasterService.GetActivityMasterCByLeadId(Id),
                activityMaster = master,
                taskSections = await taskManagementService.GetAllTaskSectionByProjectId(1),


            };
            ViewBag.leadName = leadName;
            ViewBag.patientId = Id;
            if (model.activityMaster == null)
            {
                model.activityMaster.isreminder = 0;
                model.activityMaster.activityTypeId = 0;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityMaster([FromForm] PatientActivityMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activityMasters = await activityMasterService.GetAllActivityMaster();
                return View(model);
            }

            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);

            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            if (team == null)
            {
                team = new Team();
            }
            ActivityMaster data = new ActivityMaster
            {
                Id = model.Id,
                leadsId = Convert.ToInt32(model.leadsID),
                subject = model.subject,
                taskOwner = HttpContext.User.Identity.Name,
                ownerType = model.assignTo,
                activityCategoryId = Convert.ToInt32(model.category),
                priority = model.priority,
                description = model.description,
                activityTypeId = model.typeId,
                activityMasterId = model.parentID,
                activitySessionId = model.session,
                activityStatusId = model.activityStatusId,
                activitiesDate = model.activitiesDate,
                isreminder = model.isreminder,
                reminderTime = model.reminderTime,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            int masterId = await activityMasterService.SaveActivityMaster(data);
            TaskInformation datat = new TaskInformation
            {
                Id = model.Id,
                leadsId = Convert.ToInt32(model.leadsID),


                activityCategoryId = Convert.ToInt32(model.category),

                description = model.description,
                activityTypeId = model.typeId,

                activitySessionId = model.session,
                activityStatusId = model.activityStatusId,
                satrtDate = model.activitiesDate,


                taskSectionId = model.tasksectionId,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };
            int masterIdt = await taskManagementService.SaveTaskInformation(datat);
            ActivityStatusProgress dataa = new ActivityStatusProgress
            {
                activityMasterId = masterId,
                activityStatusId = model.activityStatusId,
                remarks = model.statusremarks,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now

            };
            await activityStatusProgressService.SaveActivityStatusProgress(dataa);

            if (model.contactlist != null)
            {
                await activityDetailsService.DeleteActivityDetailsByMasterId(masterId);
                if (model.contactlist.Count() > 0)
                {

                    for (int i = 0; i < model.contactlist.Count(); i++)
                    {
                        ActivityDetails datadd = new ActivityDetails
                        {
                            Id = 0,
                            activityMasterId = masterId,
                            contactsId = (int)model.contactlist[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await activityDetailsService.SaveActivityDetails(datadd);
                    }

                }
            }
            if (model.teamlist != null && model.assignTo == "TeamMember")
            {
                await activityTeamService.DeleteActivityTeamByMasterId(masterId);
                if (model.teamlist.Count() > 0)
                {

                    for (int i = 0; i < model.teamlist.Count(); i++)
                    {
                        ActivityTeam dataddt = new ActivityTeam
                        {
                            Id = 0,
                            activityMasterId = masterId,
                            teamId = (int)model.teamlist[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await activityTeamService.SaveActivityTeam(dataddt);
                    }

                }
            }
            else
            {
                await activityTeamService.DeleteActivityTeamByMasterId(masterId);

                ActivityTeam dataddt = new ActivityTeam
                {
                    Id = 0,
                    activityMasterId = masterId,
                    teamId = team.Id,
                    createdBy = HttpContext.User.Identity.Name,
                    createdAt = DateTime.Now
                };

                await activityTeamService.SaveActivityTeam(dataddt);
            }

            #region Save History
            string actDetailss = string.Empty;
            if (model.Id == 0)
            {
                actDetailss = "Activity is Created.";
            }
            else
            {
                actDetailss = "Activity is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(model.leadsID),
                actionName = "Activity",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return RedirectToAction(nameof(ActivityMasterView), new
            {
                Id = masterId
            });
        }


        #endregion

        #region Activity Master Views

        public async Task<IActionResult> ActivityMasterView(int id)
        {
            string username = HttpContext.User.Identity.Name;
            ViewBag.Id = id;
            var activitymaster = await activityMasterService.GetActivityMasterById(id);
            PatientActivityMasterViewModel model = new PatientActivityMasterViewModel()
            {

                activityMasterCAViewModels = await activityMasterService.GetActivityMasterByparentId(id),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                activityStatusProgresses = await activityStatusProgressService.GetAllActivityStatusProgressByLeadId((int)activitymaster.leadsId)

            };

            ViewBag.patientId = model.activityMasterCAViewModels.FirstOrDefault().activityMasters.leads.Id;
            ViewBag.leadName = model.activityMasterCAViewModels.FirstOrDefault().activityMasters.leads.leadName;

            return View(model);
        }

        public async Task<IActionResult> DeleteActivityById(int id)
        {
            var data = await activityMasterService.GetActivityMasterById(id);

            await activityDetailsService.DeleteActivityDetailsByMasterId(id);
            await activityTeamService.DeleteActivityTeamByMasterId(id);
            await activityStatusProgressService.DeleteActivityStatusProgressByMasterId(id);
            await activityMasterService.DeleteActivityMasterById(id);

            return RedirectToAction("ActivityMaster", "PatientActivity", new { id = data.leadsId, leadName = data.leads.leadName, Area = "Patient" });
        }

        #endregion

        #region Activity master Follow up

        public async Task<IActionResult> ActivityMasterF(int Id, string leadName, int? masterId)
        {
            ViewBag.masterId = masterId;
            masterId = null;
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            var contacts = await contactsService.GetAllContactsbyLeadId(Id);
            var master = new ActivityMaster();
            if (masterId != null)
            {
                master = await activityMasterService.GetActivityMasterById((int)masterId);
            }

            if (contacts == null)
            {
                contacts = new List<Contacts>();
            }
            int teamId = 0;
            if (team != null)
            {
                teamId = team.Id;
            }


            PatientActivityMasterViewModel model = new PatientActivityMasterViewModel()
            {
                activityMasters = await activityMasterService.GetActivityMasterByLeadId(Id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                contacts = contacts,
                teams = await teamService.GetTeamByParrentId(teamId),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                activityMasterCViewModels = await activityMasterService.GetActivityMasterCByLeadId(Id),
                activityMaster = master

            };
            ViewBag.leadName = leadName;
            ViewBag.patientId = Id;
            if (model.activityMaster == null)
            {
                model.activityMaster.isreminder = 0;
                model.activityMaster.activityTypeId = 0;
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityMasterF([FromForm] PatientActivityMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activityMasters = await activityMasterService.GetAllActivityMaster();
                return View(model);
            }

            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            ActivityMaster data = new ActivityMaster
            {
                Id = model.Id,
                leadsId = Convert.ToInt32(model.leadsID),
                subject = model.subject,
                taskOwner = HttpContext.User.Identity.Name,
                ownerType = model.assignTo,
                activityCategoryId = Convert.ToInt32(model.category),
                priority = model.priority,
                description = model.description,
                activityTypeId = model.typeId,
                activityMasterId = model.parentID,
                activitySessionId = model.session,
                activityStatusId = model.activityStatusId,
                activitiesDate = model.activitiesDate,
                isreminder = model.isreminder,
                reminderTime = model.reminderTime,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            int masterId = await activityMasterService.SaveActivityMaster(data);
            ActivityStatusProgress dataa = new ActivityStatusProgress
            {
                activityMasterId = masterId,
                activityStatusId = model.activityStatusId,
                remarks = model.statusremarks,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now

            };
            await activityStatusProgressService.SaveActivityStatusProgress(dataa);
            if (model.contactlist != null)
            {
                await activityDetailsService.DeleteActivityDetailsByMasterId(masterId);
                if (model.contactlist.Count() > 0)
                {

                    for (int i = 0; i < model.contactlist.Count(); i++)
                    {
                        ActivityDetails datadd = new ActivityDetails
                        {
                            Id = 0,
                            activityMasterId = masterId,
                            contactsId = (int)model.contactlist[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await activityDetailsService.SaveActivityDetails(datadd);
                    }

                }
            }
            if (model.teamlist != null && model.assignTo == "TeamMember")
            {
                await activityTeamService.DeleteActivityTeamByMasterId(masterId);
                if (model.teamlist.Count() > 0)
                {

                    for (int i = 0; i < model.teamlist.Count(); i++)
                    {
                        ActivityTeam dataddt = new ActivityTeam
                        {
                            Id = 0,
                            activityMasterId = masterId,
                            teamId = (int)model.teamlist[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await activityTeamService.SaveActivityTeam(dataddt);
                    }

                }
            }
            else
            {
                await activityTeamService.DeleteActivityTeamByMasterId(masterId);



                ActivityTeam dataddt = new ActivityTeam
                {
                    Id = 0,
                    activityMasterId = masterId,
                    teamId = team.Id,
                    createdBy = HttpContext.User.Identity.Name,
                    createdAt = DateTime.Now
                };

                await activityTeamService.SaveActivityTeam(dataddt);



            }

            #region Save History
            string actDetailss = string.Empty;
            if (model.Id == 0)
            {
                actDetailss = "Activity is Created.";
            }
            else
            {
                actDetailss = "Activity is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(model.leadsID),
                actionName = "Activity",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            //return RedirectToAction(nameof(ActivityMaster), new
            //{
            //    id = Convert.ToInt32(model.leadsID)

            //});
            return RedirectToAction(nameof(ActivityMasterView), new
            {
                // id = Convert.ToInt32(model.leadsID),

                Id = masterId

            });

        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromForm] PatientActivityMasterViewModel model)
        {
            ActivityStatusProgress data = new ActivityStatusProgress
            {
                activityMasterId = model.taskId,
                activityStatusId = model.taskStatusId,
                remarks = model.statusdescription,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now

            };
            await activityStatusProgressService.SaveActivityStatusProgress(data);
            return RedirectToAction(nameof(ActivityMasterView), new
            {
                id = Convert.ToInt32(model.taskId)

            });

        }



        #endregion

        #region All List

        public async Task<IActionResult> ActivityMasterViewAll(int id)
        {
            string username = HttpContext.User.Identity.Name;
            ViewBag.Id = id;
            PatientActivityMasterViewModel model = new PatientActivityMasterViewModel()
            {

                activityMasterCAViewModels = await activityMasterService.GetActivityMasterByleadId(id),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                activityStatusProgresses = await activityStatusProgressService.GetAllActivityStatusProgressByLeadId(id)

            };

            ViewBag.patientId = model.activityMasterCAViewModels.FirstOrDefault().activityMasters.leads.Id;
            ViewBag.leadName = model.activityMasterCAViewModels.FirstOrDefault().activityMasters.leads.leadName;

            return View(model);
        }

        #endregion


    }
}