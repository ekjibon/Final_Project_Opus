using OPUSERP.CLUB.Data.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Forum.Interface
{
   public interface ISponsorshipService
    {
        Task<bool> SaveSponsorShip(SponsorShip sponsorship);
        Task<IEnumerable<SponsorShip>> GetSponsorShip();
        Task<SponsorShip> GetSponsorShipById(int id);
        Task<bool> DeleteSponsorShip(int id);
    }
}
