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
    public class LevelofEducationService : ILevelofEducationService
    {
        private readonly ERPDbContext _context;

        public LevelofEducationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveLevelofEducation(LevelofEducation loe)
        {
            if(loe.Id != 0)
                _context.levelofEducations.Update(loe);
            else
                _context.levelofEducations.Add(loe);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LevelofEducation>> GetAllLevelofEducation()
        {
            return await _context.levelofEducations.AsNoTracking().ToListAsync();
        }

        public async Task<LevelofEducation> GetLevelofEducationById(int id)
        {
            return await _context.levelofEducations.FindAsync(id);
        }

        public async Task<bool> DeleteLevelofEducationById(int id)
        {
            _context.levelofEducations.Remove(_context.levelofEducations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

       
    }
}
