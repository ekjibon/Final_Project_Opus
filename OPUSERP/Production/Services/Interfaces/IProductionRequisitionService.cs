using OPUSERP.Areas.Inventory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Production.Data.Entity;

namespace OPUSERP.Production.Services.Interfaces
{
    public interface IProductionRequisitionService
    {
        Task<int> SaveProductionRequsitionMaster(ProductionRequsitionMaster productionRequsitionMaster);
        Task<IEnumerable<ProductionRequsitionMaster>> GetProductionRequsitionMaster();
        Task<ProductionRequsitionMaster> GetProductionRequsitionMasterById(int Id);
        Task<bool> DeleteProductionRequsitionMasterById(int Id);
        Task<bool> UpdateProductionRequsitionMasterStockCloseById(int id);

        Task<int> SaveProductionRequsitionDetails(ProductionRequsitionDetails productionRequsitionDetails);
        Task<IEnumerable<ProductionRequsitionDetails>> GetProductionRequsitionDetails();
        Task<IEnumerable<ProductionRequsitionMaster>> GetAllProductionRequsitionMasterForStockOut();
        Task<ProductionRequsitionDetails> GetProductionRequsitionDetailsById(int Id);
        Task<IEnumerable<ProductionRequsitionDetails>> GetProductionRequsitionDetailsByMasterId(int masterId);
        Task<bool> DeleteProductionRequsitionDetailsById(int Id);
        Task<bool> DeleteProductionRequsitionDetailsByMasterId(int masterId);
        Task<IEnumerable<StockItemViewModel>> GetDueStockoutDetailsInvoiceList(int Id);
    }
}
