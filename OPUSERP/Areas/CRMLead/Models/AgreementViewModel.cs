using OPUSERP.Areas.Auth.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Address = OPUSERP.CRM.Data.Entity.Lead.Address;

namespace OPUSERP.Areas.CRMLead.Models
{
    public class AgreementViewModel
    {
        public int agreementId { get; set; }
        
        //public int?[] productList { get; set; }
        public string productName { get; set; }

        public string leadName { get; set; }

        public int? isPersonal { get; set; }

        public int leadsId { get; set; }
        public int? agreementTypeId { get; set; }
        public int? ratingCategoryTypeId { get; set; }
        public string agreementNo { get; set; }
        public DateTime? agreementDate { get; set; }
        public DateTime? sendingDate { get; set; }
        public DateTime? signingDate { get; set; }
        public DateTime? expiredDate { get; set; }
        public DateTime? expectedReportDate { get; set; }
        public int? agreementCategoryId { get; set; }
        public int? noOfReports { get; set; }
        public decimal? vatPercent { get; set; }
        public string vatType { get; set; }
        public string remarks { get; set; }
        public string revremarks { get; set; }
        public string companySignatory { get; set; }
        public string companyWitness { get; set; }
        public string cosigdesignationName { get; set; }
        public string cowitdesignationName { get; set; }
        public string consigdesignationName { get; set; }
        public string conwitdesignationName { get; set; }
        public int? contactSignatoryId { get; set; }
        public int? contactWitnessId { get; set; }
        public decimal? loanLimitAmount { get; set; }
        public string loanLimitUnit { get; set; }
        public string priority { get; set; }
        public int? agreementStatusId { get; set; }
        

        public int?[] ratingYearId { get; set; }
        public int?[] vatCategoryId { get; set; }
        public int?[] taxCategoryId { get; set; }
        public DateTime?[] ratingDate { get; set; }
        public decimal?[] agreementAmount { get; set; }
        public decimal?[] ratingAmount { get; set; }
        public decimal?[] vatAmount { get; set; }
        public decimal?[] taxAmount { get; set; }
        public decimal?[] totalAmount { get; set; }
        public int?[] isProposed { get; set; }


        public IEnumerable<Agreement> agreements { get; set; }
        public IEnumerable<AgreementDetails> agreementDetailspre { get; set; }
        public IEnumerable<AgreementType> agreementTypes { get; set; }
        public IEnumerable<Product> products { get; set; }
        public IEnumerable<RelProductAgreement> relProductAgreement { get; set; }
        public IEnumerable<AgreementCategory>  agreementCategories { get; set; }
        public IEnumerable<Contacts> contacts { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsers { get; set; }
        public IEnumerable<Comment> comments { get; set; }
        public IEnumerable<DocumentPhotoAttachment> photoes { get; set; }
        public IEnumerable<DocumentPhotoAttachment> documents { get; set; }
        public Agreement getAgreementDeatilsById { get; set; }
        public Agreement getAgreementDeatilsBypreId { get; set; }
        public IEnumerable<AgreementDetails> agreementDetails { get; set; }
        public IEnumerable<RatingYear> ratingYears { get; set; }
        public IEnumerable<VatCategory> vatCategories { get; set; }
        public IEnumerable<TaxCategory> taxCategories { get; set; }
        public IEnumerable<RatingCategory> ratingCategories { get; set; }
        public GetAgreementReportViewModel getAgreementReportByAgreementId { get; set; }
        public IEnumerable<Company> companies { get; set; }
        public IEnumerable<AspNetUsersViewModel> aspNetUsersViewModels { get; set; }
        public IEnumerable<GetLeadInfoVerificationListViewModel> getLeadInfoVerificationListViewModels { get; set; }
        public IEnumerable<GetLeadInfoPrintListViewModel> getLeadInfoPrintListViewModels { get; set; }
        public IEnumerable<Address> addresses { get; set; }
        public IEnumerable<LeadsBankInfo> leadsBankInfos { get; set; }
        public IEnumerable<EmployeeInfo> employeeInfos { get; set; }


    }
}
