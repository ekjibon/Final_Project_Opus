using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Organogram
{
    public class OrganizationTypeService : IOrganizationTypeService
    {
        private readonly ERPDbContext _context;

        public OrganizationTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveOrganizationType(OrganizationType organizationType)
        {
            if (organizationType.Id != 0)
                _context.organizationTypes.Update(organizationType);
            else
                _context.organizationTypes.Add(organizationType);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrganizationType>> GetAllOrganizationType()
        {
            return await _context.organizationTypes.AsNoTracking().ToListAsync();
        }

        public async Task<OrganizationType> GetOrganizationTypeById(int id)
        {
            return await _context.organizationTypes.FindAsync(id);
        }

        public async Task<bool> DeleteOrganizationTypeById(int id)
        {
            _context.organizationTypes.Remove(_context.organizationTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
