using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Data.Entity.MasterData
{
    public class UserBackup:Base
    {
        public string EmpCode { get; set; }
        public int? userTypeId { get; set; }
        public int? userId { get; set; }
        public int? companyId { get; set; }

        public string userName { get; set; }
        public string email { get; set; }
        public string AspnetId { get; set; }

        public decimal? MaxAmount { get; set; }

        public int? isActive { get; set; }
        public string org { get; set; }
        
        public int? specialBranchUnitId { get; set; }

        public string deleteBy { get; set; }
    }
}
