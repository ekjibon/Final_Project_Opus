using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    public class JobAssignController : Controller
    {
        private readonly ICandidateInfoService candidateInfoService;
        public JobAssignController(ICandidateInfoService candidateInfoService)
        {
            this.candidateInfoService = candidateInfoService;
        }
        public async Task<IActionResult> Index()
        {
            CandidateInfoViewModel model = new CandidateInfoViewModel
            {
                candidateInfos = await candidateInfoService.GetCandidateInfo()
            };
            return View(model);
        }

        public IActionResult InterviewCalling()
        {
            return View();
        }

        public IActionResult Marking()
        {
            return View();
        }

        public IActionResult FinalSelection()
        {
            return View();
        }
    }
}