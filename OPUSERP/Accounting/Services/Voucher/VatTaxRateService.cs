using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Data;
using OPUSERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.Voucher
{
    public class VatTaxRateService : IVatTaxRateService
    {
        private readonly ERPDbContext _context;

        public VatTaxRateService(ERPDbContext context)
        {
            _context = context;
        }
        #region RateType 
        public async Task<int> SavevatTaxRate(VatTaxRate vatTaxRate)
        {
            try
            {
                if (vatTaxRate.Id != 0)
                {                   
                    _context.VatTaxRates.Update(vatTaxRate);
                }
                else
                {                   
                    _context.VatTaxRates.Add(vatTaxRate);
                }

                await _context.SaveChangesAsync();
                return vatTaxRate.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<VatTaxRate>> GetvatTaxRate()
        {
            return await _context.VatTaxRates.Include(x => x.rateType).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<VatTaxRate>> GetvatTaxRatebyTypeId(int Id)
        {
            return await _context.VatTaxRates.Include(x=>x.rateType).Where(x=>x.rateTypeId==Id).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteVatTaxRateById(int id)
        {
            _context.VatTaxRates.Remove(_context.VatTaxRates.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion



    }
}
