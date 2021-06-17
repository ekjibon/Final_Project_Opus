using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class StatusLogVM
    {
        public int Id { get; set; }

        public int Sl { get; set; }

        public string empName { get; set; }

        public string nextEmpName { get; set; }

        public string Status { get; set; }

        public string timeDate { get; set; }
    }
}
