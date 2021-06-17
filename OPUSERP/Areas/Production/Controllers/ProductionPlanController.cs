using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Production.Models;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Production.Services.Interfaces;

namespace OPUSERP.Areas.Production.Controllers
{
    [Area("Production")]
    public class ProductionPlanController : Controller
    {
        private readonly IProductionPlanService productionPlanService;
        private readonly IBOMService bOMService;

        public ProductionPlanController(IProductionPlanService productionPlanService, IBOMService bOMService)
        {
            this.productionPlanService = productionPlanService;
            this.bOMService = bOMService;
        }

        public async Task<IActionResult> Index(int? id)
        {
            var plan = await productionPlanService.GetProductionPlan();
            string productionNo = ("PPlan/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();
            ProductionPlan productionPlan = new ProductionPlan();
            if (id > 0)
            {
                var master = await productionPlanService.GetProductionPlanById(Convert.ToInt32(id));
                productionPlan.Id = master.Id;
                productionPlan.planNumber = master.planNumber;
                productionPlan.planDate = master.planDate;
                productionPlan.targetDate = master.targetDate;
                productionPlan.quantity = master.quantity;
                productionPlan.bOMMasterId = master.bOMMasterId;
            }
            else
            {
                productionPlan.Id = 0;
                productionPlan.planNumber = productionNo;
                productionPlan.planDate = DateTime.Now;
                productionPlan.targetDate = DateTime.Now;
                productionPlan.quantity = 0;
                productionPlan.bOMMasterId = null;
            }

            ProductionPlanViewModel model = new ProductionPlanViewModel
            {
                bOMMasters = await bOMService.GetAllBOMMaster(),
                planId = productionPlan.Id,
                planNumber = productionPlan.planNumber,
                planDate = productionPlan.planDate,
                targetDate = productionPlan.targetDate,
                quantity = productionPlan.quantity,
                bOMMasterId = productionPlan.bOMMasterId
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] ProductionPlanViewModel model)
        {
            var plan = await productionPlanService.GetProductionPlan();
            string productionNo = ("PPlan/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            if (model.planId > 0)
            {
                productionNo = model.planNumber;
            }

            if (model.batchDates == null)
            {
                ModelState.AddModelError(string.Empty, "Have to Add minimum 1 Batch");
                model.bOMMasters = await bOMService.GetAllBOMMaster();
                model.planId = 0;
                model.planNumber = productionNo;
                model.planDate = DateTime.Now;
                model.targetDate = DateTime.Now;
                model.bOMMasterId = model.bOMMasterId;
                model.quantity = 0;
                return View(model);
            }
            ProductionPlan master = new ProductionPlan
            {
                Id = Convert.ToInt32(model.planId),
                planNumber = productionNo,
                planDate = model.planDate,
                targetDate = model.targetDate,
                quantity = model.quantity,
                bOMMasterId = model.bOMMasterId,
            };
            int productionId = await productionPlanService.SaveProductionPlan(master);

            if (model.planId > 0)
            {
                await productionPlanService.DeleteProductionBatchByPlanId(Convert.ToInt32(model.planId));
            }
            decimal? diff = 0;
            for (int i = 0; i < model.batchDates.Length; i++)
            {
                ProductionBatch details = new ProductionBatch
                {
                    Id = 0,
                    productionPlanId = productionId,
                    batchNumber = productionNo + "/Batch/"+ (i+1),
                    startDate = model.batchDates[i],
                    quantity = model.batchQuantitys[i],
                    perDayTargetQuantity = model.perDayTargetQuantitys[i],
                    expectedEndDate = model.batchTargetDate[i]
                };
                await productionPlanService.SaveProductionBatch(details);
            }
            return RedirectToAction(nameof(ProductPlanList));
        }

        public async Task<IActionResult> ProductPlanList()
        {
            ProductionPlanViewModel model = new ProductionPlanViewModel
            {
                productionPlans = await productionPlanService.GetProductionPlan()
            };
            return View(model);
        }

        [Route("api/Production/GetProductionBatchByPlanId/{id}")]
        [AllowAnonymous]
        public async Task<JsonResult> GetProductionBatchByPlanId(int id)
        {
            var result = await productionPlanService.GetProductionBatchByPlanId(id);
            return Json(result);
        }

        [Route("api/Production/GetProductionBatchById/{id}")]
        [AllowAnonymous]
        public async Task<JsonResult> GetProductionBatchById(int id)
        {
            var result = await productionPlanService.GetProductionBatchById(id);
            return Json(result);
        }
    }
}