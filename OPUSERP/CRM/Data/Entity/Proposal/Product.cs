using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Proposal
{
    [Table("Product", Schema = "CRM")]
    public class Product : Base
    {
        [MaxLength(250)]
        public string productName { get; set; }
        public string productDescription { get; set; }
    }
}
