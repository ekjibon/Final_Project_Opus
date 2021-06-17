using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class HRDonerViewModel
    {
        public int? Id { get; set; }

        public string name { get; set; }

        public string code { get; set; }

        public string contactNumber { get; set; }

        public string Address { get; set; }

        public IEnumerable<HRDoner> hRDoners { get; set; }
    }
}
