using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSAssignment.Helpers;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Authorize]
    [Area("Recruitment")]
    public class RcrtTrainingController : Controller
    {
        private readonly IAddressService addressService;
        private readonly ITrainingService trainingService;
        private readonly IRcrtTrainingService rcrtTrainingService;
        private readonly ICandidateInfoService candidateInfoService;

        public RcrtTrainingController(ICandidateInfoService candidateInfoService, IAddressService addressService, ITrainingService trainingService, IRcrtTrainingService rcrtTrainingService)
        {
            this.candidateInfoService = candidateInfoService;
            this.addressService = addressService;
            this.trainingService = trainingService;
            this.rcrtTrainingService = rcrtTrainingService;
        }
        // GET: Training
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new TrainingViewModel
            {
                candidateId = id,
                countries = await addressService.GetAllContry(),
                trainingCategories = await trainingService.GetTrainingCategories(),
                trainingInstitutes = await trainingService.GetTrainingInstitute(),
                rCRTTrainingLogs = await rcrtTrainingService.GetTraningLogListById(id),
            };
            //return Json(model);
            return View(model);
        }

        // POST: Training/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] TrainingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            RCRTTrainingLog data = new RCRTTrainingLog
            {
                Id = model.trainingLogID,
                candidateId = model.candidateId,
                fromDate = model.fromDate,
                toDate = model.toDate,
                // countryId = Int32.Parse(model.country),
                trainingCategoryId = Int32.Parse(model.category),
                trainingInstituteId = Int32.Parse(model.institute),
                trainingTitle = model.trainingTitle,
                remarks = model.remarks,
                //sponsoringAgency = model.sponsoringAgency
            };

            await rcrtTrainingService.SaveRcrtTraning(data);

            return RedirectToAction("Index", "RcrtTraining", new
            {
                id = model.candidateId
            });
        }

        // Delete: Language
        //public async Task<IActionResult> Delete(int id, int empId)
        //{
        //    await traningHistoryService.DeleteTraningHistoryById(id);
        //    //return RedirectToAction(nameof(Index));
        //    return RedirectToAction("Index", "Training", new
        //    {
        //        id = empId
        //    });
        //}
    }
}