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
    public class TrainingPlanDetailsService : ITrainingPlanDetailsService
    {
        private readonly ERPDbContext _context;

        public TrainingPlanDetailsService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<bool> DeleteTrainingPlanDetailsById(int id)
        {
            _context.planDetails.Remove(_context.planDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlanDetails>> GetAllTrainingPlanDetails()
        {
            return await _context.planDetails.Include(x => x.trainingOffer).Include(x => x.plan).AsNoTracking().ToListAsync();
        }

        public async Task<PlanDetails> GetTrainingPlanDetailsById(int id)
        {
            return await _context.planDetails.FindAsync(id);
        }

        public async Task<bool> SaveTrainingPlanDetails(PlanDetails planDetails)
        {
            if (planDetails.Id != 0)
                _context.planDetails.Update(planDetails);
            else
                _context.planDetails.Add(planDetails);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
