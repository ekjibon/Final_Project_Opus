//using OPUSERP.Areas.HRPMSACR.Models.Lang;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSEmployee.Models.Lang;
using OPUSERP.Areas.HRPMSLibrary.Models.Lang;
using OPUSERP.Data.Entity.Master;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.ACR.Interfaces;
using OPUSERP.HRPMS.Services.DisciplineInvestigation.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.Library.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Area("HRPMSEmployee")]
    [Authorize]
    public class InfoViewController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IWagesPersonalInfoService wagesPersonalInfoService;
        private readonly IAwardPublicationLanguageService awardPublicationLanguageService;
        private readonly IEmployeeInfoService employeeInfoService;
        private readonly IWagesEmployeeInfoService wagesEmployeeInfoService;
        private readonly IWagesEmergencyContactService wagesEmergencyContactService;
        private readonly IWagesReferenceService wagesReferenceService;
        private readonly IWagesPhotographService wagesPhotographService;
        private readonly IDrivingLicenseService drivingLicenseService;
        private readonly IMembershipService membershipService;
        private readonly IPassportInfoService passportInfoService;
        private readonly ISpouseChildrenService spouseChildrenService;
        private readonly ITravelInfoService travelInfoService;
        private readonly IPromotionService promotionService;
        private readonly IAcrInfoService acrInfoService;
        private readonly IServiceHistoryService serviceHistoryService;
        private readonly ITraningHistoryService traningHistoryService;
        private readonly IPromotionLogService promotionLogService;
        private readonly IDisciplineInvestigation disciplineInvestigation;
        private readonly IPhotographService photographService;
        private readonly IBankInfoService bankInfoService;
        private readonly IWagesBankInfoService wagesBankInfoService;
        private readonly ILeaveLogService leaveLogService;
        private readonly IUserInfoes userInfo;
        private readonly INomineeService nomineeService;
        private readonly INomineeDetailService nomineeDetailService;
        private readonly ISupervisorService supervisorService;
        public readonly IOtherActivityService otherActivityService;
        public readonly IBelongingsService belongingsService;
        public readonly IPriviousEmploymentService priviousEmploymentService;
        public readonly IFreedomFighterService freedomFighterService;
        public readonly IReferenceService referenceService;
        public readonly IEmergencyContactService emergencyContactService;
        public readonly IEmployeeProjectActivityService employeeProjectActivityService;
      
        private readonly string rootPath;
        private readonly MyPDF myPDF;


        //langSection
        private readonly LangGenerate<EmployeeInfoLn> _langEmp;
        private readonly LangGenerate<Models.Lang.Address> _langAddress;
        private readonly LangGenerate<EducationalQualificationLn> _langEducation;
        private readonly LangGenerate<TransferLogLn> _langTransfer;
        private readonly LangGenerate<TrainingLn> _langTraing;
        private readonly LangGenerate<PromotionLogLn> _langPromotion;
        private readonly LangGenerate<OPUSERP.Areas.HRPMSACR.Models.Lang.ACRLn> _langAcr;
        private readonly LangGenerate<SpouseLn> _langSpous;
        private readonly LangGenerate<ChildrenLn> _langChildren;
        private readonly LangGenerate<License> _langLicense;
        private readonly LangGenerate<Passport> _langPassport;
        private readonly LangGenerate<Travel> _langTravel;
        private readonly LangGenerate<LanguageLn> _langLanguage;
        private readonly LangGenerate<Award> _langAward;
        private readonly LangGenerate<BankLn> _langBank;
        private readonly LangGenerate<Models.Lang.Publication> _langPublication;
        private readonly LangGenerate<Disciplinary> _langDisciplinary;

        private readonly IBorrowerInfoService borrowerInfoService;
        private readonly LangGenerate<BookLn> _langBook;
        private readonly LangGenerate<LeaveLogLn> _langLeave;
        private readonly LangGenerate<NomineeLn> _langNominee;
        private readonly LangGenerate<SupervisorLn> _langSupervisor;
        private readonly LangGenerate<Membership> _langMembership;




        public InfoViewController(IHostingEnvironment hostingEnvironment, IEmployeeProjectActivityService employeeProjectActivityService, IEmergencyContactService emergencyContactService, IReferenceService referenceService, IFreedomFighterService freedomFighterService, IPriviousEmploymentService priviousEmploymentService, IBelongingsService belongingsService, IOtherActivityService otherActivityService, ISupervisorService supervisorService, INomineeDetailService nomineeDetailService, INomineeService nomineeService, IWagesPersonalInfoService wagesPersonalInfoService, IWagesEmployeeInfoService wagesEmployeeInfoService, IWagesEmergencyContactService wagesEmergencyContactService, IWagesReferenceService wagesReferenceService, IWagesBankInfoService wagesBankInfoService, IWagesPhotographService wagesPhotographService,ILeaveLogService leaveLogService, IBorrowerInfoService borrowerInfoService, IBankInfoService bankInfoService, ITravelInfoService travelInfoService, IDisciplineInvestigation disciplineInvestigation, IPromotionLogService promotionLogService, ITraningHistoryService traningHistoryService, IServiceHistoryService serviceHistoryService, IAcrInfoService acrInfoService, IPromotionService promotionService, IPersonalInfoService personalInfoService, IAwardPublicationLanguageService awardPublicationLanguageService, IEmployeeInfoService employeeInfoService, IDrivingLicenseService drivingLicenseService, IMembershipService membershipService, IPassportInfoService passportInfoService, ISpouseChildrenService spouseChildrenService, IPhotographService photographService, IConverter converter, IUserInfoes userInfo)
        {
            this.userInfo = userInfo;
            this.personalInfoService = personalInfoService;
            this.wagesPersonalInfoService = wagesPersonalInfoService;
            this.wagesEmployeeInfoService = wagesEmployeeInfoService;
            this.wagesEmergencyContactService = wagesEmergencyContactService;
            this.wagesReferenceService = wagesReferenceService;
            this.wagesBankInfoService = wagesBankInfoService;
            this.wagesPhotographService = wagesPhotographService;
            this.awardPublicationLanguageService = awardPublicationLanguageService;
            this.employeeInfoService = employeeInfoService;
            this.drivingLicenseService = drivingLicenseService;
            this.membershipService = membershipService;
            this.passportInfoService = passportInfoService;
            this.spouseChildrenService = spouseChildrenService;
            this.travelInfoService = travelInfoService;
            this.promotionService = promotionService;
            this.acrInfoService = acrInfoService;
            this.serviceHistoryService = serviceHistoryService;
            this.traningHistoryService = traningHistoryService;
            this.promotionLogService = promotionLogService;
            this.disciplineInvestigation = disciplineInvestigation;
            this.photographService = photographService;
            this.bankInfoService = bankInfoService;
            this.borrowerInfoService = borrowerInfoService;
            this.leaveLogService = leaveLogService;
            this.nomineeService = nomineeService;
            this.nomineeDetailService = nomineeDetailService;
            this.supervisorService = supervisorService;
            this.otherActivityService = otherActivityService;
            this.belongingsService = belongingsService;
            this.priviousEmploymentService = priviousEmploymentService;
            this.freedomFighterService = freedomFighterService;
            this.referenceService = referenceService;
            this.emergencyContactService = emergencyContactService;
            this.employeeProjectActivityService = employeeProjectActivityService;

             _langEmp = new LangGenerate<EmployeeInfoLn>(hostingEnvironment.ContentRootPath);
            _langAddress = new LangGenerate<Models.Lang.Address>(hostingEnvironment.ContentRootPath);
            _langEducation = new LangGenerate<EducationalQualificationLn>(hostingEnvironment.ContentRootPath);
            _langTransfer = new LangGenerate<TransferLogLn>(hostingEnvironment.ContentRootPath);
            _langTraing = new LangGenerate<TrainingLn>(hostingEnvironment.ContentRootPath);
            _langPromotion = new LangGenerate<PromotionLogLn>(hostingEnvironment.ContentRootPath);
            _langAcr = new LangGenerate<OPUSERP.Areas.HRPMSACR.Models.Lang.ACRLn>(hostingEnvironment.ContentRootPath);
            _langSpous = new LangGenerate<SpouseLn>(hostingEnvironment.ContentRootPath);
            _langChildren = new LangGenerate<ChildrenLn>(hostingEnvironment.ContentRootPath);
            _langLicense = new LangGenerate<License>(hostingEnvironment.ContentRootPath);
            _langPassport = new LangGenerate<Passport>(hostingEnvironment.ContentRootPath);
            _langTravel = new LangGenerate<Travel>(hostingEnvironment.ContentRootPath);
            _langLanguage = new LangGenerate<LanguageLn>(hostingEnvironment.ContentRootPath);
            _langAward = new LangGenerate<Award>(hostingEnvironment.ContentRootPath);
            _langBank = new LangGenerate<BankLn>(hostingEnvironment.ContentRootPath);
            _langPublication = new LangGenerate<Models.Lang.Publication>(hostingEnvironment.ContentRootPath);
            _langDisciplinary = new LangGenerate<Disciplinary>(hostingEnvironment.ContentRootPath);
            _langBook = new LangGenerate<BookLn>(hostingEnvironment.ContentRootPath);
            _langLeave = new LangGenerate<LeaveLogLn>(hostingEnvironment.ContentRootPath);
            _langNominee = new LangGenerate<NomineeLn>(hostingEnvironment.ContentRootPath);
            _langSupervisor = new LangGenerate<SupervisorLn>(hostingEnvironment.ContentRootPath);
            _langMembership = new LangGenerate<Membership>(hostingEnvironment.ContentRootPath);

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
                traveInfos = await travelInfoService.GetTraveInfoByEmpId(id),
                promotions = await promotionService.GetPromotionInfoByEmpId(id),
                acrInfos = await acrInfoService.GetAcrInfoByEmpId(id),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                traningLogs = await traningHistoryService.GetTraningHistoryByEmpId(id),
                promotionLogs = await promotionLogService.GetPromotionLogByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                disciplinaries = await disciplineInvestigation.GetAllDisciplinaryByEmpIdandStatus(id, "approved"),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                borrowerInfos = await borrowerInfoService.GetBorrowerInfoByEmpId(id),
                bankInfos = await bankInfoService.GetBankInfoByEmpId(id),
                leaveLogs = await leaveLogService.GetLeaveLogByEmpId(id),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
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
                aCRLn = _langAcr.PerseLang("ACR/ACRDetailsEN.json", "ACR/ACRDetailsBN.json", Request.Cookies["lang"]),
                childrenLn = _langChildren.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                licenseLn = _langLicense.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                travelLn = _langTravel.PerseLang("Employee/TraveInfoEN.json", "Employee/TraveInfoBN.json", Request.Cookies["lang"]),
                passportLn = _langPassport.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                languageLn = _langLanguage.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                awardLn = _langAward.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                bankLn = _langBank.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                publicationLn = _langPublication.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                disciplinaryLn = _langDisciplinary.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                bookLn = _langBook.PerseLang("Book/BorrowerEN.json", "Book/BorrowerBN.json", Request.Cookies["lang"]),
                leaveLogLn = _langLeave.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
            };

            if (model.employee == null) model.employee = new EmployeeInfo
            {
                religion = new HRPMS.Data.Entity.Master.Religion(),
                employeeType = new HRPMS.Data.Entity.Master.EmployeeType()
            };

            if (model.addressPermanent == null) model.addressPermanent = new HRPMS.Data.Entity.Employee.Address
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.addressPresent == null) model.addressPresent = new HRPMS.Data.Entity.Employee.Address
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.photograph == null) model.photograph = new Photograph
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
                traveInfos = await travelInfoService.GetTraveInfoByEmpId(id),
                promotions = await promotionService.GetPromotionInfoByEmpId(id),
                acrInfos = await acrInfoService.GetAcrInfoByEmpId(id),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                traningLogs = await traningHistoryService.GetTraningHistoryByEmpId(id),
                promotionLogs = await promotionLogService.GetPromotionLogByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                disciplinaries = await disciplineInvestigation.GetAllDisciplinaryByEmpIdandStatus(id, "approved"),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                bankInfos = await bankInfoService.GetBankInfoByEmpId(id),
                leaveLogs = await leaveLogService.GetLeaveLogByEmpId(id),
                borrowerInfos = await borrowerInfoService.GetBorrowerInfoByEmpId(id),
                nomineeDetails = await nomineeDetailService.GetNomineeDetailByEmployeeId(id),
                supervisors=await supervisorService.GetSupervisorByEmpId(id),
                otherActivities=await otherActivityService.GetOtherActivityByEmpId(id),
                belongings=await belongingsService.GetBelongingsByEmpId(id),
                priviousEmployments=await priviousEmploymentService.GetPriviousEmploymentByEmpId(id),
                freedomFighters=await freedomFighterService.GetFreedomFighterlistByEmpId(id),
                references=await referenceService.GetReferenceByEmpId(id),
                emergencyContacts=await emergencyContactService.GetEmergencyContactByEmpId(id),
                employeeProjectActivities=await employeeProjectActivityService.GetEmployeeProjectActivityByEmpId(id)





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
                aCRLn = _langAcr.PerseLang("ACR/ACRDetailsEN.json", "ACR/ACRDetailsBN.json", Request.Cookies["lang"]),
                childrenLn = _langChildren.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                licenseLn = _langLicense.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                travelLn = _langTravel.PerseLang("Employee/TraveInfoEN.json", "Employee/TraveInfoBN.json", Request.Cookies["lang"]),
                passportLn = _langPassport.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                languageLn = _langLanguage.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                awardLn = _langAward.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                bankLn = _langBank.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                publicationLn = _langPublication.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                disciplinaryLn = _langDisciplinary.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                leaveLogLn = _langLeave.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
                bookLn = _langBook.PerseLang("Book/BorrowerEN.json", "Book/BorrowerBN.json", Request.Cookies["lang"]),
                nomineeLn = _langNominee.PerseLang("Employee/NomineeListEN.json", "Employee/NomineeListBN.json", Request.Cookies["lang"]),
                supervisorLn = _langSupervisor.PerseLang("Employee/SupervisorEN.json", "Employee/SupervisorBN.json", Request.Cookies["lang"]),
                membershipLn=_langMembership.PerseLang("Employee/MembershipEN.json", "Employee/MembershipBN.json", Request.Cookies["lang"])


            };

            if (model.employee == null) model.employee = new EmployeeInfo
            {
                religion = new HRPMS.Data.Entity.Master.Religion(),
                employeeType = new HRPMS.Data.Entity.Master.EmployeeType()
            };

            if (model.addressPermanent == null) model.addressPermanent = new HRPMS.Data.Entity.Employee.Address
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.addressPresent == null) model.addressPresent = new HRPMS.Data.Entity.Employee.Address
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.photograph == null) model.photograph = new Photograph
            {
                url = "images/user.png"
            };


            return View(model);
        }

        // GET: InfoView
        public async Task<IActionResult> WagesIndex(int id)
        {
            InfoViewModel model = new InfoViewModel
            {
                wagesEmployeeInfo = await wagesPersonalInfoService.GetEmployeeInfoById(id),
                employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(id),
                wagesAddressPermanent = await wagesEmployeeInfoService.GetAddressByTypeAndEmpId(id, "permanent"),
                wagesAddressPresent = await wagesEmployeeInfoService.GetAddressByTypeAndEmpId(id, "present"),
                wagesPhotograph = await wagesPhotographService.GetPhotographByEmpIdAndType(id, "profile"),
                wagesBankInfos = await wagesBankInfoService.GetBankInfoByEmpId(id),
                wagesReferences = await wagesReferenceService.GetReferenceByEmpId(id),
                wagesEmergencyContacts = await wagesEmergencyContactService.GetEmergencyContactByEmpId(id)
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
                aCRLn = _langAcr.PerseLang("ACR/ACRDetailsEN.json", "ACR/ACRDetailsBN.json", Request.Cookies["lang"]),
                childrenLn = _langChildren.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                licenseLn = _langLicense.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                travelLn = _langTravel.PerseLang("Employee/TraveInfoEN.json", "Employee/TraveInfoBN.json", Request.Cookies["lang"]),
                passportLn = _langPassport.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                languageLn = _langLanguage.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                awardLn = _langAward.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                bankLn = _langBank.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                publicationLn = _langPublication.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                disciplinaryLn = _langDisciplinary.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                bookLn = _langBook.PerseLang("Book/BorrowerEN.json", "Book/BorrowerBN.json", Request.Cookies["lang"]),
                leaveLogLn = _langLeave.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
            };

            if (model.wagesEmployeeInfo == null) model.wagesEmployeeInfo = new WagesEmployeeInfo
            {
                religion = new HRPMS.Data.Entity.Master.Religion(),
                employeeType = new HRPMS.Data.Entity.Master.EmployeeType()
            };

            if (model.wagesAddressPermanent == null) model.wagesAddressPermanent = new WagesAddress
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.wagesAddressPresent == null) model.wagesAddressPresent = new WagesAddress
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.wagesPhotograph == null) model.wagesPhotograph = new WagesPhotograph
            {
                url = "images/user.png"
            };


            return View(model);
        }

        // GET: PrintView
        [AllowAnonymous]
        public async Task<IActionResult> WagesPrintView(int id)
        {
            InfoViewModel model = new InfoViewModel
            {
                wagesEmployeeInfo = await wagesPersonalInfoService.GetEmployeeInfoById(id),
                employeeNameCode = await wagesPersonalInfoService.GetEmployeeNameCodeById(id),
                wagesAddressPermanent = await wagesEmployeeInfoService.GetAddressByTypeAndEmpId(id, "permanent"),
                wagesAddressPresent = await wagesEmployeeInfoService.GetAddressByTypeAndEmpId(id, "present"),
                wagesPhotograph = await wagesPhotographService.GetPhotographByEmpIdAndType(id, "profile"),
                wagesBankInfos = await wagesBankInfoService.GetBankInfoByEmpId(id),
                wagesReferences = await wagesReferenceService.GetReferenceByEmpId(id),
                wagesEmergencyContacts = await wagesEmergencyContactService.GetEmergencyContactByEmpId(id)
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
                aCRLn = _langAcr.PerseLang("ACR/ACRDetailsEN.json", "ACR/ACRDetailsBN.json", Request.Cookies["lang"]),
                childrenLn = _langChildren.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                licenseLn = _langLicense.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                travelLn = _langTravel.PerseLang("Employee/TraveInfoEN.json", "Employee/TraveInfoBN.json", Request.Cookies["lang"]),
                passportLn = _langPassport.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                languageLn = _langLanguage.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                awardLn = _langAward.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                bankLn = _langBank.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                publicationLn = _langPublication.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                disciplinaryLn = _langDisciplinary.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                leaveLogLn = _langLeave.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
                bookLn = _langBook.PerseLang("Book/BorrowerEN.json", "Book/BorrowerBN.json", Request.Cookies["lang"]),
            };

            if (model.wagesEmployeeInfo == null) model.wagesEmployeeInfo = new WagesEmployeeInfo
            {
                religion = new HRPMS.Data.Entity.Master.Religion(),
                employeeType = new HRPMS.Data.Entity.Master.EmployeeType()
            };

            if (model.wagesAddressPermanent == null) model.wagesAddressPermanent = new WagesAddress
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.wagesAddressPresent == null) model.wagesAddressPresent = new WagesAddress
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.wagesPhotograph == null) model.wagesPhotograph = new WagesPhotograph
            {
                url = "images/user.png"
            };
            
            return View(model);
        }

        //PrintPDF
        [AllowAnonymous]
        public IActionResult Wagespdspdf(int id)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;
            string fileName;

            string url = scheme + "://" + host + "/HRPMSEmployee/InfoView/WagesPrintView/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
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
                traveInfos = await travelInfoService.GetTraveInfoByEmpId(id),
                promotions = await promotionService.GetPromotionInfoByEmpId(id),
                acrInfos = await acrInfoService.GetAcrInfoByEmpId(id),
                transferLogs = await serviceHistoryService.GetServiceHistoryByEmpId(id),
                traningLogs = await traningHistoryService.GetTraningHistoryByEmpId(id),
                promotionLogs = await promotionLogService.GetPromotionLogByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id),
                disciplinaries = await disciplineInvestigation.GetAllDisciplinaryByEmpIdandStatus(id, "approved"),
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                bankInfos = await bankInfoService.GetBankInfoByEmpId(id),
                leaveLogs = await leaveLogService.GetLeaveLogByEmpId(id),
                borrowerInfos = await borrowerInfoService.GetBorrowerInfoByEmpId(id),
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
                aCRLn = _langAcr.PerseLang("ACR/ACRDetailsEN.json", "ACR/ACRDetailsBN.json", Request.Cookies["lang"]),
                childrenLn = _langChildren.PerseLang("Employee/ChildrenEN.json", "Employee/ChildrenBN.json", Request.Cookies["lang"]),
                licenseLn = _langLicense.PerseLang("Employee/LicenseEN.json", "Employee/LicenseBN.json", Request.Cookies["lang"]),
                travelLn = _langTravel.PerseLang("Employee/TraveInfoEN.json", "Employee/TraveInfoBN.json", Request.Cookies["lang"]),
                passportLn = _langPassport.PerseLang("Employee/PassportEN.json", "Employee/PassportBN.json", Request.Cookies["lang"]),
                languageLn = _langLanguage.PerseLang("Employee/LanguageEN.json", "Employee/LanguageBN.json", Request.Cookies["lang"]),
                awardLn = _langAward.PerseLang("Employee/AwardEN.json", "Employee/AwardBN.json", Request.Cookies["lang"]),
                bankLn = _langBank.PerseLang("Employee/BankEN.json", "Employee/BankBN.json", Request.Cookies["lang"]),
                publicationLn = _langPublication.PerseLang("Employee/PublicationEN.json", "Employee/PublicationBN.json", Request.Cookies["lang"]),
                disciplinaryLn = _langDisciplinary.PerseLang("Employee/DisciplinaryEN.json", "Employee/DisciplinaryBN.json", Request.Cookies["lang"]),
                leaveLogLn = _langLeave.PerseLang("Employee/LeaveLogEN.json", "Employee/LeaveLogBN.json", Request.Cookies["lang"]),
                bookLn = _langBook.PerseLang("Book/BorrowerEN.json", "Book/BorrowerBN.json", Request.Cookies["lang"]),
            };

            if (model.employee == null) model.employee = new EmployeeInfo
            {
                religion = new HRPMS.Data.Entity.Master.Religion(),
                employeeType = new HRPMS.Data.Entity.Master.EmployeeType()
            };

            if (model.addressPermanent == null) model.addressPermanent = new HRPMS.Data.Entity.Employee.Address
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.addressPresent == null) model.addressPresent = new HRPMS.Data.Entity.Employee.Address
            {
                division = new Division(),
                district = new District(),
                thana = new Thana(),
                country = new Country()
            };

            if (model.photograph == null) model.photograph = new Photograph
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

            string url = scheme + "://" + host + "/HRPMSEmployee/InfoView/PrintView/" + id;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        public async Task<IActionResult> PersonalProfile()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
            var EmpInfo = await personalInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
            int empId = 0;
            if (EmpInfo != null)
            {
                empId = EmpInfo.Id;
            }

            return RedirectToAction("Index", "InfoView", new
            {
                id = empId,
            });
        }
    }
}