using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMClient.Models.Dashboard
{
    public class CRMDataCountViewModel
    {
        public int? TotalLead { get; set; }
        public int? TotalClient { get; set; }
        public int? ThisMonthTotalLead { get; set; }
        public int? ThisMonthTotalClient { get; set; }
        public int? clientpercent { get; set; }
        public int? notClientpercent { get; set; }
        public List<string> months { get; set; }
        public List<int> activityCompletePercent { get; set; }
        public List<int> activityInprogressPercent { get; set; }
        public List<string> leadprogressstatus { get; set; }
        public List<int> leadprogressstatuscount { get; set; }
        public List<string> products { get; set; }
        public List<int> productscount { get; set; }
        public List<string> sources { get; set; }
        public List<int> sourcescount { get; set; }
        public List<string> teams { get; set; }
        public List<int> countleadgeneration { get; set; }
        public List<int> countleadproposal { get; set; }
        public List<int> countleadagrement { get; set; }
        public List<int> countleadconversion { get; set; }
        public List<string> OwnerShipTypes { get; set; }
        public List<int> countOwnerShipTypes { get; set; }
        public List<int> counttechnical { get; set; }
        public List<int> countfinacial { get; set; }
        public List<int> countfinacialtechnical { get; set; }

    }
}
