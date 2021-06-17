using OPUSERP.Areas.Auth.Models;
using OPUSERP.SCM.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class TeamMasterViewModel
    {
        public int? teamMasterId { get; set; }
        public string teamCode { get; set; }
        public string teamName { get; set; }
        public int? leaderId { get; set; }
        public int? isActive { get; set; }

        public int? teamMemberId { get; set; }
        public int? memberId { get; set; }
        


        public IEnumerable<TeamMaster> teamMasters { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsersViews { get; set; }
    }
}
