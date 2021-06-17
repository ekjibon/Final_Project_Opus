using OPUSERP.CRM.Data.Entity.Lead;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace OPUSERP.Areas.CRMLead.Models
{
    public class LeadInfoViewModel
    {
        [Required]
        public string leadID { get; set; }

        [Required]
        [Display(Name = "Lead Name")]
        public string leadName { get; set; }

        [Display(Name = "Lead Short Name")]
        public string leadShortName { get; set; }

        [Display(Name = "Group Name")]
        public string groupName { get; set; }

        [Display(Name = "Category")]
        public string category { get; set; }

        [Display(Name = "Industry")]
        public string industry { get; set; }

        [Required]
        [Display(Name = "Ownership Type")]
        public string ownershipType { get; set; }

        [Required]
        [Display(Name = "Phone")]
        public string phone { get; set; }

        [Required]
        [Display(Name = "Mobile")]
        public string mobile { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string email { get; set; }

        [Required]
        [Display(Name = "Fax")]
        public string fax { get; set; }

        [Required]
        [Display(Name = "Website")]
        public string website { get; set; }

        [Required]
        [Display(Name = "Total Employees")]
        public string totalEmployees { get; set; }

        [Required]
        [Display(Name = "Source Name")]
        public string sourceName { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Address")]
        public string address { get; set; }

        [Required]
        [Display(Name = "Division")]
        public string division { get; set; }

        [Required]
        [Display(Name = "District")]
        public string district { get; set; }

        [Required]
        [Display(Name = "Upazila")]
        public string upazila { get; set; }

        [Required]
        [Display(Name = "Post Office")]
        public string postOffice { get; set; }

        [Display(Name = "Factory Address")]
        public string factoryAddress { get; set; }

        [Display(Name = "factoryDivision")]
        public string factoryDivision { get; set; }

        [Display(Name = "factoryDistrict")]
        public string factoryDistrict { get; set; }

        [Display(Name = "Factory Upazila")]
        public string factoryUpazila { get; set; }

        [Display(Name = "Factory Post Office")]
        public string factoryPostOffice { get; set; }

        public IEnumerable<Leads> leads { get; set; }

    }
}
