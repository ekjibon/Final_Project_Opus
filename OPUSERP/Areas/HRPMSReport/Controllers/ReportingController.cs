using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Areas.HRPMSReport.Models;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Report;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSReport.Controllers
{
    [Authorize]
    [Area("HRPMSReport")]
    public class ReportingController : Controller
    {
        private readonly IReports reports;
        private readonly LangGenerate<EmployeeInfoLn> _lang;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ITypeService typeService;
        private readonly IAddressService addressService;
        private readonly IReligionMunicipalityService religionMunicipalityService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IDegreeService degreeService;
        private readonly ISubjectService subjectService;
        private readonly IOrganizationService organizationService;
        private readonly IMembershipLanguageService membershipLanguageService;
        private readonly ILeaveTypeService leaveTypeService;

        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IPNSService pNSService;
        private readonly IHRProjectService hRProjectService;
        private readonly IHRDonerSevice hRDonerSevice;
        private readonly IHRActivityService hRActivityService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public ReportingController(IReports reports, IHRActivityService hRActivityService, IHRDonerSevice hRDonerSevice, IHRProjectService hRProjectService, IPNSService pNSService, ISpecialBranchUnitService specialBranchUnitService, ILeaveTypeService leaveTypeService, IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, ITypeService typeService, UserManager<ApplicationUser> userManager, IAddressService addressService, IReligionMunicipalityService religionMunicipalityService, IDesignationDepartmentService designationDepartmentService, IDegreeService degreeService, ISubjectService subjectService, IOrganizationService organizationService, IMembershipLanguageService membershipLanguageService, IConverter converter)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            this.personalInfoService = personalInfoService;
            this.typeService = typeService;
            this.addressService = addressService;
            this.religionMunicipalityService = religionMunicipalityService;
            this.designationDepartmentService = designationDepartmentService;
            this.degreeService = degreeService;
            this.subjectService = subjectService;
            this.organizationService = organizationService;
            this.membershipLanguageService = membershipLanguageService;
            this.leaveTypeService = leaveTypeService;

            this.specialBranchUnitService = specialBranchUnitService;
            this.pNSService = pNSService;
            this.hRProjectService = hRProjectService;
            this.hRDonerSevice = hRDonerSevice;
            this.hRActivityService = hRActivityService;

            _userManager = userManager;
            this.reports = reports;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new ReportingViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                countries = await addressService.GetAllContry(),
                districts = await addressService.GetAllDistrict(),
                divisions = await addressService.GetAllDivision(),
                thanas = await addressService.GetAllThana(),
                religions = await religionMunicipalityService.GetReligions(),
                //allEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg(user.org),
                employeeTypes = await typeService.GetAllEmployeeType(),
                designations = await designationDepartmentService.GetDesignations(),
                degrees = await degreeService.GetAllDegree(),
                subjects = await subjectService.GetAllSubject(),
                organizations = await organizationService.GetAllOrganization(),
                languages = await membershipLanguageService.GetLanguageInfo(),
                leaveTypelist = await leaveTypeService.GetAllLeaveType(),

                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                pNs = await pNSService.GetPNS(),
                hRProjects = await hRProjectService.GetHRProject(),
                hRDoners = await hRDonerSevice.GetHRDoner(),
                hRActivities = await hRActivityService.GetHRActivity(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);
        }

        public async Task<IActionResult> WagesIndex()
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            var model = new ReportingViewModel
            {
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                countries = await addressService.GetAllContry(),
                districts = await addressService.GetAllDistrict(),
                divisions = await addressService.GetAllDivision(),
                thanas = await addressService.GetAllThana(),
                religions = await religionMunicipalityService.GetReligions(),
                //allEmployeeInfos = await personalInfoService.GetEmployeeInfoByOrg(user.org),
                employeeTypes = await typeService.GetAllEmployeeType(),
                designations = await designationDepartmentService.GetDesignations(),
                degrees = await degreeService.GetAllDegree(),
                subjects = await subjectService.GetAllSubject(),
                organizations = await organizationService.GetAllOrganization(),
                languages = await membershipLanguageService.GetLanguageInfo(),
                leaveTypelist = await leaveTypeService.GetAllLeaveType(),
                departments = await designationDepartmentService.GetDepartment(),

                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                pNs = await pNSService.GetPNS(),
                hRProjects = await hRProjectService.GetHRProject(),
                hRDoners = await hRDonerSevice.GetHRDoner(),
                hRActivities = await hRActivityService.GetHRActivity(),
            };
            return View(model);
        }

        // GET: Reporting
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FilteredEmployeeList(string qData, string org)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            string[] Tokens = qData.Split("|");
            List<string> qList = new List<string>();
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken.Length > 2) {
                        qList.Add(SepToken[0] + " = " + SepToken[1] + " To " + SepToken[2] );
                    }
                    else if (SepToken[0] == "Religion")
                    {
                        var religion = await religionMunicipalityService.GetReligionById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + religion.name);
                    } else if (SepToken[0] == "EmployeePosition")
                    {
                        var type = await typeService.GetEmployeeTypeById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + type.empType);
                    } else if (SepToken[0] == "Division")
                    {
                        var division = await addressService.GetDivisionById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + division.divisionName);
                    } else if (SepToken[0] == "District")
                    {
                        var division = await addressService.GetDistrictById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + division.districtName);
                    } else if (SepToken[0] == "Thana")
                    {
                        var division = await addressService.GetThanaById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + division.thanaName);
                    } else if (SepToken[0] == "Degree")
                    {
                        var degree = await degreeService.GetDegreeById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + degree.degreeName);
                    } else if (SepToken[0] == "Group")
                    {
                        var subject = await subjectService.GetSubjectById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + subject.subjectName);
                    } else if (SepToken[0] == "University")
                    {
                        var university = await organizationService.GetOrganizationById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + university.organizationName);
                    } else if (SepToken[0] == "SpouseHomeDistrict")
                    {
                        var division = await addressService.GetDistrictById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + division.districtName);
                    } else if (SepToken[0] == "Language")
                    {
                        var language = await membershipLanguageService.GetLanguageInfoById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + language.languageName);
                    }
                    else
                    {
                        qList.Add(SepToken[0] + " = " + SepToken[1]);
                    }
                    
                }
            }

            //foreach (string data in qList) Console.WriteLine("\n\n"+data+"\n\n");

            var model = new EmployeeReportViewModel
            {
                qList = qList,
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                employeeReports = await reports.GetEmployeeInfoAsQueryAble(qData, org)

            };
            return View(model);
        }


        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> FilteredWagesList(string qData, string org)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            string[] Tokens = qData.Split("|");
            List<string> qList = new List<string>();
            foreach (string token in Tokens)
            {
                string[] SepToken = token.Split("=");
                if (SepToken.Length > 1)
                {
                    if (SepToken.Length > 2)
                    {
                        qList.Add(SepToken[0] + " = " + SepToken[1] + " To " + SepToken[2]);
                    }
                    else if (SepToken[0] == "Religion")
                    {
                        var religion = await religionMunicipalityService.GetReligionById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + religion.name);
                    }
                    else if (SepToken[0] == "EmployeePosition")
                    {
                        var type = await typeService.GetEmployeeTypeById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + type.empType);
                    }
                    else if (SepToken[0] == "Division")
                    {
                        var division = await addressService.GetDivisionById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + division.divisionName);
                    }
                    else if (SepToken[0] == "District")
                    {
                        var division = await addressService.GetDistrictById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + division.districtName);
                    }
                    else if (SepToken[0] == "Thana")
                    {
                        var division = await addressService.GetThanaById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + division.thanaName);
                    }
                    else if (SepToken[0] == "Degree")
                    {
                        var degree = await degreeService.GetDegreeById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + degree.degreeName);
                    }
                    else if (SepToken[0] == "Group")
                    {
                        var subject = await subjectService.GetSubjectById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + subject.subjectName);
                    }
                    else if (SepToken[0] == "University")
                    {
                        var university = await organizationService.GetOrganizationById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + university.organizationName);
                    }
                    else if (SepToken[0] == "SpouseHomeDistrict")
                    {
                        var division = await addressService.GetDistrictById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + division.districtName);
                    }
                    else if (SepToken[0] == "Language")
                    {
                        var language = await membershipLanguageService.GetLanguageInfoById(Int32.Parse(SepToken[1]));
                        qList.Add(SepToken[0] + " = " + language.languageName);
                    }
                    else
                    {
                        qList.Add(SepToken[0] + " = " + SepToken[1]);
                    }

                }
            }

            //foreach (string data in qList) Console.WriteLine("\n\n"+data+"\n\n");

            var model = new EmployeeReportViewModel
            {
                qList = qList,
                fLang = _lang.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                employeeReports = await reports.GetWagesAsQueryAble(qData, org)

            };
            return View(model);
        }


        // API Section 
        #region API
        [AllowAnonymous]
        [Route("global/api/getEmployeeInfoAsQueryAble/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoAsQueryAble(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await reports.GetEmployeeInfoAsQueryAble(queryString, "ddm"));
        }


        [AllowAnonymous]
        [Route("global/api/getWagesAsQueryAble/{queryString}")]
        [HttpGet]
        public async Task<IActionResult> getWagesAsQueryAble(string queryString)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await reports.GetWagesAsQueryAble(queryString, "ddm"));
        }


        [AllowAnonymous]
        [Route("global/api/GenerateReport/{qData}")]
        [HttpGet]
        public async Task<IActionResult> GenerateReport(string qData)
        {
            ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, $"http://localhost:5000/HRPMSReport/Reporting/FilteredEmployeeList?qData={qData}&org={user.org}");

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        #endregion
    }
}