using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Models
{
	public class StoreKeeperDashboardViewModel
	{
		public int? CreateRequisition { get; set; }
		public int? OnGoingRequisition { get; set; }
		public int? ApprovedRequisition { get; set; }
		public int? CompleteRequisition { get; set; }
		public int? RejectRequisition { get; set; }
		public IEnumerable<MostRecentStoreKeeperViewModel> MostRecent { get; set; }
		public IEnumerable<TopTenStoreKeeperViewModel> TopTen { get; set; }
		public IEnumerable<FeaturedStoreKeeperViewModel> Featured { get; set; }
	}
	public class MostRecentStoreKeeperViewModel
	{
		public string specificationName { get; set; }
		public decimal total { get; set; }
		public string SKUNumber { get; set; }
		public string specImageUrl { get; set; }
	}
    public class MostRecentRequisitionNewViewModel
    {
        public int? itemSpecificationId { get; set; }
        public string specificationName { get; set; }
        public decimal total { get; set; }
        public string SKUNumber { get; set; }
        public string specImageUrl { get; set; }
        public int? isFeatured { get; set; }
    }
    public class TopTenStoreKeeperViewModel
	{
		public string specificationName { get; set; }
		public decimal total { get; set; }
		public string SKUNumber { get; set; }
		public string specImageUrl { get; set; }
	}
	public class FeaturedStoreKeeperViewModel
	{
		public string specificationName { get; set; }
		public string specImageUrl { get; set; }
	}
}
