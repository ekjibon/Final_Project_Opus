using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.ERPServices.AuthService.Interfaces;
using OPUSERP.ERPServices.Interfaces;

namespace OPUSERP.Areas.ERPSettings.Controllers
{
    [Authorize]
    [Area("ERPSettings")]
    public class NavbarController : Controller
    {
        private readonly IModuleAssignService moduleAssignService;
        private readonly INavbarService navbarService;

        public NavbarController(IHostingEnvironment hostingEnvironment, INavbarService navbarService, IModuleAssignService moduleAssignService)
        {
            this.moduleAssignService = moduleAssignService;
            this.navbarService = navbarService;
        }

        // GET: Bank
        public async Task<IActionResult> Create()
        {
            var model = new NavbarViewModel
            {
                ERPModules = await moduleAssignService.GetERPModules(),
                navbarsbyparent = await navbarService.GetNavbarItemByParent(),
                navbars = await navbarService.GetNavbarItem(),
            };
            return View(model);
        }

        // POST: Bank/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] NavbarViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.ERPModules = await moduleAssignService.GetERPModules();
                model.navbarsbyparent = await navbarService.GetNavbarItemByParent();
                model.navbars = await navbarService.GetNavbarItem();
                return View(model);
            }
            int? parentId = model.parentID;
            if (model.isParent == 2)
            {
                parentId = model.bandID;
            }

            Navbar data = new Navbar
            {
                Id = model.Id ?? 0,
                nameOption = model.nameOption,
                nameOptionBangla = model.nameOptionBangla,
                moduleId = model.moduleId,
                area = model.area,
                controller = model.controller,
                action = model.action,
                imageClass = model.imageClass,
                activeLi = model.activeLi,
                status = model.status,
                isParent = model.isParent,
                parentID = (int)parentId,
                displayOrder = model.displayOrder
            };

            await navbarService.SaveNavbarItem(data);

            return RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await navbarService.DeleteNavbarItemById(id);

            return RedirectToAction(nameof(Create));
        }

        #region API Section
        [Route("global/api/getNavbarItemByParentByModule/{moduleId}/{isParent}")]
        [HttpGet]
        public async Task<IActionResult> GetNavbarItemByParentByModule(int moduleId, int isParent)
        {
            return Json(await navbarService.GetNavbarItemByParentByModule(moduleId, isParent));
        }
        [Route("global/api/GetNavbarItemByParentIdByModule/{moduleId}/{isParent}")]
        [HttpGet]
        public async Task<IActionResult> GetNavbarItemByParentIdByModule(int moduleId, int isParent)
        {
            return Json(await navbarService.GetNavbarItemByParentIdByModule(moduleId, isParent));
        }

        [Route("global/api/GetNavbarParentIdById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetNavbarParentIdById(int id)
        {
            return Json(await navbarService.GetNavbarParentIdById(id));
        }
        #endregion
    }
}