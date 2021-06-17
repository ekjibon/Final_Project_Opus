namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class ActivityWiseProjectLocationModel
    {
        public int? projectId { get; set; }
        public string reportingUser { get; set; }
        public int? projectLocationId { get; set; }
        public string projectShortName { get; set; }
        public string locationPosition { get; set; }
        public string activityName { get; set; }
        public string unitName { get; set; }
        public decimal? qty { get; set; }
        public int? totalItem { get; set; }
        public decimal? itemWiseTotalQTY { get; set; }
        public string userName { get; set; }
        public string gridLocation { get; set; }
    }
}
