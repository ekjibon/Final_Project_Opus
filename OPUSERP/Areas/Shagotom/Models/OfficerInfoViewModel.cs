using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;


namespace OPUSERP.Areas.Shagotom.Models
{
    public class OfficerInfoViewModel
    {
        public string name { get; set; }
        public string email { get; set; }
        public string mobile { get; set; }

        public int? departmentId { get; set; }
        public int? designationId { get; set; }
        public int? roomNoId { get; set; }
        public IFormFile img { get; set; }

        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<Department> departments { get; set; }
        public IEnumerable<Designation> designations { get; set; }
   
    }
}
