using OPUSERP.CRM.Data.Entity.Client;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.CRMLead.Models
{
    public class LeadViewModel
    {
        [Required]
        public int leadId { get; set; }
        public int? sector { get; set; }

        [Required]
        [Display(Name = "Lead Name")]
        public string leadName { get; set; }
        public string leadOwner { get; set; }

        public string leadShortName { get; set; }
      
        public string leadNumber { get; set; }
     
        public int? companyGroupId { get; set; }
        public int? fITypeId { get; set; }
        public int? ownerShipTypeId { get; set; }
        
        public int? sourceId { get; set; }
        
        public string sourceDescription { get; set; }
        public int? totalemployee { get; set; }

        public IEnumerable<Leads> leads { get; set; }
        public IEnumerable<Clients> clients { get; set; }
        public IEnumerable<Sector> sectors { get; set; }
        public IEnumerable<CompanyGroup> companyGroups { get; set; }
        public IEnumerable<OwnerShipType> ownerShipTypes { get; set; }
        public IEnumerable<Source> sources { get; set; }
        public LeadAutoNumber leadAutoNumbers { get; set; }

        public Leads GetLeadDetailsById { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }

        public IEnumerable<CRM.Data.Entity.Lead.Address> Addresses { get; set; }
        public IEnumerable<ActivityMaster> activityMasters { get; set; }
        public IEnumerable<Contacts> contacts { get; set; }
        public IEnumerable<GetLeadInfoListViewModel> getLeadInfoListViewModels { get; set; }
        public IEnumerable<GetLeadsForConversionListViewModel> getLeadsForConversionListViewModels { get; set; }
        public IEnumerable<FIType> fITypes { get; set; }

        public IEnumerable<CRMDepartment> departments { get; set; }
        public IEnumerable<CRMDesignation> designations { get; set; }
        public IEnumerable<LeadsBankInfo> leadsBankInfos { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }
        public Leads leadsingal { get; set; }

    }
}
