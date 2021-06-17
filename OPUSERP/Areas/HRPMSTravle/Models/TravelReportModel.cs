using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Travel;
using System.Collections.Generic;

namespace OPUSERP.Areas.HRPMSTravle.Models
{
    public class TravelReportModel
    {
        public Company company { get; set; }
        public TravelMaster travelMaster { get; set; }
        public IEnumerable<TravellerInfo> travellerInfos { get; set; }
        public IEnumerable<TravellingInfo> travellingInfos { get; set; }
        public IEnumerable<TravelStatusLog> travelStatusLogs { get; set; }
        public ApprovalDetail approvalDetail { get; set; }
    }
}
