using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.MasterData
{
    public class StoreDepartmentTypeService : IStoreDepartmentService
    {
        private readonly ERPDbContext _context;

        public StoreDepartmentTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteDeartmentType(int id)
        {
            _context.StoreDepartmentTypes.Remove(await _context.StoreDepartmentTypes.FindAsync(id));
           return 1== await _context.SaveChangesAsync();
            
        }

        public async Task<StoreDepartmentType> GetDeartmentTypeById(int id)
        {
            return await _context.StoreDepartmentTypes.FindAsync(id);
        }

        public async Task<IEnumerable<StoreDepartmentType>> GetDeartmentTypeList()
        {
            return await _context.StoreDepartmentTypes.AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveDepartmentType(StoreDepartmentType storeDepartmentType)
        {
            if (storeDepartmentType.Id != 0)
            {
                storeDepartmentType.updatedBy = storeDepartmentType.createdBy;
                storeDepartmentType.updatedAt = DateTime.Now;
                _context.StoreDepartmentTypes.Update(storeDepartmentType);
            }
            else
            {
                storeDepartmentType.createdAt = DateTime.Now;
                _context.StoreDepartmentTypes.Add(storeDepartmentType);
            }
            await _context.SaveChangesAsync();
            return storeDepartmentType.Id;
        }


        public void UpdateDepartmentType(StoreDepartmentType storeDepartmentType)
        {
            _context.Entry(storeDepartmentType).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
