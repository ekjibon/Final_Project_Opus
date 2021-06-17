using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class OwnershipService : IOwnershipService
    {
        private readonly ERPDbContext _context;

        public OwnershipService(ERPDbContext context)
        {
            _context = context;
        }

        // Sector
        public async Task<bool> SaveOwnership(OwnerShipType ownerShipType)
        {
            if (ownerShipType.Id != 0)
                _context.OwnerShipTypes.Update(ownerShipType);
            else
                _context.OwnerShipTypes.Add(ownerShipType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<OwnerShipType>> GetAllOwnership()
        {
            return await _context.OwnerShipTypes.AsNoTracking().ToListAsync();
        }

        public async Task<OwnerShipType> GetOwnershipById(int id)
        {
            return await _context.OwnerShipTypes.FindAsync(id);
        }

        public async Task<bool> DeleteOwnershipById(int id)
        {
            _context.OwnerShipTypes.Remove(_context.OwnerShipTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
