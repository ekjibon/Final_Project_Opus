using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.IncomeTax.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;

namespace OPUSERP.Payroll.Services.IncomeTax
{
    public class IncomeTaxSlabService : IIncomeTaxSlabService
    {
        private readonly ERPDbContext _context;

        public IncomeTaxSlabService(ERPDbContext context)
        {
            _context = context;
        }



 

        #region Tax Slab
        public async Task<int> SaveIncomeTaxSlab(SlabIncomeTax slabIncomeTax)
        {
            if (slabIncomeTax.Id != 0)
            {
                _context.SlabIncomeTaxes.Update(slabIncomeTax);
            }
            else
            {
                _context.SlabIncomeTaxes.Add(slabIncomeTax);
            }
           await _context.SaveChangesAsync();
            return slabIncomeTax.Id;
        }

        public async Task<IEnumerable<SlabIncomeTax>> GetSlabIncomeTax()
        {
            return await _context.SlabIncomeTaxes.Include(x=>x.taxYear).Include(x=>x.slabType).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<SlabType>> GetSlabType()
        {
            return await _context.SlabTypes.AsNoTracking().ToListAsync();
        }
        #endregion
        #region Rebate Slab
        public async Task<int> SaveRebateSlab(SlabRebate slabRebate)
        {
            if (slabRebate.Id != 0)
            {
                _context.SlabRebates.Update(slabRebate);
            }
            else
            {
                _context.SlabRebates.Add(slabRebate);
            }
            await _context.SaveChangesAsync();
            return slabRebate.Id;
        }

        public async Task<IEnumerable<SlabRebate>> GetSlabRebate()
        {
            return await _context.SlabRebates.Include(x => x.taxYear).Include(x => x.slabType).AsNoTracking().ToListAsync();
        }

        #endregion
        #region Tax Setup
        public async Task<int> SaveTaxSetup(IncomeTaxSetup incomeTaxSetup)
        {
            if (incomeTaxSetup.Id != 0)
            {
                _context.IncomeTaxSetups.Update(incomeTaxSetup);
            }
            else
            {
                _context.IncomeTaxSetups.Add(incomeTaxSetup);
            }
            await _context.SaveChangesAsync();
            return incomeTaxSetup.Id;
        }

        public async Task<IEnumerable<IncomeTaxSetup>> GetTaxSetup()
        {
            return await _context.IncomeTaxSetups.Include(x => x.taxYear).Include(x => x.salaryHead).AsNoTracking().ToListAsync();
        }

        #endregion
        #region Tax Challan
        public async Task<int> SaveTaxChallan(TaxChallan taxChallan )
        {
            if (taxChallan.Id != 0)
            {
                _context.TaxChallans.Update(taxChallan);
            }
            else
            {
                _context.TaxChallans.Add(taxChallan);
            }
            await _context.SaveChangesAsync();
            return taxChallan.Id;
        }

        public async Task<IEnumerable<TaxChallan>> GetTaxChallan()
        {
            return await _context.TaxChallans.Include(x => x.salaryPeriod.taxYear).AsNoTracking().ToListAsync();
        }

        #endregion
        #region Additional Tax Info
        public async Task<int> SaveAdditionalTax(AdditionalTaxInfo additionalTaxInfo)
        {
            if (additionalTaxInfo.Id != 0)
            {
                _context.AdditionalTaxInfos.Update(additionalTaxInfo);
            }
            else
            {
                _context.AdditionalTaxInfos.Add(additionalTaxInfo);
            }
            await _context.SaveChangesAsync();
            return additionalTaxInfo.Id;
        }

        public async Task<IEnumerable<AdditionalTaxInfo>> GetAdditionalTaxInfo()
        {
            return await _context.AdditionalTaxInfos.Include(x => x.taxYear).Include(x=>x.salaryHead).Include(x=>x.employeeInfo).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AdditionalTaxInfo>> GetAdditionalTaxInfobyEmpId(int id)
        {
            return await _context.AdditionalTaxInfos.Where(x=>x.employeeInfoId==id).Include(x => x.taxYear).Include(x => x.salaryHead).Include(x => x.employeeInfo).AsNoTracking().ToListAsync();
        }

        #endregion
        #region process
        public  Task<TaxProcessViewModel> taxprocess(int? periodId, int? empId)
        {
            int taxyearid = _context.salaryPeriods.Include(x => x.taxYear).Where(x => x.Id == periodId).Select(x=>x.taxYearId).FirstOrDefault();
            var result = _context.taxProcessViewModels.FromSql($"spProcessEmpIncomeTax {periodId}, {taxyearid}, {empId}").FirstOrDefaultAsync();
            return result;
        }
        public async Task<bool> taxProcessNew(int? periodId, int? empId)
        {
           
            List<EmployeeInfo> employeeInfos = _context.employeeInfos.ToList();
            List<int> deductsalaryId = _context.salaryHeads.Where(x => x.isInvestments == "Yes" && x.salaryHeadType == "Deduction").Select(x => x.Id).ToList();
            var perioddata = _context.salaryPeriods.Include(x => x.taxYear).Where(x => x.Id == periodId).FirstOrDefault();
            decimal taxlimit = (decimal)perioddata.taxYear.taxLimit;
            List<ProcessEmpSalaryStructure> listProcessEmpSalaryStructure = _context.ProcessEmpSalaryStructures.Include(x=>x.salaryHead).Where(x => x.salaryPeriod.taxYearId == perioddata.taxYearId).ToList();

            List<EmployeesSalaryStructure> employeesSalaryStructures = _context.employeesSalaryStructures.Include(x=>x.salaryHead).ToList();
            List<EmployeeArrear> lstArrear = _context.EmployeeArrears.Where(x=>x.salaryPeriod.taxYearId==perioddata.taxYearId).ToList();
            List<AdditionalTaxInfo> additionalTaxInfos = _context.AdditionalTaxInfos.Where(x => x.taxYearId == perioddata.taxYearId).ToList();
            List<IncomeTaxSetup> incomeTaxSetups = _context.IncomeTaxSetups.Where(x => x.taxYearId == perioddata.taxYearId).ToList();

            InvestmentRebateSettings investmentRebateSettings = _context.InvestmentRebateSettings.Where(x => x.taxYearId == perioddata.taxYearId).FirstOrDefault();
            List<SlabIncomeTax> slabIncomeTaxes = _context.SlabIncomeTaxes.Include(x=>x.slabType).Where(x => x.taxYearId == perioddata.taxYearId).ToList();

           decimal MonthNo;
           
            if (perioddata.monthName == "July")
            {
                MonthNo = 12;
            }
            else if (perioddata.monthName == "August")
            {
                MonthNo = 11;
            }
            else if (perioddata.monthName == "September")
            {
                MonthNo = 10;
            }
            else if (perioddata.monthName == "October")
            {
                MonthNo = 9;
            }
            else if (perioddata.monthName == "November")
            {
                MonthNo = 8;
            }
            else if (perioddata.monthName == "December")
            {
                MonthNo = 7;
            }
            else if (perioddata.monthName == "January")
            {
                MonthNo = 6;
            }
            else if (perioddata.monthName == "February")
            {
                MonthNo = 5;
            }
            else if (perioddata.monthName == "March")
            {
                MonthNo = 4;
            }
            else if (perioddata.monthName == "April")
            {
                MonthNo = 3;
            }
            else if (perioddata.monthName == "May")
            {
                MonthNo = 2;
            }
            else if (perioddata.monthName == "June")
            {
                MonthNo = 1;
            }
            else
            {
                MonthNo = 1;
            }
            List<int> lstprocid = listProcessEmpSalaryStructure.Where(x => x.salaryPeriodId == periodId).Select(x => x.employeeInfoId).ToList();
            List<int> lstempId = employeesSalaryStructures.Where(x => x.salaryHead.isIncomeTax == "Yes"&&lstprocid.Contains(x.employeeInfoId)).Select(x => x.employeeInfoId).Distinct().ToList();
            if (empId >0)
            {
                string gender = employeeInfos.Where(x => x.Id == empId).Select(x => x.gender).FirstOrDefault();

                decimal PayableAmount = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && deductsalaryId.Contains(x.salaryHeadId)).Sum(x => x.amount);
                var salpfstructure = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Provident Fund - Own").Take(1).OrderByDescending(x => x.Id);
                decimal PFAmount = salpfstructure.Sum(x => x.amount) * MonthNo;

                decimal thisYearAdjustment = additionalTaxInfos.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "This Year Adjustment").Sum(x => x.exemptionAmount);
                decimal ABasic = lstArrear.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Basic").Sum(x => x.arrearAmount);
                decimal AHR = lstArrear.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "House Rent").Sum(x => x.arrearAmount);
                decimal ACon = lstArrear.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Conveyance").Sum(x => x.arrearAmount);
                decimal AMed = lstArrear.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Medical").Sum(x => x.arrearAmount);
                decimal SBasic = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Basic").Sum(x => x.amount);
                decimal SHR = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "House Rent").Sum(x => x.amount);
                decimal SCon = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Conveyance").Sum(x => x.amount);
                decimal SMed = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Medical").Sum(x => x.amount);
                List<EmployeesSalaryStructure> lststruc = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.isIncomeTax == "Yes" && x.salaryHead.salaryHeadType == "Addition").ToList();


