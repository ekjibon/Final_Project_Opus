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
    public class TrainingInfoDetailsService : ITrainingInfoDetailsService
    {
        private readonly ERPDbContext _context;

        public TrainingInfoDetailsService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteTrainingInfoDetailsId(int id)
        {
            _context.trainingInfoDetails.Remove(_context.trainingInfoDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingInfoDetails>> GetAllTrainingInfoDetails()
        {
            return await _context.trainingInfoDetails.Include(x => x.trainingInfo).AsNoTracking().ToListAsync();
        }

        public async Task<TrainingInfoDetails> GetTrainingInfoDetailsId(int id)
        {
            return await _context.trainingInfoDetails.FindAsync(id);
        }

        public async Task<bool> SaveTrainingInfoDetails(TrainingInfoDetails trainingInfoDetails)
        {
            if (trainingInfoDetails.Id != 0)
                _context.trainingInfoDetails.Update(trainingInfoDetails);
            else
                _context.trainingInfoDetails.Add(trainingInfoDetails);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
