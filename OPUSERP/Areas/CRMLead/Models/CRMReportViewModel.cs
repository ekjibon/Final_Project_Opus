using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class CRMReportViewModel
    {
        public IEnumerable<Sector> sectors { get; set; }
        public IEnumerable<Source> sources { get; set; }
        public IEnumerable<FIType> fITypes { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public IEnumerable<Company> companies { get; set; }

        public IEnumerable<LeadReportViewModel> leadReportViewModels { get; set; }
    }
}
