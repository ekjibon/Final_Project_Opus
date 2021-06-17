using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.PurchaseOrder.Interface
{
    public interface IPODismissService
    {
        Task<int> SavePODismissMaster(PODismissMaster pODismissMaster);
        Task<IEnumerable<PODismissMaster>> GetPODismissMaster();

        Task<int> SavePODismissDetails(PODismissDetail pODismissDetail);
        Task<IEnumerable<PODismissDetail>> GetPODismissDetailsByMaster(int masterId);
        Task<IEnumerable<PurchaseOrderDismissViewModel>> GetPOForDismisses(string userName);
    }
}
