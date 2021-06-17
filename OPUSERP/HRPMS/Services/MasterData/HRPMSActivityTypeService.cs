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
    public class HRPMSActivityTypeService: IHRPMSActivityTypeService
    {
        private readonly ERPDbContext _context;

        public HRPMSActivityTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveHRPMSActivityType(HRPMSActivityType hRPMSActivityType)
        {
            if (hRPMSActivityType.Id != 0)
                _context.hRPMSActivityTypes.Update(hRPMSActivityType);
            else
                _context.hRPMSActivityTypes.Add(hRPMSActivityType);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<HRPMSActivityType>> GetHRPMSActivityType()
        {
            return await _context.hRPMSActivityTypes.AsNoTracking().ToListAsync();
        }

        public async Task<HRPMSActivityType> GetHRPMSActivityTypeById(int id)
        {
            return await _context.hRPMSActivityTypes.FindAsync(id);
        }

        public async Task<bool> DeleteHRPMSActivityTypeById(int id)
        {
            _context.hRPMSActivityTypes.Remove(_context.hRPMSActivityTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
