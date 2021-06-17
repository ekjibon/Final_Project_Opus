using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Inspection;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleInspectionViewModel
    {
        public int? inspectionMasterId { get; set; }
        public int? inspectiondetailId { get; set; }
        public int? vehicleInformationId { get; set; }
        public DateTime? inspectionDate { get; set; }

        public decimal? odometer { get; set; }
        public string signature { get; set; }
        public int?[] inspectionCategoryId { get; set; }
        public int[] result { get; set; }
        public string[] description { get; set; }
        public int?[] files { get; set; }
        //public IFormFile[] formFile { get; set; }






        public IEnumerable<InspectionMaster> inspectionMasters { get; set; }
        public InspectionMaster inspectionMastersbyId { get; set; }
        public IEnumerable<InspectionDetail> inspectionDetails { get; set; }
        public IEnumerable<InspectionCheckLIstCategory> inspectionCheckLIstCategories { get; set; }
        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }






    }
}
