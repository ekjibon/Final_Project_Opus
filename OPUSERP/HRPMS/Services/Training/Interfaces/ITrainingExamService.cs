using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
   public interface ITrainingExamService
    {
        Task<bool> SaveTrainingExam(TrainingExam trainingExam);
        Task<IEnumerable<TrainingExam>> GetAllTrainingExam();
        Task<TrainingExam> GetTrainingExamId(int id);
        Task<bool> DeleteTrainingExamId(int id);
    }
}
