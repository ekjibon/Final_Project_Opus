using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Area("HRPMSEmployee")]
    public class InfoController : Controller
    {
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly LangGenerate<GridViewLn> _lang1;

        private readonly IPersonalInfoService personalInfoService;
        private readonly IReligionMunicipalityService religionMunicipalityService;
        private readonly ITypeService typeService;
        private readonly IOrganizationPostService organizationPostService;
        private readonly IEmployeeOrganogramService employeeOrganogramService;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IAddressService addressService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IFunctionsInfoService functionsInfoService;
        private readonly ILocationService locationService;
        private readonly IStatusService statusService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public object dateOfBirth { get; private set; }

        public InfoController(IHostingEnvironment hostingEnvironment, IFunctionsInfoService functionsInfoService, ILocationService locationService, RoleManager<ApplicationRole> roleManager, IStatusService statusService, IPersonalInfoService personalInfoService, IReligionMunicipalityService religionMunicipalityService, ITypeService typeService, UserManager<ApplicationUser> userManager, IOrganizationPostService organizationPostService, IEmployeeOrganogramService employeeOrganogramService, IDesignationDepartmentService designationDepartmentService, IAddressService addressService, ISpecialBranchUnitService specialBranchUnitService)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new LangGenerate<GridViewLn>(hostingEnvironment.ContentRootPath);
            this.personalInfoService = personalInfoService;
            this.religionMunicipalityService = religionMunicipalityService;
            this.organizationPostService = organizationPostService;
            this.employeeOrganogramService = employeeOrganogramService;
            this.designationDepartmentService = designationDepartmentService;
            this.typeService = typeService;
            this.addressService = addressService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.locationService = locationService;
            this.functionsInfoService = functionsInfoService;
            this.statusService = statusService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Info/AllEmployeeList
        public async Task<IActionResult> AllEmployeeList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            

            var model = new EmployeeInfoViewModel
            {
                allEmployeeJsons = await personalInfoService.GetAllEmployeeInfoJson(),
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg("ddm"),
                employeeTypes = await typeService.GetAllEmployeeType(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        public async Task<IActionResult> AllInactiveEmployeeList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new EmployeeInfoViewModel
            {
                allEmployeeJsons = await personalInfoService.GetAllInactiveEmployeeInfoJson(),
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg("ddm"),
                employeeTypes = await typeService.GetAllEmployeeType()
            };
            return View(model);
        }


        public async Task<IActionResult> ApproveInfo(int Id)
        {
            //ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            await personalInfoService.ApproveEmployeeinfoById(Id);
            //var model = new EmployeeInfoViewModel
            //{
            //    fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
            //    allEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg("ddm"),
            //    employeeTypes = await typeService.GetAllEmployeeType()
            //};
            return RedirectToAction(nameof(AllEmployeeListForApprove));
        }
        public async Task<IActionResult> AllEmployeeListForApprove()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new EmployeeInfoViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg("ddm"),
                employeeTypes = await typeService.GetAllEmployeeType()
            };
            return View(model);
        }

        // GET: Info/Create
        public async Task<IActionResult> Create()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new EmployeeInfoViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                religions = await religionMunicipalityService.GetReligions(),
                employeeTypes = await typeService.GetAllEmployeeType(),
                organoOrganizations = await organizationPostService.GetAllOrganizationByIds(this.GetAllIdsByOrg(user.org)),
                designations = await designationDepartmentService.GetDesignations(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                districts = await addressService.GetAllDistrict(),
                departments = await designationDepartmentService.GetDepartment(),
                serviceStatuses = await statusService.GetServiceStatus(),
                hrPrograms = await statusService.GetHrProgram(),
                hrUnits = await statusService.GetHrUnit(),
                locations = await locationService.GetLocation(),
                functionInfos = await functionsInfoService.GetFunctionInfo(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        // POST: Info/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EmployeeInfoViewModel model)
        {
            //return Json(model);
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            int temp = await personalInfoService.IsThisEmpIDPresent(model.employeeCode);
            bool flag = false;
            if (temp != 0 && temp != Int32.Parse((model.employeeID)))
            {
                ModelState.AddModelError(string.Empty, "This Employee Code Already Taken. Please Try Another Employee Code");
                flag = true;
            }
            //var periodCheck = await personalInfoService.GetDuplicateEmpCode(Convert.ToInt32(model.employeeID), model.employeeCode);

            if (!ModelState.IsValid || flag)
            //if (!ModelState.IsValid || periodCheck.Count() > 0)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]);
                model.religions = await religionMunicipalityService.GetReligions();
                model.employeeTypes = await typeService.GetAllEmployeeType();
                model.organoOrganizations = await organizationPostService.GetAllOrganizationByIds(this.GetAllIdsByOrg(user.org));
                model.designations = await designationDepartmentService.GetDesignations();
                model.specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit();
                model.districts = await addressService.GetAllDistrict();
                model.departments = await designationDepartmentService.GetDepartment();
                model.serviceStatuses = await statusService.GetServiceStatus();
                model.hrPrograms = await statusService.GetHrProgram();
                model.hrUnits = await statusService.GetHrUnit();
                model.locations = await locationService.GetLocation();
                model.functionInfos = await functionsInfoService.GetFunctionInfo();
                model.visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData();

                //if (periodCheck.Count() > 0)
                //{
                //    ModelState.AddModelError(string.Empty, "This Employee Code Already Taken. Please try another Code !!!");
                //}

                return View(model);
            }


            DateTime dateBirth = Convert.ToDateTime(model.dateOfBirth);
            DateTime? dateLPR = dateBirth.AddYears(59);
            DateTime? LPRDate;
            DateTime? PRLStartDate;
            DateTime? PRLEndDate;
            if (model.freedomFighter == "Yes")
            {
                dateLPR = Convert.ToDateTime(dateLPR).AddYears(1);
            }
            if (model.employeeType != 1)
            {
                LPRDate = null;
                PRLStartDate = null;
                PRLEndDate = null;
            }
            else
            {
                LPRDate = Convert.ToDateTime(dateLPR).AddDays(-1);
                PRLStartDate = Convert.ToDateTime(dateLPR);
                PRLEndDate = Convert.ToDateTime(dateLPR).AddYears(1);
            }


            string motherNameEnglish;
            string fatherNameEnglish;
            if (string.IsNullOrEmpty(model.motherNameEnglish))
            {
                motherNameEnglish = "";
            }
            else
            {
                motherNameEnglish = model.motherNameEnglish.ToUpper();
            }
            if (string.IsNullOrEmpty(model.fatherNameEnglish))
            {
                fatherNameEnglish = "";
            }
            else
            {
                fatherNameEnglish = model.fatherNameEnglish.ToUpper();
            }

            string ApplicationUserId = await personalInfoService.GetAuthCodeByUserId(Int32.Parse(model.employeeID));

            EmployeeInfo data = new EmployeeInfo
            {
                Id = Int32.Parse(model.employeeID),
                employeeCode = model.employeeCode,
                nationalID = model.nationalID,
                birthIdentificationNo = model.birthIdentificationNo,
                govtID = model.govtID,
                gpfNomineeName = model.gpfNomineeName,
                gpfAcNo = model.gpfAcNo,
                nameEnglish = model.nameEnglish.ToUpper(),
                nameBangla = model.nameBangla,
                motherNameEnglish = motherNameEnglish,
                motherNameBangla = model.motherNameBangla,
                fatherNameEnglish = fatherNameEnglish,
                fatherNameBangla = model.fatherNameBangla,
                nationality = model.nationality,
                disability = model.disability,
                disablityType = model.disablityType,
                dateOfBirth = model.dateOfBirth,
                gender = model.gender,
                birthPlace = model.birthPlace,
                maritalStatus = model.maritalStatus,
                religionId = model.religion,
                employeeTypeId = model.employeeType,
                tin = model.tin,
                batch = model.batch,
                bloodGroup = model.bloodGroup,
                freedomFighter = model.freedomFighter == "Yes" ? true : false,
                freedomFighterNo = model.freedomFighterNo,
                telephoneOffice = model.telephoneOffice,
                telephoneResidence = model.telephoneResidence,
                pabx = model.pabx,
                emailAddress = model.emailAddress,
                mobileNumberOffice = model.mobileNumberOffice,
                mobileNumberPersonal = model.mobileNumberPersonal,
                emailAddressPersonal = model.emailAddressPersonal,

                LPRDate = LPRDate, // Convert.ToDateTime(dateLPR).AddDays(-1),
                PRLStartDate = PRLStartDate, // Convert.ToDateTime(dateLPR),
                PRLEndDate = PRLEndDate, //Convert.ToDateTime(dateLPR).AddYears(1),
                joiningDatePresentWorkstation = model.joiningDatePresentWorkstation,
                joiningDateGovtService = model.joiningDateGovtService,
                dateofregularity = model.dateofregularity,
                dateOfPermanent = model.dateOfPermanent,
                branchId = model.sbu,
                pNSId = model.pns,

                natureOfRequitment = model.natureOfRequitment,
                activityStatus = model.activityStatus,
                departmentId = model.department,
                specialSkill = model.specialSkill,
                seniorityNumber = model.seniorityNumber,
                joiningDesignation = model.joiningDesignation,
                designation = model.designation,
                skypeId = model.skypeId,
                employeeStatusId = model.employeeStatusId,
                hrProgramId = model.hrProgramId,
                hrUnitId = model.hrUnitId,
                functionInfoId = model.functionInfoId,
                locationId = model.locationId,
                post = model.post,
                homeDistrict = model.homeDistrict,
                orgType = "ddm",
                ApplicationUserId = ApplicationUserId,
                salaryStatus = model.salaryStatus,
                salaryStatusComment = model.salaryStatusComment
            };

            int lstId = await personalInfoService.SaveEmployeeInfo(data);
            await personalInfoService.UpdateEmployeeinfoById(lstId);
            return RedirectToAction(nameof(Index), new { @id = lstId });
        }

        // GET: Info
        public async Task<IActionResult> Index(int id)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            ViewBag.employeeID = id.ToString();
            var model = new EmployeeInfoViewModel
            {
                employeeID = id.ToString(),
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                religions = await religionMunicipalityService.GetReligions(),
                employeeTypes = await typeService.GetAllEmployeeType(),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                organoOrganizations = await organizationPostService.GetAllOrganizationByIds(this.GetAllIdsByOrg(user.org)),
                designations = await designationDepartmentService.GetDesignations(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                districts = await addressService.GetAllDistrict(),
                departments = await designationDepartmentService.GetDepartment(),
                serviceStatuses = await statusService.GetServiceStatus(),
                hrPrograms = await statusService.GetHrProgram(),
                hrUnits = await statusService.GetHrUnit(),
                locations = await locationService.GetLocation(),
                functionInfos = await functionsInfoService.GetFunctionInfo(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
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

        public IActionResult PeerSearch()
        {
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> GetAllEmpList(string empStatus)
        //{
        //    ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
        //    var roles = await _userManager.GetRolesAsync(user);

        //    if (roles.Contains("HR CRM CRO Admin") || roles.Contains("HRAdmin") || roles.Contains("SCMAdmin") || roles.Contains("admin"))
        //    {
        //        return Json(await personalInfoService.GetAllEmployeeInfo(empStatus, "ddm"));

        //    }
        //    else
        //    {
        //        return Json(await personalInfoService.GetEmployeeInfoAsQueryAbleSingle(empStatus, "ddm", user.EmpCode));
        //    }
        //}

        #region API Section
        [Route("global/api/GetEmployeeInfoBySearch/{searchKey}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoBySearch(string searchKey)
        {
            return Json(await personalInfoService.GetEmployeeInfoBySearch(searchKey));
        }
        [AllowAnonymous]
        [Route("global/api/employeeByCode/{code}")]
        [HttpGet]
        public async Task<IActionResult> employeeById(string code)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await personalInfoService.GetEmployeeInfoByCodeAndOrg(code, "ddm"));
        }

        [AllowAnonymous]
        [Route("global/api/getEmployeeInfoByOrg/")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoByOrg()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await personalInfoService.GetEmployeeInfoByOrg("ddm"));
        }

        [AllowAnonymous]
        [Route("global/api/freeEmployeeByCode/{code}")]
        [HttpGet]
        public async Task<IActionResult> FreeEmployeeByCode(string code)
        {
            return Json(await personalInfoService.GetFreeEmployeeByCode(code));
        }

        // Newly added
        [AllowAnonymous]
        [Route("global/api/allEmployeeList/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> AllEmployeeList(int queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);

            if (roles.Contains("HR CRM CRO Admin") || roles.Contains("HRAdmin") || roles.Contains("SCMAdmin") || roles.Contains("admin"))
            {
                return Json(await personalInfoService.GetAllEmployeeInfo(queryString, "ddm"));

            }
            else
            {
                return Json(await personalInfoService.GetEmployeeInfoSingle(queryString, "ddm", user.EmpCode));
            }

        }

        [AllowAnonymous]
        [Route("global/api/allEmpList/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> AllEmpList(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            //var data = await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, "ddm");
            //if (roles[0].ToString() == "HR CRM CRO Admin" || roles[0].ToString() == "HRAdmin" || roles[0].ToString() == "SCMAdmin")
            if (roles.Contains("HR CRM CRO Admin") || roles.Contains("HRAdmin") || roles.Contains("SCMAdmin") || roles.Contains("admin"))
            {
                return Json(await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, "ddm"));

            }
            else
            {
                return Json(await personalInfoService.GetEmployeeInfoAsQueryAbleSingle(queryString, "ddm", user.EmpCode));
            }
        }

        [AllowAnonymous]
        [Route("global/api/allActiveEmpList/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> AllActiveEmpList(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            //var data = await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, "ddm");
            //if (roles[0].ToString() == "HR CRM CRO Admin" || roles[0].ToString() == "HRAdmin" || roles[0].ToString() == "SCMAdmin")
            if (roles.Contains("HR CRM CRO Admin") || roles.Contains("HRAdmin") || roles.Contains("SCMAdmin") || roles.Contains("admin"))
            {
                //return Json(await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, "ddm"));
                return Json(await personalInfoService.GetActiveEmployeeInfoAsQueryAble(queryString, "ddm"));

            }
            else
            {
                return Json(await personalInfoService.GetEmployeeInfoAsQueryAbleSingle(queryString, "ddm", user.EmpCode));
            }
        }

        [AllowAnonymous]
        [Route("global/api/allInactiveEmpList/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> AllInactiveEmpList(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            //var data = await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, "ddm");
            //if (roles[0].ToString() == "HR CRM CRO Admin" || roles[0].ToString() == "HRAdmin" || roles[0].ToString() == "SCMAdmin")
            if (roles.Contains("HR CRM CRO Admin") || roles.Contains("HRAdmin") || roles.Contains("SCMAdmin") || roles.Contains("admin"))
            {
                //return Json(await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, "ddm"));
                return Json(await personalInfoService.GetInactiveEmployeeInfoAsQueryAble(queryString, "ddm"));

            }
            else
            {
                return Json(await personalInfoService.GetEmployeeInfoAsQueryAbleSingle(queryString, "ddm", user.EmpCode));
            }
        }

        [AllowAnonymous]
        [Route("global/api/allEmpListForApp/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> allEmpListForApp(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            ////var data = await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, "ddm");
            //if (roles[0].ToString() == "General User")
            //{
            //    return Json(await personalInfoService.GetEmployeeInfoAsQueryAbleSingle(queryString, "ddm", user.EmpCode));
            //}
            //else
            //{
            //    return Json(await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, "ddm"));
            //}

            return Json(await personalInfoService.GetEmployeeInfoAsQueryAbleApprove(queryString, "ddm", user.EmpCode));

        }

        [AllowAnonymous]
        [Route("global/api/allAvailablePosts/{orgId}")]
        [HttpGet]
        public async Task<IActionResult> allAvailablePosts(int orgId)
        {
            return Json(await employeeOrganogramService.employeeAvailablePosts(orgId));
        }
        [AllowAnonymous]
        [Route("global/api/GetAllEmployeeInfo")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeInfo()
        {
            return Json(await personalInfoService.GetEmployeeInfo());
        }

        #endregion

        #region Recursion
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