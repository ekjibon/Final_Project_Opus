using OPUSERP.Areas.Auth.Models;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class AssetAssignViewModel
    {
        public int? assetAssignId { get; set; }
        public int? assetRegistrationId { get; set; } 
        public DateTime? assignDate { get; set; }
        public int? empId { get; set; }
        public int? coordinatorempId { get; set; }
        public int? departmentId { get; set; }
        public int? deliveryLocationId { get; set; }
        public string remarks { get; set; }

        public IEnumerable<Department> departments { get; set; }
        public IEnumerable<DeliveryLocation> deliveryLocations { get; set; }
        public IEnumerable<EmployeeInfoViewModel> employeeInfoViewModels { get; set; }
        public IEnumerable<AssetAssign> assetAssigns { get; set; }
        public AssetAssign assetAssign { get; set; }
        public IEnumerable<AssetRegistration> assetRegistrations { get; set; }
        public Task<AssetRegistration> assetRegistration { get; set; }
    }
}
