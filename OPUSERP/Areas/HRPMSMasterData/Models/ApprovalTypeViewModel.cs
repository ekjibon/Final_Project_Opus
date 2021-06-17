using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSMasterData.Models
{
    public class ApprovalTypeViewModel
    {
        public int approvalTypeId { get; set; }
        public string approvalTypeName { get; set; }
        

        public IEnumerable<ApprovalType> approvalTypes { get; set; }
    }
}
