//using UMBRELLA.Areas.LeadManagement.Models.Lang;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data.Entity.Master;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace OPUSERP.Areas.CRMLead.Models
{
    public class AddressViewModel
    {
        public int? Id { get; set; }
        public int? companyAddressId { get; set; }
        public int? factoryAddressId { get; set; }
        public int leadId { get; set; }
      
        public int? addressTypeId { get; set; }
        public string leadName { get; set; }
        [Required]
        public string mailingAddress { get; set; }
        [Required]
        public int? divisionId { get; set; }

        [Required]
        [Display(Name = "District")]
        public int? districtId { get; set; }

        [Required]
        [Display(Name = "Thana")]
        public int? thanaId { get; set; }

        public int? postOfficeId { get; set; }
        public string postOfficeName { get; set; }
        [Display(Name = "Email")]
        public string email { get; set; }

        [Display(Name = "Mobile")]
        public string mobile { get; set; }


        [Display(Name = "Website")]
        public string website { get; set; }

        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Display(Name = "Fax")]
        public string fax { get; set; }

        public string type { get; set; }

        public Address factory { get; set; }
        public Address company { get; set; }

        public IEnumerable<AddressType> addressTypes { get; set; }
        public IEnumerable<Address> Addresses { get; set; }
        public IEnumerable<Address> addresses { get; set; }
        public IEnumerable<District> districts { get; set; }
        public IEnumerable<Division> divisions { get; set; }
        public IEnumerable<Thana> thanas { get; set; }
        public IEnumerable<PostOffice> postOffices { get; set; }
    }
}
