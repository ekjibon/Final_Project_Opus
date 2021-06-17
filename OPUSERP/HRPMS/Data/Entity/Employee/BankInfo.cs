using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.Payroll.Data.Entity.Salary;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.Employee
{
    [Table("BankInfo", Schema = "HR")]
    public class BankInfo : Base
    {
        public int employeeId { get; set; }
        public EmployeeInfo employee { get; set; }

        public int? bankId { get; set; }
        public Bank bank { get; set; }

        public int? walletTypeId { get; set; }
        public WalletType walletType { get; set; }

        [MaxLength(200)]
        public string branchName { get; set; }
        [MaxLength(30)]
        public string accountNumber { get; set; }
        [MaxLength(50)]
        public string walletNumber { get; set; }
        [MaxLength(200)]
        public string ibus { get; set; }
    }
}