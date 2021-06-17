using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.SCM.Data.Entity.Supplier
{
    [Table("Organization", Schema = "SCM")]
    public class Organization:Base
    {
        [StringLength(250)]
        public string organizationName { get; set; }
        [StringLength(250)]
        public string organizationNameBn { get; set; }
        [StringLength(50)]
        public string orgCode { get; set; }
        [StringLength(100)]
        public string LicenseNumber { get; set; }
        [StringLength(100)]
        public string eTinNumber { get; set; }
        [StringLength(100)]
        public string VATNumber { get; set; }
        [StringLength(100)]
        public string email { get; set; }
        [StringLength(100)]
        public string alternativeEmail { get; set; }
        [StringLength(100)]
        public string mobile { get; set; }
        [StringLength(100)]
        public string phone { get; set; }
        [StringLength(120)]
        public string website { get; set; }
        public int? ledgerId { get; set; }
        public Ledger ledger { get; set; }
    }
}
