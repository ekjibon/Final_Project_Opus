namespace OPUSERP.Areas.SCMMasterData.Models
{
    public class SpecificationDetailViewModel
    {
        public int? Id { get; set; }
        public string specificationName { get; set; }
        public string SKUNumber { get; set; }
        public string specificationCategoryName { get; set; }
        public int? specificationCategoryId { get; set; }
		public string FileName { get; set; }
		public string value { get; set; }
    }
}
