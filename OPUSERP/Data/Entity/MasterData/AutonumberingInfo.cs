using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Data.Entity.MasterData
{
    public class AutonumberingInfo:Base
    {
        public string fieldName { get; set; }
        public int? NumType { get; set; } //1=Default or 2 = Customise
        public int? defaultValue { get; set; }
        public string prefix { get; set; }
        public int? startValue { get; set; }
        public int? NumValue { get; set; }
        public string separator { get; set; }
        public string yseparator { get; set; }
        public string mseparator { get; set; }
        public string dseparator { get; set; }
        public int? isyear { get; set; }
        public int? ismonth { get; set; }
        public int? isdate { get; set; }

    }
}
