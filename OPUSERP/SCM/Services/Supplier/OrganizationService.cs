using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.SCM.Services.Supplier.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Supplier
{
    public class OrganizationService: IOrganizationService
    {
        private readonly ERPDbContext _context;

        public OrganizationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveOrganization(Organization organization)
        {
            if (organization.Id != 0)
            {
                _context.Organizations.Update(organization);
            }
            else
            {
                _context.Organizations.Add(organization);
            }
            await _context.SaveChangesAsync();
            return organization.Id;
        }

        public async Task<bool> UpdateVendorStatus(int Id, int ledgerId)
        {
            var dailyBillReceive = _context.Organizations.Find(Id);
            dailyBillReceive.ledgerId = ledgerId;

            _context.Entry(dailyBillReceive).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Organization>> GetAllOrganization()
        {
            return await _context.Organizations.AsNoTracking().OrderBy(a => a.Id).ToListAsync();
        }
        public async Task<IEnumerable<Organization>> GetOrganizationByLedgerId()
        {
            return await _context.Organizations.Where(x=>x.ledgerId==null).ToListAsync();
        }

        public async Task<Organization> GetOrganizationById(int id)
        {
            return await _context.Organizations.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteOrganizationsById(int id)
        {
            _context.Organizations.Remove(_context.Organizations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<Organization> GetorganizationBysupplierName(string SupplierName)
        {
            return await _context.Organizations.Where(x => x.organizationName == SupplierName).FirstOrDefaultAsync();
        }


        // Auto Organization Code
        public async Task<string> GetAutoOrganizationCode()
        {
            var data = await _context.organizationCodeViewModels.FromSql(@"GetAutoOrganizationCode").AsNoTracking().ToListAsync();
            return data.FirstOrDefault().orgCode;
        }
        //public async Task<bool> UpdateVendorStatus(int Id, int ledgerId)
        //{
        //    var Agreement = _context.Ledgers.Find(Id);
        //    //Agreement.isProposed = statusId;
        //    _context.Entry(Agreement).State = EntityState.Modified;
        //    return 1 == await _context.SaveChangesAsync();
        //}
    }
}
