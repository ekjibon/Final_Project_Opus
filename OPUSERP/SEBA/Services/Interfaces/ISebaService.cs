using OPUSERP.CRM.Data.SPModel;
using OPUSERP.SEBA.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SEBA.Services.Interfaces
{
    public interface ISebaService
    {
        #region CustomerProductWarranty

        Task<bool> SaveCustomerProductWarranty(CustomerProductWarranty customerProductWarranty);
        Task<IEnumerable<CustomerProductWarranty>> GetAllCustomerProductWarranty();
        Task<IEnumerable<CustomerProductWarranty>> GetCustomerProductListByLeadId(int leadId);
        Task<CustomerProductWarranty> GetCustomerProductWarrantyById(int id);
        Task<bool> DeleteCustomerProductWarrantyById(int id);

        #endregion

        #region ProblemMaster

        Task<int> SaveProblemMaster(ProblemMaster problemMaster);
        Task<IEnumerable<ProblemMaster>> GetAllProblemMaster();
        Task<ProblemMaster> GetProblemMasterById(int id);
        Task<bool> DeleteProblemMasterById(int id);
        Task<IEnumerable<System.Object>> GetProblemsList();

        #endregion

        #region ProblemDetail

        Task<bool> SaveProblemDetail(ProblemDetail problemDetail);
        Task<IEnumerable<ProblemDetail>> GetProblemDetailByMasterId(int? masterId);
        Task<bool> DeleteProblemDetailById(int masterId);

        #endregion
    }
}
