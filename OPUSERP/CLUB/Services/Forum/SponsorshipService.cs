using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Forum;
using OPUSERP.CLUB.Services.Forum.Interface;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Forum
{
    public class SponsorshipService: ISponsorshipService
    {
        private readonly ERPDbContext _context;

        public SponsorshipService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteSponsorShip(int id)
        {
            _context.sponsorShips.Remove(_context.sponsorShips.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<SponsorShip> GetSponsorShipById(int id)
        {
            return await _context.sponsorShips.FindAsync(id);
        }

        public async Task<IEnumerable<SponsorShip>> GetSponsorShip()
        {
            return await _context.sponsorShips.AsNoTracking().OrderByDescending(x => x.Id).ToListAsync();
        }

        public async Task<bool> SaveSponsorShip(SponsorShip sponsorShip)
        {
            if (sponsorShip.Id != 0)
                _context.sponsorShips.Update(sponsorShip);
            else
                _context.sponsorShips.Add(sponsorShip);
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
