using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.HRPMS.Data.Entity.ACR
{
    [Table("PhysicalProperty", Schema = "HR")]
    public class PhysicalProperty:Base
    {
        public string physicalType { get; set; }
        public string physicalValue { get; set; }
        public string physicalUnit { get; set; }
        public string remarks { get; set; }
    }
}
