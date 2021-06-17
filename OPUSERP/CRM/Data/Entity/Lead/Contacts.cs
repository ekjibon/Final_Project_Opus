using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("Contacts", Schema = "CRM")]
    public class Contacts : Base
    {
        public string contactOwner { get; set; }
        
        public int? resourceId { get; set; }
        public Resource resource { get; set; }
        
        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        public int? isLead { get; set; }
        [MaxLength(400)]
        public string departmentsName { get; set; }
        [MaxLength(400)]
        public string designationsName { get; set; }
        [MaxLength(400)]
        public string crmDepartmentsName { get; set; }
        [MaxLength(400)]
        public string crmDesignationsName { get; set; }
        

    }
}
