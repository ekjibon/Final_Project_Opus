using OPUSERP.Data;
using OPUSERP.HRPMS.Services.Training.Interfaces;

namespace OPUSERP.HRPMS.Services.Training
{
    public class TrainingEvaluationQuestionTraineeService : ITrainingEvaluationQuestionTraineeService
    {
        //private readonly ERPDbContext _context;

        //public TrainingEvaluationQuestionTraineeService(ERPDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task<bool> SaveTrainingEvaluationQuestionTrainee(TrainingEvaluationQuestionTrainee trainingEvaluationQuestionTrainee)
        //{
        //    if (trainingEvaluationQuestionTrainee.Id != 0)
        //        _context.trainingEvaluationQuestionTrainees.Update(trainingEvaluationQuestionTrainee);
        //    else
        //        _context.trainingEvaluationQuestionTrainees.Add(trainingEvaluationQuestionTrainee);
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<TrainingEvaluationQuestionTrainee>> GetAllTrainingEvaluationQuestionTrainee()
        //{
        //    return await _context.trainingEvaluationQuestionTrainees.ToListAsync();
        //}

        //public async Task<TrainingEvaluationQuestionTrainee> GetTrainingEvaluationQuestionTraineeById(int id)
        //{
        //    return await _context.trainingEvaluationQuestionTrainees.FindAsync(id);
        //}

        //public async Task<bool> DeleteTrainingEvaluationQuestionTraineeById(int id)
        //{
        //    _context.trainingEvaluationQuestionTrainees.Remove(_context.trainingEvaluationQuestionTrainees.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}

       
    }
}