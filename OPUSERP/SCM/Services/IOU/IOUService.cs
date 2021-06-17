using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.SCMIOU.Models;
using OPUSERP.Data;
using OPUSERP.Models;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Services.IOU.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.IOU
{
    public class IOUService : IIOUService
    {
        private readonly ERPDbContext _context;

        public IOUService(ERPDbContext context)
        {
            _context = context;
        }
        #region RFQ
        public async Task<int> SaveRFQMaster(RFQMaster iOUMaster)
        {
            if (iOUMaster.Id != 0)
            {
                iOUMaster.updatedBy = iOUMaster.createdBy;
                iOUMaster.updatedAt = DateTime.Now;
                _context.RFQMasters.Update(iOUMaster);
            }
            else
            {
                iOUMaster.createdAt = DateTime.Now;
                _context.RFQMasters.Add(iOUMaster);
            }

            await _context.SaveChangesAsync();
            return iOUMaster.Id;
        }

        public async Task<bool> UpdateRFQMaster(int? id,  string updateBy)
        {
            var VoucherMasters = _context.RFQMasters.Find(id);
            VoucherMasters.isclose = 1;
            VoucherMasters.updatedBy = updateBy;
            VoucherMasters.updatedAt = DateTime.Now;

            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public void UpdateRFQMasterById(int reqId, int status)
        {
            var user = _context.RFQMasters.Find(reqId);
            user.status = status;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public async Task<IEnumerable<RFQMaster>> GetRFQMaster()
        {
            var result = await _context.RFQMasters.OrderByDescending(x=>x.rfqDate).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RFQApprovedListViewModel>> GetRFQApprovedListViewModels(int userId)
        {
            return await _context.RFQApprovedListViews.FromSql(@"Sp_GetRFQListForApproved @p0", userId).AsNoTracking().ToListAsync();
        }
        public async Task<RFQMaster> GetRFQMasterbyId(int Id)
        {
            var result = await _context.RFQMasters.Where(x=>x.Id==Id).FirstOrDefaultAsync();
            return result;
        }

        public async Task<int> SaveRFQReqDetail(RFQReqDetail iOUMaster)
        {
            if (iOUMaster.Id != 0)
            {
                iOUMaster.updatedBy = iOUMaster.createdBy;
                iOUMaster.updatedAt = DateTime.Now;
                _context.RFQReqDetails.Update(iOUMaster);
            }
            else
            {
                iOUMaster.createdAt = DateTime.Now;
                _context.RFQReqDetails.Add(iOUMaster);
            }

            await _context.SaveChangesAsync();
            return iOUMaster.Id;
        }
        public async Task<IEnumerable<RFQReqDetail>> GetRFQReqDetail()
        {
            var result = await _context.RFQReqDetails.Include(x=>x.requisitionMaster).Include(x=>x.rFQMaster).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RFQReqDetail>> GetRFQReqDetailbyreqId(int?reqId)
        {
            var result = await _context.RFQReqDetails.Include(x=>x.requisitionDetail.itemSpecification.Item.unit).Include(x=>x.requisitionMaster).Include(x=>x.rFQMaster).Where(x=>x.rFQMasterId==reqId).ToListAsync();
            return result;
        }

        public async Task<int> SaveRFQSupDetail(RFQSupDetail iOUMaster)
        {
            if (iOUMaster.Id != 0)
            {
                iOUMaster.updatedBy = iOUMaster.createdBy;
                iOUMaster.updatedAt = DateTime.Now;
                _context.RFQSupDetails.Update(iOUMaster);
            }
            else
            {
                iOUMaster.createdAt = DateTime.Now;
                _context.RFQSupDetails.Add(iOUMaster);
            }

            await _context.SaveChangesAsync();
            return iOUMaster.Id;
        }
        public async Task<IEnumerable<RFQSupDetail>> GetRFQSupDetails()
        {
            var result = await _context.RFQSupDetails.Include(x => x.supplier).Include(x => x.rFQMaster).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RFQSupDetail>> GetRFQSupDetailsbyrfqid(int?Id)
        {
            var result = await _context.RFQSupDetails.Include(x => x.supplier).Include(x => x.rFQMaster).Where(x=>x.rFQMasterId==Id).ToListAsync();
            return result;
        }

        public async Task<int> SaveRFQPriceMaster(RFQPriceMaster iOUMaster)
        {
            if (iOUMaster.Id != 0)
            {
                iOUMaster.updatedBy = iOUMaster.createdBy;
                iOUMaster.updatedAt = DateTime.Now;
                _context.RFQPriceMasters.Update(iOUMaster);
            }
            else
            {
                iOUMaster.createdAt = DateTime.Now;
                _context.RFQPriceMasters.Add(iOUMaster);
            }

            await _context.SaveChangesAsync();
            return iOUMaster.Id;
        }
        public async Task<IEnumerable<RFQPriceMaster>> GetRFQPriceMaster()
        {
            var result = await _context.RFQPriceMasters.Include(x=>x.rFQMaster).Include(x=>x.supplier).ToListAsync();
            return result;
        }
        public async Task<RFQPriceMaster> GetRFQPriceMasterbyId(int Id)
        {
            var result = await _context.RFQPriceMasters.Where(x => x.Id == Id).FirstOrDefaultAsync();
            return result;
        }
        public async Task<IEnumerable<RFQPriceMaster>> GetRFQPriceMasterbyrfqmasterId(int Id)
        {
            var result = await _context.RFQPriceMasters.Where(x => x.rFQMasterId == Id).ToListAsync();
            return result;
        }

        public async Task<int> SaveRFQReqDetailPrice(RFQReqDetailPrice iOUMaster)
        {
            if (iOUMaster.Id != 0)
            {
                iOUMaster.updatedBy = iOUMaster.createdBy;
                iOUMaster.updatedAt = DateTime.Now;
                _context.RFQReqDetailPrices.Update(iOUMaster);
            }
            else
            {
                iOUMaster.createdAt = DateTime.Now;
                _context.RFQReqDetailPrices.Add(iOUMaster);
            }

            await _context.SaveChangesAsync();
            return iOUMaster.Id;
        }
        public async Task<IEnumerable<RFQReqDetailPrice>> GetRFQReqDetailPrice()
        {
            var result = await _context.RFQReqDetailPrices.Include(x => x.rFQPriceMaster).Include(x => x.rFQReqDetail.requisitionDetail.requisitionMaster).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RFQReqDetailPrice>> GetRFQReqDetailPricebyreqId(int? reqId, int? suppid)
        {
            var result = await _context.RFQReqDetailPrices.Include(x => x.rFQPriceMaster.rFQMaster).Include(x=>x.rFQReqDetail.rFQMaster).Include(x => x.rFQPriceMaster.supplier).Include(x => x.rFQReqDetail.requisitionDetail.requisitionMaster).Include(x => x.rFQReqDetail.requisitionDetail.itemSpecification.Item.unit).Where(x => x.rFQPriceMasterId == reqId&&x.rFQPriceMaster.supplierId==suppid).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RFQReqDetailPrice>> GetRFQReqDetailPricebyrfqId(int? reqId)
        {
            var result = await _context.RFQReqDetailPrices.Include(x => x.rFQPriceMaster.rFQMaster).Include(x=>x.rFQReqDetail.rFQMaster).Include(x => x.rFQPriceMaster.supplier).Include(x => x.rFQReqDetail.requisitionDetail.requisitionMaster).Include(x => x.rFQReqDetail.requisitionDetail.itemSpecification.Item.unit).Where(x => x.rFQPriceMaster.rFQMasterId == reqId).ToListAsync();
            return result;
        }
        public async Task<bool> DeleteRFQPriceDetailsByMasterId(int id)
        {
            _context.RFQReqDetailPrices.RemoveRange(_context.RFQReqDetailPrices.Where(x => x.rFQPriceMasterId == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> SaveRFQDocumentAttachmentDetail(RFQDocumentAttachmentDetail iOUMaster)
        {
            if (iOUMaster.Id != 0)
            {
                iOUMaster.updatedBy = iOUMaster.createdBy;
                iOUMaster.updatedAt = DateTime.Now;
                _context.RFQDocumentAttachmentDetails.Update(iOUMaster);
            }
            else
            {
                iOUMaster.createdAt = DateTime.Now;
                _context.RFQDocumentAttachmentDetails.Add(iOUMaster);
            }

            await _context.SaveChangesAsync();
            return iOUMaster.Id;
        }
        public async Task<IEnumerable<RFQDocumentAttachmentDetail>> GetRFQDocumentAttachmentDetails()
        {
            var result = await _context.RFQDocumentAttachmentDetails.Include(x => x.rFQPriceMaster.supplier).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RFQDocumentAttachmentDetail>> GetRFQDocumentAttachmentDetailbyrfqid(int? Id,int?suppid)
        {
            var result = await _context.RFQDocumentAttachmentDetails.Include(x => x.rFQPriceMaster.supplier).Where(x => x.rFQPriceMasterId == Id&&x.rFQPriceMaster.supplierId==suppid).ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RFQDocumentAttachmentDetail>> GetRFQDocumentAttachmentDetailbyrfqmasterid(int? Id)
        {
            var result = await _context.RFQDocumentAttachmentDetails.Include(x => x.rFQPriceMaster.supplier).Where(x => x.rFQPriceMaster.rFQMasterId == Id).ToListAsync();
            return result;
        }
        #endregion

        #region IOU Master
        public async Task<int> SaveIOUMaster(IOUMaster iOUMaster)
        {
            if (iOUMaster.Id != 0)
            {
                iOUMaster.updatedBy = iOUMaster.createdBy;
                iOUMaster.updatedAt = DateTime.Now;
                _context.IOUMasters.Update(iOUMaster);
            }
            else
            {
                iOUMaster.createdAt = DateTime.Now;
                _context.IOUMasters.Add(iOUMaster);
            }

            await _context.SaveChangesAsync();
            return iOUMaster.Id;
        }

        //public async Task<IEnumerable<IOUMaster>> GetIOUMaster(string userName)
        //{
        //    var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
        //    int userid = user.userId;
        //    return await _context.IOUMasters.AsNoTracking().Include(x=>x.project).Where(x=>x.userId==userid).ToListAsync();
        //}

        public async Task<IEnumerable<IOUMaster>> GetIOUMaster(string userName)
        {
           
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userid = user.userId;
            List<int?> Iouids = _context.StockMasters.Where(x => x.userId == userid).Select(x => x.IOUId).ToList();

            var result = await (from m in _context.IOUMasters
                                join iou in _context.IOUDetails on m.Id equals iou.IOUId
                                join rd in _context.RequisitionDetails on iou.requisitionDetailId equals rd.Id
                                join rm in _context.RequisitionMasters on rd.requisitionMasterId equals rm.Id
                                join p in _context.Projects on m.projectId equals p.Id
                                where rm.reqBy == userid && !Iouids.Contains(m.Id)
                                select new IOUMaster
                                {
                                    Id = m.Id,
                                    iouNo = m.iouNo,
                                    iouDate = m.iouDate,
                                    expectedDeliveryDate = m.expectedDeliveryDate,
                                    iouStatus = m.iouStatus,
                                    attentionTo = m.attentionTo,
                                    attentionToId = m.attentionToId,
                                    projectId = m.projectId,
                                    userId = m.userId,
                                    createdAt = m.createdAt,
                                    updatedAt = m.updatedAt,
                                    createdBy = m.createdBy,
                                    updatedBy = m.updatedBy,
                                    projectName=p.projectName
                                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<IOUMaster>> GetIOUMasterForPayment()
        {
            List<int?> payments = await _context.IOUPayments.Select(x => x.IOUId).ToListAsync();
            return await _context.IOUMasters.Include(x => x.project).Where(x=> x.iouStatus ==3 && !payments.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<IOUMaster>> GetIOUMasterByUserName(string userName)
        {
            var  user =await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userId = user.userId;
            var result = await (from m in _context.IOUMasters
                                join iud in (from d in _context.IOUDetails
                                             group d by d.IOUId  into g
                                             select new {iouValue = g.Sum(x => x.qty * x.rate), IOUId = g.Key }) on m.Id equals iud.IOUId
                                //join ir in (from d1 in _context.IOUDetails
                                //            group new { d1 } by new { d1.requisitionId,d1.IOUId} into g
                                //            select new { g.Key.requisitionId, g.Key.IOUId}) on m.Id equals ir.IOUId
                                //join r in _context.RequisitionMasters on ir.requisitionId equals r.Id
                                join iu in _context.Users on m.userId equals iu.userId
                                join pp in _context.employeeInfos on iu.Id equals pp.ApplicationUserId
                                join phl in _context.PrintHistories on m.Id equals phl.iOUId into ph1
                                from ph in ph1.DefaultIfEmpty()
                                where m.userId == userId && Convert.ToInt32(ph.printStatus)==0
                                select new IOUMaster
                                {
                                    Id = m.Id,
                                    //requisition = r,
                                    procurementPerson = pp.nameEnglish,
                                    iouNo = m.iouNo,
                                    iouDate = m.iouDate,
                                    expectedDeliveryDate=m.expectedDeliveryDate,
                                    iouValue = iud.iouValue,
                                    iouStatus=m.iouStatus,
                                    iouCurrentStatus = (m.iouStatus == 1 ? "IOU Raised" : m.iouStatus == 2 ? "On Approve" : m.iouStatus == 3 ? "Approved" : m.iouStatus == 4 ? "Returned" : m.iouStatus == -1 ? "Reject" : "N/A")
        
                                }).ToListAsync();

            return result;
            //return await _context.IOUMasters.AsNoTracking().Include(x => x.requisition).Where(x=>x.createdBy == userName).ToListAsync();
        }

        public async Task<IEnumerable<IOUMaster>> GetApprovedIOUMasterListByUserName(string userName)
        {
            var  user =await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userId = user.userId;
            var result = await (from m in _context.IOUMasters
                                join iud in (from d in _context.IOUDetails
                                             group d by d.IOUId  into g
                                             select new {iouValue = g.Sum(x => x.qty * x.rate), IOUId = g.Key }) on m.Id equals iud.IOUId
                                //join ir in (from d1 in _context.IOUDetails
                                //            group new { d1 } by new { d1.requisitionId,d1.IOUId} into g
                                //            select new { g.Key.requisitionId, g.Key.IOUId}) on m.Id equals ir.IOUId
                                //join r in _context.RequisitionMasters on ir.requisitionId equals r.Id
                                join iu in _context.Users on m.userId equals iu.userId
                                join pp in _context.employeeInfos on iu.Id equals pp.ApplicationUserId
                                join phl in _context.PrintHistories on m.Id equals phl.iOUId into ph1
                                from ph in ph1.DefaultIfEmpty()
                                where m.userId == userId && Convert.ToInt32(ph.printStatus)==0 && m.iouStatus==3
                                select new IOUMaster
                                {
                                    Id = m.Id,
                                    //requisition = r,
                                    procurementPerson = pp.nameEnglish,
                                    iouNo = m.iouNo,
                                    iouDate = m.iouDate,
                                    expectedDeliveryDate=m.expectedDeliveryDate,
                                    iouValue = iud.iouValue,
                                    iouStatus=m.iouStatus,
                                    iouCurrentStatus = (m.iouStatus == 1 ? "IOU Raised" : m.iouStatus == 2 ? "On Approve" : m.iouStatus == 3 ? "Approved" : m.iouStatus == 4 ? "Returned" : m.iouStatus == -1 ? "Reject" : "N/A")
        
                                }).ToListAsync();

            return result;
            //return await _context.IOUMasters.AsNoTracking().Include(x => x.requisition).Where(x=>x.createdBy == userName).ToListAsync();
        }

        public async Task<IEnumerable<IOUMaster>> GetIssuedIOUInformation(string userName)
        {
            int userId = _context.Users.Where(x => x.UserName == userName).FirstOrDefault().userId;
            var result = await (from m in _context.IOUMasters
                                join iud in (from d in _context.IOUDetails
                                             group d by d.IOUId into g
                                             select new { iouValue = g.Sum(x => x.qty * x.rate), IOUId = g.Key }) on m.Id equals iud.IOUId
                                //join ir in (from d1 in _context.IOUDetails
                                //            group new { d1 } by new { d1.requisitionId, d1.IOUId } into g
                                //            select new { g.Key.requisitionId, g.Key.IOUId }) on m.Id equals ir.IOUId
                                //join r in _context.RequisitionMasters on ir.requisitionId equals r.Id
                                join iu in _context.Users on m.userId equals iu.userId
                                join pp in _context.employeeInfos on iu.Id equals pp.ApplicationUserId
                                join phl in _context.PrintHistories on m.Id equals phl.iOUId into ph1
                                from ph in ph1.DefaultIfEmpty()
                                where m.userId == userId && Convert.ToInt32(ph.printStatus) == 1 && m.iouStatus == 3
                                select new IOUMaster
                                {
                                    Id = m.Id,
                                    //requisition = r,
                                    procurementPerson = pp.nameEnglish,
                                    iouNo = m.iouNo,
                                    iouDate = m.iouDate,
                                    expectedDeliveryDate = m.expectedDeliveryDate,
                                    iouValue = iud.iouValue,
                                    iouStatus = m.iouStatus,
                                    iouCurrentStatus = (m.iouStatus == 1 ? "IOU Raised" : m.iouStatus == 2 ? "On Approve" : m.iouStatus == 3 ? "Approved" : m.iouStatus == 4 ? "Returned" : m.iouStatus == -1 ? "Reject" : "N/A"
        )
                                }).ToListAsync();

            return result;
            //return await _context.IOUMasters.AsNoTracking().Include(x => x.requisition).Where(x=>x.createdBy == userName).ToListAsync();
        }


        public async Task<IEnumerable<IOUMaster>> GetIOUListForApprove(int userId)
        {
            List<int?> statusids = new List<int?> { 3, 4, -1 };
            var result = await (from m in _context.IOUMasters
                                join iud in (from d in _context.IOUDetails
                                            group d by d.IOUId into g
                                            select new { iouValue=g.Sum(x=>x.qty * x.rate),IOUId=g.Key}) on m.Id equals iud.IOUId
                                //join ir in (from d1 in _context.IOUDetails
                                //            group new { d1 } by new { d1.requisitionId, d1.IOUId } into g
                                //            select new { g.Key.requisitionId, g.Key.IOUId }) on m.Id equals ir.IOUId
                                //join r in _context.RequisitionMasters on ir.requisitionId equals r.Id
                                join iu in _context.Users on m.userId equals iu.userId
                                join pp in _context.employeeInfos on iu.Id equals pp.ApplicationUserId
                                join a in (from a1 in _context.ApprovalLogs.Where(x => x.matrixTypeId == 6)
                                           group new { a1 } by new { a1.masterId } into a11
                                           select new { masterId = a11.Key.masterId, sequenceNo = a11.Max(x => x.a1.sequenceNo) } into g
                                           join a2 in _context.ApprovalLogs.Where(x => x.matrixTypeId == 6) on new { g.masterId, g.sequenceNo } equals new { a2.masterId, a2.sequenceNo }
                                           group new { g, a2 } by new { a2.masterId, a2.userId, a2.sequenceNo, a2.isActive } into css
                                           select new { css.Key.userId, css.Key.masterId, css.Key.sequenceNo, css.Key.isActive }
                                          ) on m.Id equals a.masterId
                                join au in _context.Users on a.userId equals au.userId
                                join ap in _context.employeeInfos on au.Id equals ap.ApplicationUserId
                                join al in _context.ApprovalLogs.Where(x => x.matrixTypeId == 6 && x.isActive == 1) on m.Id equals al.masterId
                                where al.userId == userId  && !statusids.Contains(m.iouStatus)
                                select new IOUMaster
                                {
                                    Id = m.Id,
                                    //requisition = r,
                                    procurementPerson = pp.nameEnglish,
                                    iouNo = m.iouNo,
                                    iouDate = m.iouDate,
                                    expectedDeliveryDate = m.expectedDeliveryDate,
                                    approverName = ap.nameEnglish,
                                    iouValue=iud.iouValue,
                                    iouStatus=m.iouStatus,
                                    iouCurrentStatus= (m.iouStatus == 1 ? "IOU Raised" : m.iouStatus == 2 ? "On Approve" : m.iouStatus == 3 ? "Approved" : m.iouStatus == 4 ? "Returned" : m.iouStatus == -1 ? "Reject" : "N/A"
        )
                                }).ToListAsync();
            return result;
        }

        public async Task<IOUMaster> GetIOUMasterById(int id)
        {
            return await _context.IOUMasters.Where(x=>x.Id==id).Include(x=>x.project).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteIOUMasterById(int id)
        {
            _context.IOUMasters.Remove(_context.IOUMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<string> GetIOUNo()
        {
            try
            {
                string year = DateTime.Now.Year.ToString();
                int maxId = await _context.IOUMasters.CountAsync() + 1;
                string iouNo = "IOU/" + year + "/" + maxId;

                return iouNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateIOUMaster(int id, int status)
        {
            var user = _context.IOUMasters.Find(id);
            user.iouStatus = status;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion

        #region IOU Details
        public async Task<int> SaveIOUDetails(IOUDetails iOUDetails)
        {
            if (iOUDetails.Id != 0)
            {
                iOUDetails.updatedBy = iOUDetails.createdBy;
                iOUDetails.updatedAt = DateTime.Now;
                _context.IOUDetails.Update(iOUDetails);
            }
            else
            {
                iOUDetails.createdAt = DateTime.Now;
                _context.IOUDetails.Add(iOUDetails);
            }

            await _context.SaveChangesAsync();
            return iOUDetails.Id;
        }

        public async Task<IEnumerable<IOUDetails>> GetIOUDetails()
        {
            return await _context.IOUDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<IOUDetails>> GetIOUDetailsByMasterId(int id)
        {
            return await _context.IOUDetails.Where(x => x.IOUId == id)
                .Include(x => x.requisitionDetail.itemSpecification.Item.unit)
                .Include(x => x.IOU)
                .Include(x => x.requisitionDetail.requisitionMaster.project)
                .AsNoTracking().ToListAsync();
        }
        //public async Task<IEnumerable<IOUDetails>> GetIOUDetailsByMasterId(int id)
        //{
        //    return await _context.getAgreementReportViewModels.FromSql($"SP_RPT_CLIENT_AGREEMENT {AgreementId}").AsNoTracking().FirstOrDefaultAsync();
        //}

        public async Task<string> GetIOUOwner(int id)
        {
            string result = await (from iu in _context.IOUDetails
                                       //join rd in _context.RequisitionDetails on iu.requisitionId equals rd.requisitionMasterId
                                       //join rm in _context.RequisitionMasters on rd.requisitionMasterId equals rm.Id
                                   join rm in _context.IOUMasters on iu.IOUId equals rm.Id
                                   //join a in _context.Users on rm.attentionTo equals a.userId
                                   join e in _context.employeeInfos on rm.attentionTo equals e.nameEnglish
                                   where iu.IOUId == id
                                   select e.nameEnglish+" ("+e.employeeCode+")").FirstOrDefaultAsync();
            return result;
        }

        public async Task<IOUDetails> GetIOUDetailsById(int id)
        {
            return await _context.IOUDetails.FindAsync(id);
        }

        public async Task<bool> DeleteIOUDetailsById(int id)
        {
            _context.IOUDetails.Remove(_context.IOUDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteIOUDetailsByMasterId(int id)
        {
            _context.IOUDetails.RemoveRange(_context.IOUDetails.Where(x=>x.Id==id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region IOU Payment Master
        public async Task<int> SaveIOUPaymentMaster(IOUPaymentMaster iOUPaymentMaster)
        {
            if (iOUPaymentMaster.Id != 0)
            {
                iOUPaymentMaster.updatedBy = iOUPaymentMaster.createdBy;
                iOUPaymentMaster.updatedAt = DateTime.Now;
                _context.iOUPaymentMasters.Update(iOUPaymentMaster);
            }
            else
            {
                iOUPaymentMaster.createdAt = DateTime.Now;
                _context.iOUPaymentMasters.Add(iOUPaymentMaster);
            }

            await _context.SaveChangesAsync();
            return iOUPaymentMaster.Id;
        }

        public async Task<IEnumerable<IOUPaymentMaster>> GetAllIOUPaymentMaster()
        {
            return await _context.iOUPaymentMasters.AsNoTracking().Include(x => x.project).ToListAsync();
        }

        public async Task<IEnumerable<IOUPaymentMaster>> GetIOUPaymentMasterListForApprove(int userId)
        {

            return await (from m in _context.iOUPaymentMasters
                          join iud in (from d in _context.IOUPayments
                                       group d by d.iOUPaymentMasterId into g
                                       select new { iouValue = g.Sum(x => x.paymentAmount), iOUPaymentMasterId = g.Key }) on m.Id equals iud.iOUPaymentMasterId
                          join al in _context.ApprovalLogs.Where(x => x.matrixTypeId == 7 && x.isActive == 1) on m.Id equals al.masterId
                          where al.userId == userId && m.iouPaymentStatus != 3 && m.iouPaymentStatus != 2

                          select new IOUPaymentMaster
                          {
                              Id = m.Id,
                              pymentPerson = "",
                              iouPaymentNo = m.iouPaymentNo,
                              iouPaymentDate = m.iouPaymentDate,
                              projectName = "",
                              attentionTo = m.attentionTo,
                              approverName = "",
                              iouPaymentValue = iud.iouValue,
                              iouPaymentStatus = m.iouPaymentStatus,
                              iouPaymentCurrentStatus = (m.iouPaymentStatus == 1 ? "Raised" : m.iouPaymentStatus == 2 ? "On Approve" : m.iouPaymentStatus == 3 ? "Approved" : m.iouPaymentStatus == 4 ? "Returned" : m.iouPaymentStatus == -1 ? "Reject" : "N/A")
                          }).ToListAsync();
            //if (userId == 7)
            //{
            //     return await (from m in _context.iOUPaymentMasters
            //                  join iud in (from d in _context.IOUPayments
            //                               group d by d.iOUPaymentMasterId into g
            //                               select new { iouValue = g.Sum(x => x.paymentAmount), iOUPaymentMasterId = g.Key }) on m.Id equals iud.iOUPaymentMasterId

            //                  //join iu in _context.Users on m.userId equals iu.userId
            //                  //join pp in _context.employeeInfos on iu.Id equals pp.ApplicationUserId
            //                  //join a in (from a1 in _context.ApprovalLogs.Where(x => x.matrixTypeId == 7)
            //                  //           group new { a1 } by new { a1.masterId } into a11
            //                  //           select new { masterId = a11.Key.masterId, sequenceNo = a11.Max(x => x.a1.sequenceNo) } into g
            //                  //           join a2 in _context.ApprovalLogs.Where(x => x.matrixTypeId == 7) on new { g.masterId, g.sequenceNo } equals new { a2.masterId, a2.sequenceNo }
            //                  //           group new { g, a2 } by new { a2.masterId, a2.userId, a2.sequenceNo, a2.isActive } into css
            //                  //           select new { css.Key.userId, css.Key.masterId, css.Key.sequenceNo, css.Key.isActive }
            //                  //          ) on m.Id equals a.masterId
            //                  //join au in _context.Users on a.userId equals au.userId
            //                  //join pj in _context.Projects on m.projectId equals pj.Id
            //                  //join ap in _context.employeeInfos on au.Id equals ap.ApplicationUserId
            //                  //join al in _context.ApprovalLogs.Where(x => x.matrixTypeId == 7 && x.isActive == 1) on m.Id equals al.masterId
            //                  //where al.userId == userId && m.iouPaymentStatus != 3
            //                  where m.iouPaymentStatus != 3 && m.iouPaymentStatus != 2

            //                  select new IOUPaymentMaster
            //                  {
            //                      Id = m.Id,
            //                      pymentPerson = "",
            //                      iouPaymentNo = m.iouPaymentNo,
            //                      iouPaymentDate = m.iouPaymentDate,
            //                      projectName = "",
            //                      attentionTo = m.attentionTo,
            //                      approverName = "",
            //                      iouPaymentValue = iud.iouValue,
            //                      iouPaymentStatus = m.iouPaymentStatus,
            //                      iouPaymentCurrentStatus = (m.iouPaymentStatus == 1 ? "Raised" : m.iouPaymentStatus == 2 ? "On Approve" : m.iouPaymentStatus == 3 ? "Approved" : m.iouPaymentStatus == 4 ? "Returned" : m.iouPaymentStatus == -1 ? "Reject" : "N/A")
            //                  }).ToListAsync();
            //}
            //else
            //{
            //    return await (from m in _context.iOUPaymentMasters
            //                  join iud in (from d in _context.IOUPayments
            //                               group d by d.iOUPaymentMasterId into g
            //                               select new { iouValue = g.Sum(x => x.paymentAmount), iOUPaymentMasterId = g.Key }) on m.Id equals iud.iOUPaymentMasterId

            //                  where m.iouPaymentStatus == 2

            //                  select new IOUPaymentMaster
            //                  {
            //                      Id = m.Id,
            //                      pymentPerson = "",
            //                      iouPaymentNo = m.iouPaymentNo,
            //                      iouPaymentDate = m.iouPaymentDate,
            //                      projectName = "",
            //                      attentionTo = m.attentionTo,
            //                      approverName = "",
            //                      iouPaymentValue = iud.iouValue,
            //                      iouPaymentStatus = m.iouPaymentStatus,
            //                      iouPaymentCurrentStatus = (m.iouPaymentStatus == 1 ? "Raised" : m.iouPaymentStatus == 2 ? "On Approve" : m.iouPaymentStatus == 3 ? "Approved" : m.iouPaymentStatus == 4 ? "Returned" : m.iouPaymentStatus == -1 ? "Reject" : "N/A")
            //                  }).ToListAsync();
            //}

        }

        public async Task<IOUPaymentMaster> GetIOUPaymentMasterById(int id)
        {
            return await _context.iOUPaymentMasters.Where(x => x.Id == id).Include(x => x.project).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteIOUPaymentMasterById(int id)
        {
            _context.iOUPaymentMasters.Remove(_context.iOUPaymentMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<string> GetIOUPaymentNo()
        {
            try
            {
                string year = DateTime.Now.Year.ToString();
                int maxId = await _context.iOUPaymentMasters.CountAsync() + 1;
                string iouNo = "Disburse/" + year + "/" + maxId;

                return iouNo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateIOUPaymentMaster(int id, int status)
        {
            var user = _context.iOUPaymentMasters.Find(id);
            user.iouPaymentStatus = status;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion

        #region IOU Payment
        public async Task<int> SaveIOUPayment(IOUPayment iOUPayment)
        {
            if (iOUPayment.Id != 0)
            {
                iOUPayment.updatedBy = iOUPayment.createdBy;
                iOUPayment.updatedAt = DateTime.Now;
                _context.IOUPayments.Update(iOUPayment);
            }
            else
            {
                iOUPayment.createdAt = DateTime.Now;
                _context.IOUPayments.Add(iOUPayment);
            }

            await _context.SaveChangesAsync();
            return iOUPayment.Id;
        }
        
        public async Task<bool> UpdateIOUPaymentForApprove(int Id, decimal? paymentAmount, int? statusId)
        {
            var data = _context.IOUPayments.Find(Id);
            data.paymentAmount = paymentAmount;
            data.statusInfoId = statusId;

            _context.Entry(data).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateIOUPaymentForReceivedPayment(int Id, decimal? paymentAmount, int? receivebyId,DateTime? receiveDate, string paymentMode, string chequeNo,string bankName)
        {
            var data = _context.IOUPayments.Find(Id);
            data.paymentAmount = paymentAmount;
            data.statusInfoId = 5;
            data.receivebyId = receivebyId;
            data.receiveDate = receiveDate;
            data.paymentMode = paymentMode;
            data.bankName = bankName;
            data.chequeNo = chequeNo;

            _context.Entry(data).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateIOUPayment(IOUPayment iOUPayment)
        {
            var IOUPayments = _context.IOUPayments.Find(iOUPayment.Id);
            if (IOUPayments != null)
            {
                IOUPayments.adjustmentAmount = iOUPayment.adjustmentAmount;
                IOUPayments.adjustmentDate = iOUPayment.adjustmentDate;
                IOUPayments.statusInfoId = iOUPayment.statusInfoId;
                IOUPayments.balance = iOUPayment.balance;
                IOUPayments.adjustmentBy = iOUPayment.adjustmentBy;
            }
            await _context.SaveChangesAsync();
            return IOUPayments.Id;
        }

        public async Task<IEnumerable<IOUPayment>> GetIOUPayment()
        {
            return await _context.IOUPayments.Include(x=>x.IOU.project).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<IOUPayment>> GetIOUPaymentByType(int type)
        {
            return await _context.IOUPayments.Where(x=>x.statusInfoId == type).Include(x => x.IOU.project).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<IOUPayment>> GetIOUPaymentByMasterId(int id)
        {
            return await _context.IOUPayments.Where(x => x.Id == id).Include(x => x.IOU.project).Include(x => x.IOU).AsNoTracking().ToListAsync();
        }

        public async Task<IOUPayment> GetIOUPaymentById(int id)
        {
            return await _context.IOUPayments.Where(x=>x.Id == id).Include(x => x.IOU.project).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<IOUPayment>> GetIOUPaymentByiOUPaymentMasterId(int iOUPaymentMasterId)
        {
            return await _context.IOUPayments.Where(x => x.iOUPaymentMasterId == iOUPaymentMasterId).Include(x => x.IOU.project).Include(x=>x.iOUPaymentMaster).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteIOUPaymentById(int id)
        {
            _context.IOUPayments.Remove(_context.IOUPayments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteIOUPaymentByMasterId(int id)
        {
            _context.IOUPayments.RemoveRange(_context.IOUPayments.Where(x => x.Id == id).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<IOUPaymentStatusVM>> GetIOUPaymentStatus(int projectId, DateTime? fromDate, DateTime? toDate)
        {
            List<IOUPaymentStatusVM> IOUPayment = new List<IOUPaymentStatusVM>();
            if (projectId == 0)
            {
                IOUPayment = await _context.IOUPayments.AsNoTracking()
                                .Where(x => x.receiveDate >= fromDate && x.paymentDate <= toDate)
                                .Include(x => x.IOU.project)
                                .Include(x => x.iOUPaymentMaster)
                                .Include(x => x.receiveby)
                                .Select(x => new IOUPaymentStatusVM
                                {
                                    projectName = x.IOU.project.projectName,
                                    iouNo = x.IOU.iouNo,
                                    attentionTo = x.receiveby.nameEnglish,
                                    iouDate = x.IOU.iouDate.Value.ToString("dd-MMM-yyyy"),
                                    paymentDate = x.receiveDate.Value.ToString("dd-MMM-yyyy"),
                                    adjustmentDate = x.adjustmentDate.HasValue ? x.adjustmentDate.Value.ToString("dd-MMM-yyyy") : string.Empty,
                                    paymentAmount = x.paymentAmount,
                                    adjustmentAmount = x.adjustmentAmount ?? (decimal)0,
                                    returnAmount = x.balance ?? (decimal)0,
                                    balanceAmount = (x.paymentAmount - ((x.adjustmentAmount ?? (decimal)0) + (x.balance ?? (decimal)0)))
                                }).ToListAsync();
            }
            else
            {
                IOUPayment = await _context.IOUPayments.AsNoTracking()
                                .Where(x => x.receiveDate >= fromDate && x.paymentDate <= toDate && x.IOU.projectId == projectId)
                                .Include(x => x.IOU.project)
                                .Include(x => x.iOUPaymentMaster)
                                .Include(x => x.receiveby)
                                .Select(x => new IOUPaymentStatusVM
                                {
                                    projectName = x.IOU.project.projectName,
                                    iouNo = x.IOU.iouNo,
                                    attentionTo = x.receiveby.nameEnglish,
                                    iouDate = x.IOU.iouDate.Value.ToString("dd-MMM-yyyy"),
                                    paymentDate = x.receiveDate.Value.ToString("dd-MMM-yyyy"),
                                    adjustmentDate = x.adjustmentDate.HasValue ? x.adjustmentDate.Value.ToString("dd-MMM-yyyy") : string.Empty,
                                    paymentAmount = x.paymentAmount,
                                    adjustmentAmount = x.adjustmentAmount ?? (decimal)0,
                                    returnAmount = x.balance ?? (decimal)0,
                                    balanceAmount = (x.paymentAmount - ((x.adjustmentAmount ?? (decimal)0) + (x.balance ?? (decimal)0)))
                                }).ToListAsync();
            }
             

            return IOUPayment;
        }
        public async Task<IEnumerable<IOUPaymentStatusVM>> GetIOUPaymentStatusEmployee(int attentionToId, DateTime? fromDate, DateTime? toDate)
        {
            List<IOUPaymentStatusVM> IOUPayment = new List<IOUPaymentStatusVM>();
            if (attentionToId == 0)
            {
                IOUPayment = await _context.IOUPayments.AsNoTracking()
                                .Where(x => x.receiveDate >= fromDate && x.paymentDate <= toDate)
                                .Include(x => x.IOU.project)
                                .Include(x => x.iOUPaymentMaster)
                                .Include(x => x.receiveby)
                                .Select(x => new IOUPaymentStatusVM
                                {
                                    projectName = x.IOU.project.projectName,
                                    iouNo = x.IOU.iouNo,
                                    attentionTo= x.receiveby.nameEnglish,
                                    iouDate = x.IOU.iouDate.Value.ToString("dd-MMM-yyyy"),
                                    paymentDate = x.receiveDate.Value.ToString("dd-MMM-yyyy"),
                                    adjustmentDate = x.adjustmentDate.HasValue ? x.adjustmentDate.Value.ToString("dd-MMM-yyyy") : string.Empty,
                                    paymentAmount = x.paymentAmount,
                                    adjustmentAmount = x.adjustmentAmount ?? (decimal)0,
                                    returnAmount = x.balance ?? (decimal)0,
                                    balanceAmount = (x.paymentAmount - ((x.adjustmentAmount ?? (decimal)0) + (x.balance ?? (decimal)0)))
                                }).ToListAsync();
            }
            else
            {
                IOUPayment = await _context.IOUPayments.AsNoTracking()
                                .Where(x => x.receiveDate >= fromDate && x.paymentDate <= toDate && x.receivebyId == attentionToId)
                                .Include(x => x.IOU.project)
                                 .Include(x => x.iOUPaymentMaster)
                                 .Include(x => x.receiveby)
                                .Select(x => new IOUPaymentStatusVM
                                {
                                    projectName = x.IOU.project.projectName,
                                    iouNo = x.IOU.iouNo,
                                    attentionTo = x.receiveby.nameEnglish,
                                    iouDate = x.IOU.iouDate.Value.ToString("dd-MMM-yyyy"),
                                    paymentDate = x.receiveDate.Value.ToString("dd-MMM-yyyy"),
                                    adjustmentDate = x.adjustmentDate.HasValue ? x.adjustmentDate.Value.ToString("dd-MMM-yyyy") : string.Empty,
                                    paymentAmount = x.paymentAmount,
                                    adjustmentAmount = x.adjustmentAmount ?? (decimal)0,
                                    returnAmount = x.balance ?? (decimal)0,
                                    balanceAmount = (x.paymentAmount - ((x.adjustmentAmount ?? (decimal)0) + (x.balance ?? (decimal)0)))
                                }).ToListAsync();
            }


            return IOUPayment;
        }

        #endregion

        // Disbarse

        public async Task<IEnumerable<IOUMaster>> GetIOUMasterByUserNameNDateTime(string userName, string formDate, string toDate)
        {
            List<int?> payments = await _context.IOUPayments.Select(x => x.IOUId).ToListAsync();
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userId = user.userId;
            var result = await (from m in _context.IOUMasters
                                join iud in (from d in _context.IOUDetails
                                             group d by d.IOUId into g
                                             select new { iouValue = g.Sum(x => x.qty * x.rate), IOUId = g.Key }) on m.Id equals iud.IOUId
                                //join ir in (from d1 in _context.IOUDetails
                                //            group new { d1 } by new { d1.requisitionId, d1.IOUId } into g
                                //            select new { g.Key.requisitionId, g.Key.IOUId }) on m.Id equals ir.IOUId
                                //join r in _context.RequisitionMasters on ir.requisitionId equals r.Id
                                join iu in _context.Users on m.userId equals iu.userId
                                join pj in _context.Projects on m.projectId equals pj.Id
                                join pp in _context.employeeInfos on iu.Id equals pp.ApplicationUserId
                                join phl in _context.PrintHistories on m.Id equals phl.iOUId into ph1
                                from ph in ph1.DefaultIfEmpty()
                                where  !payments.Contains(m.Id) && m.iouStatus==3
                                //where m.userId == userId && Convert.ToInt32(ph.printStatus) == 0
                                //where m.iouDate >= Convert.ToDateTime(formDate) && m.iouDate <= Convert.ToDateTime(toDate)
            select new IOUMaster
                                {
                                    Id = m.Id,
                                    //requisition = m.requisition,
                                    procurementPerson = pp.nameEnglish,
                                    iouNo = m.iouNo,
                                    iouDate = m.iouDate,
                                    projectName = pj.projectName,
                                    expectedDeliveryDate = m.expectedDeliveryDate,
                                    iouValue = iud.iouValue,
                                    iouStatus = m.iouStatus,
                                    projectId = m.projectId,
                                    iouCurrentStatus = (m.iouStatus == 1 ? "IOU Raised" : m.iouStatus == 2 ? "On Approve" : m.iouStatus == 3 ? "Approved" : m.iouStatus == 4 ? "Returned" : m.iouStatus == -1 ? "Reject" : "N/A"
        )
                                }).ToListAsync();

            return result;
            //return await _context.IOUMasters.AsNoTracking().Include(x => x.requisition).Where(x=>x.createdBy == userName).ToListAsync();
        }
        public async Task<IEnumerable<IOUPaymentMaster>> GetIOUPaymentMasterByUserName(string userName)
        {
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userId = user.userId;
            var result = await (from m in _context.iOUPaymentMasters
                                join iud in (from d in _context.IOUPayments
                                             group d by d.iOUPaymentMasterId into g
                                             select new { iouValue = g.Sum(x => x.paymentAmount), iOUPaymentMasterId = g.Key }) on m.Id equals iud.iOUPaymentMasterId

                                //join iu in _context.Users on m.userId equals iu.userId
                                //join pj in _context.Projects on m.projectId equals pj.Id
                                //join pp in _context.employeeInfos on iu.Id equals pp.ApplicationUserId
                                join phl in _context.PrintHistories on m.Id equals phl.iOUPaymentMasterId into ph1
                                from ph in ph1.DefaultIfEmpty()
                                where  Convert.ToInt32(ph.printStatus) == 0 
                                select new IOUPaymentMaster
                                {
                                    Id = m.Id,
                                    pymentPerson = "",
                                    attentionTo = m.attentionTo,
                                    iouPaymentNo = m.iouPaymentNo,
                                    iouPaymentDate = m.iouPaymentDate,
                                    projectName = "",
                                    iouPaymentValue = iud.iouValue,
                                    iouPaymentStatus = m.iouPaymentStatus,
                                    iouPaymentCurrentStatus = (m.iouPaymentStatus == 1 ? "Raised" : m.iouPaymentStatus == 2 ? "On Approve" : m.iouPaymentStatus == 3 ? "Approved" : m.iouPaymentStatus == 4 ? "Returned" : m.iouPaymentStatus == -1 ? "Reject" : "N/A")

                                }).ToListAsync();

            return result;
            
        }
        public async Task<IEnumerable<IOUPaymentMaster>> GetIssuedIOUPaymentMasterByUserName(string userName)
        {
            var user = await _context.Users.Where(x => x.UserName == userName).FirstOrDefaultAsync();
            int userId = user.userId;
            var result = await (from m in _context.iOUPaymentMasters
                                join iud in (from d in _context.IOUPayments
                                             group d by d.iOUPaymentMasterId into g
                                             select new { iouValue = g.Sum(x => x.paymentAmount), iOUPaymentMasterId = g.Key }) on m.Id equals iud.iOUPaymentMasterId

                                //join iu in _context.Users on m.userId equals iu.userId
                                //join pj in _context.Projects on m.projectId equals pj.Id
                                //join pp in _context.employeeInfos on iu.Id equals pp.ApplicationUserId
                                join phl in _context.PrintHistories on m.Id equals phl.iOUPaymentMasterId into ph1
                                from ph in ph1.DefaultIfEmpty()
                                where  Convert.ToInt32(ph.printStatus) == 1 && m.iouPaymentStatus==3
                                select new IOUPaymentMaster
                                {
                                    Id = m.Id,
                                    pymentPerson = "",
                                    attentionTo = m.attentionTo,
                                    iouPaymentNo = m.iouPaymentNo,
                                    iouPaymentDate = m.iouPaymentDate,
                                    projectName = "",
                                    iouPaymentValue = iud.iouValue,
                                    iouPaymentStatus = m.iouPaymentStatus,
                                    iouPaymentCurrentStatus = (m.iouPaymentStatus == 1 ? "Raised" : m.iouPaymentStatus == 2 ? "On Approve" : m.iouPaymentStatus == 3 ? "Approved" : m.iouPaymentStatus == 4 ? "Returned" : m.iouPaymentStatus == -1 ? "Reject" : "N/A")

                                }).ToListAsync();

            return result;

        }

    }
}
