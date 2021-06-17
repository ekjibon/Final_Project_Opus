using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Payroll.Data.Entity.Salary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Payroll.Services.FinalSettlements.Interfaces
{
    public interface IFinalSettlementService
    {
        #region FinalSettlement
        Task<bool> SaveFinalSettlement(FinalSettlement finalSettlement);
        Task<IEnumerable<FinalSettlement>> GetAllFinalSettlements();
        Task<FinalSettlement> GetFinalSettlementsByemployeeInfoId(int employeeInfoId);
        Task<FinalSettlementDataViewModel> GetFinalSettlementDataByEmpId(int employeeInfoId);
        #endregion
    }
}
