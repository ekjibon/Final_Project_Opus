using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.POS.Data.Entity;
using OPUSERP.POS.Services.POS.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS
{
    public class PosCollectionMasterService : IPosCollectionMasterService
    {
        private readonly ERPDbContext _context;

        public PosCollectionMasterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PosCollectionMaster>> GetPosCollectionMaster()
        {
            return await _context.PosCollectionMaster.AsNoTracking().ToListAsync();
        }

        public async Task<PosCollectionMaster> GetPosCollectionMasterById(int id)
        {
            return await _context.PosCollectionMaster.FindAsync(id);
        }

        public async Task<int> SavePosCollectionMaster(PosCollectionMaster posCollectionMaster)
        {
            if (posCollectionMaster.Id != 0)
                _context.PosCollectionMaster.Update(posCollectionMaster);
            else
                _context.PosCollectionMaster.Add(posCollectionMaster);
            await _context.SaveChangesAsync();
            return posCollectionMaster.Id;
        }

        public async Task<bool> DeletePosCollectionMasterById(int id)
        {
            _context.PosCollectionMaster.Remove(_context.PosCollectionMaster.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<PosBillReturnPaymentMaster>> GetBillReturnPaymentMaster()
        {
            return await _context.posBillReturnPaymentMasters.AsNoTracking().Include(x => x.PosCustomer).ToListAsync();
        }
        public async Task<PosBillReturnPaymentMaster> GetBillReturnPaymentbyMasterId(int Id)
        {
            return await _context.posBillReturnPaymentMasters.Where(x => x.Id == Id).AsNoTracking().Include(x => x.PosCustomer).FirstOrDefaultAsync();
        }
        public async Task<int> SaveReturnBillPaymentMaster(PosBillReturnPaymentMaster billPaymentMaster)
        {
            try
            {
                if (billPaymentMaster.Id != 0)
                {
                    billPaymentMaster.updatedBy = billPaymentMaster.createdBy;
                    billPaymentMaster.updatedAt = DateTime.Now;
                    _context.posBillReturnPaymentMasters.Update(billPaymentMaster);
                }
                else
                {
                    billPaymentMaster.createdAt = DateTime.Now;
                    _context.posBillReturnPaymentMasters.Add(billPaymentMaster);
                }

                await _context.SaveChangesAsync();
                return billPaymentMaster.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<int> SaveBillReturnPaymentDetail(PosBillReturnPaymentDetail billPaymentDetail)
        {
            try
            {
                if (billPaymentDetail.Id != 0)
                {
                    billPaymentDetail.updatedBy = billPaymentDetail.createdBy;
                    billPaymentDetail.updatedAt = DateTime.Now;
                    _context.posBillReturnPaymentDetails.Update(billPaymentDetail);
                }
                else
                {
                    billPaymentDetail.createdAt = DateTime.Now;
                    _context.posBillReturnPaymentDetails.Add(billPaymentDetail);
                }

                await _context.SaveChangesAsync();
                return billPaymentDetail.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
