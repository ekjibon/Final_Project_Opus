using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class TransferPostingController : Controller
    {
        private readonly LangGenerate<TransferPosting> _lang;

        public TransferPostingController(IHostingEnvironment hostingEnvironment)
        {
            _lang = new LangGenerate<TransferPosting>(hostingEnvironment.ContentRootPath);
        }
        // GET: TransferPosting

        public ActionResult Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new TransferPostingViewModel
            {
                fLang = _lang.PerseLang("Employee/TransferPostingEN.json")
            };
            return View(model);
        }

        // POST: TransferPosting/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([FromForm] TransferPostingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return Json(model);
        }

    }
}