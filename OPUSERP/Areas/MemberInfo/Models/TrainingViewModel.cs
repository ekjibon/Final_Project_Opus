using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;

namespace OPUSERP.Areas.MemberInfo.Models
{
    public class TrainingViewModel
    {
        public int employeeID { get; set; }

        public int trainingLogID { get; set; }

        public DateTime? fromDate { get; set; }

        public DateTime? toDate { get; set; }
        
        public string country { get; set; }

        public string category { get; set; }

        [Required]
        public string trainingTitle { get; set; }

        public string institute { get; set; }

        public string sponsoringAgency { get; set; }

        public string remarks { get; set; }

        public string employeeNameCode { get; set; }

        public TrainingLn fLang { get; set; }

        public IEnumerable<Country> countries { get; set; }

        public IEnumerable<TrainingCategory> trainingCategories { get; set; }

        public IEnumerable<TrainingInstitute> trainingInstitutes { get; set; }

        public IEnumerable<MemberTraningLog> traningLogs { get; set; }
    }
}
