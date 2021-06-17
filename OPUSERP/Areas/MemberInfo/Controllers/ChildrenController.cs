using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.Areas.MemberInfo.Models;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class ChildrenController : Controller
    {
        private readonly LangGenerate<ChildrenLn> _lang;
        private readonly ISpouseChildrenService spouseChildrenService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISpecialSkillService spacialSkilService;

        public ChildrenController(IHostingEnvironment hostingEnvironment, ISpecialSkillService spacialSkilService, IPersonalInfoService personalInfoService, ISpouseChildrenService spouseChildrenService)
        {
            _lang = new LangGenerate<ChildrenLn>(hostingEnvironment.ContentRootPath);
            this.spouseChildrenService = spouseChildrenService;
            this.personalInfoService = personalInfoService;
            this.spacialSkilService = spacialSkilService;
        }

        // GET: Children
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new ChildrenViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                children = await spouseChildrenService.GetChildrenByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                spacialSkills = await spacialSkilService.GetSpacialSkills()
            };
            return View(model);
        }

        // POST: Children/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ChildrenViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.children = await spouseChildrenService.GetChildrenByEmpId(Int32.Parse(model.childrenID));
                model.spacialSkills = await spacialSkilService.GetSpacialSkills();
                model.fLang = _lang.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]);
                return View(model);
            }

            MemberChildren data = new MemberChildren
            {
                Id = Int32.Parse(model.childrenID),
                employeeId = Int32.Parse(model.employeeID),
                childName = model.childName,
                childNameBN = model.childNameBN,
                dateOfBirth = model.dateOfBirth,
                education = model.education,
                occupation = model.occupation,
                gender = model.gender,
                designation = model.designation,
                organization = model.organization,
                bin = model.bin,
                nid = model.nid,
                bloodGroup = model.bloodGroup
            };

            if (model.skills != null)
            {
                data.spacialSkillIds = String.Join(",", model.skills);
                data.spacialSkills = String.Join(", ", await spacialSkilService.GetSpacialSkillsByIds(model.skills));
            }
            else
            {
                data.spacialSkillIds = String.Empty;
                data.spacialSkills = String.Empty;
            }

            await spouseChildrenService.SaveChildren(data);

            return RedirectToAction(nameof(Index));
        }

        // Delete: Children
        public async Task<IActionResult> Delete(int id,int empId)
        {
            await spouseChildrenService.DeleteChildrenById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Children", new
            {
                id= empId
            });
        }
    }
}