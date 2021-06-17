using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.REMS.Models;
using OPUSERP.Data;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.REMS.Services
{
    public class ClaimAssignService: IClaimAssignService
    {
        private readonly ERPDbContext _context;

        public ClaimAssignService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveClaimAssign(ClaimAssign claimAssign)
        {
            if (claimAssign.Id != 0)
            {
                _context.ClaimAssigns.Update(claimAssign);
            }
            else
            {
                _context.ClaimAssigns.Add(claimAssign);
            }

            await _context.SaveChangesAsync();
            return claimAssign.Id;
        }

        public async Task<IEnumerable<ClaimAssign>> GetClaimAssign()
        {
            return await _context.ClaimAssigns.OrderByDescending(x => x.Id)
                .Include(x=>x.claim.assetRegistration.itemSpecification.Item).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ClaimAssign>> GetClaimAssignByUserId(int assignUserId)
        {
            return await _context.ClaimAssigns.Where(x=>x.assignUserId == assignUserId).OrderByDescending(x => x.Id)
                .Include(x=>x.claim.assetRegistration.itemSpecification.Item).Include(x=>x.organization).AsNoTracking().ToListAsync();
            
        }

        public async Task<bool> DeleteClaimAssignById(int id)
        {
            _context.ClaimAssigns.Remove(_context.ClaimAssigns.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ClaimAssignViewModel>> GetClaimAssignListByAssignType(int userId,int assignTypeId,int statusId)
        {
            var result =await (from c in _context.ClaimAssigns
                          join r in _context.ClaimRegisters on c.claimId equals r.Id
                          join ar in _context.AssetRegistration on r.assetRegistrationId equals ar.Id
                          join po in _context.PurchaseInfo on ar.purchaseInfoId equals po.Id
                          join o in _context.Organizations on po.supplierId equals o.Id
                          join s in _context.ClaimStatusInfos on r.statusId equals s.Id
                          join spec in _context.ItemSpecifications on ar.itemSpecificationId equals spec.Id
                          join i in _context.Items on spec.itemId equals i.Id
                          join a in _context.Users on c.assignUserId equals a.userId
                          join ae in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on a.userId equals ae.UserId
                          join ue in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on r.userId equals ue.UserId
                          where c.assignTypeId==assignTypeId && r.statusId==statusId && c.assignUserId==userId
                          select new ClaimAssignViewModel
                          {
                              id=c.Id,
                              userId = c.assignUserId,
                              claimId=r.Id,
                              claimUserId = ue.UserId,
                              claimUserName = ue.EmpName,
                              assetRegistrationId = ar.Id,
                              supplierId = o.Id,
                              suppCode = o.orgCode,
                              deliveryMode = c.deliveryMode,
                              supplierName = o.organizationName,
                              assignDate =c.assignDate,
                              problemDescription=c.problemDescription,
                              assetName=i.itemName+" ("+spec.specificationName+")",
                              empName=ae.EmpName,
                              remarks=c.remarks,
                              claimDate=r.claimDate,
                              claimDescription=r.claimDescription,
                              claimNumber=r.claimNumber,
                              assetNo=ar.assetNo,
                              statusId = r.statusId,
                              statusInfoName =s.statusName
                          }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ClaimAssignViewModel>> GetClaimAssignListByAssignUser(int userId, int assignTypeId)
        {
            var result = await (from c in _context.ClaimAssigns
                                join r in _context.ClaimRegisters on c.claimId equals r.Id
                                join ar in _context.AssetRegistration on r.assetRegistrationId equals ar.Id
                                join po in _context.PurchaseInfo on ar.purchaseInfoId equals po.Id
                                join o in _context.Organizations on po.supplierId equals o.Id
                                join s in _context.ClaimStatusInfos on r.statusId equals s.Id
                                join spec in _context.ItemSpecifications on ar.itemSpecificationId equals spec.Id
                                join i in _context.Items on spec.itemId equals i.Id
                                join a in _context.Users on c.assignUserId equals a.userId
                                join ae in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on a.userId equals ae.UserId
                                join ue in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on r.userId equals ue.UserId
                                where c.assignTypeId == assignTypeId //&& c.assignUserId==userId
                                select new ClaimAssignViewModel
                                {
                                    id = c.Id,
                                    claimId = r.Id,
                                    userId = c.assignUserId,
                                    assetRegistrationId = ar.Id,
                                    claimUserId = ue.UserId,
                                    claimUserName = ue.EmpName,
                                    supplierId = o.Id,
                                    suppCode = o.orgCode,
                                    supplierName = o.organizationName,
                                    assignDate = c.assignDate,
                                    problemDescription = c.problemDescription,
                                    assetName = i.itemName + " (" + spec.specificationName + ")",
                                    empName = ae.EmpName,
                                    remarks = c.remarks,
                                    claimDate = r.claimDate,
                                    claimDescription = r.claimDescription,
                                    claimNumber = r.claimNumber,
                                    assetNo = ar.assetNo,
                                    statusId=r.statusId,
                                    statusInfoName = s.statusName
                                }).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<ClaimAssignViewModel>> GetClaimAssignListByAssignTypeNew(int userId, int assignTypeId, int statusId)
        {
            var result = await (from c in _context.ClaimAssigns
                                join r in _context.ClaimRegisters on c.claimId equals r.Id
                                join ar in _context.AssetRegistration on r.assetRegistrationId equals ar.Id
                                join po in _context.PurchaseInfo on ar.purchaseInfoId equals po.Id
                                join o in _context.Organizations on po.supplierId equals o.Id
                                join s in _context.ClaimStatusInfos on r.statusId equals s.Id
                                join spec in _context.ItemSpecifications on ar.itemSpecificationId equals spec.Id
                                join i in _context.Items on spec.itemId equals i.Id
                                join a in _context.Users on c.assignUserId equals a.userId
                                join ae in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on a.userId equals ae.UserId
                                join ue in _context.aspNetUsersViews.FromSql(@"sp_GetUsersByEmployeeInfo") on r.userId equals ue.UserId
                                where c.assignTypeId == assignTypeId && r.statusId == statusId
                                select new ClaimAssignViewModel
                                {
                                    id = c.Id,
                                    userId = c.assignUserId,
                                    claimId = r.Id,
                                    claimUserId = ue.UserId,
                                    claimUserName = ue.EmpName,
                                    assetRegistrationId = ar.Id,
                                    supplierId = o.Id,
                                    suppCode = o.orgCode,
                                    deliveryMode = c.deliveryMode,
                                    supplierName = o.organizationName,
                                    assignDate = c.assignDate,
                                    problemDescription = c.problemDescription,
                                    assetName = i.itemName + " (" + spec.specificationName + ")",
                                    empName = ae.EmpName,
                                    remarks = c.remarks,
                                    claimDate = r.claimDate,
                                    claimDescription = r.claimDescription,
                                    claimNumber = r.claimNumber,
                                    assetNo = ar.assetNo,
                                    statusId = r.statusId,
                                    statusInfoName = s.statusName
                                }).ToListAsync();
            return result;
        }
    }
}
