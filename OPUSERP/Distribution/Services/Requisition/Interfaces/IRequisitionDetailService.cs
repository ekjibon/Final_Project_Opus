using OPUSERP.Areas.POS.Models;
using OPUSERP.Distribution.Data.Entity.Requisition;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.Requisition.Interfaces
{
    public interface IRequisitionDetailService
    {
        #region ReturnSaleDetail
        Task<bool> SaveRequisitionDetail(RequisitionDetail requisitionDetail);
        Task<IEnumerable<RequisitionDetail>> GetAllRequisitionDetail();
        Task<IEnumerable<RequisitionDetail>> GetAllRequisitionDetailsByMasterId(int masterId);
        Task<RequisitionDetail> GetRequisitionDetailById(int id);

        Task<bool> DeleteRequisitionDetailById(int id);
        Task<bool> DeleteRequisitionDetailByMasterId(int masterId);
        #endregion
      
    }
}
