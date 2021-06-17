using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.CLUB.Services.Master.Interface;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Report.Interface;
using System.IO;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Area("MemberInfo")]
    public class MemberReportController : Controller
    {
        private readonly OPUSERP.Helpers.LangGenerate<EmployeeInfoLn> _lang;
        private readonly OPUSERP.Helpers.LangGenerate<GridViewLn> _lang1;
        private readonly IConverter converter;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;
        private readonly IReligionMunicipalityService religionMunicipalityService;

        private readonly IMemberTypeService memberTypeService;
        private readonly IOrganizationService organizationService;

        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IAddressService addressService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDegreeService degreeService;
        private readonly ISubjectService subjectService;



        private readonly ISpecialSkillService specialSkillService;


        private readonly IReportService reportService;
        private readonly IMemberTransferLogService memberTransferService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public MemberReportController(IHostingEnvironment hostingEnvironment, IConverter converter, IPersonalInfoService personalInfoService,
            IPhotographService photographService, IReligionMunicipalityService religionMunicipalityService,
            IMemberTypeService memberTypeService, IOrganizationService organizationService,
            IDesignationDepartmentService designationDepartmentService, IAddressService addressService,
            UserManager<ApplicationUser> userManager, IDegreeService degreeService, ISubjectService subjectService,
            ISpecialSkillService specialSkillService, IReportService reportService, IMemberTransferLogService memberTransferService)
        {
            _lang = new OPUSERP.Helpers.LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new OPUSERP.Helpers.LangGenerate<GridViewLn>(hostingEnvironment.ContentRootPath);
            this.converter = converter;
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            this.religionMunicipalityService = religionMunicipalityService;
            this.memberTypeService = memberTypeService;
            this.organizationService = organizationService;
            this.designationDepartmentService = designationDepartmentService;
            this.addressService = addressService;
            _userManager = userManager;
            this.degreeService = degreeService;
            this.subjectService = subjectService;
            this.specialSkillService = specialSkillService;
            this.reportService = reportService;
            this.memberTransferService = memberTransferService;
            this.myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index()
        {
            var model = new MemberInfoViewModel
            {
                memberTransferLogs = await memberTransferService.GetOrganization(),
                memberTypes = await memberTypeService.GetAllMemberType(),
            };

            return View(model);
        }

        public async Task<JsonResult> GetAllOrganizationList()
        {
            var model = await memberTransferService.GetOrganization();
            return Json(model);
        }

        public async Task<JsonResult> GetAllMemberTypeList()
        {
            var model = await memberTypeService.GetAllMemberType();
            return Json(model);
        }

        [AllowAnonymous]
        public IActionResult MemberReportPdf(int memberTypeId, int org)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;

            url = $"" + scheme + "://" + host + "/MemberInfo/MemberReport/MemberReport?memberTypeId=" + memberTypeId + "&org=" + org;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }
            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> MemberReport(int memberTypeId, int org)
        {
            //var company = await eRPCompanyService.GetAllCompany();
            MemberInfoViewModel model = new MemberInfoViewModel
            {
                //companies = company,
                memberInfoReportViewModels=await memberTransferService.OrganizationWiseMemberInfoReport(memberTypeId, org),
                //memberTransferLogs = await memberTransferService.GetOrganization(),
                memberTypes = await memberTypeService.GetAllMemberType(),
            };
            return View(model);
        }
    }
}