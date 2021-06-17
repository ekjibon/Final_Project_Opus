using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.VMS.Models;
using OPUSERP.Data.Entity.AttachmentComment;

namespace OPUSERP.Areas.VMS.Models
{
    public class VehicleInformationViewModel
    {
        public int? vehicleId { get; set; }

        public int? vehicleTypeId { get; set; }

        public string vehicleName { get; set; }

        public string vinNo { get; set; }

        public string licensePlate { get; set; }

        public string vehicleYear { get; set; }

        public string trim { get; set; }

        public int? vehicleManufactureId { get; set; }

        public int? vehicleModelId { get; set; }

        public int? vehicleStatusId { get; set; }

        public int? vehicleGroupId { get; set; }

        public string registrationStateArea { get; set; }

        public string operatorId { get; set; }

        public int? vehicleOwnershipId { get; set; }

        public string color { get; set; }

        public int? vehicleBodyTypeId { get; set; }

        public int? vehicleBodySubTypeId { get; set; }

        public decimal? msrPrice { get; set; }

        public int? vehicleLinkedLId { get; set; }

        public string actionType { get; set; }

        #region spec
        public int? vehicleSpecificationId { get; set; }
        public decimal? width { get; set; }
        public decimal? height { get; set; }
        public decimal? length { get; set; }
        public decimal? interiorVolume { get; set; }
        public decimal? passengerVolume { get; set; }
        public decimal? cargoVolume { get; set; }
        public decimal? groundClearance { get; set; }
        public decimal? bedLength { get; set; }
        public decimal? curbWeight { get; set; }
        public decimal? grossVehicleWeightRating { get; set; }
        public decimal? towingCapacity { get; set; }
        public decimal? maxPayload { get; set; }
        public decimal? epaCity { get; set; }
        public decimal? epaHighway { get; set; }
        public decimal? epaCombined { get; set; }
        
        #endregion

        #region vehicleenginetransmission
        public int? vehicleenginetransmissionId { get; set; }
        public int? aspirationId { get; set; }
      
        public int? blockTypeId { get; set; }
      
        public int? camTypeId { get; set; }
       
        public int? fuelInductionId { get; set; }
       
        public int? transmissionTypeId { get; set; }
       
        public string engineSummary { get; set; }
        
        public string engineBrand { get; set; }
        public decimal? bore { get; set; }
        public decimal? compression { get; set; }
        public decimal? cylinders { get; set; }
        public decimal? displacement { get; set; }
       
        public string fuelQuality { get; set; }
        public decimal? maxHp { get; set; }
        public decimal? maxTorque { get; set; }
        
        public string redLineRpm { get; set; }
        public decimal? stroke { get; set; }
        public decimal? valves { get; set; }
        public string transmissionSummary { get; set; }
       
        public string transmissionBrand { get; set; }
        public decimal? transmissionGears { get; set; }

        #endregion

        #region vehiclewheeltire
        public int? vehicleWheelTireId { get; set; }
        public int? driveTypeId { get; set; }
        public int? brakeSystemId { get; set; }
        public decimal? frontTrackWidth { get; set; }
        public decimal? rearTrackWidth { get; set; }
        public decimal? wheelBase { get; set; }
        
        public string frontWheelDiameter { get; set; }
      
        public string rearWheelDiameter { get; set; }
      
        public string rearAxle { get; set; }
        
        public string frontTireType { get; set; }
        public decimal? frontTirePsi { get; set; }
        
        public string rearTireType { get; set; }
        public decimal? rearTirePsi { get; set; }
        #endregion

        #region vehiclefluid
        public int?[] vehiclefluidId { get; set; }
        public int?[] fuelTypeId { get; set; }
       
        public string[] fuelQualityfluid { get; set; }
        //public decimal? fuelTank1Capacity { get; set; }
        //public decimal? fuelTank2Capacity { get; set; }
        //public decimal? oilCapacity { get; set; }
 
        public string[] fuelTankNo { get; set; }
        //public decimal? fuelTank1Capacity { get; set; }
        //public decimal? fuelTank2Capacity { get; set; }
        public decimal?[] Capacity { get; set; }
        #endregion

        public DashboardDataModel Issues { get; set; }
        public DashboardDataModel ServiceReminder { get; set; }
        public IEnumerable<VehicleType> vehicleTypes { get; set; }
        public IEnumerable<VehicleModel> vehicleModels { get; set; }
        public IEnumerable<VehicleManufacture> vehicleManufactures { get; set; }
        public IEnumerable<VehicleStatus> vehicleStatuses { get; set; }
        public IEnumerable<VehicleGroup> vehicleGroups { get; set; }
        public IEnumerable<VehicleOwnership> vehicleOwnerships { get; set; }
        public IEnumerable<VMSResource> resources { get; set; }
        public IEnumerable<VehicleBodyType> vehicleBodyTypes { get; set; }
        public IEnumerable<VehicleBodySubType> vehicleBodySubTypes { get; set; }
        public IEnumerable<Aspiration> aspirations { get; set; }
        public IEnumerable<BlockType> blockTypes { get; set; }
        public IEnumerable<CamType> camTypes { get; set; }
        public IEnumerable<FuelInduction> fuelInductions { get; set; }
        public IEnumerable<TransmissionType> transmissionTypes { get; set; }
        public IEnumerable<DriveType> driveTypes { get; set; }
        public IEnumerable<BrakeSystem> brakeSystems { get; set; }
        public IEnumerable<FuelType> fuelTypes { get; set; }
        public IEnumerable<VehicleInformation> vehicleInformation { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<VehicleComment> vehicleComments { get; set; }
        public IEnumerable<OPUSERP.VMS.Models.CommentsViewModel> commentsViews { get; set; }
        public VehicleInformation VehicleInformationbyid { get; set; }
        public IEnumerable<VehicleFluid> vehicleFluid { get; set; }
        public VehicleWheelTire vehicleWheelTire { get; set; }
        public VehicleEngineTransmission vehicleEngineTransmission { get; set; }
        public VehicleSpecification vehicleSpecification { get; set; }
        public Odometer odometer { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }

        //public VehicleLN vlang { get; set; }
    }
}
