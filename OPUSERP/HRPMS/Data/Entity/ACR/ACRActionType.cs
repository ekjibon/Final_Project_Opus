using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("ACRActionType", Schema = "HR")]
    public class ACRActionType:Base
    {
        public string acrType { get; set; }
    }
}
