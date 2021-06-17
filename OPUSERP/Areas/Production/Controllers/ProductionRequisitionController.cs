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
    public class ProductionRequisitionController : Controller
    {
        private readonly IProductionService productionService;
        private readonly IProductionRequisitionService productionRequisitionService;
        private readonly IProductionPlanService productionPlanService;
        private readonly IBOMService bOMService;

        public ProductionRequisitionController(IProductionService productionService, IProductionPlanService productionPlanService, IProductionRequisitionService productionRequisitionService, IBOMService bOMService)
        {
            this.productionService = productionService;
            this.productionRequisitionService = productionRequisitionService;
            this.productionPlanService = productionPlanService;
            this.bOMService = bOMService;
        }

        public async Task<IActionResult> Index()
        {
            ProductionRequsitionViewModel model = new ProductionRequsitionViewModel
            {
                productionRequsitionMasters = await productionRequisitionService.GetProductionRequsitionMaster()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateRequisition(int? id)
        {
            var RequsitionMaster = await productionRequisitionService.GetProductionRequsitionMaster();
            string requsitionNumber = ("PReq/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (RequsitionMaster.Count() + 1)).ToString();
            ProductionRequsitionMaster productionRequsitionMaster = new ProductionRequsitionMaster();
            if (id > 0)
            {
                var master = await productionRequisitionService.GetProductionRequsitionMasterById(Convert.ToInt32(id));
                productionRequsitionMaster.Id = master.Id;
                productionRequsitionMaster.requsitionNumber = master.requsitionNumber;
                productionRequsitionMaster.date = master.date;
                productionRequsitionMaster.requisitionQty = master.requisitionQty;
                productionRequsitionMaster.productionPlanId = master.productionPlanId;
            }
            else
            {
                productionRequsitionMaster.Id = 0;
                productionRequsitionMaster.requsitionNumber = requsitionNumber;
                productionRequsitionMaster.date = DateTime.Now;
                productionRequsitionMaster.requisitionQty = 0;
                productionRequsitionMaster.productionPlanId = 0;
            }

            ProductionRequsitionViewModel model = new ProductionRequsitionViewModel
            {
                requisitionId = productionRequsitionMaster.Id,
                requisitionNo = productionRequsitionMaster.requsitionNumber,
                requisitionDate = productionRequsitionMaster.date,
                planId = productionRequsitionMaster.productionPlanId,
                requisitionQty = productionRequsitionMaster.requisitionQty,
                productionPlans = await productionPlanService.GetProductionPlan(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateRequisition([FromForm] ProductionRequsitionViewModel model)
        {
            var RequsitionMaster = await productionRequisitionService.GetProductionRequsitionMaster();
            string requsitionNumber = ("PReq/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (RequsitionMaster.Count() + 1)).ToString();
            if (model.requisitionId > 0)
            {
                requsitionNumber = model.requisitionNo;
            }

            ProductionRequsitionMaster master = new ProductionRequsitionMaster
            {
                Id = Convert.ToInt32(model.requisitionId),
                requsitionNumber = requsitionNumber,
                date = model.requisitionDate,
                productionPlanId = model.planId,
                productionBatchId=model.BatchId,
                requisitionQty = model.requisitionQty,
                isClose = 0,
                isStockClose = 0
            };
            int requisitionId = await productionRequisitionService.SaveProductionRequsitionMaster(master);

            if (model.requisitionId > 0)
            {
                await productionRequisitionService.DeleteProductionRequsitionDetailsByMasterId(Convert.ToInt32(model.requisitionId));
            }

            List<ProductionRequsitionDetails> lstDetails = new List<ProductionRequsitionDetails>();
            for (int i = 0; i < model.bOMDetailId.Length; i++)
            {
                ProductionRequsitionDetails details = new ProductionRequsitionDetails
                {
                    Id = 0,
                    productionRequsitionMasterId = requisitionId,
                    BOMDetailId = model.bOMDetailId[i],
                    requsitionQuantity = model.qty[i],
                };
                await productionRequisitionService.SaveProductionRequsitionDetails(details);
            }            
            return Json(requisitionId);
        }

        //xxxxxxxxxxxxxxxxxxx
    }
}