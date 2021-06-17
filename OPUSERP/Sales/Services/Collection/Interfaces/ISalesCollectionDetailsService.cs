
using OPUSERP.Sales.Data.Entity.Collection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Services.Collection.Interface
{
   public interface ISalesCollectionDetailsService
    {
        #region Sales Collection Details
        Task<bool> SaveSalesCollectionDetail(SalesCollectionDetail salesCollection);
        Task<IEnumerable<SalesCollectionDetail>> GetAllSalesCollectionDetail();
        Task<SalesCollectionDetail> GetSalesCollectionDetailById(int id);
        Task<bool> DeleteSalesCollectionDetailById(int id);

        Task<IEnumerable<SalesCollectionDetail>> GetAllSalesCollectionDetailByCollectionId(int id);
        #endregion
        #region Rep Sales Collection Details
        Task<bool> SaveRepSalesCollectionDetail(RepSalesCollectionDetail salesCollectionDetail);
        Task<IEnumerable<RepSalesCollectionDetail>> GetAllRepSalesCollectionDetail();
        Task<IEnumerable<RepSalesCollectionDetail>> GetAllRepSalesCollectionDetailByCollectionId(int id);
        Task<RepSalesCollectionDetail> GetRepSalesCollectionDetailById(int id);
        Task<bool> DeleteRepSalesCollectionDetailById(int id);
        #endregion



    }
}
