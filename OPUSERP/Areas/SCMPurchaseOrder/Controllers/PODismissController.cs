using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;

namespace OPUSERP.Areas.SCMPurchaseOrder.Controllers
{
    [Area("SCMPurchaseOrder")]
    public class PODismissController : Controller
    {
        private readonly IPODismissService iPODismissService;
        private readonly IPurchaseOrderService purchaseOrderService;

        public PODismissController(IPODismissService iPODismissService, IPurchaseOrderService purchaseOrderService)
        {
            this.iPODismissService = iPODismissService;
            this.purchaseOrderService = purchaseOrderService;
        }

        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            PurchaseOrderDismissViewModel model = new PurchaseOrderDismissViewModel
            {
                purchaseOrderDismisses = await iPODismissService.GetPOForDismisses(userName)
            };
            return View(model);
        }
    }
}