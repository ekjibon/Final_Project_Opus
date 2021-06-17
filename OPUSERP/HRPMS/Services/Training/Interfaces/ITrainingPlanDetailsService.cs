using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public  interface ITrainingPlanDetailsService
    {
        Task<bool> SaveTrainingPlanDetails(PlanDetails planDetails);
        Task<IEnumerable<PlanDetails>> GetAllTrainingPlanDetails();
        Task<PlanDetails> GetTrainingPlanDetailsById(int id);
        Task<bool> DeleteTrainingPlanDetailsById(int id);
    }
}
