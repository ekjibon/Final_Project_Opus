using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OPUSERP.Areas.HRPMSOrganogram.Models;
using OPUSERP.Areas.HRPMSOrganogram.Models.Lang;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSOrganogram.Controllers
{
    [Authorize]
    [Area("HRPMSOrganogram")]
    public class OrganizationController : Controller
    {
        private readonly LangGenerate<OrganoOrganizationLn> _lang;
        private readonly LangGenerate<PostLn> _langPost;
        private readonly IOrganizationPostService organizationPostService;
        private readonly IOrganizationTypeService organizationTypeService;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private int Depth;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrganizationController(IHostingEnvironment hostingEnvironment, IOrganizationPostService organizationPostService, IOrganizationTypeService organizationTypeService, UserManager<ApplicationUser> userManager, IDesignationDepartmentService designationDepartmentService)
        {
            _lang = new LangGenerate<OrganoOrganizationLn>(hostingEnvironment.ContentRootPath);
            _langPost = new LangGenerate<PostLn>(hostingEnvironment.ContentRootPath);
            this.organizationPostService = organizationPostService;
            this.organizationTypeService = organizationTypeService;
            this.designationDepartmentService = designationDepartmentService;
            _userManager = userManager;
            Depth = 0;
        }

        // GET: Organization
        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            OrganoOrganizationViewModel model = new OrganoOrganizationViewModel
            {
                fLang = _lang.PerseLang("Organogram/OrganizationEN.json", "Organogram/OrganizationBN.json", Request.Cookies["lang"]),
                fLangPost = _langPost.PerseLang("Organogram/PostEN.json", "Organogram/PostBN.json", Request.Cookies["lang"]),
                organoOrganizations = await organizationPostService.GetAllOrganization(),
                organizationTypes = await organizationTypeService.GetAllOrganizationType(),
                organoRoots = await organizationPostService.GetRootOrganizations(),
                designations = await designationDepartmentService.GetDesignations(),
                organizationType = user.org
            };
            return View(model);
        }

        // POST: Organization/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] OrganoOrganizationViewModel model)
        {
            //return Json(model);
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("Organogram/OrganizationEN.json", "Organogram/OrganizationBN.json", Request.Cookies["lang"]);
                model.organoOrganizations = await organizationPostService.GetAllOrganization();
                model.organizationTypes = await organizationTypeService.GetAllOrganizationType();
                model.organoRoots = await organizationPostService.GetRootOrganizations();
                return View(model);
            }

            OrganoOrganization data = new OrganoOrganization
            {
                Id = model.organoOrganizationId,
                organizationTypeId = model.organizationTypeId,
                organoOrganizationId = model.organoOrganizationParrentId,
                nameEN = model.nameEN,
                nameBN = model.nameBN,
                remarks = model.remarks,
            };

            //return Json(model);

            await organizationPostService.SaveOrganization(data);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobPost([FromForm] OrganoOrganizationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Post data = new Post
                {
                    altDesignationId = model.altDesignationId,
                    designationId = model.designationId,
                    numberOfPost = model.numberOfPost,
                    organoOrganizationId = model.organoOrganizationId,
                    IsHead = model.IsHead
                };
                await organizationPostService.SaveOrUpdatePost(data);
            }
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> GetOrganizationsJson(string org)
        {
            Depth = 2;
            string s,tm;
            if (org == "ddm")
            {
                tm = await this.GenerateTree(1, "Start", 0);
                if (tm == "")
                    s = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", 1, "Opus", "Opus", "null", 1, tm) + "}";
                else s = tm;
            }   
            else if (org == "ministry")
            {
                tm = await this.GenerateTree(2, "Start", 0);
                if(tm == "")
                    s = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", 2, "Ministry", "মিনিস্ট্রি", "null", 1, tm) + "}";
                else
                    s = tm;
            } 
            else
            {
                tm = await this.GenerateTree(3, "Start", 0);
                if (tm == "")
                    s = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", 0, "Start", "StartBN", "null", 1, tm) + "}";
                else
                    s = tm;
            }
                
            dynamic data = new JObject();
            data.menus = s;
            data.depth = Depth;
            return Json(data);
        }

        //Recursion For Retriving Tree 
        private async Task<string> GenerateTree(int parrentid, string parrentName, int level)
        {
            int isHead = 2;
            Depth = Math.Max(level, Depth);
            string data = "";
            IEnumerable<OrganoOrganization> organoOrganizations = await organizationPostService.GetOrganizationByParrentId(parrentid);
           
            if (organoOrganizations.Count() <= 0) return data;
            int last = organoOrganizations.Last().Id;

            foreach (OrganoOrganization menu in organoOrganizations)
            {
                string child = await GenerateTree(menu.Id, menu.nameEN, level + 1);
                string name = menu.nameEN;
                string Temp = await organizationPostService.GetAllPostString(menu.Id);
                if (Temp != "") name += "|" + Temp;
                string S = "{" + string.Format("\"data\":{0},\"name\":\"{1}\",\"nameBN\":\"{2}\",\"parent\":\"{3}\",\"head\":{4},\"children\":[{5}]", menu.Id, name, menu.nameEN, parrentid, isHead, child) + "}";

                if (menu.Id != last)
                {
                    S += ",";
                }
                data += S;
            }
            return data;
        }

        #region API Section
        [Route("global/api/organoOrganization/{id}")]
        [HttpGet]
        public async Task<IActionResult> OrganoOrganization(int Id)
        {
            return Json(await organizationPostService.GetOrganizationById(Id));
        }
        #endregion
    }
}