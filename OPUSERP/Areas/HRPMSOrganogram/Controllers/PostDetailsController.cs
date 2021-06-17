using OPUSERP.Areas.HRPMSOrganogram.Models;
using OPUSERP.Areas.HRPMSOrganogram.Models.Lang;
using OPUSERP.HRPMS.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;

namespace OPUSERP.Areas.HRPMSOrganogram.Controllers
{
    [Authorize]
    [Area("HRPMSOrganogram")]
    public class PostDetailsController : Controller
    {
        private readonly LangGenerate<PostDetailsLn> _lang;
        private readonly IOrganizationPostService organizationPostService;
        private readonly IPostingEmploymentTypeService postingEmploymentTypeService;
        private readonly UserManager<ApplicationUser> _userManager;

        public PostDetailsController(IHostingEnvironment hostingEnvironment, IOrganizationPostService organizationPostService, IPostingEmploymentTypeService postingEmploymentTypeService, UserManager<ApplicationUser> userManager)
        {
            _lang = new LangGenerate<PostDetailsLn>(hostingEnvironment.ContentRootPath);
            this.organizationPostService = organizationPostService;
            this.postingEmploymentTypeService = postingEmploymentTypeService;
            _userManager = userManager;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            PostDetailsViewModel model = new PostDetailsViewModel
            {
                fLang = _lang.PerseLang("Organogram/PostDetailsEN.json", "Organogram/PostDetailsBN.json", Request.Cookies["lang"]),
                posts = await organizationPostService.GetAllPost(),
                postingTypes = await postingEmploymentTypeService.GetAllPostingType(),
                employmentTypesposts = await postingEmploymentTypeService.GetAllEmploymentType(),
                postDetails = await organizationPostService.GetAllPostDetailsByOrgIds(this.GetAllIdsByOrg(user.org))
            };

            return View(model);
        }

        // POST: Organization/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PostDetailsViewModel model)
        {
            //return Json(model);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Organogram/PostDetailsEN.json", "Organogram/PostDetailsBN.json", Request.Cookies["lang"]);
                model.posts = await organizationPostService.GetAllPost();
                model.postingTypes = await postingEmploymentTypeService.GetAllPostingType();
                model.employmentTypesposts = await postingEmploymentTypeService.GetAllEmploymentType();
                model.postDetails = await organizationPostService.GetAllPostDetailsByOrgIds(this.GetAllIdsByOrg(user.org));
                return View(model);
            }

            PostDetails data = new PostDetails
            {
                Id = model.postDetailsId,
                postId = model.postId,
                employeeId = model.employeeId,
                postingTypeId = model.postingTypeId,
                employmentTypeId = model.employmentTypeId,
                reportingBossId = model.reportingBossId == 0 ? (int?)null: model.reportingBossId,
                assaignDate = Convert.ToDateTime(model.assaignDate),
                remarks = model.remarks,
            };

            await organizationPostService.SavePostDetails(data);

            return RedirectToAction(nameof(Index));
        }

        #region Recursion..

        private List<int?> GetAllIdsByOrg(string org)
        {
            List<int> data = new List<int>();

            if (org == "ddm")
            {
                data.Add(1);
                data.AddRange(this.GetChildIds(1));

            }
            else if (org == "ministry")
            {
                data.Add(2);
                data.AddRange(this.GetChildIds(2));
            }
            else
            {
                data.Add(0);
                data.AddRange(this.GetChildIds(0));
            }
            return data.Cast<int?>().ToList();
        }

        private List<int> GetChildIds(int id)
        {
            List<int> childs = organizationPostService.GetllChildIdsByparrentId(id);
            List<int> Temp = new List<int>();
            foreach (int myId in childs)
            {
                Temp.AddRange(GetChildIds(myId));
            }
            Temp.AddRange(childs);
            return Temp;
        }

        #endregion
    }
}