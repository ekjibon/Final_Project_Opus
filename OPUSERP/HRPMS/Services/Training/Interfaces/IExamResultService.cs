using OPUSERP.HRPMS.Data.Entity.Training;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Training.Interfaces
{
    public interface IExamResultService
    {
        Task<bool> SaveExamResult(ExamResult examResult);
        Task<IEnumerable<ExamResult>> GetAllExamResult();
        Task<ExamResult> GetExamResultId(int id);
        Task<bool> DeleteExamResultId(int id);
    }
}
