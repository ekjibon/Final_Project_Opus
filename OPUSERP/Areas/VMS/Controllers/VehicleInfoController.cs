using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.CarManagement.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class VehicleInfoController : Controller
    {
        private readonly ICarInfo carInfo;
        //private readonly LangGenerate<VehicleInformationLN> _lang;
        public VehicleInfoController(IHostingEnvironment hostingEnvironment, ICarInfo carInfo)
        {
            this.carInfo = carInfo;
            //_lang = new LangGenerate<VehicleInformationLN>(hostingEnvironment.ContentRootPath);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var sourceType = await carInfo.GetSourceType();
                var vehicleInfo = await carInfo.GetVehicleInformation();

                var model = new VehicleInformationViewModelOld
                {
                    sourceTypes = sourceType,
                    vehicleInformations = vehicleInfo,
                    //flang = _lang.PerseLang("CarManagement/VehicleInformationEN.json", "CarManagement/VehicleInformationBN.json", Request.Cookies["lang"])
                };
                return View(model);
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] VehicleInformationViewModelOld model)
        {
            string userId = HttpContext.User.Identity.Name;
            VehicleInformation vehicleInformation = new VehicleInformation
            {
                Id = Convert.ToInt32(model.ID),
                //sourceTypeId = model.sourceTypeId,
                //typeOfVehicle=model.typeOfVehicle,
                //modelno = model.modelno,
                //brand = model.brand,
                //regNo = model.regNo,
                //mileage = model.mileage,
                //capacity=model.capacity,
                //licenseNo=model.licenseNo,
                //color=model.color,
                //engineCapacity=model.engineCapacity,
                //fuelType=model.fuelType,
                //taxToken=model.taxToken,
                //eToken=model.eToken,
                //chassisNo=model.chassisNo,
                createdBy = userId
            };
            await carInfo.SaveVehicleInfo(vehicleInformation);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<JsonResult> GetVehicleInfoListByTypeId(int sourceTypeId)
        {
            var data = await carInfo.GetVehicleInformationBySourceTypeID(sourceTypeId);
            return Json(data);
        }
    }
}