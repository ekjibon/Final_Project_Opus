using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface ITrainingDeliverablesService
    {
        Task<bool> SaveTrainingDeliverables(TrainingDeliverables trainingDeliverables);
        Task<IEnumerable<TrainingDeliverables>> GetAllTrainingDeliverables();
        Task<TrainingDeliverables> GetTrainingDeliverablesId(int id);
        Task<bool> DeleteTrainingDeliverablesId(int id);
    }
}
