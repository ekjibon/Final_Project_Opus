using OPUSERP.HRPMS.Data.Entity.Training;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface ITrainerInfoService
    {
        Task<bool> SaveTrainerInfo(Trainer trainerInfo);
        Task<IEnumerable<Trainer>> GetAllTrainerInfo();
        Task<Trainer> GetTrainerInfoById(int id);
        Task<bool> DeleteTrainerInfoById(int id);
    }
}
