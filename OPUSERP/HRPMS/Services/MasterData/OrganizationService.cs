using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace OPUSERP.HRPMS.Services.MasterData
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ERPDbContext _context;

        public OrganizationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveOrganization(Organization organization)
        {
            if(organization.Id != 0)
                _context.organizations.Update(organization);
            else
                _context.organizations.Add(organization);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Organization>> GetAllOrganization()
        {
            return await _context.organizations.AsNoTracking().ToListAsync();
        }

        public async Task<Organization> GetOrganizationById(int id)
        {
            return await _context.organizations.FindAsync(id);
        }

        public async Task<bool> DeleteOrganizationById(int id)
        {
            _context.organizations.Remove(_context.organizations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
