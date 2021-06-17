using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSAssignment.Helpers;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSReward.Models;
using OPUSERP.Areas.HRPMSReward.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.RewardInfo;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.RewardInfo.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSReward.Controllers
{
    [Authorize]
    [Area("HRPMSReward")]
    public class RewardInfoController : Controller
    {
        private readonly LangGenerate<RewardLn> _lang;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IRewardService rewardService;

        public RewardInfoController(IHostingEnvironment hostingEnvironment, IPersonalInfoService personalInfoService, IRewardService rewardService)
        {
            _lang = new LangGenerate<RewardLn>(hostingEnvironment.ContentRootPath);
            this.personalInfoService = personalInfoService;
            this.rewardService = rewardService;
        }

        // GET: Punishment/CPEmployeeList
        public async Task<IActionResult> RewardEmployeeList()
        {
            var model = new EmployeeInfoViewModel
            {
                fLang2 = _lang.PerseLang("Reward/RewardEmpListBN.json"),
                allEmployeeInfos = await personalInfoService.GetEmployeeInfo()
            };
            return View(model);
        }

        // GET: RewardInfo
        public async Task<ActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new RewardViewModel
            {
                //fLang = _lang.PerseLang("Assignment/AssignmentEN.json"), 
                rewards = await rewardService.GetRewardByEmpId(id)
            };
            return View(model);
        }

        // POST: RewardInfo/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RewardViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeId.ToString();
                model.rewards = await rewardService.GetRewardByEmpId(model.employeeId);
                return View(model);
            }

            HRPMS.Data.Entity.RewardInfo.Reward data = new HRPMS.Data.Entity.RewardInfo.Reward
            {
                Id = Convert.ToInt32(model.rewardId),
                employeeId = Convert.ToInt32(model.employeeId),
                letterNo = model.letterNo,
                rewardDate = model.rewardDate,
                rewardName = model.rewardName,
                remarks = model.remarks
            };

            int lstId = await rewardService.SaveReward(data);

            return RedirectToAction(nameof(Index), new { @id = model.employeeId });
        }

        // GET: RewardInfo/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RewardInfo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RewardInfo/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
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

        // GET: RewardInfo/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RewardInfo/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RewardInfo/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RewardInfo/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}