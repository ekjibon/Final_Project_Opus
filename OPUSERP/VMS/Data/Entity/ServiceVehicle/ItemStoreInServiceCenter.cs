using OPUSERP.Data.Entity;
using OPUSERP.VMS.Data.Entity.CarManagement;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.ServiceVehicle
{
    [Table("ItemStoreInServiceCenter", Schema = "VMS")]
    public class ItemStoreInServiceCenter : Base
    {
        public int? vehicleServiceHistoryId { get; set; }
        public VehicleServiceHistory vehicleServiceHistory { get; set; }

        
        public int? sparePartsId { get; set; }
        public SpareParts spareParts { get; set; }
        public decimal? quantity { get; set; }
        
    }
}
