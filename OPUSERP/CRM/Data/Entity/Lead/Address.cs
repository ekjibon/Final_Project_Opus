using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("Address", Schema = "CRM")]
    public class Address : Base
    {
        public int leadsId { get; set; }

        public int addressTypeId { get; set; }
        public AddressType addressType { get; set; }

        public string maillingAddress { get; set; }
        
        public int? divisionId { get; set; }
        public Division division { get; set; }        
        public int? districtId { get; set; }
        public District district { get; set; }
        public int? thanaId { get; set; }
        public Thana thana { get; set; }
        public int? postOfficeId { get; set; }
        public PostOffice postOffice { get; set; }
        public string postOfficeName { get; set; }

        public string email { get; set; }
        
        public string fax { get; set; }
        
        public string website { get; set; }
        
        public string phone { get; set; }
        
        public string mobile { get; set; }

        public int? addressCategoryId { get; set; }
        public AddressCategory addressCategory { get; set; }

        public int? resourceId { get; set; }
        public Resource resource { get; set; }

        public int? countryId { get; set; }
        public Country country { get; set; }

        public string union { get; set; }

        public string postCode { get; set; }

        public string blockSector { get; set; }

        public string houseVillage { get; set; }

        public string type { get; set; }

        public int? organizationId { get; set; }
        public Organization organization { get; set; }

        public int? companyId { get; set; }
        public Company company { get; set; }




    }
}
