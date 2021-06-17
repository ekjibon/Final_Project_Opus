using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMSupplier.Models
{
    public class SupplierViewModel
    {
        #region Customer/Resource
        public int? resourceId { get; set; }
        public int relSupplierCustomerResourseId { get; set; }
        public int? organizationId { get; set; }
        public string nameEnglish { get; set; }
        public string orgCode { get; set; }
        public string organizationName { get; set; }
        public string LicenseNumber { get; set; }

        public string eTinNumber { get; set; }

        public string VATNumber { get; set; }

        public string email { get; set; }

        public string alternativeEmail { get; set; }

        public string mobile { get; set; }

        public string phone { get; set; }

        public string telephoneResidence { get; set; }

        public string pabx { get; set; }

        public string mobileNumberPersonal { get; set; }

        public string nationalID { get; set; }

        public string nationality { get; set; }
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

        #region Item Info
        public int?[] item { get; set; }
        public int?[] itemidall { get; set; }

        public int? customerItemInfoId { get; set; }
        public int? itemId { get; set; }
        public string itemName { get; set; }
        #endregion

        public IEnumerable<Organization> organizations { get; set; }


        public string actionType { get; set; }
        public Organization GetOrganizationbyId { get; set; }
        public Address GetAddress { get; set; }
        public IEnumerable<Item> items { get; set; }
        public IEnumerable<AddressType> addressTypes { get; set; }
    }
}
