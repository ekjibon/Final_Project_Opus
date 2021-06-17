using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class MasterJobListController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IPersonalInfoService personalInfoService;
        private readonly ILeadsService leadsService;
       
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public MasterJobListController(IHostingEnvironment hostingEnvironment, IDistributeJobService distributeJobService, IPersonalInfoService personalInfoService, ILeadsService leadsService, IConverter converter)
        {
            this.distributeJobService = distributeJobService;
            this._hostingEnvironment = hostingEnvironment;
            this.personalInfoService = personalInfoService;
            this.leadsService = leadsService;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        public async Task<IActionResult> Index()
        {
            MasterJobDataViewModel model = new MasterJobDataViewModel()
            {
                leads = await leadsService.GetAllLeads(),
                jobStatuses = await distributeJobService.GetAllJobStatus(),
               

            };
            return View(model);
        }
        [Route("global/api/GetAllLeadsdd")]
        [HttpGet]
        public async Task<IActionResult> GetAllLeadsdd()
        {
            return Json(await leadsService.GetAllLeads());
        }

        [Route("global/api/Getmasterdata")]
        [HttpGet]
        public async Task<IActionResult> Getmasterdata()
        {
          //  string FromDate,string Todate,string TeamLeader,string Fa,string BD,int LeadId,int JobstatusId,string ratingType
            return Json(await distributeJobService.GetLeadMasterJob(Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"), Convert.ToDateTime(DateTime.Now).ToString("yyyyMMdd"),"","","","",0,""));
        }
        [Route("global/api/GetOperationStatus/{Id}")]
        [HttpGet]
        public async Task<IActionResult> GetOperationStatus(int Id)
        {
          //  string FromDate,string Todate,string TeamLeader,string Fa,string BD,int LeadId,int JobstatusId,string ratingType
            return Json(await distributeJobService.GetStatusLogByOperationMasterId(Id));
        }
        [Route("global/api/Getmasterdatafilter/{FromDate}/{Todate}/{TeamLeader}/{Fa}/{BD}/{LeadId}/{JobstatusId}/{ratingType}")]
        [HttpGet]
        public async Task<IActionResult> Getmasterdatafilter(string FromDate, string Todate, string TeamLeader, string Fa, string BD, string LeadId, int JobstatusId, string ratingType)
        {

            string FDate = FromDate;// Convert.ToDateTime(FromDate).ToString("yyyyMMdd");
            string TDate = Todate;//Convert.ToDateTime(Todate).ToString("yyyyMMdd");
            if (FDate == "NoData")
            {
                FDate = "";
            }
            else
            {
                FDate = Convert.ToDateTime(FromDate).ToString("yyyyMMdd");
            }
            if (TDate == "NoData")
            {
                TDate = "";
            }
            else
            {
                TDate = Convert.ToDateTime(Todate).ToString("yyyyMMdd");
            }
            if (TeamLeader=="NoData")
            {
                TeamLeader = "";
            }
            if (Fa == "NoData")
            {
                Fa = "";
            }
            if (BD == "NoData")
            {
                BD = "";
            }
            if (ratingType == "NoData")
            {
                ratingType = "";
            }
            if (LeadId == "NoData")
            {
                LeadId = "";
            }
            //  string FromDate,string Todate,string TeamLeader,string Fa,string BD,int LeadId,int JobstatusId,string ratingType
            return Json(await distributeJobService.GetLeadMasterJob(FDate,TDate, TeamLeader, Fa, BD, LeadId, JobstatusId, ratingType));
        }
        //[Route("global/api/Getmasterdatafilter/{FromDate}/{Todate}/{TeamLeader}/{Fa}/{BD}/{LeadId}/{JobstatusId}/{ratingType}")]
        // [HttpGet]
        [AllowAnonymous]
        public IActionResult JoblistAction(string FromDate, string Todate, string TeamLeader, string Fa, string BD, string LeadId, int JobstatusId, string ratingType)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string url = "";
            string fileName;

           
                url = scheme + "://" + host + "/CRO/MasterJobList/Joblist?FromDate=" + FromDate+ "&Todate="+ Todate + "&TeamLeader=" + TeamLeader + "&Fa=" + Fa + "&BD=" + BD + "&LeadId=" + LeadId + "&JobstatusId=" + JobstatusId + "&ratingType=" + ratingType;
          

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
        public async Task<IActionResult> Joblist(string FromDate, string Todate, string TeamLeader, string Fa, string BD, string LeadId, int JobstatusId, string ratingType)
        {
            string FDate = FromDate;
            string TDate = Todate;
           // string FDate = Convert.ToDateTime(FromDate).ToString("yyyyMMdd");
          //  string TDate = Convert.ToDateTime(Todate).ToString("yyyyMMdd");
            if (FDate == "NoData")
            {
                FDate = "";
            }
            else
            {
                FDate = Convert.ToDateTime(FromDate).ToString("yyyyMMdd");
            }
            if (TDate == "NoData")
            {
                TDate = "";
            }
            else
            {
                TDate = Convert.ToDateTime(Todate).ToString("yyyyMMdd");
            }
            if (TeamLeader=="NoData")
            {
                TeamLeader = "";
            }
            if (Fa == "NoData")
            {
                Fa = "";
            }
            if (BD == "NoData")
            {
                BD = "";
            }
            if (ratingType == "NoData")
            {
                ratingType = "";
            }
            if (LeadId == "NoData")
            {
                LeadId = "";
            }
            //  string FromDate,string Todate,string TeamLeader,string Fa,string BD,int LeadId,int JobstatusId,string ratingType
            // return Json(await distributeJobService.GetLeadMasterJob(FDate,TDate, TeamLeader, Fa, BD, LeadId, JobstatusId, ratingType));
            MasterJobDataViewModel model = new MasterJobDataViewModel()
            {
                leads = await leadsService.GetAllLeads(),
                jobStatuses = await distributeJobService.GetAllJobStatus(),
                masterJobViewModels= await distributeJobService.GetLeadMasterJob(FDate, TDate, TeamLeader, Fa, BD, LeadId, JobstatusId, ratingType)

            };
            return View(model);
        }
    }
}