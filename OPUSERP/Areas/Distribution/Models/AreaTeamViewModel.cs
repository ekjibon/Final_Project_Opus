using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Distribution.Models
{
    public class AreaTeamViewModel
    {
        public int Id { get; set; }
        public string areaCode { get; set; }


        public string areaName { get; set; }

        public string areaLocation { get; set; }

        public int? areaId { get; set; }


        public int? salesLevelId { get; set; }


        public string nameEnglish { get; set; }
    }
}
