﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPServices.AuthService.Interfaces;
using OPUSERP.ERPServices.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Models;
using OPUSERP.VMS.Models;

namespace OPUSERP.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPageAssignService pageAssignService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly INavbarService navbarService;
        private readonly IBillCollectionService billCollectionService;
        private readonly UserManager<ApplicationUser> _userManager;
        private ERPDbContext _db;

        public HomeController(IPageAssignService pageAssignService, IERPCompanyService eRPCompanyService, INavbarService navbarService, IBillCollectionService billCollectionService, ERPDbContext db, UserManager<ApplicationUser> _userManager)
        {
            this.pageAssignService = pageAssignService;
            this.eRPCompanyService = eRPCompanyService;
            this.navbarService = navbarService;
            this.billCollectionService = billCollectionService;
            this._userManager = _userManager;
            _db = db;
        }

        public async Task<ActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(user);
            //if (roles.Contains("Requisition Raiser"))
            //{
            //    return RedirectToAction("Dashboard", "RequsitionRaiserDashbord", new { Area = "SCMRequisition" });
            //}
            //else
            //{
            //    return RedirectToAction("StoreKeeper", "RequisitionDashbord", new { Area = "SCMRequisition" });
            //}
            return View();
        }

        #region Privacy

        public IActionResult Privacy()
        {
            return View();
        }

        #endregion

        #region Navigation



        public async Task<IActionResult> Navigation()
        {
            string userName = HttpContext.User.Identity.Name;

            NavbarViewModel model = new NavbarViewModel
            {
                navbars = await navbarService.GetNavigationMenu(userName),

            };
            ViewBag.UserTypeID = 1;

            return PartialView("_NavMenu", model);
        }

        public async Task<IActionResult> AssignPage()
        {
            string userName = HttpContext.User.Identity.Name;
            var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
            List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            List<int?> lstmodule = _db.UserAccessPages.Where(x => roleids.Contains(x.applicationRoleId)).Select(x => x.navbarId).ToList();

            List<int> lstparentId = _db.Navbars.Where(x => lstmodule.Contains(x.Id)).Select(x => x.parentID).ToList();
            List<int> lstparentIdF = _db.Navbars.Where(x => lstparentId.Contains(x.Id)).Select(x => x.parentID).ToList();

            var navdata = await pageAssignService.GetNavbars(userName);
            var adminrole = _db.UserRoles.Where(x => x.UserId == userId && x.RoleId == "0583d54e-74a8-46a3-b880-e13698723f69").ToList();
            if (adminrole.Count() == 0)
            {
                navdata = navdata.Where(x => lstmodule.Contains(x.Id) || lstparentId.Contains(x.Id) || lstparentIdF.Contains(x.Id));
            }
            List<int?> modid = navdata.Select(x => x.moduleId).ToList();
            var modules = await pageAssignService.GetERPModules();
            if (adminrole.Count() == 0)
            {
                modules = modules.Where(x => modid.Contains(x.Id));
            }
            NavbarViewModel model = new NavbarViewModel
            {
                navbars = navdata,//await pageAssignService.GetNavbars(userName),
                ERPModules = modules//await pageAssignService.GetERPModules()
            };

            ViewBag.UserTypeID = 1;

            return PartialView("_Navbar", model);
        }

        public async Task<IActionResult> GridMenuPage(int moduleId, int perentId)
        {
            string userName = HttpContext.User.Identity.Name;
            var userId = _db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();
            // var data = _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            List<string> roleids = _db.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToList();
            List<int?> lstmodule = _db.UserAccessPages.Where(x => roleids.Contains(x.applicationRoleId)).Select(x => x.navbarId).ToList();

            List<int> lstparentId = _db.Navbars.Where(x => lstmodule.Contains(x.Id)).Select(x => x.parentID).ToList();

            List<Navbar> lstMenu = _db.Navbars.Where(x => x.moduleId == moduleId && x.parentID == perentId && x.status == true).OrderBy(x => x.displayOrder).ToList();
            List<Navbar> lstChieldMenu = new List<Navbar>();
            //foreach(var item in lstMenu)
            //{
            //    lstChieldMenu.AddRange(_db.Navbars.Where(x => x.parentID == item.Id).OrderBy(x => x.displayOrder).ToList());
            //}

            var navdata = await pageAssignService.GetNavbars(userName);
            var adminrole = _db.UserRoles.Where(x => x.UserId == userId && x.RoleId == "0583d54e-74a8-46a3-b880-e13698723f69").ToList();
            if (adminrole.Count() == 0)
            {
                lstChieldMenu = navdata.Where(x => lstmodule.Contains(x.Id)).ToList();
                lstMenu = lstMenu.Where(x => lstparentId.Contains(x.Id)).ToList();
            }
            else
            {
                lstChieldMenu = navdata.ToList();
            }
            List<int?> modid = navdata.Select(x => x.moduleId).ToList();
            var modules = await pageAssignService.GetERPModules();
            if (adminrole.Count() == 0)
            {
                modules = modules.Where(x => modid.Contains(x.Id));
            }

            var model = new NavbarViewModel
            {
                navbars = lstChieldMenu,
                navbarsbyparent = lstMenu,
                ERPModules = modules
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        #endregion

        #region Dashboard

        public ActionResult CrmDashboard()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetCollectionDashboardByDateArea(DateTime? frmDate, DateTime? toDate, int? areaId)
        {

            return Json(await billCollectionService.GetCollectionDashboardByDateArea(frmDate, toDate, areaId));
        }


        #endregion
    }
}
