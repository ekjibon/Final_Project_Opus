using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IQualificationAndValidationService
    {
        Task<bool> SaveQualificationAndValidation(QualificationAndValidation qualificationAndValidation);
        Task<IEnumerable<QualificationAndValidation>> GetQualificationAndValidation();
        Task<QualificationAndValidation> GetQualificationAndValidationById(int id);
        Task<bool> DeleteQualificationAndValidationById(int id);
        Task<IEnumerable<QualificationAndValidation>> GetQualificationAndValidationByEmpId(int empId);
    }
}
