using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.MemberInfo.Models
{
    public class PhotographViewModel
    {
        public int employeeID { get; set; }

        public int photographID { get; set; }

        [Display(Name = "Employee Photo")]
        public IFormFile empPhoto { get; set; }

        public MemberPhotograph photograph { get;set; }

        public string employeeNameCode { get; set; }

        public EmployeeInfoLn fLang { get; set; }       
    }
}
