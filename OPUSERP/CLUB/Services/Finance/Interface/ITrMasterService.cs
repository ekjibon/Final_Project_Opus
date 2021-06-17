using OPUSERP.CLUB.Data.Finance;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Finance.Interface
{
    public interface ITrMasterService
    {
        Task<int> SaveTrMaster(TrMaster trMaster);
        Task<IEnumerable<TrMaster>> GetTrMaster();
        Task<IEnumerable<TrMaster>> GetTrMasterByEmpId(int empId);
        Task<IEnumerable<TrMaster>> GetPendingTrMaster();
        Task<TrMaster> GetTrMasterById(int id);
        Task<bool> DeleteTrMasterById(int id);
    }
}
