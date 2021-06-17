using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Hall
{
    public class HallRentalShift : Base
    {
        public string shiftName { get; set; }
        public string shiftNameBn { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
    }
}
