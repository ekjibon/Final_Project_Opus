using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Data;
using OPUSERP.Models.Dashboard;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.Voucher
{
    public class DailyBillReceiveService : IDailyBillReceiveService
    {
        private readonly ERPDbContext _context;

        public DailyBillReceiveService(ERPDbContext context)
        {
            _context = context;
        }

        #region Daily Bill Receive 

        public async Task<int> SavedailyBillReceive(DailyBillReceive dailyBillReceive)
        {
            try
            {
                if (dailyBillReceive.Id != 0)
                {
                    _context.DailyBillReceives.Update(dailyBillReceive);
                }
                else
                {
                    _context.DailyBillReceives.Add(dailyBillReceive);
                }

                await _context.SaveChangesAsync();
                return dailyBillReceive.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<bool> UpdatedailyBillReceive(int? Id, int? VMID)
        {
            var dailyBillReceive = _context.DailyBillReceives.Find(Id);
            dailyBillReceive.BillStatusId = 1;
            dailyBillReceive.voucherMasterId = VMID;
         
            _context.Entry(dailyBillReceive).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DailyBillReceive>> GetdailyBillReceive()
        {
            return await _context.DailyBillReceives.Include(x => x.employeeInfo).Include(x => x.ResposneemployeeInfo).Include(x => x.supplier).Include(X => X.NonDepartment).Include(X => X.Item).AsNoTracking().ToListAsync();
        }
      
        public async Task<DailyBillReceive> GetdailyBillReceivebyId(int id)
        {
            return await _context.DailyBillReceives.Where(x => x.Id == id).Include(x => x.employeeInfo).Include(x => x.ResposneemployeeInfo).Include(x => x.supplier).Include(X => X.NonDepartment).Include(X => X.Item).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<SLNoViewModel> GetSLNumber()
        {
            var result = await _context.SLNoViewModels.FromSql($"SLnoBill").FirstOrDefaultAsync();
            return result;
        }
        public async Task<AutoProcessNumberViewModel> GetProcessNumber()
        {
            var result = await _context.AutoProcessNumberViewModels.FromSql($"BillProcessNo").FirstOrDefaultAsync();
            return result;
        }


        public async Task<bool> DeletedailyBillReceiveById(int id)
        {
            _context.VoucherMasters.Remove(_context.VoucherMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region bill Payment

        public async Task<DailyBillPaymentViewModel> GetDailyBillPaymentViewModel(decimal BasePrice, decimal VATRate, decimal VATExempted, decimal VATInclusive, decimal MushokProvided, decimal Rebatable, decimal Deductable, decimal VATPayBy, decimal Rebate)
        {


            decimal Vat = 0;
            decimal InvoiceValue = 0;
            decimal BankPayable = 0;
            decimal VATCurrentAmount = 0;
            decimal VATAmount = 0;
            decimal VATPaybleAmount = 0;

            if (VATExempted == 1)
            {
                Vat = 0;
            }
            else
            {
                if (VATInclusive == 0)
                {
                    Vat = BasePrice * VATRate / 100;

                }
                else
                {

                    Vat = BasePrice * VATRate / (100 + VATRate);
                }

            }

            if (VATExempted == 0)
            {
                InvoiceValue = BasePrice;
                BankPayable = BasePrice;
            }


            if (Deductable == 0 && VATExempted == 0 && Rebatable == 0)
            {
                VATCurrentAmount = Vat * Rebate / 100;
                VATAmount = Vat;
                Vat = 0;
            }

            if (Deductable == 0 && VATExempted == 0)
            {
                InvoiceValue = BasePrice + Vat;
                BankPayable = BasePrice + Vat;
                if (Rebatable == 1 && MushokProvided == 1)
                {
                    VATCurrentAmount = Vat * Rebate / 100;
                    VATAmount = Vat;
                    Vat = 0;
                }
                else
                {
                    VATCurrentAmount = 0;
                }

            }
            if (Deductable == 1 && VATExempted == 0)
            {
                if (VATInclusive == 1)
                {
                    InvoiceValue = BasePrice;
                    BankPayable = BasePrice - Vat;
                }
                else
                {
                    VATAmount = Vat;
                    InvoiceValue = BasePrice + Vat;
                    BankPayable = BasePrice;
                }

                if (Rebatable == 1 && MushokProvided == 1)
                {
                    VATCurrentAmount = Vat * Rebate / 100;

                }


            }
            DailyBillPaymentViewModel data = new DailyBillPaymentViewModel
            {
                VatAmount = VATAmount,
                VatCurrentAmount = VATCurrentAmount,
                BankPayable = BankPayable,
                InvoiceValue = InvoiceValue,
                VatPayableAmount = VATPaybleAmount
            };
            return data;

        }
        public async Task<DailyBillPaymentViewModel> GetDailyBillPaymentViewModelTax(decimal BasePrice, decimal VATRate, decimal VATExempted, decimal VATInclusive, decimal VATPayBy, int vendorId, int taxyearId, int calId, decimal BaseCalculatePrice)
        {


            decimal Vat = 0;
            decimal InvoiceValue = 0;
            decimal BankPayable = 0;
            
            TaxYear taxYear =await _context.taxYears.Take(1).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            int? VLID =await _context.Organizations.Where(x => x.Id == vendorId).Select(x => x.ledgerId).FirstOrDefaultAsync();
            IEnumerable<VoucherDetail> voucherDetails = await _context.VoucherDetails.Where(x => x.voucher.taxYearId == taxYear.Id && x.voucher.voucherType.voucherTypeName == "Payment" && x.ledgerRelation.subLedgerId == Convert.ToInt32(VLID)).AsNoTracking().ToListAsync();
            List<int?> VMIDs = voucherDetails.Select(x => x.voucherId).Distinct().ToList();
            if (VATExempted == 1)
            {
                Vat = 0;
            }
            else
            {
                if (VATRate > 0)
                {
                    //if (VATInclusive == 0)
                    //{
                    //    Vat = BasePrice * VATRate / 100;

                    //}
                    //else
                    //{

                    //    Vat = BasePrice * VATRate / (100 + VATRate);
                    //}
                    Vat = BasePrice * VATRate / 100;
                }
                else
                {

                
                  
                    decimal? ThisYearAmt = voucherDetails.Sum(x => x.amount);
                    ThisYearAmt = ThisYearAmt + BaseCalculatePrice;
                    if (calId == 0)
                    {
                        if (BasePrice <= 1500000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.02);
                        }
                        else if (BasePrice > 1500000 && BasePrice <= 2500000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.03);
                        }
                        else if (BasePrice > 2500000 && BasePrice <= 10000000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.04);
                        }
                        else if (BasePrice > 10000000 && BasePrice <= 50000000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.05);
                        }
                        else if (BasePrice > 50000000 && BasePrice <= 100000000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.06);
                        }
                        else
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.07);
                        }
                    }
                    else
                    {
                        if (BasePrice <= 1500000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.02);
                        }
                        else if (BasePrice > 1500000 && BasePrice <= 2500000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.03);
                        }
                        else if (BasePrice > 2500000 && BasePrice <= 10000000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.04);
                        }
                        else if (BasePrice > 10000000 && BasePrice <= 50000000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.05);
                        }
                        else if (BasePrice > 50000000 && BasePrice <= 100000000)
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.06);
                        }
                        else
                        {
                            Vat = Convert.ToDecimal(ThisYearAmt) * Convert.ToDecimal(0.07);
                        }

                    }

                }


            }

            if (VATExempted == 0)
            {
                InvoiceValue = BasePrice;
                BankPayable = BasePrice;
            }

            else
            {
                if (VATInclusive == 1)
                {
                    InvoiceValue = BaseCalculatePrice - Vat;
                    BankPayable = BaseCalculatePrice - Vat;
                }
                else
                {
                    if (VATPayBy == 0)
                    {
                        InvoiceValue = BaseCalculatePrice - Vat;
                        BankPayable = BaseCalculatePrice - Vat;
                    }
                    else
                    {
                        InvoiceValue = BaseCalculatePrice;
                        BankPayable = BaseCalculatePrice;

                    }

                }
            }


            decimal? paidTax = _context.VoucherDetails.Where(x => VMIDs.Contains(x.voucherId) && x.ledgerRelation.ledger.accountName == "TAX Payable ").Sum(x=>x.amount);
            decimal?balanceTax= _context.VoucherDetails.Sum(x => x.amount);
           
            DailyBillPaymentViewModel data = new DailyBillPaymentViewModel
            {
                VatAmount = 0,
               
                BankPayable = BankPayable,
                InvoiceValue = InvoiceValue,
                VatPayableAmount = Vat,
                PaidTAX=paidTax,
                BalanceAount=balanceTax,
                BaseCalculatePrice=BaseCalculatePrice,
                BasePrice=BasePrice
            };
            return data;

        }
        #endregion



    }
}
