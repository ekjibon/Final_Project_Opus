using OPUSERP.Rental.Data.Entity.Sales;
using System.Collections.Generic;

namespace OPUSERP.Areas.Rental.Models
{
    public class TermsAndConditionsViewModel
    {
        public int termId { get; set; }
        public string termName { get; set; }
        public int? shortOrder { get; set; }

        public IEnumerable<TermsAndConditions> termsAndConditions { get; set; }
       
    }
}
