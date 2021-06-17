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
    public class ProductionInfoController : Controller
    {
        private readonly IProductionService productionService;
        private readonly IProductionPlanService productionPlanService;
        private readonly IBOMService bOMService;

        public ProductionInfoController(IProductionService productionService, IBOMService bOMService, IProductionPlanService productionPlanService)
        {
            this.productionService = productionService;
            this.productionPlanService = productionPlanService;
            this.bOMService = bOMService;
        }

        public async Task<IActionResult> Index()
        {
            ProductionViewModel model = new ProductionViewModel
            {
                productionMasters=await productionService.GetAllProductionInfo()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateProduction(int? id)
        {
            var production = await productionService.GetAllProductionInfo();
            string productionNo = ("P/"+DateTime.Now.Month+"/"+DateTime.Now.Year+"/"+(production.Count() + 1)).ToString();
            ProductionMaster productionMaster = new ProductionMaster();
            if (id > 0)
            {
                var master = await productionService.GetAllProductionInfoById(Convert.ToInt32(id));
                productionMaster.Id = master.Id;
                productionMaster.productionNo = master.productionNo;
                productionMaster.productionDate = master.productionDate;
                productionMaster.bomQty = master.bomQty;
                productionMaster.bOMId = master.bOMId;
                productionMaster.batchId = master.batchId;
                productionMaster.bomItem = master.bOM.itemSpecification.specificationName;
            }
            else
            {
                productionMaster.Id = 0;
                productionMaster.productionNo = productionNo;
                productionMaster.productionDate = DateTime.Now;
                productionMaster.bomQty = 0;
                productionMaster.bOMId = 0;
                productionMaster.batchId = 0;
                productionMaster.bomItem = string.Empty;
            }
            ProductionViewModel model = new ProductionViewModel
            {
                bOMMasters = await bOMService.GetAllBOMMaster(),
                productionId = productionMaster.Id,
                productionNo = productionMaster.productionNo,
                productionDate = productionMaster.productionDate,
                bomQty = productionMaster.bomQty,
                bOMId = productionMaster.bOMId,
                bomItem = productionMaster.bomItem,
                //productionPlans = await productionPlanService.GetProductionPlan()
                BatchId = productionMaster.batchId,
                productionBatch = await productionPlanService.GetProductionBatchById((int)productionMaster.batchId),
                productionPlans = await productionPlanService.GetProductionPlan()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> CreateProduction([FromForm] ProductionViewModel model)
        {
            var production = await productionService.GetAllProductionInfo();
            string productionNo = ("P/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (production.Count() + 1)).ToString();
            
            ProductionMaster master = new ProductionMaster
            {
                Id=Convert.ToInt32(model.productionId),
                productionNo= productionNo,
                productionDate=model.productionDate,
                bOMId=model.bOMId,
                batchId = model.BatchId,
                bomQty =model.bomQty,
                remarks=model.remarks,
                isClose=0,
                operationType="production"
            };
            int productionId = await productionService.SaveProductionMaster(master);

            if (model.productionId > 0)
            {
                await productionService.DeleteProductionDetailsbyProductionId(Convert.ToInt32(model.productionId));
            }

            List<ProductionDetails> lstDetails = new List<ProductionDetails>();
            for (int i = 0; i < model.bOMDetailId.Length; i++)
            {
                ProductionDetails details = new ProductionDetails
                {
                    Id=0,
                    productionId = productionId,
                    bOMDetailId = model.bOMDetailId[i],
                    qty=model.qty[i],
                    //defaultPrice=model.defaultPrice[i],
                    wastePercent=model.wastePercent[i]
                };
                lstDetails.Add(details);
            }
            await productionService.SaveProductionDetailList(lstDetails);
            return Json(productionId);
        }

        public async Task<IActionResult> ContractualProduction()
        {
            ProductionViewModel model = new ProductionViewModel
            {
                productionMasters = await productionService.GetAllContractualProductionInfo()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateContractualProduction(int? id)
        {
            var production = await productionService.GetAllContractualProductionInfo();
            string productionNo = ("CP/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (production.Count() + 1)).ToString();
            ProductionMaster productionMaster = new ProductionMaster();
            if (id > 0)
            {
                var master = await productionService.GetAllContractualProductionInfoById(Convert.ToInt32(id));
                productionMaster.Id = master.Id;
                productionMaster.productionNo = master.productionNo;
                productionMaster.productionDate = master.productionDate;
                productionMaster.bomQty = master.bomQty;
                productionMaster.bOMId = master.bOMId;
                productionMaster.bomItem = master.bOM.itemSpecification.specificationName;
                productionMaster.supplierId = master.supplierId;
                productionMaster.supplierName = master.supplier.organizationName;
            }
            else
            {
                productionMaster.Id = 0;
                productionMaster.productionNo = productionNo;
                productionMaster.productionDate = DateTime.Now;
                productionMaster.bomQty = 0;
                productionMaster.bOMId = 0;
                productionMaster.bomItem = string.Empty;
                productionMaster.supplierId = 0;
                productionMaster.supplierName = string.Empty;
            }
            ProductionViewModel model = new ProductionViewModel
            {
                bOMMasters = await bOMService.GetAllBOMMaster(),
                productionId = productionMaster.Id,
                productionNo = productionMaster.productionNo,
                productionDate = productionMaster.productionDate,
                bomQty = productionMaster.bomQty,
                bOMId = productionMaster.bOMId,
                bomItem = productionMaster.bomItem,
                supplierId= productionMaster.supplierId,
                supplierName= productionMaster.supplierName
            };
            return View(model);
        }
        [HttpPost]
        public async Task<JsonResult> CreateContractualProduction([FromForm] ProductionViewModel model)
        {
            var production = await productionService.GetAllContractualProductionInfo();
            string productionNo = ("CP/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (production.Count() + 1)).ToString();

            ProductionMaster master = new ProductionMaster
            {
                Id = Convert.ToInt32(model.productionId),
                productionNo = productionNo,
                productionDate = model.productionDate,
                bOMId = model.bOMId,
                bomQty = model.bomQty,
                remarks = model.remarks,
                supplierId=model.supplierId,
                isClose = 0,
                operationType = "contratual"
            };
            int productionId = await productionService.SaveProductionMaster(master);

            if (model.productionId > 0)
            {
                await productionService.DeleteProductionDetailsbyProductionId(Convert.ToInt32(model.productionId));
            }

            List<ProductionDetails> lstDetails = new List<ProductionDetails>();
            for (int i = 0; i < model.bOMDetailId.Length; i++)
            {
                ProductionDetails details = new ProductionDetails
                {
                    Id = 0,
                    productionId = productionId,
                    bOMDetailId = model.bOMDetailId[i],
                    qty = model.qty[i],
                    wastePercent = model.wastePercent[i]
                };
                lstDetails.Add(details);
            }
            await productionService.SaveProductionDetailList(lstDetails);
            return Json(productionId);
        }

        [Route("api/Production/GetBOMDetailsByBOMId/{id}/{qty}")]
        public async Task<JsonResult> GetBOMDetailsByBOMId(int id,decimal? qty)
        {
            var result = await productionService.GetBoMDetailsByBoMId(id,qty);
            return Json(result);
        }
    }
}