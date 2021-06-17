using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class ApprovalViewModel
    {
        public int approvalMasterId { get; set; }
        public int? employeeInfoId { get; set; }
        public int? approvalTypeId { get; set; }

        public int?[] approverId { get; set; }
        public int?[] sortOrder { get; set; }
        public string[] status { get; set; }

        public IEnumerable<ApprovalMaster> approvalMasters { get; set; }
        public IEnumerable<ApprovalType> approvalTypes { get; set; }

        public string visualEmpCodeName { get; set; }
    }
}
