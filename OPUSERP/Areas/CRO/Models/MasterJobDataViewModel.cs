using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRO.Models
{
    public class MasterJobDataViewModel
    {
        public IEnumerable<JobStatus> jobStatuses { get; set; }
        public IEnumerable<Leads> leads { get; set; }
        public IEnumerable<MasterJobViewModel> masterJobViewModels { get; set; }
    }
}
