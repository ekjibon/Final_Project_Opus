using OPUSERP.Areas.HRPMSReport.Models;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.TrainingNew.Interfaces
{
    public interface IEnrollTraineeService
    {
        Task<int> SaveEnrolledTrainee(EnrolledTrainee enrolledTrainee);
        Task<bool> UpdatePositionEnrolledTrainee(EnrolledTrainee enrolledTrainee);
        Task<IEnumerable<EnrolledTrainee>> GetEnrolledTrainee();
        Task<IEnumerable<EnrolledTrainee>> GetEnrolledTraineeByPlannId(int id);
        Task<EnrolledTrainee> GetEnrolledTraineeById(int id);
        Task<bool> DeleteEnrolledTraineeById(int id);

        Task<IEnumerable<EmployeeTraining>> GetTrainingListByEmployeeId(int employeeId);
    }
}
