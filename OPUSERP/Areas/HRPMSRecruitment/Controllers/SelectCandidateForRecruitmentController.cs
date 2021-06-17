﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSRecruitment.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSRecruitment.Controllers
{
    [Area("HRPMSRecruitment")]
    public class SelectCandidateForRecruitmentController : Controller
    {
        // GET: SelectCandidateForRecruitment
        public ActionResult Index()
        {
            return View();
        }
        
        // POST: CreateJobCircular/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Index([FromForm] SelectCandidateForRecruitmentViewModel model)
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