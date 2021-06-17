using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.Requisition;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.Data.Entity.AttachmentComment;

namespace OPUSERP.Areas.VMS.Models
{
    public class RequisitionViewModel
    {
        public string reqNo { get; set; }

        public string reqBy { get; set; }

        public string reqDate { get; set; }

        public string reqTime { get; set; }

        public int? vehicleTypeId { get; set; }

        public string actionType { get; set; }
        public string vehicleCondition { get; set; }

        public string vehicleSeat { get; set; }
        public int? requisitionId { get; set; }
        public IFormFile formFileImage { get; set; }
        public IFormFile formFileDoc { get; set; }
        public string[] routingPlace { get; set; }

        public int?[] sequenseNo { get; set; }
        public int?[] requisitionDetailsId { get; set; }
        public string[] from { get; set; }
        public string[] to { get; set; }




        public IEnumerable<VehicleType> vehicleTypes { get; set; }
        public IEnumerable<VehicleModel> vehicleModels { get; set; }
        public IEnumerable<VehicleManufacture> vehicleManufactures { get; set; }
        public IEnumerable<VehicleStatus> vehicleStatuses { get; set; }
        public IEnumerable<VehicleGroup> vehicleGroups { get; set; }
        public IEnumerable<VehicleOwnership> vehicleOwnerships { get; set; }
        public IEnumerable<VMSResource> resources { get; set; }
        public IEnumerable<VehicleBodyType> vehicleBodyTypes { get; set; }
        public IEnumerable<VehicleBodySubType> vehicleBodySubTypes { get; set; }
       
        public IEnumerable<FuelType> fuelTypes { get; set; }
        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<RequisitionComment> requisitionComments { get; set; }
        public IEnumerable<CommentsViewModel> commentsViews { get; set; }
        public IEnumerable<VMSRequisitionMaster> requisitionMasters { get; set; }
        public VMSRequisitionMaster RequisitionMasterbyId { get; set; }
        public IEnumerable<VMSRequisitionDetails> RequisitionDetails { get; set; }
        public VMSRequisitionDetails RequisitionDetailsbyId { get; set; }
        
        public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }

        //public VehicleLN vlang { get; set; }
    }
}
