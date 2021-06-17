using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Helpers;

namespace OPUSERP.Areas.Budget.Controllers
{
    [Authorize]
    [Area("Budget")]
    public class BudgetHeadsController : Controller
    {
        private readonly LangGenerate<BudgetMainHeadLn> _lang;
        private readonly LangGenerate<BudgetHeadLn> _lang1;
        private readonly IBudgetHeadService budgetHeadService;
        private readonly ILedgerService ledgerService;

        public BudgetHeadsController(IHostingEnvironment hostingEnvironment, IBudgetHeadService budgetHeadService, ILedgerService ledgerService)
        {
            _lang = new LangGenerate<BudgetMainHeadLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new LangGenerate<BudgetHeadLn>(hostingEnvironment.ContentRootPath);
            this.budgetHeadService = budgetHeadService;
            this.ledgerService = ledgerService;
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            BudgetHeadViewModel model = new BudgetHeadViewModel
            {
                fLang1 = _lang1.PerseLang("Budget/BudgetHeadEN.json", "Budget/BudgetHeadBN.json", Request.Cookies["lang"]),
                budgetHeads = await budgetHeadService.GetBudgetHead(),
                budgetMainHeads = await budgetHeadService.GetBudgetMainHead()
            };
            return View(model);
        }

        // POST: Country/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] BudgetHeadViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    model.budgetHeads = await budgetHeadService.GetBudgetHead();
            //    return View(model);
            //}

            BudgetHead data = new BudgetHead
            {
                Id = model.headId ?? 0,
                code = model.code,
                name = model.name,
                codeBN = model.codeBN,
                nameBN = model.nameBN,
                budgetSubHeadId = model.budgetSubHeadId
            };

            int budgetid = await budgetHeadService.SaveBudgetHead(data);
            if (budgetid != 0)
            {
                await budgetHeadService.DeleteBudgetHeadDetailByHeadId(budgetid);
                if (model.ids != null)
                {
                    for (var i = 0; i < model.ids.Length; i++)
                    {
                        BudgetHeadDetail data1 = new BudgetHeadDetail
                        {
                            ledgerId = model.ids[i],
                            budgetHeadId = budgetid,
                        };
                        await budgetHeadService.SaveBudgetHeadDetail(data1);
                    }
                }


            }
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> MainHead()
        {
            BudgetMainHeadViewModel model = new BudgetMainHeadViewModel
            {
                fLang = _lang.PerseLang("Budget/BudgetMainHeadEN.json", "Budget/BudgetMainHeadBN.json", Request.Cookies["lang"]),
                budgetMainHeads = await budgetHeadService.GetBudgetMainHead()
            };
            return View(model);
        }

        // POST: Country/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> MainHead([FromForm] BudgetMainHeadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.budgetMainHeads = await budgetHeadService.GetBudgetMainHead();
                return View(model);
            }

            BudgetMainHead data = new BudgetMainHead
            {
                Id = model.headId ?? 0,
                code = model.code,
                name = model.name,
                codeBN = model.codeBN,
                nameBN = model.nameBN
            };

            await budgetHeadService.SaveBudgetMainHead(data);

            return RedirectToAction(nameof(MainHead));

        }
        
        [Route("global/api/GetBudgetHeadDetailbyHeadId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetBudgetHeadDetailbyHeadId(int Id)
        {
            return Json(await budgetHeadService.GetBudgetHeadDetailByHeadId(Id));
        }
        [Route("global/api/GetLedgerWithoutSubLedger/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetLedgerWithoutSubLedger()
        {
            var data = await ledgerService.GetLedgerWithoutSubLedger();
            return Json(data);
            //return Json(data.Where(x=>x.group.natureId==4));
        }
    }
}