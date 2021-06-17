using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Budget.Models;

namespace OPUSERP.Areas.Budget.Controllers
{
    [Authorize]
    [Area("Budget")]
    public class BudgetBranchController : Controller
    {
        //private readonly IAddressService addressService;

        //public BudgetHeadsController(IAddressService addressService)
        //{
        //    this.addressService = addressService;
        //}

        // GET: 
        public IActionResult Index()
        {
            BudgetBranchViewModel model = new BudgetBranchViewModel
            {
                //countries = await addressService.GetAllContry()
            };
            return View(model);
        }

        // POST: Country/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([FromForm] BudgetBranchViewModel model)
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