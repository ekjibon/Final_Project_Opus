using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Message.Models
{
    public class ContactListViewModel
    {
        public int Id { get; set; }
        public int grpId { get; set; }
        public string name { get; set; }
        public string code { get; set; }
        public string groupName { get; set; }
        public string tagline { get; set; }
        public string empPhoto { get; set; }
        public string grpPhoto { get; set; }
        public EmployeeInfo employeeInfo { get; set; }

    }
}
