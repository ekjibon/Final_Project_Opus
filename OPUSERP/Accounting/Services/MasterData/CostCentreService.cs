using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.MasterData
{
    public class CostCentreService : ICostCentreService
    {
        private readonly ERPDbContext _context;

        public CostCentreService(ERPDbContext context)
        {
            _context = context;
        }

        #region Cost Centre

        public async Task<int> SavecostCentre(CostCentre costCentre)
        {
            try
            {
                if (costCentre.Id != 0)
                {
                    _context.CostCentres.Update(costCentre);
                }
                else
                {
                    _context.CostCentres.Add(costCentre);
                }

                await _context.SaveChangesAsync();
                return costCentre.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<IEnumerable<CostCentre>> GetCostCentres()
        {
            return await _context.CostCentres.AsNoTracking().ToListAsync();
        }

        public async Task<CostCentre> GetcostCentreById(int id)
        {
            try
            {
                var record = await _context.CostCentres.Where(x => x.Id == id).FirstOrDefaultAsync();
                return record;
            }
            catch (Exception ex)
            {
                throw ex;

            }
        }

        public async Task<bool> DeletecostCentresById(int id)
        {
            _context.CostCentres.Remove(_context.CostCentres.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Auto Voucher Name

        public async Task<int> SaveAutoVoucherName(AutoVoucherName autoVoucherName)
        {
            if (autoVoucherName.Id != 0)
            {
                _context.AutoVoucherNames.Update(autoVoucherName);
            }
            else
            {
                _context.AutoVoucherNames.Add(autoVoucherName);
            }
            await _context.SaveChangesAsync();
            return autoVoucherName.Id;
        }

        public async Task<IEnumerable<AutoVoucherName>> GetAllAutoVoucherName()
        {
            return await _context.AutoVoucherNames.AsNoTracking().ToListAsync();
        }

        public async Task<AutoVoucherName> GetAutoVoucherNameId(int id)
        {
            return await _context.AutoVoucherNames.Where(x => x.Id == id).FirstOrDefaultAsync();            
        }

        public async Task<bool> DeleteAutoVoucherNameById(int id)
        {
            _context.AutoVoucherNames.Remove(_context.AutoVoucherNames.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Auto Voucher Master

        public async Task<int> SaveAutoVoucherMaster(AutoVoucherMaster autoVoucherMaster)
        {
            if (autoVoucherMaster.Id != 0)
            {
                _context.AutoVoucherMasters.Update(autoVoucherMaster);
            }
            else
            {
                _context.AutoVoucherMasters.Add(autoVoucherMaster);
            }
            await _context.SaveChangesAsync();
            return autoVoucherMaster.Id;
        }

        public async Task<IEnumerable<AutoVoucherMaster>> GetAllAutoVoucherMaster()
        {
            return await _context.AutoVoucherMasters.Include(a => a.autoVoucherName).Include(a => a.voucherType).Include(a => a.project.specialBranchUnit).OrderByDescending(a => a.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AutoVoucherMaster>> GetDuplicateAutoVoucherMaster(int masterId, int? nameId)
        {
            return await _context.AutoVoucherMasters.Where(x => x.Id != masterId && x.autoVoucherNameId == nameId).AsNoTracking().ToListAsync();
        }

        public async Task<AutoVoucherMaster> GetAutoVoucherMasterById(int id)
        {
            return await _context.AutoVoucherMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAutoVoucherMasterById(int id)
        {
            _context.AutoVoucherMasters.Remove(_context.AutoVoucherMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Auto Voucher Detail

        public async Task<int> SaveAutoVoucherDetail(AutoVoucherDetail autoVoucherDetail)
        {
            if (autoVoucherDetail.Id != 0)
            {
                _context.AutoVoucherDetails.Update(autoVoucherDetail);
            }
            else
            {
                _context.AutoVoucherDetails.Add(autoVoucherDetail);
            }
            await _context.SaveChangesAsync();
            return autoVoucherDetail.Id;
        }

        public async Task<IEnumerable<AutoVoucherDetail>> GetAutoVoucherDetailByMasterId(int masterId)
        {
            return await _context.AutoVoucherDetails.Include(a=>a.ledgerRelation.ledger).Include(a => a.transectionMode).Where(a => a.autoVoucherMasterId == masterId).AsNoTracking().ToListAsync();
        }

        public async Task<AutoVoucherDetail> GetAutoVoucherDetailById(int id)
        {
            return await _context.AutoVoucherDetails.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAutoVoucherDetailByMasterId(int masterId)
        {
            _context.AutoVoucherDetails.RemoveRange(_context.AutoVoucherDetails.Where(a=>a.autoVoucherMasterId== masterId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Auto Voucher SP FOR FDR/Investment

        public async Task<IEnumerable<DeleteVoucherViewModel>> FDRCreateAutoVoucher(decimal? amount, string fdrDate, string fdrNo, string createBy)
        {
            return await _context.deleteVoucherViewModels.FromSql($"SP_AUTOVOUCHER_FDRCREATE {amount},{Convert.ToDateTime(fdrDate).ToString("yyyyMMdd")},{fdrNo},{createBy}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DeleteVoucherViewModel>> FDREncashmentAutoVoucher(decimal? totalRecAmount, decimal? banhchrgAmount, decimal? taxAmount, decimal? fdrAmount, decimal? interestAmount, string fdrDate, string fdrNo, string createBy)
        {
            return await _context.deleteVoucherViewModels.FromSql($"SP_AUTOVOUCHER_FDRENCASH {totalRecAmount},{banhchrgAmount},{taxAmount},{fdrAmount},{interestAmount},{Convert.ToDateTime(fdrDate).ToString("yyyyMMdd")},{fdrNo},{createBy}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<DeleteVoucherViewModel>> FDRenewAutoVoucher(decimal? amount, string fdrDate, string fdrNo, string createBy)
        {
            return await _context.deleteVoucherViewModels.FromSql($"SP_AUTOVOUCHER_FDRRENEW {amount},{Convert.ToDateTime(fdrDate).ToString("yyyyMMdd")},{fdrNo},{createBy}").AsNoTracking().ToListAsync();
        }

        #endregion
    }
}
