using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IProfessionalQualificationsService
    {
        Task<bool> DeleteProfessionalQualificationsById(int id);
        Task<IEnumerable<ProfessionalQualifications>> GetProfessionalQualifications();
        Task<IEnumerable<ProfessionalQualifications>> GetProfessionalQualificationsByEmpId(int empId);
        Task<ProfessionalQualifications> GetProfessionalQualificationsById(int id);
        Task<bool> SaveProfessionalQualifications(ProfessionalQualifications professionalQualifications);
    }
}
