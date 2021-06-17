using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;

namespace OPUSERP.Areas.Budget.Controllers
{
    [Authorize]
    [Area("Budget")]
    public class BudgetBranchAddressController : Controller
    {
        private readonly IAddressTypeService addressTypeService;

        public BudgetBranchAddressController(IAddressTypeService addressTypeService)
        {
            this.addressTypeService = addressTypeService;
        }

        // GET: 
        public async Task<IActionResult> Index()
        {
            BudgetBranchAddressViewModel model = new BudgetBranchAddressViewModel
            {
                addressTypes = await addressTypeService.GetAllAddressType()
                //countries = await addressService.GetAllContry()
            };
            return View(model);
        }

        // POST: Country/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([FromForm] BudgetBranchAddressViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.countries = await addressService.GetAllContry();
                return View(model);
            }

            //BudgetHead country = new BudgetHead
            //{
            //    Id = Int32.Parse(model.countryId),
            //    countryCode = model.countryCode,
            //    shortName = model.shortName,
            //    countryName = model.countryName,
            //    countryNameBn = model.countryNameBn
            //};

            //await addressService.SaveCountry(country);

            return RedirectToAction(nameof(Index));

        }
    }
}