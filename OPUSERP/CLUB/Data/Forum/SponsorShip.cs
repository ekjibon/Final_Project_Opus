using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Forum
{
    [Table("SponsorShip", Schema = "Club")]
    public class SponsorShip:Base
    {
        public string companyName { get; set; }
        public string link { get; set; }
        public string sponsorInfo { get; set; }
        public string details { get; set; }
        public DateTime? date { get; set; }
    }
}
