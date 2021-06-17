using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.REMS.Controllers
{
    [Area("REMS")]
    public class ThirdPirtyVendorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}