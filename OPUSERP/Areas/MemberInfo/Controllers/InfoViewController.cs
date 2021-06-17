//using CLUB.Areas.ACR.Models.Lang;
using DinkToPdf.Contracts;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Threading.Tasks;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.Data.Entity.Master;
using Award = OPUSERP.HRPMS.Data.Entity.Master.Award;
using OPUSERP.Areas.MemberInfo.Models.Lang;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.CLUB.Data.Master;

namespace OPUSERP.Areas.MemberInfo.Controllers
{
    [Area("MemberInfo")]
    [Authorize(Roles = "admin,user,Press Club Admin")]
    public class InfoViewController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IAwardPublicationLanguageService awardPublicationLanguageService;
        private readonly IMemberInfoService employeeInfoService;
        private readonly IDrivingLicenseService drivingLicenseService;
        private readonly IMembershipService membershipService;
        private readonly IPassportInfoService passportInfoService;
        private readonly ISpouseChildrenService spouseChildrenService;
        private readonly IServiceHistoryService serviceHistoryService;
        private readonly ITraningHistoryService traningHistoryService;
        private readonly IPhotographService photographService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;


        //langSection
        private readonly LangGenerate<EmployeeInfoLn> _langEmp;
        private readonly LangGenerate<Address> _langAddress;
        private readonly LangGenerate<EducationalQualificationLn> _langEducation;
        private readonly LangGenerate<TransferLogLn> _langTransfer;
        private readonly LangGenerate<TrainingLn> _langTraing;
        private readonly LangGenerate<PromotionLogLn> _langPromotion;
        private readonly LangGenerate<SpouseLn> _langSpous;
        private readonly LangGenerate<ChildrenLn> _langChildren;
        private readonly LangGenerate<License> _langLicense;
        private readonly LangGenerate<Passport> _langPassport;
        private readonly LangGenerate<Travel> _langTravel;
        private readonly LangGenerate<LanguageLn> _langLanguage;
        private readonly LangGenerate<Award> _langAward;
        private readonly LangGenerate<BankLn> _langBank;
        private readonly LangGenerate<Publication> _langPublication;
        private readonly LangGenerate<Disciplinary> _langDisciplinary;

        private readonly LangGenerate<LeaveLogLn> _langLeave;


        public InfoViewController(IHostingEnvironment hostingEnvironment, ITraningHistoryService traningHistoryService, IServiceHistoryService serviceHistoryService, IPersonalInfoService personalInfoService, IAwardPublicationLanguageService awardPublicationLanguageService, IMemberInfoService employeeInfoService, IDrivingLicenseService drivingLicenseService, IMembershipService membershipService, IPassportInfoService passportInfoService, ISpouseChildrenService spouseChildrenService, IPhotographService photographService, IConverter converter)
        {
            this.personalInfoService = personalInfoService;
            this.awardPublicationLanguageService = awardPublicationLanguageService;
            this.employeeInfoService = employeeInfoService;
            this.drivingLicenseService = drivingLicenseService;
            this.membershipService = membershipService;
            this.passportInfoService = passportInfoService;
            this.spouseChildrenService = spouseChildrenService;
            this.serviceHistoryService = serviceHistoryService;
            this.traningHistoryService = traningHistoryService;
            this.photographService = photographService;

            _langEmp = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _langAddress = new LangGenerate<Address>(hostingEnvironment.ContentRootPath);
            _langEducation = new LangGenerate<EducationalQualificationLn>(hostingEnvironment.ContentRootPath);
            _langTransfer = new LangGenerate<TransferLogLn>(hostingEnvironment.ContentRootPath);
            _langTraing = new LangGenerate<TrainingLn>(hostingEnvironment.ContentRootPath);
            _langPromotion = new LangGenerate<PromotionLogLn>(hostingEnvironment.ContentRootPath);
            _langSpous = new LangGenerate<SpouseLn>(hostingEnvironment.ContentRootPath);
            _langChildren = new LangGenerate<ChildrenLn>(hostingEnvironment.ContentRootPath);
            _langLicense = new LangGenerate<License>(hostingEnvironment.ContentRootPath);
            _langPassport = new LangGenerate<Passport>(hostingEnvironment.ContentRootPath);
            _langTravel = new LangGenerate<Travel>(hostingEnvironment.ContentRootPath);
            _langLanguage = new LangGenerate<LanguageLn>(hostingEnvironment.ContentRootPath);
            _langAward = new LangGenerate<Award>(hostingEnvironment.ContentRootPath);
            _langBank = new LangGenerate<BankLn>(hostingEnvironment.ContentRootPath);
            _langPublication = new LangGenerate<Publication>(hostingEnvironment.ContentRootPath);
            _langDisciplinary = new LangGenerate<Disciplinary>(hostingEnvironment.ContentRootPath);
            _langLeave = new LangGenerate<LeaveLogLn>(hostingEnvironment.ContentRootPath);

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;

        }


