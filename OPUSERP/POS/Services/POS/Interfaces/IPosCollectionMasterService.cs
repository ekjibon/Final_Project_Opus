using OPUSERP.POS.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS.Interfaces
{
   public interface IPosCollectionMasterService
    {
        Task<int> SavePosCollectionMaster(PosCollectionMaster posCollectionMaster);
        Task<IEnumerable<PosCollectionMaster>> GetPosCollectionMaster();
        Task<PosCollectionMaster> GetPosCollectionMasterById(int id);
        Task<bool> DeletePosCollectionMasterById(int id);
        Task<int> SaveBillReturnPaymentDetail(PosBillReturnPaymentDetail billPaymentDetail);
        Task<int> SaveReturnBillPaymentMaster(PosBillReturnPaymentMaster billPaymentMaster);
        Task<PosBillReturnPaymentMaster> GetBillReturnPaymentbyMasterId(int Id);
        Task<IEnumerable<PosBillReturnPaymentMaster>> GetBillReturnPaymentMaster();
    }
}
