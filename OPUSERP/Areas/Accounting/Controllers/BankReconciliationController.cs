using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;

namespace OPUSERP.Areas.Accounting.Controllers
{
    [Authorize]
    [Area("Accounting")]
    public class BankReconciliationController : Controller
    {
        private readonly IVoucherService voucherService;
        public BankReconciliationController(IVoucherService voucherService)
        {
            this.voucherService = voucherService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult PaymentStatus()
        {
            var model = new PaymentStatusViewModel
            {
                voucherMasters = voucherService.GetvoucherMasterByTypeId(6),
            };
            return View(model);
        }

        public IActionResult ChequeDelivery()
        {
            return View();
        }

        public IActionResult BankReconciliation()
        {
            return View();
        }

        public IActionResult BankReconciliationOD()
        {
            return View();
        }
    }
}