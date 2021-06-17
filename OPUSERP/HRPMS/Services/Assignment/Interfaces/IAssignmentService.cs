using OPUSERP.Areas.HRPMSAssignment.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Assignment.Interfaces
{
    public interface IAssignmentService
    {
        Task<int> SaveAssignment(Data.Entity.Assignment.Assignment assignment);
        Task<IEnumerable<Data.Entity.Assignment.Assignment>> GetAssignmentByTypeAndEmpId(int empId, int assignmentType);
        Task<Data.Entity.Assignment.Assignment> GetAssignmentById(int id);
        Task<bool> DeleteAssignmentById(int id);
        Task<IEnumerable<Data.Entity.Assignment.Assignment>> GetAssignmentByEmpId(int empId);
        Task<IEnumerable<AssignmentViewModel>> GetAssignmentByFiltering(int id,int empId);
    }
}
