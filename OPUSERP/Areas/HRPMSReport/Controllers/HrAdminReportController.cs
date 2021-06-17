using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Areas.HRPMSReport.Models;
using OPUSERP.Data.Entity;
using OPUSERP.ERPServices.MasterData.Interfaces;
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
    public class HrAdminReportController : Controller
    {
        private readonly IReports reports;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly LangGenerate<EmployeeInfoLn> _lang; 
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ERPServices.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService;
        private readonly ISpecialBranchUnitService specialBranchUnitService;
        private readonly IYearCourseTitleService yearCourseTitleService;
        private readonly IOrganizationService organizationService;
        private readonly ILevelofEducationService levelofEducationService;
        private readonly IBelongingsItemService belongingsItemService;
        private readonly ISubjectService subjectService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public HrAdminReportController(IReports reports, IHostingEnvironment hostingEnvironment,  UserManager<ApplicationUser> userManager, ERPServices.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService, IERPCompanyService eRPCompanyService, ISpecialBranchUnitService specialBranchUnitService, IBelongingsItemService belongingsItemService, ISubjectService subjectService, IOrganizationService organizationService, IYearCourseTitleService yearCourseTitleService, ILevelofEducationService levelofEducationService, IConverter converter)
        {
            _lang = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
                    
            this.designationDepartmentService = designationDepartmentService;
            this.specialBranchUnitService = specialBranchUnitService;
            this.subjectService = subjectService;
            this.belongingsItemService = belongingsItemService;
            this.organizationService = organizationService;
            this.levelofEducationService = levelofEducationService;
            this.yearCourseTitleService = yearCourseTitleService;
            this.eRPCompanyService = eRPCompanyService;
            _userManager = userManager;
            this.reports = reports;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<ActionResult> Index()
        {
            var model = new AllHrReportViewModel
            {
                specialBranchUnits = await specialBranchUnitService.GetSpecialBranchUnit(),
                subjects = await subjectService.GetAllSubject(),
                organizations = await organizationService.GetAllOrganization(),
                courseTitles = await yearCourseTitleService.GetCourseTitle(),
                levelofEducations = await levelofEducationService.GetAllLevelofEducation(),
                belongingItems=await belongingsItemService.GetBelongingItem()
            };
            return View(model);
        }

        #region Report
        [AllowAnonymous]
        public IActionResult HrReportDataByBelongingItem(string rptType, int? belongingId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "9")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByBelongingPdf?belongingId=" + belongingId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult HrReportDataByBelongingItemExcel(string rptType, int? belongingId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "9")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByBelongingPdf?belongingId=" + belongingId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }


            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public IActionResult HrReportDataByTrainingCourse(string rptType, int? courseId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "8")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByTrainingPdf?courseId=" + courseId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult HrReportDataByTrainingCourseExcel(string rptType, int? courseId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "8")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByTrainingPdf?courseId=" + courseId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }
        [AllowAnonymous]
        public IActionResult HrReportDataByEducation(string rptType, int? subjectId, int? universityId, int? loeId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "5")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportBySubjectPdf?subjectId=" + subjectId + "&universityId=" + universityId + "&loeId=" + loeId;
            }
            if (rptType == "6")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByUniversityPdf?subjectId=" + subjectId + "&universityId=" + universityId + "&loeId=" + loeId;
            }
            if (rptType == "7")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByLoePdf?subjectId=" + subjectId + "&universityId=" + universityId + "&loeId=" + loeId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult HrReportDataByEducationExcel(string rptType, int? subjectId, int? universityId, int? loeId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "5")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportBySubjectPdf?subjectId=" + subjectId + "&universityId=" + universityId + "&loeId=" + loeId;
            }
            if (rptType == "6")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByUniversityPdf?subjectId=" + subjectId + "&universityId=" + universityId + "&loeId=" + loeId;
            }
            if (rptType == "7")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByLoePdf?subjectId=" + subjectId + "&universityId=" + universityId + "&loeId=" + loeId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public IActionResult HrReportDataByDesig(string rptType, string desigId, int? deptId,string bloodGroup, int? sbuId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "1")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByDepartmentPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }
            if (rptType == "2")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByDesigPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }
            else if(rptType == "3")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByBloodPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }
            else if (rptType == "4")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataBySbuPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }
            else if (rptType == "10")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByDesigSummaryPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult HrReportDataByDesigExcel(string rptType, string desigId, int? deptId, string bloodGroup, int? sbuId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "1")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByDepartmentPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }
            if (rptType == "2")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportByDesigPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }
            else if (rptType == "3")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByBloodPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }
            else if (rptType == "4")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataBySbuPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }
            else if (rptType == "10")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByDesigSummaryPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup + "&sbuId=" + sbuId;
            }

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> HrReportByBelongingPdf(int? belongingId)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrBelongingReportViewModels = await reports.GetHrDataByBelongingItem(belongingId)
            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> HrReportByTrainingPdf( int? courseId)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrTrainingReportViewModels = await reports.GetHrDataByTrCourse(courseId)
            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> HrReportBySubjectPdf( int? subjectId, int? universityId, int? loeId)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrEducationReportViewModels = await reports.GetHrDataByEducation(subjectId, universityId, loeId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> HrReportByUniversityPdf(int? subjectId, int? universityId, int? loeId)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrEducationReportViewModels = await reports.GetHrDataByEducation(subjectId, universityId, loeId)
            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> HrReportByLoePdf(int? subjectId, int? universityId, int? loeId)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrEducationReportViewModels = await reports.GetHrDataByEducation(subjectId, universityId, loeId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> HrReportByDepartmentPdf(string desigId, int? deptId,string bloodGroup, int? sbuId)
        {
           
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrReportViewModels = await reports.GetHrDataByDesig(desigId, deptId, bloodGroup, sbuId)
            };
            
            return View(model);
        }
        [AllowAnonymous]
        public IActionResult HrReportDataBySummary(string rptType, string callName)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "10")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByDesigSummaryPdf?callName=" + callName;                
            }
            if (rptType == "11")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByDeptSummaryPdf?callName=" + callName;
            }
            if (rptType == "12")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByManpowerSummaryPdf?callName=" + callName;
            }

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult HrReportDataBySummaryExcel(string rptType, string callName)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

            if (rptType == "10")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByDesigSummaryPdf?callName=" + callName;
            }
            if (rptType == "11")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByDeptSummaryPdf?callName=" + callName;
            }
            if (rptType == "12")
            {
                url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByManpowerSummaryPdf?callName=" + callName;
            }

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            //var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            //return new FileStreamResult(stream, "application/pdf");
            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".xls");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToExcel(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> HrReportDataByDesigSummaryPdf(string callName)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrSummaryReportViewModels = await reports.GetHrSummaryData(callName)
            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> HrReportDataByDeptSummaryPdf(string callName)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrSummaryReportViewModels = await reports.GetHrSummaryData(callName)
            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> HrReportDataByManpowerSummaryPdf(string callName)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrSummaryReportViewModels = await reports.GetHrSummaryData(callName)
            };
            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> HrReportByDesigPdf(string desigId, int? deptId, string bloodGroup, int? sbuId)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrReportViewModels = await reports.GetHrDataByDesig(desigId, deptId, bloodGroup, sbuId)
            };
            return View(model);
        }

        //[AllowAnonymous]
        //public IActionResult HrReportDataByBlood(string desigId, int? deptId, string bloodGroup)
        //{
        //    string scheme = Request.Scheme;
        //    var host = Request.Host;

        //    string url = scheme + "://" + host + "/HRPMSReport/HrAdminReport/HrReportDataByBloodPdf?desigId=" + desigId + "&deptId=" + deptId + "&bloodGroup=" + bloodGroup;

        //    string fileName;
        //    string status = myPDF.GenerateLandscapePDF(out fileName, url);

        //    if (status != "done")
        //    {
        //        return Content("<h1>Something Went Wrong</h1>");
        //    }

        //    var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
        //    return new FileStreamResult(stream, "application/pdf");
        //}

        [AllowAnonymous]
        public async Task<ActionResult> HrReportDataByBloodPdf(string desigId, int? deptId, string bloodGroup, int? SbuId)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrReportViewModels = await reports.GetHrDataByDesig(desigId, deptId, bloodGroup, SbuId)
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> HrReportDataBySbuPdf(string desigId, int? deptId, string bloodGroup, int? SbuId)
        {
            AllHrReportViewModel model = new AllHrReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                hrReportViewModels = await reports.GetHrDataByDesig(desigId, deptId, bloodGroup, SbuId)
            };
            return View(model);
        }

        #endregion

        #region API

        [Route("global/api/GetAllHrDesignations/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllHrDesignations()
        {
            return Json(await designationDepartmentService.GetDesignations());
        }

        [Route("global/api/GetHrDataByDesig/{desigId}/{deptId}/{bloodGroup}/{sbuId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetHrDataByDesig(string desigId, int? deptId, string bloodGroup, int? sbuId)
        {
            return Json(await reports.GetHrDataByDesig(desigId, deptId, bloodGroup, sbuId));
        }
        [Route("global/api/GetHrDataByEducation/{subjectId}/{universityId}/{loeId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetHrDataByEducation(int? subjectId, int? universityId, int? loeId)
        {
            return Json(await reports.GetHrDataByEducation(subjectId, universityId, loeId));
        }
        [Route("global/api/GetHrDataByTrCourse/{courseId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetHrDataByTrCourse(int? courseId)
        {
            return Json(await reports.GetHrDataByTrCourse(courseId));
        }

        [Route("global/api/GetHrDataByBelongingItem/{belongingId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetHrDataByBelongingItem(int? belongingId)
        {
            return Json(await reports.GetHrDataByBelongingItem(belongingId));
        }

        #endregion
    }
}