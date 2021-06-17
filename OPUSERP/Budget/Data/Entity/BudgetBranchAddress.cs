using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Budget.Data.Entity
{
    [Table("BudgetBranchAddress", Schema = "Budget")]
    public class BudgetBranchAddress:Base
    {
        public int? addressTypeId { get; set; }
        public AddressType addressType { get; set; }

        public int? budgetBranchId { get; set; }
        public SpecialBranchUnit budgetBranch { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }

        public int? divisionId { get; set; }
        public Division division { get; set; }

        public int? districtId { get; set; }
        public District district { get; set; }

        public int? thanaId { get; set; }
        public Thana thana { get; set; }

        public string union { get; set; }

        public string postOffice { get; set; }

        public string postCode { get; set; }

        public string blockSector { get; set; }

        public string houseVillage { get; set; }
    }
}
