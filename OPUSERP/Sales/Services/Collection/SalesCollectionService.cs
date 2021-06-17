

using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Sales.Models.NotMapped;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data;
using OPUSERP.Sales.Data.Entity.Collection;
using OPUSERP.Sales.Data.Entity.Sales;
using OPUSERP.Sales.Services.Collection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Services.Collection
{
    public class SalesCollectionService : ISalesCollectionService
    {
        private readonly ERPDbContext _context;

        public SalesCollectionService(ERPDbContext context)
        {
            _context = context;
        }
        #region SalesCollection
        public async Task<int> SaveSalesCollection(SalesCollection salesCollection)
        {
            if (salesCollection.Id != 0)
                _context.SalesCollections.Update(salesCollection);
            else
                _context.SalesCollections.Add(salesCollection);
            await _context.SaveChangesAsync();
            return salesCollection.Id;
        }

        public async Task<IEnumerable<SalesCollection>> GetAllSalesCollection()
        {
            return await _context.SalesCollections.Include(x=>x.leads).AsNoTracking().ToListAsync();
        }

        public async Task<SalesCollection> GetSalesCollectionById(int id)
        {
            return await _context.SalesCollections.Where(x=>x.Id==id).Include(x=>x.company).Include(x=> x.leads).FirstOrDefaultAsync();
        }
       

        public async Task<bool> DeleteSalesCollectionById(int id)
        {
            _context.SalesCollections.Remove(_context.SalesCollections.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Leads>> GetDuesCustomerList()
        {
            List<int?> relCustomerIds = await _context.SalesInvoiceMasters.Where(x => x.isClose == 0).Select(x => x.leadsId).ToListAsync();

            return await _context.Leads.Where(x => relCustomerIds.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Leads>> GetCustomerListForSalesReport()
        {
            List<int?> relCustomerIds = await _context.SalesInvoiceMasters.Select(x => x.leadsId).ToListAsync();

            return await _context.Leads.Where(x => relCustomerIds.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Leads>> GetCollectionCustomerList()
        {
            List<int?> relCustomerIds = await _context.SalesCollections.Select(x => x.leadsId).ToListAsync();

            return await _context.Leads.Where(x => relCustomerIds.Contains(x.Id)).AsNoTracking().ToListAsync();
        }


        public async Task<IEnumerable<CustomerWithDue>> GetCustomerWithDue()
        {
            List<int?> relCustomerIds = await _context.SalesInvoiceMasters.Where(x => x.isClose == 0).Select(x => x.leadsId).ToListAsync();

            IEnumerable<Leads> relSupplierCustomerResourses = await _context.Leads.Where(x => relCustomerIds.Contains(x.Id)).AsNoTracking().ToListAsync();

            List<CustomerWithDue> data = new List<CustomerWithDue>();
            foreach (Leads relSupplierCustomerResourse in relSupplierCustomerResourses)
            {
                var haveToPay = _context.SalesInvoiceMasters.Where(x => x.leadsId == relSupplierCustomerResourse.Id).Sum(s => s.NetTotal);
                SalesInvoiceMaster datastore = _context.SalesInvoiceMasters.Where(x => x.leadsId == relSupplierCustomerResourse.Id && x.isClose == 0).FirstOrDefault();

                var collection = _context.SalesCollections.Where(x => x.leadsId == relSupplierCustomerResourse.Id).Sum(s => s.collectionAmount);
                var Due = haveToPay - collection;
                if (Due > 0)
                {
                    data.Add(new CustomerWithDue
                    {
                        relSupplierCustomerResourse = relSupplierCustomerResourse,
                        haveToPay = haveToPay,
                        paid = collection,
                        due = Due
                    });
                }

            }
            return data;
        }
        #endregion
        #region repcollection
        public async Task<int> SaveRepSalesCollection(RepSalesCollection salesCollection)
        {
            if (salesCollection.Id != 0)
                _context.RepSalesCollections.Update(salesCollection);
            else
                _context.RepSalesCollections.Add(salesCollection);
            await _context.SaveChangesAsync();
            return salesCollection.Id;
        }

        public async Task<IEnumerable<RepSalesCollection>> GetAllRepSalesCollection()
        {
            return await _context.RepSalesCollections.Include(x => x.leads).AsNoTracking().ToListAsync();
        }

        public async Task<RepSalesCollection> GetRepSalesCollectionById(int id)
        {
            return await _context.RepSalesCollections.Where(x => x.Id == id).Include(x => x.company).Include(x => x.leads).FirstOrDefaultAsync();
        }


        public async Task<bool> DeleteRepSalesCollectionById(int id)
        {
            _context.RepSalesCollections.Remove(_context.RepSalesCollections.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
        #endregion


    }
}
