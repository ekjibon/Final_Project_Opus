using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Report.Interface
{
   public interface IRequisition
    {
        Task<IEnumerable<RequisitionDetail>> GetRequisitionDetails();
        Task<IEnumerable<ItemSpecification>> GetItemSpecifications();
       // Task<IEnumerable<StockDetails>> GetStockDetails();
    }
}
