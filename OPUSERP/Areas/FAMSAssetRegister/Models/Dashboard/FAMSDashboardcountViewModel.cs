using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.FAMSAssetRegister.Models.Dashboard
{
    public class FAMSDashboardcountViewModel
    {
        //1
        public decimal assetOpening{ get; set; }
        //2
        public decimal assetAddition { get; set; }
        //3
        public decimal assetTransfer { get; set; }
        //4
        public decimal assetDisposal { get; set; }
        //5
        public decimal assetClosing { get; set; }
        //6
        public decimal depriciationOpening { get; set; }
        //7
        public decimal depriciationcharged { get; set; }
        //8
        public decimal depriciationadjustment { get; set; }
        //9
        public decimal depriciationClosing { get; set; }
        //10
        public decimal assetNet { get; set; }
        //11
        public List<int> years { get; set; }
        public List<decimal> yearlyvalues { get; set; }
        //12
        public List<string> categoryname { get; set; }
        public List<decimal> categorywisepercent { get; set; }



    }
}
