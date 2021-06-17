using OPUSERP.Accounting.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class CostCentreViewModel
    {
        public int costCentreId { get; set; }
        public string centreName { get; set; }
        public string aliesName { get; set; }
        public int? companyId { get; set; }
        public int? departmentId { get; set; }
        public string centreCode { get; set; }

        public IEnumerable<CostCentre> costCentres { get; set; }       


    }
}
