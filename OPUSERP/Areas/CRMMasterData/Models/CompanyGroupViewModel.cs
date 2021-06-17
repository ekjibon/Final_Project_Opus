using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class CompanyGroupViewModel
    {
        public int Id { get; set; }

        [Required]
        public string companyGroupName { get; set; }

        public IEnumerable<CompanyGroup> companyGroups { get; set; }
    }
}
