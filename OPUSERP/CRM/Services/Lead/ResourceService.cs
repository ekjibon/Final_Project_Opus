using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{
   
    public class ResourceService : IResourceService
    {
        private readonly ERPDbContext _context;

        public ResourceService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Resource>> GetTotalContact()
        {
            return await _context.Resources.ToListAsync();
        }

        public async Task<bool> SaveResource(Resource Resource)
        {
            if (Resource.Id != 0)
                _context.Resources.Update(Resource);
            else
                _context.Resources.Add(Resource);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Resource>> GetAllResource()
        {
            return await _context.Resources.AsNoTracking().Include(x=>x.departments).Include(x=>x.designations).ToListAsync();
        }

        public async Task<Resource> GetResourceById(int id)
        {
            //return await _context.Resources.FindAsync(id);
            return await _context.Resources.Where(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteResourceById(int id)
        {
            _context.Resources.Remove(_context.Resources.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        public async Task<int> SaveResourceReturnId(Resource Resource)
        {
            if (Resource.Id != 0)
                _context.Resources.Update(Resource);
            else
                _context.Resources.Add(Resource);
           await _context.SaveChangesAsync();
            return Resource.Id;
        }

    }
}
