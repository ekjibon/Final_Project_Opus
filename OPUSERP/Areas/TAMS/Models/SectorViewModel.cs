using OPUSERP.TAMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class SectorViewModel
    {
        public TaskSection taskSection { get; set; }

        public IEnumerable<TaskInformationHistory> taskInformationHistories { get; set; }

    }
}
