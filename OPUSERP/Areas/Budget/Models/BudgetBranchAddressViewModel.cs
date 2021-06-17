using OPUSERP.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Budget.Models
{
    public class BudgetBranchAddressViewModel
    {
        public int? addressId { get; set; }

        public int? addressTypeId { get; set; }

        public int? budgetBranchId { get; set; }

        public int? countryId { get; set; }

        public int? divisionId { get; set; }

        public int? districtId { get; set; }

        public int? thanaId { get; set; }

        public string union { get; set; }

        public string postOffice { get; set; }

        public string postCode { get; set; }

        public string blockSector { get; set; }

        public string houseVillage { get; set; }

        public IEnumerable<AddressType> addressTypes { get; set; }
    }
}
