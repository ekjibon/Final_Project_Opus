using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
    public interface ITraningHistoryService
    {
        Task<bool> SaveTraningHistory(TraningLog traningLog);
        Task<IEnumerable<TraningLog>> GetTraningHistory();
        Task<TraningLog> GetTraningHistoryById(int id);
        Task<bool> DeleteTraningHistoryById(int id);
        Task<IEnumerable<TraningLog>> GetTraningHistoryByEmpId(int empId);

        //Wahid
        Task<IEnumerable<TraningLog>> GetTraningHistoryListById(int? empId);
        //Wahid
    }
}
