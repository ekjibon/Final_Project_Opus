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
    public class TrainingDeliverablesService: ITrainingDeliverablesService
    {
        private readonly ERPDbContext _context;

        public TrainingDeliverablesService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteTrainingDeliverablesId(int id)
        {
            _context.trainingDeliverables.Remove(_context.trainingDeliverables.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingDeliverables>> GetAllTrainingDeliverables()
        {
            return await _context.trainingDeliverables.Include(x => x.trainingInfo).AsNoTracking().ToListAsync();
        }

        public async Task<TrainingDeliverables> GetTrainingDeliverablesId(int id)
        {
            return await _context.trainingDeliverables.FindAsync(id);
        }

        public async Task<bool> SaveTrainingDeliverables(TrainingDeliverables trainingDeliverables)
        {
            if (trainingDeliverables.Id != 0)
                _context.trainingDeliverables.Update(trainingDeliverables);
            else
                _context.trainingDeliverables.Add(trainingDeliverables);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
