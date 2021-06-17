using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.CLUB.Data.Member;
using System;
using System.Collections.Generic;


namespace OPUSERP.Areas.MemberInfo.Models
{
    public class InfoViewModel
    {
        public string title { get; set; }
        public string employeeID { get; set; }
        public string nid { get; set; }
        public string bin { get; set; }
        public string govtID { get; set; }
        public string gpfNomineeName { get; set; }
        public string gpfAccNo { get; set; }
        public string name { get; set; }
        public string bangla { get; set; }
        public string motherName { get; set; }
        public string fatherName { get; set; }
        public string nationality { get; set; }
        public string disability { get; set; }
        public string dateOfBirth { get; set; }
        public string gender { get; set; }
        public string birthPlace { get; set; }
        public string employeePhoto { get; set; }
        public string emailAddress { get; set; }
        public string maritalStatus { get; set; }
        public string religion { get; set; }
        public string designation { get; set; }
        public string employeeType { get; set; }
        public string basicGrade { get; set; }
        public string presentOffice { get; set; }
        public string bloodGroup { get; set; }
        public string freedomFighter { get; set; }
        public string freedomFighterNo { get; set; }
        public string joiningDateofPresentPost { get; set; }
        public string joiningDateofPresentWorkStation { get; set; }
        public string joiningDateofGovtService { get; set; }
        public string postofFirstJoining { get; set; }
        public string telephoneOffice { get; set; }
        public string telephoneResidence { get; set; }
        public string pabx { get; set; }
        public string mobileNumberOffice { get; set; }
        public string mobileNumber { get; set; }
        public string mobileNumberPersonal { get; set; }
        public string dateofRegularity { get; set; }
        public string dateofPermanennt { get; set; }
        public string dateOfLPR { get; set; }
        public string emailAddressPersonal { get; set; }
        public string sameAddress { get; set; }
        public string Division { get; set; }
        public string District { get; set; }
        public string Upazila { get; set; }
        public string Union { get; set; }
        public string PostOffice { get; set; }
        public string PostCode { get; set; }
        public string BlockSector { get; set; }
        public string HouseVillage { get; set; }
        public string permanent { get; set; }
        public string present { get; set; }
        public string levelOfEducation { get; set; }
        public string degree { get; set; }
        public string institution { get; set; }
        public string result { get; set; }
        public string resultSpecification { get; set; }
        public string grade { get; set; }
        public string passingYear { get; set; }
        public string subject { get; set; }
        public string majorGroup { get; set; }
        public string organization { get; set; }
        public string spouseName { get; set; }
        public string spouseNameBN { get; set; }
        public string occupation { get; set; }
        public string contact { get; set; }
        public string higherEducation { get; set; }
        public string childName { get; set; }
        public string childNameBN { get; set; }
        public string education { get; set; }
        public string language { get; set; }
        public string languageShortName { get; set; }
        public string proficiency { get; set; }
        public string awardName { get; set; }
        public string perpose { get; set; }
        public string awardDate { get; set; }
        public string publicationName { get; set; }
        public string publicationType { get; set; }
        public string publicationDate { get; set; }
        public string actionName { get; set; }
        public string actionDate { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string supervisorName { get; set; }
        public string supervisorDesignation { get; set; }
        public string signatoryName { get; set; }
        public string signatoryDesignation { get; set; }
        public string remarks { get; set; }
        public string passPortNumber { get; set; }
        public string place { get; set; }
        public string dateOfIssue { get; set; }
        public string dateOfExpiry { get; set; }
        public string tourType { get; set; }
        public string tourPurpose { get; set; }
        public string location { get; set; }
        public string presentDivision { get; set; }
        public string country { get; set; }
        public string sponsoringAgency { get; set; }
        public string tourStartDate { get; set; }
        public string tourEndDate { get; set; }
        public string goNumber { get; set; }
        public string goDate { get; set; }
        public string file { get; set; }
        public string titleOfFile { get; set; }
        public string licenseNumber { get; set; }
        public string promotionDate { get; set; }
        public string rank { get; set; }
        public string promotionNature { get; set; }
        public string trainingType { get; set; }
        public string trFromDate { get; set; }
        public string trToDate { get; set; }
        public string workStation { get; set; }
        public string trainingTitle { get; set; }
        public string action { get; set; }

        public InfoViewModelAllLn fLang { get; set; }

        public OPUSERP.CLUB.Data.Member.MemberInfo employee { get; set; }

        public MemberAddress addressPresent { get; set; }
        public MemberAddress addressPermanent { get; set; }

        public IEnumerable<MemberEducationalQualification> educationalQualifications { get; set; }

        public IEnumerable<MemberSpouse> spouses { get; set; }

        public IEnumerable<MemberChildren> childrens { get; set; }

        public IEnumerable<MemberDrivingLicense> drivingLicenses { get; set; }

        public IEnumerable<MemberPassportDetails> passportDetails { get; set; }

        public IEnumerable<OtherMembership> employeeMemberships { get; set; }

        public IEnumerable<MemberLanguage> employeeLanguages { get; set; }

        public IEnumerable<MemberAward> employeeAwards { get; set; }

        public IEnumerable<MemberPublication> publications { get; set; }

        public IEnumerable<MemberTransferLog> transferLogs { get; set; }

        public IEnumerable<MemberTraningLog> traningLogs { get; set; }

        public MemberPhotograph photograph { get; set; }
        public string employeeNameCode { get; set; }

    }
}
