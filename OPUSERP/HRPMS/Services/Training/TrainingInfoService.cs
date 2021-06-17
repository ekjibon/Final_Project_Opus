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
    public class TrainingInfoService : ITrainingInfoService
    {
        private readonly ERPDbContext _context;

        public TrainingInfoService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteTrainingInfoId(int id)
        {
            _context.trainingInfos.Remove(_context.trainingInfos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingInfo>> GetAllTrainingInfo()
        {
            return await _context.trainingInfos.Include(x=>x.planDetails).Include(x => x.organization).AsNoTracking().ToListAsync();
        }

        public async Task<TrainingInfo> GetTrainingInfoId(int id)
        {
            return await _context.trainingInfos.FindAsync(id);
        }

        public async Task<bool> SaveTrainingInfo(TrainingInfo training)
        {
            if (training.Id != 0)
                _context.trainingInfos.Update(training);
            else
                _context.trainingInfos.Add(training);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
