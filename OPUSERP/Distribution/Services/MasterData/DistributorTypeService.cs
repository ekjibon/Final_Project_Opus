using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.MasterData
{
    public class DistributorTypeService : IDistributorTypeService
    {
        private readonly ERPDbContext _context;

        public DistributorTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteDistributorTypeById(int id)
        {
            _context.distributorTypes.Remove(_context.distributorTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<DistributorType>> GetAllDistributorType()
        {
            return await _context.distributorTypes.AsNoTracking().ToListAsync();
        }

        public async Task<DistributorType> GetDistributorTypeById(int id)
        {
            return await _context.distributorTypes.Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public async Task<bool> SaveDistributorType(DistributorType distributorType)
        {
            if (distributorType.Id != 0)
                _context.distributorTypes.Update(distributorType);
            else
                _context.distributorTypes.Add(distributorType);

            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
