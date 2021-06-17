using OPUSERP.HRPMS.Data.Entity.ACR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class PositionViewModel
    {
        public string employeeName { get; set; }
        public int? totalEmplyee { get; set; }
        public int? noOfOfficer { get; set; }
        public int? noOfWarker { get; set; }
        public decimal? developmentBudget { get; set; }
        public decimal? infrasturcturBudget { get; set; }
        public IEnumerable<Position> positions { get; set; }
    }
}
