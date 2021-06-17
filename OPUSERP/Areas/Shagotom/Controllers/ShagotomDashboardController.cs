using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Shagotom.Models;
using OPUSERP.Shagotom.Services.Visitor.Interfaces;

namespace OPUSERP.Areas.Shagotom.Controllers
{
    [Area("Shagotom")]
    public class ShagotomDashboardController : Controller
    {
        private readonly IIssueCardService issueCardService;

        public ShagotomDashboardController(IIssueCardService issueCardService)
        {
            this.issueCardService = issueCardService;
        }

        public async Task<IActionResult>  Index()
        {

            VisitorEntryViewModel model = new VisitorEntryViewModel
            {
                newRequstList = await issueCardService.GetAllNewRequest(),
                inMeetingRoomList = await issueCardService.GetAllCurrentInMeetingRoomMaster()
            };

            return View(model);
        }

        public async Task<IActionResult> ShowDetailsByMasterId(int id)
        {
            VisitorEntryViewModel models = new VisitorEntryViewModel
            {
                listOfDetailsInfoByMasterId = await issueCardService.GetAllDetailsListByMasterId(id)
            };

            return Json(models);
        }
    }
}