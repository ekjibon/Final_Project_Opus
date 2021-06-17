using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Program.Models;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Program.Controllers
{
    [Authorize]
    [Area("Program")]
    public class ProgramPlanController : Controller
    {
        private readonly IProgramCategoryService programCategoryService;
        private readonly IProgramMainCategoryService programMainCategoryService;
        private readonly IProgramYearService programYearService;
        private readonly IProgramPlanService programPlanService;
        private readonly HRPMS.Services.MasterData.Interfaces.IAddressService addressService;
        private readonly IProgramMasterService programMasterService;
        private readonly IProgramHeadlineService programHeadlineService;
        private readonly ERPServices.MasterData.Interfaces.IAddressService countryService;
        private readonly IERPCompanyService iERPCompanyService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;


        public ProgramPlanController(IHostingEnvironment hostingEnvironment, IConverter converter, IProgramCategoryService programCategoryService, HRPMS.Services.MasterData.Interfaces.IAddressService addressService, IProgramMainCategoryService programMainCategoryService, IProgramYearService programYearService, IProgramPlanService programPlanService, IProgramMasterService programMasterService, IProgramHeadlineService programHeadlineService, ERPServices.MasterData.Interfaces.IAddressService countryService, IERPCompanyService iERPCompanyService)
        {
            this.programCategoryService = programCategoryService;
            this.addressService = addressService;
            this.programMainCategoryService = programMainCategoryService;
            this.programYearService = programYearService;
            this.programPlanService = programPlanService;
            this.programMasterService = programMasterService;
            this.programHeadlineService = programHeadlineService;
            this.countryService = countryService;
            this.iERPCompanyService = iERPCompanyService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;

        }

        #region ProgramPlan

        public async Task<IActionResult> Index()
        {
            var plan = await programPlanService.GetprogramPlan();
            string productionNo = ("P-Plan/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            ProgramPlanViewModel programMainCategoryView = new ProgramPlanViewModel
            {
                programMainCategories = await programMainCategoryService.GetProgramMainCategory(),
                divisions = await addressService.GetAllDivision(),
                number = productionNo,
                programYears = await programYearService.GetProgramYear(),
                programPlans = await programPlanService.GetprogramPlan()
            };

            return View(programMainCategoryView);
        }


        [HttpPost]
        public async Task<IActionResult> Index(ProgramPlanViewModel model)
        {
            string result = "error";
            var plan = await programPlanService.GetprogramPlan();
            string productionNo = ("P-Plan/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            if (!ModelState.IsValid)
            {
                ProgramPlanViewModel programMainCategoryView = new ProgramPlanViewModel
                {
                    programMainCategories = await programMainCategoryService.GetProgramMainCategory(),
                    divisions = await addressService.GetAllDivision(),
                    number = productionNo,
                    programYears = await programYearService.GetProgramYear(),
                    programPlans = await programPlanService.GetprogramPlan()
                };

                return Json(result);


            }

            programPlan programYear = new programPlan
            {
                Id = model.Id,
                number = model.number,
                startDate = model.startDate,
                endDate = model.endDate,
                programCategoryId = model.programCategoryId,
                divisionId = model.divisionId,
                districtId = model.districtId,
                programYearId = model.programYearId
            };

            await programPlanService.SaveprogramPlan(programYear);

            result = "success";

            return Json(result);
        }

        #endregion

        #region Program Year

        [HttpGet]
        public async Task<IActionResult> ProgramYear()
        {
            ProgramYearViewModel programMainCategoryView = new ProgramYearViewModel
            {
                programYears = await programYearService.GetProgramYear()
            };

            return View(programMainCategoryView);
        }

        [HttpPost]
        public async Task<IActionResult> ProgramYear(ProgramYearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ProgramYearViewModel programMainCategoryView = new ProgramYearViewModel
                {
                    programYears = await programYearService.GetProgramYear()
                };

                return View(programMainCategoryView);
            }

            ProgramYear programYear = new ProgramYear
            {
                Id = model.Id,
                name = model.name
            };

            await programYearService.SaveProgramYear(programYear);

            return RedirectToAction(nameof(ProgramYear));
        }

        public async Task<IActionResult> DeleteYear(int id)
        {
            try
            {
                await programYearService.DeleteProgramYearById(id);
                return RedirectToAction(nameof(ProgramYear));
            }
            catch
            {
                return RedirectToAction(nameof(ProgramYear));
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await programPlanService.DeleteprogramPlanById(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        #endregion

        #region Program pending List

        public async Task<IActionResult> ProgramPendingPlanList()
        {
            var model = new ProgramWorkPlanViewModel
            {
                programMasters = await programMasterService.GetProgramMaster()
            };

            return View(model);
        }

        #endregion

        #region Program Work Plan List

        public async Task<IActionResult> ProgramPlanList()
        {
            var model = new ProgramWorkPlanViewModel
            {
                programPlanListViewModels = await programPlanService.GetProjectPlanList()
            };

            return View(model);
        }

        #endregion

        #region Program Execution List

        public async Task<IActionResult> ProgramExecutionList()
        {
            var model = new ProgramWorkPlanViewModel
            {
                programPlanListViewModels = await programPlanService.GetProjectPlanList()
            };

            return View(model);
        }

        #endregion


        #region WorkPlan Executaion

        public async Task<IActionResult> WorkPlan(int? id, int? workPlanId, int? programYearId)
        {
            var countrydata = await countryService.GetAllContry();
            int bangladeshcountryId = countrydata.Where(a => a.countryName == "Bangladesh").FirstOrDefault().Id;
            ViewBag.bangladeshcountryId = bangladeshcountryId;

            ViewBag.masterId = id;
            ViewBag.workPlanId = workPlanId;
            ViewBag.programYearId = programYearId;
            ProgramWorkPlanViewModel model = new ProgramWorkPlanViewModel
            {
                programYears = await programYearService.GetProgramYear()
            };
            return View(model);
        }

        public async Task<IActionResult> WorkPlanExecutaion(int? id, int? workPlanId, int? programYearId)
        {
            ViewBag.masterId = id;
            ViewBag.workPlanId = workPlanId;
            ViewBag.programYearId = programYearId;
            ProgramWorkPlanViewModel model = new ProgramWorkPlanViewModel
            {
                programYears = await programYearService.GetProgramYear()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveWorkPlan([FromForm] ProgramWorkPlanViewModel model)
        {
            try
            {
                //if (model.workPlanId > 0)
                //{
                await programPlanService.DeleteProgramWorkPlansByMasterYearId(model.programYearId, model.masterId);
                //}
                for (int i = 0; i < model.programActivityIdAll.Length; i++)
                {
                    ProgramWorkPlan details = new ProgramWorkPlan
                    {
                        programYearId = model.programYearId,
                        programActivityId = model.programActivityIdAll[i],
                        firstMonth = model.firstMonthAll[i],
                        secondMonth = model.secondMonthAll[i],
                        thirdMonth = model.thirdMonthAll[i],
                        fourthMonth = model.fourthMonthAll[i],
                        fifthMonth = model.fifthMonthAll[i],
                        sixthMonth = model.sixthMonthAll[i],
                        seventhMonth = model.seventhMonthAll[i],
                        eighthMonth = model.eighthMonthAll[i],
                        ninethMonth = model.ninethMonthAll[i],
                        tenthMonth = model.tenthMonthAll[i],
                        eleventhMonth = model.eleventhMonthAll[i],
                        twelvethMonth = model.twelvethMonthAll[i],
                        remarks = model.remarks
                    };
                    await programPlanService.SaveProgramWorkPlan(details);
                }
                return RedirectToAction(nameof(ProgramPlanList));
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProgramWorkPlanById(int? yearId, int? masterId)
        {
            return Json(await programPlanService.GetProgramWorkPlanById(yearId, masterId));
        }

        [HttpGet]
        public async Task<IActionResult> GetProgramWorkPlanByMasterYearId(int? yearId, int? masterId)
        {
            var data = await programPlanService.GetProgramWorkPlanByMasterYearId(yearId, masterId);
            return Json(data);
        }

        [HttpGet]
        public async Task<JsonResult> GetActivityDetailsByMasterId(int masterid)
        {
            ProgramWorkPlanViewModel model = new ProgramWorkPlanViewModel
            {
                programActivities = await programHeadlineService.GetActivityDetailsByMasterId(masterid)
            };
            return Json(model);
        }

        #endregion

        #region Program Plan Location

        [HttpPost]
        public async Task<JsonResult> SaveProgramPlanLocation([FromForm] ProgramPlanLocationViewModel model)
        {
            try
            {
                ProgramPlanLocation data = new ProgramPlanLocation
                {
                    Id = model.editId,
                    programActivityId = model.programActivityId,
                    programYearId = model.clickprogramYearId,
                    monthName = model.clickMonthName,
                    planQuantity = model.planQuantity,
                    targetQuantity = model.targetQuantity,
                    divisionId = model.divisionId,
                    districtId = model.districtId,
                    thanaId = model.thanaId,
                    maillingAddress = model.maillingAddress,
                };
                int DocumentId = await programPlanService.SaveProgramPlanLocation(data);

                return Json(DocumentId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProgramPlanLocationByActivityMonthYearId(int? yearId, int? activityId, string month)
        {
            var data = await programPlanService.GetProgramPlanLocationByActivityMonthYearId(yearId, activityId, month);
            return Json(data);
        }

        [HttpPost]
        public async Task<JsonResult> DeleteProgramPlanLocationById(int? id)
        {
            await programPlanService.DeleteProgramPlanLocationById(id);
            return Json(true);
        }

        [HttpGet]
        public async Task<IActionResult> GetBalanceBeforeExecution(int? yearId, int? activityId, string month)
        {
            var stock = await programPlanService.GetProgramPlanLocationByActivityMonthYearId(yearId, activityId, month);
            var targetQty = stock.Sum(x => x.targetQuantity);
            var planQty = stock.Sum(x => x.planQuantity);
            //var balance = ((stockin == null) ? 0 : stockin) - ((stockout == null) ? 0 : stockout);
            var balance = targetQty - planQty;
            return Json(balance);
        }

        [HttpGet]
        public async Task<IActionResult> GetBalanceAfterExecution(int? yearId, int? activityId, string month)
        {
            var stock = await programPlanService.GetProgramPlanLocationByActivityMonthYearId(yearId, activityId, month);
            var executionQty = stock.Sum(x => x.executionQuantity);
            var planQty = stock.Sum(x => x.planQuantity);
            var balance = planQty - executionQty;
            return Json(balance);
        }
        #endregion

        #region Save Execution

        [HttpPost]
        public async Task<JsonResult> UpdateProgramPlanLocation([FromForm] ProgramPlanLocationViewModel model)
        {
            try
            {
                var masterId = await programPlanService.UpdateProgramPlanLocation(model.editId, model.executionQuantity);
                return Json(masterId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #region Program Plan Report

        [AllowAnonymous]
        public IActionResult ProgramPlanReport(int id, int? yearId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Program/ProgramPlan/ProgramPlanReportPdf/" + "?id=" + id + "&yearId=" + yearId;

            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgramPlanReportPdf(int id, int? yearId)
        {
            var model = new ProgramMasterView
            {
                companies = await iERPCompanyService.GetAllCompany(),
                programLocations = await programMasterService.GetAllProgramLocationBymasterId(id),
                programImplementors = await programMasterService.GetAllProgramImplementorBymasterId(id),
                programSourceFunds = await programMasterService.GetProgramSourceFundBymasterId(id),
                programPlanReportViewModels = await programPlanService.GetProgramPlanReport(id, yearId)
            };
            return View(model);
        }

        #endregion

        #region Program Plan Execution Report

        [AllowAnonymous]
        public IActionResult ProgramExecutionReport(int id, int? yearId)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Program/ProgramPlan/ProgramExecutionReportPdf/" + "?id=" + id + "&yearId=" + yearId;

            string status = myPDF.GenerateLandscapePDFA4(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> ProgramExecutionReportPdf(int id, int? yearId)
        {
            var model = new ProgramMasterView
            {
                companies = await iERPCompanyService.GetAllCompany(),
                programLocations = await programMasterService.GetAllProgramLocationBymasterId(id),
                programImplementors = await programMasterService.GetAllProgramImplementorBymasterId(id),
                programSourceFunds = await programMasterService.GetProgramSourceFundBymasterId(id),
                programPlanReportViewModels = await programPlanService.GetProgramPlanReport(id, yearId)
            };
            return View(model);
        }

        #endregion

    }
}