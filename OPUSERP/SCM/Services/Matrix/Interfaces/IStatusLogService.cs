using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Data.Entity.Matrix;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix.Interfaces
{
    public interface IStatusLogService
    {
        Task<int> SaveStatusLog(StatusLog statusLog);
        Task<IEnumerable<StatusLog>> GetStatusLogList();
        Task<StatusLog> GetStatusLogById(int id);
        Task<bool> DeleteStatusLogById(int id);
        Task<IEnumerable<StatusLogVM>> GetStatusLogListByReqId(int reqId);
        Task<IEnumerable<StatusLog>> GetStatusLogListbyreqsid(int Id);
    }
}
