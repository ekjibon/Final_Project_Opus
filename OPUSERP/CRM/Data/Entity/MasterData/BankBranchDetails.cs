using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.MasterData
{
    [Table("BankBranchDetails", Schema = "CRM")]
    public class BankBranchDetails : Base
    {   
        public int? bankId { get; set; }
        public Bank bank { get; set; }

        public int? bankBranchId { get; set; }
        public BankBranch bankBranch { get; set; }

        public int? thanaId { get; set; }
        public Thana thana { get; set; }
        [MaxLength(100)]
        public string accountNo { get; set; }
        [MaxLength(350)]
        public string maillingAddress { get; set; }
        [MaxLength(100)]
        public string email { get; set; }
        [MaxLength(20)]
        public string fax { get; set; }
        [MaxLength(50)]
        public string website { get; set; }
        [MaxLength(15)]
        public string phone { get; set; }
        [MaxLength(16)]
        public string mobile { get; set; }

        [MaxLength(3)]
        public string collectionType { get; set; } //yes/No
    }
}
