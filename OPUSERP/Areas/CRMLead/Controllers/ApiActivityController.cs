using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Cold;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Cold.Interfaces;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPService.MasterData.Interfaces;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiActivityController : Controller
    {
        private readonly IActivityMasterService activityMasterService;
        private readonly IContactsService contactsService; 
        private readonly IActivityCategoryService activityCategoryService;
        private readonly IActivityDetailsService activityDetailsService;
        private readonly IActivityTeamService activityTeamService;
        private readonly IActivityStatusService activityStatusService;
        private readonly IActivitySessionService activitySessionService;
        private readonly IActivityTypeService activityTypeService;
        private readonly ITeamService teamService;
        private readonly IUserInfoes userInfo;
        private readonly ILeadsService leadsService;
        private readonly IColdService coldService;
        public readonly IActivityStatusProgressService activityStatusProgressService;


        public ApiActivityController(IActivityMasterService activityMasterService, IActivityStatusProgressService activityStatusProgressService, IColdService coldService, IActivityTeamService activityTeamService, IUserInfoes userInfo, ITeamService teamService, IActivitySessionService activitySessionService, IActivityTypeService activityTypeService, IActivityCategoryService activityCategoryService, IContactsService contactsService, IActivityDetailsService activityDetailsService, IActivityStatusService activityStatusService, ILeadsService leadsService)
        {
            this.activityMasterService = activityMasterService;
            this.contactsService = contactsService;
            this.activityCategoryService = activityCategoryService;
            this.activityDetailsService = activityDetailsService;
            this.activityStatusService = activityStatusService;
            this.activitySessionService = activitySessionService;
            this.activityTypeService = activityTypeService;
            this.activityTeamService = activityTeamService;
            this.coldService = coldService;
            this.teamService = teamService;
            this.userInfo = userInfo;
            this.leadsService = leadsService;
            this.activityStatusProgressService = activityStatusProgressService;
        }
        
        public async Task<IActionResult> ActivityMasterView(int id)
        {
            string username = HttpContext.User.Identity.Name;
            ViewBag.Id = id;
            var activitymaster = await activityMasterService.GetActivityMasterById(id);
            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                
                activityMasterCAViewModels = await activityMasterService.GetActivityMasterByparentId(id),
                activityStatuses=await activityStatusService.GetAllActivityStatus(),
                activityStatusProgresses=await activityStatusProgressService.GetAllActivityStatusProgressByLeadId((int)activitymaster.leadsId)

            };

            ViewBag.leadId = model.activityMasterCAViewModels.FirstOrDefault().activityMasters.leads.Id;
            ViewBag.leadName = model.activityMasterCAViewModels.FirstOrDefault().activityMasters.leads.leadName;

            return View(model);
        }
        public async Task<IActionResult> ActivityMasterViewAll(int id)
        {
            string username = HttpContext.User.Identity.Name;
            ViewBag.Id = id;
          //  var activitymaster = await activityMasterService.GetActivityMasterById(id);
            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {

                activityMasterCAViewModels = await activityMasterService.GetActivityMasterByleadId(id),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                activityStatusProgresses = await activityStatusProgressService.GetAllActivityStatusProgressByLeadId(id)

            };

            ViewBag.leadId = model.activityMasterCAViewModels.FirstOrDefault().activityMasters.leads.Id;
            ViewBag.leadName = model.activityMasterCAViewModels.FirstOrDefault().activityMasters.leads.leadName;

            return View(model);
        }
        public async Task<IActionResult> ColdActivityMasterView(int id)
        {
            string username = HttpContext.User.Identity.Name;
            ViewBag.Id = id;

            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                //activityMasters = await activityMasterService.GetActivityMasterByLeadId(id),
                //ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                //ActivitySessions = await activitySessionService.GetAllActivitySession(),
                //ActivityTypes = await activityTypeService.GetAllActivityType(),
                //contacts = contacts,
                //teams = await teamService.GetTeamByParrentId(team.Id),
                //activityStatuses = await activityStatusService.GetAllActivityStatus(),
                //activityMasterCViewModels = await activityMasterService.GetActivityMasterCByLeadId(id)
                coldActivityMasterCAViewModels = await activityMasterService.GetColdActivityMasterByparentId(id)

            };
       


            return View(model);
        }
        public async Task<IActionResult> ColdVisitActivityMasterView(int id)
        {
            string username = HttpContext.User.Identity.Name;
            ViewBag.Id = id;

            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                //activityMasters = await activityMasterService.GetActivityMasterByLeadId(id),
                //ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                //ActivitySessions = await activitySessionService.GetAllActivitySession(),
                //ActivityTypes = await activityTypeService.GetAllActivityType(),
                //contacts = contacts,
                //teams = await teamService.GetTeamByParrentId(team.Id),
                //activityStatuses = await activityStatusService.GetAllActivityStatus(),
                //activityMasterCViewModels = await activityMasterService.GetActivityMasterCByLeadId(id)
                coldActivityMasterCAViewModels = await activityMasterService.GetColdActivityMasterByparentId(id)

            };



            return View(model);
        }
        public async Task<IActionResult> ActivityMasterColdPhoneCall(int?Id)
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            //var contacts = await contactsService.GetAllContactsbyLeadId(id);
            var teamactive = await teamService.GetTeamByParrentId(team.Id);
            if (teamactive == null)
            {
                teamactive = new List<Team>();
            }
            var master = new ColdActivityMasters();
            if (Id != null)
            {
                master = await coldService.GetColdActivityMastersById((int)Id);
            }

            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                coldActivityMasters = await coldService.GetAllColdActivityMasters(),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                //// contacts = contacts,
                teams = teamactive,
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                coldActivityMaster=master,
                coldActivityMasterCViewModels = await coldService.GetColdActivityMasterCByLeadId(),


            };


            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus([FromForm] ActivityMasterViewModel model)
        {
            ActivityStatusProgress data = new ActivityStatusProgress
            {
                activityMasterId = model.taskId,
                activityStatusId = model.taskStatusId,
                remarks=model.statusdescription,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now

            };
            await activityStatusProgressService.SaveActivityStatusProgress(data);
            return RedirectToAction(nameof(ActivityMasterView), new
            {
                id = Convert.ToInt32(model.taskId)

            });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityMasterColdVisit([FromForm] ActivityMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.coldActivityMasters = await coldService.GetAllColdActivityMasters();
                return View(model);
            }

            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            ColdActivityMasters data = new ColdActivityMasters
            {
                Id = model.Id,
                companyName = model.leadName,
                subject = model.subject,
                taskOwner = HttpContext.User.Identity.Name,
                ownerType = model.assignTo,
                activityCategoryId = Convert.ToInt32(model.category),
                priority = model.priority,
                description = model.description,
                activityTypeId = model.typeId,
                coldActivityMastersId = model.parentID,
                activitySessionId = model.session,
                activityStatusId = model.activityStatusId,
                activitiesDate = model.activitiesDate,
                isreminder = model.isreminder,
                reminderTime = model.reminderTime,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            int masterId = await coldService.SaveColdActivityMasters(data);
            if (model.contactlistname != null)
            {
                await coldService.DeleteColdActivityDetailsByMasterId(masterId);
                if (model.contactlistname.Count() > 0)
                {

                    for (int i = 0; i < model.contactlistname.Count(); i++)
                    {
                        ColdActivityDetails datadd = new ColdActivityDetails
                        {
                            Id = 0,
                            coldActivityMastersId = masterId,
                            contactName = model.contactlistname[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await coldService.SaveColdActivityDetails(datadd);
                    }

                }
            }
            if (model.teamlist != null && model.assignTo == "TeamMember")
            {
                await coldService.DeleteColdActivityTeamsByMasterId(masterId);
                if (model.teamlist.Count() > 0)
                {

                    for (int i = 0; i < model.teamlist.Count(); i++)
                    {
                        ColdActivityTeams dataddt = new ColdActivityTeams
                        {
                            Id = 0,
                            coldActivityMastersId = masterId,
                            teamId = (int)model.teamlist[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await coldService.SaveColdActivityTeams(dataddt);
                    }

                }
            }
            else
            {
                await coldService.DeleteColdActivityTeamsByMasterId(masterId);



                ColdActivityTeams dataddt = new ColdActivityTeams
                {
                    Id = 0,
                    coldActivityMastersId = masterId,
                    teamId = team.Id,
                    createdBy = HttpContext.User.Identity.Name,
                    createdAt = DateTime.Now
                };

                await coldService.SaveColdActivityTeams(dataddt);



            }

            return RedirectToAction("ActivityMasterColdVisit");

            //return RedirectToAction(nameof(ActivityMasterColdPhoneCall), new
            //{
            //    id = Convert.ToInt32(model.leadsID)

            //});

        }
        public async Task<IActionResult> ActivityMasterColdVisit(int?Id)
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            //var contacts = await contactsService.GetAllContactsbyLeadId(id);
            var master = new ColdActivityMasters();
            if (Id != null)
            {
                master = await coldService.GetColdActivityMastersById((int)Id);
            }

            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                coldActivityMasters = await coldService.GetAllColdActivityMasters(),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
               // contacts = contacts,
                teams = await teamService.GetTeamByParrentId(team.Id),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                coldActivityMaster=master,
             coldActivityMasterCViewModels = await coldService.GetColdActivityMasterCByLeadId(),


            };
          

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityMasterColdPhoneCall([FromForm] ActivityMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.coldActivityMasters = await coldService.GetAllColdActivityMasters();
                return View(model);
            }

            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            ColdActivityMasters data = new ColdActivityMasters
            {
                Id = model.Id,
                companyName = model.leadName,
                subject = model.subject,
                taskOwner = HttpContext.User.Identity.Name,
                ownerType = model.assignTo,
                activityCategoryId = Convert.ToInt32(model.category),
                priority = model.priority,
                description = model.description,
                activityTypeId = model.typeId,
                coldActivityMastersId = model.parentID,
                activitySessionId = model.session,
                activityStatusId = model.activityStatusId,
                activitiesDate = model.activitiesDate,
                isreminder = model.isreminder,
                reminderTime = model.reminderTime,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            int masterId = await coldService.SaveColdActivityMasters(data);
            if (model.contactlistname != null)
            {
                await coldService.DeleteColdActivityDetailsByMasterId(masterId);
                if (model.contactlistname.Count() > 0)
                {

                    for (int i = 0; i < model.contactlistname.Count(); i++)
                    {
                        ColdActivityDetails datadd = new ColdActivityDetails
                        {
                            Id = 0,
                            coldActivityMastersId = masterId,
                            contactName = model.contactlistname[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await coldService.SaveColdActivityDetails(datadd);
                    }

                }
            }
            if (model.teamlist != null && model.assignTo == "TeamMember")
            {
                await coldService.DeleteColdActivityTeamsByMasterId(masterId);
                if (model.teamlist.Count() > 0)
                {

                    for (int i = 0; i < model.teamlist.Count(); i++)
                    {
                   ColdActivityTeams dataddt = new ColdActivityTeams
                   {
                            Id = 0,
                            coldActivityMastersId = masterId,
                            teamId = (int)model.teamlist[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await coldService.SaveColdActivityTeams(dataddt);
                    }

                }
            }
            else
            {
                await coldService.DeleteColdActivityTeamsByMasterId(masterId);



                ColdActivityTeams dataddt = new ColdActivityTeams
                {
                    Id = 0,
                    coldActivityMastersId = masterId,
                    teamId = team.Id,
                    createdBy = HttpContext.User.Identity.Name,
                    createdAt = DateTime.Now
                };

                await coldService.SaveColdActivityTeams(dataddt);



            }

            return RedirectToAction("ActivityMasterColdPhoneCall");

            //return RedirectToAction(nameof(ActivityMasterColdPhoneCall), new
            //{
            //    id = Convert.ToInt32(model.leadsID)

            //});

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityMasterColdPhoneCallDis([FromForm] ActivityMasterViewModel model)
        {
            if (model.discussionlist != null)
            {
                await coldService.DeleteColdActivityDiscussionByMasterId((int)model.CparentID);
                if (model.discussionlist.Count() > 0)
                {

                    for (int i = 0; i < model.discussionlist.Count(); i++)
                    {
                        ColdActivityDiscussion datadd = new ColdActivityDiscussion
                        {
                            Id = 0,
                            coldActivityMastersId = (int)model.CparentID,
                            discussion = model.discussionlist[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await coldService.SaveColdActivityDiscussion(datadd);
                    }

                }
            }
            if (model.isfollowUp == 1 && model.subject!=null)
            {
                if (!ModelState.IsValid)
                {
                    model.coldActivityMasters = await coldService.GetAllColdActivityMasters();
                    return View(model);
                }

                string username = HttpContext.User.Identity.Name;
                var userinfo = await userInfo.GetUserInfoByUser(username);
                var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
                ColdActivityMasters data = new ColdActivityMasters
                {
                    Id = model.Id,
                    companyName = model.leadName,
                    subject = model.subject,
                    taskOwner = HttpContext.User.Identity.Name,
                    ownerType = model.assignTo,
                    activityCategoryId = Convert.ToInt32(model.category),
                    priority = model.priority,
                    description = model.description,
                    activityTypeId = model.typeId,
                    coldActivityMastersId = model.parentID,
                    activitySessionId = model.session,
                    activityStatusId = model.activityStatusId,
                    activitiesDate = model.activitiesDate,
                    isreminder = model.isreminder,
                    reminderTime = model.reminderTime,
                    createdBy = HttpContext.User.Identity.Name,
                    createdAt = DateTime.Now
                };

                int masterId = await coldService.SaveColdActivityMasters(data);
                if (model.contactlistname != null)
                {
                    await coldService.DeleteColdActivityDetailsByMasterId(masterId);
                    if (model.contactlistname.Count() > 0)
                    {

                        for (int i = 0; i < model.contactlistname.Count(); i++)
                        {
                            ColdActivityDetails datadd = new ColdActivityDetails
                            {
                                Id = 0,
                                coldActivityMastersId = masterId,
                                contactName = model.contactlistname[i],
                                createdBy = HttpContext.User.Identity.Name,
                                createdAt = DateTime.Now
                            };

                            await coldService.SaveColdActivityDetails(datadd);
                        }

                    }
                }
                if (model.teamlist != null && model.assignTo == "TeamMember")
                {
                    await coldService.DeleteColdActivityTeamsByMasterId(masterId);
                    if (model.teamlist.Count() > 0)
                    {

                        for (int i = 0; i < model.teamlist.Count(); i++)
                        {
                            ColdActivityTeams dataddt = new ColdActivityTeams
                            {
                                Id = 0,
                                coldActivityMastersId = masterId,
                                teamId = (int)model.teamlist[i],
                                createdBy = HttpContext.User.Identity.Name,
                                createdAt = DateTime.Now
                            };

                            await coldService.SaveColdActivityTeams(dataddt);
                        }

                    }
                }
                else
                {
                    await coldService.DeleteColdActivityTeamsByMasterId(masterId);



                    ColdActivityTeams dataddt = new ColdActivityTeams
                    {
                        Id = 0,
                        coldActivityMastersId = masterId,
                        teamId = team.Id,
                        createdBy = HttpContext.User.Identity.Name,
                        createdAt = DateTime.Now
                    };

                    await coldService.SaveColdActivityTeams(dataddt);



                }
            }


            return RedirectToAction(nameof(ActivityMasterColdPhoneCall), new
            {
                id = Convert.ToInt32(model.leadsID)

            });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityMasterColdVisitDis([FromForm] ActivityMasterViewModel model)
        {
            if (model.discussionlist != null)
            {
                await coldService.DeleteColdActivityDiscussionByMasterId((int)model.CparentID);
                if (model.discussionlist.Count() > 0)
                {

                    for (int i = 0; i < model.discussionlist.Count(); i++)
                    {
                        ColdActivityDiscussion datadd = new ColdActivityDiscussion
                        {
                            Id = 0,
                            coldActivityMastersId = (int)model.CparentID,
                            discussion = model.discussionlist[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await coldService.SaveColdActivityDiscussion(datadd);
                    }

                }
            }
            if (model.isfollowUp == 1 && model.subject != null)
            {
                if (!ModelState.IsValid)
                {
                    model.coldActivityMasters = await coldService.GetAllColdActivityMasters();
                    return View(model);
                }

                string username = HttpContext.User.Identity.Name;
                var userinfo = await userInfo.GetUserInfoByUser(username);
                var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
                ColdActivityMasters data = new ColdActivityMasters
                {
                    Id = model.Id,
                    companyName = model.leadName,
                    subject = model.subject,
                    taskOwner = HttpContext.User.Identity.Name,
                    ownerType = model.assignTo,
                    activityCategoryId = Convert.ToInt32(model.category),
                    priority = model.priority,
                    description = model.description,
                    activityTypeId = model.typeId,
                    coldActivityMastersId = model.parentID,
                    activitySessionId = model.session,
                    activityStatusId = model.activityStatusId,
                    activitiesDate = model.activitiesDate,
                    isreminder = model.isreminder,
                    reminderTime = model.reminderTime,
                    createdBy = HttpContext.User.Identity.Name,
                    createdAt = DateTime.Now
                };

                int masterId = await coldService.SaveColdActivityMasters(data);
                if (model.contactlistname != null)
                {
                    await coldService.DeleteColdActivityDetailsByMasterId(masterId);
                    if (model.contactlistname.Count() > 0)
                    {

                        for (int i = 0; i < model.contactlistname.Count(); i++)
                        {
                            ColdActivityDetails datadd = new ColdActivityDetails
                            {
                                Id = 0,
                                coldActivityMastersId = masterId,
                                contactName = model.contactlistname[i],
                                createdBy = HttpContext.User.Identity.Name,
                                createdAt = DateTime.Now
                            };

                            await coldService.SaveColdActivityDetails(datadd);
                        }

                    }
                }
                if (model.teamlist != null && model.assignTo == "TeamMember")
                {
                    await coldService.DeleteColdActivityTeamsByMasterId(masterId);
                    if (model.teamlist.Count() > 0)
                    {

                        for (int i = 0; i < model.teamlist.Count(); i++)
                        {
                            ColdActivityTeams dataddt = new ColdActivityTeams
                            {
                                Id = 0,
                                coldActivityMastersId = masterId,
                                teamId = (int)model.teamlist[i],
                                createdBy = HttpContext.User.Identity.Name,
                                createdAt = DateTime.Now
                            };

                            await coldService.SaveColdActivityTeams(dataddt);
                        }

                    }
                }
                else
                {
                    await coldService.DeleteColdActivityTeamsByMasterId(masterId);



                    ColdActivityTeams dataddt = new ColdActivityTeams
                    {
                        Id = 0,
                        coldActivityMastersId = masterId,
                        teamId = team.Id,
                        createdBy = HttpContext.User.Identity.Name,
                        createdAt = DateTime.Now
                    };

                    await coldService.SaveColdActivityTeams(dataddt);



                }
            }


            return RedirectToAction(nameof(ActivityMasterColdVisit), new
            {
                id = Convert.ToInt32(model.leadsID)

            });

        }
        public async Task<IActionResult> ActivityMaster(int id, string leadName,int?masterId)
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            var contacts = await contactsService.GetAllContactsbyLeadId(id);
            var master = new ActivityMaster();
            if(masterId!=null)
            {
                master = await activityMasterService.GetActivityMasterById((int)masterId);
            }
           
            if (contacts == null)
            {
                contacts = new List<Contacts>();
            }
            int teamId = 0;
            if(team != null)
            {
                teamId = team.Id;
            }


            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                activityMasters = await activityMasterService.GetActivityMasterByLeadId(id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions=await activitySessionService.GetAllActivitySession(),
                ActivityTypes=await activityTypeService.GetAllActivityType(),
                contacts = contacts,
                teams=await teamService.GetTeamByParrentId(teamId),
                activityStatuses=await activityStatusService.GetAllActivityStatus(),
                activityMasterCViewModels=await activityMasterService.GetActivityMasterCByLeadId(id),
                activityMaster=master

            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            if (model.activityMaster == null)
            {
                model.activityMaster.isreminder = 0;
                model.activityMaster.activityTypeId = 0;
            }
            return View(model);
        }
        public async Task<IActionResult> ActivityMasterF(int id, string leadName, int? masterId)
        {
            ViewBag.masterId = masterId;
            masterId = null;
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            var contacts = await contactsService.GetAllContactsbyLeadId(id);
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
          

            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                activityMasters = await activityMasterService.GetActivityMasterByLeadId(id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                contacts = contacts,
                teams = await teamService.GetTeamByParrentId(teamId),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                activityMasterCViewModels = await activityMasterService.GetActivityMasterCByLeadId(id),
                activityMaster = master

            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            if (model.activityMaster == null)
            {
                model.activityMaster.isreminder = 0;
                model.activityMaster.activityTypeId = 0;
            }
            return View(model);
        }
        public async Task<IActionResult> ActivityMasterTeam(int id, string leadName, int? masterId)
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            var contacts = await contactsService.GetAllContactsbyLeadId(id);
            var master = new ActivityMaster();
            if (masterId != null)
            {
                master = await activityMasterService.GetActivityMasterById((int)masterId);
            }

            if (contacts == null)
            {
                contacts = new List<Contacts>();
            }
  

            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                activityMasters = await activityMasterService.GetActivityMasterByLeadId(id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                contacts = contacts,
                teams = await teamService.GetTeamByParrentId(team.Id),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                activityMasterCViewModels = await activityMasterService.GetActivityMasterCByLeadId(id),
                activityMaster = master,
                activityMasterCAViewModels = await activityMasterService.GetActivityMasterByuserparentId((int)masterId, username),

            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityMasterTeam([FromForm] ActivityMasterViewModel model)
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

            return RedirectToAction(nameof(ActivityMasterTeam), new
            {
                id = Convert.ToInt32(model.leadsID),
                leadName = "",
                masterId = model.parentID

            });


        }
        public async Task<IActionResult> ActivityMasterList()
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                activityMasters = await activityMasterService.GetAllActivityMasterbyuser(userinfo.aspnetId)
            };
            return View(model);
        }
       

        // POST: ActivityMaster/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityMaster([FromForm] ActivityMasterViewModel model)
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
                activitySessionId=model.session,
                activityStatusId=model.activityStatusId,
                activitiesDate=model.activitiesDate,
                isreminder=model.isreminder,
                reminderTime=model.reminderTime,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

          int masterId=  await activityMasterService.SaveActivityMaster(data);
            ActivityStatusProgress dataa = new ActivityStatusProgress
            {
                activityMasterId = masterId,
                activityStatusId = model.activityStatusId,
                remarks = model.statusremarks,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now

            };
            await activityStatusProgressService.SaveActivityStatusProgress(dataa);
            if (model.contactlist!=null)
            {
                await activityDetailsService.DeleteActivityDetailsByMasterId(masterId);
                if (model.contactlist.Count() > 0)
                {

                    for (int i = 0; i < model.contactlist.Count(); i++)
                    {
                        ActivityDetails datadd = new ActivityDetails
                        {
                            Id =0,
                            activityMasterId = masterId,
                            contactsId =(int)model.contactlist[i],
                            createdBy = HttpContext.User.Identity.Name,
                            createdAt = DateTime.Now
                        };

                        await activityDetailsService.SaveActivityDetails(datadd);
                    }

                }
            }
            if (model.teamlist != null && model.assignTo== "TeamMember")
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

                id = masterId

            });

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityMasterF([FromForm] ActivityMasterViewModel model)
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

                id = masterId

            });

        }
        public async Task<IActionResult> ActivityDetailsEntry(int Id)
        {

            var datalist = await activityMasterService.GetActivityMasterById(Id);
            int leadsdd = Convert.ToInt32(datalist.leadsId);
            ActivityDetailsViewModel model = new ActivityDetailsViewModel()
            {
                
                activityMaster = datalist,
                contacts = await contactsService.GetAllContactsbyLeadId(leadsdd),
                activityDetails = await activityDetailsService.GetAllActivityDetailsByActivityMasterId(Id),
                activityStatus = await activityStatusService.GetAllActivityStatus()
            };
            ViewBag.activityMasterId = Id;
            return View(model);
        }
        // POST: ActivityDetailsEntry/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ActivityDetailsEntry([FromForm] ActivityDetailsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activityDetails = await activityDetailsService.GetAllActivityDetailsByActivityMasterId(model.activityMasterId);
                return View(model);
            }


            ActivityDetails data = new ActivityDetails
            {
                Id = model.Id,
                activityMasterId = model.activityMasterId,
                contactsId = model.contactsId,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await activityDetailsService.SaveActivityDetails(data);

            return RedirectToAction(nameof(ActivityDetailsEntry));
        }
        public async Task<IActionResult> ClientActivity(int id, string leadName)
        {

            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                activityMasters = await activityMasterService.GetActivityMasterByLeadId(id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                contacts = await contactsService.GetAllContactsbyLeadId(id)
            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }
        public async Task<IActionResult> DeleteActivityById(int id)
        {
            var data = await activityMasterService.GetActivityMasterById(id);
            try
            {
                await activityDetailsService.DeleteActivityDetailsByMasterId(id);
                await activityTeamService.DeleteActivityTeamByMasterId(id);
                await activityMasterService.DeleteActivityMasterById(id);

                return RedirectToAction("ActivityMaster", "Activity", new { id= data.leadsId, leadName= data.leads.leadName, Area = "CRMLead" });
            }
            catch
            {
                return RedirectToAction("ActivityMaster", "Activity", new { id = data.leadsId, leadName = data.leads.leadName, Area = "CRMLead" });
            }
        }
        // POST: ActivityMaster/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientActivity([FromForm] ActivityMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.activityMasters = await activityMasterService.GetAllActivityMaster();
                return View(model);
            }


            ActivityMaster data = new ActivityMaster
            {
                Id = model.Id,
                leadsId = Convert.ToInt32(model.leadsID),
                subject = model.subject,
                taskOwner = HttpContext.User.Identity.Name,
                ownerType = "Own",
                activityCategoryId = Convert.ToInt32(model.category),
                priority = model.priority,
                description = model.description,
                activityTypeId = model.typeId,
                activityMasterId = model.parentID,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await activityMasterService.SaveActivityMaster(data);
            return RedirectToAction(nameof(ClientActivity), new
            {
                id = Convert.ToInt32(model.leadsID)

            });
            
        }

        public async Task<IActionResult> MyActivity()
        {
            string username = HttpContext.User.Identity.Name;
            var userinfo = await userInfo.GetUserInfoByUser(username);
            var team = await teamService.GetTeamByaspnetuserId(userinfo.aspnetId);
            //var contacts = await contactsService.GetAllContactsbyLeadId(id);
            var master = new ActivityMaster();
            //if (masterId != null)
            //{
            //    master = await activityMasterService.GetActivityMasterById((int)masterId);
            //}

            //if (contacts == null)
            //{
            //    contacts = new List<Contacts>();
            //}
            int teamId = 0;
            if (team != null)
            {
                teamId = team.Id;
            }


            ActivityMasterViewModel model = new ActivityMasterViewModel()
            {
                //activityMasters = await activityMasterService.GetActivityMasterByLeadId(id),
                ActivityCategories = await activityCategoryService.GetAllActivityCategory(),
                ActivitySessions = await activitySessionService.GetAllActivitySession(),
                ActivityTypes = await activityTypeService.GetAllActivityType(),
                //contacts = contacts,
                teams = await teamService.GetTeamByParrentId(teamId),
                activityStatuses = await activityStatusService.GetAllActivityStatus(),
                //activityMasterCViewModels = await activityMasterService.GetActivityMasterCByLeadId(id),
                activityMaster = master

            };
            //ViewBag.leadName = leadName;
            //ViewBag.leadId = id;
            //if (model.activityMaster == null)
            //{
            //    model.activityMaster.isreminder = 0;
            //    model.activityMaster.activityTypeId = 0;
            //}
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveMyActivity([FromForm] ActivityMasterViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.activityMasters = await activityMasterService.GetAllActivityMaster();
            //    return View(model);
            //}

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

            return RedirectToAction(nameof(ActivityMasterList));

        }

        #region API Section
        [Route("global/api/ActivityDetails/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetActivitiyDetails(int Id)
        {
            return Json(await activityDetailsService.GetAllActivityDetailsByActivityMasterId(Id));
        }
        [Route("global/api/ActivityTeams/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetActivitiyTeams(int Id)
        {
            return Json(await activityTeamService.GetAllActivityTeamByActivityMasterId(Id));
        }
        [Route("global/api/ActivityDetailsCold/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetActivitiyDetailsCold(int Id)
        {
            return Json(await coldService.GetAllColdActivityDetailsByActivityMasterId(Id));
        }
        [Route("global/api/ActivityTeamsCold/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetActivitiyTeamsCold(int Id)
        {
            return Json(await coldService.GetAllColdActivityTeamsByActivityMasterId(Id));
        }
        [Route("global/api/ActivityDetailsByParentId/{id}")]
        [HttpGet]
        public async Task<IActionResult> ActivityDetailsByParentId(int Id)
        {
            return Json(await activityMasterService.GetActivityMasterByparentId(Id));

        }
        [Route("global/api/ActivityDiscussionByParentId/{id}")]
        [HttpGet]
        public async Task<IActionResult> ActivityDiscussionByParentId(int Id)
        {
            return Json(await coldService.GetAllColdActivityDiscussionsByActivityMasterId(Id));
        }
        #endregion
    }
}