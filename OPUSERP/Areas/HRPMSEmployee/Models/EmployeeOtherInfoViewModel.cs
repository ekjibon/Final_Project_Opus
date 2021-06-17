using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.SCM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class EmployeeOtherInfoViewModel
    {
        public int id { get; set; }
        public int employeeId { get; set; }
        public int? hRFacilityId { get; set; }
        public string simsId { get; set; }
        public string busArea { get; set; }
        public string type { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<HRFacility> hRFacilities { get; set; }
        public IEnumerable<EmployeeOtherInfo> employeeOtherInfos { get; set; }
    }
}
