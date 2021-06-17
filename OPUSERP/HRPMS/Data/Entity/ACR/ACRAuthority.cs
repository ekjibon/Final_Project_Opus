using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("ACRAuthority", Schema = "HR")]
    public class ACRAuthority:Base
    {
        public string empDesignationName { get; set; }

        public string CADesignationName { get; set; }

        public string CSADesignationName { get; set; }

    }
}
