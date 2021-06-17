using OPUSERP.Areas.POS.Models;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales.Interfaces

{
   public interface IOSalesCollectionDetailsService
    {
        Task<bool> SaveSalesCollectionDetail(SalesCollectionDetail salesCollection);
        Task<IEnumerable<SalesCollectionDetail>> GetAllSalesCollectionDetail();
        Task<SalesCollectionDetail> GetSalesCollectionDetailById(int id);
        Task<bool> DeleteSalesCollectionDetailById(int id);

        Task<IEnumerable<SalesCollectionDetail>> GetAllSalesCollectionDetailByCollectionId(int id);
        Task<IEnumerable<BillReturnPaymentDetail>> GetAllBillReturnPaymentDetailByMasterId(int id);

    }
}
