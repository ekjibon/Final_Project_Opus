using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Program.Models;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Programs.Service.Interface;

namespace OPUSERP.Areas.Program.Controllers
{
    [Area("Program")]
    public class ProgramReportController : Controller
    {
        private readonly IProgramCategoryService programCategoryService;

        private readonly IAddressService addressService;
        private readonly IProgramMasterService programMasterService;
        private readonly IProgramAttachmentService programAttachmentService;
        private readonly IProgramAttendeeService programAttendeeService;
        private readonly IProgramHeadlineService programHeadlineService;
        private readonly IProgramMainCategoryService programMainCategoryService;
        private readonly IProgramPeopleInDiscussionService programPeopleInDiscussionService;
        private readonly IProgramSubjectService programSubjectService;
        private readonly IProgramViewerService programViewerService;
        private readonly IProgramAddressService ProgramAddressService;
        private readonly IProgramReportService programReportService;
        private readonly IPhotographService photographService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;


        public ProgramReportController(IHostingEnvironment hostingEnvironment, IConverter converter, IProgramCategoryService programCategoryService, IAddressService addressService, IProgramMasterService programMasterService
            , IProgramAttachmentService programAttachmentService, IProgramAttendeeService programAttendeeService, IProgramHeadlineService programHeadlineService,
            IProgramMainCategoryService programMainCategoryService, IProgramPeopleInDiscussionService programPeopleInDiscussionService, IProgramSubjectService programSubjectService
            , IProgramViewerService programViewerService, IProgramAddressService ProgramAddressService, IPhotographService photographService, IProgramReportService programReportService)
        {
            this.programCategoryService = programCategoryService;
            this.addressService = addressService;
            this.programMasterService = programMasterService;
            this.programAttachmentService = programAttachmentService;
            this.programAttendeeService = programAttendeeService;
            this.programHeadlineService = programHeadlineService;
            this.programMainCategoryService = programMainCategoryService;
            this.programPeopleInDiscussionService = programPeopleInDiscussionService;
            this.programSubjectService = programSubjectService;
            this.programViewerService = programViewerService;
            this.ProgramAddressService = ProgramAddressService;
            this.photographService = photographService;
            this.programReportService = programReportService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;

        }

        [AllowAnonymous]
        public async Task<IActionResult> Index(int main, int cat, DateTime from, DateTime to)
        {
            var model = new ProgramMasterView
            {
                programReportListModel = await programReportService.programReportListModel(main, cat, from, to)
            };

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult IndexPdf(int main, int cat, DateTime from, DateTime to)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Program/ProgramReport/Index?main=" + main + "&cat=" + cat + "&from=" + from + "&to=" + to;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgramImage(int main, int cat, DateTime from, DateTime to)
        {
            var model = new ProgramMasterView
            {
                programReportListModel = await programReportService.programReportListModel(main, cat, from, to)
            };

            return View(model);
        }

        [AllowAnonymous]
        public IActionResult ProgramImagePdf(int main, int cat, DateTime from, DateTime to)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Program/ProgramReport/ProgramImage?main=" + main + "&cat=" + cat + "&from=" + from + "&to=" + to;

            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }


        public async Task<IActionResult> IndexAction()
        {
            var model = new ProgramMasterView
            {
                programMainCategories = await programMainCategoryService.GetProgramMainCategory()
            };

            return View(model);
        }


        [Route("global/api/getsubcategory/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> getsubcategory(int id)
        {
            return Json(await programCategoryService.GetProgramCategoryByCatId(id));
        }
    }
}