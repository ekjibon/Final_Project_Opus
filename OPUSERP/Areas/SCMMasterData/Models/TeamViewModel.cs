using OPUSERP.Areas.SCMMasterData.Models.Lang;
using OPUSERP.Data.Entity.MasterData;
using System.Collections.Generic;

namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class TeamViewModel
    {
        public int teamId { get; set; }
        public int userId { get; set; }

        public int parentId { get; set; }


        public string leaderName { get; set; }
        public string memberName { get; set; }
        public int teamOrder { get; set; }
        public int isActive { get; set; }



        public TeamLn fLang { get; set; }
        public IEnumerable<Team> teams { get; set; }
        public IEnumerable<TeamDataViewModel> TeamDataViewModels { get; set; }

    }
}
