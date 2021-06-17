using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Services.Master.Interface;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Area("MemberInfo")]
    [Authorize]
    public class InfoController : Controller
    {
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly LangGenerate<GridViewLn> _lang1;

        private readonly IPersonalInfoService personalInfoService;
        private readonly IReligionMunicipalityService religionMunicipalityService;
        private readonly IMemberTypeService memberTypeService;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IAddressService addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDegreeService degreeService;
        private readonly ISubjectService subjectService;
        private readonly IMemberInfoService memberInfoService;
        private readonly IOrganizationService organizationService;
        private readonly ISpecialSkillService specialSkillService;
        private readonly IPhotographService  photographService;

        public object dateOfBirth { get; private set; }

        public InfoController(IHostingEnvironment hostingEnvironment, 
            IPhotographService photographService, 
            IOrganizationService organizationService, 
            ISubjectService subjectService, 
            IDegreeService degreeService, 
            ISpecialSkillService specialSkillService, 
            IPersonalInfoService personalInfoService,
            IReligionMunicipalityService religionMunicipalityService,
            IMemberTypeService memberTypeService, 
            UserManager<ApplicationUser> userManager, 
            IDesignationDepartmentService designationDepartmentService,
            IAddressService addressService,
            IMemberInfoService memberInfoService)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new LangGenerate<GridViewLn>(hostingEnvironment.ContentRootPath);
            this.personalInfoService = personalInfoService;
            this.religionMunicipalityService = religionMunicipalityService;
            this.designationDepartmentService = designationDepartmentService;
            this.memberTypeService = memberTypeService;
            this.addressService = addressService;
            this.degreeService = degreeService;
            this.subjectService = subjectService;
            this.memberInfoService = memberInfoService;
            this.organizationService = organizationService;
            this.specialSkillService = specialSkillService;
            this.photographService = photographService;
            _userManager = userManager;
        }

        // GET: Info/AllEmployeeList
        public async Task<IActionResult> AllMemberList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new MemberInfoViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg(user.org),
                religions = await religionMunicipalityService.GetReligions(),
                memberTypes = await memberTypeService.GetAllMemberType(),
                divisions = await addressService.GetAllDivision(),
                districts = await addressService.GetAllDistrict(),
                thanas = await addressService.GetAllThana(),
                degrees = await degreeService.GetAllDegree(),
                subjects = await subjectService.GetAllSubject(),
                organizations = await organizationService.GetAllOrganization(),
                specialSkills = await specialSkillService.GetSpacialSkills(),
                allMemberJsons = await personalInfoService.GetAllMemberInfoJson(),
            };
            return View(model);
        }



        // GET: Info/AllEmployeeList 
        public async Task<IActionResult> AllMemberListGridView()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new MemberInfoViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
            };
            return View(model);
        }


        // GET: Info/Create
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new MemberInfoViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                religions = await religionMunicipalityService.GetReligions(),
                memberTypes = await memberTypeService.GetAllMemberType(),
                designations = await designationDepartmentService.GetDesignations(),
                districts = await addressService.GetAllDistrict(),
                specialSkills = await specialSkillService.GetSpacialSkills()
            };
            return View(model);
        }

        // POST: Info/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] MemberInfoViewModel model)
        {
            //return Json(model);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            //int temp = await personalInfoService.IsThisEmpIDPresent(model.employeeCode);
            //bool flag = false;
            //if (temp != 0 && temp != Int32.Parse((model.employeeID)))
            //{
            //    ModelState.AddModelError(string.Empty, "This Code Already Taken");
            //    flag = true;
            //}

            int validation = await personalInfoService.GetEmployeeInfoCheck(model.employeeCode, Int32.Parse(model.employeeID));

            if (!ModelState.IsValid || validation == 0)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]);
                model.religions = await religionMunicipalityService.GetReligions();
                model.memberTypes = await memberTypeService.GetAllMemberType();
                model.designations = await designationDepartmentService.GetDesignations();
                model.specialSkills = await specialSkillService.GetSpacialSkills();
                if (validation == 0)
                {
                    ModelState.AddModelError(string.Empty, "Membership Code already exists.");
                }
                return View(model);
            }


            DateTime dateBirth = Convert.ToDateTime(model.dateOfBirth);
            DateTime dateLPR = dateBirth.AddYears(59);
            if (model.freedomFighter == "Yes") dateLPR = dateLPR.AddYears(1);

            string ApplicationUserId = await personalInfoService.GetAuthCodeByUserId(Int32.Parse(model.employeeID));
            //Console.WriteLine("\n\n\n"+dateLPR+"\n\n\n");

            OPUSERP.CLUB.Data.Member.MemberInfo data = new OPUSERP.CLUB.Data.Member.MemberInfo
            {
                Id = Int32.Parse(model.employeeID),
                employeeCode = model.employeeCode,
                nationalID = model.nationalID,
                birthIdentificationNo = model.birthIdentificationNo,
                govtID = model.govtID,
                gpfNomineeName = model.gpfNomineeName,
                gpfAcNo = model.gpfAcNo,
                nameEnglish = model.nameEnglish,
                nameBangla = model.nameBangla,
                motherNameEnglish = model.motherNameEnglish,
                motherNameBangla = model.motherNameBangla,
                fatherNameEnglish = model.fatherNameEnglish,
                fatherNameBangla = model.fatherNameBangla,
                nationality = model.nationality,
                dateOfBirth = model.dateOfBirth,
                gender = model.gender,
                birthPlace = model.birthPlace,
                maritalStatus = model.maritalStatus,
                religionId = model.religion,
                memberTypeId = Convert.ToInt32(model.employeeType),
                bloodGroup = model.bloodGroup,
                freedomFighter = model.freedomFighter == "Yes" ? true : false,
                freedomFighterNo = model.freedomFighterNo,
                telephoneOffice = model.telephoneOffice,
                telephoneResidence = model.telephoneResidence,
                emailAddress = model.emailAddress,
                mobileNumberOffice = model.mobileNumberOffice,
                mobileNumberPersonal = model.mobileNumberPersonal,
                emailAddressPersonal = model.emailAddressPersonal,

                natureOfRequitment = model.natureOfRequitment,
                specialSkill = model.specialSkill,
                seniorityNumber = model.seniorityNumber,
                joiningDesignation = model.joiningDesignation,
                designation = model.designation,
                activityStatus = model.activityStatus,
                post = model.post,
                homeDistrict = model.homeDistrict,
                orgType = user.org,
                //ApplicationUserId = ApplicationUserId,
                joiningDateGovtService = model.dateOfMembership
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

            int lstId = await personalInfoService.SaveEmployeeInfo(data);
            return RedirectToAction(nameof(Index), new { @id = lstId });
        }

        // GET: Info
        public async Task<IActionResult> Index(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.employeeID = id.ToString();
            var model = new MemberInfoViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                religions = await religionMunicipalityService.GetReligions(),
                memberTypes = await memberTypeService.GetAllMemberType(),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                //designation =  designationDepartmentService.GetDesignationById(id),
                designations = await designationDepartmentService.GetDesignations(),
                districts = await addressService.GetAllDistrict(),
                specialSkills = await specialSkillService.GetSpacialSkills(),
                photograph = await photographService.GetPhotographById(id)

            };
            return View(model);
        }

        [Authorize(Roles = "admin,user")]
        public async Task<IActionResult> GridView(int id)
        {
            ViewBag.employeeID = id.ToString();
            GridViewViewModel model = new GridViewViewModel
            {
                fLang = _lang1.PerseLang("Home/HomeEN.json", "Home/HomeBN.json", Request.Cookies["lang"]),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }



        #region API Section
    
        [HttpGet]
        public async Task<IActionResult> employeeGridView(int perPage, int pageNo)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            int dataPerPage = perPage;
            double dataCount;


            var model = new MemberInfoViewModel
            {
                allMemberJsons = await personalInfoService.GetAllMemberInfoJson(),
            };

            List<AllMemberJson> AllMemberJson = model.allMemberJsons.ToList();
            dataCount = AllMemberJson.Count();
            model.allMemberJsons = AllMemberJson.OrderBy(x => x.nameEnglish).Skip(perPage * (pageNo - 1)).Take(perPage).ToList();
            int totalPage = (int)Math.Ceiling(dataCount / perPage);

            model.CurrentPage = pageNo;
            model.TotalPage = totalPage;
            model.dataCount = (int)Math.Ceiling(dataCount);
            
            return Json(model);

        }

       [HttpGet]
        public async Task<IActionResult> EmployeeTableView()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new MemberInfoViewModel
            {
                allMemberJsons = await personalInfoService.GetAllMemberInfoJson(),
            };
            return Json(model);
        }


        [AllowAnonymous]
        [Route("global/api/getEmployeeInfo/")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfo()
        {
            return Json(await personalInfoService.GetEmployeeInfo());
        }

        [AllowAnonymous]
        [Route("global/api/employeeByCode/{code}")]
        [HttpGet]
        public async Task<IActionResult> employeeById(string code)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await personalInfoService.GetEmployeeInfoByCodeAndOrg(code, user.org));
        }

        [AllowAnonymous]
        [Route("global/api/freeEmployeeByCode/{code}")]
        [HttpGet]
        public async Task<IActionResult> FreeEmployeeByCode(string code)
        {
            return Json(await personalInfoService.GetFreeEmployeeByCode(code));
        }

        [AllowAnonymous]
        [Route("global/api/allMemberList/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> AllEmpList(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, user.org));
        }

        [AllowAnonymous]
        [Route("global/api/getEmployeeInfoAsQueryAbleMore/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoAsQueryAbleMore(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await personalInfoService.GetEmployeeInfoAsQueryAbleMore(queryString, user.org));
        }


        [AllowAnonymous]
        [Route("global/api/AllMemberList")]
        [HttpGet]
        public async Task<IActionResult> GetAllMemberList()
        {
            //ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            return Json(await personalInfoService.GetEmployeeInfo());
        }

        #endregion




    }
}