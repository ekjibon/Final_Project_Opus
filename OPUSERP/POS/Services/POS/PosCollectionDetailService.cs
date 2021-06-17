using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.POS.Data.Entity;
using OPUSERP.POS.Services.POS.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS
{
    public class PosCollectionDetailService: IPosCollectionDetailService
    {
        private readonly ERPDbContext _context;

        public PosCollectionDetailService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PosCollectionDetail>> GetPosCollectionDetail()
        {
            return await _context.posCollectionDetails.AsNoTracking().ToListAsync();
        }

        public async Task<PosCollectionDetail> GetPosCollectionDetailById(int id)
        {
            return await _context.posCollectionDetails.FindAsync(id);
        }

        public async Task<int> SavePosCollectionDetail(PosCollectionDetail posCollectionDetail)
        {
            if (posCollectionDetail.Id != 0)
                _context.posCollectionDetails.Update(posCollectionDetail);
            else
                _context.posCollectionDetails.Add(posCollectionDetail);
            await _context.SaveChangesAsync();
            return posCollectionDetail.Id;
        }

        public async Task<bool> DeletePosCollectionDetailById(int id)
        {
            _context.posCollectionDetails.Remove(_context.posCollectionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();


        }
    }
}
