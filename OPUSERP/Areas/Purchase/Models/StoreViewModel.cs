using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.Requisition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models
{
    public class StoreViewModel
    {
        #region store
        public int? storeId { get; set; }
        public string storeName { get; set; }

        public int? storeTypeId { get; set; }

        public string licenseNumber { get; set; }

        public string email { get; set; }

        public string alternativeEmail { get; set; }

        public string mobile { get; set; }

        public string phone { get; set; }
        #endregion

        #region contacts
        public int? contactid { get; set; }
        public string resourceName { get; set; }
        public int designationId { get; set; }
        public string[] phoneall { get; set; }
        public string[] mobileall { get; set; }
        public string[] contactall { get; set; }
        public string[] deptall { get; set; }
        public string[] desall { get; set; }

        #endregion

        #region Address
        public int?[] orgAddressID { get; set; }

        public int?[] addressCategoryId { get; set; }

        public int?[] orgDivision { get; set; }

        public int?[] orgDistrict { get; set; }

        public int?[] orgUpazila { get; set; }

        public string[] orgUnion { get; set; }

        public string[] orgPostOffice { get; set; }

        public string[] orgPostCode { get; set; }

        public string[] orgBlockSector { get; set; }

        public string[] orgHouseVillage { get; set; }

        public string orgType { get; set; }
        #endregion


        public string actionType { get; set; }

        public IEnumerable<DocumentAttachment> photoes { get; set; }
        public IEnumerable<DocumentAttachment> documents { get; set; }
        public Store GetStoreById { get; set; }
        public StoreAddress GetAddress { get; set; }
        public IEnumerable<Store> stores { get; set; }
        public IEnumerable<StoreType> storeTypes { get; set; }
        public IEnumerable<StoreContact> storeContacts { get; set; }
        public IEnumerable<StoreAddress> storeAddresses { get; set; }
        public IEnumerable<Designation> designations { get; set; }
        public IEnumerable<AddressType> addressCategories { get; set; }
    }
}
