using OPUSERP.Data.Entity.Matrix;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix.Interfaces
{
    public interface IStatusInfoService
    {
        // Unit
        Task<bool> SaveStatusInfo(StatusInfo statusInfo);
        Task<IEnumerable<StatusInfo>> GetAllStatusInfo();
        Task<StatusInfo> GetStatusInfoById(int id);
        Task<bool> DeleteStatusInfoById(int id);
    }
}
