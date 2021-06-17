using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace OPUSERP.HRPMS.Services.MasterData
{
    public class OccupationCadreService : IOccupationCadreService
    {
        private readonly ERPDbContext _context;

        public OccupationCadreService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Occupation>> GetOccupationInfo()
        {
            return await _context.occupations.AsNoTracking().ToListAsync();
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
