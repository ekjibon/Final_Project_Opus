using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Rental.Data.Entity.Sales
{
    [Table("TermsAndConditions", Schema = "Rental")]
    public class TermsAndConditions : Base
    {        
        public string termName { get; set; }
        public int? shortOrder { get; set; }
    }
}
