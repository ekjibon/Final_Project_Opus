using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.POS.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.MasterData
{
    [Table("StoreAddress", Schema = "Purchase")]
    public class StoreAddress : Base
    {
        public int? addressCategoryId { get; set; }
        public AddressType addressCategory { get; set; }

        public int? storeId { get; set; }
        public Store store { get; set; }

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

        public string type { get; set; }  

    }
}
