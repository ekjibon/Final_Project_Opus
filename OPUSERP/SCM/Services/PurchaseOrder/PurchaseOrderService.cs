using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Areas.Purchase.Models.NotMapped;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Data;
using OPUSERP.Models;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.Matrix;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.PurchaseOrder
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly ERPDbContext _context;

        public PurchaseOrderService(ERPDbContext context)
        {
            _context = context;
        }

        #region Master data Get

        public async Task<IEnumerable<Currency>> GetCurrency()
        {
            return await _context.Currencies.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DeliveryLocation>> GetDeliveryLocation()
        {
            return await _context.DeliveryLocations.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DeliveryMode>> GetDeliveryMode()
        {
            return await _context.DeliveryModes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PaymentMode>> GetPaymentMode()
        {
            return await _context.PaymentModes.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PaymentTerms>> GetpaymentTerms()
        {
            return await _context.PaymentTerms.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CardType>> GetAllCardType()
        {
            return await _context.CardTypes.AsNoTracking().ToListAsync();
        }

        public async Task<decimal> GetDiscountRateForSales()
        {
            return await _context.DiscountRates.OrderByDescending(x => x.Id).Select(x => (decimal)x.rate).FirstOrDefaultAsync();
        }

        #endregion

        #region PurchaseOrder Master

        public AutoNumberViewModel GetPuerchaseOrderNo(int reqId)
        {
            try
            {
                string day = DateTime.Now.Day.ToString();
                string month = "0" + DateTime.Now.Month.ToString();
                string year = DateTime.Now.Year.ToString();
                string poDate = day + "/" + month + "/" + year;
                string reqDept = string.Empty;
                string poNo = string.Empty;
                string countPO = "0000";
                var reqInfo = _context.RequisitionMasters.Include(x => x.project).Where(x => x.Id == reqId).FirstOrDefault();
                reqDept = reqInfo.project.projectShortName;
                if (reqDept == string.Empty)
                    reqDept = reqInfo.reqDept;

                var poInfo = _context.PurchaseOrderMasters.Where(x => Convert.ToDateTime(x.poDate).Month == DateTime.Now.Month).LastOrDefault();

                if (poInfo != null)
                {
                    string pocnt = poInfo.poNo.Substring(poInfo.poNo.Length - 12, 12);
                    int x = Convert.ToInt32(pocnt.Substring(0, 4));
                    int y = Convert.ToInt32(x) + 1;
                    string z = countPO.Substring(countPO.Length - 4) + y;
                    countPO = z.Substring(Math.Max(0, z.Length - 4));
                }
                else
                {
                    countPO = "0001";
                }

                poNo = "PO/" + reqDept + "/" + countPO + "/" + month.Substring(month.Length - 2, 2) + "/" + year;

                AutoNumberViewModel model = new AutoNumberViewModel
                {
                    autoNumber = poNo,
                    dept = reqDept
                };
                return model;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        public async Task<int> SavePurchaseOrderMaster(PurchaseOrderMaster purchaseOrderMaster)
        {
            try
            {
                if (purchaseOrderMaster.Id != 0)
                {
                    _context.PurchaseOrderMasters.Update(purchaseOrderMaster);
                }
                else
                {
                    _context.PurchaseOrderMasters.Add(purchaseOrderMaster);
                }

                await _context.SaveChangesAsync();
                return purchaseOrderMaster.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
           
        }

        public async Task<bool> UpdatePurchaseOrderMasterStockCloseById(int id)
        {
            PurchaseOrderMaster purchaseOrderMaster = await _context.PurchaseOrderMasters.FindAsync(id);
            if (purchaseOrderMaster != null)
            {
                purchaseOrderMaster.isStockClose = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        //public async Task<IEnumerable<PurchaseOrderMaster>> GetPurchaseOrderMasterForGR(string userName)
        //{
        //    List<int?> purchaseId = _context.StockMasters.Select(x => x.purchaseId).ToList();

        //    var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        //    int userid = user.userId;
        //    return await _context.PurchaseOrderMasters.Include(x=>x.cSMaster).Include(x=>x.supplier).Where(x=>x.cSMaster.requisition.reqBy== userid && !purchaseId.Contains(x.Id)).AsNoTracking().ToListAsync();
        //}

        public async Task<IEnumerable<PurchaseOrderMaster>> GetPurchaseOrderMasterForGR(string userName)
        {
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userid = user.userId;
            var result =await (from p in _context.PurchaseOrderMasters
                          join d in (from pd in _context.PurchaseOrderDetails
                                     group pd by pd.purchaseId into dd
                                     select new { purchaseId = dd.Key, poQty =dd.Sum(x => x.poQty) }) on p.Id equals d.purchaseId
                          join c in _context.CSMasters.Where(x => x.requisition.reqBy == userid) on p.csMasterId equals c.Id
                          join su in _context.Organizations on p.supplierId equals su.Id
                          join g in (from sm in _context.StockMasters
                                     join sd in _context.StockDetails on sm.Id equals sd.stockMasterId
                                     group new { sm, sd } by new { sm.purchaseId } into ss
                                     select new { ss.Key.purchaseId, grQty = ss.Sum(x => x.sd.grQty) }) on p.Id equals g.purchaseId into gg
                          from gr in gg.DefaultIfEmpty()
                          where d.poQty > (gr.grQty==null?0:gr.grQty)
                               select new PurchaseOrderMaster
                          {
                              poNo = p.poNo,
                              poDate = p.poDate,
                              csMasterId = p.csMasterId,
                              cSMaster = c,
                              supplierId = p.supplierId,
                              supplier=su,
                              deliveryDate = p.deliveryDate,
                              deliveryMode = p.deliveryMode,
                              deliveryModeId = p.deliveryModeId,
                              poStatus = p.poStatus,
                              poType = p.poType,
                              poValue = p.poValue,
                              printStatus = p.printStatus,
                              savingAmount = p.savingAmount,
                              taxAmount = p.taxAmount,
                              totalAmount = p.totalAmount,
                              vatAmount = p.vatAmount,
                              vatPercent = p.vatPercent,
                              billToLocation = p.billToLocation,
                              receiveStatus = p.receiveStatus,
                              Id = p.Id,
                              isClose = p.isClose,
                              isCustomise = p.isCustomise,
                              isStockClose = p.isStockClose,
                              netTotal = p.netTotal,
                              paymentDate = p.paymentDate,
                              paymentMode = p.paymentMode,
                              paymentModeId = p.paymentModeId,
                              paymentTerms = p.paymentTerms,
                              paymentTermsId = p.paymentTermsId,
                              rfqRef = p.rfqRef,
                              savingPercent = p.savingPercent,
                              remarks = p.remarks,
                              taxPercent = p.taxPercent,
                              userId = p.userId

                          }).AsNoTracking().ToListAsync();

            return result;
        }

        public async Task<IEnumerable<PurchaseOrderMaster>> GetPurchaseOrderMaster(string userId)
        {
            var result = await (from p in _context.PurchaseOrderMasters
                                join c in _context.CSMasters on p.csMasterId equals c.Id
                                join s in _context.Organizations on p.supplierId equals s.Id
                                join pd in (from pdd in _context.PurchaseOrderDetails
                                            group pdd by pdd.purchaseId into g
                                            select new { purchaseId = g.Key, poValue = g.Sum(x => x.poRate * x.poQty) }) on p.Id equals pd.purchaseId
                                join phl in _context.PrintHistories on p.Id equals phl.purchaseId into phg
                                from ph in phg.DefaultIfEmpty()
                                join u in _context.Users on p.userId equals u.userId
                                where u.UserName == userId && Convert.ToInt32(ph.printStatus)==0
                                select new PurchaseOrderMaster
                                {
                                    Id = p.Id,
                                    poDate = p.poDate,
                                    poNo = p.poNo,
                                    billToLocation = p.billToLocation,
                                    poValue = pd.poValue,
                                    supplierId = p.supplierId,
                                    csMasterId = p.csMasterId,
                                    printStatus=ph.printStatus,
                                    cSMaster = c,
                                    supplier = s

                                }).AsNoTracking().ToListAsync();
            return result;
            //return await _context.PurchaseOrderMasters.Include(x=>x.cSMaster).Include(x=>x.supplier).AsNoTracking().ToListAsync();
        }
        
        public async Task<IEnumerable<PurchaseOrderMaster>> GetIssuedPurchaseOrderMaster(string userId)
        {
            var result = await (from p in _context.PurchaseOrderMasters
                                join c in _context.CSMasters on p.csMasterId equals c.Id
                                join s in _context.Organizations on p.supplierId equals s.Id
                                join pd in (from pdd in _context.PurchaseOrderDetails
                                            group pdd by pdd.purchaseId into g
                                            select new { purchaseId = g.Key, poValue = g.Sum(x => x.poRate * x.poQty) }) on p.Id equals pd.purchaseId
                                join phl in _context.PrintHistories on p.Id equals phl.purchaseId into phg
                                from ph in phg.DefaultIfEmpty()
                                join u in _context.Users on p.userId equals u.userId
                                where u.UserName == userId && Convert.ToInt32(ph.printStatus) == 1
                                select new PurchaseOrderMaster
                                {
                                    Id = p.Id,
                                    poDate = p.poDate,
                                    poNo = p.poNo,
                                    billToLocation = p.billToLocation,
                                    poValue = pd.poValue,
                                    supplierId = p.supplierId,
                                    csMasterId = p.csMasterId,
                                    printStatus = ph.printStatus,
                                    cSMaster = c,
                                    supplier = s

                                }).AsNoTracking().ToListAsync();
            return result;
            //return await _context.PurchaseOrderMasters.Include(x=>x.cSMaster).Include(x=>x.supplier).AsNoTracking().ToListAsync();
        }

        public async Task<PurchaseOrderMaster> GetPurchaseOrderMasterById(int id)
        {
            return await _context.PurchaseOrderMasters.Where(x=>x.Id==id).Include(x => x.supplier).Include(x => x.cSMaster.requisition).Include(x => x.supplier).FirstOrDefaultAsync();
        }       

        public async Task<IEnumerable<PurchaseOrderMaster>> GetPurchaseOrderMasterAll()
        {
            return await _context.PurchaseOrderMasters.AsNoTracking().Include(x => x.supplier).OrderByDescending(x => x.Id).ToListAsync();           
        }

        public async Task<IEnumerable<PurchaseOrderMaster>> GetDueStockPurchaseInvoiceList()
        {
            IEnumerable<PurchaseOrderMaster> purchaseOrderMasters = await _context.PurchaseOrderMasters.Where(x => x.isStockClose == 0).AsNoTracking().Include(x => x.supplier).ToListAsync();
            return purchaseOrderMasters;
        }

        public async Task<bool> DeletePurchaseOrderMasterById(int id)
        {
            _context.PurchaseOrderMasters.Remove(_context.PurchaseOrderMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region PurchaseOrderDetails
        public async Task<int> SavePurchaseOrderDetails(PurchaseOrderDetails purchaseOrderDetails)
        {
            if (purchaseOrderDetails.Id != 0)
            {
                purchaseOrderDetails.updatedBy = purchaseOrderDetails.createdBy;
                purchaseOrderDetails.updatedAt = DateTime.Now;
                _context.PurchaseOrderDetails.Update(purchaseOrderDetails);
            }
            else
            {
                purchaseOrderDetails.createdAt = DateTime.Now;
                _context.PurchaseOrderDetails.Add(purchaseOrderDetails);
            }

            await _context.SaveChangesAsync();
            return purchaseOrderDetails.Id;
        }

       

        public async Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetails()
        {
            return await _context.PurchaseOrderDetails.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetailsbyspecId(int Id)
        {
            return await _context.PurchaseOrderDetails.Include(x => x.purchase).Include(x => x.cSDetail.supplier).Include(x => x.cSDetail.requisitionDetail.itemSpecification.Item.unit).Include(x=>x.cSDetail.requisitionDetail.requisitionMaster).Where(x=>x.cSDetail.requisitionDetail.itemSpecificationId==Id).AsNoTracking().OrderByDescending(x=>x.Id).Take(3).ToListAsync();
        }
        public async Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetailsbyspecIdA(int Id)
        {
            return await _context.PurchaseOrderDetails.Include(x => x.purchase).Include(x => x.cSDetail.supplier).Include(x => x.cSDetail.requisitionDetail.itemSpecification.Item.unit).Include(x=>x.cSDetail.requisitionDetail.requisitionMaster).Where(x=>x.cSDetail.requisitionDetail.itemSpecificationId==Id).AsNoTracking().OrderByDescending(x=>x.Id).ToListAsync();
        }

        public async Task<IEnumerable<StockItemViewModel>> GetDueStockPurchaseDetailsInvoiceList(int Id)
        {
            List<StockItemViewModel> data = new List<StockItemViewModel>();
            IEnumerable<PurchaseOrderDetails> purchaseOrderDetails = await _context.PurchaseOrderDetails.Where(x => x.purchaseId == Id).Include(x => x.itemSpecification.Item).ToListAsync();
            foreach (PurchaseOrderDetails purchasedetail in purchaseOrderDetails)
            {
                var totalquantity = purchasedetail.poQty;
                var stockDue = _context.StockDetails.Where(x => x.purchaseOrdersDetailId == purchasedetail.Id).Sum(s => s.grQty);
                var Due = totalquantity - stockDue;
                if (Due > 0)
                {
                    data.Add(new StockItemViewModel

                    {
                        Id = purchasedetail.Id,
                        purchaseId = purchasedetail?.purchaseId,
                        itemSpecificationId = purchasedetail.itemSpecificationId,
                        description = purchasedetail?.description,
                        quantity = purchasedetail?.poQty,
                        dueQuantity = Due,
                        rate = purchasedetail.poRate,
                        currencyId = purchasedetail?.currencyId,
                        vatPercent = purchasedetail?.vatPercent,
                        taxPercent = purchasedetail?.taxPercent,
                        itemName = purchasedetail?.itemSpecification?.Item?.itemName,
                        itemSpecificationName = purchasedetail?.itemSpecification?.specificationName,
                        PONO = purchasedetail.purchase?.poNo,
                        unitName = purchasedetail.itemSpecification?.Item?.unit?.unitName
                    });
                }
            }
            return data;
        }

        public async Task<IEnumerable<PurchaseInvoiceWithDue>> GetDuePurchaseInvoiceByCustomerId(int customerId)
        {
            IEnumerable<PurchaseOrderMaster> purchaseOrderMasters = await _context.PurchaseOrderMasters.Where(x => x.isClose == 0 && x.supplierId == customerId).AsNoTracking().ToListAsync();

            List<PurchaseInvoiceWithDue> data = new List<PurchaseInvoiceWithDue>();
            foreach (PurchaseOrderMaster purchaseOrderMaster in purchaseOrderMasters)
            {
                var totalAmount = purchaseOrderMaster.netTotal;
                var collectionDue = _context.BillPaymentDetails.Where(x => x.billReceiveInformation.purchaseOrderMasterId == purchaseOrderMaster.Id).Sum(s => s.amount);
                var Due = totalAmount - collectionDue;
                if (Due > 0)
                {
                    data.Add(new PurchaseInvoiceWithDue
                    {
                        purchaseOrderMaster = purchaseOrderMaster,
                        due = Due,
                    });
                }
            }
            return data;
        }

        public async Task<IEnumerable<PurchaseOrderRPTViewModel>> GetRPTPurchaseOrderDetailsByMasterId(int poId)
        {
            var result =await _context.PurchaseOrderRPTViewModels.FromSql($"SCM.SP_rptPurchaseOrder_Combined {poId}").ToListAsync();
            return result;
        }

        public async Task<IEnumerable<PurchaseOrderDetails>> GetPurchaseOrderDetailsByMasterId(int id)
        {
            return await _context.PurchaseOrderDetails.Where(x=>x.Id==id).AsNoTracking().ToListAsync();
        }

        public async Task<PurchaseOrderDetails> GetPurchaseOrderDetailsById(int id)
        {
            return await _context.PurchaseOrderDetails.FindAsync(id);
        }

        public IQueryable<PurchaseOrderDetails> GetPurchaseOrderDetailByPOId(int poId)
        {
            return _context.PurchaseOrderDetails.Include(x => x.itemSpecification.Item.unit).Where(x => x.purchaseId == poId);
        }

        public Task<PurchaseOrderDetails> GetPurchaseOrderDetailById(int poId)
        {
            return _context.PurchaseOrderDetails.Include(x => x.itemSpecification.Item.unit).Where(x => x.Id == poId).FirstOrDefaultAsync();
        }

        public async Task<bool> DeletePurchaseOrderDetailsById(int id)
        {
            _context.PurchaseOrderDetails.Remove(_context.PurchaseOrderDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePurchaseOrderDetailsByMasterId(int id)
        {
            _context.PurchaseOrderDetails.RemoveRange(_context.PurchaseOrderDetails.Where(x=>x.Id==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePurchaseOrderDetailsByPurchaseId(int purchaseId)
        {
            _context.PurchaseOrderDetails.RemoveRange(_context.PurchaseOrderDetails.Where(x => x.purchaseId == purchaseId).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region POTermAndCondition
        public async Task<int> SavePOTermAndCondition(POTermAndCondition pOTermAndCondition)
        {
            if (pOTermAndCondition.Id != 0)
            {
                pOTermAndCondition.updatedBy = pOTermAndCondition.createdBy;
                pOTermAndCondition.updatedAt = DateTime.Now;
                _context.POTermAndConditions.Update(pOTermAndCondition);
            }
            else
            {
                pOTermAndCondition.createdAt = DateTime.Now;
                _context.POTermAndConditions.Add(pOTermAndCondition);
            }

            await _context.SaveChangesAsync();
            return pOTermAndCondition.Id;
        }

        public async Task<IEnumerable<POTermAndCondition>> GetPOTermAndCondition()
        {
            return await _context.POTermAndConditions.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<POTermAndCondition>> GetPOTermAndConditionByMasterId(int id)
        {
            return await _context.POTermAndConditions.Where(x=>x.Id==id).AsNoTracking().ToListAsync();
        }

        public async Task<POTermAndCondition> GetPOTermAndConditionById(int id)
        {
            return await _context.POTermAndConditions.FindAsync(id);
        }

        public async Task<bool> DeletePOTermAndConditionById(int id)
        {
            _context.POTermAndConditions.Remove(_context.POTermAndConditions.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePOTermAndConditionByMasterId(int id)
        {
            _context.POTermAndConditions.RemoveRange(_context.POTermAndConditions.Where(x=>x.Id==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
               
        #region Print History
        public async Task<bool> SavePintHistory(PrintHistory printHistory)
        {
            PrintHistory poPrintHistory = new PrintHistory();
            if (printHistory.matrixTypeId == 1)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == printHistory.matrixTypeId && x.requisitionId == printHistory.requisitionId).FirstOrDefaultAsync();
                if (poPrintHistory != null)
                {
                    poPrintHistory.printStatus = printHistory.printStatus;
                    printHistory = poPrintHistory;
                    _context.PrintHistories.Update(printHistory);
                }
                else
                {
                    _context.PrintHistories.Add(printHistory);
                }
            }
            else if (printHistory.matrixTypeId == 2)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == printHistory.matrixTypeId && x.purchaseId == printHistory.purchaseId).FirstOrDefaultAsync();
                if (poPrintHistory != null)
                {
                    poPrintHistory.printStatus = printHistory.printStatus;
                    printHistory = poPrintHistory;
                    _context.PrintHistories.Update(printHistory);
                }
                else
                {
                    _context.PrintHistories.Add(printHistory);
                }
            }
            else if (printHistory.matrixTypeId == 4)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == printHistory.matrixTypeId && x.stockId == printHistory.stockId).FirstOrDefaultAsync();
                if (poPrintHistory != null)
                {
                    poPrintHistory.printStatus = printHistory.printStatus;
                    printHistory = poPrintHistory;
                    _context.PrintHistories.Update(printHistory);
                }
                else
                {
                    _context.PrintHistories.Add(printHistory);
                }
            }
            else if (printHistory.matrixTypeId == 6)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == printHistory.matrixTypeId && x.iOUId == printHistory.iOUId).FirstOrDefaultAsync();
                if (poPrintHistory != null)
                {
                    poPrintHistory.printStatus = printHistory.printStatus;
                    printHistory = poPrintHistory;
                    _context.PrintHistories.Update(printHistory);
                }
                else
                {
                    _context.PrintHistories.Add(printHistory);
                }
            }
            else if (printHistory.matrixTypeId == 7)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == printHistory.matrixTypeId && x.iOUId == printHistory.iOUId).FirstOrDefaultAsync();
                if (poPrintHistory != null)
                {
                    poPrintHistory.printStatus = printHistory.printStatus;
                    printHistory = poPrintHistory;
                    _context.PrintHistories.Update(printHistory);
                }
                else
                {
                    _context.PrintHistories.Add(printHistory);
                }
            }

            //if (poPrintHistory.Id != 0)
            //{
            //    _context.PrintHistories.Update(printHistory);
            //}
            //else
            //{
            //    _context.PrintHistories.Add(printHistory);
            //}

            return 1== await _context.SaveChangesAsync();
           
        }

        public async Task<PrintHistory> GetPrintHistoryById(int matrixTypeId,int id)
        {
            PrintHistory poPrintHistory = new PrintHistory();
            if (matrixTypeId == 1)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == matrixTypeId && x.requisitionId == id).FirstOrDefaultAsync();
            }
            else if (matrixTypeId == 2)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == matrixTypeId && x.purchaseId == id).FirstOrDefaultAsync();
               
            }
            else if (matrixTypeId == 4)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == matrixTypeId && x.stockId == id).FirstOrDefaultAsync();
            }
            else if (matrixTypeId == 6)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == matrixTypeId && x.iOUId == id).FirstOrDefaultAsync();
            }
            else if (matrixTypeId == 7)
            {
                poPrintHistory = await _context.PrintHistories.Where(x => x.matrixTypeId == matrixTypeId && x.iOUPaymentMasterId == id).FirstOrDefaultAsync();
            }
            return poPrintHistory;
        }


        #endregion

    }
}
