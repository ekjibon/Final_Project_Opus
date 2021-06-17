using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IAwardPublicationLanguageService
    {
        //Award
        Task<bool> SaveAward(EmployeeAward employeeAward);
        Task<IEnumerable<EmployeeAward>> GetAward();
        Task<EmployeeAward> GetAwardById(int id);
        Task<bool> DeleteAwardById(int id);
        Task<IEnumerable<EmployeeAward>> GetAwardsByEmpId(int empId);

        //publications
        Task<bool> SavePublication(Publication publication);
        Task<IEnumerable<Publication>> GetPublication();
        Task<Publication> GetPublicationById(int id);
        Task<bool> DeletePublicationById(int id);
        Task<IEnumerable<Publication>> GetPublicationsByEmpId(int empId);

        //Languages
        Task<bool> SaveLanguage(EmployeeLanguage employeeLanguage);
        Task<IEnumerable<EmployeeLanguage>> GetLanguage();
        Task<EmployeeLanguage> GetLanguageById(int id);
        Task<bool> DeleteLanguageById(int id);
        Task<IEnumerable<EmployeeLanguage>> GetLanguageByEmpId(int empId);
    }
}
