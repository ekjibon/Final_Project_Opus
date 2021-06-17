using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.Training;
using OPUSERP.HRPMS.Services.Training.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training
{
    public class TrainingEvaluationQuestionTrainerService : ITrainingEvaluationQuestionTrainerService
    {
        private readonly ERPDbContext _context;

        public TrainingEvaluationQuestionTrainerService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveTrainingEvaluationQuestionTrainer(Evaluation evaluation)
        {
            if (evaluation.Id != 0)
                _context.evaluations.Update(evaluation);
            else
                _context.evaluations.Add(evaluation);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Evaluation>> GetAllTrainingEvaluationQuestionTrainer()
        {
            return await _context.evaluations.Include(x=>x.employee).Include(x => x.evaluationFectors).Include(x => x.trainer.employee).Include(x => x.trainingInfo).AsNoTracking().ToListAsync();
        }

        public async Task<Evaluation> GetTrainingEvaluationQuestionTrainerById(int id)
        {
            return await _context.evaluations.FindAsync(id);
        }

        public async Task<bool> DeleteTrainingEvaluationQuestionTrainerById(int id)
        {
            _context.evaluations.Remove(_context.evaluations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}