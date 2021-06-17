using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Grettings.Models;

namespace OPUSERP.Areas.Grettings.Controllers
{
    [Area("Grettings")]
    [Authorize]
    public class GrettingsController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;

        public GrettingsController(IPersonalInfoService personalInfoService)
        {
            this.personalInfoService = personalInfoService;
        }

        // GET: Grettings
        public async Task<IActionResult> Index()
        {
            GrettingsViewModel model = new GrettingsViewModel
            {
                //grettings = await personalInfoService.GetEmployeeGrettings(),
            };

            //return Json(model);
            return View(model);
        }

        // GET: Grettings/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Grettings/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grettings/Create
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

        // GET: Grettings/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Grettings/Edit/5
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

        // GET: Grettings/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Grettings/Delete/5
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