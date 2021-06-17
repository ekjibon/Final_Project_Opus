using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Budget.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Budget.Models
{
    public class UnitOfTakaViewModel
    {
        public int? UnitId { get; set; }
        public string UnitName { get; set; }
        public decimal? value { get; set; }
        public int?[] status { get; set; }

        public UnitLn fLang { get; set; }
        public IEnumerable<UnitOfTaka> unitOfTakas { get; set; }
    }
}
