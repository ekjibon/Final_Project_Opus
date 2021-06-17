using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Employee.Interfaces
{
   public interface INomineeDetailService
    {
        Task<int> SaveNomineeDetail(NomineeDetail nomineeDetail);
        Task<IEnumerable<NomineeDetail>> GetNomineeDetail();
        Task<NomineeDetail> GetNomineeDetailById(int id);
        Task<bool> DeleteNomineeDetailById(int id);
        Task<bool> DeleteNomineeDetailByNomineeId(int nomineeId);
        Task<IEnumerable<NomineeDetail>> GetNomineeDetailByNomineeId(int NomineeId);
        Task<decimal> GetNomineeRemainingFunByEmpIdAndFundId(int empId, int fundId);
        Task<IEnumerable<NomineeDetail>> GetNomineeDetailByEmployeeId(int employeeId);
    }
}
