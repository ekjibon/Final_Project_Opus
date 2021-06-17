using OPUSERP.POS.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS.Interfaces
{
   public interface IPosCollectionDetailService
    {
        Task<int> SavePosCollectionDetail(PosCollectionDetail posCollectionDetail);
        Task<IEnumerable<PosCollectionDetail>> GetPosCollectionDetail();
        Task<PosCollectionDetail> GetPosCollectionDetailById(int id);
        Task<bool> DeletePosCollectionDetailById(int id);
    }
}
