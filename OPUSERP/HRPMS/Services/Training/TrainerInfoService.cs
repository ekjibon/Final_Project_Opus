using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Training;
using OPUSERP.HRPMS.Services.Training.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training
{
    public class TrainerInfoService : ITrainerInfoService
    {
        private readonly ERPDbContext _context;

        public TrainerInfoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTrainerInfo(Trainer trainerInfo)
        {
            if (trainerInfo.Id != 0)
                _context.trainers.Update(trainerInfo);
            else
                _context.trainers.Add(trainerInfo);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Trainer>> GetAllTrainerInfo()
        {
            return await _context.trainers.Include(x => x.employee).AsNoTracking().ToListAsync();
        }

        public async Task<Trainer> GetTrainerInfoById(int id)
        {
            return await _context.trainers.FindAsync(id);
        }

        public async Task<bool> DeleteTrainerInfoById(int id)
        {
            _context.trainers.Remove(_context.trainers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}