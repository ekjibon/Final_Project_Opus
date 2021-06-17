using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.SCMDashboard.Models;
using OPUSERP.Areas.SCMPurchaseProcess.Models;
using OPUSERP.Areas.SCMReport.Models;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.Data;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Requisition
{
    public class RequisitionService : IRequisitionService
    {
        private readonly ERPDbContext _context;

        public RequisitionService(ERPDbContext context)
        {
            _context = context;
        }

        #region RequisitionMaster
        public async Task<int> SaveRequisitionMaster(RequisitionMaster requisitionMaster)
        {
            try
            {
                if (requisitionMaster.Id != 0)
                {
                    requisitionMaster.updatedBy = requisitionMaster.createdBy;
                    requisitionMaster.updatedAt = DateTime.Now;
                    _context.RequisitionMasters.Update(requisitionMaster);
                }
                else
                {
                    requisitionMaster.createdAt = DateTime.Now;
                    _context.RequisitionMasters.Add(requisitionMaster);
                }

                await _context.SaveChangesAsync();
                return requisitionMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void UpdateRequisitionMasterStatusById(int reqId, int status)
        {
            var user = _context.RequisitionMasters.Find(reqId);
            user.statusInfoId = status;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;


            //var requisition = new RequisitionMaster() { Id = reqId, statusInfoId = status};

            //_context.RequisitionMasters.Attach(requisition).Property(x => x.statusInfoId).IsModified = true;

            //_context.RequisitionMasters.Attach(requisition).Property(x => x.updatedAt).IsModified = true;
            //_context.RequisitionMasters.Attach(requisition);
            //_context.Entry(requisition).Property(x => x.statusInfoId).IsModified = true;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<Project>> GetProjectByUser(string userName)
        {
            var result = await (from p in _context.Projects
                                join a in (from d in _context.ApprovalMatrices
                                           group d by new { d.projectId, d.userId } into g
                                           select new { projectId = g.Key.projectId, userId = g.Key.userId }) on p.Id equals a.projectId
                                join u in _context.Users on a.userId equals u.userId
                                where u.UserName == userName
                                select p).AsNoTracking().ToListAsync();

            return result;
        }


        public void AssignTeamInRequisitionMasterById(int masterId, int status, int teamId)
        {
            var user = _context.RequisitionMasters.Find(masterId);
            user.statusInfoId = status;
            user.updatedAt = DateTime.Now;
            user.assignDate = DateTime.Now;
            user.teamMasterId = teamId;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void ReturnTeamInRequisitionMasterById(int masterId)
        {
            var user = _context.RequisitionMasters.Find(masterId);
            user.statusInfoId = 3;
            user.updatedAt = DateTime.Now;
            user.assignDate = DateTime.Now;
            user.teamMasterId = null;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void AssignTeamInRequisitionDetailsById(int Id, int status, int memberId)
        {
            var user = _context.RequisitionDetails.Find(Id);

            int masterId = user.requisitionMasterId;
            if (user != null)
            {
                user.updatedAt = DateTime.Now;
                user.jobAssignDate = DateTime.Now;
                user.teamMemberId = memberId;
            }

            // _context.Entry(user).State = EntityState.Modified;

            var master = _context.RequisitionMasters.Find(masterId);
            if (master != null)
            {
                master.updatedAt = DateTime.Now;
                master.statusInfoId = status;
            }

            // _context.Entry(master).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<RequisitionMaster>> GetRequisitionMasterList(int reqBy)
        {
            return await _context.RequisitionMasters.AsNoTracking().Where(x => x.reqBy == reqBy).ToListAsync();
        }

        public async Task<IEnumerable<RequisitionMaster>> GetRequisitionMasterListByPRStatus(int reqBy, int status)
        {
            return await _context.RequisitionMasters.AsNoTracking().Where(x => x.reqBy == reqBy && x.statusInfoId == status).ToListAsync();
        }

        public async Task<DashboardModel> GetRequisitionMasterListForDashboard(int reqBy)
        {
            var requisition = await _context.RequisitionMasters.AsNoTracking().Where(x => x.reqBy == reqBy).Include(x => x.statusInfo).ToListAsync();
            var reqIds = requisition.Select(x => x.Id);

            var CSMaster = await _context.CSMasters.Where(x => reqIds.Contains((int)x.requisitionId)).Include(x => x.requisition).ToListAsync();
            var CSIds = CSMaster.Select(x => x.Id);
            var CSReqIds = CSMaster.Select(x => x.requisitionId);

            var approvalStage = requisition.Where(x => !CSReqIds.Contains(x.Id)).ToList();

            var PO = await _context.PurchaseOrderMasters.Where(x => CSIds.Contains((int)x.csMasterId)).Include(x => x.cSMaster.requisition).ToListAsync();

            var POIds = PO.Select(x => x.csMasterId);

            var POStage = CSMaster.Where(x => POIds.Contains(x.Id)).ToList();

            DashboardModel dashboardViewModel = new DashboardModel
            {
                requisitionMasters = requisition,
                approvalStage = approvalStage,
                purchaseOrderMasters = PO,
                purchaseOrderStages = POStage
            };
            return dashboardViewModel;
        }


        public async Task<IEnumerable<RequisitionMaster>> GetRequisitionMasterByReqId(int reqId)
        {
            return await _context.RequisitionMasters.AsNoTracking().Where(x => x.Id == reqId).ToListAsync();

        }
        public async Task<IEnumerable<RequisitionMaster>> GetRequisitionMasterListForAssign()
        {
            return await _context.RequisitionMasters.AsNoTracking().Where(x => x.statusInfoId == 3 && x.isActive == 1).ToListAsync();
        }

        public async Task<IEnumerable<RequisitionMaster>> GetRequisitionMasterListByStatus(int status)
        {
            return await _context.RequisitionMasters.AsNoTracking().Where(x => x.statusInfoId == status && x.isActive == 1).Include(x => x.teamMaster).ToListAsync();
        }

        public async Task<IEnumerable<RequisitionMaster>> GetAllRequisitionMasterList()
        {
            return await _context.RequisitionMasters.AsNoTracking().Where(x => x.isActive == 1).Include(x => x.teamMaster).Include(x => x.statusInfo).ToListAsync();
        }

        public async Task<IEnumerable<RequisitionMaster>> GetRequisitionMasterListByActivityStatus(int status)
        {
            return await _context.RequisitionMasters.AsNoTracking().Where(x => x.isActive == status).Include(x => x.teamMaster).Include(x => x.statusInfo).ToListAsync();
        }

        public async Task<RequisitionMaster> GetRequisitionMasterById(int id)
        {
            return await _context.RequisitionMasters.Include(x => x.statusInfo).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteRequisitionMasterById(int id)
        {
            _context.RequisitionMasters.Remove(_context.RequisitionMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RequisitionMaster>> GetAllRequisitionMasterForIOUList(string userName)
        {
            List<int> iouids = _context.IOUDetails.Select(x => x.requisitionDetail.Id).ToList();
            List<int?> csids = _context.CSDetails.Include(x => x.cSMaster).Select(x => x.requisitionDetailId).ToList();
            //return await _context.RequisitionMasters.AsNoTracking().Where(x => x.isActive == 1 && x.statusInfoId == 7 && !iouids.Contains(x.Id) && !csids.Contains(x.Id)).Include(x => x.teamMaster).Include(x => x.statusInfo).Include(x=> x.project).ToListAsync();
            var result = await _context.RequisitionMasters.AsNoTracking().Where(x => x.isActive == 1 && x.statusInfoId == 7 && !iouids.Contains(x.Id)).Include(x => x.teamMaster).Include(x => x.statusInfo).Include(x => x.project).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequisitionMasterForStatusVM>> GetAllRequisitionMasterListForViewStatus(DateTime? fromDate, DateTime? toDate, string type, int? userId)
        {
            var aspnetuser = _context.Users.Where(x => x.userId == userId)?.FirstOrDefault()?.Id;
            var roleId = _context.UserRoles.Where(x => x.UserId == aspnetuser)?.FirstOrDefault()?.RoleId;
            var role = _context.Roles.Where(x => x.Id == roleId)?.FirstOrDefault()?.Name;

            if (role == "PO Processor" || role == "SCMAdmin" || role == "PO Admin")
            {
                var reqMasters = await _context.RequisitionMasters.AsNoTracking()
              .Where(x => x.reqDate >= fromDate && x.reqDate <= toDate)
              .Include(x => x.project)
              .Select(x => new RequisitionMasterForStatusVM
              {
                  reqNo = x.reqNo,
                  csNo = "",
                  projectName = x.project.projectName,
                  reqDate = x.reqDate,
                  reqValue = 0,
                  csValue = 0,
                  subject = x.subject,
                  action = "<a class='btn btn-info' title='View Status' onclick='LoadPRStatusById(" + x.Id + ")'><i class='fa fa-file'></i></a><a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' onclick='PRReportView(" + x.Id + ")'><i class='fa fa-print'></i></a>"
                  //action = "<input type='button' class='btn btn-info btn-sm' value='View Status' onclick='LoadPRStatusById(" + x.Id + ")' /><a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' onclick='PRReportView(" + x.Id + ")'><i class='fa fa-print'></i></a>"
              }).ToListAsync();

                return reqMasters;
            }
            else
            {
                var reqMasters = await _context.RequisitionMasters.AsNoTracking()
              .Where(x => x.reqDate >= fromDate && x.reqDate <= toDate && x.reqBy == userId)
              .Include(x => x.project)
              .Select(x => new RequisitionMasterForStatusVM
              {
                  reqNo = x.reqNo,
                  csNo = "",
                  projectName = x.project.projectName,
                  reqDate = x.reqDate,
                  reqValue = 0,
                  csValue = 0,
                  subject = x.subject,
                  action = "<a class='btn btn-info' title='View Status' onclick='LoadPRStatusById(" + x.Id + ")'><i class='fa fa-file'></i></a><a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' onclick='PRReportView(" + x.Id + ")'><i class='fa fa-print'></i></a>"
                  //action = "<input type='button' class='btn btn-info btn-sm' value='View Status' onclick='LoadPRStatusById(" + x.Id + ")' /><a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' onclick='PRReportView(" + x.Id + ")'><i class='fa fa-print'></i></a>"
              }).ToListAsync();

                return reqMasters;
            }

        }

        #endregion

        #region RequisitionDetail
        public async Task<int> SaveRequisitionDetail(RequisitionDetail requisitionDetail)
        {
            if (requisitionDetail.Id != 0)
            {
                requisitionDetail.updatedBy = requisitionDetail.createdBy;
                requisitionDetail.updatedAt = DateTime.Now;
                _context.RequisitionDetails.Update(requisitionDetail);
            }
            else
            {
                requisitionDetail.createdAt = DateTime.Now;
                _context.RequisitionDetails.Add(requisitionDetail);
            }

            await _context.SaveChangesAsync();
            return requisitionDetail.Id;
        }

        public async Task<IEnumerable<RequisitionDetailViewModel>> GetLinqRequisitionDetailListByReqId(int reqId)
        {
            List<int?> reqDetailids = _context.CSDetails.Where(x => x.requisitionDetail.requisitionMaster.Id == reqId).Select(x => x.requisitionDetailId).ToList();


            var result = await (from rd in _context.RequisitionDetails
                                join isp in _context.ItemSpecifications on rd.itemSpecificationId equals isp.Id
                                join i in _context.Items on isp.itemId equals i.Id
                                join u in _context.Units on i.unitId equals u.Id
                                join csd in (from cm in _context.CSMasters
                                             join cd in _context.CSDetails on cm.Id equals cd.cSMasterId
                                             where cm.requisitionId == reqId
                                             group new { cm, cd } by new { cm.requisitionId, cd.requisitionDetailId } into cs
                                             select new { cs.Key.requisitionDetailId, cs.Key.requisitionId, qty = cs.Sum(x => x.cd.qty) }) on rd.Id equals csd.requisitionDetailId into cos
                                from ccss in cos.DefaultIfEmpty()
                                join iou in (from um in _context.IOUMasters
                                             join ud in _context.IOUDetails on um.Id equals ud.IOUId
                                             where ud.requisitionId == reqId
                                             group new { um, ud } by new { ud.requisitionId, ud.requisitionDetailId } into cs
                                             select new { cs.Key.requisitionDetailId, cs.Key.requisitionId, qty = cs.Sum(x => x.ud.qty) }) on rd.Id equals iou.requisitionDetailId into ious
                                from iuss in ious.DefaultIfEmpty()
                                where Convert.ToDecimal(ccss.qty) + Convert.ToDecimal(iuss.qty) < Convert.ToDecimal(rd.reqQty)
                                //join iu in (from iud in _context.IOUDetails
                                //            group iud by new { iud.requisitionDetailId, iud.requisitionId } into iiud
                                //            select new { iiud.Key.requisitionDetailId, qty = iiud.Sum(x => x.qty) }) on rd.Id equals iu.requisitionDetailId 
                                //join csd in (from cm in _context.CSMasters
                                //             join cd in _context.CSDetails on cm.Id equals cd.cSMasterId
                                //             where cm.requisitionId == reqId
                                //             group new { cm, cd } by new { cm.requisitionId, cd.requisitionDetailId } into cs
                                //             select new { cs.Key.requisitionDetailId, cs.Key.requisitionId, qty = cs.Sum(x => x.cd.qty) }) on rd.Id equals csd.requisitionDetailId 
                                //    //where rd.requisitionMasterId == reqId && !reqDetailids.Contains(rd.Id) && rd.reqQty > (iudd.qty ?? 0)
                                //where rd.requisitionMasterId == reqId  && rd.reqQty > (iu.qty ?? 0)+ (csd.qty ?? 0)
                                select new RequisitionDetailViewModel
                                {
                                    Id = rd.Id,
                                    requisitionMasterId = rd.requisitionMasterId,
                                    reqQty = rd.reqQty,
                                    reqRate = rd.reqRate,
                                    targetDate = rd.targetDate,
                                    justification = rd.justification,
                                    teamMemberId = rd.teamMemberId,
                                    jobAssignDate = rd.jobAssignDate,
                                    itemSpecificationId = rd.itemSpecificationId,
                                    specificationName = isp.specificationName,
                                    itemId = i.Id,
                                    itemName = i.itemName,
                                    itemCode = i.itemCode,
                                    unitId = u.Id,
                                    unitName = u.unitName,
                                    unOrderQty = rd.reqQty - (ccss.qty ?? 0) - (iuss.qty ?? 0),
                                    orderQty = ccss.qty ?? 0 + iuss.qty ?? 0
                                }).AsNoTracking().Where(x => x.requisitionMasterId == reqId).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<RequisitionDetail>> GetRequisitionDetailListByReqId(int reqId)
        {
            return await _context.RequisitionDetails.Include(x => x.itemSpecification.Item.unit).Where(x => x.requisitionMasterId == reqId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<RequisitionDetail>> GetRequisitionDetailByitemIdandspec(int ItemId, int spec)
        {
            return await _context.RequisitionDetails.AsNoTracking().Where(x => x.itemSpecification.itemId == ItemId && x.itemSpecificationId == spec).ToListAsync();
        }

        public async Task<IEnumerable<RequisitionDetail>> GetRequisitionDetailByitemIdandspecandReqId(int ItemId, int spec, int reqId)
        {
            return await _context.RequisitionDetails.AsNoTracking().Where(x => x.itemSpecification.itemId == ItemId && x.itemSpecificationId == spec && x.requisitionMasterId == reqId).ToListAsync();
        }

        public async Task<RequisitionDetail> GetRequisitionDetailById(int id)
        {
            return await _context.RequisitionDetails.Where(x => x.Id == id).Include(x => x.requisitionMaster).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteRequisitionDetailById(int id)
        {
            _context.RequisitionDetails.Remove(_context.RequisitionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteRequisitionDetailByreqId(int reqId)
        {
            _context.RequisitionDetails.RemoveRange(_context.RequisitionDetails.Where(x => x.requisitionMasterId == reqId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<RequisitionAutoNumberViewModel> GetReqAutoNumberBySp(int projectId, string reqBy)
        {
            var data = _context.GetReqAutoNumberBySp.FromSql($"AutoRequisitionNo {projectId},{reqBy}");
            return await data.FirstOrDefaultAsync();
        }

        public async Task<CumulativeGRQtyBySpecIdViewModel> GetCumulativeGRQtyBySpecId(int itemspecId)
        {
            var data = _context.cumulativeGRQtyBySpecIdViewModels.FromSql($"SP_CumulativeGRQtyBySpecId {itemspecId}");
            return await data.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RequisitionDetailsWithCSViewModel>> GetRequisitionDetailListForBuyer(int reqId, int userId)
        {
            var result = await (from d in _context.RequisitionDetails
                                join ispec in _context.ItemSpecifications on d.itemSpecificationId equals ispec.Id
                                join i in _context.Items on ispec.itemId equals i.Id
                                join u in _context.Units on i.unitId equals u.Id
                                join r in _context.RequisitionMasters on d.requisitionMasterId equals r.Id
                                join rs in _context.StatusInfos on r.statusInfoId equals rs.Id
                                //join tm in _context.TeamMembers on d.teamMemberId equals tm.Id
                                join tm in _context.TeamMembers on d.teamMemberId equals tm.Id into TMM
                                from team in TMM.DefaultIfEmpty()
                                join csd in (from cm in _context.CSMasters
                                             join cd in _context.CSDetails on cm.Id equals cd.cSMasterId
                                             where cm.requisitionId == reqId
                                             group new { cm, cd } by new { cm.requisitionId, cd.requisitionDetailId } into cs
                                             select new { cs.Key.requisitionDetailId, cs.Key.requisitionId, qty = cs.Sum(x => x.cd.qty) }) on d.Id equals csd.requisitionDetailId into cos
                                from ccss in cos.DefaultIfEmpty()
                                join iou in (from um in _context.IOUMasters
                                             join ud in _context.IOUDetails on um.Id equals ud.IOUId
                                             where ud.requisitionId == reqId
                                             group new { um, ud } by new { ud.requisitionId, ud.requisitionDetailId } into cs
                                             select new { cs.Key.requisitionDetailId, cs.Key.requisitionId, qty = cs.Sum(x => x.ud.qty) }) on d.Id equals iou.requisitionDetailId into ious
                                from iuss in ious.DefaultIfEmpty()
                                    //where d.requisitionMasterId == reqId && team.memberId == userId && Convert.ToDecimal(ccss.qty) + Convert.ToDecimal(iuss.qty) < Convert.ToDecimal(d.reqQty)
                                where d.requisitionMasterId == reqId && Convert.ToDecimal(ccss.qty) + Convert.ToDecimal(iuss.qty) < Convert.ToDecimal(d.reqQty)
                                select new RequisitionDetailsWithCSViewModel
                                {
                                    requisitionDetailId = d.Id,
                                    reqNo = r.reqNo,
                                    reqDate = r.reqDate,
                                    itemCode = i.itemCode,
                                    itemName = i.itemName,
                                    itemId = i.Id,
                                    itemSpecificationId = ispec.Id,
                                    itemSpecificationName = ispec.specificationName,
                                    unitName = u.unitName,
                                    reqDept = r.reqDept,
                                    reqStatus = rs.StatusName,
                                    teamMemberId = d.teamMemberId,
                                    reqQty = d.reqQty,
                                    reqRate = d.reqRate,
                                    csQty = Convert.ToDecimal(ccss.qty),
                                    unOrderQty = d.reqQty - Convert.ToDecimal(ccss.qty) + Convert.ToDecimal(iuss.qty),
                                    justification = d.justification,
                                    targetDate = d.targetDate,
                                }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<DashboardVM>> GetTopFiveUsedItem(int userId)
        {
            var result = await (from rd in _context.RequisitionDetails
                                join r in _context.RequisitionMasters on rd.requisitionMasterId equals r.Id
                                join s in _context.ItemSpecifications on rd.itemSpecificationId equals s.Id
                                join i in _context.Items on s.itemId equals i.Id
                                where r.reqBy == userId
                                group new { rd, s, i } by new { rd.itemSpecificationId, s.specificationName, i.itemName } into g

                                select new DashboardVM
                                {
                                    totalValue = g.Sum(x => x.rd.reqQty * x.rd.reqRate),
                                    totalCount = g.Count(),
                                    itemName = g.Key.itemName + " (" + g.Key.specificationName + ")"
                                }).OrderByDescending(x => x.totalCount).Take(5).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<DashboardVM>> GetCurrentMonthPRStatus(int userId)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var result = await (from r in _context.RequisitionMasters
                                join rd in (from d in _context.RequisitionDetails
                                            group d by d.requisitionMasterId into g
                                            select new { requisitionMasterId = g.Key, value = g.Sum(x => x.reqQty * x.reqRate) }) on r.Id equals rd.requisitionMasterId
                                where r.reqBy == userId && r.reqDate >= startDate && r.reqDate <= now
                                select new DashboardVM
                                {
                                    requisitionMaster = r,
                                    totalValue = rd.value,
                                }).ToListAsync();
            return result;
        }



        #endregion

        #region Requisition Approve

        //public async Task<IEnumerable<RequisitionMaster>> GetRequisitionApproveList(int userId)
        //{
        //    return await _context.RequisitionMasters.FromSql(@"Sp_GetRequisitionListForApproved @p0", userId).AsNoTracking().ToListAsync();
        //}
        public async Task<IEnumerable<GetRequisitionListForApprovedViewModel>> GetRequisitionApproveList(int userId)
        {
            return await _context.getRequisitionListForApprovedViewModels.FromSql(@"Sp_GetRequisitionListForApproved @p0", userId).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<RequisitionTotalNumberForApproveViewModel>> GetRequisitionApproveCount(int userId)
        {
            return await _context.requisitionTotalNumberForApproveViewModels.FromSql(@"Sp_GetRequisitionListForApprovedCount @p0", userId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RequisitionDetail>> GetItemListByRequisitionId(int reqId)
        {
            var result = await _context.RequisitionDetails.Include(x => x.requisitionMaster).Include(a => a.itemSpecification.Item.unit).Where(x => x.requisitionMasterId == reqId).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequisitionGRCumulativeViewModel>> GetRequisitionByReqId(int reqId)
        {
            return await _context.requisitionGRCumulativeViewModels.FromSql($"SP_GetRequisitionDetailsByReqId {reqId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RequisitionGRCumulativeViewModel>> GetItemWiseCumutativeQTY(int itemId,int specId)
        {
            return await _context.requisitionGRCumulativeViewModels.FromSql($"SP_GetItemWiseCumutativeQTY {itemId},{specId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RequisitionDetail>> GetItemListByrfqId(int reqId)
        {
            var data = await _context.RFQReqDetails.Where(x => x.rFQMasterId == reqId).ToListAsync();
            List<int?> reqids = data.Select(x => x.requisitionMasterId).ToList();
            var result = await _context.RequisitionDetails.Include(x => x.requisitionMaster).Include(a => a.itemSpecification.Item.unit).Where(x => reqids.Contains(x.requisitionMasterId)).AsNoTracking().ToListAsync();
            return result;
        }
        public async Task<IEnumerable<RFQReqDetail>> GetRFQReqDetailListByrfqId(int reqId)
        {

            var result = await _context.RFQReqDetails.Include(x => x.requisitionMaster).Include(a => a.requisitionDetail.itemSpecification.Item.unit).Where(x => x.rFQMasterId == reqId).AsNoTracking().ToListAsync();
            return result;
        }

        public async Task<IEnumerable<RequisitionDetailVM>> GetAllRequisitionDetailListByReqId(int reqId)
        {
            return await _context.requisitionDetailVMs.FromSql(@"SP_GetMasterWiseRequisitionDetails @p0", reqId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrderSuppReportViewModel>> PurchaseOrderSuppReportViewModels(DateTime fromdate, DateTime todate, int supplierId)
        {
            return await _context.purchaseOrderSuppReportViewModels.FromSql($"getPObyDateRange {Convert.ToDateTime(fromdate).ToString("yyyyMMdd")}, {Convert.ToDateTime(todate).ToString("yyyyMMdd")},{supplierId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrderItemReportViewModel>> PurchaseOrderItemReportViewModels(DateTime fromdate, DateTime todate, int itemId)
        {
            return await _context.purchaseOrderItemReportViewModels.FromSql($"getPObyItem {Convert.ToDateTime(fromdate).ToString("yyyyMMdd")}, {Convert.ToDateTime(todate).ToString("yyyyMMdd")},{itemId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<PurchaseOrderProjectReportViewModel>> PurchaseOrderProjectReportViewModels(DateTime fromdate, DateTime todate, int projectId)
        {
            return await _context.purchaseOrderProjectReportViewModels.FromSql($"getPObyProject {Convert.ToDateTime(fromdate).ToString("yyyyMMdd")}, {Convert.ToDateTime(todate).ToString("yyyyMMdd")},{projectId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PurchaseOrderDiffReportViewModel>> PurchaseOrderDiffReportViewModels(DateTime fromdate, DateTime todate)
        {
            return await _context.purchaseOrderDiffReportViewModels.FromSql($"getPORefDiffbyDateRange {Convert.ToDateTime(fromdate).ToString("yyyyMMdd")}, {Convert.ToDateTime(todate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<PurchaseOrderBuyerReportViewModel>> PurchaseOrderBuyerReportViewModels(DateTime fromdate, DateTime todate)
        {
            return await _context.purchaseOrderBuyerReportViewModels.FromSql($"getPObyBuyer {Convert.ToDateTime(fromdate).ToString("yyyyMMdd")}, {Convert.ToDateTime(todate).ToString("yyyyMMdd")}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RequisitionDetailViewModel>> GetAllRequisitionDetailListByRequisitionId(int reqId)
        {
            var result = await (from D in _context.RequisitionDetails
                                join M in _context.RequisitionMasters on D.requisitionMasterId equals M.Id
                                join S in _context.ItemSpecifications on D.itemSpecificationId equals S.Id
                                join I in _context.Items on S.itemId equals I.Id
                                join U in _context.Units on I.unitId equals U.Id
                                let poQuantity =
                                          (from pd in _context.PurchaseOrderDetails
                                           join pm in _context.PurchaseOrderMasters on pd.purchaseId equals pm.Id
                                           where M.Id == pm.cSMaster.requisitionId && M.projectId == pm.cSMaster.requisition.projectId
                                           group pd by new { pd.cSDetail.requisitionDetail.itemSpecification.itemId, pd.cSDetail.requisitionDetail.requisitionMaster.projectId } into gpo
                                           select gpo.Sum(x => x.poQty)
                                          )
                                let iouQuantity =
                                           (from id in _context.IOUDetails
                                            join im in _context.IOUMasters on id.IOUId equals im.Id
                                            where M.Id == im.requisition.Id && M.projectId == im.requisition.projectId
                                            group id by new { id.requisitionDetail.itemSpecification.itemId, id.requisitionDetail.requisitionMaster.projectId } into gpo
                                            select gpo.Sum(x => x.qty)
                                           )

                                let lastPricePO =
                                         (from pd in _context.PurchaseOrderDetails
                                          join pm in _context.PurchaseOrderMasters on pd.purchaseId equals pm.Id
                                          where M.Id == pm.cSMaster.requisitionId && M.projectId == pm.cSMaster.requisition.projectId
                                          select pd.poRate
                                          ).LastOrDefault()
                                let lastPriceIOU =
                                          (from pd in _context.IOUDetails
                                           join pm in _context.IOUMasters on pd.IOUId equals pm.Id
                                           where M.Id == pm.requisition.Id && M.projectId == pm.requisition.projectId
                                           select pd.rate
                                          ).LastOrDefault()
                                where D.requisitionMasterId == reqId
                                select new RequisitionDetailViewModel
                                {
                                    Id = D.Id,
                                    specificationName = S.specificationName,
                                    itemCode = I.itemCode,
                                    itemName = I.itemName,
                                    unitName = U.unitName,
                                    reqQty = D.reqQty,
                                    reqRate = D.reqRate,
                                    teamMasterId = M.teamMasterId,
                                    total = (D.reqQty * D.reqRate),
                                    qumQTY = Convert.ToDecimal(poQuantity) + Convert.ToDecimal(iouQuantity),
                                    lastPrice = Convert.ToDecimal(lastPricePO) == 0 ? Convert.ToDecimal(lastPriceIOU) : Convert.ToDecimal(lastPricePO)
                                }).ToListAsync();

            return result;
        }

        public async Task<IEnumerable<System.Object>> GetAllItemListByRequisitionId(int reqId)
        {
            var result = (from D in _context.RequisitionDetails
                          join M in _context.RequisitionMasters on D.requisitionMasterId equals M.Id
                          join S in _context.ItemSpecifications on D.itemSpecificationId equals S.Id
                          join I in _context.Items on S.itemId equals I.Id
                          join U in _context.Units on I.unitId equals U.Id
                          where D.requisitionMasterId == reqId
                          select new
                          {
                              D.Id,
                              S.specificationName,
                              I.itemCode,
                              I.itemName,
                              U.unitName,
                              D.reqQty,
                              D.reqRate,
                              M.teamMasterId,
                              total = (D.reqQty * D.reqRate),
                          }).ToListAsync();

            return await result;
        }



        public async Task<System.Object> GetApprovalLogByRequisitionId(int reqId)
        {
            //var empInfo = await _context.employeeInfoViewModels.FromSql(@"sp_Get_PayrollEmployeeInfo").AsNoTracking().ToListAsync();

            var result = (from AL in _context.ApprovalLogs
                          join RM in _context.RequisitionMasters on AL.masterId equals RM.Id
                          join U in _context.Users on RM.reqBy equals U.userId
                          join E in _context.employeeInfos on U.Id equals E.ApplicationUserId
                          where RM.Id == reqId
                          select new
                          {
                              RM.Id,
                              RM.reqNo,
                              reqDate = Convert.ToDateTime(RM.reqDate).ToString("dd/MM/yyyy"),
                              E.nameEnglish,
                              RM.justification,
                              RM.subject,
                              targetDate = Convert.ToDateTime(RM.targetDate).ToString("dd/MM/yyyy")
                          }).OrderBy(x => x.reqNo).FirstOrDefaultAsync();

            return await result;
        }

        public async Task<System.Object> GetRequisitorInfoByRequisitionId(int reqId)
        {
            var empInfo = await _context.employeeInfoViewModels.FromSql(@"sp_Get_PayrollEmployeeInfo").AsNoTracking().ToListAsync();

            var result = (from RM in _context.RequisitionMasters
                          join SF in _context.StatusInfos on RM.statusInfoId equals SF.Id
                          join U in _context.Users on RM.reqBy equals U.userId
                          join E in empInfo on U.EmpCode equals E.EmpCode
                          where RM.Id == reqId
                          select new
                          {
                              RM.Id,
                              RM.reqNo,
                              reqDate = Convert.ToDateTime(RM.reqDate).ToString("dd/MM/yyyy"),
                              RM.reqBy,
                              SF.StatusName,
                              RM.justification,
                              RM.subject,
                              RM.reqDept,
                              E.EmpName,
                              targetDate = Convert.ToDateTime(RM.targetDate).ToString("dd/MM/yyyy")
                          }).OrderBy(x => x.reqNo).FirstOrDefaultAsync();

            return await result;
        }
        #endregion
        #region PO Report Press Club
        public async Task<IEnumerable<POSupplierReportViewModel>> POSuppReportViewModels(DateTime fromdate, DateTime todate, int supplierId)
        {
            return await _context.pOSupplierReportViewModels.FromSql($"getPObyDateRange {Convert.ToDateTime(fromdate).ToString("yyyyMMdd")}, {Convert.ToDateTime(todate).ToString("yyyyMMdd")},{supplierId}").AsNoTracking().ToListAsync();
        }
        #endregion
        #region DocumentAttachment
        public async Task<int> SaveDocumentAttachment(DocumentAttachment documentAttachment)
        {
            if (documentAttachment.Id != 0)
            {
                documentAttachment.updatedBy = documentAttachment.createdBy;
                documentAttachment.updatedAt = DateTime.Now;
                _context.DocumentAttachments.Update(documentAttachment);
            }
            else
            {
                documentAttachment.createdAt = DateTime.Now;
                _context.DocumentAttachments.Add(documentAttachment);
            }

            await _context.SaveChangesAsync();
            return documentAttachment.Id;
        }

        public async Task<IEnumerable<DocumentAttachment>> GetDocumentAttachmentList(int masterId)
        {
            return await _context.DocumentAttachments.AsNoTracking().Where(x => x.masterId == masterId).ToListAsync();
        }

        public async Task<IEnumerable<DocumentAttachment>> GetAttachmentListByMatrixType(int masterId, int matrixTypeId)
        {
            return await _context.DocumentAttachments.AsNoTracking().Where(x => x.masterId == masterId && x.matrixTypeId == matrixTypeId).ToListAsync();
        }

        public async Task<DocumentAttachment> GetDocumentAttachmentById(int id)
        {
            return await _context.DocumentAttachments.FindAsync(id);
        }

        public async Task<bool> DeleteDocumentAttachmentById(int id)
        {
            _context.DocumentAttachments.Remove(_context.DocumentAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteDocumentAttachmentBymasterId(int masterId, int matrixTypeId)
        {
            _context.DocumentAttachments.RemoveRange(_context.DocumentAttachments.Where(x => x.masterId == masterId && x.matrixTypeId == matrixTypeId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region DocumentAttachmentdetail
        public async Task<int> SaveDocumentAttachmentDetails(DocumentAttachmentDetail documentAttachment)
        {
            if (documentAttachment.Id != 0)
            {
                documentAttachment.updatedBy = documentAttachment.createdBy;
                documentAttachment.updatedAt = DateTime.Now;
                _context.documentAttachmentDetails.Update(documentAttachment);
            }
            else
            {
                documentAttachment.createdAt = DateTime.Now;
                _context.documentAttachmentDetails.Add(documentAttachment);
            }

            await _context.SaveChangesAsync();
            return documentAttachment.Id;
        }

        public async Task<IEnumerable<DocumentAttachmentDetail>> GetDocumentAttachmentDetailList(int detailid, int itemspecid)
        {
            return await _context.documentAttachmentDetails.AsNoTracking().Where(x => x.requisitionDetailId == detailid && x.itemSpecificationId == itemspecid).ToListAsync();
        }
        public async Task<IEnumerable<DocumentAttachmentDetail>> GetDocumentAttachmentDetailListreqid(int reqId)
        {
            return await _context.documentAttachmentDetails.AsNoTracking().Where(x => x.requisitionDetail.requisitionMasterId == reqId).ToListAsync();
        }


        #endregion

        #region PreferableVendor
        public async Task<int> SavePreferableVendor(PreferableVendor preferableVendor)
        {
            if (preferableVendor.Id != 0)
            {
                preferableVendor.updatedBy = preferableVendor.createdBy;
                preferableVendor.updatedAt = DateTime.Now;
                _context.PreferableVendors.Update(preferableVendor);
            }
            else
            {
                preferableVendor.createdAt = DateTime.Now;
                _context.PreferableVendors.Add(preferableVendor);
            }

            await _context.SaveChangesAsync();
            return preferableVendor.Id;
        }

        public async Task<IEnumerable<PreferableVendor>> GetPreferableVendorList(int masterId)
        {
            return await _context.PreferableVendors.AsNoTracking().Include(x => x.requisitionDetail).Include(x => x.organization).Include(x => x.requisitionDetail.itemSpecification.Item).Include(x => x.requisitionDetail.itemSpecification).Where(x => x.requisitionDetail.requisitionMasterId == masterId).ToListAsync();
        }

        public async Task<PreferableVendor> GetPreferableVendorById(int id)
        {
            return await _context.PreferableVendors.FindAsync(id);
        }

        public async Task<bool> DeletePreferableVendorById(int id)
        {
            _context.PreferableVendors.Remove(_context.PreferableVendors.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletePreferableVendorByreqId(int reqId)
        {
            _context.PreferableVendors.RemoveRange(_context.PreferableVendors.Where(x => x.requisitionDetail.requisitionMasterId == reqId));
            return 1 == await _context.SaveChangesAsync();
        }


		#endregion

		#region requisitionassign
		//public async Task<int> SaveRequisitionAssign(RequisitionAssignToTeamLead requisitionAssignToTeamLead)
		//{
		//    if (requisitionAssignToTeamLead.Id != 0)
		//    {
		//        requisitionAssignToTeamLead.updatedBy = requisitionAssignToTeamLead.createdBy;
		//        requisitionAssignToTeamLead.updatedAt = DateTime.Now;
		//        _context.re.Update(requisitionAssignToTeamLead);
		//    }
		//    else
		//    {
		//        requisitionDetail.createdAt = DateTime.Now;
		//        _context.RequisitionDetails.Add(requisitionDetail);
		//    }

		//    await _context.SaveChangesAsync();
		//    return requisitionDetail.Id;
		//}
		#endregion

		public async Task<int> GetCreatedRequisition(string statusName)
		{
			int id = await _context.StatusInfos.Where(x => x.StatusName == statusName).Select(s => s.Id).FirstOrDefaultAsync();
			return await _context.RequisitionMasters.Where(p => p.statusInfoId == id).CountAsync();
		}

		public async Task<IEnumerable<MostRecentRequisitionViewModel>> GetMostRecentReq()
		{
			var data = await _context.mostRecentRequisitions.FromSql($"sp_GetMostRecentReqItems").ToListAsync();
			return data;
		}
        public async Task<IEnumerable<TopTenRequisitionViewModel>> GetTopTenReq(int dptId)
        {
            var data = await _context.topTenRequisition.FromSql($"sp_GetMostRecentReqItems {dptId}").ToListAsync();
            return data;
        }

        public async Task<IEnumerable<TopTenRequisitionViewModel>> GetTopTenReq()
        {
            var data = await _context.topTenRequisition.FromSql($"sp_GetMostRecentReqItems").ToListAsync();
            return data;
        }

        public async Task<IEnumerable<FeaturedItemViewModel>> GetFeaturedReq()
		{
			var data = await _context.featuredRequisition.FromSql($"sp_FeaturedReqItems").ToListAsync();
			return data;
		}
        public async Task<IEnumerable<MostRecentRequisitionNewViewModel>> GetMostRecentRequisition(int dptId, string username)
        {
            var userid = _context.Users.Where(x => x.UserName == username).Select(x => x.userId).FirstOrDefault();
            var data = await _context.mostRecentRequisitionsNew.FromSql($"sp_GetMostRecentReqItemsWithFav {dptId}, {userid}").ToListAsync();
            return data;
        }
        public async Task<IEnumerable<FeaturedItemViewModel>> GetFeaturedReq(string username, int id)
        {
            var userid = _context.Users.Where(x => x.UserName == username).Select(x => x.userId).FirstOrDefault();
            var data = await _context.featuredRequisition.FromSql($"sp_FeaturedReqItemsByUserId {userid}, {id}").ToListAsync();
            return data;
        }
    }
}
