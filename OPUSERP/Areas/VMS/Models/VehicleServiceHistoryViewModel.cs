using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data.Entity.AttachmentComment;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleServiceHistoryViewModel
    {
        public int vehicleServiceHistoryId { get; set; }
        public int vehicleInformationId { get; set; }
        public string odometer { get; set; }
        public int? supplierId { get; set; }
        public DateTime? completionDate { get; set; }
        public DateTime? startDate { get; set; }
        public string referenceNo { get; set; }
        public int? labelTypeId { get; set; }
        public string comment { get; set; }
        public string generalNotes { get; set; }
        public int serviceLabelId { get; set; }
        

        public decimal? subTotalSummary { get; set; }
        public decimal? laborTotal { get; set; }
        public decimal? partsTotal { get; set; }
        public string discountType { get; set; }
        public decimal? discount { get; set; }
        public string taxType { get; set; }
        public decimal? tax { get; set; }
        public decimal? total { get; set; }


        public int?[] serviceTaskId { get; set; }
        public decimal?[] labor { get; set; }
        public decimal?[] parts { get; set; }
        public decimal?[] subtotal { get; set; }

        public int?[] spearIds { get; set; }
        public decimal?[] quantitys { get; set; }

        public IEnumerable<VehicleServiceHistory>  vehicleServiceHistories { get; set; }
        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }
        public IEnumerable<VMSSupplier>  suppliers { get; set; }
        public IEnumerable<LabelType>  labelTypes { get; set; }
        public IEnumerable<ServiceTask>  serviceTasks { get; set; }
        public IEnumerable<VehicleStatus> vehicleStatuses { get; set; }
        public IEnumerable<ServiceLabel>  serviceLabels { get; set; }

        public VehicleServiceHistory vehicleServiceHistory { get; set; }
        public VehicleInformation VehicleInformationbyid { get; set; }
        public Odometer odometers { get; set; }
        public IEnumerable<VehicleLineItem>  vehicleLineItems { get; set; }

        
        public IEnumerable<VehicleServiceComment>  vehicleServiceComments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }

        public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }
        public IEnumerable<SpareParts> spareParts { get; set; }
        public IEnumerable<ItemStoreInServiceCenter> itemStoreInServiceCenters { get; set; }



    }
}
