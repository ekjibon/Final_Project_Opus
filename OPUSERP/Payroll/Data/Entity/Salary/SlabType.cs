using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("SlabType", Schema = "Payroll")]
    public class SlabType : Base
    {
        [MaxLength(30)]
        public string slabTypeName { get; set; }    
    }
}
