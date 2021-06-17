using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//using UMBRELLA.Areas.MasterData.Models.Lang;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Areas.MasterData.Models;
using Microsoft.AspNetCore.Authorization;

namespace OPUSERP.Areas.MasterData.Controllers
{
    [Area("MasterData")]
    public class AddressTypeController : Controller
    {
        //private readonly LangGenerate<ThanaInfoLn> _lang;
        private readonly IAddressService addressService;
        private readonly IAddressTypeService addressTypeService;

        public AddressTypeController(IAddressService addressService, IAddressTypeService addressTypeService)
        {
            //_lang = new LangGenerate<ThanaInfoLn>(hostingEnvironment.ContentRootPath);
            this.addressService = addressService;
            this.addressTypeService = addressTypeService;
        }
        // GET: Thana
        public async Task<IActionResult> Index()
        {
            AddressTypeViewModel model = new AddressTypeViewModel
            {
               
                addressTypes=await addressTypeService.GetAllAddressType(),
            };
            return View(model);
        }

        // POST: Thana/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AddressTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.addressTypes = await addressTypeService.GetAllAddressType();
                return View(model);
            }

            AddressType data = new AddressType
            {
                Id = (int)model.addressTypeId,
               typeName = model.typeName,
               
            };

            await addressTypeService.SaveAddressType(data);

            return RedirectToAction(nameof(Index));

        }

        // GET: Thana/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        [Route("global/api/GetAllAddressType/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllAddressType()
        {
            return Json( await addressTypeService.GetAllAddressType());
        }


    }
}