using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.MasterData.Models
{
    public class CompanyGroupViewModel
    {
        public int companyGroupId { get; set; }

        public string compGroupName { get; set; }

        public IEnumerable<CompanyGroup> companyGroups { get; set; }
    }
}
