using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Authorize]
    [Area("Recruitment")]
    public class RcrtPublicationController : Controller
    {
        private readonly IRcrtPublicationService rcrtPublicationService;

        public RcrtPublicationController(IRcrtPublicationService rcrtPublicationService)
        {
            this.rcrtPublicationService = rcrtPublicationService;
        }

        // GET: Publication
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.candidateId = id.ToString();
            var model = new PublicationViewModel
            {
                candidateId = id.ToString(),
                rcrtPublications = await rcrtPublicationService.GetPublicationsByCandidateId(id)
            };
            return View(model);
        }

        // POST: Publication/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] PublicationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.candidateId = model.candidateId;
                model.rcrtPublications = await rcrtPublicationService.GetPublicationsByCandidateId(Int32.Parse(model.candidateId));
                return View(model);
            }

            RcrtPublication data = new RcrtPublication
            {
                Id = Int32.Parse(model.publicationId),
                candidateId = Int32.Parse(model.candidateId),
                publicationName = model.publicationName,
                publicationType = model.publicationType,
                publicationPlace = model.publicationPlace,
                publicationDate = model.publicationDate
            };

            await rcrtPublicationService.SaveRcrtPublication(data);
            //await personalInfoService.UpdateEmployeeinfoById(Int32.Parse(model.employeeID));
            return RedirectToAction(nameof(Index));
        }

        // Delete: Publication
        //public async Task<IActionResult> Delete(int id, int empId)
        //{
        //    await awardPublicationService.DeletePublicationById(id);
        //    //return RedirectToAction(nameof(Index));
        //    return RedirectToAction("Index", "Publication", new
        //    {
        //        id = empId
        //    });
        //}
    }
}