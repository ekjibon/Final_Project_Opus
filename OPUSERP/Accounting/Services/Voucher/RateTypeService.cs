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
    public class RateTypeService : IRateTypeService
    {
        private readonly ERPDbContext _context;

        public RateTypeService(ERPDbContext context)
        {
            _context = context;
        }

        #region RateType 

        public async Task<int> SaveRateType(RateType rateType)
        {
            try
            {
                if (rateType.Id != 0)
                {                   
                    _context.RateTypes.Update(rateType);
                }
                else
                {                   
                    _context.RateTypes.Add(rateType);
                }

                await _context.SaveChangesAsync();
                return rateType.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<RateType>> GetrateType()
        {
            return await _context.RateTypes.AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteRateTypeById(int id)
        {
            _context.RateTypes.Remove(_context.RateTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion



    }
}
