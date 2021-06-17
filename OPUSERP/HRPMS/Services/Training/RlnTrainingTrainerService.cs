using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Training;
using OPUSERP.HRPMS.Services.Training.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training
{
    public class RlnTrainingTrainerService : IRlnTrainingTrainerService
    {
        private readonly ERPDbContext _context;

        public RlnTrainingTrainerService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteRlnTrainingTrainerId(int id)
        {
            _context.rlnTrainingTrainers.Remove(_context.rlnTrainingTrainers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RlnTrainingTrainer>> GetAllRlnTrainingTrainer()
        {
            return await _context.rlnTrainingTrainers.Include(x=>x.trainer.employee).Include(x=>x.trainingOffer).AsNoTracking().ToListAsync();
        }

        public async Task<RlnTrainingTrainer> GetRlnTrainingTrainerId(int id)
        {
            return await _context.rlnTrainingTrainers.FindAsync(id);
        }

        public async Task<bool> SaverlnTrainingTrainer(RlnTrainingTrainer rlnTrainingTrainer)
        {
            if (rlnTrainingTrainer.Id != 0)
                _context.rlnTrainingTrainers.Update(rlnTrainingTrainer);
            else
                _context.rlnTrainingTrainers.Add(rlnTrainingTrainer);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
