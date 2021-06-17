using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VEMSMasterData.Models;
using OPUSERP.VEMS.Data.Entity.MasterData;
using OPUSERP.VEMS.Services.MasterData.Interface;

namespace OPUSERP.Areas.VEMSMasterData.Controllers
{
    [Area("VEMSMasterData")]
    public class RequiredDocController : Controller
    {
        private readonly IMasterDataServices masterDataServices;

        public RequiredDocController(IHostingEnvironment hostingEnvironment, IMasterDataServices masterDataServices)
        {
            this.masterDataServices = masterDataServices;
        }

        public async Task<ActionResult> Index()
        {
            RequeredDocumentViewModel model = new RequeredDocumentViewModel
            {
                requiredDocuments = await masterDataServices.GetRequiredDocument(),
            };
            return View(model);
        }

        public async Task<ActionResult> Create(int id)
        {
            RequeredDocumentViewModel model = new RequeredDocumentViewModel
            {
                reqDocId = id,
                requiredDocument = await masterDataServices.GetRequiredDocumentById(id),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] RequeredDocumentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            RequiredDocuments data = new RequiredDocuments
            {
                Id = Convert.ToInt32(model.reqDocId),
                description = model.description
            };
            var masterId = await masterDataServices.SaveRequiredDocument(data);

            return RedirectToAction(nameof(Index));
        }

        //xxxxxxxxxxxxxxxxx
    }
}