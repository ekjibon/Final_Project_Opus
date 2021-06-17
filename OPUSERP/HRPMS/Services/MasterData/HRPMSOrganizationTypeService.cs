using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class HRPMSOrganizationTypeService: IHRPMSOrganizationTypeService
    {
        private readonly ERPDbContext _context;

        public HRPMSOrganizationTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveHRPMSOrganizationType(HRPMSOrganizationType hRPMSOrganizationType)
        {
            if (hRPMSOrganizationType.Id != 0)
                _context.hRPMSOrganizationTypes.Update(hRPMSOrganizationType);
            else
                _context.hRPMSOrganizationTypes.Add(hRPMSOrganizationType);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HRPMSOrganizationType>> GetHRPMSOrganizationType()
        {
            return await _context.hRPMSOrganizationTypes.AsNoTracking().ToListAsync();
        }

        public async Task<HRPMSOrganizationType> GetHRPMSOrganizationTypeById(int id)
        {
            return await _context.hRPMSOrganizationTypes.FindAsync(id);
        }

        public async Task<bool> DeleteHRPMSOrganizationTypeById(int id)
        {
            _context.hRPMSOrganizationTypes.Remove(_context.hRPMSOrganizationTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
