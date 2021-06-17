using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class DisciplinaryViewModel
    {
        public int disciplinaryId { get; set; }

        public int employeeId { get; set; }

        public string employeeName { get; set; }

        public string designation { get; set; }

        public int? offenseId { get; set; }

        public string punishmentId { get; set; }

        public DateTime? punishmentDate { get; set; }

        public DateTime? startFrom { get; set; }

        public DateTime? endTo { get; set; }

        public string goNo { get; set; }

        [Display(Name = "GO Attachment")]
        public IFormFile goAttachment { get; set; }

        public string remarks { get; set; }

        public string employeeNameCode { get; set; }

        public Disciplinary fLang { get; set; }
        
        
    }
}
