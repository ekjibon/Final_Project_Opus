using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface IRlnTrainingTrainerService
    {
        Task<bool> SaverlnTrainingTrainer(RlnTrainingTrainer rlnTrainingTrainer);
        Task<IEnumerable<RlnTrainingTrainer>> GetAllRlnTrainingTrainer();
        Task<RlnTrainingTrainer> GetRlnTrainingTrainerId(int id);
        Task<bool> DeleteRlnTrainingTrainerId(int id);
    }
}
