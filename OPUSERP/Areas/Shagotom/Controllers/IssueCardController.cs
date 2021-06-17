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
    public class IssueCardController : Controller
    {
        private readonly IIssueCardService issueCardService;
        private readonly IVisitorEntryMasterService visitorEntryMasterService;
        private readonly IVisitorInfoDetailsService visitorInfoDetailsService;

        public IssueCardController(IIssueCardService issueCardService, 
            IVisitorEntryMasterService visitorEntryMasterService,
            IVisitorInfoDetailsService visitorInfoDetailsService)
        {
            this.issueCardService = issueCardService;
            this.visitorEntryMasterService = visitorEntryMasterService;
            this.visitorInfoDetailsService = visitorInfoDetailsService;
        }

        public async Task<IActionResult>  Index()
        {
            VisitorEntryViewModel model = new VisitorEntryViewModel
            {
                approvedList = await issueCardService.GetAllApproveList()
            };

            return View(model);
        }

       [HttpPost]
        public async Task<IActionResult> Index(VisitorEntryViewModel model)
        {
            var msg = "error";

            if (model.Id > 0 && model.detailsId > 0 && !string.IsNullOrEmpty(model.cardNo))
            {
                VisitorEntryDetails details = await visitorInfoDetailsService.GetVisitorEntryDetailsById((int)model.detailsId);

                if (details != null)
                {
                    details.cardNo = model.cardNo;
                    details.status = 4;

                    await visitorInfoDetailsService.SaveVisitorEntryDetails(details);

                    msg = "success";
                }


                IEnumerable<VisitorEntryDetails> detailsCount = await issueCardService.GetAllApproveListByMasterId((int)model.Id);

                int count = 0;
                foreach (var detailse in detailsCount) count++;

                if (count == 0)
                {
                    VisitorEntryMaster master = await visitorEntryMasterService.GetVisitorEntryMasterById((int) model.Id);

                    if (master != null)
                    {
                        master.status = 4;
                        await visitorEntryMasterService.SaveVisitorEntryMaster(master);
                    }
                }
              

            }

            return Json(msg);
        }
    }
}