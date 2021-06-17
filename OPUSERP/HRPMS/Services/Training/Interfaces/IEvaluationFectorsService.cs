using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface IEvaluationFectorsService
    {
        Task<bool> SaveEvaluationFectors(EvaluationFectors dailyAttendance);
        Task<IEnumerable<EvaluationFectors>> GetAllEvaluationFectors();
        Task<EvaluationFectors> GetEvaluationFectorsId(int id);
        Task<bool> DeleteEvaluationFectorsId(int id);
    }
}
