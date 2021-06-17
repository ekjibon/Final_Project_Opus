using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Hall
{
    public class HallInfo : Base
    {
        public string hallName { get; set; }
        public string hallNameBn { get; set; }
        public string floor { get; set; }
        public int seatCapacity { get; set; }
    }
}
