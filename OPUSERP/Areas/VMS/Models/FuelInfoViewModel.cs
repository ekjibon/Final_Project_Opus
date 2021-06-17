using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.FuelInfo;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.Data.Entity.AttachmentComment;

namespace OPUSERP.Areas.VMS.Models
{
    public class FuelInfoViewModel
    {
        public int fuelInformationId { get; set; }
        public int vehicleInformationId { get; set; }
        public int? fuelStationId { get; set; }
        public string odometer { get; set; }
        public DateTime? fuelTakenDate { get; set; }
        public string referenceNo { get; set; }
        public int fuelCommentId { get; set; }
        public string comment { get; set; }

        public int?[] fuelType { get; set; }
        public decimal?[] fuelQty { get; set; }
        public decimal?[] fuelRate { get; set; }
        public IFormFile formFileImage { get; set; }
        public IFormFile formFileDoc { get; set; }
        public string[] headColumn { get; set; }
        public string[] dataColumn { get; set; }


        //public FuelInfoLN flang { get; set; }
        public IEnumerable<FuelInformation>  fuelInformation { get; set; }
        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }
        public IEnumerable<FuelType>  fuelTypes { get; set; }
        public IEnumerable<FuelStationInfo> fuelStations { get; set; }
        public IEnumerable<FuelDetail>  fuelDetails { get; set; }
        public IEnumerable<FuelComment> fuelComments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<FuelInfoReportViewModel> fuelInfoReportViewModels { get; set; }
        public FuelInformation getfuelinfobyId { get; set; }
        public VehicleInformation vehicleInformationbyId { get; set; }


    } 
}
