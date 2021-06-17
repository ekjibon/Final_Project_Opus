using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using System;
using System.Threading.Tasks;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.Areas.MemberInfo.Models.Lang;

namespace OPUSERP.CLUB.Areas.MemberInfo.Controllers
{
    [Authorize]
    [Area("MemberInfo")]
    public class SpouseController : Controller
    {
        private readonly LangGenerate<SpouseLn> _lang;
        private readonly ISpouseChildrenService spouseChildrenService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ISpecialSkillService specialSkillService;

        public SpouseController(IHostingEnvironment hostingEnvironment, ISpecialSkillService specialSkillService, IPersonalInfoService personalInfoService, ISpouseChildrenService spouseChildrenService)
        {
            _lang = new LangGenerate<SpouseLn>(hostingEnvironment.ContentRootPath);
            this.spouseChildrenService = spouseChildrenService;
            this.personalInfoService = personalInfoService;
            this.specialSkillService = specialSkillService;
        }

        // GET: Spouse
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new SpouseViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]),
                spouses = await spouseChildrenService.GetSpouseByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                specialSkills = await specialSkillService.GetSpacialSkills()
            };
            if (model.spouse == null) model.spouse = new MemberSpouse();
            return View(model);
        }

        // POST: Spouse/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] SpouseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]);
                model.specialSkills = await specialSkillService.GetSpacialSkills();
                if (model.spouse == null) model.spouse = new MemberSpouse();
                return View(model);
            }

            MemberSpouse data = new MemberSpouse
            {
                Id = Int32.Parse(model.spouseID),
                employeeId = Int32.Parse(model.employeeID),
                spouseName = model.spouseName,
                spouseNameBN = model.spouseNameBN,
                email = model.emailAddress,
                dateOfBirth = model.dateOfBirth,
                dateOfMarriage = model.dateOfMarriage,
                occupation = model.occupation,
                gender = model.gender,
                designation = model.designation,
                organization = model.organization,
                bin = model.bin,
                nid = model.nid,
                contact = model.contact,
                highestEducation = model.higherEducation,
                bloodGroup = model.bloodGroup,
                homeDistrict = model.homeDistrict
            };

            if (model.skills != null)
            {
                data.spacialSkillIds = String.Join(",", model.skills);
                data.spacialSkills = String.Join(", ", await specialSkillService.GetSpacialSkillsByIds(model.skills));
            }
            else
            {
                data.spacialSkillIds = String.Empty;
                data.spacialSkills = String.Empty;
            }

            await spouseChildrenService.SaveSpouse(data);

            return RedirectToAction(nameof(Index));
        }

        // Delete: Children
        public async Task<IActionResult> Delete(int id, int empId)
        {
            await spouseChildrenService.DeleteSpouseById(id);
            //return RedirectToAction(nameof(Index));
            return RedirectToAction("Index", "Spouse", new
            {
                id = empId
            });
        }

    }
}