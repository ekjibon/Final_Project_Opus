using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class SalaryMasterController : Controller
    {
        private readonly ISalaryService salaryService;
        public SalaryMasterController(ISalaryService salaryService)
        {
            this.salaryService = salaryService;

        }

        #region Salary Year
        public async Task<ActionResult> SalaryYear()
        {
            SalaryYearViewModel model = new SalaryYearViewModel
            {
                salaryYearsList = await salaryService.GetAllSalaryYear()
            };
            //if (model.salaryYear == null) model.salaryYear = new SalaryYear();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalaryYear([FromForm] SalaryYearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.salaryYearsList = await salaryService.GetAllSalaryYear();
                return View(model);
            }

            SalaryYear data = new SalaryYear
            {
                Id = model.editId,
                yearName = model.yearName,
                startDate = model.startDate,
                endDate = model.endDate
            };


            await salaryService.SaveSalaryYear(data);

            return RedirectToAction(nameof(SalaryYear));
        }

        #endregion

        #region Tax Year
        public async Task<ActionResult> TaxYear()
        {
            TaxYearViewModel model = new TaxYearViewModel
            {
                taxYearsList = await salaryService.GetAllTaxYear()
            };
            if (model.taxYear == null) model.taxYear = new TaxYear();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> TaxYear([FromForm] TaxYearViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.taxYearsList = await salaryService.GetAllTaxYear();
                return View(model);
            }

            TaxYear data = new TaxYear
            {
                Id = model.editId,
                taxYearName = model.taxYearName,
                startDate = model.startDate,
                endDate = model.endDate,
                taxLimit = model.taxLimit,
                allowablePerquisite = model.allowablePerquisite,
                assessmentYear = model.assessmentYear
            };


            await salaryService.SaveTaxYear(data);
            return RedirectToAction(nameof(TaxYear));
        }

        #endregion

        #region Salary Head
        public async Task<ActionResult> SalaryHead()
        {
            SalaryHeadViewModel model = new SalaryHeadViewModel
            {
                salaryHeadsList = await salaryService.GetAllSalaryHead()
            };
            if (model.salaryHead == null) model.salaryHead = new SalaryHead();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalaryHead([FromForm] SalaryHeadViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.salaryHeadsList = await salaryService.GetAllSalaryHead();
                return View(model);
            }

            SalaryHead data = new SalaryHead
            {
                Id = model.editId,
                salaryHeadName = model.salaryHeadName,
                salaryHeadCode = model.salaryHeadCode,
                salaryHeadType = model.salaryHeadType,
                sortOrder = model.sortOrder,
                isIncomeTax = model.isIncomeTax,
                isInvestments = model.isInvestments,
                isAdvance = model.isAdvance,
                isArrear = model.isArrear,
                isBonus = model.isBonus,
                isMonthlyAllowance = model.isMonthlyAllowance,
                isLoan = model.isLoan,
                headShortName = model.headShortName
            };


            await salaryService.SaveSalaryHead(data);
            return RedirectToAction(nameof(SalaryHead));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSalaryHeadById(int Id)
        {
            await salaryService.DeleteSalaryHeadById(Id);
            return Json(true);
        }

        #endregion

        #region Salary Type
        public async Task<ActionResult> SalaryType()
        {
            SalaryTypeViewModel model = new SalaryTypeViewModel
            {
                salaryTypesList = await salaryService.GetAllSalaryType()
            };
            if (model.salaryType == null) model.salaryType = new SalaryType();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalaryType([FromForm] SalaryTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.salaryTypesList = await salaryService.GetAllSalaryType();
                return View(model);
            }

            SalaryType data = new SalaryType
            {
                Id = model.editId,
                salaryTypeName = model.salaryTypeName,
            };


            await salaryService.SaveSalaryType(data);
            return RedirectToAction(nameof(SalaryType));
        }

        #endregion

        #region Bonus Type
        public async Task<ActionResult> BonusType()
        {
            BonusTypeViewModel model = new BonusTypeViewModel
            {
                bonusTypesList = await salaryService.GetAllBonusType()
            };
            if (model.bonusType == null) model.bonusType = new BonusType();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BonusType([FromForm] BonusTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.bonusTypesList = await salaryService.GetAllBonusType();
                return View(model);
            }

            BonusType data = new BonusType
            {
                Id = model.editId,
                bonusTypeName = model.bonusTypeName
            };


            await salaryService.SaveBonusType(data);
            return RedirectToAction(nameof(BonusType));
        }

        #endregion

        #region Bonus Structuree
        public async Task<ActionResult> BonousStructuree()
        {
            BonousStructureViewModel model = new BonousStructureViewModel
            {
                bonousCategories = await salaryService.GetAllBonusCategory(),
                salaryCalulationTypes = await salaryService.GetAllSalaryCalulationType(),
                bonousStructures = await salaryService.GetAllBonousStructure()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BonousStructuree([FromForm] BonousStructureViewModel model)
        {
            int bonusId = 0;

            BonousStructure data = new BonousStructure
            {
                Id = model.bonousStructureId,
                BonousSubCategoryId = model.BonousSubCategoryId,
                salaryCalulationTypeId = model.salaryCalulationTypeId,
                monthYearType = model.monthYearType,
                minMonthValue = model.minMonthValue,
                maxMonthValue = model.maxMonthValue,
                percentAmount = model.percentAmount,
                bonousBasedOn = model.bonousBasedOn,
                isActive = model.isActive,
                hasEmployee = model.hasEmployee
            };

            bonusId = await salaryService.SaveBonousStructure(data);

            await salaryService.DeleteEmployeesBonusStructureBybonusId(bonusId);
            if (model.hasEmployee == 1)
            {
                if (bonusId != 0)
                {
                    
                    if (model.ids != null)
                    {

                        for (var i = 0; i < model.ids.Length; i++)
                        {
                            EmployeesBonusStructure employee = new EmployeesBonusStructure
                            {
                                bonousStructureId = bonusId,
                                employeeInfoId = model.ids[i]
                            };
                            await salaryService.SaveEmployeesBonusStructure(employee);
                        }
                    }
                }
            }

            return RedirectToAction(nameof(BonousStructuree));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteBonousStructureById(int Id)
        {
            await salaryService.DeleteEmployeesBonusStructureBybonusId(Id);
            await salaryService.DeleteBonousStructureById(Id);
            return Json(true);
        }

        #endregion

        #region Wallet Type
        public async Task<ActionResult> WalletType()
        {
            WalletTypeViewModel model = new WalletTypeViewModel
            {
                walletTypes = await salaryService.GetAllWalletType()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> WalletType([FromForm] WalletTypeViewModel model)
        {
            WalletType data = new WalletType
            {
                Id = model.walletTypeId,
                walletTypeName = model.walletTypeName
            };

            await salaryService.SaveWalletType(data);
            return RedirectToAction(nameof(WalletType));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteWalletTypeById(int Id)
        {
            await salaryService.DeleteWalletTypeById(Id);
            return Json(true);
        }

        #endregion

        #region Salary Calculation Type
        public async Task<ActionResult> SalaryCalulationType()
        {
            SalaryCalulationTypeViewModel model = new SalaryCalulationTypeViewModel
            {
                salaryCalulationTypesList = await salaryService.GetAllSalaryCalulationType()
            };
            if (model.salaryCalulationType == null) model.salaryCalulationType = new SalaryCalulationType();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalaryCalulationType([FromForm] SalaryCalulationTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.salaryCalulationTypesList = await salaryService.GetAllSalaryCalulationType();
                return View(model);
            }

            SalaryCalulationType data = new SalaryCalulationType
            {
                Id = model.editId,
                salaryCalulationTypeName = model.salaryCalulationTypeName
            };


            await salaryService.SaveSalaryCalulationType(data);
            return RedirectToAction(nameof(SalaryCalulationType));
        }

        #endregion

        #region Salary Period
        public async Task<ActionResult> SalaryPeriod()
        {
            SalaryPeriodViewModel model = new SalaryPeriodViewModel
            {
                salaryPeriodsList = await salaryService.GetAllSalaryPeriod(),
                salaryYearsList = await salaryService.GetAllSalaryYear(),
                salaryTypesList = await salaryService.GetAllSalaryType(),
                taxYearsList = await salaryService.GetAllTaxYear(),
                bonusTypesList = await salaryService.GetAllBonusType()
            };
            if (model.salaryPeriod == null) model.salaryPeriod = new SalaryPeriod();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalaryPeriod([FromForm] SalaryPeriodViewModel model)
        {
            var periodCheck = await salaryService.GetDuplicateSalaryPeriodById(model.editId, model.salaryYearId, model.salaryTypeId,model.monthName);
            if (!ModelState.IsValid || periodCheck.Count() > 0)
            {
                model.salaryPeriodsList = await salaryService.GetAllSalaryPeriod();
                model.salaryYearsList = await salaryService.GetAllSalaryYear();
                model.salaryTypesList = await salaryService.GetAllSalaryType();
                model.taxYearsList = await salaryService.GetAllTaxYear();
                model.bonusTypesList = await salaryService.GetAllBonusType();
                if (periodCheck.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Duplicate period in same year or month or type !!!");
                }
                return View(model);
            }

            SalaryPeriod data = new SalaryPeriod
            {
                Id = model.editId,
                salaryYearId = model.salaryYearId,
                taxYearId = model.taxYearId,
                salaryTypeId = model.salaryTypeId,
                bonusTypeId = model.bonusTypeId,
                organizationsBranchId = model.organizationsBranchId,
                periodName = model.periodName,
                monthName = model.monthName,
                lockLabel = 0,
                daysWorking = model.daysWorking
                //mailText = model.mailText,
                //mailSub = model.mailSub
            };


            await salaryService.SaveSalaryPeriod(data);
            return RedirectToAction(nameof(SalaryPeriod));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteSalaryPeriodById(int Id)
        {
            await salaryService.DeleteSalaryPeriodById(Id);
            return Json(true);
        }
        #endregion

        #region Bonus Period
        public async Task<ActionResult> BonusPeriod()
        {
            SalaryPeriodViewModel model = new SalaryPeriodViewModel
            {
                salaryPeriodsList = await salaryService.GetAllSalaryPeriod(),
                salaryYearsList = await salaryService.GetAllSalaryYear(),
                salaryTypesList = await salaryService.GetAllSalaryType(),
                taxYearsList = await salaryService.GetAllTaxYear(),
                bonusTypesList = await salaryService.GetAllBonusType()
            };
            if (model.salaryPeriod == null) model.salaryPeriod = new SalaryPeriod();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BonusPeriod([FromForm] SalaryPeriodViewModel model)
        {
            var periodCheck = await salaryService.GetDuplicateSalaryPeriodById(model.editId, model.salaryYearId, model.salaryTypeId, model.monthName);
            if (!ModelState.IsValid || periodCheck.Count() > 0)
            {
                model.salaryPeriodsList = await salaryService.GetAllSalaryPeriod();
                model.salaryYearsList = await salaryService.GetAllSalaryYear();
                model.salaryTypesList = await salaryService.GetAllSalaryType();
                model.taxYearsList = await salaryService.GetAllTaxYear();
                model.bonusTypesList = await salaryService.GetAllBonusType();
                if (periodCheck.Count() > 0)
                {
                    ModelState.AddModelError(string.Empty, "Duplicate period in same year or month or type !!!");
                }
                return View(model);
            }
            SalaryPeriod data = new SalaryPeriod
            {
                Id = model.editId,
                salaryYearId = model.salaryYearId,
                taxYearId = model.taxYearId,
                salaryTypeId = model.salaryTypeId,
                bonusTypeId = model.bonusTypeId,
                organizationsBranchId = model.organizationsBranchId,
                periodName = model.periodName,
                monthName = model.monthName,
                lockLabel = 0,
                daysWorking = model.daysWorking
                //mailText = model.mailText,
                //mailSub = model.mailSub
            };
            await salaryService.SaveSalaryPeriod(data);
            return RedirectToAction(nameof(BonusPeriod));
        }
       
        #endregion

        #region Salary Slab
        public async Task<ActionResult> SalarySlab()
        {
            SalarySlabViewModel model = new SalarySlabViewModel
            {
                salarySlabsList = await salaryService.GetAllSalarySlab(),
                salaryGradesList = await salaryService.GetAllSalaryGrade()
            };
            if (model.salarySlab == null) model.salarySlab = new SalarySlab();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalarySlab([FromForm] SalarySlabViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.salarySlabsList = await salaryService.GetAllSalarySlab();
                model.salaryGradesList = await salaryService.GetAllSalaryGrade();
                return View(model);
            }

            SalarySlab data = new SalarySlab
            {
                Id = model.editId,
                slabName = model.slabName,
                salaryGradeId = model.salaryGradeId,
                slabAmount = model.slabAmount,
                effectDate = model.effectDate
            };


            await salaryService.SaveSalarySlab(data);
            return RedirectToAction(nameof(SalarySlab));
        }

        #endregion

        #region Salary Grade 
        public async Task<IActionResult> SalaryGrade()
        {
            SalaryGradeViewModel model = new SalaryGradeViewModel
            {
                salaryGrades = await salaryService.GetAllSalaryGrade()
            };
            return View(model);
        }

        // POST: SalaryGrade/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalaryGrade([FromForm] SalaryGradeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.salaryGrades = await salaryService.GetAllSalaryGrade();
                return View(model);
            }

            SalaryGrade data = new SalaryGrade
            {
                Id = model.salaryGradeId,
                gradeName = model.gradeName,
                basicAmount = model.basicAmount,
                currentBasic = model.currentBasic,
                payScale = model.payScale,
            };

            await salaryService.SaveSalaryGrade(data);

            return RedirectToAction(nameof(SalaryGrade));
        }

        #endregion

        #region Salary Grade Percent
        public async Task<ActionResult> SalaryGradePercent()
        {
            SalaryGradePercentViewModel model = new SalaryGradePercentViewModel
            {
                salaryGradePercentsList = await salaryService.GetAllSalaryGradePercent(),
                salaryGradesList = await salaryService.GetAllSalaryGrade(),
                salaryHeadsList = await salaryService.GetAllSalaryHead(),
                salaryCalulationTypesList = await salaryService.GetAllSalaryCalulationType()
            };
            if (model.salarySlab == null) model.salarySlab = new SalarySlab();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SalaryGradePercent([FromForm] SalaryGradePercentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.salaryGradePercentsList = await salaryService.GetAllSalaryGradePercent();
                model.salaryGradesList = await salaryService.GetAllSalaryGrade();
                model.salaryHeadsList = await salaryService.GetAllSalaryHead();
                model.salaryCalulationTypesList = await salaryService.GetAllSalaryCalulationType();
                return View(model);
            }

            SalaryGradePercent data = new SalaryGradePercent
            {
                Id = model.editId,
                salaryHeadId = model.salaryHeadId,
                salaryGradeId = model.salaryGradeId,
                salaryCalulationTypeId = model.salaryCalulationTypeId,
                percentAmount = model.percentAmount
            };


            await salaryService.SaveSalaryGradePercent(data);
            return RedirectToAction(nameof(SalaryGradePercent));
        }

        #endregion

        #region Loan Policy
        public async Task<ActionResult> LoanPolicy()
        {
            var salary = await salaryService.GetAllSalaryHead();
            LoanPolicyViewModel model = new LoanPolicyViewModel
            {
                loanPolicies = await salaryService.GetAllLoanPolicy(),
                salaryGradesList = await salaryService.GetAllSalaryGrade(),
                salaryHeadsList = salary.Where(a => a.isLoan == "Yes"),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoanPolicy([FromForm] LoanPolicyViewModel model)
        {
            var salary = await salaryService.GetAllSalaryHead();
            if (!ModelState.IsValid)
            {
                model.loanPolicies = await salaryService.GetAllLoanPolicy();
                model.salaryGradesList = await salaryService.GetAllSalaryGrade();
                model.salaryHeadsList = salary.Where(a => a.isLoan == "Yes");
                return View(model);
            }

            LoanPolicy data = new LoanPolicy
            {
                Id = model.editId,
                salaryHeadId = model.salaryHeadId,
                salaryGradeId = model.salaryGradeId,
                loanPolicyName = model.loanPolicyName,
                maximumLoanAmount = model.maximumLoanAmount,
                loanInterestRate = model.loanInterestRate,
                loanNoOfInstallment = model.loanNoOfInstallment,
                isActive = model.isActive,
            };

            await salaryService.SaveLoanPolicy(data);
            return RedirectToAction(nameof(LoanPolicy));
        }

        [HttpPost]
        public async Task<JsonResult> DeleteLoanPolicyById(int Id)
        {
            await salaryService.DeleteLoanPolicyById(Id);
            return Json(true);
        }

        #endregion


        #region API Section
        [Route("global/api/SalaryMasterController/GetAllSalaryYear/")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllSalaryYear()
        {
            return Json(await salaryService.GetAllSalaryYear());
        }

        [Route("/api/global/GetBonusSubCategoryByMasterId/{id}")]
        public async Task<JsonResult> GetBonusSubCategoryByMasterId(int id)
        {
            var subLedgers = await salaryService.GetBonusSubCategoryByMasterId(id);
            return Json(subLedgers);
        }
        [Route("global/api/GetEmployeesBonusStructureByBonusId/{id}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetEmployeesBonusStructureByBonusId(int Id)
        {
            return Json(await salaryService.GetEmployeesBonusStructureByBonusId(Id));
        }
        #endregion

    }
}