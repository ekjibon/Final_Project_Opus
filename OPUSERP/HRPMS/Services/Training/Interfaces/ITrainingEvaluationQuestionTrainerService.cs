using OPUSERP.HRPMS.Data.Entity.Training;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface ITrainingEvaluationQuestionTrainerService
    {
        Task<bool> SaveTrainingEvaluationQuestionTrainer(Evaluation evaluation);
        Task<IEnumerable<Evaluation>> GetAllTrainingEvaluationQuestionTrainer();
        Task<Evaluation> GetTrainingEvaluationQuestionTrainerById(int id);
        Task<bool> DeleteTrainingEvaluationQuestionTrainerById(int id);
    }
}
