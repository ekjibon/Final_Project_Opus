using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    public class VivaPanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}