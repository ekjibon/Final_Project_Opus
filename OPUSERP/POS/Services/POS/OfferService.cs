using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.POS.Data.Entity;
using OPUSERP.POS.Services.POS.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.POS.Services.POS
{
    public class OfferService : IOfferService
    {
        private readonly ERPDbContext _context;

        public OfferService(ERPDbContext context)
        {
            _context = context;
        }

        #region Offer Master

        public async Task<IEnumerable<OfferMaster>> GetOfferMaster()
        {
            return await _context.OfferMasters.AsNoTracking().ToListAsync();
        }

        public async Task<OfferMaster> GetOfferMasterById(int id)
        {
            return await _context.OfferMasters.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveOfferMaster(OfferMaster offerMaster)
        {
            if (offerMaster.Id != 0)
                _context.OfferMasters.Update(offerMaster);
            else
                _context.OfferMasters.Add(offerMaster);
            await _context.SaveChangesAsync();
            return offerMaster.Id;
        }

        public async Task<bool> DeleteOfferMasterById(int id)
        {
            _context.OfferMasters.Remove(_context.OfferMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }


        #endregion

        #region Offer Details

        public async Task<IEnumerable<OfferDetails>> GetOfferDetails()
        {
            return await _context.OfferDetails.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<OfferDetails>> GetOfferDetailsByMasterId(int masterId)
        {
            return await _context.OfferDetails.Where(x => x.offerMasterId == masterId).Include(x => x.itemPriceFixing.itemSpecification.Item).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<OfferDetails>> GetOfferDetailsByspecId(int specid)
        {
            var spec = await _context.ItemPriceFixings.Include(x => x.itemSpecification.Item).Where(x => x.Id == specid).FirstOrDefaultAsync();

            return await _context.OfferDetails.Include(x => x.itemPriceFixing.itemSpecification.Item).Include(x => x.offerMaster).Where(x => x.offerMaster.barCode == spec.barCode).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteOfferDetailsByMasterId(int masterId)
        {
            _context.OfferDetails.RemoveRange(_context.OfferDetails.Where(x => x.offerMasterId == masterId).ToList());
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<OfferDetails> GetOfferDetailsById(int id)
        {
            return await _context.OfferDetails.FindAsync(id);
        }

        public async Task<bool> SaveOfferDetails(OfferDetails offerDetails)
        {
            if (offerDetails.Id != 0)
                _context.OfferDetails.Update(offerDetails);
            else
                _context.OfferDetails.Add(offerDetails);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteOfferDetailsById(int id)
        {
            _context.OfferDetails.Remove(_context.OfferDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
    }
}
