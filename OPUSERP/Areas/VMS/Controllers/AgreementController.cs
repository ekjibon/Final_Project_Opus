using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.Agreement;
using OPUSERP.VMS.Services.Agreement.Interfaces;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.VMS.Services.VehicleService.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class AgreementController : Controller
    {
        private readonly IVMSAgreementService agreementService;
        private readonly IVMSVehicleInfoService vehicleInfoService;
        private readonly IVehicleServiceHistoryService vehicleServiceHistoryService;
        //private readonly LangGenerate<AgreementInformationLN> _lang;

        public AgreementController(IHostingEnvironment hostingEnvironment, IVMSAgreementService agreementService, IVMSVehicleInfoService vehicleInfoService, IVehicleServiceHistoryService vehicleServiceHistoryService)
        {
            this.agreementService = agreementService;
            this.vehicleInfoService = vehicleInfoService;
            this.vehicleServiceHistoryService = vehicleServiceHistoryService;
            //_lang = new LangGenerate<AgreementInformationLN>(hostingEnvironment.ContentRootPath);
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            string userName = User.Identity.Name;
            AgreementInformationViewModel model = new AgreementInformationViewModel();
            var agreementInfo = await agreementService.GetAgreementInformationById(id);
            if (agreementInfo == null)
            {
                agreementInfo = new AgreementInformation();
            }
            else
            {
                model.agreementInfoId = agreementInfo.Id;
                model.vehicleId = agreementInfo.vehicleId;
                model.supplierId = agreementInfo.supplierId;
                model.agreementDate = agreementInfo.agreementDate;
                model.agreementBy = agreementInfo.agreementBy;
                model.expireDate = agreementInfo.expireDate;
            }
            
            model.costHeads = await agreementService.GetAllCostHead();
            model.vehicleInformation = await vehicleInfoService.GetVehicleInformation();
            model.suppliers = await vehicleServiceHistoryService.GetSupplier();
            model.limitAmountTypes = await vehicleServiceHistoryService.GetlimitAmountType();
            model.limitPeriodTypes = await vehicleServiceHistoryService.GetlimitPeriodType();
            
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveAgreementBasic([FromForm] AgreementInformationViewModel model)
        {
            try
            {
                string userName = User.Identity.Name;
                AgreementInformation agreementInformation = new AgreementInformation
                {
                    Id = Convert.ToInt32(model.agreementInfoId),
                    vehicleId = model.vehicleId,
                    supplierId = model.supplierId,
                    agreementDate = model.agreementDate,
                    expireDate = model.expireDate,
                    agreementBy=model.agreementBy,
                    createdBy = userName,
                };
                int masterId = await agreementService.SaveAgreementInformation(agreementInformation);
                

                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveCostInformation([FromForm] AgreementCostInformationViewModel model)
        {
            try
            {
                string userName = User.Identity.Name;
                if (model.costHeadId.Length > 0)
                {
                    await agreementService.DeleteAgreementCostInformationByAgreementId(Convert.ToInt32(model.agreementId));
                    for (int i = 0; i < model.costHeadId.Length; i++)
                    {
                        AgreementCostInformation agreement = new AgreementCostInformation
                        {
                            Id=0,
                            agreementId = Convert.ToInt32(model.agreementId),
                            costHeadId = model.costHeadId[i],
                            periodTypeId = model.periodTypeId[i],
                            amountTypeId = model.amountTypeId[i],
                            value = model.value[i],
                            createdBy = userName,
                        };
                        int masterId = await agreementService.SaveAgreementCostInformation(agreement);
                    }
                }
                return Json(true);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IActionResult> AgreementList()
        {
            AgreementInformationViewModel model = new AgreementInformationViewModel
            {
                agreements = await agreementService.GetAllAgreementInformation()
            };
            return View(model);
        }

        public JsonResult AgreementCostDetailsList(int id)
        {
            var result =agreementService.GetAgreementCostInformationByAgreementId(id);
            return Json(result);
        }
    }
}