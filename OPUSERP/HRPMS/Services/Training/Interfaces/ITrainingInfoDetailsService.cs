using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
   public interface ITrainingInfoDetailsService
    {
        Task<bool> SaveTrainingInfoDetails(TrainingInfoDetails trainingInfoDetails);
        Task<IEnumerable<TrainingInfoDetails>> GetAllTrainingInfoDetails();
        Task<TrainingInfoDetails> GetTrainingInfoDetailsId(int id);
        Task<bool> DeleteTrainingInfoDetailsId(int id);
    }
}
