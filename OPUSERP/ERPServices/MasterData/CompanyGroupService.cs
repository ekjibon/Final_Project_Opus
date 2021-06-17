using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.ERPServices.MasterData
{
    public class CompanyGroupService : ICompanyGroupService
    {
        private readonly ERPDbContext _context;

        public CompanyGroupService(ERPDbContext context)
        {
            _context = context;
        }

        // Company Group
        public async Task<bool> SaveCompanyGroup(CompanyGroup companyGroup)
        {
            if (companyGroup.Id != 0)
                _context.CompanyGroups.Update(companyGroup);
            else
                _context.CompanyGroups.Add(companyGroup);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CompanyGroup>> GetAllCompanyGroup()
        {
            return await _context.CompanyGroups.AsNoTracking().ToListAsync();
        }

        public async Task<CompanyGroup> GetCompanyGroupById(int id)
        {
            return await _context.CompanyGroups.FindAsync(id);
        }

        public async Task<bool> DeleteCompanyGroupById(int id)
        {
            _context.CompanyGroups.Remove(_context.CompanyGroups.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
