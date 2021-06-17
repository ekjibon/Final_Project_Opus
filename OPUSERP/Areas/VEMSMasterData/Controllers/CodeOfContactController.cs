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
    public class CodeOfContactController : Controller
    {
        private readonly IMasterDataServices masterDataServices;

        public CodeOfContactController(IHostingEnvironment hostingEnvironment, IMasterDataServices masterDataServices)
        {
            this.masterDataServices = masterDataServices;
        }

        public async Task<ActionResult> Index()
        {
            CodeOfContactViewModel model = new CodeOfContactViewModel
            {
                codeOfContacts = await masterDataServices.GetCodeOfContact(),
            };
            return View(model);
        }

        public async Task<ActionResult> Create(int id)
        {
            CodeOfContactViewModel model = new CodeOfContactViewModel
            {
                cocId = id,
                codeOfContact = await masterDataServices.GetCodeOfContactById(id),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([FromForm] CodeOfContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //return Json(model);

            CodeOfContact data = new CodeOfContact
            {
                Id = Convert.ToInt32(model.cocId),
                description = model.description
            };
            var masterId = await masterDataServices.SaveCodeOfContact(data);

            return RedirectToAction(nameof(Index));
        }

        //xxxxxxxxxxxxxxxxxxxx
    }
}