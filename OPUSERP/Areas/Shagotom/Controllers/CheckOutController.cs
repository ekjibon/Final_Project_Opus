using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Shagotom.Models;
using OPUSERP.Shagotom.Data.Entity.Visitor;
using OPUSERP.Shagotom.Services.Visitor.Interfaces;

namespace OPUSERP.Areas.Shagotom.Controllers
{
    [Area("Shagotom")]
    public class CheckOutController : Controller
    {
        private readonly IIssueCardService issueCardService;
        private readonly IVisitorEntryMasterService visitorEntryMasterService;
        private readonly IVisitorInfoDetailsService visitorInfoDetailsService;

        public CheckOutController(IIssueCardService issueCardService,
            IVisitorEntryMasterService visitorEntryMasterService,
            IVisitorInfoDetailsService visitorInfoDetailsService)
        {
            this.issueCardService = issueCardService;
            this.visitorEntryMasterService = visitorEntryMasterService;
            this.visitorInfoDetailsService = visitorInfoDetailsService;
        }

        public async Task<IActionResult> Index()
        {
            VisitorEntryViewModel model = new VisitorEntryViewModel
            {
                approvedList = await issueCardService.GetAllCurrentInMeetingRoom()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index(int detailsId)
        {
            var msg = "error";

            if (detailsId > 0)
            {
                VisitorEntryDetails details = await visitorInfoDetailsService.GetVisitorEntryDetailsById(detailsId);

                if (details != null)
                {
                    details.status = 5;
                    await visitorInfoDetailsService.SaveVisitorEntryDetails(details);

                    msg = "success";
                }


                IEnumerable<VisitorEntryDetails> detailsCount = await issueCardService.GetAllCheckoutListByMasterId((int)details.visitorEntryMasterId);

                int count = 0;
                foreach (var detailse in detailsCount) count++;

                if (count == 0)
                {
                    VisitorEntryMaster master = await visitorEntryMasterService.GetVisitorEntryMasterById((int)details.visitorEntryMasterId);

                    if (master != null)
                    {
                        master.status = 5;
                        await visitorEntryMasterService.SaveVisitorEntryMaster(master);
                    }
                }


            }

            return Json(msg);
        }

        [Route("global/api/CheckOut/GetAllApprovedList")]
        public async Task<IActionResult> GetAllApprovedList()
        {
            VisitorEntryViewModel model = new VisitorEntryViewModel
            {
                approvedList = await issueCardService.GetAllCurrentInMeetingRoom()
            };

            return Json(model);
        }

        
        public async Task<IActionResult> GetVisitorDetailsByDetailsId(int detailsId)
        {
            VisitorEntryViewModel model = new VisitorEntryViewModel
            {
                approvedList = await issueCardService.GetVisitorDetailsByDetailsId(detailsId)
            };

            return Json(model);
        }
    }
}