using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;

using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Services.FuelStation.Interfaces;
using OPUSERP.VMS.Services.Resources.Interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.Resources
{
    public class VMSResourceService : IVMSResourceService
    {
        private readonly ERPDbContext _context;

        public VMSResourceService(ERPDbContext context)
        {
            _context = context;
        }

        #region Resource
        public async Task<int> SaveResource(VMSResource resource)
        {
            if (resource.Id != 0)
            {
                resource.updatedBy = resource.createdBy;
                resource.updatedAt = DateTime.Now;
                _context.VMSResources.Update(resource);
            }
            else
            {
                resource.createdAt = DateTime.Now;
                _context.VMSResources.Add(resource);
            }

            await _context.SaveChangesAsync();
            return resource.Id;
        }

        public async Task<IEnumerable<VMSResource>> GetResource()
        {
            return await _context.VMSResources.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<VMSResource> GetResourceById(int id)
        {
            return await _context.VMSResources.FindAsync(id);
        }

        public async Task<bool> DeleteResourcesById(int id)
        {
            _context.VMSResources.Remove(_context.VMSResources.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
      



    }
}
