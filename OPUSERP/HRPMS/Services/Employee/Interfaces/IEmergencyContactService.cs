using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
   public interface IEmergencyContactService
    {
        Task<bool> SaveEmergencyContact(EmergencyContact emergencyContact);
        Task<IEnumerable<EmergencyContact>> GetEmergencyContact();
        Task<EmergencyContact> GetEmergencyContactById(int id);
        Task<bool> DeleteEmergencyContactById(int id);
        Task<IEnumerable<EmergencyContact>> GetEmergencyContactByEmpId(int empId);
    }
}
