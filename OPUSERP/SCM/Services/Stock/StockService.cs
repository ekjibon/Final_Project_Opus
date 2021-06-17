using OPUSERP.Areas.SCMStock.Models;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Services.Stock.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.SCM.Data.Entity.Stock;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.Areas.Inventory.Models;

namespace OPUSERP.SCM.Services.Stock
{
    public class StockService : IStockService
    {
        private readonly ERPDbContext _context;

        public StockService(ERPDbContext context)
        {
            _context = context;
        }

        #region Stock Master
        public async Task<int> SaveStockMaster(StockMaster stockMaster)
        {
            if (stockMaster.Id != 0)
            {
                _context.StockMasters.Update(stockMaster);
                //_context.Entry(stockMaster).State = EntityState.Modified;
            }
            else
            {
                _context.StockMasters.Add(stockMaster);
            }

            await _context.SaveChangesAsync();
            return stockMaster.Id;
        }

        public async Task<IEnumerable<StockItemViewModel>> GetDueStockPurchaseDetailsInvoiceList(int Id)
        {
            List<StockItemViewModel> data = new List<StockItemViewModel>();
            IEnumerable<PurchaseOrderDetails> purchaseOrderDetails = await _context.PurchaseOrderDetails.Where(x => x.purchaseId == Id).Include(x => x.cSDetail.requisitionDetail.itemSpecification.Item).ToListAsync();
            foreach (PurchaseOrderDetails purchasedetail in purchaseOrderDetails)
            {
                var totalquantity = purchasedetail.poQty;
                var stockDue = _context.StockDetails.Where(x => x.orderDetailsId == purchasedetail.Id).Sum(s => s.grQty);
                var Due = totalquantity - stockDue;
                if (Due > 0)
                {
                    data.Add(new StockItemViewModel
                    {
                        Id = purchasedetail.Id,
                        itemSpecificationId = purchasedetail?.cSDetail?.requisitionDetail?.itemSpecificationId,
                        quantity = purchasedetail?.poQty,
                        dueQuantity = Due,
                        rate = purchasedetail.poRate,
                        itemName = purchasedetail?.cSDetail?.requisitionDetail?.itemSpecification?.Item?.itemName,
                        itemSpecificationName = purchasedetail?.cSDetail?.requisitionDetail?.itemSpecification?.specificationName,
                    });
                }
            }
            return data;
        }
        public async Task<IEnumerable<StockMaster>> GetAllStockMaster(int StatusId)
        {
            if (StatusId == 1)
            {
                List<StockMaster> lstdata = new List<StockMaster>();
                IEnumerable<StockMaster> lststock = await _context.StockMasters.Where(x => x.stockStatusId == StatusId).AsNoTracking().ToListAsync();
                List<int> stockids = lststock.Where(x => x.stockStatusId == StatusId).Select(x => x.Id).ToList();
                IEnumerable<StockDetails> Stocks = await _context.StockDetails.Where(x => stockids.Contains((int)x.stockMasterId)).Include(x => x.purchaseOrdersDetail.purchase.relSupplierCustomerResourse.organizationName).Include(x => x.transferDetail.transferMaster.storeFrom).ToListAsync();
                foreach (StockMaster data in lststock)
                {
                    var xdata = Stocks.Where(x => x.stockMasterId == data.Id).FirstOrDefault();
                    if (xdata == null)
                    {
                        lstdata.Add(new StockMaster
                        {
                            Id = data.Id,
                            companyId = data.companyId,
                            StockDate = data.StockDate,
                            MRNO = data.MRNO,
                            remarks = data.remarks,
                            stockStatusId = data.stockStatusId,
                            storeId = data.storeId,
                            SupplierName = xdata?.purchaseOrdersDetail?.purchase?.relSupplierCustomerResourse?.organizationName
                        });
                    }
                    else
                    {
                        lstdata.Add(new StockMaster
                        {
                            Id = data.Id,
                            companyId = data.companyId,
                            StockDate = data.StockDate,
                            MRNO = data.MRNO,
                            remarks = data.remarks,
                            stockStatusId = data.stockStatusId,
                            storeId = data.storeId,
                            SupplierName = xdata?.transferDetail?.transferMaster?.storeFrom?.storeName
                        });
                    }

                }
                return lstdata;
            }
            else
            {
                return await _context.StockMasters.Where(x => x.stockStatusId == StatusId).AsNoTracking().ToListAsync();
            }
        }
        public async Task<IEnumerable<StockMaster>> GetStockMasterbyType(int type)
        {
            try
            {
                IEnumerable<StockMaster> stockMasters = await _context.StockMasters.Where(x => x.stockStatusId == type).AsNoTracking().ToListAsync();

                return stockMasters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StockMaster>> GetStockMasterbyTypeForProd(int type,int? storeId)
        {
            try
            {
                IEnumerable<StockMaster> stockMasters = await _context.StockMasters.Where(x => x.stockStatusId == type && x.storeId==storeId).AsNoTracking().ToListAsync();

                return stockMasters;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeletestockByPOId(int poId)
        {
            _context.StockDetails.RemoveRange(_context.StockDetails.Where(x => x.stockMaster.purchaseId == poId));
            await _context.SaveChangesAsync();
            _context.StockMasters.RemoveRange(_context.StockMasters.Where(x => x.purchaseId == poId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletestockByStockMasterId(int stockMasterId)
        {
            _context.StockDetails.RemoveRange(_context.StockDetails.Where(x => x.stockMasterId == stockMasterId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockItemViewModel>> GetDueStockIOUDetailsInvoiceList(int Id)
        {
            List<StockItemViewModel> data = new List<StockItemViewModel>();
            IEnumerable<IOUDetails> iOUDetails = await _context.IOUDetails.Where(x => x.IOUId == Id).Include(x => x.requisitionDetail.itemSpecification.Item).ToListAsync();
            foreach (IOUDetails iouDetail in iOUDetails)
            {
                var totalquantity = iouDetail.qty;
                var stockDue = _context.StockDetails.Where(x => x.iOUDetailsId == iouDetail.Id).Sum(s => s.grQty);
                var Due = totalquantity - stockDue;
                if (Due > 0)
                {
                    data.Add(new StockItemViewModel
                    {
                        Id = iouDetail.Id,
                        itemSpecificationId = iouDetail?.requisitionDetail?.itemSpecificationId,
                        quantity = iouDetail?.qty,
                        dueQuantity = Due,
                        rate = iouDetail.rate,
                        itemName = iouDetail?.requisitionDetail?.itemSpecification?.Item?.itemName,
                        itemSpecificationName = iouDetail?.requisitionDetail?.itemSpecification?.specificationName,
                    });
                }
            }
            return data;
        }



        public async Task<IEnumerable<StockMaster>> GetStockMaster()
        {
            return await _context.StockMasters.AsNoTracking().Include(x => x.purchase.supplier).ToListAsync();
        }

        public async Task<StockMaster> GetStockMasterById(int id)
        {
            return await _context.StockMasters.AsNoTracking().Include(x => x.purchase.cSMaster.requisition).Where(x => x.Id == id).FirstOrDefaultAsync();
        }
        public async Task<StockMaster> GetStockInMasterById(int Id)
        {
            return await _context.StockMasters.Where(x => x.Id == Id && x.stockStatusId == 1).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StockMaster>> GetStockMasterByReceiveType(string type, string userName)
        {
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userid = user.userId;
            return await _context.StockMasters.AsNoTracking().Where(x => x.receiveType == type && x.userId == userid).Include(x => x.purchase.supplier).Include(x => x.IOU).ToListAsync();
        }

        public async Task<bool> DeleteStockMasterByMasterId(int id)
        {
            _context.StockMasters.Remove(_context.StockMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<StockMaster> GetStockOutMasterById(int Id)
        {
            return await _context.StockMasters.Where(x => x.Id == Id && x.stockStatusId == 2).AsNoTracking().FirstOrDefaultAsync();

        }

        public async Task<StockMaster> GetStockOutForProductionMasterById(int Id)
        {
            return await _context.StockMasters.Include(x=>x.productionRequsition).Where(x => x.Id == Id && x.stockStatusId == 2).AsNoTracking().FirstOrDefaultAsync();

        }

        public async Task<string> GetGRNumber()
        {
            var sale = await _context.StockMasters.AsNoTracking().ToListAsync();
            int Cpurchase = 0;
            Cpurchase = sale.Count();
            string idate = DateTime.Now.Year.ToString();
            string issueNo = "GR" + '/' + idate + '/' + (Cpurchase + 1);
            return issueNo;
        }

        #endregion

        #region Stock Detail
        public async Task<int> SaveStock(StockDetails stock)
        {
            try
            {
                if (stock.Id != 0)
                {
                    _context.StockDetails.Update(stock);
                }
                else
                {
                    _context.StockDetails.Add(stock);
                }

                await _context.SaveChangesAsync();
                return stock.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StockDetails>> GetStockDetails()
        {
            return await _context.StockDetails.Include(x => x.itemSpecification.Item).Include(x => x.stockMaster).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<StockDetails>> GetStockDetailsbyspecid(int Id)
        {
            return await _context.StockDetails.Include(x => x.stockMaster).AsNoTracking().Where(x => x.itemSpecificationId == Id).ToListAsync();
        }
        public async Task<StockDetails> GetStockDetailsById(int Id)
        {
            return await _context.StockDetails.AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<StockDetails> GetStockBymasterId(int masterId)
        {
            return await _context.StockDetails.Include(x => x.itemSpecification.Item).Include(x => x.stockMaster).AsNoTracking().Where(x => x.stockMasterId == masterId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StockDetails>> GetStockDetailsByMasterId(int id)
        {
            return await _context.StockDetails.Include(x => x.stockMaster.purchase.cSMaster.requisition.project).Include(x => x.stockMaster.purchase.supplier).Include(x => x.stockMaster.IOU.project).Include(x => x.orderDetails.cSDetail.requisitionDetail.itemSpecification.Item.unit).Include(x => x.iOUDetails.requisitionDetail.itemSpecification.Item.unit).Where(x => x.stockMasterId == id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<StockDetails>> GetAllStockByMasterId(int Id)
        {
            return await _context.StockDetails.AsNoTracking().Include(x => x.itemSpecification.Item).Include(x => x.orderDetails.purchase.supplier).Include(x => x.salesInvoiceDetail.salesInvoiceMaster.relSupplierCustomerResourse.Leads).Where(x => x.stockMasterId == Id).ToListAsync();
        }

        public async Task<IEnumerable<StockDetails>> GetAllStockForProductionReqByMasterId(int Id)
        {
            return await _context.StockDetails.AsNoTracking().Include(x => x.itemSpecification.Item).Include(x => x.productionRequsitionDetails.productionRequsitionMaster).Where(x => x.stockMasterId == Id).ToListAsync();
        }

        public async Task<IEnumerable<StockDetails>> GetAllStockOutBySalesMaster(int Id)
        {
            return await _context.StockDetails.AsNoTracking().Include(x => x.itemSpecification).Include(x => x.salesInvoiceDetail).Where(x => x.salesInvoiceDetail.salesInvoiceMasterId == Id).ToListAsync();
        }


        public async Task<StockDetails> GetstockMasterIdBytransferMasterId(int Id)
        {
            return await _context.StockDetails.AsNoTracking().Include(x => x.transferDetail).Where(x => x.transferDetail.transferMasterId == Id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<StockDetails>> GetAllStockInByPurchaseMaster(int Id)
        {
            return await _context.StockDetails.AsNoTracking().Include(x => x.itemSpecification).Include(x => x.orderDetails).Where(x => x.orderDetails.purchaseId == Id).ToListAsync();
        }

        public async Task<IEnumerable<StockBalanceViewModel>> InventoryReport(int? projectId, DateTime? fromDate, DateTime? toDate)
        {
            var result = _context.stockBalanceViewModels.FromSql($"spInventoryReport {projectId}, {fromDate}, {toDate}").ToListAsync();
            return await result;
        }

        public async Task<StockDetails> GetstockMasterByitemSpecificationId(int itemSpecificationId)
        {
            return await _context.StockDetails.AsNoTracking().Where(x => x.itemSpecificationId == itemSpecificationId).OrderByDescending(x => x.Id).FirstOrDefaultAsync();
        }


        public async Task<decimal> OnHanStockBySpecIdForTransfer(int id)
        {
            IEnumerable<StockDetails> stocks = await _context.StockDetails.Include(x => x.stockMaster).Include(x => x.purchaseOrdersDetail).Include(x => x.itemSpecification.Item).Include(x => x.transferDetail).ToListAsync();

            decimal stockin = stocks.Where(x => x.stockMaster.stockStatusId == 1 && x.itemSpecificationId == id).Sum(x => (decimal)x.grQty);
            decimal stockOut = stocks.Where(x => x.stockMaster.stockStatusId == 2 && x.itemSpecificationId == id).Sum(x => (decimal)x.grQty);
            decimal stockReturn = stocks.Where(x => x.stockMaster.stockStatusId == 3 && x.itemSpecificationId == id).Sum(x => (decimal)x.grQty);
            decimal onHand = 0;
            onHand = stockin + stockReturn - stockOut;

            return onHand;
        }

        public async Task<bool> DeleteStockMasterById(int id)
        {
            _context.StockDetails.RemoveRange(_context.StockDetails.Where(x => x.stockMasterId == id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatesingleStock(int id, int masterId, decimal qty)
        {
            StockDetails stock = await _context.StockDetails.FindAsync(id);
            if (stock != null)
            {
                stock.qty = qty;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }


        #endregion

        #region reqitem
        public async Task<IEnumerable<ItemReqMaster>> GettemReqMaster()
        {
            return await _context.ItemReqMasters.AsNoTracking().ToListAsync();
        }
        public async Task<ItemReqMaster> GettemReqMasterbyId(int Id)
        {
            return await _context.ItemReqMasters.Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }



        public async Task<bool> DeleteItemReqMasterbyId(int Id)
        {
            _context.ItemReqMasters.Remove(_context.ItemReqMasters.Where(x => x.Id == Id).FirstOrDefault());
            return 1 == await _context.SaveChangesAsync();
        }



        public async Task<int> SaveItemReqMaster(ItemReqMaster stockMaster)
        {
            try
            {
                if (stockMaster.Id != 0)
                {
                    stockMaster.updatedBy = stockMaster.createdBy;
                    stockMaster.updatedAt = DateTime.Now;
                    _context.ItemReqMasters.Update(stockMaster);
                }
                else
                {
                    stockMaster.createdAt = DateTime.Now;
                    _context.ItemReqMasters.Add(stockMaster);
                }

                await _context.SaveChangesAsync();
                return stockMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region reqitem detail
        public async Task<IEnumerable<ItemReqDetails>> GettemReqDetail()
        {
            return await _context.ItemReqDetails.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ItemReqDetails>> GettemReqDetailbymasterId(int Id)
        {
            return await _context.ItemReqDetails.Where(x => x.itemReqMasterId == Id).AsNoTracking().ToListAsync();
        }
        public async Task<ItemReqDetails> GettemReqDetailbyId(int Id)
        {
            return await _context.ItemReqDetails.Where(x => x.Id == Id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<StockBalancesViewModel>> GetStockBalanceViewModels(int tid, int? itemspecid, DateTime? fromdate, DateTime? todate)
        {
            List<StockBalancesViewModel> stockBalanceViewModels = new List<StockBalancesViewModel>();
            IEnumerable<StockDetails> stocks = await _context.StockDetails.Include(x => x.stockMaster).Include(x => x.itemSpecification.Item).Where(x => x.itemSpecification.itemId == tid).ToListAsync();
            if (itemspecid != 0)
            {
                stocks = stocks.Where(x => x.itemSpecificationId == itemspecid).ToList();
            }
            decimal? stockinBalance = stocks.Where(x => x.stockMaster.StockDate < fromdate && x.stockMaster.stockStatusId == 1 || x.stockMaster.stockStatusId == 3).Sum(x => x.grQty);
            decimal? stockoutBalance = stocks.Where(x => x.stockMaster.StockDate < fromdate && x.stockMaster.stockStatusId == 2).Sum(x => x.qty);
            IEnumerable<StockDetails> stockin = stocks.Where(x => x.stockMaster.StockDate >= fromdate && x.stockMaster.StockDate <= todate && x.stockMaster.stockStatusId == 1 || x.stockMaster.stockStatusId == 3).ToList();
            IEnumerable<StockDetails> stockout = stocks.Where(x => x.stockMaster.StockDate >= fromdate && x.stockMaster.StockDate <= todate && x.stockMaster.stockStatusId == 2).ToList();
            stockBalanceViewModels.Add(new StockBalancesViewModel
            {
                stockinBalance = stockinBalance,
                stockOutBalance = stockoutBalance,
                stocksIn = stockin,
                stocksout = stockout

            });

            return stockBalanceViewModels;

        }

        public async Task<IEnumerable<StockSummaryViewModel>> GetStockSummary(string FDate, string TDate)
        {
            return await _context.stockSummaryViewModels.FromSql($"RPT_GRStockBalanceSummary {Convert.ToDateTime(FDate).ToString("yyyyMMdd")}, {Convert.ToDateTime(TDate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<StockBalanceByItemViewModel>> GetStockBalanceByItem(int? itemspecid)
        {
            return await _context.stockBalanceByItemViewModels.FromSql($"RPT_StockBalanceByItem {itemspecid}").AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteItemReqDetailbyId(int Id)
        {
            _context.ItemReqDetails.Remove(_context.ItemReqDetails.Where(x => x.Id == Id).FirstOrDefault());
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteItemReqDetailbymasterId(int Id)
        {
            _context.ItemReqDetails.RemoveRange(_context.ItemReqDetails.Where(x => x.itemReqMasterId == Id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }



        public async Task<int> SaveItemReqDetail(ItemReqDetails stockMaster)
        {
            try
            {
                if (stockMaster.Id != 0)
                {
                    stockMaster.updatedBy = stockMaster.createdBy;
                    stockMaster.updatedAt = DateTime.Now;
                    _context.ItemReqDetails.Update(stockMaster);
                }
                else
                {
                    stockMaster.createdAt = DateTime.Now;
                    _context.ItemReqDetails.Add(stockMaster);
                }

                await _context.SaveChangesAsync();
                return stockMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region OpeningStock
        public async Task<int> SaveOpeningStock(OpeningStock stock)
        {
            try
            {
                if (stock.Id != 0)
                {
                    _context.OpeningStocks.Update(stock);
                }
                else
                {
                    _context.OpeningStocks.Add(stock);
                }

                await _context.SaveChangesAsync();
                return stock.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<OpeningStock>> GetOpeningStockAll()
        {
            return await _context.OpeningStocks.AsNoTracking().Include(x => x.itemSpecification.Item).ToListAsync();
        }

        public async Task<OpeningStock> GetOpeningStockById(int id)
        {
            return await _context.OpeningStocks.AsNoTracking().Include(x => x.itemSpecification.Item).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteOpeningStockById(int id)
        {
            _context.OpeningStocks.Remove(_context.OpeningStocks.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
