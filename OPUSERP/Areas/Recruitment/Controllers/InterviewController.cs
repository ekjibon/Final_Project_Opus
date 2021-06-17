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
    public class InterviewController : Controller
    {
        private readonly ICandidateInfoService candidateInfoService;
        private readonly IInterviewCallingService interviewCallingService;
        public InterviewController(IInterviewCallingService interviewCallingService, ICandidateInfoService candidateInfoService)
        {
            this.candidateInfoService = candidateInfoService;
            this.interviewCallingService = interviewCallingService;
        }

        public async Task<IActionResult> Index()
        {
            InterviewCallingViewModel model = new InterviewCallingViewModel
            {
                candidateInfos = await candidateInfoService.GetCandidateInfo(),
                interviewCallings = await interviewCallingService.GetInterviewCalling()
            };
            return View(model);
        }


        // POST: Interview/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] InterviewCallingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.interviewCallings = await interviewCallingService.GetInterviewCalling();
                return View(model);
            }

            InterviewCalling data = new InterviewCalling
            {
                jobRequisitionId = model.reqId,
                //jobPostId = model.jobId,
                candidateInfoId = model.candidateId,
                interviewType = model.interviewType,
                interviewDate = model.interviewDate,
                communicationType = model.communicationType,
                venue = model.venue,
                //dateOfBirth = Convert.ToDateTime(model.dateOfBirth),
                interviewTime = model.interviewTime,
                status = model.status
            };

            await interviewCallingService.SaveInterviewCalling(data);

            return RedirectToAction(nameof(Index));
        }
    }
}