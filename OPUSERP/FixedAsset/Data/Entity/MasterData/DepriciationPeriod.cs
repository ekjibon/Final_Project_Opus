using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.FixedAsset.Data.Entity.MasterData
{
    [Table("DepriciationPeriod", Schema = "FAMS")]
    public class DepriciationPeriod: Base
    {
        public string PeriodName { get; set; }
        public string MonthName { get; set; }
        public int? YearID { get; set; }
        public AssetYear Year { get; set; }

        public int? DaysWorking { get; set; }
        public DateTime SDate { get; set; }
        public DateTime EDate { get; set; }
        public string islock { get; set; }

    }
}
