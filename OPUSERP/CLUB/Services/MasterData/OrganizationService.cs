using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CLUB.Services.MasterData
{
    public class OrganizationService : IOrganizationService
    {
        private readonly ApplicationDbContext _context;

        public OrganizationService(ApplicationDbContext context)
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

        public async Task<int> GetOrganizationCheck(int id, string name)
        {
            var Result = await _context.organizations.Where(x => x.organizationName == name && x.Id != id).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
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
