using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.FAMSMasterData.Models
{
    public class DepriciationPeriodViewModel
    {
        public int depriciationPeriodId { get; set; }
        public string PeriodName { get; set; }
        public string MonthName { get; set; }
        public int? YearID { get; set; }
        public int? DaysWorking { get; set; }
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        public string islock { get; set; }

        public IEnumerable<DepriciationPeriod> depriciationPeriods { get; set; }
        public IEnumerable<AssetYear> assetYears { get; set; }
    }
}
