using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Shagotom.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Shagotom.Data.Entity.Visitor;
using OPUSERP.Shagotom.Services.MasterData.Interfaces;
using OPUSERP.Shagotom.Services.Visitor.Interfaces;

namespace OPUSERP.Areas.Shagotom.Controllers
{
    [Area("Shagotom")]
    public class VisitorEntryController : Controller
    {
        private readonly IDesignationDepartmentService designationDepartmentService;
        private readonly IEmployeeInfoServiceForShagotom employeeInfoService;
        private readonly IVisitorInfoDetailsService visitorInfoDetailsService;
        private readonly IVisitorEntryMasterService visitorEntryMasterService;

        public VisitorEntryController(
            IDesignationDepartmentService designationDepartmentService,
            IEmployeeInfoServiceForShagotom employeeInfoService,
            IVisitorEntryMasterService visitorEntryMasterService,
            IVisitorInfoDetailsService visitorInfoDetailsService
        )
        {
            this.designationDepartmentService = designationDepartmentService;
            this.employeeInfoService = employeeInfoService;
            this.visitorEntryMasterService = visitorEntryMasterService;
            this.visitorInfoDetailsService = visitorInfoDetailsService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(VisitorEntryViewModel model)
        {
            string msg = "error";

            if (model.Id > 0 && model.nameArray.Length > 0)
            {
                VisitorEntryMaster master = new VisitorEntryMaster
                {
                    employeeInfoId = model.Id,
                    status = 1
                };

                int masterId = await visitorEntryMasterService.SaveVisitorEntryMaster(master);

                for (int i = 0; i < model.nameArray.Length ; i++)
                {
                    VisitorEntryDetails visitorInfo = new VisitorEntryDetails
                    {
                        visitorEntryMasterId = masterId,
                        name = model.nameArray[i],
                        email = model.emailArray[i],
                        mobile = model.mobileArray[i],
                        company = model.companyArray[i],
                        imgUrl = model.imgUrlArray[i],
                        date = model.dateArray[i],
                        time = model.timeArray[i],
                        status = 1
                    };

                    await visitorInfoDetailsService.SaveVisitorEntryDetails(visitorInfo);

                    msg = "success";
                }
            }

            return Json(msg);
        }

        [Route("global/api/VisitorEntry/GetAllEmployeeInfo")]
        [HttpGet]
        public async Task<IActionResult> GetAllEmployeeInfo()
        {
            IEnumerable<EmployeeInfo> model = await employeeInfoService.GetAllEmployeeInfo();

            return Json(model);
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeInfoById(int id)
        {
            VisitorEntryViewModel model = await employeeInfoService.GetEmployeeInfoById(id);

            return Json(model);
        }
    }
}