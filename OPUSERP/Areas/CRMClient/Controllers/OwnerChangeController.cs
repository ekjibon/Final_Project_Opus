using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMClient.Models;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Client.Interfaces;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using IDesignationDepartmentService = OPUSERP.HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService;

namespace OPUSERP.Areas.CRMClient.Controllers
{
    [Area("CRMClient")]
    public class OwnerChangeController : Controller
    {
        private ERPDbContext _db;
        private readonly IClientService clientService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly ILeadsService leadsService;
        private readonly IUserInfoes userInfoes;
        private readonly IBankBranchService bankBranchService;
        public OwnerChangeController(ERPDbContext db, IHostingEnvironment hostingEnvironment, IBankBranchService bankBranchService,  IClientService clientService, ILeadsService leadsService, IUserInfoes userInfoes)
        {
           
            this._hostingEnvironment = hostingEnvironment;
            _db = db;
            this.clientService = clientService;
            this.leadsService = leadsService;
            this.bankBranchService = bankBranchService;
            this.userInfoes = userInfoes;
           
        }

        #region Owner Change
        public IActionResult Index()
        {
            OwnerChangeViewModel model = new OwnerChangeViewModel
            {
                //getClientInfoListViewModels = await clientService.GetClientsByOwner(HttpContext.User.Identity.Name)

            };
            return View(model);
        }
        public IActionResult ChangeActivity()
        {
            OwnerChangeViewModel model = new OwnerChangeViewModel
            {
                //getClientInfoListViewModels = await clientService.GetClientsByOwner(HttpContext.User.Identity.Name)

            };
            return View(model);
        }
        public async Task<IActionResult> AllClientList()
        {
            var data = await bankBranchService.GetLeadsBankInfo();
            OwnerChangeViewModel model = new OwnerChangeViewModel
            {
                //getClientInfoListViewModels = await clientService.GetClientsByOwner(HttpContext.User.Identity.Name)
                leadsBankInfos = data.Where(x=>x.bankType=="Primary")

            };
            return View(model);
        }
        public IActionResult OwnerChangeHistory()
        {
            //OwnerChangeViewModel model = new OwnerChangeViewModel
            //{
            //    //getClientInfoListViewModels = await clientService.GetClientsByOwner(HttpContext.User.Identity.Name)

            //};
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> SaveOwnerChange([FromForm] OwnerChangeViewModel model)
        {
            try
            {
                var count = 1;
                //if (model.changeType == "OneOwnertoanotherOwner")
                //{
                //    await leadsService.SaveChangeOwnerUserOneToOne(model.previousOwner, model.previousOwnerUser, model.newOwner, model.newOwnerUser, HttpContext.User.Identity.Name);
                //}
                //else
                //{
                //    for (int i = 0; i < model.leadsId.Length; i++)
                //    {
                //        await leadsService.SaveChangeOwnerSingleUser(model.leadsId[i], model.previousOwner, model.previousOwnerUser, model.newOwner, model.newOwnerUser, HttpContext.User.Identity.Name);

                //    }
                //}
                for (int i = 0; i < model.leadsId.Length; i++)
                {
                    var data = await leadsService.GetLeadsById((int)model.leadsId[i]);
                    await leadsService.SaveChangeOwnerSingleUser(model.leadsId[i], data.leadOwner, data.leadOwner, model.newOwneremail, model.newOwneremail, HttpContext.User.Identity.Name);

                }

                return Json(count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public async Task<JsonResult> SaveChangeActivity([FromForm] OwnerChangeViewModel model)
        {
            try
            {
                var count = 1;
                //if (model.changeType == "OneOwnertoanotherOwner")
                //{
                //    await leadsService.SaveChangeOwnerUserOneToOne(model.previousOwner, model.previousOwnerUser, model.newOwner, model.newOwnerUser, HttpContext.User.Identity.Name);
                //}
                //else
                //{
                //    for (int i = 0; i < model.leadsId.Length; i++)
                //    {
                //        await leadsService.SaveChangeOwnerSingleUser(model.leadsId[i], model.previousOwner, model.previousOwnerUser, model.newOwner, model.newOwnerUser, HttpContext.User.Identity.Name);

                //    }
                //}
                int? isactive = 0;
                for (int i = 0; i < model.leadsId.Length; i++)
                {
                    var clientdata = await clientService.GetClientsByLeadId((int)model.leadsId[i]);
                    isactive = clientdata.isactive;
                    if (isactive == 1)
                    {
                        isactive = 0;
                    }
                    else
                    {
                        isactive = 1;
                    }
                    var data = await leadsService.GetLeadsById((int)model.leadsId[i]);
                    await leadsService.SaveChangeActivity(model.leadsId[i],isactive, HttpContext.User.Identity.Name);

                }

                return Json(count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsList()
        {
            return Json(await leadsService.GetAllLeads());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsListSP()
        {
            return Json(await leadsService.GetAllLeadsListSP());
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsByuser(string UserName)
        {
            return Json(await leadsService.GetAllLeadsByuser(UserName));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsByuserSP(string UserName)
        {
            return Json(await leadsService.GetAllLeadsByuserSP(UserName));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsCByuserSP(string UserName)
        {
            return Json(await leadsService.GetAllLeadsCByuserSP(UserName));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsById(int Id)
        {
            return Json(await leadsService.GetLeadslistById(Id));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsByName(string lead)
        {
            return Json(await leadsService.GetLeadInfoByfil(lead));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsByNameSP(string lead)
        {
            return Json(await leadsService.GetAllLeadsByNameSP(lead));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsCByNameSP(string lead)
        {
            return Json(await leadsService.GetAllLeadsCByNameSP(lead));
        }

        [Route("global/api/GetAllActiveEmployeeInfoForOwnerChange")]
        [HttpGet]
        public async Task<IActionResult> GetAllActiveEmployeeInfoForOwnerChange()
        {
            return Json(await userInfoes.GetAllActiveEmployeeInfoForOwnerChange());
        }
        #endregion


    }
}