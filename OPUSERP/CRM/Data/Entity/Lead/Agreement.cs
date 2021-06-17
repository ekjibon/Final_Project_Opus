using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("Agreement", Schema = "CRM")]
    public class Agreement : Base
    {
        [MaxLength(300)]
        public string agreementOwner { get; set; }

        [MaxLength(50)]
        public string agreementNo { get; set; }

        public int? leadsId { get; set; }
        public Leads leads { get; set; }

        public int? agreementTypeId { get; set; }
        public AgreementType agreementType { get; set; }

        public int? agreementCategoryId { get; set; }
        public AgreementCategory agreementCategory { get; set; }

        public int? ratingCategoryId { get; set; }
        public RatingCategory ratingCategory { get; set; }

        public int? contactSignatoryId { get; set; }
        public Contacts contactSignatory { get; set; }

        public int? contactWitnessId { get; set; }
        public Contacts contactWitness { get; set; }

        public int? agreementStatusId { get; set; }
        public AgreementStatus agreementStatus { get; set; }

        public DateTime? agreementDate { get; set; }
        public DateTime? sendingDate { get; set; }
        public DateTime? signingDate { get; set; }
        public DateTime? expiredDate { get; set; }
        public DateTime? expectedReportDate { get; set; }       
        public int? noOfReports { get; set; }
        public decimal? loanLimitAmount { get; set; }
        [MaxLength(10)]
        public string loanLimitUnit { get; set; }
        [MaxLength(10)]
        public string priority { get; set; }
        public string remarks { get; set; }
        public string reviewremarks { get; set; }
        [MaxLength(200)]
        public string companySignatory { get; set; }
        [MaxLength(200)]
        public string companyWitness { get; set; }
        public int? isPrint { get; set; }
        public DateTime? printDate { get; set; }
        [MaxLength(400)]
        public string companySignatoryDesignation { get; set; }
        [MaxLength(400)]
        public string companyWitnessDesignation { get; set; }
        [MaxLength(400)]
        public string contactSignatoryName { get; set; }
        [MaxLength(400)]
        public string contactWitnessName { get; set; }
        [MaxLength(400)]
        public string contactSignatoryDesignation { get; set; }
        [MaxLength(400)]
        public string contactWitnessDesignation { get; set; }
    }
}
