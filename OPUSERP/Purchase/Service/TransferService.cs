using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Data;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.Purchase.Service.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Purchase.Service
{
    public class TransferService: ITransferService
    {
        private readonly ERPDbContext _context;

        public TransferService(ERPDbContext context)
        {
            _context = context;
        }

        #region TransferMaster
        public async Task<int> SaveTransferMaster(TransferMaster transferMaster)
        {
            try
            {
                if (transferMaster.Id != 0)
                {
                    transferMaster.updatedBy = transferMaster.createdBy;
                    transferMaster.updatedAt = DateTime.Now;
                    _context.TransferMasters.Update(transferMaster);
                }
                else
                {
                    transferMaster.createdAt = DateTime.Now;
                    _context.TransferMasters.Add(transferMaster);
                }

                await _context.SaveChangesAsync();
                return transferMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<TransferMaster>> GetAllTransferMaster()
        {
            return await _context.TransferMasters.AsNoTracking().Include(x => x.storeFrom).Include(x => x.storeTo).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<IEnumerable<TransferMaster>> GetAllTransferMasterbyStoreId()
        {
            return await _context.TransferMasters.Where(x => x.isStockClose == 0).AsNoTracking().Include(x => x.storeFrom).Include(x => x.storeTo).OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<IEnumerable<TransferMaster>> GetAllTransferMasterByUser(string Username)
        {
            return await _context.TransferMasters.AsNoTracking().Include(x => x.storeFrom).Include(x => x.storeTo).Where(x => x.createdBy == Username).OrderByDescending(x => x.Id).ToListAsync();
        }
        public async Task<TransferMaster> GetAllTransferMasterByMasterId(int Id)
        {
            return await _context.TransferMasters.AsNoTracking().Include(x => x.storeFrom).Include(x => x.storeTo).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTransferMasterById(int id)
        {
            _context.TransferMasters.Remove(_context.TransferMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateTransferMasterStockCloseById(int id)
        {
            TransferMaster purchaseOrderMaster = await _context.TransferMasters.FindAsync(id);
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
        public async Task<bool> UpdateTransferMasterStockOpenById(int id)
        {
            TransferMaster purchaseOrderMaster = await _context.TransferMasters.FindAsync(id);
            if (purchaseOrderMaster != null)
            {
                purchaseOrderMaster.isStockClose = 0;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        #endregion

        #region Transfer Detail
        public async Task<int> SaveTransferDetail(TransferDetail transferDetail)
        {
            try
            {
                if (transferDetail.Id != 0)
                {
                    transferDetail.updatedBy = transferDetail.createdBy;
                    transferDetail.updatedAt = DateTime.Now;
                    _context.TransferDetails.Update(transferDetail);
                }
                else
                {
                    transferDetail.createdAt = DateTime.Now;
                    _context.TransferDetails.Add(transferDetail);
                }

                await _context.SaveChangesAsync();
                return transferDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StockItemViewModel>> GetDueStockTransferDetailsInvoiceList(int Id)
        {
            List<StockItemViewModel> data = new List<StockItemViewModel>();
            IEnumerable<TransferDetail> purchaseOrderDetails = await _context.TransferDetails.Where(x => x.transferMasterId == Id).Include(x => x.itemSpecification.Item).ToListAsync();
            foreach (TransferDetail purchasedetail in purchaseOrderDetails)
            {
                var totalquantity = purchasedetail.qty;
                var stockDue = _context.StockDetails.Where(x => x.transferDetailId == purchasedetail.Id).Sum(s => s.grQty);
                var Due = totalquantity - stockDue;
                if (Due > 0)
                {

                    data.Add(new StockItemViewModel

                    {
                        Id = purchasedetail.Id,
                        purchaseId = purchasedetail?.transferMasterId,
                        itemSpecificationId = purchasedetail.itemSpecificationId,
                        description = "",
                        deliveryLoacationId = 0,
                        quantity = purchasedetail?.qty,
                        dueQuantity = Due,
                        rate = purchasedetail.rate,
                        currencyId = 0,
                        vatPercent = 0,
                        taxPercent = 0,
                        itemName = purchasedetail?.itemSpecification?.Item?.itemName,
                        itemSpecificationName = purchasedetail?.itemSpecification?.specificationName,
                        PONO = purchasedetail.transferMaster?.transferNO,
                        unitName = purchasedetail.itemSpecification?.Item?.unit?.unitName

                    });
                }
            }



            return data;
        }
        public IQueryable<TransferDetail> GetAllTransferDetailsBytransferMasterId(int transferMasterId)
        {
            return _context.TransferDetails.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.transferMasterId == transferMasterId);
        }

        //public async Task<IEnumerable<TransferDetail>> GetAllTransferDetailsBytransferMasterId(int transferMasterId)
        //{

        //    var data = await _context.TransferDetails.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.transferMasterId == transferMasterId).ToListAsync();

        //    List<TransferDetail> lstdetail = new List<TransferDetail>();

        //    foreach (TransferDetail d in data)
        //    {
        //        ItemSpecification spec = _context.ItemSpecifications.Where(x => x.Id == d.itemSpecificationId).FirstOrDefault();
        //        IEnumerable<SpecificationDetail> details = _context.SpecificationDetails.Where(x => x.itemSpecificationId == spec.Id).Include(x => x.specificationCategory);
        //        string specName = "";
        //        int len = details.Count();
        //        int i = 0;
        //        foreach (SpecificationDetail specdetail in details)
        //        {
        //            if (specName == "")
        //            {
        //                if (i + 1 == len)
        //                {
        //                    specName = spec.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
        //                }
        //                else
        //                {
        //                    specName = spec.specificationName + "(" + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
        //                }


        //            }
        //            else
        //            {
        //                if (i + 1 == len)
        //                {
        //                    specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value;
        //                }
        //                else
        //                {
        //                    specName = specName + specdetail.specificationCategory.specificationCategoryName + ":" + specdetail.value + ",";
        //                }



        //            }
        //            i += 1;

        //        }
        //        specName = specName + ")";



        //        lstdetail.Add(new TransferDetail
        //        {
        //            transferMasterId = d.transferMasterId,
        //            transferMaster = d.transferMaster,
        //            itemSpecificationId = d.itemSpecificationId,
        //            specName = specName,
        //            itemSpecification = d.itemSpecification,

        //            qty = d.qty,
        //            rate = d.rate



        //        });

        //    }


        //    return lstdetail;
        //}

        public async Task<TransferDetail> GetAllTransferDetailsById(int Id)
        {
            return await _context.TransferDetails.AsNoTracking().Include(x => x.itemSpecification.Item.unit).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTransferDetailById(int id)
        {
            _context.TransferDetails.Remove(_context.TransferDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteTransferDetailsBytransferMasterId(int transferMasterId)
        {
            _context.TransferDetails.RemoveRange(_context.TransferDetails.Where(x => x.transferMasterId == transferMasterId));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

    }
}