                decimal Amount = 0;
                decimal exemption = 0;
                foreach (EmployeesSalaryStructure emps in lststruc)
                {

                    if (emps.salaryHead.salaryHeadName == "Basic")
                    {
                        decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + SBasic * MonthNo + ABasic - thisYearAdjustment * 50 / 100;
                        IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                        if (incomeTaxSetup?.exemption == "Yes")
                        {
                            if (incomeTaxSetup?.exemptionPercent == 100)
                            {
                                Amount += 0;

                            }
                            else if (incomeTaxSetup?.exemptionPercent == 0)
                            {
                                exemption = incomeTaxSetup.exemptionAmount;
                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                            else
                            {
                                if (AMT * incomeTaxSetup?.exemptionPercent > incomeTaxSetup?.exemptionAmount)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                }
                                else
                                {
                                    exemption = AMT * incomeTaxSetup.exemptionPercent;
                                }

                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                        }
                        else
                        {
                            Amount += AMT;

                        }
                    }
                    else if (emps.salaryHead.salaryHeadName == "House Rent")
                    {
                        decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + SHR * MonthNo + AHR - thisYearAdjustment * 25 / 100;
                        IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                        if (incomeTaxSetup?.exemption == "Yes")
                        {
                            if (incomeTaxSetup?.exemptionPercent == 100)
                            {
                                Amount += 0;

                            }
                            else if (incomeTaxSetup.exemptionPercent == 0)
                            {
                                exemption = incomeTaxSetup.exemptionAmount;
                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                            else
                            {
                                if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                }
                                else
                                {
                                    exemption = AMT * incomeTaxSetup.exemptionPercent;
                                }

                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                        }
                        else
                        {
                            Amount += AMT;

                        }
                    }
                    else if (emps.salaryHead.salaryHeadName == "Conveyance")
                    {
                        decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + SCon * MonthNo + ACon - thisYearAdjustment * Convert.ToDecimal(7.5) / 100;
                        IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                        if (incomeTaxSetup?.exemption == "Yes")
                        {
                            if (incomeTaxSetup.exemptionPercent == 100)
                            {
                                Amount += 0;

                            }
                            else if (incomeTaxSetup.exemptionPercent == 0)
                            {
                                exemption = incomeTaxSetup.exemptionAmount;
                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                            else
                            {
                                if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                }
                                else
                                {
                                    exemption = AMT * incomeTaxSetup.exemptionPercent;
                                }

                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                        }
                        else
                        {
                            Amount += AMT;

                        }
                    }
                    else if (emps.salaryHead?.salaryHeadName == "Medical")
                    {
                        decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + SMed * MonthNo + AMed - thisYearAdjustment * Convert.ToDecimal(10) / 100;
                        IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                        if (incomeTaxSetup?.exemption == "Yes")
                        {
                            if (incomeTaxSetup.exemptionPercent == 100)
                            {
                                Amount += 0;

                            }
                            else if (incomeTaxSetup.exemptionPercent == 0)
                            {
                                exemption = incomeTaxSetup.exemptionAmount;
                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                            else
                            {
                                if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                }
                                else
                                {
                                    exemption = AMT * incomeTaxSetup.exemptionPercent;
                                }

                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                        }
                        else
                        {
                            Amount += AMT;

                        }
                    }
                    else if (emps.salaryHead.salaryHeadName == "Provident Fund - Employer")
                    {
                        decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + PFAmount;
                        IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                        if (incomeTaxSetup?.exemption == "Yes")
                        {
                            if (incomeTaxSetup.exemptionPercent == 100)
                            {
                                Amount += 0;

                            }
                            else if (incomeTaxSetup.exemptionPercent == 0)
                            {
                                exemption = incomeTaxSetup.exemptionAmount;
                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                            else
                            {
                                if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                }
                                else
                                {
                                    exemption = AMT * incomeTaxSetup.exemptionPercent;
                                }

                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                        }
                        else
                        {
                            Amount += AMT;

                        }
                    }
                    else if (emps.salaryHead.salaryHeadName == "Performance Bonus")
                    {
                        decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + PFAmount;
                        IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                        if (incomeTaxSetup?.exemption == "Yes")
                        {
                            if (incomeTaxSetup.exemptionPercent == 100)
                            {
                                Amount += 0;

                            }
                            else if (incomeTaxSetup.exemptionPercent == 0)
                            {
                                exemption = incomeTaxSetup.exemptionAmount;
                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                            else
                            {
                                if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                }
                                else
                                {
                                    exemption = AMT * incomeTaxSetup.exemptionPercent;
                                }

                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                        }
                        else
                        {
                            Amount += AMT;

                        }
                    }
                    else
                    {
                        decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount);
                        IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                        if (incomeTaxSetup?.exemption == "Yes")
                        {
                            if (incomeTaxSetup.exemptionPercent == 100)
                            {
                                Amount += 0;

                            }
                            else if (incomeTaxSetup.exemptionPercent == 0)
                            {
                                exemption = incomeTaxSetup.exemptionAmount;
                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                            else
                            {
                                if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                }
                                else
                                {
                                    exemption = AMT * incomeTaxSetup.exemptionPercent;
                                }

                                if (AMT > exemption)
                                {
                                    Amount += AMT - exemption;

                                }
                                else
                                {
                                    Amount += 0;
                                }
                            }
                        }
                        else
                        {
                            Amount += AMT;

                        }
                    }
                }

                decimal taxableIncome = Amount * investmentRebateSettings.allowableInvestment / 100;
                decimal rebattax = 0;
                if (Amount <= 1000000)
                {
                    rebattax = taxableIncome * 15 / 100;
                }
                else if (Amount > 1000000 && Amount <= 3000000)
                {


                    rebattax = Convert.ToDecimal(250000 * 15 * 0.01) + (taxableIncome - 250000) * Convert.ToDecimal(12) * Convert.ToDecimal(0.01);

                }
                else
                {
                    decimal restvalue = 0;
                    decimal restvalue2 = 0;
                    if (taxableIncome - 250000 >= 500000)
                    {

                        restvalue = Convert.ToDecimal(500000 * 12 * 0.01);
                    }
                    else
                    {
                        restvalue = (taxableIncome - Convert.ToDecimal(250000)) * Convert.ToDecimal(12) * Convert.ToDecimal(0.01);
                    }
                    if (taxableIncome - 250000 - 500000 > 0)
                    {

                        restvalue2 = (taxableIncome - Convert.ToDecimal(250000) - Convert.ToDecimal(500000)) * Convert.ToDecimal(10) * Convert.ToDecimal(0.01); ;
                    }
                    else
                    {
                        restvalue2 = 0;
                    }

                    rebattax = Convert.ToDecimal(250000 * 15 * 0.01) + (taxableIncome - 250000) * Convert.ToDecimal(12) * Convert.ToDecimal(0.01) + restvalue + restvalue2;

                }

                decimal proamount = Amount;
                decimal totaltax = 0;
                List<SlabIncomeTax> slabIncomes = slabIncomeTaxes.Where(x => x.slabType.slabTypeName == gender).ToList();
                foreach (SlabIncomeTax st in slabIncomes.OrderBy(x => x.sortOrder))
                {
                    if (proamount > 0)
                    {
                        if (proamount > st.slabAmount)
                        {
                            totaltax = totaltax + st.slabAmount * st.taxRate / 100;
                            proamount = proamount - st.slabAmount;

                        }
                        else
                        {
                            totaltax = totaltax + proamount * st.taxRate / 100;
                            proamount = 0;
                        }
                    }


                }

                if (totaltax < taxlimit)
                {
                    totaltax = taxlimit;
                }
                else if (totaltax - rebattax < taxlimit)
                {
                    totaltax = taxlimit;
                }
                else
                {
                    totaltax = totaltax - rebattax;
                }
                decimal OtherPaidTax = thisYearAdjustment + additionalTaxInfos.Where(x => x.employeeInfoId == empId && x.salaryHead?.salaryHeadName == "Vehicle Tax").Sum(x => x.exemptionAmount);
                decimal Totalpaidtax = listProcessEmpSalaryStructure.Where(x => x.salaryPeriodId < periodId && x.salaryHead?.salaryHeadName == "Income Tax").Sum(x => x.amount);
                decimal exixtingtax = 0;
                if (totaltax == 0)
                {
                    exixtingtax = 0;
                }
                else if (OtherPaidTax > totaltax)
                {
                    exixtingtax = Totalpaidtax * -1;

                }
                else
                {
                    exixtingtax = totaltax - Totalpaidtax - OtherPaidTax;
                }
                decimal taxToBePaid = exixtingtax / MonthNo;

                var SalaryStructur = _context.ProcessEmpSalaryStructures.Find(listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHead?.salaryHeadName == "Income Tax" && x.salaryPeriodId == periodId).FirstOrDefault().Id);
                SalaryStructur.amount = taxToBePaid;
                _context.Entry(SalaryStructur).State = EntityState.Modified;

            }
            else
            {

                foreach (int id in lstempId)
                {
                    empId = id;
                    string gender = employeeInfos.Where(x => x.Id == empId).Select(x => x.gender).FirstOrDefault();

                    decimal PayableAmount = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && deductsalaryId.Contains(x.salaryHeadId)).Sum(x => x.amount);
                    var salpfstructure = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Provident Fund - Own").Take(1).OrderByDescending(x => x.Id);
                    decimal PFAmount = salpfstructure.Sum(x => x.amount) * MonthNo;

                    decimal thisYearAdjustment = additionalTaxInfos.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "This Year Adjustment").Sum(x => x.exemptionAmount);
                    decimal ABasic = lstArrear.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Basic").Sum(x => x.arrearAmount);
                    decimal AHR = lstArrear.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "House Rent").Sum(x => x.arrearAmount);
                    decimal ACon = lstArrear.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Conveyance").Sum(x => x.arrearAmount);
                    decimal AMed = lstArrear.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Medical").Sum(x => x.arrearAmount);
                    decimal SBasic = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Basic").Sum(x => x.amount);
                    decimal SHR = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "House Rent").Sum(x => x.amount);
                    decimal SCon = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Conveyance").Sum(x => x.amount);
                    decimal SMed = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.salaryHeadName == "Medical").Sum(x => x.amount);
                    List<EmployeesSalaryStructure> lststruc = employeesSalaryStructures.Where(x => x.employeeInfoId == empId && x.salaryHead.isIncomeTax == "Yes" && x.salaryHead.salaryHeadType == "Addition").ToList();


