using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.TrainingNew.Interfaces
{
   public interface ITrainingResourcePersonService
    {
        Task<int> SaveTrainingResourcePerson(TrainingResourcePerson trainingResourcePerson);
        Task<IEnumerable<TrainingResourcePerson>> GetTrainingResourcePerson();
        Task<TrainingResourcePerson> GetTrainingResourcePersonById(int id);
        Task<IEnumerable<TrainingResourcePerson>> GetTrainingResourcePersonByTrainingId(int TrainingId);
        Task<bool> DeleteTrainingResourcePersonById(int id);
    }
}
