using OPUSERP.POS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.OtherSales.Models.NotMapped.Dashboard
{
    public class DailySalesModel
    {
        public decimal? numberOfSales { get; set; }
        public decimal? revenue { get; set; }
        public decimal? profit { get; set; }
        public decimal? cost { get; set; }

        public IEnumerable<TopFiveItemModel> topFiveItemModels { get; set; }

        public IEnumerable<GraphModel> graphModels { get; set; }
        public IEnumerable<SalesDetailModel> salesDetailModels { get; set; }

        public int? poscustnew { get; set; }
        public int? poscustrepeat { get; set; }
        public IEnumerable<PosCustomer> posCustomersNew { get; set; }
        public IEnumerable<PosCustomer> posCustomersRepeat { get; set; }
    }
}
