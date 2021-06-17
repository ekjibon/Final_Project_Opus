using OPUSERP.CRM.Data.Entity.Cold;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.CRMLead.Models
{
    public class ActivityReportViewModel
    {
        public string LeadName { get; set; }
        public string Subject { get; set; }
        public string status { get; set; }
        public string nameEnglish { get; set; }
        public string description { get; set; }
        public string activitiesDate { get; set; }
    }
}
