using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Data;
using OPUSERP.Models.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.Voucher
{
    public class VoucherService : IVoucherService
    {
        private readonly ERPDbContext _context;

        public VoucherService(ERPDbContext context)
        {
            _context = context;
        }
        #region Voucher Master 
        public async Task<int> SavevoucherMaster(VoucherMaster voucherMaster)
        {
            try
            {
                if (voucherMaster.Id != 0)
                {                   
                    _context.VoucherMasters.Update(voucherMaster);
                }
                else
                {                   
                    _context.VoucherMasters.Add(voucherMaster);
                }

                await _context.SaveChangesAsync();
                return voucherMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<VoucherMaster>> GetvoucherMaster()
        {
            return await _context.VoucherMasters.Include(x => x.fiscalYear).Include(X => X.fundSource).Include(X => X.voucherType).Include(x => x.taxYear).AsNoTracking().ToListAsync();
        }

        public IQueryable<VoucherMaster> GetvoucherMasterByTypeId(int TypeId)
        {
            return _context.VoucherMasters.Where(x => x.voucherTypeId == TypeId).Include(x => x.fiscalYear).Include(X => X.fundSource).Include(X => X.voucherType).Include(x => x.taxYear);
        }
        public async Task<IEnumerable<VoucherMaster>> GetvoucherMasterDetailsByTypeId(int TypeId)
        {
            try
            {
                List<VoucherMaster> record = new List<VoucherMaster>();
                if(TypeId==0)
                {
                    record = await (from vm in _context.VoucherMasters
                                    join vd in _context.VoucherDetails on vm.Id equals vd.voucherId
                                    join f in _context.salaryYears on vm.fiscalYearId equals f.Id
                                    join fs in _context.FundSources on vm.fundSourceId equals fs.Id
                                    join vt in _context.VoucherTypes on vm.voucherTypeId equals vt.Id
                                    join ty in _context.taxYears on vm.taxYearId equals ty.Id
                                    where  vd.isPrincAcc == 1
                                    select new VoucherMaster
                                    {
                                        Id = vm.Id,
                                        voucherNo = vm.voucherNo,
                                        voucherDate = vm.voucherDate,
                                        voucherAmount = vm.voucherAmount,
                                        voucherTypeId = vm.voucherTypeId,
                                        remarks = vm.remarks,
                                        isPosted = vm.isPosted,
                                        fiscalYearId = vm.fiscalYearId,
                                        taxYearId = vm.taxYearId,
                                        companyId = vm.companyId,
                                        fundSourceId = vm.fundSourceId,
                                        ledgerRelationId = vd.ledgerRelationId,
                                        accountName = vd.accountName,
                                        amount = vd.amount,
                                        refNo = vm.refNo,
                                        voucherType = vt,
                                        fiscalYear = f,
                                        taxYear = ty,
                                        fundSource = fs
                                    }).OrderByDescending(a => a.Id).ToListAsync();
                }
                else
                {
                    record = await (from vm in _context.VoucherMasters
                                    join vd in _context.VoucherDetails on vm.Id equals vd.voucherId
                                    join f in _context.salaryYears on vm.fiscalYearId equals f.Id
                                    join fs in _context.FundSources on vm.fundSourceId equals fs.Id
                                    join vt in _context.VoucherTypes on vm.voucherTypeId equals vt.Id
                                    join ty in _context.taxYears on vm.taxYearId equals ty.Id
                                    where vm.voucherTypeId == TypeId && vd.isPrincAcc == 1
                                    select new VoucherMaster
                                    {
                                        Id = vm.Id,
                                        voucherNo = vm.voucherNo,
                                        voucherDate = vm.voucherDate,
                                        voucherAmount = vm.voucherAmount,
                                        voucherTypeId = vm.voucherTypeId,
                                        remarks = vm.remarks,
                                        isPosted = vm.isPosted,
                                        fiscalYearId = vm.fiscalYearId,
                                        taxYearId = vm.taxYearId,
                                        companyId = vm.companyId,
                                        fundSourceId = vm.fundSourceId,
                                        ledgerRelationId = vd.ledgerRelationId,
                                        accountName = vd.accountName,
                                        amount = vd.amount,
                                        refNo = vm.refNo,
                                        voucherType = vt,
                                        fiscalYear = f,
                                        taxYear = ty,
                                        fundSource = fs
                                    }).OrderByDescending(a=>a.Id).ToListAsync();
                }



                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<VoucherMaster> GetGetvoucherMasterByJournalId(int id)
        {
            try
            {
                var record = await (from vm in _context.VoucherMasters
                                    join vd in _context.VoucherDetails on vm.Id equals vd.voucherId
                                    join f in _context.salaryYears on vm.fiscalYearId equals f.Id
                                    join fs in _context.FundSources on vm.fundSourceId equals fs.Id
                                    join vt in _context.VoucherTypes on vm.voucherTypeId equals vt.Id
                                    join ty in _context.taxYears on vm.taxYearId equals ty.Id
                                    where vm.Id == id
                                    select new VoucherMaster
                                    {
                                        Id = vm.Id,
                                        voucherNo = vm.voucherNo,
                                        voucherDate = vm.voucherDate,
                                        voucherAmount = vm.voucherAmount,
                                        voucherTypeId = vm.voucherTypeId,
                                        remarks = vm.remarks,
                                        isPosted = vm.isPosted,
                                        fiscalYearId = vm.fiscalYearId,
                                        taxYearId = vm.taxYearId,
                                        companyId = vm.companyId,
                                        fundSourceId = vm.fundSourceId,
                                        ledgerRelationId = vd.ledgerRelationId,
                                        accountName = vd.accountName,
                                        amount = vd.amount,
                                        refNo = vm.refNo,
                                        voucherType = vt,
                                        fiscalYear = f,
                                        taxYear = ty,
                                        fundSource = fs,
                                        projectId=vm.projectId
                                    }).FirstOrDefaultAsync();


                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<VoucherMaster> GetGetvoucherMasterById(int id)
        {
            try
            {
                var record = await (from vm in _context.VoucherMasters
                                    join vd in _context.VoucherDetails on vm.Id equals vd.voucherId
                                    join f in _context.salaryYears on vm.fiscalYearId equals f.Id
                                    join fs in _context.FundSources on vm.fundSourceId equals fs.Id
                                    join vt in _context.VoucherTypes on vm.voucherTypeId equals vt.Id
                                    join ty in _context.taxYears on vm.taxYearId equals ty.Id
                                    where vm.Id == id && vd.isPrincAcc.GetValueOrDefault(1) == 1
                                    select new VoucherMaster
                                    {
                                        Id = vm.Id,
                                        voucherNo = vm.voucherNo,
                                        voucherDate = vm.voucherDate,
                                        voucherAmount = vm.voucherAmount,
                                        voucherTypeId = vm.voucherTypeId,
                                        remarks = vm.remarks,
                                        refNo = vm.refNo,
                                        isPosted = vm.isPosted,
                                        fiscalYearId = vm.fiscalYearId,
                                        taxYearId = vm.taxYearId,
                                        companyId = vm.companyId,
                                        fundSourceId = vm.fundSourceId,
                                        ledgerRelationId = vd.ledgerRelationId,
                                        accountName = vd.accountName,
                                        amount = vd.amount,

                                        voucherType = vt,
                                        fiscalYear = f,
                                        taxYear = ty,
                                        fundSource = fs,
                                        projectId=vm.projectId
                                    }).FirstOrDefaultAsync();


                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public async Task<bool> DeletevoucherMasterById(int id)
        {
            _context.VoucherMasters.Remove(_context.VoucherMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateVoucherMasterStatus(int? id, int? isPosted, string updateBy)
        {
            var VoucherMasters = _context.VoucherMasters.Find(id);
            VoucherMasters.isPosted = isPosted;
            VoucherMasters.updatedBy = updateBy;
            VoucherMasters.updatedAt = DateTime.Now;
            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateVoucherNo(int? id, string newVoucherNo)
        {
            var VoucherMasters = _context.VoucherMasters.Find(id);
            VoucherMasters.voucherNo = newVoucherNo;
            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<VoucherMaster> GetVoucherMasterByRefNo(string refNo)
        {
            return await _context.VoucherMasters.Where(x => x.refNo == refNo).AsNoTracking().FirstOrDefaultAsync();
        }

        #endregion

        #region Voucher Details 
        public async Task<int> SavevoucherDetail(VoucherDetail voucherDetail)
        {
            try
            {
                if (voucherDetail.Id != 0)
                {                   
                    _context.VoucherDetails.Update(voucherDetail);
                }
                else
                {                   
                    _context.VoucherDetails.Add(voucherDetail);
                }

                await _context.SaveChangesAsync();
                return voucherDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<VoucherDetail>> GetvoucherDetail()
        {
            return await _context.VoucherDetails.Include(x => x.ledgerRelation).Include(X => X.transectionMode).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<VoucherDetail>> GetvoucherDetailByMasterIdEdit(int MasterId)
        {


            return await _context.VoucherDetails
                .Where(x => x.voucherId == MasterId && x.isPrincAcc.GetValueOrDefault(0) == 0)
                .Include(x => x.ledgerRelation.ledger)
                .Include(x => x.ledgerRelation.subLedger)
                .Include(X => X.transectionMode)
                .OrderBy(X => X.Id)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<VoucherDetail>> GetvoucherDetailByMasterId(int MasterId)
        {


            return await _context.VoucherDetails
                .Where(x => x.voucherId == MasterId)
                .Include(x => x.ledgerRelation.ledger.group)
                .Include(x => x.ledgerRelation.subLedger)
                .Include(X => X.transectionMode)
                .OrderBy(X => X.Id)
                .AsNoTracking().ToListAsync();
        }



        public async Task<VoucherDetail> GetGetvoucherDetailById(int id)
        {
            try
            {
                var record = await _context.VoucherDetails.Where(x => x.Id == id).Include(x => x.ledgerRelation).Include(X => X.transectionMode).FirstOrDefaultAsync();
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<IEnumerable<VoucherDetail>> GetGetvoucherDetailByLedgerRelId(int id)
        {
            try
            {
                var record = await _context.VoucherDetails.Where(x => x.ledgerRelationId == id).Include(x => x.ledgerRelation).Include(X => X.transectionMode).ToListAsync();
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }


        public async Task<bool> DeletevoucherDetailByVMId(int vmid)
        {
            _context.VoucherDetails.RemoveRange(_context.VoucherDetails.Where(x => x.voucherId == vmid));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Cost CEntre Allocation
        public async Task<int> SavecostCentreAllocation(CostCentreAllocation costCentreAllocation)
        {
            try
            {
                if (costCentreAllocation.Id != 0)
                {                   
                    _context.CostCentreAllocations.Update(costCentreAllocation);
                }
                else
                {                   
                    _context.CostCentreAllocations.Add(costCentreAllocation);
                }

                await _context.SaveChangesAsync();
                return costCentreAllocation.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<IEnumerable<CostCentreAllocation>> GetCostCentreAllocations()
        {
            return await _context.CostCentreAllocations.Include(x => x.voucherDetail).Include(X => X.costCentre).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<CostCentreAllocation>> GetCostCentreAllocationsByDetailId(int id)
        {
            var record = new List<CostCentreAllocation>();
            var recordV = new List<CostCentreAllocation>();
            try
            {
                var data = await _context.VoucherDetails.Where(x => x.voucherId == id).ToListAsync();
                foreach (var vdata in data)
                {
                    recordV = await _context.CostCentreAllocations.Where(x => x.voucherDetailId == vdata.Id).Include(x => x.voucherDetail).Include(X => X.costCentre).ToListAsync();
                    foreach (CostCentreAllocation costCentreAllocation in recordV)
                    {
                        record.Add(costCentreAllocation);
                    }

                }
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }
        public async Task<bool> DeleteCostCentreAllocationByVMId(int vmid)
        {
            var data = await _context.VoucherDetails.Where(x => x.voucherId == vmid).AsNoTracking().ToListAsync();
            for (int i = 0; i < data.Count(); i++)
            {
                _context.CostCentreAllocations.RemoveRange(_context.CostCentreAllocations.Where(x => x.voucherDetailId == data[i].Id));
            }
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Dashboard

        public async Task<IEnumerable<VoucherViewModel>> GetVoucherListDashboard(string userName)
        {
            IQueryable<VoucherViewModel> result = from vm in _context.VoucherMasters
                                                  join t in _context.VoucherTypes on vm.voucherTypeId equals t.Id
                                                  join vd in _context.VoucherDetails on vm.Id equals vd.voucherId
                                                  where vd.transectionModeId == 2
                                                  group new { vm, t, vd } by new { vm.voucherTypeId, t.voucherTypeName } into g
                                                  select new VoucherViewModel
                                                  {
                                                      voucherTypeId = g.Key.voucherTypeId,
                                                      voucherTypeName = g.Key.voucherTypeName,
                                                      totalCount = g.Count(),
                                                      amount = g.Sum(x => x.vd.amount)
                                                  };

            return await result.ToListAsync();

        }

        public async Task<IEnumerable<VoucherViewModel>> CountVoucherByType(string userName)
        {
            IQueryable<VoucherViewModel> result = from vm in _context.VoucherMasters
                                                  join t in _context.VoucherTypes on vm.voucherTypeId equals t.Id
                                                  group new { vm, t } by new { vm.voucherTypeId, t.voucherTypeName } into g
                                                  select new VoucherViewModel
                                                  {
                                                      voucherTypeId = g.Key.voucherTypeId,
                                                      voucherTypeName = g.Key.voucherTypeName,
                                                      totalCount = g.Count(),
                                                      amount = g.Sum(x => x.vm.amount)
                                                  };

            return await result.ToListAsync();

        }

        #endregion

        #region Voucher Setting 
        public async Task<int> SaveVoucherSetting(VoucherSetting voucherSetting)
        {
            try
            {
                if (voucherSetting.Id != 0)
                {                    
                    _context.VoucherSettings.Update(voucherSetting);
                }
                else
                {                   
                    _context.VoucherSettings.Add(voucherSetting);
                }

                await _context.SaveChangesAsync();
                return voucherSetting.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<VoucherSetting>> GetVoucherSetting()
        {
            return await _context.VoucherSettings.Include(x => x.paymentMode).Include(x => x.bankAccountLedger.ledger).Include(x => x.cashAccountLedger.ledger).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteVoucherSettingById(int id)
        {
            _context.VoucherSettings.Remove(_context.VoucherSettings.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Voucher Approve Log

        public async Task<int> SaveVoucherApproveLog(VoucherApproveLog voucherApproveLog)
        {
            try
            {
                if (voucherApproveLog.Id != 0)
                {
                    _context.VoucherApproveLogs.Update(voucherApproveLog);
                }
                else
                {
                    _context.VoucherApproveLogs.Add(voucherApproveLog);
                }

                await _context.SaveChangesAsync();
                return voucherApproveLog.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<VoucherApproveLog>> GetVoucherApproveLog()
        {
            return await _context.VoucherApproveLogs.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<VoucherApproveLog>> GetVoucherApproveLogByMasterId(int? masterId)
        {
            return await _context.VoucherApproveLogs
                .Where(x => x.voucherMasterId == masterId)  
                .OrderBy(X => X.Id)
                .AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<VoucherMaster>> GetNotApproveVoucherList()
        {            
            //List<int?> masterIds = _context.VoucherApproveLogs.Select(x => x.voucherMasterId).ToList();

            return await _context.VoucherMasters.Include(x => x.fiscalYear).Include(X => X.fundSource).Include(X => X.voucherType).Include(x => x.taxYear).ToListAsync();
        }

        #endregion

        #region Delete voucher
        public async Task<IEnumerable<GetVoucherListForDeleteViewModel>> GetVoucherListForDelete(string FDate, string TDate, string voucherNo)
        {
            return await _context.getVoucherListForDeleteViewModels.FromSql($"SP_GetVoucherListForDelete {Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")},{voucherNo}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GetVoucherListForDeleteViewModel>> GetVoucherListByVoucherNo(string voucherNo)
        {
            return await _context.getVoucherListForDeleteViewModels.FromSql($"SP_GetVoucherListByVoucherNo {voucherNo}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DeleteVoucherViewModel>> DeleteVoucher(int? voucherMasterId, string remarks, string user)
        {
            return await _context.deleteVoucherViewModels.FromSql($"SP_DeleteVoucher {voucherMasterId},{remarks},{user}").AsNoTracking().ToListAsync();
        }

        #endregion

        #region ReBack voucher
        public async Task<IEnumerable<GetVoucherListForDeleteViewModel>> GetVoucherListForReBack(string FDate, string TDate, string voucherNo)
        {
            return await _context.getVoucherListForDeleteViewModels.FromSql($"SP_GetVoucherListForReBack {Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")},{voucherNo}").AsNoTracking().ToListAsync();

        }
        
        #endregion

    }
}
