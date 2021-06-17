using OPUSERP.HRPMS.Data.Entity.ACR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSACR.Models
{
    public class ACRProcessViewModel
    {
        public int? acrSeduleId { get; set; }

        public int? sequence { get; set; }

        public string acrActorName { get; set; }

        public string actionType { get; set; }

        public string remarks { get; set; }

        public string routingType { get; set; }//rout,reroud

        public string dueDate { get; set; }//targat date

        public IEnumerable<ACRSchedule> aCRSchedules { get; set; }
    }
}
