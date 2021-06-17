using OPUSERP.Areas.Auth.Models;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Models
{
    public class AssetTransferViewModel
    {
        public int? assetTransferId { get; set; }
        public int? assetRegistrationId { get; set; }  
        public int? deliveryLocationId { get; set; } 
        public int? departmentId { get; set; }
        public DateTime? transferDate { get; set; }
        public int? currentEmpId { get; set; }
        public int? previousEmpId { get; set; }
        public int? currentCoordinatorId { get; set; }
        public int? previousCoordinatorId { get; set; }
        public IEnumerable<Department> departments { get; set; }
        public IEnumerable<DeliveryLocation> deliveryLocations { get; set; }
        public IEnumerable<EmployeeInfoViewModel> employeeInfoViewModels { get; set; }
        public IEnumerable<AssetTransfer> assetTransfers { get; set; }
        public AssetTransfer assetTransfer { get; set; }
        public IEnumerable<AssetRegistration> assetRegistrations { get; set; }
        public Task<AssetRegistration> assetRegistration { get; set; }
    }
}
