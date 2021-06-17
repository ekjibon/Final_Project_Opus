namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class TeamDataViewModel
    {
        public int teamId { get; set; }
        public int userId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
        public string DivisionName { get; set; }
        public string DesignationName { get; set; }
        public int parentId { get; set; }
       
        public int teamOrder { get; set; }
        public int isActive { get; set; }
      
    }
}
