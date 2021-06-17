using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Purchase.Models.NotMapped;
using OPUSERP.Data;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.Purchase.Service.Interfaces;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Purchase.Service
{
    public class BillPaymentService: IBillPaymentService
    {
        private readonly ERPDbContext _context;

        public BillPaymentService(ERPDbContext context)
        {
            _context = context;
        }

        #region purchase Return

        public async Task<int> SavePurchaseReturnInfoMaster(PurchaseReturnInfoMaster billPaymentMaster)
        {
            try
            {
                if (billPaymentMaster.Id != 0)
                {
                    billPaymentMaster.updatedBy = billPaymentMaster.createdBy;
                    billPaymentMaster.updatedAt = DateTime.Now;
                    _context.purchaseReturnInfoMasters.Update(billPaymentMaster);
                }
                else
                {
                    billPaymentMaster.createdAt = DateTime.Now;
                    _context.purchaseReturnInfoMasters.Add(billPaymentMaster);
                }

                await _context.SaveChangesAsync();
                return billPaymentMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<PurchaseReturnInfoMaster> GetPurchaseReturnInfoMasterById(int Id)
        {
            return await _context.purchaseReturnInfoMasters.Where(x => x.Id == Id).Include(x => x.relSupplierCustomerResourse).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PurchaseReturnInfoMaster>> GetPurchaseReturnInfoMaster()
        {
            return await _context.purchaseReturnInfoMasters.AsNoTracking().Include(x => x.relSupplierCustomerResourse).ToListAsync();
        }

        public async Task<int> SavePurchaseReturnInfoDetail(PurchaseReturnInfoDetail billPaymentMaster)
        {
            try
            {
                if (billPaymentMaster.Id != 0)
                {
                    billPaymentMaster.updatedBy = billPaymentMaster.createdBy;
                    billPaymentMaster.updatedAt = DateTime.Now;
                    _context.purchaseReturnInfoDetails.Update(billPaymentMaster);
                }
                else
                {
                    billPaymentMaster.createdAt = DateTime.Now;
                    _context.purchaseReturnInfoDetails.Add(billPaymentMaster);
                }

                await _context.SaveChangesAsync();
                return billPaymentMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<PurchaseReturnInfoDetail>> GetPurchaseReturnInfoDetailByMasterId(int Id)
        {
            return await _context.purchaseReturnInfoDetails.Where(x=>x.purchaseReturnInfoMasterId==Id).AsNoTracking().Include(x => x.purchaseReturnInfoMaster).Include(x => x.purchaseOrdersDetail.itemSpecification.Item.unit).ToListAsync();
        }

        #endregion

        #region BillReceiveInformation

        public async Task<int> SaveBillReceiveInformation(BillReceiveInformation billPaymentMaster)
        {
            try
            {
                if (billPaymentMaster.Id != 0)
                {
                    billPaymentMaster.updatedBy = billPaymentMaster.createdBy;
                    billPaymentMaster.updatedAt = DateTime.Now;
                    _context.billReceiveInformation.Update(billPaymentMaster);
                }
                else
                {
                    billPaymentMaster.createdAt = DateTime.Now;
                    _context.billReceiveInformation.Add(billPaymentMaster);
                }

                await _context.SaveChangesAsync();
                return billPaymentMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> UpdateBillReceiveInformationById(int id)
        {
            BillReceiveInformation purchaseOrderMaster = await _context.billReceiveInformation.FindAsync(id);
            if (purchaseOrderMaster != null)
            {
                purchaseOrderMaster.ispaid = 1;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        public async Task<IEnumerable<BillReceiveInformation>> GetBillReceiveInformation()
        {
            return await _context.billReceiveInformation.AsNoTracking().Include(x => x.relSupplierCustomerResourse).ToListAsync();
        }

        public async Task<IEnumerable<BillReceiveInformation>> GetBillReceiveInformationByInvoiceId(int Id)
        {
            return await _context.billReceiveInformation.Where(x=>x.purchaseOrderMasterId==Id).AsNoTracking().Include(x => x.relSupplierCustomerResourse).ToListAsync();
        }

        public async Task<BillReceiveInformation> GetBillReceiveInformationById(int Id)
        {
            return await _context.billReceiveInformation.Where(x => x.Id == Id).Include(x => x.paymentMode).Include(x => x.relSupplierCustomerResourse).FirstOrDefaultAsync();
        }

        #endregion

        #region BillPaymentMaster
        public async Task<int> SaveBillPaymentMaster(BillPaymentMaster billPaymentMaster)
        {
            try
            {
                if (billPaymentMaster.Id != 0)
                {
                    billPaymentMaster.updatedBy = billPaymentMaster.createdBy;
                    billPaymentMaster.updatedAt = DateTime.Now;
                    _context.BillPaymentMasters.Update(billPaymentMaster);
                }
                else
                {
                    billPaymentMaster.createdAt = DateTime.Now;
                    _context.BillPaymentMasters.Add(billPaymentMaster);
                }

                await _context.SaveChangesAsync();
                return billPaymentMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        

        public async Task<IEnumerable<BillPaymentMaster>> GetBillPaymentMaster()
        {
            return await _context.BillPaymentMasters.AsNoTracking().Include(x => x.relSupplierCustomerResourse).ToListAsync();
        }
       

        public async Task<BillPaymentMaster> GetBillPaymentMasterById(int Id)
        {
            return await _context.BillPaymentMasters.Where(x => x.Id == Id).Include(x => x.company).Include(x=>x.paymentMode).Include(x => x.relSupplierCustomerResourse).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteBillPaymentMasterById(int id)
        {
            _context.BillPaymentMasters.Remove(_context.BillPaymentMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Organization>> GetDueSupplierList()
        {
            List<int?> relSupplierIds = await _context.PurchaseOrderMasters.Where(x => x.isClose == 0).Select(x => x.supplierId).ToListAsync();

            return await _context.Organizations.Where(x => relSupplierIds.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

       

        public async Task<IEnumerable<SupplierWithDue>> GetSupplierWithDue()
        {
            List<int?> relSupplierIds = await _context.PurchaseOrderMasters.Where(x => x.isClose == 0).Select(x => x.supplierId).ToListAsync();

            IEnumerable<Organization> relSupplierCustomerResourses = await _context.Organizations.Where(x => relSupplierIds.Contains(x.Id)).AsNoTracking().ToListAsync();

            List<SupplierWithDue> data = new List<SupplierWithDue>();
            foreach (Organization relSupplierCustomerResourse in relSupplierCustomerResourses)
            {
                var haveToPay = _context.PurchaseOrderMasters.Where(x => x.supplierId == relSupplierCustomerResourse.Id).Sum(s => s.netTotal);

                var collection = _context.BillPaymentMasters.Where(x => x.relSupplierCustomerResourseId == relSupplierCustomerResourse.Id).Sum(s => s.totalAmount);

                var Due = haveToPay - collection;
                if (Due > 0)
                {
                    data.Add(new SupplierWithDue
                    {
                        relSupplierCustomerResourse = relSupplierCustomerResourse,
                        haveToPay = haveToPay,
                        paid = collection,
                        due = Due,
                    });
                }

            }
            return data;
        }
        

        #endregion

        #region BillPaymentDetails
        public async Task<int> SaveBillPaymentDetail(BillPaymentDetail billPaymentDetail)
        {
            try
            {
                if (billPaymentDetail.Id != 0)
                {
                    billPaymentDetail.updatedBy = billPaymentDetail.createdBy;
                    billPaymentDetail.updatedAt = DateTime.Now;
                    _context.BillPaymentDetails.Update(billPaymentDetail);
                }
                else
                {
                    billPaymentDetail.createdAt = DateTime.Now;
                    _context.BillPaymentDetails.Add(billPaymentDetail);
                }

                await _context.SaveChangesAsync();
                return billPaymentDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }        

        public async Task<IEnumerable<BillPaymentDetail>> GetBillPaymentDetailByMasterId(int MasterId)
        {
            return await _context.BillPaymentDetails.AsNoTracking().Where(x => x.billPaymentMasterId == MasterId).Include(x => x.billReceiveInformation.purchaseOrderMaster).ToListAsync();
        }

        public async Task<BillPaymentDetail> GetBillPaymentDetailById(int Id)
        {
            return await _context.BillPaymentDetails.FindAsync(Id);
        }

        public async Task<bool> DeleteBillPaymentDetailById(int id)
        {
            _context.BillPaymentDetails.Remove(_context.BillPaymentDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteBillPaymentDetailByTMId(int TMId)
        {
            _context.BillPaymentDetails.RemoveRange(_context.BillPaymentDetails.Where(x => x.billPaymentMasterId == TMId));
            return 1 == await _context.SaveChangesAsync();
        }        

        #endregion

    }
}
