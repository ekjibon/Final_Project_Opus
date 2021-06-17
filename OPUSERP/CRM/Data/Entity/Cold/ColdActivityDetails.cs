using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Cold
{
    [Table("ColdActivityDetails", Schema = "CRM")]
    public class ColdActivityDetails:Base
    {
        public int coldActivityMastersId { get; set; }
        public ColdActivityMasters coldActivityMasters { get; set; }

    
        public string contactName { get; set; }
    }
}
