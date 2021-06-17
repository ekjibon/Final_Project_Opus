using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IOtherQualificationService
    {
        Task<bool> DeleteOtherQualificationById(int id);
        Task<IEnumerable<OtherQualification>> GetOtherQualification();
        Task<IEnumerable<OtherQualification>> GetOtherQualificationByEmpId(int empId);
        Task<OtherQualification> GetOtherQualificationById(int id);
        Task<bool> SaveOtherQualification(OtherQualification otherQualification);
    }
}
