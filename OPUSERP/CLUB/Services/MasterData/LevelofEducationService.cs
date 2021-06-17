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
    public class LevelofEducationService : ILevelofEducationService
    {
        private readonly ApplicationDbContext _context;

        public LevelofEducationService(ApplicationDbContext context)
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

        public async Task<int> GetLevelofEducationCheck(int id, string name)
        {
            var Result = await _context.levelofEducations.Where(x => x.levelofeducationName == name && x.Id != id).FirstOrDefaultAsync();
            if (Result == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
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
