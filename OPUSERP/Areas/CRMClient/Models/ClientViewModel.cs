using OPUSERP.CRM.Data.Entity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.CRMClient.Models
{
    public class ClientViewModel
    {
        public int leadsID { get; set; }
        public int? isconverted { get; set; }
        public int? isactive { get; set; }
        public IEnumerable<Clients>  clients { get; set; }
        public IEnumerable<GetClientInfoListViewModel> getClientInfoListViewModels { get; set; }
    }
}
