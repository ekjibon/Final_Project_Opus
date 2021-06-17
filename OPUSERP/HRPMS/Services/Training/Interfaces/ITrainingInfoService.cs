using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface ITrainingInfoService
    {
        Task<bool> SaveTrainingInfo(TrainingInfo training);
        Task<IEnumerable<TrainingInfo>> GetAllTrainingInfo();
        Task<TrainingInfo> GetTrainingInfoId(int id);
        Task<bool> DeleteTrainingInfoId(int id);
    }
}
