using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace OPUSERP.Areas.MemberInfo.Models
{
    [NotMapped]
    public class MemberWithDesignationVM
    {
        public int? Id { get; set; }

        public string employeeCode { get; set; }

        public string nameBangla { get; set; }

        public string nameEnglish { get; set; }

        public string designationName { get; set; }

        public string fatherNameEnglish { get; set; }

        public string fatherNameBangla { get; set; }

        public string motherNameBangla { get; set; }

        public string motherNameEnglish { get; set; }

        public string joiningDate { get; set; }

        public string promotionType { get; set; }

        public string empType { get; set; }

        public string gradeName { get; set; }

        public DateTime dateOfBirth { get; set; }

        public decimal? Basic { get; set; }

        //public EmployeeInfoLn fLang { get; set; }

        public IEnumerable<MemberWithDesignationVM> employeeWithDesignations { get; set; }

    }
}
