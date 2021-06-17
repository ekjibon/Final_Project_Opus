using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.SCM.Services.Requisition.Interfaces;

namespace OPUSERP.Areas.SCMRequisition.Controllers
{
    [Area("SCMRequisition")]
    public class RequsitionRaiserDashbordController : Controller
    {
		public readonly IRequisitionService _requisitionService;

		public RequsitionRaiserDashbordController(IRequisitionService requisitionService)
		{
			_requisitionService = requisitionService;
		}

		public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Dashboard()
        {
			var model = new RequisitionRaiserDashViewModel
			{
				CreateRequisition = await _requisitionService.GetCreatedRequisition("PR Raise"),
				OnGoingRequisition = await _requisitionService.GetCreatedRequisition("Approval process"),
				ApprovedRequisition = await _requisitionService.GetCreatedRequisition("Final Approved"),
				CompleteRequisition = await _requisitionService.GetCreatedRequisition("Final Approved"),
				RejectRequisition = await _requisitionService.GetCreatedRequisition("Reject"),
				MostRecent = await _requisitionService.GetMostRecentReq(),
				TopTen = await _requisitionService.GetTopTenReq(),
				Featured = await _requisitionService.GetFeaturedReq()
			};
            return View(model);
        }
    }
}
