using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.Data;
using OPUSERP.SEBA.Data.Entity;
using OPUSERP.SEBA.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SEBA.Services
{
    public class SebaService : ISebaService
    {
        private readonly ERPDbContext _context;

        public SebaService(ERPDbContext context)
        {
            _context = context;
        }

        #region CustomerProductWarranty

        public async Task<bool> SaveCustomerProductWarranty(CustomerProductWarranty customerProductWarranty)
        {
            if (customerProductWarranty.Id != 0)
                _context.CustomerProductWarranties.Update(customerProductWarranty);
            else
                _context.CustomerProductWarranties.Add(customerProductWarranty);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CustomerProductWarranty>> GetAllCustomerProductWarranty()
        {
            return await _context.CustomerProductWarranties.Include(a => a.leads).Include(a => a.itemSpecification.Item).ToListAsync();
        }

        public async Task<IEnumerable<CustomerProductWarranty>> GetCustomerProductListByLeadId(int leadId)
        {
            return await _context.CustomerProductWarranties.Include(a => a.itemSpecification.Item).Where(a => a.leadsId == leadId).ToListAsync();
        }

        public async Task<CustomerProductWarranty> GetCustomerProductWarrantyById(int id)
        {
            return await _context.CustomerProductWarranties.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCustomerProductWarrantyById(int id)
        {
            _context.CustomerProductWarranties.Remove(_context.CustomerProductWarranties.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region ProblemMaster

        public async Task<int> SaveProblemMaster(ProblemMaster problemMaster)
        {
            if (problemMaster.Id != 0)
                _context.ProblemMasters.Update(problemMaster);
            else
                _context.ProblemMasters.Add(problemMaster);
            await _context.SaveChangesAsync();
            return problemMaster.Id;
        }

        public async Task<IEnumerable<ProblemMaster>> GetAllProblemMaster()
        {
            return await _context.ProblemMasters.ToListAsync();
        }

        public async Task<ProblemMaster> GetProblemMasterById(int id)
        {
            return await _context.ProblemMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProblemMasterById(int id)
        {
            _context.ProblemMasters.Remove(_context.ProblemMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        

        public async Task<IEnumerable<System.Object>> GetProblemsList()
        {
            try
            {
                var result = (from P in _context.ProblemMasters                              
                              select new
                              {
                                  P.Id,
                                  P.tokenNo,
                                  P.priority,
                                  leadId = (from lp in _context.ProblemDetails
                                              join cpw in _context.CustomerProductWarranties on lp.customerProductWarrantyId equals cpw.Id 
                                              where P.Id == lp.problemMasterId
                                              select cpw.leadsId).FirstOrDefault(),
                                  leadName = (from lp in _context.ProblemDetails
                                                 join cpw in _context.CustomerProductWarranties on lp.customerProductWarrantyId equals cpw.Id
                                                 join ld in _context.Leads on cpw.leadsId equals ld.Id
                                                 where P.Id == lp.problemMasterId select ld.leadName).FirstOrDefault(),
                                  itemName = (from lp in _context.ProblemDetails
                                              join cpw in _context.CustomerProductWarranties on lp.customerProductWarrantyId equals cpw.Id
                                              join itsp in _context.ItemSpecifications on cpw.itemSpecificationId equals itsp.Id
                                              join itm in _context.Items on itsp.itemId equals itm.Id
                                              where P.Id == lp.problemMasterId
                                              select itm.itemName).FirstOrDefault(),
                              }).OrderByDescending(x => x.Id)
                       .AsNoTracking().ToListAsync();
                return await result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Problem Detail

        public async Task<bool> SaveProblemDetail(ProblemDetail problemDetail)
        {
            if (problemDetail.Id != 0)
                _context.ProblemDetails.Update(problemDetail);
            else
                _context.ProblemDetails.Add(problemDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProblemDetail>> GetProblemDetailByMasterId(int? masterId)
        {
            return await _context.ProblemDetails.Include(a => a.customerProductWarranty.itemSpecification.Item).Where(a => a.problemMasterId == masterId).ToListAsync();
        }

        public async Task<bool> DeleteProblemDetailById(int masterId)
        {
            _context.ProblemDetails.RemoveRange(_context.ProblemDetails.Where(x => x.problemMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


    }
}
