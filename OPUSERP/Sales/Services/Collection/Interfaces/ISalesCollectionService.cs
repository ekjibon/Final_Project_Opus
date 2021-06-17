

using OPUSERP.Areas.Sales.Models.NotMapped;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Sales.Data.Entity.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Sales.Services.Collection.Interfaces
{
   public interface ISalesCollectionService
    {
        #region SalesCollection
        Task<int> SaveSalesCollection(SalesCollection salesCollection);
        Task<IEnumerable<SalesCollection>> GetAllSalesCollection();
        Task<SalesCollection> GetSalesCollectionById(int id);
        Task<bool> DeleteSalesCollectionById(int id);

       Task<IEnumerable<CustomerWithDue>> GetCustomerWithDue();
        Task<IEnumerable<Leads>> GetDuesCustomerList();
        Task<IEnumerable<Leads>> GetCollectionCustomerList();
        Task<IEnumerable<Leads>> GetCustomerListForSalesReport();
        #endregion
        #region RepSalesCollection
        Task<int> SaveRepSalesCollection(RepSalesCollection salesCollection);
        Task<IEnumerable<RepSalesCollection>> GetAllRepSalesCollection();
        Task<RepSalesCollection> GetRepSalesCollectionById(int id);
        Task<bool> DeleteRepSalesCollectionById(int id);
        #endregion
    }
}
