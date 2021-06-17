using OPUSERP.TAMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class TaskInformationHistory
    {
        public TaskInformation taskInformation { get; set; }

        public int? followers { get; set; }

        public int? Tags { get; set; }

        public int? files { get; set; }

        public int? Comments { get; set; }

        public int? Following { get; set; }

        public int? doneSubtusk { get; set; }

        public int? unDoneSubtusk { get; set; }

        public IEnumerable<int?> taskFollowers { get; set; }

        public int? LeaderID { get; set; }

    }
}
