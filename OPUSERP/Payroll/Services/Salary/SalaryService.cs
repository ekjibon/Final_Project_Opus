using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.Payroll.Data.Entity.PF;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Services.Salary
{
    public class SalaryService : ISalaryService
    {
        private readonly ERPDbContext _context;

        public SalaryService(ERPDbContext context)
        {
            _context = context;
        }

        #region Gratuity Data Process

        public async Task<bool> SaveSendEmailLogStatus(SendEmailLogStatus salaryYear)
        {
            if (salaryYear.Id != 0)
            {
                _context.SendEmailLogStatus.Update(salaryYear);
            }
            else
            {
                _context.SendEmailLogStatus.Add(salaryYear);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SendEmailLogStatus>> GetSendEmailLogStatus()
        {
            return await _context.SendEmailLogStatus.Include(x => x.employee).AsNoTracking().ToListAsync();
        }


        public async Task<bool> SaveGratiutyProcessData(GratiutyProcessData salaryYear)
        {
            if (salaryYear.Id != 0)
            {
                _context.gratiutyProcessDatas.Update(salaryYear);
            }
            else
            {
                _context.gratiutyProcessDatas.Add(salaryYear);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GratiutyProcessData>> GetAllGratiutyProcessData()
        {
            return await _context.gratiutyProcessDatas.Include(x=>x.employeeInfo).AsNoTracking().ToListAsync();
        }

        public List<DateTime?> GetAllGratiutyProcessDataDates()
        {
            return _context.gratiutyProcessDatas.Select(x=>x.date).Distinct().ToList();
        }

        public async Task<IEnumerable<GratuityReportViewModel>> GetAllGratuityReportViewModel()
        {
            List<GratuityReportViewModel> data = new List<GratuityReportViewModel>();
            var processdata  =  await _context.gratiutyProcessDatas.Include(x=>x.employeeInfo).AsNoTracking().ToListAsync();
            foreach(var list in processdata)
            {
                data.Add(new GratuityReportViewModel {
                    employeeCode = list.employeeInfo.employeeCode,
                    nameEnglish = list.employeeInfo.nameEnglish,
                    designation = list.designation,
                    joiningDate = list.employeeInfo.joiningDateGovtService?.ToString("dd-MM-yyyy"),
                    uptoDate = list.date?.ToString("dd-MM-yyyy"),
                    basicAmount  =(int)list.basic,
                    fractionalYear = list.year,
                    gratuityAmount = list.gratuity
                });
            }
            return data;
        }

        public async Task<IEnumerable<GratuityReportViewModel>> GetAllGratuityReportViewModelByDate(DateTime date)
        {
            List<GratuityReportViewModel> data = new List<GratuityReportViewModel>();
            var processdata = await _context.gratiutyProcessDatas.Where(x=>x.date==date).Include(x => x.employeeInfo).AsNoTracking().ToListAsync();
            foreach (var list in processdata)
            {
                data.Add(new GratuityReportViewModel
                {
                    employeeCode = list.employeeInfo.employeeCode,
                    nameEnglish = list.employeeInfo.nameEnglish,
                    designation = list.designation,
                    joiningDate = list.employeeInfo.joiningDateGovtService?.ToString("dd-MM-yyyy"),
                    uptoDate = list.date?.ToString("dd-MM-yyyy"),
                    basicAmount = (int)list.basic,
                    fractionalYear = list.year,
                    gratuityAmount = list.gratuity
                });
            }
            return data;
        }

        #endregion

        #region Salary Year

        public async Task<bool> SaveSalaryYear(SalaryYear salaryYear)
        {
            if (salaryYear.Id != 0)
            {
                _context.salaryYears.Update(salaryYear);
            }
            else
            {
                _context.salaryYears.Add(salaryYear);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryYear>> GetAllSalaryYear()
        {
            return await _context.salaryYears.AsNoTracking().ToListAsync();
        }

        public async Task<SalaryYear> GetSalaryYearById(int id)
        {
            return await _context.salaryYears.FindAsync(id);
        }

        public async Task<bool> DeleteSalaryYearById(int id)
        {
            _context.salaryYears.RemoveRange(_context.salaryYears.Where(a => a.Id == id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region EmployeeIncomeTax

        public async Task<bool> SaveEmployeesTax(EmployeesTax employeesTax)
        {
            if (employeesTax.Id != 0)
            {
                _context.employeesTaxes.Update(employeesTax);
            }
            else
            {
                _context.employeesTaxes.Add(employeesTax);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeesTax>> GetAllEmployeesTax()
        {
            return await _context.employeesTaxes.Include(x => x.employeeInfo).Include(x => x.taxYear).Where(x => x.isActive == 1).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeesTax> GetEmployeesTaxById(int id)
        {
            return await _context.employeesTaxes.FindAsync(id);
        }

        public async Task<bool> DeleteEmployeeTaxById(int id)
        {
            _context.employeesTaxes.RemoveRange(_context.employeesTaxes.Where(a => a.Id == id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateEmployeesStatus(int? id, int taxyearid)
        {
            var VoucherMasters = _context.employeesTaxes.Where(x => x.employeeInfoId == id && x.taxYearId == taxyearid).FirstOrDefault();
            if (VoucherMasters != null)
            {
                //foreach (EmployeesTax tax in VoucherMasters)
                //{
                //    tax.isActive = 0;
                //    tax.updatedBy = updateBy;
                //    tax.updatedAt = DateTime.Now;
                //    _context.Entry(tax).State = EntityState.Modified;
                //    await _context.SaveChangesAsync();
                //}
                _context.employeesTaxes.Remove(_context.employeesTaxes.Find(VoucherMasters.Id));
                await _context.SaveChangesAsync();
            }

            return true;
        }

        #endregion

        #region Salary Grade
        public async Task<bool> SaveSalaryGrade(SalaryGrade grade)
        {
            if (grade.Id != 0)
                _context.salaryGrades.Update(grade);
            else
                _context.salaryGrades.Add(grade);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryGrade>> GetAllSalaryGrade()
        {
            return await _context.salaryGrades.AsNoTracking().ToListAsync();
        }

        public async Task<SalaryGrade> GetSalaryGradeById(int id)
        {
            return await _context.salaryGrades.FindAsync(id);
        }

        public async Task<bool> DeleteSalaryGradeById(int id)
        {
            _context.salaryGrades.Remove(_context.salaryGrades.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Tax Year
        public async Task<bool> SaveTaxYear(TaxYear taxYear)
        {
            if (taxYear.Id != 0)
            {
                _context.taxYears.Update(taxYear);
            }
            else
            {
                _context.taxYears.Add(taxYear);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TaxYear>> GetAllTaxYear()
        {
            return await _context.taxYears.AsNoTracking().ToListAsync();
        }
        public async Task<TaxYear> TaxYearbyId(int id)
        {
            return await _context.taxYears.FindAsync(id);
        }
        #endregion

        #region Tax Slab
        public async Task<bool> SaveTaxSlab(SlabType slabType)
        {
            if (slabType.Id != 0)
            {
                _context.SlabTypes.Update(slabType);
            }
            else
            {
                _context.SlabTypes.Add(slabType);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SlabType>> GetAllSlabType()
        {
            return await _context.SlabTypes.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Rebate Slab Type
        public async Task<bool> SaveRebateSlab(RebateSlabType rebateSlabType)
        {
            if (rebateSlabType.Id != 0)
            {
                _context.RebateSlabTypes.Update(rebateSlabType);
            }
            else
            {
                _context.RebateSlabTypes.Add(rebateSlabType);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RebateSlabType>> GetAllRebateSlabType()
        {
            return await _context.RebateSlabTypes.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Investment Rebate Setting
        public async Task<bool> SaveRebateSetting(InvestmentRebateSettings investmentRebateSettings)
        {
            if (investmentRebateSettings.Id != 0)
            {
                _context.InvestmentRebateSettings.Update(investmentRebateSettings);
            }
            else
            {
                _context.InvestmentRebateSettings.Add(investmentRebateSettings);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<InvestmentRebateSettings>> GetAllRebateSetting()
        {
            return await _context.InvestmentRebateSettings.Include(x => x.taxYear).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<InvestmentRebateSettings>> GetAllRebateSettingbytaxyearid(int Id)
        {
            return await _context.InvestmentRebateSettings.Include(x => x.taxYear).Where(x => x.taxYearId == Id).AsNoTracking().ToListAsync();
        }
        #endregion

        #region Salary Head
        public async Task<bool> SaveSalaryHead(SalaryHead salaryHead)
        {
            if (salaryHead.Id != 0)
            {
                _context.salaryHeads.Update(salaryHead);
            }
            else
            {
                _context.salaryHeads.Add(salaryHead);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryHead>> GetAllSalaryHead()
        {
            return await _context.salaryHeads.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalaryHead>> GetAllSalaryHeadByFilter(string filter)
        {
            return await _context.salaryHeads.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteSalaryHeadById(int id)
        {
            _context.salaryHeads.Remove(_context.salaryHeads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Salary Type
        public async Task<bool> SaveSalaryType(SalaryType salaryType)
        {
            if (salaryType.Id != 0)
            {
                _context.salaryTypes.Update(salaryType);
            }
            else
            {
                _context.salaryTypes.Add(salaryType);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryType>> GetAllSalaryType()
        {
            return await _context.salaryTypes.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Bonus Type
        public async Task<bool> SaveBonusType(BonusType bonusType)
        {
            if (bonusType.Id != 0)
            {
                _context.bonusTypes.Update(bonusType);
            }
            else
            {
                _context.bonusTypes.Add(bonusType);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BonusType>> GetAllBonusType()
        {
            return await _context.bonusTypes.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Bonus Category

        public async Task<bool> SaveBonusCategory(BonousCategory bonusCategory)
        {
            if (bonusCategory.Id != 0)
            {
                _context.bonousCategories.Update(bonusCategory);
            }
            else
            {
                _context.bonousCategories.Add(bonusCategory);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BonousCategory>> GetAllBonusCategory()
        {
            return await _context.bonousCategories.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteBonusCategoryById(int id)
        {
            _context.bonousCategories.Remove(_context.bonousCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Bonus Sub Category

        public async Task<bool> SaveBonusSubCategory(BonousSubCategory bonusSubCategory)
        {
            if (bonusSubCategory.Id != 0)
            {
                _context.bonousSubCategories.Update(bonusSubCategory);
            }
            else
            {
                _context.bonousSubCategories.Add(bonusSubCategory);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BonousSubCategory>> GetAllBonusSubCategory()
        {
            return await _context.bonousSubCategories.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BonousSubCategory>> GetBonusSubCategoryByMasterId(int masterId)
        {
            return await _context.bonousSubCategories.Where(a => a.bonousCategoryId == masterId).ToListAsync();
        }

        public async Task<bool> DeleteBonusSubCategoryById(int id)
        {
            _context.bonousSubCategories.Remove(_context.bonousSubCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Bonous Structure
        public async Task<int> SaveBonousStructure(BonousStructure bonousStructure)
        {
            if (bonousStructure.Id != 0)
            {
                _context.bonousStructures.Update(bonousStructure);
            }
            else
            {
                _context.bonousStructures.Add(bonousStructure);
            }
            await _context.SaveChangesAsync();
            return bonousStructure.Id;
        }

        public async Task<IEnumerable<BonousStructure>> GetAllBonousStructure()
        {
            return await _context.bonousStructures.Include(a => a.bonousSubCategory).Include(a => a.bonousSubCategory.bonousCategory).Include(a => a.salaryCalulationType).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteBonousStructureById(int id)
        {
            _context.bonousStructures.Remove(_context.bonousStructures.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Employees Bonous Structure
        public async Task<int> SaveEmployeesBonusStructure(EmployeesBonusStructure bonousStructure)
        {
            if (bonousStructure.Id != 0)
            {
                _context.employeesBonusStructures.Update(bonousStructure);
            }
            else
            {
                _context.employeesBonusStructures.Add(bonousStructure);
            }
            await _context.SaveChangesAsync();
            return bonousStructure.Id;
        }

        public async Task<IEnumerable<EmployeesBonusStructure>> GetEmployeesBonusStructureByBonusId(int bonusId)
        {
            return await _context.employeesBonusStructures.Include(a => a.employeeInfo).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteEmployeesBonusStructureBybonusId(int bonusId)
        {
            _context.employeesBonusStructures.RemoveRange(_context.employeesBonusStructures.Where(x => x.bonousStructureId == bonusId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Wallet Type
        public async Task<bool> SaveWalletType(WalletType walletType)
        {
            if (walletType.Id != 0)
            {
                _context.walletTypes.Update(walletType);
            }
            else
            {
                _context.walletTypes.Add(walletType);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WalletType>> GetAllWalletType()
        {
            return await _context.walletTypes.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteWalletTypeById(int id)
        {
            _context.walletTypes.Remove(_context.walletTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Employees Cash Setup
        public async Task<int> SaveEmployeesCashSetup(EmployeesCashSetup employeesCashSetup)
        {
            if (employeesCashSetup.Id != 0)
            {
                _context.employeesCashSetups.Update(employeesCashSetup);
            }
            else
            {
                _context.employeesCashSetups.Add(employeesCashSetup);
            }
            await _context.SaveChangesAsync();
            return employeesCashSetup.Id;
        }

        public async Task<IEnumerable<EmployeesCashSetup>> GetAllEmployeesCashSetup()
        {
            return await _context.employeesCashSetups.Include(a => a.employeeInfo).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmployeesCashSetup>> GetEmployeesCashSetupByEmployeeId(int empId)
        {
            return await _context.employeesCashSetups.Where(a => a.employeeInfoId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteEmployeesCashSetupById(int id)
        {
            _context.employeesCashSetups.Remove(_context.employeesCashSetups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Salary Calulation Type
        public async Task<bool> SaveSalaryCalulationType(SalaryCalulationType salaryCalulationType)
        {
            if (salaryCalulationType.Id != 0)
            {
                _context.salaryCalulationTypes.Update(salaryCalulationType);
            }
            else
            {
                _context.salaryCalulationTypes.Add(salaryCalulationType);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryCalulationType>> GetAllSalaryCalulationType()
        {
            return await _context.salaryCalulationTypes.AsNoTracking().ToListAsync();
        }
        #endregion

        #region Salary Period
        public async Task<bool> SaveSalaryPeriod(SalaryPeriod salaryPeriod)
        {
            if (salaryPeriod.Id != 0)
            {
                _context.salaryPeriods.Update(salaryPeriod);
            }
            else
            {
                _context.salaryPeriods.Add(salaryPeriod);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> EditSalaryPeriodForlockLabel(int Id, int lockLabel)
        {
            var SalaryPeriod = _context.salaryPeriods.Find(Id);
            SalaryPeriod.lockLabel = lockLabel;
            _context.Entry(SalaryPeriod).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> SetSalaryPeriodLock(int Id, int lockLabel,string lockBy)
        {
            var SalaryPeriod = _context.salaryPeriods.Find(Id);
            SalaryPeriod.lockLabel = lockLabel;
            SalaryPeriod.lockDate = DateTime.Now;
            SalaryPeriod.lockBy = lockBy;
            _context.Entry(SalaryPeriod).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryPeriod>> GetAllSalaryPeriod()
        {
            return await _context.salaryPeriods.Include(a => a.salaryType).Include(a => a.salaryYear).Include(a => a.taxYear).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<SalaryPeriod>> GetSalaryPeriodById(int PeriodId)
        {
            return await _context.salaryPeriods.Where(x => x.Id == PeriodId).Include(a => a.salaryType).Include(a => a.salaryYear).Include(a => a.taxYear).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalaryPeriod>> GetDuplicateSalaryPeriodById(int PeriodId, int yearId, int typeId, string month)
        {
            return await _context.salaryPeriods.Where(x => x.Id != PeriodId && x.salaryYearId == yearId && x.salaryTypeId == typeId && x.monthName == month).AsNoTracking().ToListAsync();
        }

        public async Task<SalaryPeriod> GetSalaryPeriodNameById(int periodId)
        {
            return await _context.salaryPeriods.Where(x => x.Id == periodId).Include(a => a.salaryType).Include(a => a.salaryYear).Include(a => a.taxYear).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<SalaryPeriod> GetSalaryPeriodmax()
        {
            var data = await _context.salaryPeriods.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            return await _context.salaryPeriods.Where(x => x.Id == data.Id).Include(a => a.salaryType).Include(a => a.salaryYear).Include(a => a.taxYear).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteSalaryPeriodById(int id)
        {
            _context.salaryPeriods.Remove(_context.salaryPeriods.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Salary Slab
        public async Task<bool> SaveSalarySlab(SalarySlab salarySlab)
        {
            if (salarySlab.Id != 0)
            {
                _context.salarySlabs.Update(salarySlab);
            }
            else
            {
                _context.salarySlabs.Add(salarySlab);
            }
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<SalarySlab> GetSalarySlabById(int Id)
        {
            return await _context.salarySlabs.Include(a => a.salaryGrade).Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<SalarySlab>> GetAllSalarySlab()
        {
            return await _context.salarySlabs.Include(a => a.salaryGrade).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<SalarySlab>> GetSalarySlabBysalaryGradeId(int salaryGradeId)
        {
            return await _context.salarySlabs.Include(a => a.salaryGrade).Where(x => x.salaryGradeId == salaryGradeId).AsNoTracking().ToListAsync();
        }
        #endregion

        #region Salary Grade Percent
        public async Task<bool> SaveSalaryGradePercent(SalaryGradePercent salaryGradePercent)
        {
            if (salaryGradePercent.Id != 0)
            {
                _context.salaryGradePercents.Update(salaryGradePercent);
            }
            else
            {
                _context.salaryGradePercents.Add(salaryGradePercent);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryGradePercent>> GetAllSalaryGradePercent()
        {
            return await _context.salaryGradePercents.Include(a => a.salaryGrade).Include(a => a.salaryHead).Include(a => a.salaryCalulationType).AsNoTracking().ToListAsync();
        }
        public async Task<SalaryGradePercent> GetSalaryGradePercentBysalaryHeadId(int salaryGradeId, int salaryHeadId)
        {
            return await _context.salaryGradePercents.Include(a => a.salaryGrade).Include(a => a.salaryHead).Include(a => a.salaryCalulationType).Where(x => x.salaryGradeId == salaryGradeId && x.salaryHeadId == salaryHeadId).AsNoTracking().FirstOrDefaultAsync();
        }
        #endregion

        #region Employees Salary Structure
        public async Task<bool> SaveEmployeesSalaryStructure(EmployeesSalaryStructure employeesSalaryStructure)
        {
            if (employeesSalaryStructure.Id != 0)
            {
                _context.employeesSalaryStructures.Update(employeesSalaryStructure);
            }
            else
            {
                _context.employeesSalaryStructures.Add(employeesSalaryStructure);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> EditEmployeesSalaryStructure(int Id, decimal amount, string status)
        {
            var SalaryStructur = _context.employeesSalaryStructures.Find(Id);
            SalaryStructur.amount = amount;
            SalaryStructur.isActive = status;
            _context.Entry(SalaryStructur).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeesSalaryStructure>> GetAllEmployeesSalaryStructure()
        {
            return await _context.employeesSalaryStructures.Include(x => x.salaryHead).Include(x => x.salarySlab).Include(x => x.employeeInfo).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmployeesSalaryStructure>> GetEmployeesSalaryStructureByEmpId(int empId)
        {
            return await _context.employeesSalaryStructures.Include(x => x.salaryHead).Include(x => x.salarySlab).Where(x => x.employeeInfoId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeesSalaryStructure> GetEmpStructureByEmpId(int empId)
        {
            return await _context.employeesSalaryStructures.Include(x => x.employeeInfo.department).Include(x => x.salaryHead).Include(x => x.salarySlab).Where(x => x.employeeInfoId == empId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteEmployeesSalaryStructureByempId(int empId)
        {
            _context.employeesSalaryStructures.RemoveRange(_context.employeesSalaryStructures.Where(x => x.employeeInfoId == empId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FsSalaryStructureViewModel>> GetFsStructure(int empId, int day)
        {
            return await _context.fsSalaryStructureViewModels.FromSql($"SP_Get_FinalSettlementStructure {empId},{day}").AsNoTracking().ToListAsync();
        }

        #endregion

        #region Salary Structure History

        public async Task<IEnumerable<SalaryProcessDataViewModel>> SaveStructureHistory(int employeeInfoId, string changeBy)
        {
            return await _context.salaryProcessDataViewModels.FromSql($"SP_Save_SalaryStructureHistory {employeeInfoId},{changeBy}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalaryProcessDataViewModel>> UpdateStructureHistory(int structureId, string changeBy)
        {
            return await _context.salaryProcessDataViewModels.FromSql($"SP_Update_SalaryStructureHistory {structureId},{changeBy}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<System.Object>> GetStructureHistoryByEmpId(int empId)
        {
            return await _context.structureHistoryReportViewModels.FromSql($"SP_Get_SalaryStructureHistoryByEmpId {empId}").OrderByDescending(a => a.historyCode).AsNoTracking().ToListAsync();
        }

        #endregion

        #region Wages Salary Structure
        public async Task<bool> SaveWagesSalaryStructure(WagesSalaryStructure wagesSalaryStructure)
        {
            if (wagesSalaryStructure.Id != 0)
            {
                _context.wagesSalaryStructures.Update(wagesSalaryStructure);
            }
            else
            {
                _context.wagesSalaryStructures.Add(wagesSalaryStructure);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> EditWagesSalaryStructure(int Id, decimal amount, string status)
        {
            var SalaryStructur = _context.wagesSalaryStructures.Find(Id);
            SalaryStructur.amount = amount;
            _context.Entry(SalaryStructur).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<WagesSalaryStructure>> GetAllWagesSalaryStructure()
        {
            return await _context.wagesSalaryStructures.Include(x => x.salaryHead).Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<WagesSalaryStructure>> GetWagesSalaryStructureByEmpId(int empId)
        {
            return await _context.wagesSalaryStructures.Include(x => x.salaryHead).Where(x => x.employeeId == empId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteWagesSalaryStructureByempId(int empId)
        {
            _context.wagesSalaryStructures.RemoveRange(_context.wagesSalaryStructures.Where(x => x.employeeId == empId));
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<bool> DeleteWagesSalaryStructureById(int Id)
        {
            _context.wagesSalaryStructures.Remove(_context.wagesSalaryStructures.Where(x => x.Id == Id).FirstOrDefault());
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Leave Without Pay
        public async Task<bool> SaveLeaveWithoutPay(LeaveWithoutPay leaveWithoutPay)
        {
            if (leaveWithoutPay.Id != 0)
                _context.LeaveWithoutPays.Update(leaveWithoutPay);
            else
                _context.LeaveWithoutPays.Add(leaveWithoutPay);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeaveWithoutPay>> GetAllLeaveWithoutPay()
        {
            return await _context.LeaveWithoutPays.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<LeaveWithoutPay>> GetLeaveWithoutPayBysalaryPeriodId(int salaryPeriodId)
        {
            return await _context.LeaveWithoutPays.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.salaryPeriodId == salaryPeriodId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<LeaveWithoutPay>> GetLeaveWithoutPayByemployeeInfoId(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.LeaveWithoutPays.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.salaryPeriodId == salaryPeriodId && x.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }

        public async Task<LeaveWithoutPay> GetLeaveWithoutPayById(int id)
        {
            return await _context.LeaveWithoutPays.FindAsync(id);
        }

        public async Task<bool> DeleteLeaveWithoutPayById(int id)
        {
            _context.LeaveWithoutPays.Remove(_context.LeaveWithoutPays.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Process Emp Salary Structure
        public async Task<bool> SaveProcessEmpSalaryStructure(ProcessEmpSalaryStructure processEmpSalaryStructure)
        {
            if (processEmpSalaryStructure.Id != 0)
            {
                _context.ProcessEmpSalaryStructures.Update(processEmpSalaryStructure);
            }
            else
            {
                _context.ProcessEmpSalaryStructures.Add(processEmpSalaryStructure);
            }
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> EditProcessEmpSalaryStructureForLeaveWithoutPay(int employeeInfoId, int salaryPeriodId, int noOfDays)
        {
            List<int> salaryHeads = new List<int> { 1, 2, 3, 4, 10, 11, 12, 14 };
            IEnumerable<ProcessEmpSalaryStructure> lstsalaryStructure = await _context.ProcessEmpSalaryStructures.Where(x => x.employeeInfoId == employeeInfoId && x.salaryPeriodId == salaryPeriodId && x.amount > 0 && salaryHeads.Contains(x.salaryHeadId)).AsNoTracking().ToListAsync();
            decimal? totalworkingdays = _context.salaryPeriods.Where(x => x.Id == salaryPeriodId).FirstOrDefault().daysWorking;

            foreach (ProcessEmpSalaryStructure ProcesssalaryStructure in lstsalaryStructure)
            {
                var SalaryStructur = _context.ProcessEmpSalaryStructures.Find(ProcesssalaryStructure.Id);
                SalaryStructur.amount = SalaryStructur.amount - SalaryStructur.amount * noOfDays / Convert.ToDecimal(totalworkingdays);
                _context.Entry(SalaryStructur).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> EditProcessEmpSalaryStructureForAdvanceAdjustment(int employeeInfoId, int salaryPeriodId, int salaryHeadId, decimal amount)
        {
            var ProcesssalaryStructure = await _context.ProcessEmpSalaryStructures.Where(x => x.employeeInfoId == employeeInfoId && x.salaryPeriodId == salaryPeriodId && x.salaryHeadId == salaryHeadId).AsNoTracking().ToListAsync();
            if (ProcesssalaryStructure.Count() > 0)
            {
                var SalaryStructur = _context.ProcessEmpSalaryStructures.Find(ProcesssalaryStructure.FirstOrDefault().Id);
                SalaryStructur.amount = amount;
                _context.Entry(SalaryStructur).State = EntityState.Modified;
            }

            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> EditProcessEmpSalaryStructureForEmployeeArrear(int employeeInfoId, int salaryPeriodId, decimal totalamount, decimal amount)
        {
            var ProcesssalaryStructure = await _context.ProcessEmpSalaryStructures.Where(x => x.employeeInfoId == employeeInfoId && x.salaryPeriodId == salaryPeriodId && x.salaryHeadId == 18).AsNoTracking().ToListAsync();
            if (ProcesssalaryStructure.Count() > 0)
            {
                var SalaryStructur = _context.ProcessEmpSalaryStructures.Find(ProcesssalaryStructure.FirstOrDefault().Id);
                SalaryStructur.amount = totalamount;
                _context.Entry(SalaryStructur).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            var ProcesssalaryStructurePFOwn = await _context.ProcessEmpSalaryStructures.Where(x => x.employeeInfoId == employeeInfoId && x.salaryPeriodId == salaryPeriodId && x.salaryHeadId == 5 && x.amount > 0).AsNoTracking().ToListAsync();
            var salaryGrade = await _context.employeesSalaryStructures.Include(x => x.salarySlab).Where(x => x.employeeInfoId == employeeInfoId && x.salaryHeadId == 1).AsNoTracking().ToListAsync();
            if (ProcesssalaryStructurePFOwn.Count() > 0)
            {
                var SalaryStructurPFOwn = _context.ProcessEmpSalaryStructures.Find(ProcesssalaryStructurePFOwn.FirstOrDefault().Id);
                var percentAmount = await _context.salaryGradePercents.Where(x => x.salaryHeadId == SalaryStructurPFOwn.salaryHeadId && x.salaryGradeId == salaryGrade.FirstOrDefault().salarySlab.salaryGradeId).AsNoTracking().ToListAsync();
                decimal? percent = percentAmount.FirstOrDefault().percentAmount / 100;
                SalaryStructurPFOwn.amount = SalaryStructurPFOwn.amount + amount * Convert.ToDecimal(percent);
                _context.Entry(SalaryStructurPFOwn).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            var ProcesssalaryStructurePFEmployer = await _context.ProcessEmpSalaryStructures.Where(x => x.employeeInfoId == employeeInfoId && x.salaryPeriodId == salaryPeriodId && x.salaryHeadId == 29 && x.amount > 0).AsNoTracking().ToListAsync();
            if (ProcesssalaryStructurePFEmployer.Count() > 0)
            {
                var SalaryStructurPFEmployer = _context.ProcessEmpSalaryStructures.Find(ProcesssalaryStructurePFEmployer.FirstOrDefault().Id);
                var percentAmountPFEmployer = await _context.salaryGradePercents.Where(x => x.salaryHeadId == SalaryStructurPFEmployer.salaryHeadId && x.salaryGradeId == salaryGrade.FirstOrDefault().salarySlab.salaryGradeId).AsNoTracking().ToListAsync();
                decimal? percentPFEmployer = percentAmountPFEmployer.FirstOrDefault().percentAmount / 100;
                SalaryStructurPFEmployer.amount = SalaryStructurPFEmployer.amount + amount * Convert.ToDecimal(percentPFEmployer);
                _context.Entry(SalaryStructurPFEmployer).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }

            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ProcessEmpSalaryStructure>> GetProcessEmpSalaryStructureByemployeeInfoId(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.ProcessEmpSalaryStructures.Include(a => a.salaryPeriod).Include(a => a.salaryHead).Include(a => a.employeeInfo).Where(x => x.salaryPeriodId == salaryPeriodId && x.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProcessEmpSalaryStructure>> GetProcessEmpSalaryStructureBysalaryPeriodId(int salaryPeriodId)
        {
            return await _context.ProcessEmpSalaryStructures.Include(a => a.salaryPeriod).Include(a => a.employeeInfo).Where(x => x.salaryPeriodId == salaryPeriodId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteProcessEmpSalaryStructureByempId(int empId, int salaryPeriodId)
        {
            _context.ProcessEmpSalaryStructures.RemoveRange(_context.ProcessEmpSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryPeriodId == salaryPeriodId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProcessEmpSalaryStructureBysalaryPeriodId(int salaryPeriodId)
        {
            _context.ProcessEmpSalaryStructures.RemoveRange(_context.ProcessEmpSalaryStructures.Where(x => x.salaryPeriodId == salaryPeriodId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<decimal> GetNetPayableByemployeeInfoId(int employeeInfoId, int salaryPeriodId)
        {
            IEnumerable<ProcessEmpSalaryStructure> salaryMasters = await _context.ProcessEmpSalaryStructures.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Include(x => x.salaryHead).Where(x => x.employeeInfoId == employeeInfoId && x.salaryPeriodId == salaryPeriodId).AsNoTracking().ToListAsync();

            decimal additions = salaryMasters.Where(x => x.salaryHead.salaryHeadType == "Addition").Sum(x => x.amount);
            decimal deductions = salaryMasters.Where(x => x.salaryHead.salaryHeadType == "Deduction").Sum(x => x.amount);

            decimal netPayable = additions - deductions;

            return netPayable;
        }

        #endregion 

        #region Process Emp Salary Master
        public async Task<bool> SaveProcessEmpSalaryMaster(ProcessEmpSalaryMaster processEmpSalaryMaster)
        {
            if (processEmpSalaryMaster.Id != 0)
            {
                _context.ProcessEmpSalaryMasters.Update(processEmpSalaryMaster);
            }
            else
            {
                _context.ProcessEmpSalaryMasters.Add(processEmpSalaryMaster);
            }
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ProcessEmpSalaryMaster>> GetProcessEmpSalaryMasterByemployeeInfoId(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.ProcessEmpSalaryMasters.Include(a => a.salaryPeriod).Include(a => a.employeeInfo).Where(x => x.salaryPeriodId == salaryPeriodId && x.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ProcessEmpSalaryMaster>> GetProcessEmpSalaryMasterBysalaryPeriodId(int salaryPeriodId)
        {
            return await _context.ProcessEmpSalaryMasters.Include(a => a.salaryPeriod).Include(a => a.employeeInfo).Where(x => x.salaryPeriodId == salaryPeriodId).AsNoTracking().ToListAsync();
        }
        public async Task<bool> DeleteProcessEmpSalaryMasterByempId(int empId, int salaryPeriodId)
        {
            _context.ProcessEmpSalaryMasters.RemoveRange(_context.ProcessEmpSalaryMasters.Where(x => x.employeeInfoId == empId && x.salaryPeriodId == salaryPeriodId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteProcessEmpSalaryMasterBysalaryPeriodId(int salaryPeriodId)
        {
            _context.ProcessEmpSalaryMasters.RemoveRange(_context.ProcessEmpSalaryMasters.Where(x => x.salaryPeriodId == salaryPeriodId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryProcessDataViewModel>> ProcessEmpSalaryMasterBySp(int? salaryPeriodId, int? employeeInfoId)
        {
            return await _context.salaryProcessDataViewModels.FromSql($"SP_ProcessEmployeeSalaryMaster {salaryPeriodId},{employeeInfoId}").AsNoTracking().ToListAsync();
        }
        #endregion

        #region Salary Process Log

        public async Task<bool> SaveSalaryProcessLog(SalaryProcessLog salaryProcessLog)
        {
            if (salaryProcessLog.Id != 0)
            {
                _context.SalaryProcessLogs.Update(salaryProcessLog);
            }
            else
            {
                _context.SalaryProcessLogs.Add(salaryProcessLog);
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryProcessLog>> GetAllSalaryProcessLog()
        {
            return await _context.SalaryProcessLogs.Include(a => a.salaryPeriod).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        #endregion

        #region Salary Status Log

        public async Task<int> SaveSalaryStatusLog(SalaryStatusLog salaryStatusLog)
        {
            if (salaryStatusLog.Id != 0)
            {
                _context.SalaryStatusLogs.Update(salaryStatusLog);
            }
            else
            {
                _context.SalaryStatusLogs.Add(salaryStatusLog);
            }
            await _context.SaveChangesAsync();
            return salaryStatusLog.Id;
        }

        public async Task<IEnumerable<SalaryStatusLog>> GetSalaryStatusLogByPeriodId(int periodId)
        {
            return await _context.SalaryStatusLogs.Include(a => a.salaryPeriod).Where(a => a.salaryPeriodId == periodId).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        #endregion

        #region Process Salary Remarks

        public async Task<int> SaveProcessSalaryRemarks(ProcessSalaryRemarks processSalaryRemarks)
        {
            if (processSalaryRemarks.Id != 0)
            {
                _context.ProcessSalaryRemarks.Update(processSalaryRemarks);
            }
            else
            {
                _context.ProcessSalaryRemarks.Add(processSalaryRemarks);
            }
            await _context.SaveChangesAsync();
            return processSalaryRemarks.Id;
        }

        public async Task<bool> DeleteProcessSalaryRemarks(int? empId,int? periodId)
        {
            _context.ProcessSalaryRemarks.RemoveRange(_context.ProcessSalaryRemarks.Where(a => a.employeeInfoId == empId && a.salaryPeriodId == periodId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Employee Arrear
        public async Task<bool> SaveEmployeeArrear(EmployeeArrear employeeArrear)
        {
            if (employeeArrear.Id != 0)
                _context.EmployeeArrears.Update(employeeArrear);
            else
                _context.EmployeeArrears.Add(employeeArrear);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeArrear>> GetAllEmployeeArrear()
        {
            return await _context.EmployeeArrears.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Include(x => x.salaryHead).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<EmployeeArrear>> GetEmployeeArrearBysalaryPeriodId(int salaryPeriodId)
        {
            return await _context.EmployeeArrears.Include(x => x.salaryHead).Include(x => x.employeeInfo).Where(x => x.salaryPeriodId == salaryPeriodId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<EmployeeArrear>> GetEmployeeArrearByEmpAndPeriodId(int empId, int periodId)
        {
            return await _context.EmployeeArrears.Include(x => x.salaryHead).Include(x => x.employeeInfo).Where(x => x.employeeInfoId == empId && x.salaryPeriodId == periodId).AsNoTracking().ToListAsync();
        }

        public async Task<EmployeeArrear> GetEmployeeArrearById(int id)
        {
            return await _context.EmployeeArrears.FindAsync(id);
        }

        public async Task<bool> DeleteEmployeeArrearByEmpAndPeriodId(int empId, int periodId)
        {
            _context.EmployeeArrears.RemoveRange(_context.EmployeeArrears.Where(x => x.employeeInfoId == empId && x.salaryPeriodId == periodId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeArrear>> EmployeeArrearCalculationByEmpId(int empId, int periodId, decimal? totalAmount, decimal? bonusAmount)
        {
            List<EmployeeArrear> EmployeeArrear = new List<EmployeeArrear>();
            List<int> salaryHeadId = new List<int>();

            decimal? GrossAmount = 0;
            decimal? additions = 0;
            var data = _context.employeesSalaryStructures.Include(x => x.salaryHead).Where(x => x.employeeInfoId == empId).ToList();
            if (bonusAmount > 0)
            {
                additions = data.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.isActive == "Active").Sum(x => x.amount);
                salaryHeadId = _context.salaryHeads.Where(x => x.isArrear == "Yes").Select(x => x.Id).ToList();
            }
            else
            {
                additions = data.Where(x => x.salaryHead.salaryHeadType == "Addition" && x.isActive == "Active" && x.salaryHead.isBonus != "Yes").Sum(x => x.amount);
                salaryHeadId = _context.salaryHeads.Where(x => x.isArrear == "Yes" && x.isBonus != "Yes").Select(x => x.Id).ToList();
            }
            decimal? deductions = data.Where(x => x.salaryHead.salaryHeadType == "Deduction" && x.isActive == "Active").Sum(x => x.amount);
            GrossAmount = additions - deductions;


            List<EmployeesSalaryStructure> lstStructure = _context.employeesSalaryStructures.Include(x => x.employeeInfo).Include(x => x.salaryHead).Where(x => salaryHeadId.Contains(x.salaryHeadId) && x.employeeInfoId == empId).ToList();



            foreach (EmployeesSalaryStructure item in lstStructure)
            {
                EmployeeArrear.Add(new EmployeeArrear
                {
                    employeeInfoId = item.employeeInfoId,
                    salaryPeriodId = periodId,
                    salaryHeadId = item.salaryHeadId,
                    salaryHeadName = item.salaryHead.salaryHeadName,
                    arrearAmount = Convert.ToDecimal((item.amount * totalAmount) / GrossAmount),
                    ratio = Convert.ToDecimal((item.amount / GrossAmount) * 100)
                });
            }

            return EmployeeArrear;
        }

        #endregion

        #region Advance Adjustment

        public async Task<int> SaveAdvanceAdjustment(AdvanceAdjustment advanceAdjustment)
        {
            if (advanceAdjustment.Id != 0)
                _context.AdvanceAdjustments.Update(advanceAdjustment);
            else
                _context.AdvanceAdjustments.Add(advanceAdjustment);
             await _context.SaveChangesAsync();
            return advanceAdjustment.Id;
        }

        public async Task<IEnumerable<AdvanceAdjustment>> GetAllAdvanceAdjustment()
        {
            return await _context.AdvanceAdjustments.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AdvanceAdjustment>> GetAdvanceAdjustmentBysalaryPeriodId(int salaryPeriodId)
        {
            return await _context.AdvanceAdjustments.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.salaryPeriodId <= salaryPeriodId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AdvanceAdjustment>> GetAdvanceAdjustmentByemployeeInfoId(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.AdvanceAdjustments.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.salaryPeriodId <= salaryPeriodId && x.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }
        public async Task<AdvanceAdjustment> GetAdvanceAdjustmentById(int id)
        {
            return await _context.AdvanceAdjustments.FindAsync(id);
        }

        public async Task<bool> DeleteAdvanceAdjustmentById(int id)
        {
            _context.AdvanceAdjustments.Remove(_context.AdvanceAdjustments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> SaveAdvanceAdjustmentDetail(AdvanceAdjustmentDetail advanceAdjustment)
        {
            if (advanceAdjustment.Id != 0)
                _context.advanceAdjustmentDetails.Update(advanceAdjustment);
            else
                _context.advanceAdjustmentDetails.Add(advanceAdjustment);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AdvanceAdjustmentDetail>> GetAllAdvanceAdjustmentDetailByMasterId(int id)
        {
            return await _context.advanceAdjustmentDetails.Where(x => x.advanceAdjustmentId == id).Include(x => x.advanceAdjustment.employeeInfo.department).Include(x => x.advanceAdjustment.salaryHead).AsNoTracking().ToListAsync();
        }

        public void UpdateAdvanceAdjustmentDetail(int docId, decimal? amount)
        {
            var user = _context.advanceAdjustmentDetails.Find(docId);
            user.scheduleAmount = amount;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion

        #region Loan Policy

        public async Task<bool> SaveLoanPolicy(LoanPolicy loanPolicy)
        {
            if (loanPolicy.Id != 0)
                _context.LoanPolicies.Update(loanPolicy);
            else
                _context.LoanPolicies.Add(loanPolicy);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanPolicy>> GetAllLoanPolicy()
        {
            return await _context.LoanPolicies.Include(x => x.salaryGrade).Include(x => x.salaryHead).AsNoTracking().ToListAsync();
        }

        public async Task<LoanPolicy> GetLoanPolicyById(int id)
        {
            return await _context.LoanPolicies.FindAsync(id);
        }

        public async Task<bool> DeleteLoanPolicyById(int id)
        {
            _context.LoanPolicies.Remove(_context.LoanPolicies.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Loan Schedule
        public async Task<int> SaveLoanScheduleMaster(LoanScheduleMaster advanceAdjustment)
        {
            if (advanceAdjustment.Id != 0)
                _context.loanScheduleMasters.Update(advanceAdjustment);
            else
                _context.loanScheduleMasters.Add(advanceAdjustment);
             await _context.SaveChangesAsync();
            return advanceAdjustment.Id;
        }

        public async Task<bool> SaveLoanScheduleDetail(LoanScheduleDetail advanceAdjustment)
        {
            if (advanceAdjustment.Id != 0)
                _context.loanScheduleDetails.Update(advanceAdjustment);
            else
                _context.loanScheduleDetails.Add(advanceAdjustment);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanScheduleMaster>> GetAllLoanScheduleMaster()
        {
            return await _context.loanScheduleMasters.Include(x => x.employeeInfo).Include(x => x.loanPolicy.salaryHead).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LoanScheduleMaster>> GetAllLoanScheduleMasterByEmpId(int? empId)
        {
            return await _context.loanScheduleMasters.Include(x => x.employeeInfo).Include(x => x.loanPolicy.salaryHead).Where(a => a.employeeInfoId == empId).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<LoanScheduleDetail>> GetAllLoanScheduleDetailByMasterId(int id)
        {
            return await _context.loanScheduleDetails.Where(x=>x.loanScheduleMasterId==id).Include(x=>x.loanScheduleMaster.employeeInfo.department).Include(x => x.loanScheduleMaster.loanPolicy.salaryHead).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteLoanScheduleDetailById(int id)
        {
            _context.loanScheduleDetails.RemoveRange(_context.loanScheduleDetails.Where(x => x.loanScheduleMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public void UpdateLoanScheduleDetail(int docId,decimal? amount)
        {
            var user = _context.loanScheduleDetails.Find(docId);
            user.scheduleAmount = amount;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<bool> UpdateLoanScheduleMasterApproval(int Id, int status)
        {
            LoanScheduleMaster data = await _context.loanScheduleMasters.FindAsync(Id);
            if (data != null)
            {
                data.approveStatus = status;
                _context.loanScheduleMasters.Update(data);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<IEnumerable<LoanScheduleReportViewModel>> GetLoanReportById(int masterId)
        {
            return await _context.loanScheduleReportViewModels.FromSql($"SP_LoanScheduleById {masterId}").AsNoTracking().ToListAsync();
        }

        #endregion

        #region Loan Route

        public async Task<bool> SaveLoanRoute(LoanRoute loanRoute)
        {
            if (loanRoute.Id != 0)
                _context.LoanRoutes.Update(loanRoute);
            else
                _context.LoanRoutes.Add(loanRoute);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LoanRoute>> GetLoanRouteByEmpId(int empId)
        {
            return await _context.LoanRoutes.Include(x => x.loanScheduleMaster.employeeInfo.department).Include(x => x.loanScheduleMaster.loanPolicy.salaryHead).Where(x => x.employeeId == empId && x.isActive == 1).AsNoTracking().ToListAsync();
        }

        public async Task<LoanRoute> GetLoanRouteById(int id)
        {
            return await _context.LoanRoutes.FindAsync(id);
        }

        public async Task<bool> UpdateLoanRoute(int Id, int Type)
        {
            LoanRoute data = await _context.LoanRoutes.FindAsync(Id);
            if (data != null)
            {
                data.isActive = Type;
                _context.LoanRoutes.Update(data);
                return 1 == await _context.SaveChangesAsync();
            }
            return false;
        }

        public async Task<LoanRoute> GetLoanRouteByRouteOrder(int id, int order)
        {

            return await _context.LoanRoutes.Where(x => x.loanScheduleMasterId == id && x.routeOrder == order).FirstOrDefaultAsync();
        }

        #endregion

        #region Lfa Info

        public async Task<bool> SaveLfaInfo(LfaInfo lfaInfo)
        {
            if (lfaInfo.Id != 0)
                _context.LfaInfos.Update(lfaInfo);
            else
                _context.LfaInfos.Add(lfaInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LfaInfo>> GetAllLfaInfo()
        {
            return await _context.LfaInfos.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).AsNoTracking().ToListAsync();
        }

        public async Task<LfaInfo> GetLfaInfoByEmpId(int empId)
        {
            int? lfaIds = _context.LfaInfos.Where(a => a.employeeInfoId == empId).Count();

            return await _context.LfaInfos.Where(a => a.Id == lfaIds).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteLfaInfoById(int id)
        {
            _context.LfaInfos.Remove(_context.LfaInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<ProcessEmpSalaryStructure> GetLastYearBasic(int empId, int periodId)
        {
            int yearName = Convert.ToInt32(_context.salaryPeriods.Include(x => x.salaryYear).Where(x => x.Id == periodId).Select(x => x.salaryYear.yearName).FirstOrDefault());
            int minusYear = yearName - 1;

            //var yearNames = _context.salaryPeriods.Include(x => x.salaryYear).Where(x => x.Id == periodId).ToList();
            ////int minusYear =Convert.ToInt32(yearName) - 1;
            //string yearName = "";
            //foreach (SalaryPeriod item in yearNames)
            //{
            //    yearName = item.salaryYear.yearName;
            //}

            int periodIds = _context.salaryPeriods.Include(x => x.salaryYear).Where(x => x.Id == periodId && x.salaryTypeId == 1 && x.salaryYear.yearName == yearName.ToString()).Max(x => x.Id);

            return await _context.ProcessEmpSalaryStructures.Where(a => a.employeeInfoId == empId && a.salaryHeadId == 1 && a.salaryPeriodId == periodIds).FirstOrDefaultAsync();
        }
        #endregion

        #region Salary Process
        public async Task<IEnumerable<SalaryProcessDataViewModel>> EmpSalaryProcess(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.salaryProcessDataViewModels.FromSql($"spProcessEmpSalary {salaryPeriodId},{employeeInfoId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalaryProcessDataViewModel>> WagesSalaryProcess(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.salaryProcessDataViewModels.FromSql($"spProcessWagesSalary {salaryPeriodId},{employeeInfoId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalaryProcessDataViewModel>> EmpBonusProcess(int salaryPeriodId, int salaryHeadId, int employeeInfoId, DateTime? lastDayofPeriod, string userName, string bonusFor)
        {
            return await _context.salaryProcessDataViewModels.FromSql($"spProcessEmpBonusSalary {salaryPeriodId},{salaryHeadId},{employeeInfoId},{lastDayofPeriod},{userName},{bonusFor}").AsNoTracking().ToListAsync();
        }

        #endregion

        #region Payroll Report

        public async Task<IEnumerable<PayslipReportViewModel>> GetPaySlipByEmpId(int employeeInfoId, int salaryPeriodId)
        {
            return await _context.payslipReportViewModels.FromSql($"spRptPaySlip {employeeInfoId}, {salaryPeriodId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PayslipReportViewModel>> GetWagesPaySlipByEmpId(int employeeInfoId, int salaryPeriodId)
        {
            return await _context.payslipReportViewModels.FromSql($"spWagesRptPaySlip {employeeInfoId}, {salaryPeriodId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PayslipReportViewModel>> GetPaySlipAdditionByEmpId(int employeeInfoId, int salaryPeriodId)
        {
            return await _context.payslipReportViewModels.FromSql($"spRptPaySlipAddition {employeeInfoId}, {salaryPeriodId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PayslipReportViewModel>> GetWagesPaySlipAdditionByEmpId(int employeeInfoId, int salaryPeriodId)
        {
            return await _context.payslipReportViewModels.FromSql($"spWagesRptPaySlipAddition {employeeInfoId}, {salaryPeriodId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PayslipReportViewModel>> GetPaySlipDeductionByEmpId(int employeeInfoId, int salaryPeriodId)
        {
            return await _context.payslipReportViewModels.FromSql($"spRptPaySlipDeduction {employeeInfoId}, {salaryPeriodId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PayslipReportViewModel>> GetWagesPaySlipDeductionByEmpId(int employeeInfoId, int salaryPeriodId)
        {
            return await _context.payslipReportViewModels.FromSql($"spWagesRptPaySlipDeduction {employeeInfoId}, {salaryPeriodId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MonthlySalaryReportViewModel>> GetMonthlySalaryReportByPeriodId(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            List<MonthlySalaryReportViewModel> result =  await _context.monthlySalaryReportViewModels.FromSql($"spRptMonthlySalarySheet {salaryPeriodId}, {sbuId}, {pnsId}").AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BankStatementReportViewModel>> GetBankStatementByPeriodId(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            return await _context.bankStatementReportViewModels.FromSql($"SP_RPT_BankStatement {salaryPeriodId}, {sbuId}, {pnsId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ReconciliationReportViewModel>> GetReconcilationStatement(int? empId, int? salaryPeriodId, int? presalaryPeriodId, int? typeId)
        {
            return await _context.reconciliationReportViewModels.FromSql($"spRptTotalReconciliationStatement {empId}, {salaryPeriodId}, {presalaryPeriodId},{typeId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<MonthlySalaryReportViewModel>> GetWagesMonthlySalaryReportByPeriodId(int salaryPeriodId, int? sbuId, int? pnsId)
        {
            return await _context.monthlySalaryReportViewModels.FromSql($"spWagesRptMonthlySalarySheet {salaryPeriodId}, {sbuId}, {pnsId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GratuityReportViewModel>> GetGratuityReport()
        {
            return await _context.gratuityReportViewModels.FromSql("spRptGratuityNew").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<EmpTaxDetailsViewModel>> GetEmpTaxDetails(int employeeInfoId, int taxYearId)
        {
            return await _context.empTaxDetailsViewModels.FromSql($"spRptEmpTaxDetails {employeeInfoId}, {taxYearId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<EmpTaxSlabViewModel>> GetEmpTaxSlab(int employeeInfoId, int taxYearId)
        {
            return await _context.empTaxSlabViewModels.FromSql($"spRptTaxSlab {employeeInfoId}, {taxYearId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<EmpRebatableTaxViewModel>> GetEmpRebatableTax(int employeeInfoId, int taxYearId)
        {
            return await _context.empRebatableTaxViewModels.FromSql($"spRptRebatableTax {employeeInfoId}, {taxYearId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<EmpTaxDeductFinalViewModel>> GetEmpTaxDeductFinal(int employeeInfoId, int taxYearId)
        {
            return await _context.empTaxDeductFinalViewModels.FromSql($"RPT_FinalTaxDeduct {employeeInfoId}, {taxYearId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<UniversalCodaXLTempleteViewModel>> GetUniversalCodaXLTempleteViewModels(int PeriodId, int EmployeeId)
        {

            List<UniversalCodaXLTempleteViewModel> universalCodaXLTempleteViewModels = new List<UniversalCodaXLTempleteViewModel>();
            List<ProcessEmpSalaryStructure> GetPocessEmplSalStructure = _context.ProcessEmpSalaryStructures.Include(x => x.employeeInfo).Include(x => x.salaryPeriod.salaryYear).Include(x => x.salaryHead).Where(x => x.salaryPeriodId == PeriodId).ToList();
            List<int> lstemployeeId = new List<int>();
            List<FinanceCode> lstFinancialCode = _context.financeCodes.ToList();
            if (EmployeeId > 0)
            {

                lstemployeeId = GetPocessEmplSalStructure.Where(x => x.employeeInfoId == EmployeeId).Select(x => x.employeeInfoId).Distinct().ToList();
            }
            else
            {
                lstemployeeId = GetPocessEmplSalStructure.Select(x => x.employeeInfoId).Distinct().ToList();
            }

            List<EmployeeProjectActivity> employeeProjectActivities = _context.employeeProjectActivities.Include(x => x.hRActivity).Include(x => x.hRProject).Include(x => x.hRDoner).Where(x => lstemployeeId.Contains((int)x.employeeId) && x.isActive == 1).ToList();
            int slno = 0;
            foreach (int id in lstemployeeId)
            {
                string fnCode = lstFinancialCode.Where(x => x.employeeId == id).Select(x => x.fnCode).FirstOrDefault();
                EmployeeProjectActivity employeeProjectActivity = employeeProjectActivities.Where(x => x.employeeId == id).FirstOrDefault();
                decimal gross = GetPocessEmplSalStructure.Where(x => x.employeeInfoId == id && x.salaryHead.salaryHeadName == "Gross Salary").FirstOrDefault().amount;
                decimal PF = GetPocessEmplSalStructure.Where(x => x.employeeInfoId == id && x.salaryHead.salaryHeadName == "PF Deduction Staff").FirstOrDefault().amount;
                decimal TDS = GetPocessEmplSalStructure.Where(x => x.employeeInfoId == id && x.salaryHead.salaryHeadName == "Income Tax").FirstOrDefault().amount;
                int line = 0;
                slno++;
                foreach (ProcessEmpSalaryStructure pos in GetPocessEmplSalStructure.Where(x => x.employeeInfoId == id && x.amount > 0))
                {
                    string el1 = pos?.salaryHead?.salaryHeadCode;
                    if (el1 == null)
                    {
                        el1 = "";
                    }

                    string el2 = employeeProjectActivity?.hRProject?.code;
                    if (el2 == null)
                    {
                        el2 = "";
                    }
                    string el3 = employeeProjectActivity?.hRActivity?.code;
                    if (el3 == null)
                    {
                        el3 = "";
                    }
                    string el4 = employeeProjectActivity?.hRDoner?.code;
                    if (el4 == null)
                    {
                        el4 = "";
                    }
                    string grossstring = "";


                    if (pos.salaryHead.salaryHeadName == "Gross Salary")
                    {

                        gross = pos.amount;

                    }

                    if (pos.salaryHead.salaryHeadName == "PF Deduction Staff")
                    {

                        grossstring = "PF Salary " + pos.salaryPeriod.monthName + "-" + pos.salaryPeriod.salaryYear.yearName + "- " + pos.employeeInfo.nameEnglish;

                    }
                    else if (pos.salaryHead.salaryHeadName == "Income Tax")
                    {

                        grossstring = "TDS Salary " + pos.salaryPeriod.monthName + "-" + pos.salaryPeriod.salaryYear.yearName + "- " + pos.employeeInfo.nameEnglish;
                    }
                    else
                    {
                        grossstring = "Salary " + pos.salaryPeriod.monthName + "-" + pos.salaryPeriod.salaryYear.yearName + "- " + pos.employeeInfo.nameEnglish;
                    }
                    line++;
                    if (line == 1)
                    {
                        universalCodaXLTempleteViewModels.Add(new UniversalCodaXLTempleteViewModel
                        {

                            date = DateTime.Now.ToString("dd-MM-yyyy"),
                            year = pos?.salaryPeriod?.salaryYear?.yearName,
                            salaryPeriodId = pos?.salaryPeriodId,
                            documentDescription = "Salary " + pos?.salaryPeriod?.monthName + "-" + pos?.salaryPeriod?.salaryYear.yearName + "- " + pos?.employeeInfo?.nameEnglish,
                            cc = "BD",
                            doc = "PAYF",
                            docnum = slno.ToString(),
                            line = line.ToString(),
                            el1 = "2521",//el1, //pos?.salaryHead?.salaryHeadCode,
                            el2 = fnCode, //el2, //employeeProjectActivity?.hRProject?.code,
                            el3 = "",// employeeProjectActivity?.hRActivity?.code,
                            el4 = "",// employeeProjectActivity?.hRDoner?.code,
                            cur = "BDT",
                            // debit = 0,
                            credit = gross - PF - TDS,
                            linedescription = "Salary " + pos?.salaryPeriod?.monthName + "-" + pos?.salaryPeriod?.salaryYear?.yearName + "- " + pos?.employeeInfo?.nameEnglish,




                        });

                        line++;
                        universalCodaXLTempleteViewModels.Add(new UniversalCodaXLTempleteViewModel
                        {

                            date = DateTime.Now.ToString("dd-MM-yyyy"),
                            year = pos?.salaryPeriod?.salaryYear?.yearName,
                            salaryPeriodId = pos?.salaryPeriodId,
                            documentDescription = "Salary " + pos?.salaryPeriod?.monthName + "-" + pos?.salaryPeriod?.salaryYear.yearName + "- " + pos?.employeeInfo?.nameEnglish,
                            cc = "BD",
                            doc = "PAYF",
                            docnum = slno.ToString(),
                            line = line.ToString(),
                            //el1 = pos?.salaryHead?.salaryHeadCode,
                            //el2 = employeeProjectActivity?.hRProject?.code,
                            //el3 = employeeProjectActivity?.hRActivity?.code,
                            //el4 = employeeProjectActivity?.hRDoner?.code,
                            el1 = el1, //pos?.salaryHead?.salaryHeadCode,
                            el2 = el2, //employeeProjectActivity?.hRProject?.code,
                            el3 = el3,// employeeProjectActivity?.hRActivity?.code,
                            el4 = el4,// employeeProjectActivity?.hRDoner?.code,
                            cur = "BDT",
                            debit = gross,
                            // credit = 0,
                            linedescription = grossstring




                        });

                    }
                    if (pos.salaryHead.salaryHeadName != "Gross Salary")
                    {
                        universalCodaXLTempleteViewModels.Add(new UniversalCodaXLTempleteViewModel
                        {

                            date = DateTime.Now.ToString("dd-MM-yyyy"),
                            year = pos?.salaryPeriod?.salaryYear?.yearName,
                            salaryPeriodId = pos?.salaryPeriodId,
                            documentDescription = "Salary " + pos?.salaryPeriod?.monthName + "-" + pos?.salaryPeriod?.salaryYear?.yearName + "- " + pos?.employeeInfo?.nameEnglish,
                            cc = "BD",
                            doc = "PAYF",
                            docnum = slno.ToString(),
                            line = line.ToString(),
                            //el1=pos?.salaryHead?.salaryHeadCode,
                            //el2= employeeProjectActivity?.hRProject?.code,
                            //el3= employeeProjectActivity?.hRActivity?.code,
                            //el4=employeeProjectActivity?.hRDoner?.code,
                            el1 = el1, //pos?.salaryHead?.salaryHeadCode,
                            el2 = fnCode, //el2, //employeeProjectActivity?.hRProject?.code,
                            el3 = "XBDT", //el3,// employeeProjectActivity?.hRActivity?.code,
                            el4 = "",// employeeProjectActivity?.hRDoner?.code,
                            cur = "BDT",
                            //  debit = 0,
                            credit = pos.amount,
                            linedescription = grossstring




                        });
                    }

                }



            }
            return universalCodaXLTempleteViewModels;


        }

        public async Task<IEnumerable<TaxableamountViewModel>> TaxableamountViewModels(int employeeInfoId, int taxYearId)
        {
            EmployeeInfo employee = _context.employeeInfos.Where(x => x.Id == employeeInfoId).FirstOrDefault();
            List<TaxableamountViewModel> taxableamountViewModels = new List<TaxableamountViewModel>();
            List<EmployeesSalaryStructure> employeesSalaryStructures = _context.employeesSalaryStructures.Include(x => x.salaryHead).Where(x => x.employeeInfoId == employeeInfoId).ToList();
            List<IncomeTaxSetup> incomeTaxSetups = _context.IncomeTaxSetups.Include(x => x.salaryHead).Where(x => x.taxYearId == taxYearId).ToList();
            string stringdate = "30 Jun 2020";
            if (DateTime.Now.Month >= 7)
            {
                stringdate = "30 Jun " + (DateTime.Now.Year + 1);
            }
            else
            {
                stringdate = "30 Jun " + (DateTime.Now.Year);
            }

            DateTime dateTime = DateTime.Parse(stringdate);
            decimal monthNo = Math.Round((dateTime.Subtract((DateTime)employee?.joiningDateGovtService).Days + 1) / (decimal)(365.25 / 12), 2);
            if (monthNo >= 12)
            {
                monthNo = 12;
            }
            foreach (IncomeTaxSetup data in incomeTaxSetups.OrderBy(x=>x.salaryHead.sortOrder))
            {
                decimal? amount = employeesSalaryStructures.Where(x => x.salaryHeadId == data.salaryHeadId).Select(x => x.amount).FirstOrDefault() * monthNo;
                decimal? basicamount = employeesSalaryStructures.Where(x => x.salaryHead.salaryHeadName == "Basic Salary").Select(x => x.amount).FirstOrDefault() * monthNo;
                decimal? examtionamount = 0;
                if (data.exemption == "No")
                {
                    examtionamount = 0;
                }
                else
                {
                    if (data.exemptionPercent > 0)
                    {
                        decimal? exemtiononpercent = basicamount * data.exemptionPercent / 100;
                        if (data.exemptionAmount > 0)
                        {
                            if (data.exemptionAmount > exemtiononpercent)
                            {
                                examtionamount = exemtiononpercent;
                            }
                            else
                            {
                                examtionamount = data.exemptionAmount;

                            }
                        }
                        else
                        {
                            examtionamount = exemtiononpercent;

                        }

                    }
                    else
                    {
                        examtionamount = data.exemptionAmount;
                    }
                }
                if (data.salaryHead.salaryHeadName.Contains("Bonus"))
                {
                    amount = basicamount * 2 / monthNo;
                    examtionamount = 0;
                }
                if (data.salaryHead.salaryHeadName.Contains("Provident"))
                {

                    amount = _context.employeesSalaryStructures.Where(x=>x.salaryHead.headShortName=="PFOwn").Where(x=>x.employeeInfoId==employeeInfoId)?.FirstOrDefault()?.amount*monthNo;
                    examtionamount = 0;
                }
                if (data.salaryHead.salaryHeadName.Contains("LFA"))
                {
                    examtionamount = amount * data.exemptionPercent / 100;
                    //examtionamount = 0;
                }
                decimal taxableamountf = Math.Round((decimal)amount, 2, MidpointRounding.AwayFromZero) - Math.Round((decimal)examtionamount, 2, MidpointRounding.AwayFromZero);
                if (taxableamountf <= 0)
                {
                    taxableamountf = 0;
                }
                taxableamountViewModels.Add(new TaxableamountViewModel
                {
                    accountName = data.salaryHead.salaryHeadName,
                    exemtedrule = data.exemptionRule,
                    amount = Math.Round((decimal)amount, 2, MidpointRounding.AwayFromZero),
                    exemAmount = Math.Round((decimal)examtionamount, 2, MidpointRounding.AwayFromZero),
                    taxableAmount = taxableamountf, //Math.Round((decimal)amount, 2, MidpointRounding.AwayFromZero) - Math.Round((decimal)examtionamount, 2, MidpointRounding.AwayFromZero),
                    monthNo = monthNo

                });

            }
            return taxableamountViewModels;
        }

        public async Task<IEnumerable<TaxableSlabViewModel>> TaxableslabViewModels(int employeeInfoId, int taxYearId)
        {

            List<TaxableamountViewModel> taxableamountViewModels = new List<TaxableamountViewModel>();
            List<TaxableSlabViewModel> taxableSlabViewModels = new List<TaxableSlabViewModel>();
            List<EmployeesSalaryStructure> employeesSalaryStructures = _context.employeesSalaryStructures.Include(x => x.salaryHead).Where(x => x.employeeInfoId == employeeInfoId).ToList();
            List<IncomeTaxSetup> incomeTaxSetups = _context.IncomeTaxSetups.Include(x => x.salaryHead).Where(x => x.taxYearId == taxYearId).ToList();
            EmployeeInfo employeeInfo = _context.employeeInfos.Where(x => x.Id == employeeInfoId).FirstOrDefault();
            string stringdate = "30 Jun 2020";
            if (DateTime.Now.Month >= 7)
            {
                stringdate = "30 Jun " + (DateTime.Now.Year + 1);
            }
            else
            {
                stringdate = "30 Jun " + (DateTime.Now.Year);
            }

            DateTime dateTime = DateTime.Parse(stringdate);
            decimal monthNo = Math.Round((dateTime.Subtract((DateTime)employeeInfo?.joiningDateGovtService).Days + 1) / (decimal)(365.25 / 12), 2);
            if (monthNo >= 12)
            {
                monthNo = 12;
            }
            foreach (IncomeTaxSetup data in incomeTaxSetups)
            {
                decimal? amount = employeesSalaryStructures.Where(x => x.salaryHeadId == data.salaryHeadId).Select(x => x.amount).FirstOrDefault() * monthNo;
                decimal? basicamount = employeesSalaryStructures.Where(x => x.salaryHead.salaryHeadName == "Basic Salary").Select(x => x.amount).FirstOrDefault() * monthNo;
                decimal? examtionamount = 0;
                if (data.exemption == "No")
                {
                    examtionamount = 0;
                }
                else
                {
                    if (data.exemptionPercent > 0)
                    {
                        decimal? exemtiononpercent = basicamount * data.exemptionPercent / 100;
                        if (data.exemptionAmount > 0)
                        {
                            if (data.exemptionAmount > exemtiononpercent)
                            {
                                examtionamount = exemtiononpercent;
                            }
                            else
                            {
                                examtionamount = data.exemptionAmount;

                            }
                        }
                        else
                        {
                            examtionamount = exemtiononpercent;

                        }
                    }
                    else
                    {
                        examtionamount = data.exemptionAmount;
                    }
                }
                if (data.salaryHead.salaryHeadName.Contains("Bonus"))
                {
                    amount = basicamount * 2 / monthNo;
                    examtionamount = 0;
                }
                if (data.salaryHead.salaryHeadName.Contains("Provident"))
                {
                    //amount = basicamount * 10 / 100;
                    amount = _context.employeesSalaryStructures.Where(x => x.salaryHead.headShortName == "PFOwn").Where(x => x.employeeInfoId == employeeInfoId)?.FirstOrDefault()?.amount*monthNo;
                    examtionamount = 0;
                }
                if (data.salaryHead.salaryHeadName.Contains("LFA"))
                {
                    examtionamount = amount * data.exemptionPercent / 100;
                    // examtionamount = 0;
                }
                decimal taxableamountf = Math.Round((decimal)amount, 2, MidpointRounding.AwayFromZero) - Math.Round((decimal)examtionamount, 2, MidpointRounding.AwayFromZero);
                if (taxableamountf <= 0)
                {
                    taxableamountf = 0;
                }
                taxableamountViewModels.Add(new TaxableamountViewModel
                {
                    accountName = data.salaryHead.salaryHeadName,
                    exemtedrule = data.exemptionRule,
                    amount = Math.Round((decimal)amount, 2, MidpointRounding.AwayFromZero),
                    exemAmount = Math.Round((decimal)examtionamount, 2, MidpointRounding.AwayFromZero),
                    taxableAmount = taxableamountf

                });

            }


            decimal? taxableamount = taxableamountViewModels.Sum(x => x.taxableAmount);
            SlabIncomeTaxAssign slabIncomeTaxAssign = _context.SlabIncomeTaxAssigns.Include(x => x.slabType).Where(x => x.employeeInfoId == employeeInfo.Id).LastOrDefault();
            if (slabIncomeTaxAssign != null)
            {
                employeeInfo.gender = slabIncomeTaxAssign.slabType.slabTypeName;
            }
            List<SlabIncomeTax> slabIncomeTaxes = _context.SlabIncomeTaxes.Include(x => x.slabType).Where(x => x.slabType.slabTypeName.ToLower() == employeeInfo.gender.ToLower() && x.taxYearId == taxYearId).ToList();
            foreach (SlabIncomeTax tx in slabIncomeTaxes.Where(x => x.slabType.slabTypeName == employeeInfo.gender).OrderBy(x => x.sortOrder))
            {
                decimal? slabamountc = 0;
                decimal? taxableamountcurrent = 0;
                if (taxableamount > 0)
                {
                    if (tx.slabAmount <= taxableamount && tx.slabAmount != 0)
                    {
                        slabamountc = tx.slabAmount;
                        taxableamountcurrent = tx.slabAmount * tx.taxRate / 100;

                        taxableSlabViewModels.Add(new TaxableSlabViewModel
                        {

                            calculationOfInvestment = tx.slabText,
                            rate = tx.taxRate,
                            slabamount = slabamountc,
                            taxAmount = taxableamountcurrent

                        });
                        taxableamount = taxableamount - tx.slabAmount;
                    }
                    else if (tx.slabAmount <= taxableamount && tx.slabAmount == 0)
                    {
                        slabamountc = taxableamount;
                        taxableamountcurrent = taxableamount * tx.taxRate / 100;

                        taxableSlabViewModels.Add(new TaxableSlabViewModel
                        {

                            calculationOfInvestment = tx.slabText,
                            rate = tx.taxRate,
                            slabamount = slabamountc,
                            taxAmount = taxableamountcurrent

                        });
                        taxableamount = 0;
                    }
                    else
                    {
                        slabamountc = taxableamount;
                        taxableamountcurrent = taxableamount * tx.taxRate / 100;

                        taxableSlabViewModels.Add(new TaxableSlabViewModel
                        {

                            calculationOfInvestment = tx.slabText,
                            rate = tx.taxRate,
                            slabamount = slabamountc,
                            taxAmount = taxableamountcurrent

                        });
                        taxableamount = 0;
                    }

                }


            }
            return taxableSlabViewModels;
        }
        public async Task<IEnumerable<TaxablePFViewModel>> TaxablePFViewModels(int employeeInfoId, int taxYearId)
        {

            List<TaxablePFViewModel> TaxablePFViewModels = new List<TaxablePFViewModel>();
            List<EmployeesSalaryStructure> employeesSalaryStructures = _context.employeesSalaryStructures.Include(x => x.salaryHead).Where(x => x.employeeInfoId == employeeInfoId).ToList();
            List<IncomeTaxSetup> incomeTaxSetups = _context.IncomeTaxSetups.Include(x => x.salaryHead).Where(x => x.taxYearId == taxYearId).ToList();
            EmployeeInfo employeeInfo = _context.employeeInfos.Where(x => x.Id == employeeInfoId).FirstOrDefault();

            TaxablePFViewModels.Add(new TaxablePFViewModel
            {

                accountName = "Provident Fund - Employer",
                investmentAmount = employeesSalaryStructures.Where(x => x.salaryHead.headShortName == "PFOwn").Select(x => x.amount).FirstOrDefault() * 12

            });
            TaxablePFViewModels.Add(new TaxablePFViewModel
            {

                accountName = "Provident Fund - Own",
                investmentAmount = employeesSalaryStructures.Where(x => x.salaryHead.headShortName == "PFOwn").Select(x => x.amount).FirstOrDefault() * 12

            });

            return TaxablePFViewModels;
        }
        public async Task<IEnumerable<EmployeesTax>> TaxCalculateforall(int taxYearId)
        {

            List<EmployeesSalaryStructure> employeesSalaryStructures = _context.employeesSalaryStructures.Include(x => x.salaryHead).ToList();
            List<IncomeTaxSetup> incomeTaxSetups = _context.IncomeTaxSetups.Include(x => x.salaryHead).Where(x => x.taxYearId == taxYearId).ToList();
            IEnumerable<EmployeeInfo> lstemployeeInfo = _context.employeeInfos.AsNoTracking().ToList();
            IEnumerable<SlabIncomeTaxAssign> slabIncomeTaxAssigns = _context.SlabIncomeTaxAssigns.Include(x => x.slabType).ToList();
            foreach (EmployeeInfo employeeInfo in lstemployeeInfo.Where(x => x.joiningDateGovtService != null))
            {
                string stringdate = "30 Jun 2020";
                if (DateTime.Now.Month >= 7)
                {
                    stringdate = "30 Jun " + (DateTime.Now.Year + 1);
                }
                else
                {
                    stringdate = "30 Jun " + (DateTime.Now.Year);
                }

                DateTime dateTime = DateTime.Parse(stringdate);
                decimal monthNo = Math.Round((dateTime.Subtract((DateTime)employeeInfo?.joiningDateGovtService).Days + 1) / (decimal)(365.25 / 12), 2);
                if (monthNo >= 12)
                {
                    monthNo = 12;
                }
                await UpdateEmployeesStatus(employeeInfo.Id, taxYearId);
                // _context.employeesTaxes.Remove(_context.employeesTaxes.Find(id));
                List<TaxableamountViewModel> taxableamountViewModels = new List<TaxableamountViewModel>();
                List<TaxableSlabViewModel> taxableSlabViewModels = new List<TaxableSlabViewModel>();
                List<EmployeesSalaryStructure> employeesSalaryStructuresf = employeesSalaryStructures.Where(x => x.employeeInfoId == employeeInfo.Id).ToList();
                foreach (IncomeTaxSetup data in incomeTaxSetups)
                {
                    decimal? amount = employeesSalaryStructuresf.Where(x => x.salaryHeadId == data.salaryHeadId).Select(x => x.amount).FirstOrDefault() * monthNo;
                    decimal? basicamount = employeesSalaryStructuresf.Where(x => x.salaryHead.salaryHeadName == "Basic Salary").Select(x => x.amount).FirstOrDefault() * monthNo;
                    decimal? examtionamount = 0;
                    if (data.exemption == "No")
                    {
                        examtionamount = 0;
                    }
                    else
                    {
                        if (data.exemptionPercent > 0)
                        {
                            decimal? exemtiononpercent = basicamount * data.exemptionPercent / 100;
                            if (data.exemptionAmount > 0)
                            {
                                if (data.exemptionAmount > exemtiononpercent)
                                {
                                    examtionamount = exemtiononpercent;
                                }
                                else
                                {
                                    examtionamount = data.exemptionAmount;

                                }
                            }
                            else
                            {
                                examtionamount = exemtiononpercent;

                            }
                        }
                        else
                        {
                            examtionamount = data.exemptionAmount;
                        }
                    }
                    if (data.salaryHead.salaryHeadName.Contains("Bonus"))
                    {
                        amount = basicamount * 2 / monthNo;
                        examtionamount = 0;
                    }
                    if (data.salaryHead.salaryHeadName.Contains("Provident"))
                    {
                        //amount = basicamount * 10 / 100;
                        amount = _context.employeesSalaryStructures.Where(x => x.salaryHead.headShortName == "PFOwn").Where(x => x.employeeInfoId == employeeInfo.Id)?.FirstOrDefault()?.amount* monthNo;
                        examtionamount = 0;
                    }
                    if (data.salaryHead.salaryHeadName.Contains("LFA"))
                    {
                        examtionamount = amount * data.exemptionPercent / 100;
                        // examtionamount = 0;
                    }
                    decimal taxableamountf = Math.Round((decimal)amount, 2, MidpointRounding.AwayFromZero) - Math.Round((decimal)examtionamount, 2, MidpointRounding.AwayFromZero);
                    if (taxableamountf <= 0)
                    {
                        taxableamountf = 0;
                    }
                    taxableamountViewModels.Add(new TaxableamountViewModel
                    {
                        accountName = data.salaryHead.salaryHeadName,
                        exemtedrule = data.exemptionRule,
                        amount = Math.Round((decimal)amount, 2, MidpointRounding.AwayFromZero),
                        exemAmount = Math.Round((decimal)examtionamount, 2, MidpointRounding.AwayFromZero),
                        taxableAmount = taxableamountf //Math.Round((decimal)amount, 2, MidpointRounding.AwayFromZero) - Math.Round((decimal)examtionamount, 2, MidpointRounding.AwayFromZero)



                    });

                }
                if (employeeInfo.gender == null)
                {
                    employeeInfo.gender = "Male";
                }
                SlabIncomeTaxAssign slabIncomeTaxAssign = slabIncomeTaxAssigns.Where(x => x.employeeInfoId == employeeInfo.Id).LastOrDefault();
                if (slabIncomeTaxAssign != null)
                {
                    employeeInfo.gender = slabIncomeTaxAssign.slabType.slabTypeName;
                }
                List<SlabIncomeTax> slabIncomeTaxes = _context.SlabIncomeTaxes.Include(x => x.slabType).Where(x => x.slabType.slabTypeName.ToLower() == employeeInfo.gender.ToLower() && x.taxYearId == taxYearId).ToList();
                decimal? taxableamount = taxableamountViewModels.Sum(x => x.taxableAmount);
                foreach (SlabIncomeTax tx in slabIncomeTaxes.OrderBy(x => x.sortOrder))
                {
                    decimal? slabamountc = 0;
                    decimal? taxableamountcurrent = 0;
                    if (taxableamount > 0)
                    {
                        if (tx.slabAmount <= taxableamount &&tx.slabAmount!=0)
                        {
                            slabamountc = tx.slabAmount;
                            taxableamountcurrent = tx.slabAmount * tx.taxRate / 100;

                            taxableSlabViewModels.Add(new TaxableSlabViewModel
                            {

                                calculationOfInvestment = tx.slabText,
                                rate = tx.taxRate,
                                slabamount = slabamountc,
                                taxAmount = taxableamountcurrent

                            });
                            taxableamount = taxableamount - tx.slabAmount;
                        }
                        else if (tx.slabAmount <= taxableamount && tx.slabAmount == 0)
                        {
                            slabamountc = taxableamount;
                            taxableamountcurrent = taxableamount * tx.taxRate / 100;

                            taxableSlabViewModels.Add(new TaxableSlabViewModel
                            {

                                calculationOfInvestment = tx.slabText,
                                rate = tx.taxRate,
                                slabamount = slabamountc,
                                taxAmount = taxableamountcurrent

                            });
                            taxableamount = 0;
                        }
                        else
                        {
                            slabamountc = taxableamount;
                            taxableamountcurrent = taxableamount * tx.taxRate / 100;

                            taxableSlabViewModels.Add(new TaxableSlabViewModel
                            {

                                calculationOfInvestment = tx.slabText,
                                rate = tx.taxRate,
                                slabamount = slabamountc,
                                taxAmount = taxableamountcurrent

                            });
                            taxableamount = 0;
                        }

                    }


                }
                decimal? PFamount = employeesSalaryStructuresf.Where(x => x.salaryHead.salaryHeadName == "Basic Salary").Select(x => x.amount).FirstOrDefault() * monthNo * 10 / 100;
                var investment = _context.InvestmentRebateSettings.Where(x => x.taxYearId == taxYearId);

                decimal? investmetrebatepercent = 0;
                decimal? orinvestmetrebatepercent = 0;
                int i = 0;
                foreach (var invrebate in investment)
                {

                    if (i == 0)
                    {
                        if (invrebate.orInvestmentRebate >= taxableamountViewModels.Sum(x => x.taxableAmount))
                        {
                            investmetrebatepercent = invrebate.investmentRebate;
                            orinvestmetrebatepercent = invrebate.orInvestmentRebate;
                            i = 1;
                        }
                    }

                }

                if (i == 0)
                {
                    investmetrebatepercent = investment.LastOrDefault().investmentRebate;
                    orinvestmetrebatepercent = investment.LastOrDefault().orInvestmentRebate;
                }


                decimal? reabtabletax = taxableSlabViewModels.Sum(x => x.taxAmount) - ((taxableamountViewModels.Sum(x => x.taxableAmount)) * investment.FirstOrDefault().allowableInvestment / 100) * investmetrebatepercent / 100;
                if (reabtabletax <= 5000)
                {
                    if (reabtabletax <= 0)
                    {
                        reabtabletax = 0;
                    }
                    else
                    {
                        reabtabletax = 5000;
                    }

                }
                decimal? taxpermonth = reabtabletax / monthNo;
                var taxableamounts = taxableamountViewModels.Sum(x => x.taxableAmount);
                if (taxableamounts > 0 && taxpermonth > 0)
                {

                    EmployeesTax employeesTax = new EmployeesTax
                    {
                        employeeInfoId = employeeInfo.Id,
                        taxYearId = taxYearId,
                        amount = taxpermonth,
                        yearlyTaxableincome = taxableamounts,
                        yearlyTaxableamount = reabtabletax,
                        isActive = 1

                    };
                    await SaveEmployeesTax(employeesTax);
                }


            }
            var emplyeetaxes = _context.employeesTaxes.Include(x => x.employeeInfo).Include(x => x.taxYear).ToList();
            return emplyeetaxes;
        }
        #endregion

        #region Tax

        public async Task<bool> UpdateTaxInSalary(int PeriodId, int employeeId, int Id)
        {
            var data = _context.employeesTaxes.Find(Id);
            var SalaryPeriod = _context.ProcessEmpSalaryStructures.Where(x => x.employeeInfoId == employeeId && x.salaryPeriodId == PeriodId && x.salaryHead.salaryHeadName == "Income Tax").FirstOrDefault();
            if (SalaryPeriod != null)
            {
                SalaryPeriod.amount = (decimal)data.amount;
                _context.Entry(SalaryPeriod).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return true;


        }
        public async Task<bool> UpdateTaxInstruc(int employeeId, int Id)
        {
            var data = _context.employeesTaxes.Find(Id);
            var SalaryPeriod = _context.employeesSalaryStructures.Where(x => x.employeeInfoId == employeeId && x.salaryHead.salaryHeadName == "Income Tax").OrderByDescending(c => c.Id).FirstOrDefault();
            if (SalaryPeriod != null)
            {
                SalaryPeriod.amount = (decimal)data.amount;
                _context.Entry(SalaryPeriod).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            return true;


        }

        public async Task<bool> UpdateTaxInSalaryall(int PeriodId)
        {
            var salaryperiod = _context.salaryPeriods.Find(PeriodId);

            var data = _context.employeesTaxes.Where(x => x.taxYearId == salaryperiod.taxYearId && x.isActive == 1).ToList();
            foreach (var x in data)
            {
                var salstruc = _context.ProcessEmpSalaryStructures.Where(y => y.employeeInfoId == x.employeeInfoId && y.salaryPeriodId == PeriodId && y.salaryHead.salaryHeadName == "Income Tax").FirstOrDefault();
                if (salstruc != null)
                {
                    salstruc.amount = (decimal)x?.amount;
                    _context.Entry(salstruc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

            }


            return true;

        }
        public async Task<bool> UpdateTaxInSalaryallstruc()
        {
            var salaryperiod = _context.taxYears.OrderByDescending(x => x.Id).FirstOrDefault();

            var data = _context.employeesTaxes.Where(x => x.taxYearId == salaryperiod.Id && x.isActive == 1).ToList();
            foreach (var x in data)
            {
                var salstruc = _context.employeesSalaryStructures.Where(y => y.employeeInfoId == x.employeeInfoId && y.salaryHead.salaryHeadName == "Income Tax").OrderByDescending(y => y.Id).FirstOrDefault();
                if (salstruc != null)
                {
                    salstruc.amount = (decimal)x.amount;
                    _context.Entry(salstruc).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }

            }


            return true;

        }

        #endregion 

        #region Advance Payment

        public async Task<bool> SaveAdvancePayment(AdvancePayment advancePayment)
        {
            if (advancePayment.Id != 0)
                _context.AdvancePayments.Update(advancePayment);
            else
                _context.AdvancePayments.Add(advancePayment);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AdvancePayment>> GetAllAdvancePayment()
        {
            return await _context.AdvancePayments.Include(x => x.advanceAdjustment).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AdvancePayment>> GetAdvancePaymentBysalaryPeriodId(int salaryPeriodId)
        {
            return await _context.AdvancePayments.Include(x => x.advanceAdjustment).Where(x => x.salaryPeriodId == salaryPeriodId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AdvancePayment>> GetAdvancePaymentByadvanceAdjustmentId(int advanceAdjustmentId)
        {
            return await _context.AdvancePayments.Include(x => x.advanceAdjustment).Where(x => x.advanceAdjustmentId == advanceAdjustmentId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AdvancePayment>> GetAdvancePaymentByemployeeInfoId(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.AdvancePayments.Include(x => x.advanceAdjustment).Where(x => x.salaryPeriodId == salaryPeriodId && x.advanceAdjustment.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }
        public async Task<AdvancePayment> GetAdvancePaymentById(int id)
        {
            return await _context.AdvancePayments.FindAsync(id);
        }

        public async Task<bool> DeleteAdvancePaymentBysalaryPeriodId(int salaryPeriodId)
        {
            _context.AdvancePayments.RemoveRange(_context.AdvancePayments.Where(x => x.salaryPeriodId == salaryPeriodId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAdvancePaymentByemployeeInfoId(int employeeInfoId, int salaryPeriodId)
        {
            _context.AdvancePayments.RemoveRange(_context.AdvancePayments.Where(x => x.advanceAdjustment.employeeInfoId == employeeInfoId && x.salaryPeriodId == salaryPeriodId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region reportformat
        public async Task<bool> SaveReportFormat(ReportFormat reportFormat)
        {
            if (reportFormat.Id != 0)
                _context.ReportFormats.Update(reportFormat);
            else
                _context.ReportFormats.Add(reportFormat);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReportFormat>> GetReportFormat()
        {
            return await _context.ReportFormats.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ReportFormat>> GetReportFormatByReportType(string reportType)
        {
            return await _context.ReportFormats.Where(a => a.reportTypeName == reportType).ToListAsync();
        }

        public async Task<bool> DeleteformatById(string reportType)
        {
            _context.ReportFormats.Remove(_context.ReportFormats.Where(x => x.reportTypeName == reportType).FirstOrDefault());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region salaryActivityPerchect
        public async Task<bool> SavesalaryActivityPercent(SalaryActivityPercent salaryActivityPercent)
        {
            if (salaryActivityPercent.Id != 0)
                _context.salaryActivityPercents.Update(salaryActivityPercent);
            else
                _context.salaryActivityPercents.Add(salaryActivityPercent);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalaryActivityPercent>> GetsalaryActivityPercent()
        {
            return await _context.salaryActivityPercents.Include(x => x.employeeInfo).Include(x => x.employeeProjectActivity.hRDoner).Include(x => x.employeeProjectActivity.hRProject).Include(x => x.employeeProjectActivity.hRActivity).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalaryActivityPercent>> GetsalaryActivityPercentByEmpId(int empId)
        {


            return await _context.salaryActivityPercents.Where(a => a.employeeInfoId == empId).ToListAsync();
        }

        public async Task<bool> DeletesalaryActivityPercentById(int empId)
        {
            _context.salaryActivityPercents.Remove(_context.salaryActivityPercents.Where(x => x.Id == empId).FirstOrDefault());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region salaryActivityPerchect
        public async Task<bool> SaveCodeManagement(CodeManagement codeManagement)
        {
            if (codeManagement.Id != 0)
                _context.CodeManagements.Update(codeManagement);
            else
                _context.CodeManagements.Add(codeManagement);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CodeManagement>> GetCodeManagement()
        {
            return await _context.CodeManagements.Include(x => x.employeeInfo).Include(x => x.salaryHead).Include(x => x.salaryPeriod).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CodeManagement>> GetCodeManagementByEmpPeriodId(int empId, int PeriodId)
        {


            return await _context.CodeManagements.Where(a => a.employeeInfoId == empId && a.salaryPeriodId == PeriodId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<CodeManagement>> GetCodeManagementByPeriodId(int PeriodId)
        {


            return await _context.CodeManagements.Where(a => a.salaryPeriodId == PeriodId).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteCodeManagementsByEmpPeriodId(int empId, int PeriodId)
        {
            _context.CodeManagements.RemoveRange(_context.CodeManagements.Where(x => x.Id == empId && x.salaryPeriodId == PeriodId).AsNoTracking().ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteCodeManagementsByPeriodId(int PeriodId)
        {
            _context.CodeManagements.RemoveRange(_context.CodeManagements.Where(x => x.salaryPeriodId == PeriodId).AsNoTracking().ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Monthly Allowance

        public async Task<bool> SaveMonthlyAllowance(MonthlyAllowance monthlyAllowance)
        {
            if (monthlyAllowance.Id != 0)
                _context.MonthlyAllowances.Update(monthlyAllowance);
            else
                _context.MonthlyAllowances.Add(monthlyAllowance);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MonthlyAllowance>> GetAllMonthlyAllowance()
        {
            return await _context.MonthlyAllowances.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<MonthlyAllowance>> GetMonthlyAllowanceBysalaryPeriodId(int salaryPeriodId)
        {
            return await _context.MonthlyAllowances.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.salaryPeriodId <= salaryPeriodId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<MonthlyAllowance>> GetMonthlyAllowanceByemployeeInfoId(int salaryPeriodId, int employeeInfoId)
        {
            return await _context.MonthlyAllowances.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.salaryPeriodId <= salaryPeriodId && x.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }
        public async Task<MonthlyAllowance> GetMonthlyAllowanceById(int id)
        {
            return await _context.MonthlyAllowances.FindAsync(id);
        }

        public async Task<bool> DeleteMonthlyAllowanceById(int id)
        {
            _context.MonthlyAllowances.Remove(_context.MonthlyAllowances.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<MealChargeViewModel>> GetMealChargeByPeriod(int salaryPeriodId)
        {
            return await _context.mealChargeViewModels.FromSql($"SP_GETMonthlyMealCharge {salaryPeriodId}").AsNoTracking().ToListAsync();
        }
        #endregion

        #region Attendance Summary

        public async Task<int> SaveAttendanceSummary(EmpAttendanceSummary empAttendanceSummary)
        {
            if (empAttendanceSummary.Id != 0)
                _context.EmpAttendanceSummaries.Update(empAttendanceSummary);
            else
                _context.EmpAttendanceSummaries.Add(empAttendanceSummary);
            await _context.SaveChangesAsync();
            return empAttendanceSummary.Id;
        }

        public async Task<IEnumerable<EmpAttendanceSummary>> GetAttendanceSummary()
        {
            return await _context.EmpAttendanceSummaries.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmpAttendanceSummary>> GetAttendanceSummaryBysalaryPeriodId(int salaryPeriodId)
        {
            return await _context.EmpAttendanceSummaries.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.salaryPeriodId <= salaryPeriodId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EmpAttendanceSummary>> GetDuplicateAttendanceSummary(int id, int salaryPeriodId, int employeeInfoId)
        {
            return await _context.EmpAttendanceSummaries.Include(x => x.employeeInfo).Include(x => x.salaryPeriod).Where(x => x.Id != id && x.salaryPeriodId == salaryPeriodId && x.employeeInfoId == employeeInfoId).AsNoTracking().ToListAsync();
        }

        public async Task<EmpAttendanceSummary> GetAttendanceSummaryById(int id)
        {
            return await _context.EmpAttendanceSummaries.FindAsync(id);
        }

        public async Task<IEnumerable<Areas.HRPMSAttendence.Models.AttendanceSummaryViewModel>> GetAttendanceSummaryByPeriod(int? id, int? salaryPeriodId)
        {
            return await _context.attendanceSummaryViewModels.FromSql($"SP_GETAttendanceSummary {id},{salaryPeriodId}").AsNoTracking().ToListAsync();
        }        

        public async Task<bool> DeleteAttendanceSummaryById(int id)
        {
            _context.EmpAttendanceSummaries.Remove(_context.EmpAttendanceSummaries.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

    }
}