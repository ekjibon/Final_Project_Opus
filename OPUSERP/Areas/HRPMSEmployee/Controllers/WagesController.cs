using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Area("HRPMSEmployee")]
    public class WagesController : Controller
    {
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly LangGenerate<GridViewLn> _lang1;

        private readonly IWagesPersonalInfoService personalInfoService;
        private readonly IWagesLeftDetailsService wagesLeftDetailsService;
        private readonly ISalaryService salaryService;
        private readonly IReligionMunicipalityService religionMunicipalityService;
        private readonly ITypeService typeService;
        private readonly IOrganizationPostService organizationPostService;
        private readonly IEmployeeOrganogramService employeeOrganogramService;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IAddressService addressService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IStatusService statusService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;


        public object dateOfBirth { get; private set; }

        public WagesController(IHostingEnvironment hostingEnvironment, RoleManager<ApplicationRole> roleManager, IStatusService statusService, IWagesPersonalInfoService personalInfoService, IReligionMunicipalityService religionMunicipalityService, ITypeService typeService, UserManager<ApplicationUser> userManager, IOrganizationPostService organizationPostService, IEmployeeOrganogramService employeeOrganogramService, IDesignationDepartmentService designationDepartmentService, IAddressService addressService, ISpecialBranchUnitService specialBranchUnitService, IWagesLeftDetailsService wagesLeftDetailsService, ISalaryService salaryService)
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
            this.statusService = statusService;
            this.wagesLeftDetailsService = wagesLeftDetailsService;
            this.salaryService = salaryService;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        // GET: Info/AllEmployeeList
        public async Task<IActionResult> AllWagesEmployeeList()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            var model = new EmployeeInfoViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                allWagesEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg("ddm"),
                employeeTypes = await typeService.GetAllEmployeeType()
            };
            return View(model);
        }

        public async Task<IActionResult> WagesResign()
        {
            var model = new WagesViewModel
            {
                wagesLeftDetails = await wagesLeftDetailsService.GetWagesLeftDetails()
            };
            return View(model);
        }

        public async Task<IActionResult> WagesSalaryStructure()
        {
            var model = new WagesSalaryStructureViewModel
            {
                salaryHeads = await salaryService.GetAllSalaryHead(),
                wagesSalaryStructures = await salaryService.GetAllWagesSalaryStructure()
            };
            return View(model);
        }

        public async Task<IActionResult> WagesSalaryStructureDelete(int id)
        {
            await salaryService.DeleteWagesSalaryStructureById(id);
            return RedirectToAction(nameof(WagesSalaryStructure));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesSalaryStructure([FromForm] WagesSalaryStructureViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.salaryHeads = await salaryService.GetAllSalaryHead();
                model.wagesSalaryStructures = await salaryService.GetAllWagesSalaryStructure();
                return View(model);
            }

            WagesSalaryStructure data = new WagesSalaryStructure
            {
                Id = model.editId,
                employeeId = model.employeeId,
                salaryHeadId = model.salaryHeadId,
                amount = model.amount,
            };

            await salaryService.SaveWagesSalaryStructure(data);
            return RedirectToAction(nameof(WagesSalaryStructure));
        }

        public async Task<IActionResult> WagesResignDelete(int id, int empId)
        {
            await wagesLeftDetailsService.DeleteWagesLeftDetailsById(id);
            await personalInfoService.UpdateEmployeeinfoActiveById(empId);
            return RedirectToAction(nameof(WagesResign));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WagesResign([FromForm] WagesViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.wagesLeftDetails = await wagesLeftDetailsService.GetWagesLeftDetails();
                return View(model);
            }

            WagesLeftDetails data = new WagesLeftDetails
            {
                Id = model.editId,
                employeeId = model.employeeId,
                date = model.date,
                reason = model.reason,
            };

            await wagesLeftDetailsService.SaveWagesLeftDetails(data);
            await personalInfoService.UpdateEmployeeinfoInactiveById(model.employeeId);
            return RedirectToAction(nameof(WagesResign));
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
                allWagesEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg("ddm"),
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
                ModelState.AddModelError(string.Empty, "This Code Already Taken");
                flag = true;
            }

            if (!ModelState.IsValid || flag)
            {
                ViewBag.employeeID = model.employeeID;
                model.fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]);
                model.religions = await religionMunicipalityService.GetReligions();
                model.employeeTypes = await typeService.GetAllEmployeeType();
                model.organoOrganizations = await organizationPostService.GetAllOrganizationByIds(this.GetAllIdsByOrg(user.org));
                model.designations = await designationDepartmentService.GetDesignations();
                model.districts = await addressService.GetAllDistrict();
                model.departments = await designationDepartmentService.GetDepartment();
                return View(model);
            }


            DateTime dateBirth = Convert.ToDateTime(model.dateOfBirth);
            DateTime dateLPR = dateBirth.AddYears(59);
            if (model.freedomFighter == "Yes") dateLPR = dateLPR.AddYears(1);

            string ApplicationUserId = await personalInfoService.GetAuthCodeByUserId(Int32.Parse(model.employeeID));
            //Console.WriteLine("\n\n\n"+dateLPR+"\n\n\n");

            WagesEmployeeInfo data = new WagesEmployeeInfo
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
                disability = model.disability,
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

                LPRDate = dateLPR.AddDays(-1).ToString("MM/dd/yyyy"),
                PRLStartDate = dateLPR.ToString("MM/dd/yyyy"),
                PRLEndDate = dateLPR.AddYears(1).ToString("MM/dd/yyyy"),
                joiningDatePresentWorkstation = model.joiningDatePresentWorkstation,
                joiningDateGovtService = model.joiningDateGovtService,
                dateofregularity = model.dateofregularity,
                dateOfPermanent = model.dateOfPermanent,
                branchId = model.sbu,
                pNSId = model.pns,

                natureOfRequitment = model.natureOfRequitment,
                activityStatus = 1,
                departmentId = model.department,
                specialSkill = model.specialSkill,
                seniorityNumber = model.seniorityNumber,
                joiningDesignation = model.joiningDesignation,
                designation = model.designation,
                skypeId = model.skypeId,

                post = model.post,
                homeDistrict = model.homeDistrict,
                orgType = "ddm",
                ApplicationUserId = ApplicationUserId
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
                wagesEmployeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                religions = await religionMunicipalityService.GetReligions(),
                employeeTypes = await typeService.GetAllEmployeeType(),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                organoOrganizations = await organizationPostService.GetAllOrganizationByIds(this.GetAllIdsByOrg(user.org)),
                designations = await designationDepartmentService.GetDesignations(),
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                districts = await addressService.GetAllDistrict(),
                departments = await designationDepartmentService.GetDepartment(),
            };
            return View(model);
        }

        #region Api
        [AllowAnonymous]
        [Route("global/api/allWagesEmpList/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> AllEmpList(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var roles = await _userManager.GetRolesAsync(user);
            return Json(await personalInfoService.GetEmployeeInfoAsQueryAble(queryString, "ddm"));

        }

        [AllowAnonymous]
        [Route("global/api/getWagesEmployeeInfoByOrg/")]
        [HttpGet]
        public async Task<IActionResult> getWagesEmployeeInfoByOrg()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await personalInfoService.GetEmployeeInfoByOrg("ddm"));
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