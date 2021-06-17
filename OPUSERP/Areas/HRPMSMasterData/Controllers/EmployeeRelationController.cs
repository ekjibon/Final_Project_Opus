using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.HRPMSMasterData.Models;
using OPUSERP.Areas.HRPMSMasterData.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSMasterData.Controllers
{
    [Authorize]
    [Area("HRPMSMasterData")]
    public class EmployeeRelationController : Controller
    {
        private readonly LangGenerate<EmployeeRelationLn> _lang;
        // GET: EmployeeRelation
        private readonly IRelationService relationService;

        public EmployeeRelationController(IHostingEnvironment hostingEnvironment, IRelationService relationService)
        {
            _lang = new LangGenerate<EmployeeRelationLn>(hostingEnvironment.ContentRootPath);
            this.relationService = relationService;
        }
        // GET: EmployeeRelation
        public async Task<IActionResult> Index()
        {
            RelationViewModel model = new RelationViewModel
            {
                fLang = _lang.PerseLang("MasterData/RelationEN.json", "MasterData/RelationBN.json", Request.Cookies["lang"]),
                relations = await relationService.GetEmployeeRelations()
            };
            return View(model);
        }

        // POST: EmployeeRelation/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] RelationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.fLang = _lang.PerseLang("MasterData/RelationEN.json", "MasterData/RelationBN.json", Request.Cookies["lang"]);
                model.relations = await relationService.GetEmployeeRelations();
                return View(model);
            }

            Relation data = new Relation
            {
                Id = model.relationId,
                relationName = model.relationName,
                relationNameBn = model.relationNameBn,
                relationShortName = model.relationShortName
            };

            await relationService.SaveEmployeeRelation(data);

            return RedirectToAction(nameof(Index));
        }
    }
}