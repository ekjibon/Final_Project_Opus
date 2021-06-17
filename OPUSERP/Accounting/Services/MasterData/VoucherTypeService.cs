using Microsoft.EntityFrameworkCore;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Accounting.Services.MasterData
{
    public class VoucherTypeService : IVoucherTypeService
    {
        private readonly ERPDbContext _context;

        public VoucherTypeService(ERPDbContext context)
        {
            _context = context;
        }

        #region VoucherType

        public async Task<IEnumerable<VoucherType>> GetVoucherType()
        {
            List<int> obj = new List<int> { 1, 6, 7, 8 };
            return await _context.VoucherTypes.Where(x => obj.Contains(x.Id)).AsNoTracking().ToListAsync();
        }

        public async Task<VoucherType> GetVoucherTypeById(int id)
        {
            return await _context.VoucherTypes.FindAsync(id);
        }

        public async Task<bool> SaveVoucherType(VoucherType voucherType)
        {
            if (voucherType.Id != 0)
                _context.VoucherTypes.Update(voucherType);
            else
                _context.VoucherTypes.Add(voucherType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteVoucherTypeById(int id)
        {
            _context.VoucherTypes.Remove(_context.VoucherTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Transection Mode

        public async Task<IEnumerable<TransectionMode>> GetTransectionMode()
        {
            return await _context.TransectionModes.AsNoTracking().ToListAsync();
        }

        #endregion
    }
}
