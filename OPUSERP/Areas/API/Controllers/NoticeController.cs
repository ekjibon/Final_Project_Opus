using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Notice.Interfaces;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers;
using OPUSERP.Areas.API.Models;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class NoticeController : ControllerBase
    {
        private readonly INoticeInfoService noticeInfoService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public NoticeController(IHostingEnvironment hostingEnvironment, IConverter converter, INoticeInfoService noticeInfoService)
        {
            this.noticeInfoService = noticeInfoService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        // GET: api/Notice
        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            return new OkObjectResult(await noticeInfoService.GetNoticeInformation());
        }

        // GET: api/Notice
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await noticeInfoService.GetNoticeInformationById(id));
        }
        
        // GET: api/Notice
        
        [HttpGet("pdf/{id}")]
        public async Task<IActionResult> pdf(int id)
        {
            var data = await noticeInfoService.GetNoticeInformationById(id);

            if (data == null) return BadRequest("Data Not exist");

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, $"Notice/Notice/NoticeView/{id}");

            if (status != "done")
            {

                return BadRequest("Something went wrong");
            }

            NoticeGetResponse notice = new NoticeGetResponse
            {
                Id = id,
                attatchment = data.url,
                title = data.subject,
                pdfUrl = "pdf/"+fileName
            };
            return new OkObjectResult(notice);
        }


    }
}