        // GET: InfoView
        public async Task<IActionResult> Index(int id)
        {
            InfoViewModel model = new InfoViewModel
            {
                employee = await personalInfoService.GetEmployeeInfoById(id),
                addressPermanent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "permanent"),
                addressPresent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "present"),
                educationalQualifications = await employeeInfoService.GetEducationalQualificationByEmpId(id),
                spouses = await spouseChildrenService.GetSpouseByEmpId(id),
                childrens = await spouseChildrenService.GetChildrenByEmpId(id),
                drivingLicenses = await drivingLicenseService.GetDrivingLicenseByEmpId(id),
                passportDetails = await passportInfoService.GetPassportInfoByEmpId(id),
                employeeMemberships = await membershipService.GetMembershipInfoByEmpId(id),
                employeeLanguages = await awardPublicationLanguageService.GetLanguageByEmpId(id),
                employeeAwards = await awardPublicationLanguageService.GetAwardsByEmpId(id),
                publications = await awardPublicationLanguageService.GetPublicationsByEmpId(id),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                traningLogs = await traningHistoryService.GetTraningHistoryByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile")
            };

            model.fLang = new InfoViewModelAllLn
            {
                employeeInfoLn = _langEmp.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                addressLn = _langAddress.PerseLang("Employee/AddressEN.json", "Employee/AddressBN.json", Request.Cookies["lang"]),
                educationalQualificationLn = _langEducation.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                spouseLn = _langSpous.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]),
                transferLogLn = _langTransfer.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]),
                trainingLn = _langTraing.PerseLang("Employee/TrainingEN.json", "Employee/TrainingBN.json", Request.Cookies["lang"]),
                promotionLogLn = _langPromotion.PerseLang("Employee/PromotionLogEN.json", "Employee/PromotionLogBN.json", Request.Cookies["lang"]),
                childrenLn = _langChildren.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                licenseLn = _langLicense.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                travelLn = _langTravel.PerseLang("Employee/TraveInfoEN.json", "Employee/TraveInfoBN.json", Request.Cookies["lang"]),
                passportLn = _langPassport.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                languageLn = _langLanguage.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                //awardLn = _langAward.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                bankLn = _langBank.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                publicationLn = _langPublication.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                disciplinaryLn = _langDisciplinary.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                leaveLogLn = _langLeave.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
            };

            if (model.employee == null) model.employee = new OPUSERP.CLUB.Data.Member.MemberInfo
            {
                religion = new Religion(),
                memberType = new MemberType()
            };

            if (model.addressPermanent == null) model.addressPermanent = new MemberAddress 
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.addressPresent == null) model.addressPresent = new MemberAddress
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.photograph == null) model.photograph = new MemberPhotograph
            {
                url = "images/user.png"
            };


            return View(model);
        }

        // GET: PrintView
        [AllowAnonymous]
        public async Task<IActionResult> PrintView(int id)
        {
            InfoViewModel model = new InfoViewModel
            {
                employee = await personalInfoService.GetEmployeeInfoById(id),
                addressPermanent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "permanent"),
                addressPresent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "present"),
                educationalQualifications = await employeeInfoService.GetEducationalQualificationByEmpId(id),
                spouses = await spouseChildrenService.GetSpouseByEmpId(id),
                childrens = await spouseChildrenService.GetChildrenByEmpId(id),
                drivingLicenses = await drivingLicenseService.GetDrivingLicenseByEmpId(id),
                passportDetails = await passportInfoService.GetPassportInfoByEmpId(id),
                employeeMemberships = await membershipService.GetMembershipInfoByEmpId(id),
                employeeLanguages = await awardPublicationLanguageService.GetLanguageByEmpId(id),
                employeeAwards = await awardPublicationLanguageService.GetAwardsByEmpId(id),
                publications = await awardPublicationLanguageService.GetPublicationsByEmpId(id),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                traningLogs = await traningHistoryService.GetTraningHistoryByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile")
            };

            model.fLang = new InfoViewModelAllLn
            {
                employeeInfoLn = _langEmp.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                addressLn = _langAddress.PerseLang("Employee/AddressEN.json", "Employee/AddressBN.json", Request.Cookies["lang"]),
                educationalQualificationLn = _langEducation.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                spouseLn = _langSpous.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]),
                transferLogLn = _langTransfer.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]),
                trainingLn = _langTraing.PerseLang("Employee/TrainingEN.json", "Employee/TrainingBN.json", Request.Cookies["lang"]),
                promotionLogLn = _langPromotion.PerseLang("Employee/PromotionLogEN.json", "Employee/PromotionLogBN.json", Request.Cookies["lang"]),
                childrenLn = _langChildren.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                licenseLn = _langLicense.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                travelLn = _langTravel.PerseLang("Employee/TraveInfoEN.json", "Employee/TraveInfoBN.json", Request.Cookies["lang"]),
                passportLn = _langPassport.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                languageLn = _langLanguage.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                //awardLn = _langAward.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                bankLn = _langBank.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                publicationLn = _langPublication.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                disciplinaryLn = _langDisciplinary.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                leaveLogLn = _langLeave.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
            };

            if (model.employee == null) model.employee = new OPUSERP.CLUB.Data.Member.MemberInfo
            {
                religion = new Religion(),
                memberType = new MemberType()
            };

            if (model.addressPermanent == null) model.addressPermanent = new MemberAddress
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.addressPresent == null) model.addressPresent = new MemberAddress
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.photograph == null) model.photograph = new MemberPhotograph
            {
                url = "images/user.png"
            };


            return View(model);
        }

        // GET: PrintView
        [AllowAnonymous]
        public async Task<IActionResult> EmployeeDetailsView(int id)
        {
            var model = new InfoViewModel
            {
                employee = await personalInfoService.GetEmployeeInfoById(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile")
            };

            model.fLang = new InfoViewModelAllLn
            {
                employeeInfoLn = _langEmp.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                addressLn = _langAddress.PerseLang("Employee/AddressEN.json", "Employee/AddressBN.json", Request.Cookies["lang"]),
                educationalQualificationLn = _langEducation.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                spouseLn = _langSpous.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]),
                transferLogLn = _langTransfer.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]),
                trainingLn = _langTraing.PerseLang("Employee/TrainingEN.json", "Employee/TrainingBN.json", Request.Cookies["lang"]),
                promotionLogLn = _langPromotion.PerseLang("Employee/PromotionLogEN.json", "Employee/PromotionLogBN.json", Request.Cookies["lang"]),
                childrenLn = _langChildren.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                licenseLn = _langLicense.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                travelLn = _langTravel.PerseLang("Employee/TraveInfoEN.json", "Employee/TraveInfoBN.json", Request.Cookies["lang"]),
                passportLn = _langPassport.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                languageLn = _langLanguage.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                //awardLn = _langAward.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                bankLn = _langBank.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                publicationLn = _langPublication.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                disciplinaryLn = _langDisciplinary.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                leaveLogLn = _langLeave.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
            };

            return View(model);
        }

        //Individual Info View
        public async Task<IActionResult> View(int id, string section)
        {
            ViewBag.section = section;
            InfoViewModel model = new InfoViewModel
            {
                employee = await personalInfoService.GetEmployeeInfoById(id),
                addressPermanent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "permanent"),
                addressPresent = await employeeInfoService.GetAddressByTypeAndEmpId(id, "present"),
                educationalQualifications = await employeeInfoService.GetEducationalQualificationByEmpId(id),
                spouses = await spouseChildrenService.GetSpouseByEmpId(id),
                childrens = await spouseChildrenService.GetChildrenByEmpId(id),
                drivingLicenses = await drivingLicenseService.GetDrivingLicenseByEmpId(id),
                passportDetails = await passportInfoService.GetPassportInfoByEmpId(id),
                employeeMemberships = await membershipService.GetMembershipInfoByEmpId(id),
                employeeLanguages = await awardPublicationLanguageService.GetLanguageByEmpId(id),
                employeeAwards = await awardPublicationLanguageService.GetAwardsByEmpId(id),
                publications = await awardPublicationLanguageService.GetPublicationsByEmpId(id),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                traningLogs = await traningHistoryService.GetTraningHistoryByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile")
            };

            model.fLang = new InfoViewModelAllLn
            {
                employeeInfoLn = _langEmp.PerseLang("Employee/EmployeeInfoEN.json", "Employee/EmployeeInfoBN.json", Request.Cookies["lang"]),
                addressLn = _langAddress.PerseLang("Employee/AddressEN.json", "Employee/AddressBN.json", Request.Cookies["lang"]),
                educationalQualificationLn = _langEducation.PerseLang("Employee/EducationalQualificationEN.json", "Employee/EducationalQualificationBN.json", Request.Cookies["lang"]),
                spouseLn = _langSpous.PerseLang("Employee/SpouseEN.json", "Employee/SpouseBN.json", Request.Cookies["lang"]),
                transferLogLn = _langTransfer.PerseLang("Employee/TransferLogEN.json", "Employee/TransferLogBN.json", Request.Cookies["lang"]),
                trainingLn = _langTraing.PerseLang("Employee/TrainingEN.json", "Employee/TrainingBN.json", Request.Cookies["lang"]),
                promotionLogLn = _langPromotion.PerseLang("Employee/PromotionLogEN.json", "Employee/PromotionLogBN.json", Request.Cookies["lang"]),
                childrenLn = _langChildren.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                licenseLn = _langLicense.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                travelLn = _langTravel.PerseLang("Employee/TraveInfoEN.json", "Employee/TraveInfoBN.json", Request.Cookies["lang"]),
                passportLn = _langPassport.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                languageLn = _langLanguage.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                //awardLn = _langAward.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                bankLn = _langBank.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                publicationLn = _langPublication.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                disciplinaryLn = _langDisciplinary.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                leaveLogLn = _langLeave.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
            };

            if (model.employee == null) model.employee = new OPUSERP.CLUB.Data.Member.MemberInfo
            {
                religion = new Religion(),
                memberType = new MemberType()
            };

            if (model.addressPermanent == null) model.addressPermanent = new MemberAddress
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.addressPresent == null) model.addressPresent = new MemberAddress
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.photograph == null) model.photograph = new MemberPhotograph
            {
                url = "images/user.png"
            };

            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult pdspdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/MemberInfo/InfoView/PrintView/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
    }
}