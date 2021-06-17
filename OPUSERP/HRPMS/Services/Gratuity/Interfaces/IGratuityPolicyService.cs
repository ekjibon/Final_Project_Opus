using OPUSERP.HRPMS.Data.Entity.Gratuity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.Gratuity.Interfaces
{
    public interface IGratuityPolicyService
    {
        Task<IEnumerable<GratuityPolicyMaster>> GetGratuityPolicyMaster();
        Task<IEnumerable<GratuityPolicyDetail>> GetGratuityPolicyDetail();
        Task<IEnumerable<GratuityPolicyDetail>> GetGratuityPolicyDetailByBranchId(int? branchId);
        Task<int> SaveGratuityPolicyMaster(GratuityPolicyMaster gratuityPolicyMaster);
        Task<bool> SaveGratuityPolicyDetail(GratuityPolicyDetail gratuityPolicyDetail);
        Task<bool> DeleteGratuityPolicyDetailByMasterId(int id);
        Task<GratuityPolicyMaster> GetGratuityPolicyMasterBybranch(int branch);


    }
}
