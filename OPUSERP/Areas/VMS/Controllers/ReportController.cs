using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.Helpers;
using OPUSERP.VMS.Services.FuelInfo.Interfaces;
using OPUSERP.VMS.Services.FuelStation.Interfaces;
using OPUSERP.VMS.Services.MasterData.Interfaces;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.VMS.Services.VehicleService.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class ReportController : Controller
    {
        private readonly IVMSReportService reportService;
        private readonly IVMSVehicleInfoService vehicleInfoService;
        private readonly IFuelStationInfoService fuelStationInfoService;
        private readonly IFuelInfoService fuelInfoService;
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;
        public string FileName;

        public ReportController(IHostingEnvironment hostingEnvironment, IVMSReportService reportService, IVMSVehicleInfoService vehicleInfoService, IFuelStationInfoService fuelStationInfoService, IFuelInfoService fuelInfoService, IVehicleServiceHistoryService vehicleServiceHistoryService, IConverter converter)
        {
            this.reportService = reportService;
            this.vehicleInfoService = vehicleInfoService;
            this.fuelStationInfoService = fuelStationInfoService;
            this.fuelInfoService = fuelInfoService;
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index()
        {
            BasicReportViewModel model = new BasicReportViewModel
            {
                vehicleInformation=await vehicleInfoService.GetVehicleInformation(),
                fuelStationInfos=await fuelStationInfoService.GetFuelStationInfo(),
                suppliers=await vehicleServiceHistoryService.GetSupplier()
            };
            return View(model);
        }

        //[HttpPost]
        //public IActionResult BasicReport([FromForm] BasicReportViewModel model)
        //{
        //    return View();
        //}


        #region PDF
        [AllowAnonymous]
        public IActionResult BasicReport(int rptType, int vid, int fuelStId, int vendorId, string fromDate, string toDate)
        {
            try
            {
                
                //$"http://localhost:5002/License/FinancialOrganization/PdfForOrganizationIfo/" + id + "?licenseTypeId" + licenseTypeId
                //$"http://localhost:5000/ReportWizard/Report/GetReportData/rptType=" + model.reportTypeId + "&vid=" + model.vehicleId + "&fuelStId=" + model.fuelStationId + "&vendorId=" + model.vendorId + "&fromDate=" + model.fromDate + "&toDate=" + model.toDate
                //string baseUrl = "http://localhost:5000/";
                string scheme = Request.Scheme;
                var host = Request.Host;
                string url = "";
                //string url = scheme + "://" + host + "/ReportWizard/Report/GetReportData?rptType=" + model.reportTypeId+ "&vid="+model.vehicleId+ "&fuelStId="+model.fuelStationId+ "&vendorId="+model.vendorId+ "&fromDate="+model.fromDate+"&toDate="+model.toDate;

                string fileName;
                if(rptType == 1)
                {
                    url = scheme + "://" + host + "/ReportWizard/Report/GetReportData?rptType=" + rptType + "&vid=" + vid + "&fuelStId=" + fuelStId + "&vendorId=" + vendorId + "&fromDate=" + fromDate + "&toDate=" + toDate;
                }
                if (rptType == 2)
                {
                    url = scheme + "://" + host + "/ReportWizard/Report/GetLogBookReportData?vid=" + vid + "&fromDate=" + fromDate + "&toDate=" + toDate;
                }


                string status = myPDF.GeneratePDF(out fileName, url);
                
                FileName = fileName;
                if (status != "done")
                {
                    return Content("<h1>" + status + "</h1>");
                }

                var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
                return new FileStreamResult(stream, "application/pdf");
            }
            catch(Exception ex)
            {
                return RedirectToAction(nameof(Index));
            }
            
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetReportData(int rptType,int vid,int fuelStId,int vendorId,string fromDate,string toDate)
        {
            if (rptType == 1)
            {
                ViewBag.Heading = "যানবাহন সার্ভিস রিপোর্ট";
                ViewBag.FromDate = fromDate;
                ViewBag.ToDate = toDate;
            }
            string userId = User.Identity.Name;
            IEnumerable<ServiceReportViewModel> serviceData = await reportService.GetServiceReportData(rptType, vid, fuelStId, vendorId, fromDate, toDate,userId);
            return PartialView(serviceData);
        }
        [AllowAnonymous]
        public async Task<IActionResult> GetLogBookReportData( int vid,  string fromDate, string toDate)
        {
            ViewBag.Heading = "লগ বই (টোক বই)";
            ViewBag.FromDate = fromDate;
            ViewBag.ToDate = toDate;
            string userId = User.Identity.Name;
            IEnumerable<LogBookReportViewModel> logBookData = await reportService.GetLogBookReportData(vid, fromDate, toDate, userId);
            return PartialView(logBookData);
        }

        #endregion
    }
}