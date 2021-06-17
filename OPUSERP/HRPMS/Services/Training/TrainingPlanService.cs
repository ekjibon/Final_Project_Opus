using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Training;
using OPUSERP.HRPMS.Services.Training.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training
{
    public class TrainingPlanService : ITrainingPlanService
    {
        private readonly ERPDbContext _context;

        public TrainingPlanService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTrainingPlan(Plan trainingPlan)
        {
            if (trainingPlan.Id != 0)
                _context.plans.Update(trainingPlan);
            else
                _context.plans.Add(trainingPlan);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Plan>> GetAllTrainingPlan()
        {
            return await _context.plans.AsNoTracking().ToListAsync();
        }

        public async Task<Plan> GetTrainingPlanById(int id)
        {
            return await _context.plans.FindAsync(id);
        }

        public async Task<bool> DeleteTrainingPlanById(int id)
        {
            _context.plans.Remove(_context.plans.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}