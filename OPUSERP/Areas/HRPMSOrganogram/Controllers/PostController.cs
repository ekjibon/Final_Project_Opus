using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSOrganogram.Models;
using OPUSERP.Areas.HRPMSOrganogram.Models.Lang;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSOrganogram.Controllers
{
    [Authorize]
    [Area("HRPMSOrganogram")]
    public class PostController : Controller
    {
        private readonly LangGenerate<PostLn> _lang;
        private readonly IOrganizationPostService organizationPostService;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly UserManager<ApplicationUser> _userManager;
        public PostController(IHostingEnvironment hostingEnvironment, IOrganizationPostService organizationPostService, IDesignationDepartmentService designationDepartmentService, UserManager<ApplicationUser> userManager)
        {
            _lang = new LangGenerate<PostLn>(hostingEnvironment.ContentRootPath);
            this.organizationPostService = organizationPostService;
            this.designationDepartmentService = designationDepartmentService;
            _userManager = userManager;
        }

        // GET: Post
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            PostViewModel model = new PostViewModel
            {
                fLang = _lang.PerseLang("Organogram/PostEN.json", "Organogram/PostBN.json", Request.Cookies["lang"]),
                organoOrganizations = await organizationPostService.GetAllOrganizationByIds(this.GetAllIdsByOrg(user.org)),
                designations = await designationDepartmentService.GetDesignations(),
                posts = await organizationPostService.GetAllPost(),
            };
            return View(model);
        }

        // POST: Organization/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PostViewModel model)
        {
            //return Json(model);

            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Organogram/PostEN.json", "Organogram/PostBN.json", Request.Cookies["lang"]);
                model.organoOrganizations = await organizationPostService.GetAllOrganizationByIds(this.GetAllIdsByOrg(user.org));
                model.posts = await organizationPostService.GetAllPost();
                model.designations = await designationDepartmentService.GetDesignations();
                return View(model);
            }

            Post data = new Post
            {
                Id = model.postId,
                organoOrganizationId = model.organoOrganizationId,
                designationId = model.designationId,
                IsHead = model.IsHead,
                numberOfPost = model.numberOfPost
            };

            await organizationPostService.SavePost(data);

            return RedirectToAction(nameof(Index));
        }

        #region Recursion..

        private List<int> GetAllIdsByOrg(string org)
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
            return data;
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