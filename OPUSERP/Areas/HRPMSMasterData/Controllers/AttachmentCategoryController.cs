using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class AttachmentCategoryController : Controller
    {

        private readonly IAttachmentCategoryService  attachmentCategoryService;

        public AttachmentCategoryController(IHostingEnvironment hostingEnvironment, IAttachmentCategoryService attachmentCategoryService)
        {
            this.attachmentCategoryService = attachmentCategoryService;
        }

        #region Atttachment Group
        public async Task<IActionResult> AtttachmentGroup()
        {
            AtttachmentGroupViewModel model = new AtttachmentGroupViewModel
            {
                atttachmentGroups = await attachmentCategoryService.GetAllAtttachmentGroup()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtttachmentGroup([FromForm] AtttachmentGroupViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.atttachmentGroups = await attachmentCategoryService.GetAllAtttachmentGroup();
            //    return View(model);
            //}

            AtttachmentGroup attachmentCategory = new AtttachmentGroup
            {
                Id = (int)model.groupId,
                groupName = model.groupName

            };

            await attachmentCategoryService.SaveAtttachmentGroup(attachmentCategory);

            return RedirectToAction(nameof(AtttachmentGroup));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteAtttachmentGroupById(int Id)
        {
            await attachmentCategoryService.DeleteAtttachmentGroupById(Id);
            return Json(true);
        }

        #endregion


        #region Atttachment Category
        public async Task<IActionResult> Index()
        {
            AttachmentCategoryViewModel model = new AttachmentCategoryViewModel
            {
                atttachmentGroups = await attachmentCategoryService.GetAllAtttachmentGroup(),
                atttachmentCategories = await attachmentCategoryService.GetAllAttachmentCategory()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AttachmentCategoryViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.atttachmentCategories = await attachmentCategoryService.GetAllAttachmentCategory();
            //    return View(model);
            //}

            AtttachmentCategory attachmentCategory = new AtttachmentCategory
            {
                Id = (int)model.categoryId,
                atttachmentGroupId=model.atttachmentGroupId,
                categoryName = model.categoryName

            };

            await attachmentCategoryService.SaveAttachmentCategory(attachmentCategory);           

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await attachmentCategoryService.DeleteAttachmentCategoryById(id);
            return RedirectToAction(nameof(Index));
        }

        #endregion
    }
}