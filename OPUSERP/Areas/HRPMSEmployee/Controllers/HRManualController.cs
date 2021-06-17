using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Area("HRPMSEmployee")]
    public class HRManualController : Controller
    {
        private readonly IHRManualService hRManualService;

        public HRManualController(IHRManualService hRManualService)
        {
            this.hRManualService = hRManualService;
        }

        public async Task<IActionResult> Index()
        {
            HRManualViewModel model = new HRManualViewModel
            {
                hRManualAttachments = await hRManualService.GetHRManualAttachment(),
            };
            return View(model);
        }

        // POST: Language/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] HRManualViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.hRManualAttachments = await hRManualService.GetHRManualAttachment();

                return View(model);
            }

            string fileName = String.Empty;
            string fileNameMain = String.Empty;
            string message = FileSave.SaveHRManualAttachment(out fileName, model.fileUrl);

            if (message == "success")
            {
                fileNameMain = fileName;
            }
            else
            {
                fileNameMain = fileName;
            }

            HRManualAttachment data = new HRManualAttachment
            {
                fileTitle = model.fileTitle,
                fileUrl = fileNameMain,
                remarks = model.remarks,
                date = model.date
            };

            await hRManualService.SaveHRManualAttachment(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            await hRManualService.DeleteHRManualAttachmentById(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> HRManualList()
        {
            HRManualViewModel model = new HRManualViewModel
            {
                hRManualAttachments = await hRManualService.GetHRManualAttachment(),
            };
            return View(model);
        }
    }
}