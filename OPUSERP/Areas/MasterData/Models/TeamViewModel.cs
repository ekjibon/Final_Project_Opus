using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.MasterData.Models
{
    public class TeamViewModel
    {
        public int ssteamId { get; set; }
        public int? tId { get; set; }
        public int? teamId { get; set; }
        public int? parentId { get; set; }
        public string childMemberName { get; set; }
        
        public string memberName { get; set; }
        public string teamCode { get; set; }
        public string parentTeamCode { get; set; }
        public int? moduleId { get; set; }
        public int? isActive { get; set; }
        public string aspnetuserId { get; set; }
        public string aspnetuserIdChild { get; set; }
        public int? teamModuleId { get; set; }

        public IEnumerable<Team> teams { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsers { get; set; }
        public IEnumerable<ERPModule> eRPModules { get; set; }
    }
}
