using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class SourceController : Controller
    {
        private readonly ISourceService sourceService;

        public SourceController(ISourceService sourceService)
        {
            this.sourceService = sourceService;
        }
        public async Task<IActionResult> Source()
        {
            SourceViewModel model = new SourceViewModel()
            {
                sources = await sourceService.GetAllSource()
            };
            return View(model);
        }

        // POST: Source/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Source([FromForm] SourceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.sources = await sourceService.GetAllSource();
                return View(model);
            }

            Source data = new Source
            {
                Id = model.sourceId,
                sourceName = model.sourceName,
                createdBy = HttpContext.User.Identity.Name,
                createdAt = DateTime.Now
            };

            await sourceService.SaveSource(data);

            return RedirectToAction(nameof(Source));
        }
        public async Task<IActionResult> DeleteSourceById(int id)
        {
            try
            {
                await sourceService.DeleteSourceById(id);
                return RedirectToAction("Source", "Source", new { Area = "CRMMasterData" });
            }
            catch
            {
                return RedirectToAction("Source", "Source", new { Area = "CRMMasterData" });
            }
            
        }
    }
}