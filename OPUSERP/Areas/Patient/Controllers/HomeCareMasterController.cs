using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Patient.Models;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.Patient.Services.Interfaces;
using OPUSERP.SCM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.Patient.Controllers
{
    [Authorize]
    [Area("Patient")]
    public class HomeCareMasterController : Controller
    {
        private readonly IHomeCareService homeCareService;
        private readonly IItemsService itemsService;

        public HomeCareMasterController(IHomeCareService homeCareService, IItemsService itemsService)
        {
            this.homeCareService = homeCareService;
            this.itemsService = itemsService;
        }

        #region Disease

        public async Task<IActionResult> Disease()
        {
            DiseaseInfoViewModel model = new DiseaseInfoViewModel
            {
                diseaseInfos = await homeCareService.GetAllDiseaseInfo()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Disease([FromForm] DiseaseInfoViewModel model)
        {
            DiseaseInfo data = new DiseaseInfo
            {
                Id = model.diseaseId,
                diseaseName = model.diseaseName,
                diseaseDescription = model.diseaseDescription
            };

            await homeCareService.SaveDiseaseInfo(data);
            return RedirectToAction(nameof(Disease));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteDiseaseInfoById(int Id)
        {
            await homeCareService.DeleteDiseaseInfoById(Id);
            return Json(true);
        }

        #endregion

        #region Profession Type

        public async Task<IActionResult> ProfessionType()
        {
            ProfessionTypeViewModel model = new ProfessionTypeViewModel
            {
                professionTypes = await homeCareService.GetAllProfessionType()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProfessionType([FromForm] ProfessionTypeViewModel model)
        {
            ProfessionType data = new ProfessionType
            {
                Id = model.professionId,                
                professionName = model.professionName,
                professionDescription = model.professionDescription               
            };

            await homeCareService.SaveProfessionType(data);
            return RedirectToAction(nameof(ProfessionType));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteProfessionTypeById(int Id)
        {
            await homeCareService.DeleteProfessionTypeById(Id);
            return Json(true);
        }

        #endregion

    }
}