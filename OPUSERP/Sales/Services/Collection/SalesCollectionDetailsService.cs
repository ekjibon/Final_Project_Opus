
using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Sales.Data.Entity.Collection;
using OPUSERP.Sales.Services.Collection.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Services.Collection
{
    public class SalesCollectionDetailsService : ISalesCollectionDetailsService
    {
        private readonly ERPDbContext _context;
        public SalesCollectionDetailsService(ERPDbContext context)
        {
            _context = context;
        }
        #region Collection Details


        public async Task<bool> SaveSalesCollectionDetail(SalesCollectionDetail salesCollectionDetail)
        {
            if (salesCollectionDetail.Id != 0)
                _context.SalesCollectionDetails.Update(salesCollectionDetail);
            else
                _context.SalesCollectionDetails.Add(salesCollectionDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalesCollectionDetail>> GetAllSalesCollectionDetail()
        {
            return await _context.SalesCollectionDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<SalesCollectionDetail>> GetAllSalesCollectionDetailByCollectionId(int id)
        {
            return await _context.SalesCollectionDetails.Where(x => x.salesCollectionId == id).Include(x=>x.salesInvoiceMaster).AsNoTracking().ToListAsync();
        }
       
        public async Task<SalesCollectionDetail> GetSalesCollectionDetailById(int id)
        {
            return await _context.SalesCollectionDetails.FindAsync(id);
        }

        public async Task<bool> DeleteSalesCollectionDetailById(int id)
        {
            _context.SalesCollectionDetails.Remove(_context.SalesCollectionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region RepSalesCollectionDetails 
       

        public async Task<bool> SaveRepSalesCollectionDetail(RepSalesCollectionDetail salesCollectionDetail)
        {
            if (salesCollectionDetail.Id != 0)
                _context.RepSalesCollectionDetails.Update(salesCollectionDetail);
            else
                _context.RepSalesCollectionDetails.Add(salesCollectionDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RepSalesCollectionDetail>> GetAllRepSalesCollectionDetail()
        {
            return await _context.RepSalesCollectionDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RepSalesCollectionDetail>> GetAllRepSalesCollectionDetailByCollectionId(int id)
        {
            return await _context.RepSalesCollectionDetails.Where(x => x.repSalesCollectionId == id).Include(x => x.repSalesCollection).AsNoTracking().ToListAsync();
        }

        public async Task<RepSalesCollectionDetail> GetRepSalesCollectionDetailById(int id)
        {
            return await _context.RepSalesCollectionDetails.FindAsync(id);
        }

        public async Task<bool> DeleteRepSalesCollectionDetailById(int id)
        {
            _context.RepSalesCollectionDetails.Remove(_context.RepSalesCollectionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
    }
}
