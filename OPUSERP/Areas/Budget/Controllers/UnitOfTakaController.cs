using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.Budget.Controllers
{
    [Authorize]
    [Area("Budget")]
    public class UnitOfTakaController : Controller
    {
        private readonly LangGenerate<UnitLn> _lang;
        private readonly IUnitOfTakaService unitOfTakaService;

        public UnitOfTakaController(IHostingEnvironment hostingEnvironment, IUnitOfTakaService unitOfTakaService)
        {
            _lang = new LangGenerate<UnitLn>(hostingEnvironment.ContentRootPath);
            this.unitOfTakaService = unitOfTakaService;
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            UnitOfTakaViewModel model = new UnitOfTakaViewModel
            {
                fLang = _lang.PerseLang("Budget/UnitEN.json", "Budget/UnitBN.json", Request.Cookies["lang"]),
                unitOfTakas = await unitOfTakaService.GetUnitOfTaka()
            };
            return View(model);
        }

        // POST: Country/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] UnitOfTakaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.unitOfTakas = await unitOfTakaService.GetUnitOfTaka();
                //return View(model);
                return Json("ok");
            }

            UnitOfTaka data = new UnitOfTaka
            {
                Id = model.UnitId ?? 0,
                UnitName = model.UnitName,
                value = model.value,
                status = 0
            };

            await unitOfTakaService.SaveUnitOfTaka(data);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Settings()
        {
            UnitOfTakaViewModel model = new UnitOfTakaViewModel
            {
                fLang = _lang.PerseLang("Budget/UnitEN.json", "Budget/UnitBN.json", Request.Cookies["lang"]),
                unitOfTakas = await unitOfTakaService.GetUnitOfTaka()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Settings([FromForm] UnitOfTakaViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.unitOfTakas = await unitOfTakaService.GetUnitOfTaka();
                return View(model);
            }

            if (model.status != null)
            {
                UnitOfTaka data = new UnitOfTaka { };
                for (int i = 0; i < model.status.Length; i++)
                {
                    data = new UnitOfTaka
                    {
                        Id = (int)model.status[i]
                    };
                }

                await unitOfTakaService.UpdateUnitOfTakaStatus(data);
            }
               
            return RedirectToAction(nameof(Settings));

        }
    }
}