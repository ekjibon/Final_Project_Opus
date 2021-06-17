using OPUSERP.CLUB.Data.Forum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Forum.Models
{
    public class SponsorShipViewModel
    {
        public int Id { get; set; }
        public string companyName { get; set; }
        public string link { get; set; }
        public string sponsorInfo { get; set; }
        public string details { get; set; }
        public DateTime? date { get; set; }

        public IEnumerable<SponsorShip> sponsorShips { get; set; }
    }
}
