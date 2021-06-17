using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface IEmployeeInfoService
    {
        Task<bool> SaveEducationalQualification(EducationalQualification educationalQualification);
        Task<IEnumerable<EducationalQualification>> GetAllEducationalQualification();
        Task<EducationalQualification> GetEducationalQualificationById(int id);
        Task<bool> DeleteEducationalQualificationById(int id);
        Task<IEnumerable<EducationalQualification>> GetEducationalQualificationByEmpId(int empId);
        //Wahid
        Task<IEnumerable<EducationalQualification>> GetEducationalQualificationListByEmpId(int? empId);
        //Wahid


        Task<bool> SaveAddress(Address address);
        Task<IEnumerable<Address>> GetAllAddress();
        Task<Address> GetAddressById(int id);
        Task<bool> DeleteAddressById(int id);
        Task<IEnumerable<Address>> GetAddressByEmpId(int empId);
        Task<Address> GetAddressByTypeAndEmpId(int empId, string type);
        
        Task<bool> SaveImage(Photograph photograph);
        Task<IEnumerable<Photograph>> GetPhotograph();
        Task<IEnumerable<Photograph>> GetPhotographByEmpId(int empId);
        Task<bool> DeletePhotographById(int id);
        Task<Photograph> GetPhotographByTypeAndEmpId(int empId, string type);
     
    }
}
