using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Payroll.Data.Entity.Salary
{
    [Table("WalletType", Schema = "Payroll")]
    public class WalletType : Base
    {
        [MaxLength(100)]
        public string walletTypeName { get; set; }    
    }
}
