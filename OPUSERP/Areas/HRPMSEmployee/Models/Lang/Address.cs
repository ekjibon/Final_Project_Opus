using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models.Lang
{
    public class Address
    {
        public string title { get; set; }
        public string sameAddress { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Upazila { get; set; }
        public string Union { get; set; }
        public string PostOffice { get; set; }
        public string PostCode { get; set; }
        public string BlockSector { get; set; }
        public string HouseVillage { get; set; }
        public string roadNo { get; set; }

        public string permanent { get; set; }
        public string present { get; set; }
    }
}
