using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace CLUB.Services.MasterData
{
    public class DegreeService : IDegreeService
    {
        private readonly ApplicationDbContext _context;

        public DegreeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveDegree(Degree degree)
        {
            if(degree.Id != 0)
                _context.degrees.Update(degree);
            else
                _context.degrees.Add(degree);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> GetDegreeCheck(int id, string name,int leoId)
        {
            var Result = await _context.degrees.Where(x => x.degreeName == name && x.Id != id && x.levelofeducationId == leoId).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<IEnumerable<Degree>> GetAllDegree()
        {
            return await _context.degrees.AsNoTracking().ToListAsync();
        }

        public async Task<Degree> GetDegreeById(int id)
        {
            return await _context.degrees.FindAsync(id);
        }

        public async Task<bool> DeleteDegreeById(int id)
        {
            _context.degrees.Remove(_context.degrees.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Degree>> GetDegreeByLOEId(int loEId)
        {
            return await _context.degrees.Where(x => x.levelofeducationId == loEId).ToListAsync();
        }
    }
}