                    decimal Amount = 0;
                    decimal exemption = 0;
                    foreach (EmployeesSalaryStructure emps in lststruc)
                    {

                        if (emps.salaryHead.salaryHeadName == "Basic")
                        {
                            decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + SBasic * MonthNo + ABasic - thisYearAdjustment * 50 / 100;
                            IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                            if (incomeTaxSetup?.exemption == "Yes")
                            {
                                if (incomeTaxSetup.exemptionPercent == 100)
                                {
                                    Amount += 0;

                                }
                                else if (incomeTaxSetup.exemptionPercent == 0)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                                else
                                {
                                    if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                    {
                                        exemption = incomeTaxSetup.exemptionAmount;
                                    }
                                    else
                                    {
                                        exemption = AMT * incomeTaxSetup.exemptionPercent;
                                    }

                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                            }
                            else
                            {
                                Amount += AMT;

                            }
                        }
                        else if (emps.salaryHead.salaryHeadName == "House Rent")
                        {
                            decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + SHR * MonthNo + AHR - thisYearAdjustment * 25 / 100;
                            IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                            if (incomeTaxSetup?.exemption == "Yes")
                            {
                                if (incomeTaxSetup.exemptionPercent == 100)
                                {
                                    Amount += 0;

                                }
                                else if (incomeTaxSetup.exemptionPercent == 0)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                                else
                                {
                                    if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                    {
                                        exemption = incomeTaxSetup.exemptionAmount;
                                    }
                                    else
                                    {
                                        exemption = AMT * incomeTaxSetup.exemptionPercent;
                                    }

                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                            }
                            else
                            {
                                Amount += AMT;

                            }
                        }
                        else if (emps.salaryHead.salaryHeadName == "Conveyance")
                        {
                            decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + SCon * MonthNo + ACon - thisYearAdjustment * Convert.ToDecimal(7.5) / 100;
                            IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                            if (incomeTaxSetup?.exemption == "Yes")
                            {
                                if (incomeTaxSetup.exemptionPercent == 100)
                                {
                                    Amount += 0;

                                }
                                else if (incomeTaxSetup.exemptionPercent == 0)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                                else
                                {
                                    if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                    {
                                        exemption = incomeTaxSetup.exemptionAmount;
                                    }
                                    else
                                    {
                                        exemption = AMT * incomeTaxSetup.exemptionPercent;
                                    }

                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                            }
                            else
                            {
                                Amount += AMT;

                            }
                        }
                        else if (emps.salaryHead.salaryHeadName == "Medical")
                        {
                            decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + SMed * MonthNo + AMed - thisYearAdjustment * Convert.ToDecimal(10) / 100;
                            IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                            if (incomeTaxSetup?.exemption == "Yes")
                            {
                                if (incomeTaxSetup.exemptionPercent == 100)
                                {
                                    Amount += 0;

                                }
                                else if (incomeTaxSetup.exemptionPercent == 0)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                                else
                                {
                                    if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                    {
                                        exemption = incomeTaxSetup.exemptionAmount;
                                    }
                                    else
                                    {
                                        exemption = AMT * incomeTaxSetup.exemptionPercent;
                                    }

                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                            }
                            else
                            {
                                Amount += AMT;

                            }
                        }
                        else if (emps.salaryHead.salaryHeadName == "Provident Fund - Employer")
                        {
                            decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + PFAmount;
                            IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                            if (incomeTaxSetup?.exemption == "Yes")
                            {
                                if (incomeTaxSetup.exemptionPercent == 100)
                                {
                                    Amount += 0;

                                }
                                else if (incomeTaxSetup.exemptionPercent == 0)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                                else
                                {
                                    if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                    {
                                        exemption = incomeTaxSetup.exemptionAmount;
                                    }
                                    else
                                    {
                                        exemption = AMT * incomeTaxSetup.exemptionPercent;
                                    }

                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                            }
                            else
                            {
                                Amount += AMT;

                            }
                        }
                        else if (emps.salaryHead.salaryHeadName == "Performance Bonus")
                        {
                            decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount) + PFAmount;
                            IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                            if (incomeTaxSetup?.exemption == "Yes")
                            {
                                if (incomeTaxSetup.exemptionPercent == 100)
                                {
                                    Amount += 0;

                                }
                                else if (incomeTaxSetup.exemptionPercent == 0)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                                else
                                {
                                    if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                    {
                                        exemption = incomeTaxSetup.exemptionAmount;
                                    }
                                    else
                                    {
                                        exemption = AMT * incomeTaxSetup.exemptionPercent;
                                    }

                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                            }
                            else
                            {
                                Amount += AMT;

                            }
                        }
                        else
                        {
                            decimal AMT = listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHeadId == emps.salaryHeadId).Sum(x => x.amount);
                            IncomeTaxSetup incomeTaxSetup = incomeTaxSetups.Where(x => x.salaryHeadId == emps.salaryHeadId).FirstOrDefault();
                            if (incomeTaxSetup?.exemption == "Yes")
                            {
                                if (incomeTaxSetup.exemptionPercent == 100)
                                {
                                    Amount += 0;

                                }
                                else if (incomeTaxSetup.exemptionPercent == 0)
                                {
                                    exemption = incomeTaxSetup.exemptionAmount;
                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                                else
                                {
                                    if (AMT * incomeTaxSetup.exemptionPercent > incomeTaxSetup.exemptionAmount)
                                    {
                                        exemption = incomeTaxSetup.exemptionAmount;
                                    }
                                    else
                                    {
                                        exemption = AMT * incomeTaxSetup.exemptionPercent;
                                    }

                                    if (AMT > exemption)
                                    {
                                        Amount += AMT - exemption;

                                    }
                                    else
                                    {
                                        Amount += 0;
                                    }
                                }
                            }
                            else
                            {
                                Amount += AMT;

                            }
                        }
                    }

                    decimal taxableIncome = Amount * investmentRebateSettings.allowableInvestment / 100;
                    decimal rebattax = 0;
                    if (Amount <= 1000000)
                    {
                        rebattax = taxableIncome * 15 / 100;
                    }
                    else if (Amount > 1000000 && Amount <= 3000000)
                    {


                        rebattax = Convert.ToDecimal(250000 * 15 * 0.01) + (taxableIncome - 250000) * Convert.ToDecimal(12) * Convert.ToDecimal(0.01);

                    }
                    else
                    {
                        decimal restvalue = 0;
                        decimal restvalue2 = 0;
                        if (taxableIncome - 250000 >= 500000)
                        {

                            restvalue = Convert.ToDecimal(500000 * 12 * 0.01);
                        }
                        else
                        {
                            restvalue = (taxableIncome - Convert.ToDecimal(250000)) * Convert.ToDecimal(12) * Convert.ToDecimal(0.01);
                        }
                        if (taxableIncome - 250000 - 500000 > 0)
                        {

                            restvalue2 = (taxableIncome - Convert.ToDecimal(250000) - Convert.ToDecimal(500000)) * Convert.ToDecimal(10) * Convert.ToDecimal(0.01); ;
                        }
                        else
                        {
                            restvalue2 = 0;
                        }

                        rebattax = Convert.ToDecimal(250000 * 15 * 0.01) + (taxableIncome - 250000) * Convert.ToDecimal(12) * Convert.ToDecimal(0.01) + restvalue + restvalue2;

                    }

                    decimal proamount = Amount;
                    decimal totaltax = 0;
                    List<SlabIncomeTax> slabIncomes = slabIncomeTaxes.Where(x => x.slabType.slabTypeName == gender).ToList();
                    foreach (SlabIncomeTax st in slabIncomes.OrderBy(x => x.sortOrder))
                    {
                        if (proamount > 0)
                        {
                            if (proamount > st.slabAmount)
                            {
                                totaltax = totaltax + st.slabAmount * st.taxRate / 100;
                                proamount = proamount - st.slabAmount;

                            }
                            else
                            {
                                totaltax = totaltax + proamount * st.taxRate / 100;
                                proamount = 0;
                            }
                        }


                    }

                    if (totaltax < taxlimit)
                    {
                        totaltax = taxlimit;
                    }
                    else if (totaltax - rebattax < taxlimit)
                    {
                        totaltax = taxlimit;
                    }
                    else
                    {
                        totaltax = totaltax - rebattax;
                    }
                    decimal OtherPaidTax = thisYearAdjustment + additionalTaxInfos.Where(x => x.employeeInfoId == empId && x.salaryHead?.salaryHeadName == "Vehicle Tax").Sum(x => x.exemptionAmount);
                    decimal Totalpaidtax = listProcessEmpSalaryStructure.Where(x => x.salaryPeriodId < periodId && x.salaryHead?.salaryHeadName == "Income Tax").Sum(x => x.amount);
                    decimal exixtingtax = 0;
                    if (totaltax == 0)
                    {
                        exixtingtax = 0;
                    }
                    else if (OtherPaidTax > totaltax)
                    {
                        exixtingtax = Totalpaidtax * -1;

                    }
                    else
                    {
                        exixtingtax = totaltax - Totalpaidtax - OtherPaidTax;
                    }
                    decimal taxToBePaid = exixtingtax / MonthNo;

                    var SalaryStructur = _context.ProcessEmpSalaryStructures.Find(listProcessEmpSalaryStructure.Where(x => x.employeeInfoId == empId && x.salaryHead?.salaryHeadName == "Income Tax" && x.salaryPeriodId == periodId).FirstOrDefault().Id);
                    SalaryStructur.amount = taxToBePaid;
                    _context.Entry(SalaryStructur).State = EntityState.Modified;



                }


            }

           

            return 1 == await _context.SaveChangesAsync();


        }
        #endregion
        #region Tax Slab Asign
        public async Task<int> SaveIncomeTaxSlabAssign(SlabIncomeTaxAssign slabIncomeTax)
        {
            if (slabIncomeTax.Id != 0)
            {
                _context.SlabIncomeTaxAssigns.Update(slabIncomeTax);
            }
            else
            {
                _context.SlabIncomeTaxAssigns.Add(slabIncomeTax);
            }
            await _context.SaveChangesAsync();
            return slabIncomeTax.Id;
        }

        public async Task<IEnumerable<SlabIncomeTaxAssign>> GetSlabIncomeTaxAssign()
        {
            return await _context.SlabIncomeTaxAssigns.Include(x => x.employeeInfo).Include(x => x.slabType).AsNoTracking().ToListAsync();
        }
        
        #endregion


















    }
}