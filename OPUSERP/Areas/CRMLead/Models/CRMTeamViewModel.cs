using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using System;
using System.Collections.Generic;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class CRMTeamViewModel
    {       
        public int? tId { get; set; }        
        public int? teamId { get; set; }
        public int? areaId { get; set; }
        public int? isActive { get; set; }       
        public int? moduleId { get; set; }       

        public string memberName { get; set; }
        public string teamCode { get; set; } 
        public string aspnetuserId { get; set; }
        public string empName { get; set; }
        public string areaName { get; set; }

        public IEnumerable<CRMTeamViewModel> teams { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsers { get; set; }
        public IEnumerable<Area> areas { get; set; }
        public IEnumerable<ERPModule> eRPModules { get; set; }
        public IEnumerable<Team> GetTeams { get; set; }
    }
}
