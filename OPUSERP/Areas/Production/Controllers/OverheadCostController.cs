using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Production.Models;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Production.Services.Interfaces;

namespace OPUSERP.Areas.Production.Controllers
{
    [Area("Production")]
    public class OverheadCostController : Controller
    {
        private readonly IBOMService boMService;

        public OverheadCostController(IBOMService boMService)
        {
            this.boMService = boMService;
        }

        public async Task<IActionResult> Index()
        {
            OverheadCostModel model = new OverheadCostModel
            {
                overheadCosts=await boMService.GetAllOverheadCost()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(OverheadCostModel model)
        {
            OverheadCost cost = new OverheadCost
            {
                Id=(int)model.overheadCostId,
                accountCode=model.accountCode,
                accountName=model.accountName
            };
            await boMService.SaveOverheadCost(cost);
            return RedirectToAction(nameof(Index));
        }

    }
}
