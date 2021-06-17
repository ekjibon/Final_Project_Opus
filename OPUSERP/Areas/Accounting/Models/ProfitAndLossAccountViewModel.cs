using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;

namespace OPUSERP.Areas.Accounting.Models
{
    public class ProfitAndLossAccountViewModel
    {
        public string fsLineName { get; set; }
        public string noteName { get; set; }
        public string noteNo { get; set; }
        public decimal? currentAmount { get; set; }
        public decimal? previousAmount { get; set; }
    }
}
