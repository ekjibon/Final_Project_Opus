using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.API.Models
{
    public class ActivityWiseMaterialModel
    {
        public int? Id { get; set; }
        public string materialName { get; set; }
        public int? materialId { get; set; }
        public string activityName { get; set; }
        public int? activityDetailsId { get; set; }
    }
}
