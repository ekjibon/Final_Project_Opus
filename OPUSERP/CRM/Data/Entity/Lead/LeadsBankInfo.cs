using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("LeadsBankInfo", Schema = "CRM")]
    public class LeadsBankInfo : Base
    {
        public int? leadsId { get; set; }
        public Leads leads { get; set; }       

        public int? bankBranchDetailsId { get; set; }
        public BankBranchDetails bankBranchDetails { get; set; }
       
        [MaxLength(50)]
        public string bankType { get; set; }
        public int? crmdepartmentsId { get; set; }
        public CRMDepartment crmdepartments { get; set; }

        public int? crmdesignationsId { get; set; }
        public CRMDesignation crmdesignations { get; set; }
        public string contactName { get; set; }
        public string mobile { get; set; }
        public string email { get; set; }

    }
}
