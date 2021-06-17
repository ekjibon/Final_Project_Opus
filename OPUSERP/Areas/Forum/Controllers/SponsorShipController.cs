using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Forum.Models;
using OPUSERP.CLUB.Data.Forum;
using OPUSERP.CLUB.Services.Forum.Interface;

namespace OPUSERP.Areas.Forum.Controllers
{
    [Authorize]
    [Area("Forum")]
    public class SponsorShipController : Controller
    {
        private readonly ISponsorshipService sponsorshipService;

        public SponsorShipController(ISponsorshipService sponsorshipService)
        {
            this.sponsorshipService = sponsorshipService;
        }

        public async Task<IActionResult> Index()
        {
            SponsorShipViewModel model = new SponsorShipViewModel
            {
                sponsorShips = await sponsorshipService.GetSponsorShip() 
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index([FromForm] SponsorShipViewModel model)
        {
            if (!ModelState.IsValid)
            {
                //model.fLang = _lang.PerseLang("News/NewsEN.json", "News/NewsBN.json", Request.Cookies["lang"]);
                model.sponsorShips = await sponsorshipService.GetSponsorShip() ;
                return View(model);
            }

            //return Json(model);

            SponsorShip data = new SponsorShip
            {
                Id = model.Id,
                companyName = model.companyName,
                sponsorInfo = model.sponsorInfo,
                link = model.link,
                date = model.date,
                details = model.details,
            };
            await sponsorshipService.SaveSponsorShip(data);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await sponsorshipService.DeleteSponsorShip(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }
    }
}