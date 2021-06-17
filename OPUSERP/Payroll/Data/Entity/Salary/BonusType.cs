using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("BonusType", Schema = "Payroll")]
    public class BonusType : Base
    {
        [MaxLength(100)]
        public string bonusTypeName { get; set; }    
    }
}
