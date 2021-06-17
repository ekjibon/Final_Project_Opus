using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Services.CarManagement.Interfaces;

namespace OPUSERP.Areas.VMS.Controllers
{
    [Authorize]
    [Area("VMS")]
    public class SparePartController : Controller
    {
        private readonly ICarInfo carInfo;
        //private readonly LangGenerate<SparePartsLN> _lang;
        public SparePartController(IHostingEnvironment hostingEnvironment, ICarInfo carInfo)
        {
            this.carInfo = carInfo;
            //_lang = new LangGenerate<SparePartsLN>(hostingEnvironment.ContentRootPath);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var spareParts = await carInfo.GetSpareParts();
                
                var model = new SparePartsViewModel
                {
                    spareParts = spareParts,
                    //flang = _lang.PerseLang("CarManagement/SparePartsEN.json", "CarManagement/SparePartsBN.json", Request.Cookies["lang"]),
                };
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] SparePartsViewModel model)
        {
            string userId = HttpContext.User.Identity.Name;
            SpareParts spareParts = new SpareParts
            {
                Id = Convert.ToInt32(model.ID),
                brandName=model.brandName,
                modelName=model.modelName,
                partsName=model.partsName,
                createdBy = userId
            };
            await carInfo.SaveSpareParts(spareParts);
            return RedirectToAction(nameof(Index));
        }
        #region API

        [HttpGet]
        [Route("/global/api/GetSpareParts")]
        public async Task<JsonResult> GetSpareParts()
        {
            return Json(await carInfo.GetSpareParts());
        }

        #endregion

    }
}