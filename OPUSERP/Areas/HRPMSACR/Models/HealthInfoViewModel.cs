using OPUSERP.Areas.HRPMSACR.Models.Lang;
using OPUSERP.HRPMS.Data.Entity.ACR;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class HealthInfoViewModel
    {
        public int acrInitiateId { get; set; }
        
        public int employeeId { get; set; }

        public string acrType { get; set; }

        public int acrHealthInfoId { get; set; }        

        public string height { get; set; }

        public string weight { get; set; }

        public string vision { get; set; }

        public string bloodGroup { get; set; }

        public string bloodPressure { get; set; }

        public string xray { get; set; }

        public string ecg { get; set; }

        public string treatementClassification { get; set; }

        public string physicalDisorder { get; set; }        
       
        public string signDate { get; set; }

        public string hoName { get; set; }

        public string hoDesignation { get; set; }

        public string hoOrganization { get; set; }

        public ACRLn flang { get; set; }

        public IEnumerable<AcrHealthInfo> acrHealthInfos { get; set; }

        public AcrHealthInfo acrHealthInfo { get; set; }

        //public EmployeeInfo employeeInfo { get; set; }
    }
}
