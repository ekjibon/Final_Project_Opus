using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class FunctionViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string branchCode { get; set; }
        public int? companyId { get; set; }

        public IEnumerable<FunctionInfo> functionInfos { get; set; }
        public IEnumerable<Company> companies { get; set; }
    }
}
