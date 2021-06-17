using OPUSERP.Areas.Inventory.Models;
using OPUSERP.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Production.Services.Interfaces;

namespace OPUSERP.Production.Services
{
    public class ProductionRequisitionService: IProductionRequisitionService
    {
        private readonly ERPDbContext _context;

        public ProductionRequisitionService(ERPDbContext context)
        {
            _context = context;
        }

        #region Production Requsition Master
        public async Task<int> SaveProductionRequsitionMaster(ProductionRequsitionMaster productionRequsitionMaster)
        {
            try
            {
                if (productionRequsitionMaster.Id != 0)
                {
                    productionRequsitionMaster.updatedBy = productionRequsitionMaster.createdBy;
                    productionRequsitionMaster.updatedAt = DateTime.Now;
                    _context.productionRequsitionMasters.Update(productionRequsitionMaster);
                }
                else
                {
                    productionRequsitionMaster.createdAt = DateTime.Now;
                    _context.productionRequsitionMasters.Add(productionRequsitionMaster);
                }

                await _context.SaveChangesAsync();
                return productionRequsitionMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductionRequsitionMaster>> GetAllProductionRequsitionMasterForStockOut()
        {
            return await _context.productionRequsitionMasters.Include(x=>x.productionPlan.bOMMaster).AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<ProductionRequsitionMaster>> GetProductionRequsitionMaster()
        {
            return await _context.productionRequsitionMasters.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ProductionRequsitionMaster> GetProductionRequsitionMasterById(int Id)
        {
            return await _context.productionRequsitionMasters.Include(x=>x.productionPlan).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProductionRequsitionMasterById(int Id)
        {
            _context.productionRequsitionMasters.Remove(_context.productionRequsitionMasters.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateProductionRequsitionMasterStockCloseById(int id)
        {
            ProductionRequsitionMaster productionRequsitionMaster = await _context.productionRequsitionMasters.FindAsync(id);
            if (productionRequsitionMaster != null)
            {
                productionRequsitionMaster.isStockClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        #endregion

        #region Production Requsition Details
        public async Task<int> SaveProductionRequsitionDetails(ProductionRequsitionDetails productionRequsitionDetails)
        {
            try
            {
                if (productionRequsitionDetails.Id != 0)
                {
                    productionRequsitionDetails.updatedBy = productionRequsitionDetails.createdBy;
                    productionRequsitionDetails.updatedAt = DateTime.Now;
                    _context.productionRequsitionDetails.Update(productionRequsitionDetails);
                }
                else
                {
                    productionRequsitionDetails.createdAt = DateTime.Now;
                    _context.productionRequsitionDetails.Add(productionRequsitionDetails);
                }

                await _context.SaveChangesAsync();
                return productionRequsitionDetails.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<ProductionRequsitionDetails>> GetProductionRequsitionDetails()
        {
            return await _context.productionRequsitionDetails.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<ProductionRequsitionDetails> GetProductionRequsitionDetailsById(int Id)
        {
            return await _context.productionRequsitionDetails.Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<ProductionRequsitionDetails>> GetProductionRequsitionDetailsByMasterId(int masterId)
        {
            return await _context.productionRequsitionDetails.Include(x=>x.BOMDetail.bOMMaster).Include(x => x.BOMDetail.itemSpecification.Item.unit).Where(x => x.productionRequsitionMasterId == masterId).ToListAsync();
        }

        public async Task<bool> DeleteProductionRequsitionDetailsById(int Id)
        {
            _context.productionRequsitionDetails.Remove(_context.productionRequsitionDetails.Find(Id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteProductionRequsitionDetailsByMasterId(int masterId)
        {
            _context.productionRequsitionDetails.RemoveRange(_context.productionRequsitionDetails.Where(x=>x.productionRequsitionMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockItemViewModel>> GetDueStockoutDetailsInvoiceList(int Id)
        {
            List<StockItemViewModel> data = new List<StockItemViewModel>();
            IEnumerable<ProductionRequsitionDetails> productionRequsitionDetails = _context.productionRequsitionDetails.Where(x => x.productionRequsitionMasterId == Id).Include(x => x.BOMDetail.itemSpecification.Item);
            foreach (ProductionRequsitionDetails productionRequsitionDetail in productionRequsitionDetails)
            {
                var totalquantity = productionRequsitionDetail.requsitionQuantity;
                var stockDue = _context.StockDetails.Where(x => x.productionRequsitionDetailsId == productionRequsitionDetail.Id).Sum(s => s.qty);
                var Due = totalquantity - stockDue;
                if (Due > 0)
                {
                    data.Add(new StockItemViewModel
                    {
                        Id = productionRequsitionDetail.Id,
                        purchaseId = productionRequsitionDetail?.productionRequsitionMasterId,
                        itemSpecificationId = productionRequsitionDetail?.BOMDetail?.itemSpecificationId,
                        description = "",
                        deliveryLoacationId = 1,
                        quantity = productionRequsitionDetail?.requsitionQuantity,
                        dueQuantity = Due,
                        rate = productionRequsitionDetail?.BOMDetail?.rate,
                        currencyId = 1,
                        vatPercent = 0,
                        taxPercent = 0,
                        itemName = productionRequsitionDetail?.BOMDetail?.itemSpecification?.Item?.itemName,
                        itemSpecificationName = productionRequsitionDetail?.BOMDetail?.itemSpecification?.specificationName,
                        PONO = productionRequsitionDetail?.productionRequsitionMaster?.requsitionNumber,
                        unitName = productionRequsitionDetail?.BOMDetail.itemSpecification?.Item?.unit?.unitName
                    });
                }
            }
            return data;
        }

        #endregion
    }
}
