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
    public class TrainingMiscallenousService : ITrainingMiscallenousService
    {
        private readonly ERPDbContext _context;

        public TrainingMiscallenousService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteTrainingMiscallenousId(int id)
        {
            _context.trainingMiscallenous.Remove(_context.trainingMiscallenous.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingMiscallenous>> GetAllTrainingMiscallenous()
        {
            return await _context.trainingMiscallenous.Include(x => x.trainingInfo).AsNoTracking().ToListAsync();
        }

        public async Task<TrainingMiscallenous> GetTrainingMiscallenousId(int id)
        {
            return await _context.trainingMiscallenous.FindAsync(id);
        }

        public async Task<bool> SaveTrainingMiscallenous(TrainingMiscallenous trainingMiscallenous)
        {
            if (trainingMiscallenous.Id != 0)
                _context.trainingMiscallenous.Update(trainingMiscallenous);
            else
                _context.trainingMiscallenous.Add(trainingMiscallenous);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
