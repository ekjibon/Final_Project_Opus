using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using OPUSERP.HRPMS.Services.TrainingNew.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.TrainingNew
{
    public class ResourcePersonService : IResourcePersonService
    {
        private readonly ERPDbContext _context;

        public ResourcePersonService(ERPDbContext context)
        {
            _context = context;
        }

        //ApplicationForm
        public async Task<bool> DeleteResourcePersonInfoById(int id)
        {
            _context.resourcePersons.Remove(_context.resourcePersons.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ResourcePerson>> GetResourcePersonInfo()
        {
            return await _context.resourcePersons.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ResourcePerson>> GetResourcePersonInfoByOrg(string org)
        {
            return await _context.resourcePersons.Where(x=>x.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<ResourcePerson> GetResourcePersonInfoById(int id)
        {
            return await _context.resourcePersons.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateResourcePersonInfoById(ResourcePerson resourcePerson)
        {
            ResourcePerson resourcePerson1 = _context.resourcePersons.Find(resourcePerson.Id);
            resourcePerson1.specialization = resourcePerson.specialization;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> SaveResourcePersonInfo(ResourcePerson resourcePerson)
        {
            if (resourcePerson.Id != 0)
                _context.resourcePersons.Update(resourcePerson);
            else
                _context.resourcePersons.Add(resourcePerson);
            await _context.SaveChangesAsync();
            return resourcePerson.Id;
        }
    }
}
