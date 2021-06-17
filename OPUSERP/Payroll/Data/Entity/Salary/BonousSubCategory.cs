using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("BonousSubCategory", Schema = "Payroll")]
    public class BonousSubCategory : Base
    {
        public int? bonousCategoryId { get; set; }
        public BonousCategory bonousCategory { get; set; }
        [MaxLength(50)]
        public string bonousSubCategoryName { get; set; }

        public int? isActive { get; set; }
    }
}
