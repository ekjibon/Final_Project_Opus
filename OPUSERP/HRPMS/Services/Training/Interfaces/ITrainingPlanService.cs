using OPUSERP.HRPMS.Data.Entity.Training;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface ITrainingPlanService
    {
        Task<bool> SaveTrainingPlan(Plan trainingPlan);
        Task<IEnumerable<Plan>> GetAllTrainingPlan();
        Task<Plan> GetTrainingPlanById(int id);
        Task<bool> DeleteTrainingPlanById(int id);
    }
}
