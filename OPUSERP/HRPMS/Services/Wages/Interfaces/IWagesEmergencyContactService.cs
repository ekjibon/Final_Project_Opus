using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
   public interface IWagesEmergencyContactService
    {
        Task<bool> SaveEmergencyContact(WagesEmergencyContact emergencyContact);
        Task<IEnumerable<WagesEmergencyContact>> GetEmergencyContact();
        Task<WagesEmergencyContact> GetEmergencyContactById(int id);
        Task<bool> DeleteEmergencyContactById(int id);
        Task<IEnumerable<WagesEmergencyContact>> GetEmergencyContactByEmpId(int empId);
    }
}
