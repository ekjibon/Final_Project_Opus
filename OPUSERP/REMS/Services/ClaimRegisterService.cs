using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.REMS.Models;
using OPUSERP.Data;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.REMS.Services
{
    public class ClaimRegisterService: IClaimRegisterService
    {
        private readonly ERPDbContext _context;

        public ClaimRegisterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveClaimRegister(ClaimRegister claim)
        {
            if (claim.Id != 0)
            {
                _context.ClaimRegisters.Update(claim);
            }
            else
            {
                _context.ClaimRegisters.Add(claim);
            }

            await _context.SaveChangesAsync();
            return claim.Id;
        }

        public void UpdateClaimRegister(int id, int status)
        {
            var user = _context.ClaimRegisters.Find(id);
            user.statusId = status;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public async Task<IEnumerable<ClaimRegister>> GetClaimRegisterList(int userId)
        {
            return await _context.ClaimRegisters
                .OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<ClaimRegister>> GetClaimRegisterListByStatus(int status)
        {
            return await _context.ClaimRegisters
                .OrderByDescending(x => x.Id).Where(x=>x.statusId==status).Include(x=>x.assetRegistration.itemSpecification.Item).Include(x=>x.assetRegistration.purchaseInfo.supplier).AsNoTracking().ToListAsync();
        }

        public async Task<ClaimRegister> GetClaimRegisterById(int id)
        {
            return await _context.ClaimRegisters.Where(x=>x.Id == id).Include(x=>x.assetRegistration.itemSpecification.Item).Include(x => x.assetRegistration.purchaseInfo.supplier).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<AssetRegistration>> GetAssetRegistration()
        {
            var asssetRegIds = await _context.AssetAssigns.Select(x => x.assetRegistrationId).ToListAsync();
            return await _context.AssetRegistration.AsNoTracking().Where(x=>asssetRegIds.Contains(x.Id)).Include(x => x.itemSpecification.Item).Include(x => x.purchaseInfo.supplier).ToListAsync();
        }

        public async Task<AssetWarrenty> GetAssetWarrenty(int id)
        {
            return await _context.AssetWarrenties.AsNoTracking().Where(x=>x.assetRegistrationId==id).FirstOrDefaultAsync();
        }

        public async Task<AssetAssign> GetAssetAssign(int id)
        {
            return await _context.AssetAssigns.AsNoTracking().Where(x=>x.assetRegistrationId==id).Include(x=>x.deliveryLocation).Include(x=>x.department).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<RegisterAssetViewModel>> GetAssetRegisterByUserId(int userId)
        {
            var result = await (from ar in _context.AssetRegistration
                                join p in _context.PurchaseOrderMasters on ar.purchaseInfoId equals p.Id
                                join spec in _context.ItemSpecifications on ar.itemSpecificationId equals spec.Id
                                join i in _context.Items on spec.itemId equals i.Id
                                join o in _context.Organizations on p.supplierId equals o.Id
                                select new RegisterAssetViewModel
                                {
                                    id = ar.Id,
                                    assetNo = ar.assetNo,
                                    poNo = p.poNo,
                                    itemName = i.itemName,
                                    poDate = p.poDate,
                                    dept = "HR",
                                    supplierCode = o.orgCode,
                                    supplierName=o.organizationName,
                                    warrentyStartOn=DateTime.Now,
                                    warrentyFinishedAt=DateTime.Now,
                                    isWarrenty=1,
                                    location=string.Empty,
                                }).ToListAsync();

            return result;

        }
    }
}
