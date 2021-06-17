using Microsoft.EntityFrameworkCore;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Budget.Service
{
    public class UnitOfTakaService : IUnitOfTakaService
    {
        private readonly ERPDbContext _context;

        public UnitOfTakaService(ERPDbContext context)
        {
            _context = context;
        }
        //Book Info

        public async Task<IEnumerable<UnitOfTaka>> GetUnitOfTaka()
        {
            return await _context.unitOfTakas.AsNoTracking().ToListAsync();
        }

        public async Task<UnitOfTaka> GetUnitOfTakaById(int id)
        {
            return await _context.unitOfTakas.FindAsync(id);
        }

        public async Task<int> SaveUnitOfTaka(UnitOfTaka unitOfTaka)
        {
            if (unitOfTaka.Id != 0)
                _context.unitOfTakas.Update(unitOfTaka);
            else
                _context.unitOfTakas.Add(unitOfTaka);
            await _context.SaveChangesAsync();
            return unitOfTaka.Id;
        }

        public async Task<int> UpdateUnitOfTakaStatus(UnitOfTaka unitOfTaka)
        {
            var activeUnit = _context.unitOfTakas.Where(x=>x.status == 1).FirstOrDefault();
            if (activeUnit != null)
            {
                activeUnit.status = 0;
                _context.Entry(activeUnit).State = EntityState.Modified;
                _context.SaveChanges();
            }

            var active = _context.unitOfTakas.Where(x => x.Id == unitOfTaka.Id).FirstOrDefault();
            if (unitOfTaka != null)
            {
                active.status = 1;
                _context.Entry(active).State = EntityState.Modified;
                _context.SaveChanges();
            }
            return unitOfTaka.Id;
        }

        public async Task<bool> DeleteUnitOfTakaById(int id)
        {
            _context.unitOfTakas.Remove(_context.unitOfTakas.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
