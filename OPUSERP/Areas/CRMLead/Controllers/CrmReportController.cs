using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.Areas.HRPMSAttendence.Models.Lang;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Attendance.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Report;
using System;
using System.IO;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class CrmReportController : Controller
    {
        private readonly ILeadsService leadsService;
        private readonly IEmployeeInfoService employeeInfoService;
        private readonly IPersonalInfoService personalInfoService;        
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IUserInfoes userInfo;        
        private readonly HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService;
        private readonly ISectorService sectorService;
        private readonly ISourceService sourceService;
        private readonly IBankBranchService bankBranchService;
        private IReports reports;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public CrmReportController(IHostingEnvironment hostingEnvironment, IConverter converter, IUserInfoes userInfo, IPersonalInfoService personalInfoService, IEmployeeInfoService employeeInfoService, IReports reports, IERPCompanyService eRPCompanyService, HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService designationDepartmentService, ISectorService sectorService, ISourceService sourceService, IBankBranchService bankBranchService, ILeadsService leadsService)
        {
            this.leadsService = leadsService;
            this.employeeInfoService = employeeInfoService;
            this.personalInfoService = personalInfoService;
            this.reports = reports;
            this.eRPCompanyService = eRPCompanyService;
            this.userInfo = userInfo;
            this.sectorService = sectorService;
            this.sourceService = sourceService;
            this.bankBranchService = bankBranchService;
            this.designationDepartmentService = designationDepartmentService;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }        

        #region CRM Report

        public async Task<ActionResult> CrmReport()
        {
            CRMReportViewModel model = new CRMReportViewModel
            {                
                employeeInfos = await personalInfoService.GetEmployeeInfo(),
                sectors = await sectorService.GetAllSector(),
                sources = await sourceService.GetAllSource(),
                fITypes = await bankBranchService.GetAllFIType()
            };
            return View(model);
        }
        

        [AllowAnonymous]
        public IActionResult CrmReportData(string rptType, int? sectorId, int? fitypeId, int? sourceId, string leadOwner)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;
            string url = "";

            if (rptType == "1" )
            {
                url = scheme + "://" + host + "/CRMLead/CrmReport/CrmReportBySectorPdf?sectorId=" + sectorId + "&&fitypeId=" + fitypeId + "&&sourceId=" + sourceId + "&&leadOwner=" + leadOwner;
            }  
            else if (rptType == "2")
            {
                url = scheme + "://" + host + "/CRMLead/CrmReport/CrmReportByOrganizationPdf?sectorId=" + sectorId + "&&fitypeId=" + fitypeId + "&&sourceId=" + sourceId + "&&leadOwner=" + leadOwner;
            }
            else if (rptType == "3")
            {
                url = scheme + "://" + host + "/CRMLead/CrmReport/CrmReportBySourcePdf?sectorId=" + sectorId + "&&fitypeId=" + fitypeId + "&&sourceId=" + sourceId + "&&leadOwner=" + leadOwner;
            }
            else if (rptType == "4")
            {
                url = scheme + "://" + host + "/CRMLead/CrmReport/CrmReportByOwnerPdf?sectorId=" + sectorId + "&&fitypeId=" + fitypeId + "&&sourceId=" + sourceId + "&&leadOwner=" + leadOwner;
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
        public IActionResult CrmReportDataExcel(string rptType, int? sectorId, int? fitypeId, int? sourceId, string leadOwner)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;
            string url = "";

            if (rptType == "1")
            {
                url = scheme + "://" + host + "/CRMLead/CrmReport/CrmReportBySectorPdf?sectorId=" + sectorId + "&&fitypeId=" + fitypeId + "&&sourceId=" + sourceId + "&&leadOwner=" + leadOwner;
            }
            else if (rptType == "2")
            {
                url = scheme + "://" + host + "/CRMLead/CrmReport/CrmReportByOrganizationPdf?sectorId=" + sectorId + "&&fitypeId=" + fitypeId + "&&sourceId=" + sourceId + "&&leadOwner=" + leadOwner;
            }
            else if (rptType == "3")
            {
                url = scheme + "://" + host + "/CRMLead/CrmReport/CrmReportBySourcePdf?sectorId=" + sectorId + "&&fitypeId=" + fitypeId + "&&sourceId=" + sourceId + "&&leadOwner=" + leadOwner;
            }
            else if (rptType == "4")
            {
                url = scheme + "://" + host + "/CRMLead/CrmReport/CrmReportByOwnerPdf?sectorId=" + sectorId + "&&fitypeId=" + fitypeId + "&&sourceId=" + sourceId + "&&leadOwner=" + leadOwner;
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
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> CrmReportBySectorPdf(int? sectorId, int? fitypeId, int? sourceId, string leadOwner)
        {
            CRMReportViewModel model = new CRMReportViewModel
            {               
                companies = await eRPCompanyService.GetAllCompany(),
                leadReportViewModels = await leadsService.GetLeadReport(sectorId, fitypeId, sourceId, leadOwner),
            };           

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> CrmReportByOrganizationPdf(int? sectorId, int? fitypeId, int? sourceId, string leadOwner)
        {
            CRMReportViewModel model = new CRMReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leadReportViewModels = await leadsService.GetLeadReport(sectorId, fitypeId, sourceId, leadOwner),
            };

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> CrmReportBySourcePdf(int? sectorId, int? fitypeId, int? sourceId, string leadOwner)
        {
            CRMReportViewModel model = new CRMReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leadReportViewModels = await leadsService.GetLeadReport(sectorId, fitypeId, sourceId, leadOwner),
            };

            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> CrmReportByOwnerPdf(int? sectorId, int? fitypeId, int? sourceId, string leadOwner)
        {
            CRMReportViewModel model = new CRMReportViewModel
            {
                companies = await eRPCompanyService.GetAllCompany(),
                leadReportViewModels = await leadsService.GetLeadReport(sectorId, fitypeId, sourceId, leadOwner),
            };

            return View(model);
        }



        #endregion
    }
}