using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class BelongingsController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;
        private readonly IBelongingsService belongingsService;
        private readonly IBelongingsItemService belongingsItemService;

        public BelongingsController(IHostingEnvironment hostingEnvironment, IPhotographService photographService, IBelongingsService belongingsService, IPersonalInfoService personalInfoService, IBelongingsItemService belongingsItemService)
        {
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            this.belongingsService = belongingsService;
            this.belongingsItemService = belongingsItemService;
        }

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new BelongingsViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                belongings = await belongingsService.GetBelongingsByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                belongingItems = await belongingsItemService.GetBelongingItem()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] BelongingsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.belongings = await belongingsService.GetBelongingsByEmpId((int)model.employeeID);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }
            //return Json(model);
            Belongings data = new Belongings
            {
                Id = model.belongingsItemID ?? 0,
                employeeId = (int)model.employeeID,
                belongingItemId = model.belongingId,
                assetNo = model.assetNo,
                itemSpecificationId = model.specificationID,
                description = model.remarks,
                issueDate = model.issueDate,
                returnDate = model.returnDate
            };

            await belongingsService.SaveBelongings(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            return RedirectToAction("Index", "Belongings", new
            {
                id = (int)model.employeeID
            });
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await belongingsService.DeleteBelongingsById(id);
            return RedirectToAction("Index", "Belongings", new
            {
                id = empId
            });
        }


    }
}