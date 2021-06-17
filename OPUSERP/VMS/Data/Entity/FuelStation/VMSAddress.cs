using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.VMS.Data.Entity.FuelStation
{
    [Table("VMSAddress", Schema = "VMS")]
    public class VMSAddress:Base
    {
        public int fuelStationInfoId { get; set; }
        public FuelStationInfo fuelStationInfo { get; set; }

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

        //Type: Permamnent or Present
        [Required]
        public string type { get; set; }

    }
}
