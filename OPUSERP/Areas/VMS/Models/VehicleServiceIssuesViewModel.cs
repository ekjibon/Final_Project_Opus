using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data.Entity.AttachmentComment;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleServiceIssuesViewModel
    {
        public int? vehicleserviceissueId { get; set; }
        public int? vehicleInformationId { get; set; }
  

        public DateTime? reportedDate { get; set; }
        public string summary { get; set; }
        public string description { get; set; }
        public decimal? odometerValue { get; set; }
        public string reportedBy { get; set; }
        public string assignedTo { get; set; }
        public DateTime? dueDate { get; set; }
        public decimal? overdueOdometerValue { get; set; }
        public IFormFile formFileImage { get; set; }
        public IFormFile formFileDoc { get; set; }
        public int? vehicleInformationIdIssue { get; set; }




        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }






        public VehicleInformation VehicleInformationbyid { get; set; }
        public VehicleServiceIssue vehicleServiceIssuebyId { get; set; }
        public Odometer odometers { get; set; }
        public IEnumerable<VehicleServiceIssue> vehicleServiceIssues { get; set; }

        public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }
        public IEnumerable<VehicleServiceIssueComment> vehicleServiceIssueComments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }




    }
}
