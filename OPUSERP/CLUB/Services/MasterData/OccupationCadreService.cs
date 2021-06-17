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
    public class OccupationCadreService : IOccupationCadreService
    {
        private readonly ApplicationDbContext _context;

        public OccupationCadreService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Occupation>> GetOccupationInfo()
        {
            return await _context.occupations.AsNoTracking().ToListAsync();
        }

        public async Task<int> GetOccupationCheck(int id, string name)
        {
            var Result = await _context.occupations.Where(x => x.occupationName == name && x.Id != id).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public async Task<Occupation> GetOccupationInfoById(int id)
        {
            return await _context.occupations.FindAsync(id);
        }

        public async Task<bool> SaveOccupationInfo(Occupation occupation)
        {
            if(occupation.Id != 0)
                _context.occupations.Update(occupation);
            else
                _context.occupations.Add(occupation);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteOccupationInfoById(int id)
        {
            _context.occupations.Remove(_context.occupations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        // Cadre Info

        public async Task<IEnumerable<Cadre>> GetCadreInfo()
        {
            return await _context.cadres.AsNoTracking().ToListAsync();
        }

        public async Task<Cadre> GetCadreInfoById(int id)
        {
            return await _context.cadres.FindAsync(id);
        }

        public async Task<bool> SaveCadreInfo(Cadre cadre)
        {
            if(cadre.Id != 0)
                _context.cadres.Update(cadre);
            else
                _context.cadres.Add(cadre);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCadreInfoById(int id)
        {
            _context.cadres.Remove(_context.cadres.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
