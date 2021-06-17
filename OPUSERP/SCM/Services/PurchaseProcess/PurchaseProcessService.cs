using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Areas.SCMPurchaseProcess.Models;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Services.PurchaseProcess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.PurchaseProcess
{
    public class PurchaseProcessService : IPurchaseProcessService
    {
        private readonly ERPDbContext _context;

        public PurchaseProcessService(ERPDbContext context)
        {
            _context = context;
        }

        #region CS Master
        public async Task<int> SaveCSMaster(CSMaster cSMaster)
        {
            if (cSMaster.Id != 0)
            {
                _context.CSMasters.Update(cSMaster);
            }
            else
            {
                _context.CSMasters.Add(cSMaster);
            }

            await _context.SaveChangesAsync();
            return cSMaster.Id;
        }

        public void UpdateCSMaster(int id, int status)
        {
            var user = _context.CSMasters.Find(id);
            user.csStatus = status;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<CSMaster>> GetCSMasterList(int userId)
        {
            var result = await (from m in _context.CSMasters
                                join r in _context.RequisitionMasters on m.requisitionId equals r.Id
                                join cu in _context.Users on m.userId equals cu.userId
                                join pp in _context.employeeInfos on cu.Id equals pp.ApplicationUserId
                                join aa in (from a1 in _context.ApprovalLogs
                                            where a1.matrixTypeId==2
                                            group a1 by a1.masterId into g
                                            select new { masterId=g.Key, sequenceNo=g.Max(x=>x.sequenceNo)}) on m.Id equals aa.masterId
                                join a in _context.ApprovalLogs.Where(x => x.matrixTypeId == 2) on new {aa.masterId,aa.sequenceNo} equals new {a.masterId, a.sequenceNo}
                                join fap in _context.Users on a.userId equals fap.userId
                                join ap in _context.employeeInfos on fap.Id equals ap.ApplicationUserId
                                where m.userId == userId
                                select new CSMaster
                                {
                                    Id = m.Id,
                                    requisitionId=m.requisitionId,
                                    requisition = m.requisition,
                                    procurementPerson = pp.nameEnglish,
                                    csNo = m.csNo,
                                    csDate = m.csDate,
                                    approverName = ap.nameEnglish,
                                    rfqRefNo=m.rfqRefNo,
                                    
                                }).Distinct().ToListAsync();

            return result;

        }


        public async Task<IEnumerable<CSMaster>> GetCSMasterListForPO(int userId)
        {
            var result = await (from m in _context.CSMasters
                                join r in _context.RequisitionMasters on m.requisitionId equals r.Id
                                join cd in (from dd in _context.CSDetails  
                                group dd  by dd.cSMasterId into a
                                select new { cSMasterId=a.Key,qty=a.Sum(x=>x.qty)}) on m.Id equals cd.cSMasterId into aa
                                join csd in (from cm in _context.PurchaseOrderMasters
                                             join cd in _context.PurchaseOrderDetails on cm.Id equals cd.purchaseId
                                             where cm.userId == userId
                                             group new { cm,cd }  by new { cm.csMasterId}   into cs
                                             select new { csMasterId=cs.Key.csMasterId, qty = cs.Sum(x=>x.cd.poQty) }) on m.Id equals csd.csMasterId into cos
                                from ccss in cos.DefaultIfEmpty()
                                    //join csd in (from pm in _context.PurchaseOrderMasters
                                    //             join pd in _context.PurchaseOrderDetails on pm.Id equals pd.purchaseId
                                    //             where pm.userId == userId
                                    //             group new { pm, pd } by new { pm.csMasterId, pd.cSDetailId } into cs
                                    //             select new { cs.Key.cSDetailId, cs.Key.csMasterId, qty = cs.Sum(x => x.pd.poQty) }) on m.Id equals csd.csMasterId into cos
                                    //from ccss in cos.DefaultIfEmpty()

                                where m.userId == userId && m.csStatus==3 && Convert.ToDecimal(ccss.qty) < Convert.ToDecimal(aa.Sum(x=>x.qty))
                                select new CSMaster
                                {
                                    Id = m.Id,
                                    requisitionId = m.requisitionId,
                                    requisition = m.requisition,
                                    csNo = m.csNo,
                                    csDate = m.csDate,
                                    rfqRefNo = m.rfqRefNo,

                                }).OrderBy(x => x.Id).ToListAsync();

            return result;
            
        }

        public async Task<IEnumerable<CSMaster>> GetCSListForApprove(int userId)
        {
            var result = await (from m in _context.CSMasters
                                join r in _context.RequisitionMasters on m.requisitionId equals r.Id
                                join pp in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on m.userId equals pp.UserId
                                join a in(from a1 in _context.ApprovalLogs.Where(x=>x.matrixTypeId==2) 
                                          group new { a1} by new {a1.masterId} into a11
                                          select new { masterId=a11.Key.masterId, sequenceNo = a11.Max(x=>x.a1.sequenceNo)} into g
                                          join a2 in _context.ApprovalLogs.Where(x=>x.matrixTypeId==2) on new {g.masterId,g.sequenceNo} equals new {a2.masterId,a2.sequenceNo}
                                          group new {g,a2} by new {a2.masterId,a2.userId,a2.sequenceNo,a2.isActive} into css
                                          select new {css.Key.userId,css.Key.masterId,css.Key.sequenceNo,css.Key.isActive}
                                          ) on m.Id equals a.masterId
                                join ap in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on a.userId equals ap.UserId
                                join al in _context.ApprovalLogs.Where(x => x.matrixTypeId == 2 && x.isActive==1) on m.Id equals al.masterId
                                where al.userId == userId
                                select new CSMaster
                                {
                                    Id = m.Id,
                                    requisition = m.requisition,
                                    procurementPerson = pp.EmpName,
                                    csNo = m.csNo,
                                    csDate = m.csDate,
                                    approverName = ap.EmpName
                                }).Distinct().ToListAsync();
            var lstresult = new List<CSMaster>();
            foreach (var data in result)
            {
                int count = lstresult.Where(x => x.Id == data.Id).Count();
                if(count==0)
                {
                    lstresult.Add(new CSMaster
                    {
                        Id = data.Id,
                        requisition = data.requisition,
                        procurementPerson = data.procurementPerson,
                        csNo = data.csNo,
                        csDate = data.csDate,
                        approverName = data.approverName

                    });

                }
              
            }
           // result = result.Distinct().ToList();
            return lstresult;
        }

      

        public async Task<IEnumerable<CSMaster>> GetCSMasterByReqId(int reqId)
        {
            return await _context.CSMasters.AsNoTracking().Where(x => x.requisitionId == reqId).ToListAsync();
        }

        public async Task<CSMaster> GetCSMasterById(int id)
        {
            return await _context.CSMasters.Include(x=>x.requisition).Include(x => x.procurementType).Include(x => x.procurementValue).Where(x=>x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteCSMasterById(int id)
        {
            _context.CSMasters.Remove(_context.CSMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region CS Details

        public async Task<int> SaveCSDetailsSingle(CSDetail cSDetail)
        {
            try
            {
                if (cSDetail.Id != 0)
                {
                    _context.CSDetails.Update(cSDetail);
                }
                else
                {
                    _context.CSDetails.Add(cSDetail);
                }

                await _context.SaveChangesAsync();
                return cSDetail.Id;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public void SaveCSDetails(List<CSDetail> cSDetails)
        {
           
            _context.CSDetails.AddRange(cSDetails);
            
            _context.SaveChanges();
           
        }

        public async Task<bool> UpdateCSDetailApproval(int? Id, decimal? qty, decimal? rate)
        {
            var csd = _context.CSDetails.Find(Id);
            csd.qty = qty;
            csd.rate = rate;
           
            _context.Entry(csd).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CSDetail>> GetCSDetailListByMasterId(int masterId)
        {
            return await _context.CSDetails.AsNoTracking().Where(x => x.cSMasterId == masterId).Include(x => x.supplier).ToListAsync();
        }

        public async Task<IEnumerable<CSSupplierVM>> GetCSSupplierListBycsId(int csId)
        {
            var result = await (from c in _context.CSDetails
                                join o in _context.Organizations on c.supplierId equals o.Id
                                join co in _context.Contacts on o.Id equals co.organizationId into cop
                                from cof in cop.DefaultIfEmpty().Take(1)
                                where c.cSMasterId == csId
                                group new { c, o, cof } by new { o.Id, o.orgCode, o.organizationName,cof.personName,contactNumber = cof.phoneNumber??cof.mobileNumber } into g

                                select new CSSupplierVM
                                {
                                    supplierId = g.Key.Id,
                                    organizationName = g.Key.organizationName,
                                    orgCode = g.Key.orgCode,
                                    total = g.Sum(x => x.c.qty * x.c.rate),
                                    contactNo=g.Key.contactNumber,
                                    personName=g.Key.personName
                                }).ToListAsync();


            return result;
        }

        public async Task<string> GetCSNumber()
        {
            int csCount = await _context.CSMasters.AsNoTracking().CountAsync();
            int nextNumber = csCount + 1;
            string csNumber = "CS-" + nextNumber;
            return csNumber;
        }

        public async Task<IEnumerable<CSItemVM>> GetCSDetailListBySupplierId(int supplierId)
        {
            var details = await _context.CSDetails
                .Where(x => x.supplierId == supplierId)
                .Include(x => x.requisitionDetail.itemSpecification.Item.unit).AsNoTracking().ToListAsync();

            List<CSItemVM> cSItemVMs = new List<CSItemVM>();
            foreach (var data in details)
            {
                var qnt = _context.PurchaseOrderDetails.Where(x => x.cSDetailId == data.Id).Sum(x => x.poQty);
                var unOrderQnt = data.qty - qnt;
                if (unOrderQnt!=0)
                {
                    cSItemVMs.Add(new CSItemVM
                    {
                        itemId = data.requisitionDetail.itemSpecification.Item.Id,
                        itemCode = data.requisitionDetail.itemSpecification.Item.itemCode,
                        itemName = data.requisitionDetail.itemSpecification.Item.itemName,
                        itemSpec = data.requisitionDetail.itemSpecification.specificationName,
                        unit = data.requisitionDetail.itemSpecification.Item.unit.unitName,
                        PRQnt = data.requisitionDetail.reqQty,
                        csDetailsId = data.Id,
                        qnt = data.qty,
                        UnorderQnt = unOrderQnt,
                        CSUnitPrice = data.rate
                    });
                }
            }
            return cSItemVMs;
        }

        public async Task<IEnumerable<CSItemVM>> GetCSDetailListByCSAndSupplierId(int csMasterId,int supplierId)
        {
            var details = await _context.CSDetails
                .Where(x => x.supplierId == supplierId && x.cSMasterId==csMasterId)
                .Include(x => x.requisitionDetail.itemSpecification.Item.unit).AsNoTracking().ToListAsync();

            List<CSItemVM> cSItemVMs = new List<CSItemVM>();
            foreach (var data in details)
            {
                var qnt = _context.PurchaseOrderDetails.Where(x => x.cSDetailId == data.Id).Sum(x => x.poQty);
                var unOrderQnt = data.qty - qnt;
                if (unOrderQnt != 0)
                {
                    cSItemVMs.Add(new CSItemVM
                    {
                        itemId = data.requisitionDetail.itemSpecification.Item.Id,
                        itemCode = data.requisitionDetail.itemSpecification.Item.itemCode,
                        itemName = data.requisitionDetail.itemSpecification.Item.itemName,
                        itemSpec = data.requisitionDetail.itemSpecification.specificationName,
                        unit = data.requisitionDetail.itemSpecification.Item.unit.unitName,
                        PRQnt = data.requisitionDetail.reqQty,
                        csDetailsId = data.Id,
                        qnt = data.qty,
                        UnorderQnt = unOrderQnt,
                        CSUnitPrice = data.rate
                    });
                }
            }
            return cSItemVMs;
        }

        #endregion


        #region Requisition With CS

        public async Task<IEnumerable<RequisitionForCSViewModel>> GetRequisitionListForBuyer(int userId)
        {
            var result = _context.RequisitionForCSViewModels.FromSql($"SCM.Sp_GetRequisitionListForBuyer {userId}").ToListAsync();
            return await result;
        }
        public async Task<IEnumerable<GetSupplierWiseItemDetailsViewModel>> GetSupplierWiseItemDetails(int reqDetailID)
        {
            var result = _context.getSupplierWiseItemDetailsViewModels.FromSql($"SP_Get_SupplierWise_ItemDetails {reqDetailID}").ToListAsync();
            return await result;
        }

        public async Task<IEnumerable<QuotationDetailsVM>> GetQuotationDetailView(int csMasterId)
        {
            var result = _context.QuotationDetailsVMs.FromSql($"SCM.spQutaionDetailsForView {csMasterId}").ToListAsync();
            return await result;
        }

        public async Task<IEnumerable<CSDetailsVM>> GetCSDetailView(int csMasterId)
        {
            var result = _context.CSDetailsVMs.FromSql($"SCM.spCSDetailsForView {csMasterId}").ToListAsync();
            return await result;
        }
        #endregion
    }
}
