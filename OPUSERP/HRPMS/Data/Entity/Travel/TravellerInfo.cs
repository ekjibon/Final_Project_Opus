using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Data.Entity.Travel
{
    [Table("TravellerInfo", Schema = "HR")]
    public class TravellerInfo:Base
    {
        public int? travelMasterId { get; set; }
        public TravelMaster travelMaster { get; set; }

        public int employeeID { get; set; }
        public EmployeeInfo employee { get; set; } //Traveller
    }
}
