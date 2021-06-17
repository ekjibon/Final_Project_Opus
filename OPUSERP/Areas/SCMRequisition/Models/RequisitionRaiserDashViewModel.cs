using OPUSERP.Areas.SCMMasterData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.SCMRequisition.Models
{
	public class RequisitionRaiserDashViewModel
	{
		public int? CreateRequisition { get; set; }
		public int? OnGoingRequisition { get; set; }
		public int? ApprovedRequisition { get; set; }
		public int? CompleteRequisition { get; set; }
		public int? RejectRequisition { get; set; }
		public IEnumerable<MostRecentRequisitionViewModel> MostRecent { get; set; }
		public IEnumerable<TopTenRequisitionViewModel> TopTen { get; set; }
		public IEnumerable<FeaturedItemViewModel> Featured { get; set; }
		public IEnumerable<ItemSpecificationDepartmentModel> itemSpecificationDepartmentModels { get; set; }
	}
	public class MostRecentRequisitionViewModel
	{
		public int? itemSpecificationId { get; set; }
		public string specificationName { get; set; }
		public decimal total { get; set; }
		public string SKUNumber { get; set; }
		public string specImageUrl { get; set; }
	}
	public class TopTenRequisitionViewModel
	{
		public int? itemSpecificationId { get; set; }
		public string specificationName { get; set; }
		public decimal total { get; set; }
		public string SKUNumber { get; set; }
		public string specImageUrl { get; set; }
	}
	public class FeaturedItemViewModel
	{
        public int? itemSpecificationId { get; set; }
        public string specificationName { get; set; }
		public string specImageUrl { get; set; }
	}
}
