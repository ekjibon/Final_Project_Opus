using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.VMS.Data.Entity.FuelInfo;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.Areas.VMS.Models
{
    public class FuelStationsInfoViewModel
    {
        #region Fuelstationinfo
        public int? fuelStationId { get; set; }
        public string fuelStationName { get; set; }
        public string ownerName { get; set; }
        public string managerName { get; set; }
        public string tradelicenseNo { get; set; }
        #endregion
        #region contacts
        public int? contactid { get; set; }
       public int? resourceId { get; set; }
        public string resourceName { get; set; }
        public string phone { get; set; }
        public int designationId { get; set; }
        public string mobile { get; set; }
        public string[] contactall { get; set; }
        public int?[] designationIdall { get; set; }
        public string[] phoneall { get; set; }
        public string[] mobileall { get; set; }
        public int?[] resouceidall { get; set; }
        public int?[] idall { get; set; }

        #endregion
        #region Address
        public int? orgAddressID { get; set; }
        [Display(Name = "Org Division")]
        public string orgDivision { get; set; }

        [Required]
        [Display(Name = "Org District")]
        public string orgDistrict { get; set; }

        [Required]
        [Display(Name = "Org Upazila")]
        public string orgUpazila { get; set; }

        [Required]
        [Display(Name = "Org Union")]
        public string orgUnion { get; set; }

        [Display(Name = "Org Post Office")]
        public string orgPostOffice { get; set; }

        [StringLength(50, ErrorMessage = "The {0} Must be at least {2} and at most {1} characters long.", MinimumLength = 3)]
        public string orgPostCode { get; set; }


        [Display(Name = "Org Block/Sector")]
        public string orgBlockSector { get; set; }

        [Display(Name = "Org House/Village")]
        public string orgHouseVillage { get; set; }

        public string orgType { get; set; }
        #endregion
        #region stationfuelinfo
        public int?[] fuelType { get; set; }
        public int?[] fuelQty { get; set; }
        public int?[] fuelidall { get; set; }


        public int? stationFuelInfoId { get; set; }
        public int? fuelTypeId { get; set; }
        public int? noOfNozzle { get; set; }
        #endregion
        public string actionType { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public IEnumerable<FuelStationComment> fuelStationComments { get; set; }
        public IEnumerable<FuelStationInfo> GetFuelStationInfos { get; set; }
        public FuelStationInfo GetFuelStationInfosbyId { get; set; }
        public VMSAddress GetAddress { get; set; }
        public IEnumerable<FuelType> fuelTypes { get; set; }
        public IEnumerable<Designation> designations { get; set; }
        //public FuelStationInfoLN vlang { get; set; }
    }
}
