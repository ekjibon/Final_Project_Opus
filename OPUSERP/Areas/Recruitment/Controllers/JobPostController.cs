using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    public class JobPostController : Controller
    {
        private readonly IJobRequisitionService jobRequisitionService;

        public JobPostController(IJobRequisitionService jobRequisitionService)
        {
            //_lang = new LangGenerate<ThanaInfoLn>(hostingEnvironment.ContentRootPath);
            this.jobRequisitionService = jobRequisitionService;
        }

        public async Task<IActionResult> Index()
        {
            //int reqId = 0;
            JobPostViewModel model = new JobPostViewModel
            {
                jobRequisitions = await jobRequisitionService.GetAllJobRequisition(),
                jobPosts = await jobRequisitionService.GetAllJobPost()
            };
            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Action([FromForm] JobPostViewModel model)
        {
            //ViewBag.jobReqId = model.jobReqId;
            int reqId = 0;
            if (!ModelState.IsValid)
            {
                //model.jobRequisitions = await jobRequisitionService.GetJobRequisitionByJobReqId(reqId);
                model.jobPosts = await jobRequisitionService.GetJobRequisitionByJobReqId(reqId);
                return View(model);
            }

            JobPost data = new JobPost
            {
                Id = (int)model.jobPostId,
                jobReqId = model.jobReqId,
                media = model.media,
                postDate = model.postingDate,
                deadline = model.deadline
            };

            await jobRequisitionService.SaveJobPost(data);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Apply(int id)
        {
            ViewBag.Id = id.ToString();
            //ViewBag.jobReqId = model.jobReqId;
            JobRequisitionViewModel model = new JobRequisitionViewModel
            {
                JobRequisition = await jobRequisitionService.GetJobRequisitionById(id)
            };
            return View(model);
        }
    }
}