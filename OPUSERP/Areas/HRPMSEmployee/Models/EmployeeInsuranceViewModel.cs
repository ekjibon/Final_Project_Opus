using Microsoft.AspNetCore.Http;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class EmployeeInsuranceViewModel
    {
        public int? employeeID { get; set; }
        public int? insuranceID { get; set; }

        public string name { get; set; }      
        public string relation { get; set; }        
        public string NID { get; set; }        
        public string age { get; set; }        
        public string gender { get; set; }
        public DateTime? dateOfBirth { get; set; }
        public DateTime? insuranceDate { get; set; }
        public string imageUrl { get; set; }

        //public IFormFile fileUrl { get; set; }
        public IFormFile imagePath_Challan { get; set; }

        public string employeeNameCode { get; set; }

        public Photograph photograph { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

        public IEnumerable<EmployeeInsurance> employeeInsurances { get; set; }
    }
}
