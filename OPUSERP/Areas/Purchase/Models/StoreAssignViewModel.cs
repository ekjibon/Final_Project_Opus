using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.POS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Purchase.Models
{
    public class StoreAssignViewModel
    {
        public string aspnetuserId { get; set; }

        public int?[] storeId { get; set; }
        public string[] isDefault { get; set; }


        public IEnumerable<StoreAssign> storeAssigns { get; set; }
        public IEnumerable<StoreAssignListViewModel> storeAssignListViews { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsers { get; set; }
        public IEnumerable<Store> stores { get; set; }
    }
}
