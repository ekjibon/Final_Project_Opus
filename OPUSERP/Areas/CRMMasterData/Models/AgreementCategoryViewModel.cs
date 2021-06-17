using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class AgreementCategoryViewModel
    {
        public int Id { get; set; }

        [Required]
        public string agreementCategoryName { get; set; }

        public IEnumerable<AgreementCategory> agreementCategories { get; set; }
    }
}
