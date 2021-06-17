using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSRecruitment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSRecruitment.Controllers
{
    [Authorize]
    [Area("HRPMSRecruitment")]
    public class CallForExamController : Controller
    {
        // GET: CallForExam
        public ActionResult Index()
        {
            return View();
        }

        // POST: CreateJobCircular/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] CallForExamViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}