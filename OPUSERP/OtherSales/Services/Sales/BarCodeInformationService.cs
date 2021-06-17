using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales
{
    public class BarCodeInformationService: IBarCodeService
    {
        private readonly ERPDbContext _context;

        public BarCodeInformationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveBarCodeInformation(BarCodeInformation barCodeInformation)
        {
            if (barCodeInformation.Id != 0)
                _context.BarCodeInformation.Update(barCodeInformation);
            else
                _context.BarCodeInformation.Add(barCodeInformation);
            await _context.SaveChangesAsync();
            return barCodeInformation.Id;
        }

        public async Task<List<BarCodeInformation>> GetAllBarCodeInformation()
        {
            return await _context.BarCodeInformation.Include(x=>x.itemPrice).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<BarCodeInformation>> GetBarCodeInformationByPriceId(int itemPriceId)
        {
            return await _context.BarCodeInformation.Where(x=>x.itemPriceId== itemPriceId).Include(x => x.itemPrice).AsNoTracking().ToListAsync();
        }
    }
}
