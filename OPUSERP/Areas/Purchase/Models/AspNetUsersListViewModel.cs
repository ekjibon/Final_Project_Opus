using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models
{
    public class AspNetUsersListViewModel
    {
        public string UserName { get; set; }
        public string ID { get; set; }
        public int? userTypeId { get; set; }
        public string Email { get; set; }
        public string userTypeName { get; set; }
    }
}
