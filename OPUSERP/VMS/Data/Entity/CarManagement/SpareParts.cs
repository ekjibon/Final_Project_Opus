using OPUSERP.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.CarManagement
{
    [Table("SpareParts", Schema = "VMS")]
    public class SpareParts:Base
    {
        public string brandName { get; set; }

        public string modelName { get; set; }

        public string partsName { get; set; }
    }
}
