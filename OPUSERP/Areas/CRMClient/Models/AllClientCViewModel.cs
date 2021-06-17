using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMClient.Models
{
    public class AllClientCViewModel
    {
        public int Id { get; set; }
        public string leadName { get; set; }
        public string leadOwner { get; set; }
        public string bankName { get; set; }
        public string branchName { get; set; }
        public int? isactive { get; set; }
       
    }
}
