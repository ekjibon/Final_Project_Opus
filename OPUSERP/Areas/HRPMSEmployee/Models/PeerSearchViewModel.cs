using System;

namespace OPUSERP.Areas.HRPMSEmployee.Models
{
    public class PeerSearchViewModel
    {
        public string employeeCode { get; set; }
        public string nameEnglish { get; set; }
        public DateTime dateOfBirth { get; set; }
        public DateTime joiningDateGovtService { get; set; }
        public string maritalStatus { get; set; }
        public string gender { get; set; }
        public string mobileNumberOffice { get; set; }
        public string designation { get; set; }
        public string emailAddress { get; set; }
        public int departmentId { get; set; }
        public string deptName { get; set; }
        public string bloodGroup { get; set; }
        public string shiftName { get; set; }
        public string url { get; set; }
    }
}
