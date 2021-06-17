using OPUSERP.HRPMS.Data.Entity.Training;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface ITrainingAllocationService
    {
        Task<bool> SaveTrainingAllocationInfo(TrainingAllocation trainingAllocation);
        Task<IEnumerable<TrainingAllocation>> GetAlltrainingAllocation();
        Task<TrainingAllocation> GettrainingAllocationById(int id);
        Task<bool> DeletetrainingAllocationById(int id);
    }
}
