using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OPUSERP.Accounting.Data.Entity.AccountingSettings;
using OPUSERP.Accounting.Data.Entity.FDR;
using OPUSERP.Accounting.Data.Entity.MasterData;
using OPUSERP.Accounting.Data.Entity.NonPoTransaction;
using OPUSERP.Accounting.Data.Entity.Voucher;
using OPUSERP.Areas.Accounting.Models;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Areas.CRMClient.Models;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.Areas.HRPMSACR.Models;
using OPUSERP.Areas.HRPMSAttendence.Models;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Areas.HRPMSLeave.Models.NotMapped;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Areas.SCMPurchaseOrder.Models;
using OPUSERP.Areas.SCMPurchaseProcess.Models;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.Areas.SCMStock.Models;
using OPUSERP.Areas.VMS.Models;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.CRM.Data.Entity.Client;
using OPUSERP.CRM.Data.Entity.Cold;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.FixedAsset.Data.Entity.AssetRegister;
using OPUSERP.FixedAsset.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.ACR;
using OPUSERP.HRPMS.Data.Entity.Assignment;
using OPUSERP.HRPMS.Data.Entity.Attendance;
using OPUSERP.HRPMS.Data.Entity.AwardPublication;
using OPUSERP.HRPMS.Data.Entity.DisciplineInvestigation;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Data.Entity.Leave;
using OPUSERP.HRPMS.Data.Entity.Library;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.HRPMS.Data.Entity.Organogram;
using OPUSERP.HRPMS.Data.Entity.PromotionAndTransfer;
using OPUSERP.HRPMS.Data.Entity.PunishmentCharge;
using OPUSERP.HRPMS.Data.Entity.Recruitment;
using OPUSERP.HRPMS.Data.Entity.Recruitment.ExitInterview;
using OPUSERP.HRPMS.Data.Entity.RetirementAndTermination;
using OPUSERP.HRPMS.Data.Entity.RewardInfo;
using OPUSERP.HRPMS.Data.Entity.Training;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using OPUSERP.HRPMS.Data.Entity.Travel;
using OPUSERP.HRPMS.Data.Entity.VehicleInfo;
using OPUSERP.HRPMS.Data.Entity.Visitor;
using OPUSERP.Payroll.Data.Entity.PF;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.POS.Data.Entity;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.Sales.Data.Entity.Collection;
using OPUSERP.Sales.Data.Entity.Sales;
using OPUSERP.SCM.Data.Entity.Disbarse;
using OPUSERP.SCM.Data.Entity.IOU;
using OPUSERP.SCM.Data.Entity.JobAssign;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Matrix;
using OPUSERP.SCM.Data.Entity.PurchaseOrder;
using OPUSERP.SCM.Data.Entity.PurchaseProcess;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Data.Entity.Stock;
using OPUSERP.SCM.Data.Entity.Supplier;
using OPUSERP.VEMS.Data.Entity.MasterData;
using OPUSERP.VEMS.Data.Entity.RegistrationForm;
using OPUSERP.VIMS.Data;
using OPUSERP.VMS.Data.Entity.Agreement;
using OPUSERP.VMS.Data.Entity.CarManagement;
using OPUSERP.VMS.Data.Entity.FuelInfo;
using OPUSERP.VMS.Data.Entity.FuelStation;
using OPUSERP.VMS.Data.Entity.Inspection;
using OPUSERP.VMS.Data.Entity.Inventory;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.Requisition;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.SOR;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Models;
using System;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using StatusLog = OPUSERP.Data.Entity.Matrix.StatusLog;
using OPUSERP.CRM.Data.Entity.Budget;
using OPUSERP.Models.Dashboard;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.DMS.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Wages;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.Purchase.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Gratuity;
using OPUSERP.Workflow.Data.Entity;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Patient.Data.Entity;
using OPUSERP.Meeting.Data.Entity;
using OPUSERP.Areas.Patient.Models;
using OPUSERP.Areas.FAMSAssetRegister.Models;
using OPUSERP.Areas.Distribution.Models;
using OPUSERP.SEBA.Data.Entity;
using OPUSERP.Shagotom.Data.Entity.Visitor;
using OPUSERP.TAMS.Data.Entity;
using OPUSERP.Areas.SCMDashboard.Models;
using OPUSERP.CRM.Data.Entity.Notes;
using OPUSERP.MessageBox.Data;
using OPUSERP.Areas.HRPMSReport.Models;
using OPUSERP.Areas.SCMReport.Models;
using OPUSERP.Rental.Data.Entity.Sales;
using OPUSERP.Areas.MasterData.Models;
using CommentsViewModel = OPUSERP.VMS.Models.CommentsViewModel;
using OPUSERP.Areas.Rental.Models;
using OPUSERP.Areas.DMS.Models;
using OPUSERP.SCM.Data.Entity.ProjectStatus;
using OPUSERP.Areas.SCMIOU.Models;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.Inventory.Models;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Data.Event;
using OPUSERP.CLUB.Data.Notice;
using OPUSERP.CLUB.Data.Bulk;
using OPUSERP.CLUB.Data.Forum;
using OPUSERP.CLUB.Data.Finance;
using OPUSERP.CLUB.Data.Gallery;
using OPUSERP.CLUB.Data.News;
using OPUSERP.Areas.MemberInfo.Models;
using OPUSERP.CLUB.Data.Master;
using OPUSERP.Areas.Program.Models;
using OPUSERP.Production.Data.Entity;
using OPUSERP.Areas.Production.Models;
using OPUSERP.Areas.OtherSales.Models.NotMapped;
using OPUSERP.CLUB.Data.Hall;

namespace OPUSERP.Data
{
    public class ERPDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ERPDbContext(DbContextOptions<ERPDbContext> options, IHttpContextAccessor _httpContextAccessor) : base(options)
        {
            this._httpContextAccessor = _httpContextAccessor;
            //Database.SetCommandTimeout(150000);
        }

        #region Budget
        public DbSet<BudgetHead> budgetHeads { get; set; }
        public DbSet<BudgetSubHead> budgetSubHeads { get; set; }
        public DbSet<BudgetMainHead> budgetMainHeads { get; set; }
        public DbSet<BudgetBranchAddress> budgetBranchAddresses { get; set; }
        public DbSet<FiscalYear> fiscalYears { get; set; }
        public DbSet<UnitOfTaka> unitOfTakas { get; set; }
        public DbSet<BudgetRequsitionMaster> budgetRequsitionMasters { get; set; }
        public DbSet<BudgetRequsitionDetail> budgetRequsitionDetails { get; set; }
        public DbSet<BudgetRequsitionMasterFin> budgetRequsitionMasterFins { get; set; }
        public DbSet<BudgetRequsitionDetailFin> budgetRequsitionDetailFins { get; set; }
        public DbSet<BudgetDisbursementMaster> budgetDisbursementMasters { get; set; }
        public DbSet<BudgetDisbursementDetail> budgetDisbursementDetails { get; set; }
        public DbQuery<ColumnHeading> columnHeadings { get; set; }
        public DbQuery<BudgetRequisitionApprovedListViewModel> budgetRequisitionApprovedListViewModels { get; set; }
        public DbSet<HOBudgetRequsitionMaster> hOBudgetRequsitionMasters { get; set; }
        public DbSet<HOBudgetRequsitionDetail> hOBudgetRequsitionDetails { get; set; }
        public DbSet<BudgetHeadDetail> budgetHeadDetails { get; set; }
        #endregion

        #region ERP User Manage

        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<SendEmailLogStatus> SendEmailLogStatus { get; set; }
        public DbSet<UserTypeGroup> UserTypeGroups { get; set; }
        public DbSet<UserAccessPage> UserAccessPages { get; set; }
        public DbSet<ModuleAccessPage> ModuleAccessPages { get; set; }
        public DbSet<Navbar> Navbars { get; set; }
        public DbSet<MenuBand> MenuBands { get; set; }
        public DbQuery<NavbarViewModel> navbarViewModels { get; set; }
        public DbQuery<UserAccessPageViewModel> userAccessPageViewModels { get; set; }

        public DbSet<UserLogHistory> UserLogHistories { get; set; }
        public DbQuery<Areas.Auth.Models.EmployeeInfoViewModel> employeeInfoViewModels { get; set; }
        public DbQuery<AspNetUsersViewModel> aspNetUsersViews { get; set; }
        public DbQuery<AspNetUsersApproverViewModel> aspNetUsersApproverViews { get; set; }
        #endregion

        #region VIMS Shagotom
        public DbSet<Visitor> Visitors { get; set; }
        #endregion

        #region ERP Master
        public DbSet<Entity.Master.Country> Countries { get; set; }
        public DbSet<Entity.Master.Division> Divisions { get; set; }
        public DbSet<Entity.Master.District> Districts { get; set; }
        public DbSet<Entity.Master.Thana> Thanas { get; set; }
        public DbSet<AddressType> AddressTypes { get; set; }
        public DbSet<AddressCategory> addressCategories { get; set; }
        //public DbSet<Designation> Designations { get; set; }
        //public DbSet<Department> Departments { get; set; }
        public DbSet<ERPModule> ERPModules { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<ERPModuleAssign> ERPModuleAssigns { get; set; }
        public DbSet<CompanyGroup> CompanyGroups { get; set; }
        public DbSet<CompanyAddress> CompanyAddresses { get; set; }
        public DbSet<DocumentPhotoAttachment> DocumentPhotoAttachments { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<SCM.Data.Entity.MasterData.OrganizationType> OrganizationTypes { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierType> SupplierTypes { get; set; }
        public DbSet<PostOffice> PostOffices { get; set; }
        public DbSet<UserBackup> userBackups { get; set; }
        public DbSet<AutonumberingInfo> autonumberingInfos { get; set; }

        public DbQuery<UpdateAspnetUser> updateAspnetUsers { get; set; }

        #endregion

        #region SCM Master Data
        public DbSet<Project> Projects { get; set; }
        public DbSet<SpecificationCategory> SpecificationCategories { get; set; }
        public DbSet<SpecificationDetail> SpecificationDetails { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemSpecification> ItemSpecifications { get; set; }
        public DbSet<ItemCategory> ItemCategories { get; set; }
        public DbSet<AutoGeneratedNumber> AutoGeneratedNumbers { get; set; }
        public DbQuery<ItemCodeViewModel> itemCodeViewModels { get; set; }
        public DbQuery<OrganizationCodeViewModel> organizationCodeViewModels { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<TeamMaster> TeamMasters { get; set; }
        public DbSet<TeamMember> TeamMembers { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<RFQMaster> RFQMasters { get; set; }
        public DbSet<RFQReqDetail> RFQReqDetails { get; set; }
        public DbSet<RFQSupDetail> RFQSupDetails { get; set; }
        public DbSet<RFQPriceMaster> RFQPriceMasters { get; set; }
        public DbSet<RFQReqDetailPrice> RFQReqDetailPrices { get; set; }
        public DbSet<RFQDocumentAttachmentDetail> RFQDocumentAttachmentDetails { get; set; }
        public DbSet<BuyerItemMapping> BuyerItemMappings { get; set; }

        public DbQuery<RFQApprovedListViewModel> RFQApprovedListViews { get; set; }
        public DbQuery<ActivityWiseProjectLocationModel> activityWiseProjectLocationModels { get; set; }

        #endregion

        #region SCM JOB ASSIGN
        public DbSet<JobAssignType> JobAssignTypes { get; set; }
        public DbSet<JobAssignDetail> JobAssignDetails { get; set; }
        public DbSet<JobAssignMaster> JobAssignMasters { get; set; }
        #endregion

        #region SCM Requisition
        public DbSet<RequisitionMaster> RequisitionMasters { get; set; }
        public DbSet<RequisitionDetail> RequisitionDetails { get; set; }
        public DbSet<PreferableVendor> PreferableVendors { get; set; }
        public DbSet<RequisitionAssignToTeamLead> RequisitionAssignToTeamLeads { get; set; }
        public DbSet<RequisitionStatusLog> RequisitionStatusLogs { get; set; }
        public DbQuery<RequisitionAutoNumberViewModel> GetReqAutoNumberBySp { get; set; }
        public DbQuery<MostRecentRequisitionViewModel> mostRecentRequisitions { get; set; }
        public DbQuery<TopTenRequisitionViewModel> topTenRequisition { get; set; }
        public DbQuery<FeaturedItemViewModel> featuredRequisition { get; set; }
        public DbQuery<RequisitionTotalNumberForApproveViewModel> requisitionTotalNumberForApproveViewModels { get; set; }
        public DbQuery<GetRequisitionListForApprovedViewModel> getRequisitionListForApprovedViewModels { get; set; }
        public DbQuery<RequisitionDetailVM> requisitionDetailVMs { get; set; }
        public DbQuery<POSupplierReportViewModel> pOSupplierReportViewModels { get; set; }       
        public DbQuery<PurchaseOrderSuppReportViewModel> purchaseOrderSuppReportViewModels { get; set; }
        public DbQuery<PurchaseOrderItemReportViewModel> purchaseOrderItemReportViewModels { get; set; }
        public DbQuery<PurchaseOrderProjectReportViewModel> purchaseOrderProjectReportViewModels { get; set; }
        public DbQuery<PurchaseOrderDiffReportViewModel> purchaseOrderDiffReportViewModels { get; set; }
        public DbQuery<PurchaseOrderBuyerReportViewModel> purchaseOrderBuyerReportViewModels { get; set; }
        public DbQuery<CumulativeGRQtyBySpecIdViewModel> cumulativeGRQtyBySpecIdViewModels { get; set; }
        public DbQuery<RequisitionGRCumulativeViewModel> requisitionGRCumulativeViewModels { get; set; }
        public DbQuery<MostRecentRequisitionNewViewModel> mostRecentRequisitionsNew { get; set; }
        #endregion

        #region DocumentAttachment
        public DbSet<DocumentAttachment> DocumentAttachments { get; set; }
        public DbSet<DocumentAttachmentDetail> documentAttachmentDetails { get; set; }
        #endregion

        #region SCM Supplier
        public DbSet<SCM.Data.Entity.Supplier.Organization> Organizations { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<SCM.Data.Entity.Supplier.Address> Addresses { get; set; }
        public DbSet<SupplierItemInfo> SupplierItemInfos { get; set; }
        #endregion

        #region SCM Matrix
        public DbSet<ApprovalLog> ApprovalLogs { get; set; }
        public DbSet<MatrixChangeHistory> MatrixChangeHistories { get; set; }
        public DbSet<ApprovalMatrix> ApprovalMatrices { get; set; }
        public DbSet<ApprovalMatrixlog> ApprovalMatriceslog { get; set; }
        public DbSet<MatrixType> MatrixTypes { get; set; }
        public DbSet<ChangeOfDoa> ChangeOfDoas { get; set; }
        public DbSet<ApproverType> ApproverTypes { get; set; }
        public DbSet<StatusInfo> StatusInfos { get; set; }
        public DbSet<StatusLog> StatusLogs { get; set; }
        public DbSet<PrintHistory> PrintHistories { get; set; }
        public DbSet<MailLog> MailLogs { get; set; }
        public DbSet<ApprovalReview> ApprovalReviews { get; set; }
        public DbQuery<ApproverPanelViewModel> panelViewModels { get; set; }
        public DbQuery<ApproverPanelFViewModel> panelViewFModels { get; set; }
        public DbQuery<ApprovalMatrixVMR> approvalMatrixVMRs { get; set; }
        public DbQuery<ChangeDoaViewModel> changeDoaViewModels { get; set; }
        public DbQuery<MatrixInformationVM> matrixInformationVMs { get; set; }
        #endregion

        #region SCM Purchase Process
        public DbSet<CSMaster> CSMasters { get; set; }
        public DbSet<CSDetail> CSDetails { get; set; }
        public DbSet<CSItemCategory> CSItemCategories { get; set; }
        public DbSet<ProcurementType> ProcurementTypes { get; set; }
        public DbSet<ProcurementValue> ProcurementValues { get; set; }
        public DbSet<JustificationType> JustificationTypes { get; set; }
        public DbSet<Justification> Justifications { get; set; }
        public DbQuery<CSDetailsVM> CSDetailsVMs { get; set; }
        public DbQuery<QuotationDetailsVM> QuotationDetailsVMs { get; set; }
        public DbQuery<RequisitionForCSViewModel> RequisitionForCSViewModels { get; set; }
        public DbQuery<GetSupplierWiseItemDetailsViewModel> getSupplierWiseItemDetailsViewModels { get; set; }
        #endregion

        #region SCM Purchase Order
        public DbSet<PurchaseOrderMaster> PurchaseOrderMasters { get; set; }
        public DbSet<PurchaseOrderDetails> PurchaseOrderDetails { get; set; }
        public DbSet<PaymentTerms> PaymentTerms { get; set; }
        public DbSet<PaymentMode> PaymentModes { get; set; }
        public DbSet<DeliveryMode> DeliveryModes { get; set; }
        public DbSet<DeliveryLocation> DeliveryLocations { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<POTermAndCondition> POTermAndConditions { get; set; }
        public DbQuery<PurchaseOrderRPTViewModel> PurchaseOrderRPTViewModels { get; set; }
        public DbQuery<PurchaseOrderDismissViewModel> purchaseOrderDismisses { get; set; }

        public DbSet<PODismissMaster> pODismissMasters { get; set; }
        public DbSet<PODismissDetail> pODismissDetails { get; set; }
        public DbQuery<ItemWIseOrderViewModel> itemWIseOrderViewModels { get; set; }
        public DbQuery<SupplierWIseOrderViewModel> supplierWIseOrderViewModels { get; set; }
        public DbQuery<ItemWIsePurchasedViewModel> itemWIsePurchasedViewModels { get; set; }
        public DbQuery<ItemWIseRequisitionViewModel> itemWIseRequisitionViewModels { get; set; }
        #endregion

        #region SCM IOU
        public DbSet<IOUMaster> IOUMasters { get; set; }
        public DbSet<IOUDetails> IOUDetails { get; set; }
        public DbSet<IOUPayment> IOUPayments { get; set; }
        public DbSet<IOUPaymentMaster> iOUPaymentMasters { get; set; }
        public DbSet<DisbarseMaster> DisbarseMasters { get; set; }
        public DbSet<DisbarseDetail> DisbarseDetails { get; set; }
        #endregion

        #region SCM Stock
        public DbSet<StockMaster> StockMasters { get; set; }
        public DbSet<StockDetails> StockDetails { get; set; }
        public DbSet<ItemReqMaster> ItemReqMasters { get; set; }
        public DbSet<ItemReqDetails> ItemReqDetails { get; set; }
        public DbSet<OpeningStock> OpeningStocks { get; set; }

        public DbQuery<StockBalanceViewModel> stockBalanceViewModels { get; set; }
        public DbQuery<StockSummaryViewModel> stockSummaryViewModels { get; set; }
        public DbQuery<StockBalanceByItemViewModel> stockBalanceByItemViewModels { get; set; }
        #endregion

        #region SCM Site Daily Report
        public DbSet<DailyProgressReport> DailyProgressReports { get; set; }
        public DbSet<SiteConstraint> SiteConstraints { get; set; }
        public DbSet<SiteManpower> SiteManpowers { get; set; }
        public DbSet<SiteMaterial> SiteMaterials { get; set; }
        public DbSet<SiteVisitor> SiteVisitors { get; set; }
        public DbSet<WorkProgressActivityDescription> WorkProgressActivityDescriptions { get; set; }
        public DbSet<WorkProgressActivityType> WorkProgressActivityTypes { get; set; }
        public DbSet<ManpowerType> ManpowerTypes { get; set; }
        public DbSet<SiteEquipment> SiteEquipment { get; set; }
        public DbSet<ProjectConstructionLocation> ProjectConstructionLocations { get; set; }
        public DbSet<ProjectGridLocation> ProjectGridLocations { get; set; }
        public DbSet<ProjectLocationActivityDetails> ProjectLocationActivityDetails { get; set; }
        public DbSet<ActivityWiseItemDetial> ActivityWiseItemDetials { get; set; }
        public DbSet<ActivityWiseDailyProgressDetail> ActivityWiseDailyProgressDetails { get; set; }
        public DbSet<ProjectWiseEquipment> ProjectWiseEquipment { get; set; }
        #endregion

        #region SCM POLICE DEMO
        public DbSet<StoreDepartmentType> StoreDepartmentTypes { get; set; }
        public DbSet<FeatureItem> FeatureItems { get; set; }
        #endregion

        #region CRM Master Data

        public DbSet<ActivityCategory> ActivityCategories { get; set; }
        public DbSet<ActivityNature> ActivityNatures { get; set; }
        public DbSet<ActivitySession> ActivitySessions { get; set; }
        public DbSet<CRM.Data.Entity.MasterData.ActivityStatus> ActivityStatuses { get; set; }
        public DbSet<ActivityType> ActivityTypes { get; set; }
        public DbSet<AgreementCategory> AgreementCategories { get; set; }
        public DbSet<Industry> Industries { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<CommunicationType> CommunicationTypes { get; set; }
        public DbSet<OwnerShipType> OwnerShipTypes { get; set; }
        public DbSet<RatingCategory> RatingCategories { get; set; }
        public DbSet<RatingYear> RatingYears { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<Source> Sources { get; set; }

        public DbSet<AgreementStatus> AgreementStatuses { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankBranch> BankBranches { get; set; }
        public DbSet<BankBranchDetails> BankBranchDetails { get; set; }
        public DbSet<CRMDepartment> CRMDepartments { get; set; }
        public DbSet<CRMDesignation> CRMDesignations { get; set; }
        public DbSet<TaxCategory> TaxCategories { get; set; }
        public DbSet<VatCategory> VatCategories { get; set; }
        public DbSet<FIType> FITypes { get; set; }
        public DbQuery<ActivityMasteIdViewModel> activityMasteIdViewModels { get; set; }
        public DbQuery<ActivityReportViewModel> activityReportViewModels { get; set; }
        public DbSet<LeadProgressStatus> leadProgressStatuses { get; set; }
        #endregion

        #region CRM Lead

        public DbSet<ActivityTeam> ActivityTeams { get; set; }
        public DbSet<ActivityDetails> ActivityDetails { get; set; }
        public DbSet<ActivityMaster> ActivityMasters { get; set; }
        public DbSet<ActivityStatusProgress> ActivityStatusProgresses { get; set; }
        public DbSet<CRM.Data.Entity.Lead.Address> Address { get; set; }
        public DbSet<Contacts> Contact { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<Leads> Leads { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<LeadsHistory> LeadsHistories { get; set; }
        public DbQuery<ActivityLogShowViewModel> activityLogShowViewModels { get; set; }
        public DbQuery<ChangeOwnerLogShowViewModel> changeOwnerLogShowViewModels { get; set; }
        public DbSet<AgreementType> AgreementTypes { get; set; }
        public DbSet<Agreement> Agreements { get; set; }
        public DbSet<RelProductAgreement> RelProductAgreements { get; set; }

        public DbSet<AgreementDetails> AgreementDetails { get; set; }
        public DbSet<AgreementDetailsHistory> AgreementDetailsHistories { get; set; }
        public DbSet<ApprovedforCro> ApprovedforCros { get; set; }
        public DbSet<BillCollection> BillCollections { get; set; }
        public DbSet<BillCollectionHistory> BillCollectionHistories { get; set; }
        public DbSet<BillGenerate> BillGenerates { get; set; }
        public DbSet<BillGenerateHistory> BillGenerateHistories { get; set; }
        public DbSet<LeadsBankInfo> LeadsBankInfos { get; set; }
        public DbSet<RatingReview> RatingReviews { get; set; }
        public DbSet<OwnerChangeHistory> OwnerChangeHistories { get; set; }
        public DbSet<VisualData> VisualDatas { get; set; }

        public DbQuery<PhotoViewModel> photoViewModels { get; set; }
        public DbQuery<GetAgreementReportViewModel> getAgreementReportViewModels { get; set; }
        public DbQuery<GetAgreementForCROratingViewModel> getAgreementForCROratingViewModels { get; set; }
        public DbQuery<GetLeadInfoListViewModel> getLeadInfoListViewModels { get; set; }
        public DbQuery<MasterJobViewModel> masterJobViewModels { get; set; }
        public DbQuery<GetLeadInfoVerificationListViewModel> getLeadInfoVerificationListViewModels { get; set; }
        public DbQuery<AllClientViewModel> allClientViewModels { get; set; }
        public DbQuery<AllClientCViewModel> allClientCViewModels { get; set; }
        public DbQuery<GetLeadInfoPrintListViewModel> getLeadInfoPrintListViewModels { get; set; }
        public DbQuery<GetAgreementDetailsbyRatingDateViewModel> getAgreementDetailsbyRatingDateViewModels { get; set; }
        public DbQuery<GetLeadsForConversionListViewModel> getLeadsForConversionListViewModels { get; set; }
        public DbQuery<GetAgreementDetailsbyRatingDateSApprovalViewModel> getAgreementDetailsbyRatingDateSApprovalViewModels { get; set; }
        public DbQuery<GetOwnerChangeViewModel> getOwnerChangeViewModels { get; set; }
        public DbQuery<GetAgreementInfoViewModel> getAgreementInfoViewModels { get; set; }
        public DbQuery<GetOMByassignToReviewerViewModel> getOMByassignToReviewerViewModels { get; set; }
        public DbQuery<GetOMReviewerListViewModel> getOMReviewerListViewModels { get; set; }
        public DbQuery<GetOMByassignToClosedViewModel> getOMByassignToClosedViewModels { get; set; }
        public DbQuery<GetOMByassignToDeclaredViewModel> getOMByassignToDeclaredViewModels { get; set; }
        public DbQuery<GetLeadInfoInCroViewModel> getLeadInfoInCroViewModels { get; set; }
        public DbQuery<LeadReportViewModel> leadReportViewModels { get; set; }
        public DbQuery<GetOMAssignToConverterViewModel> getOMAssignToConverterViewModels { get; set; }
        public DbQuery<GetOMAssignToConverterdViewModel> getOMAssignToConverterdViewModels { get; set; }

        #endregion
        #region CRM Budget
        public DbSet<CompanyBudgets> CompanyBudgets { get; set; }
        public DbSet<TeamBudgets> TeamBudgets { get; set; }
        public DbSet<MemberBudgets> MemberBudgets { get; set; }
        #endregion

        #region CRM Proposal

        public DbSet<Product> Products { get; set; }
        public DbSet<ProposalType> ProposalTypes { get; set; }
        public DbSet<ProposalMaster> ProposalMasters { get; set; }
        public DbSet<ProposalDetail> ProposalDetails { get; set; }
        public DbSet<RelProductProposal> RelProductProposals { get; set; }
        public DbSet<ProposalParticulars> ProposalParticulars { get; set; }
        public DbSet<ProposalStatus> ProposalStatuses { get; set; }
        public DbSet<CRMNoteMaster> CRMNoteMasters { get; set; }

        #endregion

        #region Client

        public DbSet<Clients> Clients { get; set; }
        public DbQuery<GetClientInfoListViewModel> getClientInfoListViewModels { get; set; }
        public DbQuery<ContactsSpViewModel> GetContactsSpViewModels { get; set; }
        public DbQuery<BankContactsSpViewModel> GetBankContactsSpViewModels { get; set; }


        #endregion

        #region CRM SP Query

        public DbQuery<LeadAutoNumber> GetLeadAutoNumberBySp { get; set; }
        public DbQuery<BillGenerateReportViewModel> BillGenerateReportViewModels { get; set; }
        public DbQuery<BillGenerateSPViewModel> BillGenerateSPViewModels { get; set; }
        public DbQuery<BillGenerateListSPViewModel> BillGenerateListSPViewModels { get; set; }
        public DbQuery<BillCollectionSPViewModel> BillCollectionSPViewModels { get; set; }

        #endregion

        #region CRM Cold

        public DbSet<ColdActivityMasters> ColdActivityMasters { get; set; }
        public DbSet<ColdActivityDetails> ColdActivityDetails { get; set; }
        public DbSet<ColdActivityTeams> ColdActivityTeams { get; set; }
        public DbSet<ColdActivityDiscussion> ColdActivityDiscussions { get; set; }

        #endregion

        #region Fixed Asset

        #region Asset Register
        public DbSet<AssetRegistration> AssetRegistration { get; set; }
        public DbSet<PurchaseInfo> PurchaseInfo { get; set; }
        public DbSet<DepriciationInfo> DepriciationInfo { get; set; }
        public DbSet<AssetWarrenty> AssetWarrenties { get; set; }
        public DbSet<AssetOverhauling> AssetOverhaulings { get; set; }

        #endregion

        #region Asset Register Master Data

        public DbSet<RegistrationType> RegistrationType { get; set; }
        public DbSet<AssetYear> AssetYears { get; set; }
        public DbSet<DepriciationPeriod> DepriciationPeriods { get; set; }
        public DbSet<DepriciationRate> DepriciationRates { get; set; }
        public DbSet<ProcurementSource> ProcurementSources { get; set; }

        #endregion

        #region Asset Assign

        public DbSet<AssetAssign> AssetAssigns { get; set; }
        public DbSet<AssetTransfer> AssetTransfers { get; set; }
        public DbSet<AssetReceive> AssetReceives { get; set; }
        public DbSet<AssetReject> AssetRejects { get; set; }
        public DbSet<AssetRetirementType> AssetRetirementTypes { get; set; }
        public DbSet<AssetRetirement> AssetRetirements { get; set; }


        #endregion

        #region Asset Depreciation

        public DbSet<AssetDepreciation> assetDepreciations { get; set; }

        public DbQuery<DepreciationProcessViewModel> DepreciationProcessViewModels { get; set; }
        public DbQuery<AssetRegisterReportViewModel> AssetRegisterReportViewModels { get; set; }
        public DbQuery<AssetRegisterSummaryReportViewModel> AssetRegisterSummaryReportViewModels { get; set; }
        public DbQuery<AssetScheduleReportViewModel> AssetScheduleReportViewModels { get; set; }
        public DbQuery<AssetTransferReportViewModel> AssetTransferReportViewModels { get; set; }
        public DbQuery<AssetRetirementReportViewModel> AssetRetirementReportViewModels { get; set; }
        public DbQuery<AssetDepreciationReportViewModel> AssetDepreciationReportViewModels { get; set; }
        #endregion

        #endregion

        #region REMS
        public DbSet<AssignType> AssignTypes { get; set; }
        public DbSet<ClaimAssign> ClaimAssigns { get; set; }
        public DbSet<ClaimRegister> ClaimRegisters { get; set; }
        public DbSet<ClaimStatusInfo> ClaimStatusInfos { get; set; }
        public DbSet<RepairTransactionLog> RepairTransactionLogs { get; set; }
        public DbSet<ClaimBillInformation> ClaimBillInformation { get; set; }
        public DbSet<ClaimAttachment> ClaimAttachments { get; set; }
        #endregion

        #region RECRUITMENT(RCRT)
        public DbSet<JobRequisition> JobRequisitions { get; set; }
        public DbSet<JobPost> JobPosts { get; set; }
        public DbSet<CandidateInfo> CandidateInfos { get; set; }
        public DbSet<CandidatePhoto> CandidatePhotos { get; set; }
        public DbSet<RCRTAddress> RCRTAddresses { get; set; }
        public DbSet<RCRTEducation> RCRTEducations { get; set; }
        public DbSet<InterviewCalling> InterviewCallings { get; set; }
        public DbSet<ResultMaster> ResultMasters { get; set; }
        public DbSet<ResultDetails> ResultDetails { get; set; }

        public DbSet<RCRTTrainingLog> RCRTTrainingLogs { get; set; }
        public DbSet<RCRTOtherQualification> RCRTOtherQualifications { get; set; }
        public DbSet<RCRTProfessionalQualification> RCRTProfessionalQualifications { get; set; }
        public DbSet<RcrtEmpMembership> RcrtEmpMemberships { get; set; }
        public DbSet<RcrtReference> RcrtReferences { get; set; }
        public DbSet<RcrtPreviousEmployment> RcrtPreviousEmployments { get; set; }
        public DbSet<RcrtPublication> RcrtPublications { get; set; }

        //Exit Interview

        public DbSet<ExitInterviewMaster> exitInterviewMasters { get; set; }
        public DbSet<ExitInterviewComment> exitInterviewComments { get; set; }
        public DbSet<ExitInterviewFeelAndFollowings> exitInterviewFeelAndFollowings { get; set; }
        public DbSet<ExitInterviewPayNBenefits> exitInterviewPayNBenefits { get; set; }
        public DbSet<ExitInterviewReasonOfLeaving> exitInterviewReasonOfLeavings { get; set; }
        public DbSet<ExitInterviewSuggestion> exitInterviewSuggestions { get; set; }

        #endregion

        #region VEMS Master Data
        public DbSet<RequiredDocuments> requiredDocuments { get; set; }
        public DbSet<CodeOfContact> codeOfContacts { get; set; }
        public DbSet<NDAFile> nDAFiles { get; set; }
        #endregion

        #region VEMS Registration
        public DbSet<RegistrationForm> registrationForms { get; set; }
        public DbSet<AuthorizedPerson> authorizedPerson { get; set; }
        public DbSet<BankInformation> bankInformation { get; set; }
        public DbSet<CompanyInformation> companyInformation { get; set; }
        public DbSet<TopOfficial> topOfficials { get; set; }
        public DbSet<VendorAddress> vendorAddresses { get; set; }
        public DbSet<VendorAttachment> vendorAttachments { get; set; }
        public DbSet<InterestedArea> interestedAreas { get; set; }
        #endregion

        #region HRPMS
        #region Master Data

        //Main Entity
        public DbSet<HRPMS.Data.Entity.Master.ActivityStatus> activityStatuses { get; set; }
        public DbSet<Award> awards { get; set; }
        public DbSet<BookCategory> bookCategories { get; set; }
        public DbSet<Cadre> cadres { get; set; }
        public DbSet<HRPMS.Data.Entity.Master.Department> departments { get; set; }
        public DbSet<HRPMS.Data.Entity.Master.Designation> designations { get; set; }
        //public DbSet<Country> countries { get; set; }
        //public DbSet<Division> divisions { get; set; }
        //public DbSet<District> districts { get; set; }
        public DbSet<EmployeeType> employeeTypes { get; set; }
        public DbSet<Language> languages { get; set; }
        public DbSet<Membership> memberships { get; set; }
        public DbSet<MunicipilityLocation> municipilityLocations { get; set; }
        public DbSet<Occupation> occupations { get; set; }
        public DbSet<Relation> relations { get; set; }
        public DbSet<Religion> religions { get; set; }
        //public DbSet<SalaryGrade> salaryGrades { get; set; }
        public DbSet<ServiceStatus> serviceStatuses { get; set; }
        public DbSet<Vehicle> vehicles { get; set; }
        //public DbSet<Thana> thanas { get; set; }
        public DbSet<TrainingCategory> trainingCategories { get; set; }
        public DbSet<TrainingInstitute> trainingInstitutes { get; set; }
        public DbSet<TravelPurpose> travelPurposes { get; set; }
        public DbQuery<PeerSearchViewModel> peerSearchViewModels { get; set; }
        public DbSet<HrProgram> hrPrograms { get; set; }
        public DbSet<Result> results { get; set; }
        public DbSet<HRPMS.Data.Entity.Master.Organization> organizations { get; set; }
        public DbSet<LevelofEducation> levelofEducations { get; set; }
        public DbSet<Subject> subjects { get; set; }
        public DbSet<Degree> degrees { get; set; }
        public DbSet<RelDegreeSubject> relDegreeSubjects { get; set; }
        public DbSet<Holiday> holidays { get; set; }
        public DbSet<HrUnit> hrUnits { get; set; }
        public DbSet<CourseTitle> courseTitles { get; set; }
        public DbSet<Year> years { get; set; }

        //new add in 24-10-2019
        public DbSet<QualificationHead> qualificationHeads { get; set; }
        public DbSet<OtherQualificationHead> otherQualificationHeads { get; set; }
        public DbSet<NomineeFund> nomineeFunds { get; set; }
        public DbSet<HRPMSActivityType> hRPMSActivityTypes { get; set; }
        public DbSet<ActivityName> activityNames { get; set; }
        public DbSet<HRPMSOrganizationType> hRPMSOrganizationTypes { get; set; }
        public DbSet<SpecialBranchUnit> SpecialBranchUnits { get; set; }
        public DbSet<FunctionInfo> FunctionInfos { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<EmployeeContractInfo> EmployeeContractInfos { get; set; }

        //Travel Module 
        public DbSet<HRProject> hRProjects { get; set; }
        public DbSet<HRActivity> hRActivities { get; set; }
        public DbSet<HRDoner> hRDoners { get; set; }
        public DbSet<TravelVehicleType> travelVehicleTypes { get; set; }
        public DbSet<BelongingItem> belongingItems { get; set; }
        public DbSet<AtttachmentCategory> atttachmentCategories { get; set; }
        public DbSet<AtttachmentGroup> atttachmentGroups { get; set; }
        public DbSet<HRFacility> hRFacilities { get; set; }
        //--------------------------------------------------------------------------
        //From SP Or SQl
        //Approval process 
        public DbSet<ApprovalType> approvalTypes { get; set; }
        public DbSet<ApprovalMaster> approvalMasters { get; set; }
        public DbSet<ApprovalDetail> approvalDetails { get; set; }

        // EMPLOYEE EXIT INTERVIEW 

        public DbSet<ReasonForLeaving> reasonForLeavings { get; set; }
        public DbSet<CommentORSuggestion> commentORSuggestions { get; set; }
        public DbSet<PayAndBenefits> payAndBenefits { get; set; }
        public DbSet<FeelAboutTheFollowing> feelAboutTheFollowings { get; set; }
        public DbSet<InterviewComment> interviewComments { get; set; }
        public DbSet<MemberType> memberTypes { get; set; }
        #endregion

        #region Organogram
        //Main Entit
        public DbSet<HRPMS.Data.Entity.Organogram.OrganizationType> organizationTypes { get; set; }
        public DbSet<OrganoOrganization> organoOrganizations { get; set; }
        public DbSet<Post> posts { get; set; }
        public DbSet<PostingType> postingTypes { get; set; }
        public DbSet<EmploymentType> employmentTypes { get; set; }
        public DbSet<PostDetails> postDetails { get; set; }

        //From SP Or SQl

        #endregion

        #region Document Management

        public DbSet<FieldType> fieldTypes { get; set; }
        public DbSet<FieldSettings> fieldSettings { get; set; }
        public DbSet<DocumentCategory> documentCategories { get; set; }
        public DbSet<DocumentMaster> documentMasters { get; set; }
        public DbSet<DocumentDetail> documentDetails { get; set; }
        public DbSet<DocumentStatusLog> documentStatusLogs { get; set; }
        public DbSet<AssignDocumentMaster> assignDocumentMasters { get; set; }
        public DbSet<AssignDocument> assignDocuments { get; set; }

        public DbQuery<DocumentAccessViewModel> documentAccessViewModels { get; set; }
        public DbQuery<TotalDocumentByLeadViewModel> totalDocumentByLeadViewModels { get; set; }
        public DbQuery<DocumentMasterAccessViewModel> documentMasterAccessViewModels { get; set; }
        #endregion

        #region Employee Data
        //Main Entity

        public DbSet<EmployeeInfo> employeeInfos { get; set; }
        public DbSet<HRPMS.Data.Entity.Employee.Address> addresses { get; set; }
        public DbSet<EmployeeAward> employeeAwards { get; set; }
        public DbSet<Children> childrens { get; set; }
        public DbSet<DrivingLicense> drivingLicenses { get; set; }
        public DbSet<EducationalQualification> educationalQualifications { get; set; }
        public DbSet<EmployeeLanguage> employeeLanguages { get; set; }
        public DbSet<EmployeeMembership> employeeMemberships { get; set; }
        public DbSet<PassportDetails> passportDetails { get; set; }
        public DbSet<Photograph> photographs { get; set; }
        public DbSet<Publication> publications { get; set; }
        public DbSet<Spouse> spouses { get; set; }
        public DbSet<Promotion> promotions { get; set; }
        public DbSet<TraveInfo> traveInfos { get; set; }
        public DbSet<AcrInfo> acrInfos { get; set; }
        public DbSet<TransferLog> transferLogs { get; set; }
        public DbSet<TraningLog> traningLogs { get; set; }
        public DbSet<PromotionLog> promotionLogs { get; set; }
        public DbSet<BankInfo> bankInfos { get; set; }
        public DbSet<DisciplinaryLog> disciplinaryLogs { get; set; }
        public DbSet<LeaveLog> leaveLogs { get; set; }
        public DbSet<EmployeeSignature> employeeSignatures { get; set; }

        //new add in 24-10-2019
        public DbSet<FreedomFighter> freedomFighters { get; set; }
        public DbSet<QualificationAndValidation> qualificationAndValidations { get; set; }
        public DbSet<EmergencyContact> emergencyContacts { get; set; }
        public DbSet<Nominee> nominees { get; set; }
        public DbSet<OfficeAssign> officeAssigns { get; set; }
        public DbSet<NomineeDetail> nomineeDetails { get; set; }
        public DbSet<Reference> references { get; set; }
        public DbSet<Supervisor> supervisors { get; set; }
        public DbSet<Belongings> Belongings { get; set; }
        public DbSet<OtherActivity> otherActivities { get; set; }
        public DbSet<PriviousEmployment> priviousEmployments { get; set; }
        public DbSet<ProfessionalQualifications> professionalQualifications { get; set; }
        public DbSet<OtherQualification> otherQualifications { get; set; }
        public DbSet<EmployeeProjectActivity> employeeProjectActivities { get; set; }
        public DbSet<FinanceCode> financeCodes { get; set; }
        public DbSet<PIMSVisitor> pIMSVisitors { get; set; }
        public DbSet<HRPMSAttachment> hRPMSAttachments { get; set; }
        public DbSet<HRManualAttachment> hRManualAttachments { get; set; }
        public DbSet<EmployeeInsurance> employeeInsurances { get; set; }
        public DbSet<EmployeeOtherInfo> employeeOtherInfos { get; set; }
        public DbSet<EmployeeCostCentre> employeeCostCentres { get; set; }
        public DbSet<EmployeeGrade> employeeGrades { get; set; }
        //5-11-2019
        public DbSet<PNS> pNs { get; set; }
        public DbSet<RlnSBUPNS> rlnSBUPNs { get; set; }
        public DbSet<EmployeeProjectAssign> employeeProjectAssigns { get; set; }

        //--------------------------------------------------------------------------
        //From SP Or SQl
        public DbQuery<EmployeeWithDesignationVM> employeeWithDesignations { get; set; }

        #endregion

        #region Member Data
        //Main Entity

        public DbSet<MemberInfo> memberInfos { get; set; }
        public DbSet<MemberAddress> memberAddresses { get; set; }
        public DbSet<MemberAward> memberAwards { get; set; }
        public DbSet<MemberChildren> memberChildrens { get; set; }
        public DbSet<MemberDrivingLicense> memberDrivingLicenses { get; set; }
        public DbSet<MemberEducationalQualification> memberEducationalQualifications { get; set; }
        public DbSet<MemberLanguage> memberLanguages { get; set; }
        public DbSet<OtherMembership> otherMemberships { get; set; }
        public DbSet<MemberPassportDetails> memberPassportDetails { get; set; }
        public DbSet<MemberPhotograph> memberPhotographs { get; set; }
        public DbSet<MemberPublication> memberPublications { get; set; }
        public DbSet<MemberSpouse> memberSpouses { get; set; }
        public DbSet<MemberTransferLog> memberTransferLogs { get; set; }
        public DbSet<MemberTraningLog> memberTraningLogs { get; set; }
        public DbSet<SpecialSkill> specialSkills { get; set; }

        public DbSet<MemberOtherInfo> memberOtherInfos { get; set; }

        //--------------------------------------------------------------------------
        //From SP Or SQl
        public DbQuery<MemberWithDesignationVM> memberWithDesignations { get; set; }
        public DbQuery<MemberInfoReportViewModel> memberInfoReportViewModels { get; set; }

        #endregion

        #region Hall Rent
        public DbSet<HallInfo> hallInfos { get; set; }
        public DbSet<HallRentalShift> hallRentalShifts { get; set; }
        public DbSet<HallRentalApplicationInfo> hallRentalApplicationInfos { get; set; }
        public DbSet<HallRentalPayment> hallRentalPayments { get; set; }
        #endregion

        #region Event Data
        public DbSet<EventInformation> eventInformations { get; set; }
        public DbSet<EventRegister> eventRegisters { get; set; }
        public DbSet<EventPerticipantHead> eventPerticipantHeads { get; set; }
        public DbSet<EventParticipantType> eventParticipantTypes { get; set; }
        public DbSet<ParticipantHead> participantHeads { get; set; }
        public DbSet<EventCategory> eventCategories { get; set; }
        #endregion

        #region Notice
        public DbSet<NoticeInfo> noticeInfos { get; set; }
        public DbSet<NoticeAuthor> noticeAuthors { get; set; }
        public DbSet<RlnNoticeAuthor> rlnNoticeAuthors { get; set; }
        public DbSet<NoticeType> noticeTypes { get; set; }
        #endregion

        #region News
        public DbSet<NewsInfo> newsInfos { get; set; }
        #endregion

        #region Gallery
        public DbSet<GalleryTitle> galleryTitles { get; set; }
        public DbSet<GalleryContent> galleryContents { get; set; }
        #endregion

        #region Finance
        public DbSet<PaymentLog> paymentLogs { get; set; }
        public DbSet<Balance> balances { get; set; }
        public DbSet<TrMaster> trMasters { get; set; }
        public DbSet<Invoice> invoices { get; set; }
        public DbSet<Collection> collections { get; set; }
        public DbSet<PaymentHead> paymentHeads { get; set; }
        #endregion

        #region Forum
        public DbSet<Topic> topics { get; set; }
        public DbSet<MemberComment> memberComments { get; set; }
        public DbSet<SponsorShip> sponsorShips { get; set; }
        #endregion

        #region Bulk
        public DbSet<MemberGroup> memberGroups { get; set; }
        public DbSet<RlnMemberGroup> rlnMemberGroups { get; set; }
        public DbSet<BulkHistory> bulkHistories { get; set; }
        #endregion

        #region Wages
        public DbSet<WagesEmployeeInfo> wagesEmployeeInfos { get; set; }
        public DbSet<WagesAddress> wagesAddresses { get; set; }
        public DbSet<WagesPhotograph> wagesPhotographs { get; set; }
        public DbSet<WagesBankInfo> wagesBankInfos { get; set; }
        public DbSet<WagesEmergencyContact> wagesEmergencyContacts { get; set; }
        public DbSet<WagesReference> wagesReferences { get; set; }
        public DbSet<WagesPunchCardInfo> wagesPunchCardInfos { get; set; }
        public DbSet<WagesLeftDetails> wagesLeftDetails { get; set; }
        public DbSet<WagesPriviousEmployment> wagesPriviousEmployments { get; set; }
        public DbSet<WagesLeaveRegister> wagesLeaveRegisters { get; set; }
        public DbSet<WagesProcessEmpSalaryStructure> wagesProcessEmpSalaryStructures { get; set; }
        public DbSet<WagesProcessEmpSalaryMaster> wagesProcessEmpSalaryMasters { get; set; }


        public DbSet<OTSlotType> oTSlotTypes { get; set; }
        public DbSet<OTSetupMaster> oTSetupMasters { get; set; }
        public DbSet<OTSetupDetail> oTSetupDetails { get; set; }
        public DbSet<WagesSalaryStructure> wagesSalaryStructures { get; set; }
        public DbSet<WagesEmpAttendance> wagesEmpAttendances { get; set; }
        public DbSet<OTProcessData> oTProcessDatas { get; set; }
        
        #endregion

        #region Attendence
        public DbSet<MovementTracking> MovementTracking { get; set; }
        public DbSet<AttendenceApi> AttendenceApi { get; set; }

        //Main Entity

        //public DbSet<Holiday> holidays { get; set; }
        public DbSet<ShiftGroupMaster> shiftGroupMasters { get; set; }
        public DbSet<ShiftGroupDetail> shiftGroupDetails { get; set; }
        public DbSet<AttendenceApi> attendenceApis { get; set; }
        public DbSet<EmpAttendenceTemp> empAttendenceTemps { get; set; }
        public DbSet<TempDay> tempDays { get; set; }
        public DbSet<EmployeePunchCardInfo> employeePunchCardInfos { get; set; }
        public DbSet<EmpAttendance> empAttendances { get; set; }
        public DbSet<AttendenceUpload> attendenceUploads { get; set; }
        public DbSet<AttendanceUpdateApply> attendanceUpdateApplies { get; set; }
        public DbSet<AttendanceRoute> attendanceRoutes { get; set; }
        //--------------------------------------------------------------------------
        //From SP Or SQl

        //public DbQuery<dynamic> empAttendancesSp { get; set; }
        public DbQuery<EmpAttendanceViewModel> ProcessAttendanceSP { get; set; }
        public DbQuery<JobCardReportViewModel> JobCardReportViewModels { get; set; }
        public DbQuery<IndividualAttendanceReportViewModel> individualAttendanceReportViewModels { get; set; }
        public DbQuery<IndividualAttendanceSummaryReportViewModel> individualAttendanceSummaryReportViewModels { get; set; }
        public DbQuery<AttendanceViewModelApi> AttendanceViewModelApis { get; set; }

        #endregion

        #region Leave
        //Mian Entity
        public DbSet<LeaveRegister> leaveRegisters { get; set; }
        public DbSet<LeaveStatusLog> leaveStatusLogs { get; set; }
        public DbSet<LeaveType> leaveTypes { get; set; }
        public DbSet<LeavePolicy> leavePolicies { get; set; }
        public DbSet<LeaveOpeningBalance> leaveOpeningBalances { get; set; }
        public DbSet<LeaveRoute> leaveRoutes { get; set; }
        public DbSet<LeaveDay> leaveDays { get; set; }

        public DbQuery<LeaveRegisterNotMapped> leaveRegisterNotMappeds { get; set; }
        public DbQuery<DayLeaveNotMapped> dayLeaveNotMappeds { get; set; }
        public DbQuery<LeaveBalanceViewModel> leaveBalanceViewModels { get; set; }
        public DbQuery<LeaveIndividualViewModel> leaveIndividualViewModels { get; set; }
        //--------------------------------------------------------------------------
        //From SP Or SQl

        #endregion

        #region Gratuity
        public DbSet<GratuityPolicyMaster> gratuityPolicyMasters { get; set; }
        public DbSet<GratuityPolicyDetail> gratuityPolicyDetails { get; set; }
        #endregion

        #region Requirtment
        //Main Entity
        public DbSet<JobCircular> jobCirculars { get; set; }
        public DbSet<ApplicationForm> applicationForms { get; set; }
        public DbSet<ApplicationEdu> applicationEdus { get; set; }
        public DbSet<ApplicationExp> applicationExps { get; set; }
        public DbSet<ApplicationQuota> applicationQuotas { get; set; }
        public DbSet<ApplicationExam> applicationExams { get; set; }
        public DbSet<CallForExam> callForExams { get; set; }
        public DbSet<ApplicantVerification> applicantVerifications { get; set; }

        //--------------------------------------------------------------------------
        //From SP Or SQl

        #endregion

        #region Assignment
        //Main Entity
        public DbSet<Assignment> assignments { get; set; }

        //--------------------------------------------------------------------------
        //From SP Or SQl

        //public DbQuery<AssignmentViewModel> assignmentViewModels { get; set; }

        #endregion

        #region ACR
        public DbSet<ACRAuthority> aCRAuthorities { get; set; }

        public DbSet<WorkHistory> workHistories { get; set; }
        public DbSet<PhysicalProperty> physicalProperties { get; set; }
        public DbSet<Position> positions { get; set; }

        public DbSet<ACRActionType> aCRActionTypes { get; set; }
        public DbSet<ACRSchedule> aCRSchedules { get; set; }
        public DbSet<ACRProcess> aCRProcesses { get; set; }

        public DbSet<AcrInitiate> acrInitiates { get; set; }
        public DbSet<AcrHealthInfo> acrHealthInfos { get; set; }
        public DbSet<AcrPersonalWorkDescription> acrPersonalWorkDescriptions { get; set; }
        public DbSet<AcrEvaluationMaster> acrEvaluationMasters { get; set; }
        public DbSet<AcrEvaluationDetail> acrEvaluationDetails { get; set; }
        public DbSet<AcrEvaluationValue> acrEvaluationValues { get; set; }
        public DbSet<AcrRoute> acrRoutes { get; set; }
        public DbSet<AcrRouteEvaluation> acrRouteEvaluations { get; set; }

        //From SP Or SQl
        public DbQuery<CountNoofChildrenViewModel> countNoofChildrenViewModels { get; set; }
        public DbQuery<AcrTotalMarksViewModel> acrTotalMarksViewModels { get; set; }

        public DbQuery<HrReportViewModel> hrReportViewModels { get; set; }
        public DbQuery<HrEducationReportViewModel> hrEducationReportViewModels { get; set; }
        public DbQuery<HrTrainingReportViewModel> hrTrainingReportViewModels { get; set; }
        public DbQuery<HrBelongingReportViewModel> hrBelongingReportViewModels { get; set; }
        public DbQuery<HrSummaryReportViewModel> hrSummaryReportViewModels { get; set; }
        #endregion

        #region Training
        //Main Entity
        public DbSet<ModuleTrainingCategory> moduleTrainingCategories { get; set; }
        public DbSet<EvaluationFectors> evaluationFectors { get; set; }
        public DbSet<Trainer> trainers { get; set; }
        public DbSet<TrainingOffer> trainingOffers { get; set; }
        public DbSet<RlnTrainingTrainer> rlnTrainingTrainers { get; set; }
        public DbSet<Plan> plans { get; set; }
        public DbSet<PlanDetails> planDetails { get; set; }
        public DbSet<SessionAttendance> sessionAttendances { get; set; }
        public DbSet<TrainingInfo> trainingInfos { get; set; }
        public DbSet<TrainingInfoDetails> trainingInfoDetails { get; set; }
        public DbSet<TrainingMiscallenous> trainingMiscallenous { get; set; }
        public DbSet<TrainingDeliverables> trainingDeliverables { get; set; }
        public DbSet<TrainingAllocation> trainingAllocations { get; set; }
        public DbSet<Evaluation> evaluations { get; set; }
        public DbSet<TrainingExam> trainingExams { get; set; }
        public DbSet<ExamResult> examResults { get; set; }

        //From SP Or SQl
        #endregion

        #region Training New
        public DbSet<TrainingInfoNew> trainingInfoNews { get; set; }
        public DbSet<EnrolledTrainee> enrolledTrainees { get; set; }
        public DbSet<ResourcePerson> resourcePersons { get; set; }
        public DbSet<TrainingResourcePerson> trainingResourcePersons { get; set; }
        #endregion

        #region Library Data
        //Main Entity
        public DbSet<BookEntry> bookEntries { get; set; }
        public DbSet<BorrowerInfo> borrowerInfos { get; set; }
        //From SP Or SQl

        #endregion

        #region VehicleInfo Data
        //Main Entity
        public DbSet<VehicleEntry> vehicleEntries { get; set; }
        public DbSet<MajorOverholding> majorOverholdings { get; set; }
        public DbSet<DriverAssignment> driverAssignments { get; set; }
        //From SP Or SQl

        #endregion

        #region Charge Punishment
        public DbSet<Charge> charges { get; set; }
        public DbSet<Punishment> punishments { get; set; }
        //public DbQuery<ChargeViewModel> GetCharges { get; set; }
        #endregion

        #region Reward
        public DbSet<Reward> rewards { get; set; }
        #endregion

        #region Travel
        public DbSet<TravelMaster> travelMasters { get; set; }
        public DbSet<TravellerInfo> travellerInfos { get; set; }
        public DbSet<TravellingInfo> travellingInfos { get; set; }
        public DbSet<TravelRoute> travelRoutes { get; set; }
        public DbSet<TravelStatusLog> travelStatusLogs { get; set; }
        #endregion

        #region Award&Publication
        //Main Entity

        public DbSet<PublicationEntry> publicationEntries { get; set; }
        public DbSet<PublicationAttachment> publicationAttachments { get; set; }
        public DbSet<AwardEntry> awardEntries { get; set; }
        public DbSet<AwardAttachment> awardAttachments { get; set; }
        //From SP Or SQl

        #endregion

        #region Discipline&Investigation
        //Main Entity
        public DbSet<DisciplinaryAction> disciplinaryActions { get; set; }
        public DbSet<DisiciplinaryAttachment> disiciplinaryAttachments { get; set; }
        public DbSet<NaturalPunishment> naturalPunishments { get; set; }
        public DbSet<Offense> offenses { get; set; }
        //From SP Or SQl

        #endregion

        #region Retirements&Termination
        //Main Entity
        public DbSet<PRLApplication> pRLApplications { get; set; }
        public DbSet<PRLAttachment> pRLAttachments { get; set; }
        public DbSet<ResignInformation> resignInformation { get; set; }
        //From SP Or SQl

        #endregion

        #region Promotion & Transfer
        //Main Entity
        public DbSet<PromotionEntry> promotionEntries { get; set; }
        public DbSet<PromotionAttachment> promotionAttachments { get; set; }
        public DbSet<TransferEntry> transferEntries { get; set; }
        public DbSet<TransferAttachment> transferAttachments { get; set; }
        //From SP Or SQl

        #endregion
        #endregion

        #region Payroll
        public DbSet<SalaryYear> salaryYears { get; set; }
        public DbSet<EmployeesTax> employeesTaxes { get; set; }
        public DbSet<SalaryGrade> salaryGrades { get; set; }
        public DbSet<TaxYear> taxYears { get; set; }
        public DbSet<SalaryHead> salaryHeads { get; set; }
        public DbSet<BonusType> bonusTypes { get; set; }
        public DbSet<SalaryCalulationType> salaryCalulationTypes { get; set; }
        public DbSet<SalaryActivityPercent> salaryActivityPercents { get; set; }
        public DbSet<SalaryType> salaryTypes { get; set; }
        public DbSet<SalaryPeriod> salaryPeriods { get; set; }
        public DbSet<SalarySlab> salarySlabs { get; set; }
        public DbSet<SalaryGradePercent> salaryGradePercents { get; set; }
        public DbSet<EmployeesSalaryStructure> employeesSalaryStructures { get; set; }
        public DbSet<ProcessEmpSalaryStructure> ProcessEmpSalaryStructures { get; set; }
        public DbSet<ProcessEmpSalaryMaster> ProcessEmpSalaryMasters { get; set; }
        public DbSet<ProcessSalaryRemarks> ProcessSalaryRemarks { get; set; }
        public DbSet<GratiutyProcessData> gratiutyProcessDatas { get; set; }
        public DbSet<SalaryProcessLog> SalaryProcessLogs { get; set; }
        public DbSet<SalaryStructureHistory> salaryStructureHistories { get; set; }
        public DbSet<LeaveWithoutPay> LeaveWithoutPays { get; set; }
        public DbSet<AdvanceAdjustment> AdvanceAdjustments { get; set; }
        public DbSet<EmployeeArrear> EmployeeArrears { get; set; }
        public DbSet<LfaInfo> LfaInfos { get; set; }
        public DbSet<AdvancePayment> AdvancePayments { get; set; }
        public DbSet<EmpAttendanceSummary> EmpAttendanceSummaries { get; set; }
        public DbSet<SalaryStatusLog> SalaryStatusLogs { get; set; }
        public DbSet<MonthlyAllowance> MonthlyAllowances { get; set; }
        public DbSet<LoanPolicy> LoanPolicies { get; set; }
        public DbSet<LoanScheduleMaster> loanScheduleMasters { get; set; }
        public DbSet<LoanScheduleDetail> loanScheduleDetails { get; set; }
        public DbSet<AdvanceAdjustmentDetail> advanceAdjustmentDetails { get; set; }
        public DbSet<LoanRoute> LoanRoutes { get; set; }
        public DbSet<SlabType> SlabTypes { get; set; }
        public DbSet<RebateSlabType> RebateSlabTypes { get; set; }
        public DbSet<SlabIncomeTax> SlabIncomeTaxes { get; set; }
        public DbSet<SlabIncomeTaxAssign> SlabIncomeTaxAssigns { get; set; }
        public DbSet<SlabRebate> SlabRebates { get; set; }
        public DbSet<IncomeTaxSetup> IncomeTaxSetups { get; set; }
        public DbSet<InvestmentRebateSettings> InvestmentRebateSettings { get; set; }
        public DbSet<TaxChallan> TaxChallans { get; set; }
        public DbSet<AdditionalTaxInfo> AdditionalTaxInfos { get; set; }
        public DbQuery<TaxProcessViewModel> taxProcessViewModels { get; set; }
        public DbQuery<ReconciliationReportViewModel> reconciliationReportViewModels { get; set; }
        public DbQuery<PayslipReportViewModel> payslipReportViewModels { get; set; }

        public DbQuery<MonthlySalaryReportViewModel> monthlySalaryReportViewModels { get; set; }
        public DbQuery<BankStatementReportViewModel> bankStatementReportViewModels { get; set; }
        public DbQuery<FinalSettlementDataViewModel> finalSettlementDataViewModels { get; set; }
        public DbQuery<FsSalaryStructureViewModel> fsSalaryStructureViewModels { get; set; }
        public DbQuery<GratuityReportViewModel> gratuityReportViewModels { get; set; }
        public DbQuery<SalaryProcessDataViewModel> salaryProcessDataViewModels { get; set; }
        public DbQuery<StructureHistoryReportViewModel> structureHistoryReportViewModels { get; set; }
        public DbQuery<LoanScheduleReportViewModel> loanScheduleReportViewModels { get; set; }


        public DbSet<FinalSettlement> FinalSettlements { get; set; }
        public DbSet<ReportFormat> ReportFormats { get; set; }
        public DbSet<CodeManagement> CodeManagements { get; set; }
        public DbSet<FixedTaxEmployees> FixedTaxEmployees { get; set; }
        public DbSet<BonousCategory> bonousCategories { get; set; }
        public DbSet<BonousSubCategory> bonousSubCategories { get; set; }
        public DbSet<BonousStructure> bonousStructures { get; set; }
        public DbSet<WalletType> walletTypes { get; set; }
        public DbSet<EmployeesCashSetup> employeesCashSetups { get; set; }
        public DbSet<EmployeesBonusStructure> employeesBonusStructures { get; set; }

        public DbQuery<EmpTaxDetailsViewModel> empTaxDetailsViewModels { get; set; }
        public DbQuery<EmpTaxSlabViewModel> empTaxSlabViewModels { get; set; }
        public DbQuery<EmpRebatableTaxViewModel> empRebatableTaxViewModels { get; set; }
        public DbQuery<EmpTaxDeductFinalViewModel> empTaxDeductFinalViewModels { get; set; }

        #endregion

        #region PF
        public DbSet<FdrInvestment> FdrInvestments { get; set; }
        public DbSet<PFLoan> PFLoans { get; set; }
        public DbSet<PFLoanSchedule> PFLoanSchedules { get; set; }
        public DbSet<PFLoanPayment> PFLoanPayments { get; set; }
        #endregion

        #region Accounting

        #region Master Data
        public DbSet<CostCentre> CostCentres { get; set; }
        public DbSet<FundSource> FundSources { get; set; }
        public DbSet<VoucherType> VoucherTypes { get; set; }

        public DbSet<SignatureType> signatureTypes { get; set; }
        public DbSet<Signature> signatures { get; set; }
 public DbQuery<AutoNumberModel> AutoNumberModels { get; set; }
       
        public DbQuery<SLNoViewModel> SLNoViewModels { get; set; }
        public DbQuery<AutoProcessNumberViewModel> AutoProcessNumberViewModels { get; set; }
        #endregion

        #region Account Settings
        public DbSet<AccountMode> AccountModes { get; set; }
        public DbSet<GroupNature> GroupNatures { get; set; }
        public DbSet<OpeningBalance> OpeningBalances { get; set; }
        public DbSet<AccountGroup> AccountGroups { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<SubLedger> SubLedgers { get; set; }
        public DbSet<LedgerRelation> LedgerRelations { get; set; }
        public DbSet<BalanceSheetMaster> BalanceSheetMasters { get; set; }
        public DbSet<BalanceSheetDetails> BalanceSheetDetails { get; set; }
        public DbSet<NoteMaster> NoteMasters { get; set; }
        public DbSet<BalanceSheetProcess> BalanceSheetProcesses { get; set; }
        public DbQuery<GetSignatureViewModel> getSignatureViewModels { get; set; }

        public DbSet<ProfitAndLossAccountMaster> profitAndLossAccountMasters { get; set; }
        public DbSet<ProfitAndLossAccountDetails> profitAndLossAccountDetails { get; set; }
        public DbSet<ProfitAndLossAccountProcess> profitAndLossAccountProcesses { get; set; }


        #endregion

        #region Voucher
        public DbSet<VoucherMaster> VoucherMasters { get; set; }
        public DbSet<VoucherDetail> VoucherDetails { get; set; }
        public DbSet<TransectionMode> TransectionModes { get; set; }
        public DbSet<CostCentreAllocation> CostCentreAllocations { get; set; }
        public DbSet<VoucherSetting> VoucherSettings { get; set; }
        public DbSet<VoucherApproveLog> VoucherApproveLogs { get; set; }
        public DbSet<DeleteVoucherMaster> DeleteVoucherMasters { get; set; }
        public DbSet<DeleteVoucherDetail> DeleteVoucherDetails { get; set; }
        public DbSet<DeleteCostCentreAllocation> DeleteCostCentreAllocations { get; set; }
        public DbSet<AutoVoucherName> AutoVoucherNames { get; set; }
        public DbSet<AutoVoucherMaster> AutoVoucherMasters { get; set; }
        public DbSet<AutoVoucherDetail> AutoVoucherDetails { get; set; }


        public DbQuery<GetVoucherListForDeleteViewModel> getVoucherListForDeleteViewModels { get; set; }
        public DbQuery<DeleteVoucherViewModel> deleteVoucherViewModels { get; set; }


        #endregion

        #region NonPo
        public DbSet<DailyBillReceive> DailyBillReceives { get; set; }
        public DbSet<RateType> RateTypes { get; set; }
        public DbSet<VatTaxRate> VatTaxRates { get; set; }
        #endregion

        #region FDR
        public DbSet<BankChargeMaster> bankChargeMasters { get; set; }
        public DbSet<FDRCalFormula> fDRCalFormulas { get; set; }
        public DbSet<FDRInvesmentMaster> fDRInvesmentMasters { get; set; }
        public DbSet<FDRInvestmentDetail> fDRInvestmentDetails { get; set; }
        public DbSet<FDREncashment> fDREncashments { get; set; }


        public DbQuery<FDRAutoNoViewModel> fDRAutoNoViewModels { get; set; }
        public DbQuery<FDRMaturityStatusForRenewalViewModel> fDRMaturityStatusForRenewalViewModels { get; set; }
        public DbQuery<GETFDRMaturityStatusViewModel> gETFDRMaturityStatusViewModels { get; set; }
        public DbQuery<GetFDRInformationForEncashmentViewModel> getFDRInformationForEncashmentViewModels { get; set; }
        public DbQuery<GetListOfFDREncashmentWithFDRViewModel> getListOfFDREncashmentWithFDRViewModels { get; set; }
        public DbQuery<Areas.HRPMSAttendence.Models.AttendanceSummaryViewModel> attendanceSummaryViewModels { get; set; }
        public DbQuery<MealChargeViewModel> mealChargeViewModels { get; set; }

        public DbQuery<FDRScheduleReportViewModel> fDRScheduleReportViewModels { get; set; }
        public DbQuery<FDRReportViewModel> fDRReportViewModels { get; set; }
        public DbQuery<SP_GET_FDRDetailsViewModel> sP_GET_FDRDetailsViewModels { get; set; }
        public DbQuery<FDRInterestScheduleReportViewModel> fDRInterestScheduleReportViewModels { get; set; }
        #endregion

        #region Financial Report

        public DbQuery<ProfitAndLossAccountViewModel> profitAndLossAccountViewModels { get; set; }
        public DbQuery<BalanceSheetViewModel> balanceSheetViewModels { get; set; }
        public DbQuery<TrialBalanceSPViewModel> trialBalanceSPViewModels { get; set; }
        public DbQuery<LedgerBookReportViewModel> ledgerBookReportViewModels { get; set; }
        public DbQuery<BankBookReportViewModel> bankBookReportViewModels { get; set; }
        public DbQuery<CFSDetailViewModel> cFSDetailViews { get; set; }
        public DbQuery<CFSDetailBalanceViewModel> cFSDetailBalanceViewModels { get; set; }
        public DbQuery<BudgetExpenseDetailViewModel> budgetExpenseDetailViewModels { get; set; }
        public DbQuery<BudgetExpenseDetailPViewModel> budgetExpenseDetailPViewModels { get; set; }
        public DbQuery<RVDetailViewModel> rVDetailViewModels { get; set; }
        public DbQuery<RVDetailBalanceViewModel> rVDetailBalanceViewModels { get; set; }

        #endregion

        #endregion

        #region Sales
        public DbSet<OPUSERP.Sales.Data.Entity.Sales.SalesInvoiceMaster> SalesInvoiceMasters { get; set; }
        public DbSet<OPUSERP.Sales.Data.Entity.Sales.SalesInvoiceDetail> SalesInvoiceDetails { get; set; }
        public DbSet<OPUSERP.Sales.Data.Entity.Collection.SalesCollection> SalesCollections { get; set; }
        public DbSet<OPUSERP.Sales.Data.Entity.Collection.SalesCollectionDetail> SalesCollectionDetails { get; set; }
        public DbSet<RepSalesInvoiceMaster> RepSalesInvoiceMasters { get; set; }
        public DbSet<RepSalesInvoiceDetail> RepSalesInvoiceDetails { get; set; }
        public DbSet<RepSalesCollection> RepSalesCollections { get; set; }
        public DbSet<RepSalesCollectionDetail> RepSalesCollectionDetails { get; set; }
        #endregion

        #region Other Sales
        public DbSet<RelSupplierCustomerResourse> RelSupplierCustomerResourses { get; set; }
        public DbSet<RelCustomerArea> RelCustomerAreas { get; set; }
        public DbSet<RelCustomerSalesTeamDeployment> relCustomerSalesTeamDeployments { get; set; }
        public DbSet<OPUSERP.OtherSales.Data.Entity.Sales.ItemPriceFixing> OItemPriceFixings { get; set; }
        public DbSet<BarCodeInformation> BarCodeInformation { get; set; }
        public DbSet<OPUSERP.OtherSales.Data.Entity.Sales.SalesInvoiceMaster> OSalesInvoiceMasters { get; set; }
        public DbSet<TermsAndConditions> TermsAndConditions { get; set; }
        public DbSet<RentInvoiceMaster> RentInvoiceMasters { get; set; }
        public DbSet<RentTermsConditions> RentTermsConditions { get; set; }
        public DbSet<SalesTermsConditions> SalesTermsConditions { get; set; }
        public DbSet<SalesReturnInvoiceMaster> SalesReturnInvoiceMasters { get; set; }
        public DbSet<SalesReturnInvoiceDetail> salesReturnInvoiceDetails { get; set; }
        public DbSet<PosSalesReturnInvoiceMaster> OPosSalesReturnInvoiceMasters { get; set; }
        public DbSet<PosSalesReturnInvoiceDetail> OPosSalesReturnInvoiceDetails { get; set; }
        public DbSet<OPUSERP.OtherSales.Data.Entity.Sales.SalesInvoiceDetail> OSalesInvoiceDetails { get; set; }
        public DbSet<RentInvoiceDetail> RentInvoiceDetails { get; set; }
        public DbSet<OPUSERP.OtherSales.Data.Entity.Sales.SalesCollection> OSalesCollections { get; set; }
        public DbSet<OPUSERP.OtherSales.Data.Entity.Sales.SalesCollectionDetail> OSalesCollectionDetails { get; set; }
        public DbSet<RoleType> RoleTypes { get; set; }
        public DbSet<BillReturnPaymentMaster> BillReturnPaymentMasters { get; set; }
        public DbSet<BillReturnPaymentDetail> BillReturnPaymentDetails { get; set; }
        public DbSet<SalesCollectionBillInfo> salesCollectionBillInfos { get; set; }

        public DbSet<SalesMenuCategory> SalesMenuCategories { get; set; }
        public DbSet<MealType> MealTypes { get; set; }
        public DbSet<SalesMenu> SalesMenus { get; set; }
        public DbSet<MenuMealTypeMapping> MenuMealTypeMappings { get; set; }
        public DbSet<SalesMenuOpeningBalance> SalesMenuOpeningBalances { get; set; }

        public DbQuery<RelSupplierCustomerResourseViewModel> relSupplierCustomerResourseViewModels { get; set; }
        public DbQuery<CustomerInfoViewModel> customerInfoViewModels { get; set; }
        public DbQuery<POSInvoiceListViewModel> pOSInvoiceListViewModels { get; set; }
        public DbQuery<SalesInvoiceDetailViewModel> salesInvoiceDetailViewModels { get; set; }
        public DbQuery<PatientInvoiceDetailViewModel> patientInvoiceDetailViewModels { get; set; }
        public DbQuery<MenuItemViewModel> menuItemViewModels { get; set; }
        #endregion

        #region Distribution
        public DbSet<OPUSERP.Distribution.Data.Entity.Requisition.RequisitionMaster> distributionRequisitionMasters { get; set; }
        public DbSet<OPUSERP.Distribution.Data.Entity.Requisition.RequisitionDetail> distributionRequisitionDetails { get; set; }
        public DbQuery<AreaTeamViewModel> areaTeamViewModels { get; set; }
        public DbQuery<RequisitionApprovalViewModel> requisitionApprovalViewModels { get; set; }
        public DbSet<DisItemPriceFixing> disItemPriceFixings { get; set; }
        public DbSet<DisItemPriceFixingDetails> disItemPriceFixingDetails { get; set; }
        public DbQuery<ItemPriceFixingDetailViewModel> itemPriceFixingDetailViewModels { get; set; }
        public DbSet<PackageMaster> packageMasters { get; set; }
        public DbSet<PackageDetail> packageDetails { get; set; }

        #endregion

        #region POS

        public DbSet<CardType> CardTypes { get; set; }
        public DbSet<OPUSERP.POS.Data.Entity.ItemPriceFixing> ItemPriceFixings { get; set; }
        public DbSet<MobileBankingType> MobileBankingTypes { get; set; }
        public DbSet<OfferDetails> OfferDetails { get; set; }
        public DbSet<OfferMaster> OfferMasters { get; set; }
        public DbSet<PosBillReturnPaymentDetail> posBillReturnPaymentDetails { get; set; }
        public DbSet<PosBillReturnPaymentMaster> posBillReturnPaymentMasters { get; set; }
        public DbSet<PosCollectionDetail> posCollectionDetails { get; set; }
        public DbSet<PosCollectionMaster> PosCollectionMaster { get; set; }
        public DbSet<PosCustomer> posCustomers { get; set; }
        public DbSet<PosInvoiceDetail> posInvoiceDetails { get; set; }
        public DbSet<PosInvoiceMaster> posInvoiceMasters { get; set; }
        public DbSet<PosRepInvoiceDetail> posRepInvoiceDetails { get; set; }
        public DbSet<PosRepInvoiceMaster> posRepInvoiceMasters { get; set; }
        public DbSet<OPUSERP.POS.Data.Entity.PosSalesReturnInvoiceDetail> PosSalesReturnInvoiceDetails { get; set; }
        public DbSet<OPUSERP.POS.Data.Entity.PosSalesReturnInvoiceMaster> PosSalesReturnInvoiceMasters { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<StoreType> StoreTypes { get; set; }
        public DbSet<StoreAddress> StoreAddresses { get; set; }
        public DbSet<StoreContact> StoreContacts { get; set; }
        public DbSet<StoreAssign> StoreAssigns { get; set; }
        public DbSet<DiscountRate> DiscountRates { get; set; }



        #endregion

        #region Settings Configs
        public override int SaveChanges()
        {
            AddTimestamps();
            return base.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            AddTimestamps();
            return await base.SaveChangesAsync();
        }

        private void AddTimestamps()
        {

            var entities = ChangeTracker.Entries().Where(x => x.Entity is Base && (x.State == EntityState.Added || x.State == EntityState.Modified));

            var currentUsername = !string.IsNullOrEmpty(_httpContextAccessor?.HttpContext?.User?.Identity?.Name)
                ? _httpContextAccessor.HttpContext.User.Identity.Name
                : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((Base)entity.Entity).createdAt = DateTime.Now;
                    ((Base)entity.Entity).createdBy = currentUsername;
                }
                else
                {
                    entity.Property("createdAt").IsModified = false;
                    entity.Property("createdBy").IsModified = false;
                    ((Base)entity.Entity).updatedAt = DateTime.Now;
                    ((Base)entity.Entity).updatedBy = currentUsername;
                }

                #region changelog
                int sessionId = 0;
                DateTime myDate1 = new DateTime(1970, 1, 9, 0, 0, 00);
                DateTime myDate2 = DateTime.Now;
                TimeSpan myDateResult;
                myDateResult = myDate2 - myDate1;
                double seconds = myDateResult.TotalSeconds;
                sessionId = Convert.ToInt32(seconds);

                string entityName = entity.Entity.GetType().Name;
                string entityState = entity.State.ToString();
                if (entityName != "UserLogHistory")
                {

                    var builder = new ConfigurationBuilder()
                                    .SetBasePath(Directory.GetCurrentDirectory())
                                    .AddJsonFile("appsettings.json");

                    var configuration = builder.Build();

                    using (var db = new SqlConnection(configuration.GetConnectionString("ERPConnection")))
                    {
                        db.Open();

                        var entityMember = entity.Members;
                        var value = entity.Members.Count();
                        var entityinfo = entity.Entity.GetType();
                        var entityVal = entity.Entity;
                        string customAttributeName = string.Empty;
                        var fieldName = entityinfo.GetProperties();
                        for (int i = 0; i < fieldName.Count(); i++)
                        {
                            var columnName = fieldName[i].Name;
                            string colType = fieldName[i].PropertyType.ToString();
                            var custmAttribute = fieldName[i].GetCustomAttributesData();
                            if (custmAttribute.Count() >= 1)
                                customAttributeName = custmAttribute.FirstOrDefault().AttributeType.Name;

                            if (colType.Contains("OPUSERP") || customAttributeName == "NotMappedAttribute")
                            {

                            }
                            else
                            {

                                var valueName = entity?.Property(columnName)?.CurrentValue?.ToString();
                                valueName = valueName?.Replace("'", "''");
                                string Tmp1 = $"INSERT INTO DbChangeHistories (entityName,fieldName,fieldValue,entityState,sessionId,createdBy,createdAt) VALUES('{entityName}','{columnName}','{valueName}','{entityState}','{sessionId}','{currentUsername}','{DateTime.Now}');";
                                SqlCommand cmd1 = new SqlCommand(Tmp1, db);
                                cmd1.ExecuteScalar();
                            }

                        }
                        db.Close();
                    }

                }

                #endregion
            }
        }
        #endregion

        #region VMS
        #region Vehicle Info
        public DbSet<Aspiration> Aspirations { get; set; }
        public DbSet<BlockType> BlockTypes { get; set; }
        public DbSet<BrakeSystem> BrakeSystems { get; set; }
        public DbSet<CamType> CamTypes { get; set; }
        public DbSet<VMS.Data.Entity.VehicleInfo.DriveType> DriveTypes { get; set; }
        public DbSet<FuelInduction> FuelInductions { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<LinkedVehicle> LinkedVehicles { get; set; }
        public DbSet<Odometer> Odometers { get; set; }
        public DbSet<TransmissionType> TransmissionTypes { get; set; }
        public DbSet<VehicleBodySubType> VehicleBodySubTypes { get; set; }
        public DbSet<VehicleBodyType> VehicleBodyTypes { get; set; }
        public DbSet<VehicleComment> VehicleComments { get; set; }
        public DbSet<VehicleEngineTransmission> VehicleEngineTransmissions { get; set; }
        public DbSet<VehicleFluid> VehicleFluids { get; set; }
        public DbSet<VehicleGroup> VehicleGroups { get; set; }
        public DbSet<VehicleInformation> VehicleInformations { get; set; }
        public DbSet<VehicleManufacture> VehicleManufactures { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleOwnership> VehicleOwnerships { get; set; }
        public DbSet<VehicleSpecification> VehicleSpecifications { get; set; }
        public DbSet<VehicleStatus> VehicleStatuses { get; set; }
        public DbSet<VehicleType> VehicleTypes { get; set; }
        public DbSet<VehicleWheelTire> VehicleWheelTires { get; set; }
        public DbSet<VehicleRenewalReminder> VehicleRenewalReminders { get; set; }

        public DbSet<MeterType> MeterTypes { get; set; }
        public DbSet<VehicleSetting> VehicleSettings { get; set; }
        public DbQuery<ChartViewModel> chartViewModels { get; set; }
        public DbQuery<CommentsViewModel> commentsViewModels { get; set; }
        //public DbSet<VMSDocumentAttachment> VMSDocumentAttachments { get; set; }
        #endregion

        #region ContactRenewalReminder
        public DbSet<ContactRenewalReminder> ContactRenewalReminders { get; set; }
        #endregion

        #region Vehicle Service
        public DbSet<LabelType> LabelTypes { get; set; }
        public DbSet<ServiceLabel> ServiceLabels { get; set; }
        public DbSet<VehicleServiceIssue> VehicleServiceIssues { get; set; }
        public DbSet<VehicleLineItem> VehicleLineItems { get; set; }
        public DbSet<ServiceTask> ServiceTasks { get; set; }
        public DbSet<VehicleServiceHistory> VehicleServiceHistories { get; set; }
        public DbSet<VehicleServiceComment> VehicleServiceComments { get; set; }
        public DbSet<VehicleServiceIssueComment> vehicleServiceIssueComments { get; set; }
        public DbSet<VehicleServiceReminder> VehicleServiceReminders { get; set; }
        public DbSet<ReminderSchedule> ReminderSchedules { get; set; }
        public DbSet<InspectionMaster> InspectionMasters { get; set; }
        public DbSet<InspectionDetail> InspectionDetails { get; set; }
        public DbSet<VehicleMaintenanceLimit> VehicleMaintenanceLimits { get; set; }
        public DbSet<VMSSupplier> VMSSuppliers { get; set; }


        public DbSet<VehicleMaintenanceLimitDetail> VehicleMaintenanceLimitDetails { get; set; }
        public DbSet<ItemStoreInServiceCenter> ItemStoreInServiceCenters { get; set; }
        #endregion

        #region Requisition
        public DbSet<VMSRequisitionMaster> VMSRequisitionMasters { get; set; }
        public DbSet<VMSRequisitionDetails> VMSRequisitionDetails { get; set; }
        public DbSet<RequisitionComment> RequisitionComments { get; set; }
        public DbSet<VehicleUse> VehicleUses { get; set; }
        #endregion

        #region Agreement
        public DbSet<AgreementCostHead> AgreementCostHeads { get; set; }
        public DbSet<AgreementInformation> AgreementInformation { get; set; }
        public DbSet<AgreementCostInformation> AgreementCostInformation { get; set; }
        #endregion

        #region Inventory
        public DbSet<PurchasePartsMaster> PurchasePartsMasters { get; set; }
        public DbSet<PurchasePartsDetail> PurchasePartsDetails { get; set; }

        public DbSet<PartsIssueMaster> PartsIssueMasters { get; set; }
        public DbSet<PartsIssueDetails> PartsIssueDetails { get; set; }
        #endregion

        #region Fuel Info
        public DbSet<FuelStation> FuelStations { get; set; }
        public DbSet<FuelInformation> FuelInformations { get; set; }
        public DbSet<FuelComment> FuelComments { get; set; }
        public DbSet<FuelDetail> FuelDetails { get; set; }


        #endregion

        #region Fuelstationinfo
        public DbSet<FuelStationInfo> FuelStationInfos { get; set; }
        public DbSet<StationFuelInfo> StationFuelInfos { get; set; }
        public DbSet<FuelStationComment> FuelStationComments { get; set; }
        public DbSet<VMSContact> VMSContacts { get; set; }
        public DbSet<VMSAddress> VMSAddresses { get; set; }
        #endregion


        #region Car Management
        public DbSet<SourceType> SourceTypes { get; set; }
        public DbSet<SpareParts> SpareParts { get; set; }
        #endregion

        #region SOR
        public DbSet<SORMaster> SORMasters { get; set; }
        public DbSet<SORDetails> SORDetails { get; set; }
        #endregion

        #region Master Data
        public DbSet<InspectionCheckLIstCategory> InspectionCheckLIstCategories { get; set; }
        public DbSet<LimitPeriodType> LimitPeriodTypes { get; set; }
        public DbSet<LimitAmountType> LimitAmountTypes { get; set; }
        public DbSet<VMSResourceType> VMSResourceTypes { get; set; }
        public DbSet<VMSResource> VMSResources { get; set; }
        public DbSet<RenewalType> RenewalTypes { get; set; }
        #endregion


        #region Reporting

        public DbSet<ReportField> ReportFields { get; set; }
        public DbQuery<ServiceReportViewModel> serviceReportViews { get; set; }
        public DbQuery<LogBookReportViewModel> logBookReportViews { get; set; }

        #endregion

        #endregion

        #region CRO

        #region Distribute Jobs
        public DbSet<OperationMaster> operationMasters { get; set; }
        public DbSet<JobStatus> jobStatuses { get; set; }
        public DbSet<ProposedRating> proposedRatings { get; set; }
        public DbSet<IRCRating> iRCRatings { get; set; }
        public DbSet<Archive> archives { get; set; }
        public DbSet<CRO.Data.Entity.DistributeJob.StatusLog> statusLogs { get; set; }
        public DbSet<RatingType> ratingTypes { get; set; }
        public DbSet<RatingValue> ratingValues { get; set; }
        public DbSet<AttachmentStatus> AttachmentStatuses { get; set; }
        public DbSet<AttachmentType> AttachmentTypes { get; set; }
        public DbSet<DocumentChart> DocumentCharts { get; set; }
        public DbSet<ReceiveDocument> ReceiveDocuments { get; set; }
        public DbSet<IRCMemberComments> IRCMemberComments { get; set; }
        public DbSet<IRCMeetingAttendance> IRCMeetingAttendances { get; set; }
        public DbSet<IRCSignatory> IRCSignatories { get; set; }
        public DbSet<ArchiveFloor> archiveFloors { get; set; }
        public DbSet<ArchiveShelf> archiveShelves { get; set; }

        public DbQuery<CroAttachmentViewModel> CroAttachmentViewModels { get; set; }
        public DbQuery<CroDocumentsViewModel> croDocumentsViewModels { get; set; }
        public DbQuery<CroStatusViewModel> croStatusViewModels { get; set; }
        public DbQuery<OperationMasterSPViewModel> operationMasterSPViewModels { get; set; }
        public DbQuery<OperationMasterSPJAViewModel> operationMasterSPJAViewModels { get; set; }
        public DbQuery<OperationMasterSPHoldViewModel> operationMasterSPHoldViewModels { get; set; }
        public DbQuery<OperationMasterSPBlockUnViewModel> operationMasterSPBlockUnViewModels { get; set; }
        public DbQuery<OperationMasterSPRateViewModel> operationMasterSPRateViewModels { get; set; }
        public DbQuery<PreviousRatingDataViewModel> previousRatingDataViewModels { get; set; }
        //public DbQuery<PreviousRatingDataViewModel> previousRatingDataViewModels { get; set; }
        #endregion

        #endregion

        #region Program
        public DbSet<ProgramAttachment> programAttachments { get; set; }
        public DbSet<ProgramImpact> programImpacts { get; set; }
        public DbSet<ProgramStatus> programStatuses{ get; set; }
        public DbSet<ProgramOutCome> programOutComes { get; set; }
        public DbSet<ProgramOutComeProgres> programOutComeProgres { get; set; }
        public DbSet<ProgramOutPut> programOutPuts { get; set; }
        public DbSet<ProgramOutPutProgres> programOutPutProgres { get; set; }
        public DbSet<ProgramIndicator> programIndicators { get; set; }
        public DbSet<ProgramActivity> programActivities { get; set; }
        public DbSet<ProgramActivityProgres> programActivityProgres { get; set; }
        public DbSet<ProgramCategory> programCategories { get; set; }
        public DbSet<ProgramHeadline> programHeadlines { get; set; }
        public DbSet<ProgramMainCategory> programMainCategories { get; set; }
        public DbSet<ProgramMaster> programMasters { get; set; }
        public DbSet<ProgramPeopleAttendee> programAttendees { get; set; }
        public DbSet<BenificiaryType> benificiaryTypes { get; set; }
        public DbSet<IdType> idTypes { get; set; }
        public DbSet<DateRange> dateRanges { get; set; }
        public DbSet<Gender> genders { get; set; }
        public DbSet<BenificiaryActiveType> benificiaryActiveTypes { get; set; }
        public DbSet<PeopleType> peopleTypes { get; set; }
        public DbSet<ProgramAddress> programAddresses { get; set; }
        public DbSet<ProgramPeopleInDiscussion> programPeopleInDiscussions { get; set; }
        public DbSet<ProgramSubject> programSubjects { get; set; }
        public DbSet<ProgramViewer> programViewers { get; set; }
        public DbSet<ProgramYear> programYears { get; set; }
        public DbSet<programPlan> programPlans { get; set; }
        public DbSet<ProgramVideo> programVideos { get; set; }
        public DbSet<ProgramLocation> programLocations { get; set; }
        public DbSet<ProgramImplementor> ProgramImplementors { get; set; }
        public DbSet<ProgramSourceFund> ProgramSourceFunds { get; set; }
        public DbSet<ProgramSourceFundApprove> ProgramSourceFundApproves { get; set; }
        public DbSet<ProgramWorkPlan> ProgramWorkPlans { get; set; }
        public DbSet<ProgramPlanLocation> ProgramPlanLocations { get; set; }

        public DbQuery<ProgramPlanListViewModel> programPlanListViewModels { get; set; }
        public DbQuery<ProgramPlanReportViewModel> programPlanReportViewModels { get; set; }
        #endregion

        #region Purchase
        public DbSet<PurchaseOrdersMaster> PurchaseOrderMasterss { get; set; }
        public DbSet<PurchaseOrdersDetail> PurchaseOrderDetailss { get; set; }

        public DbSet<TransferMaster> TransferMasters { get; set; }
        public DbSet<TransferDetail> TransferDetails { get; set; }

        public DbSet<BillPaymentMaster> BillPaymentMasters { get; set; }
        public DbSet<BillPaymentDetail> BillPaymentDetails { get; set; }

        public DbSet<BillReceiveInformation> billReceiveInformation { get; set; }

        public DbSet<PurchaseReturnInfoMaster> purchaseReturnInfoMasters { get; set; }
        public DbSet<PurchaseReturnInfoDetail> purchaseReturnInfoDetails { get; set; }

        #endregion

        #region Workflow
        public DbSet<Doc> docs { get; set; }
        public DbSet<DocRoute> docRoutes { get; set; }
        public DbSet<ActionInfo> actionInfos { get; set; }
        public DbSet<DocAttachment> docAttachments { get; set; }
        public DbSet<Urlgeneration> urlgenerations { get; set; }
        public DbSet<ReviewRoute> reviewRoutes { get; set; }

        public DbSet<DraftDoc> draftDocs { get; set; }
        public DbSet<DraftRouting> draftRoutings { get; set; }
        public DbSet<DraftAttachment> draftAttachments { get; set; }
        #endregion

        #region Task
        public DbSet<TaskProject> taskProjects { get; set; }
        public DbSet<TaskSection> taskSections { get; set; }
        public DbSet<TaskInformation> taskInformations { get; set; }
        public DbSet<TaskFollower> taskFollowers { get; set; }
        public DbSet<TaskTag> taskTags { get; set; }
        public DbSet<TaskAttachment> taskAttachments { get; set; }
        public DbSet<TaskComment> taskComments { get; set; }
        public DbSet<TaskSubtask> taskSubtasks { get; set; }
        public DbSet<TaskHours> taskHours { get; set; }
        public DbSet<TaskFollowUp> taskFollowUps { get; set; }
        public DbSet<TaskStatusList> taskStatusLists { get; set; }
        #endregion


        #region MessageBox
        public DbSet<MessageGroup> messageGroups { get; set; }
        public DbSet<MessageGroupMember> messageGroupMembers { get; set; }
        public DbSet<MessageBoxInfo> messageBoxes { get; set; }
        public DbSet<MessageBoxFile> messageBoxFiles { get; set; }
        #endregion

        #region Board Meeting
        public DbSet<MeetingDocument> meetingDocuments { get; set; }
        public DbSet<MeetingDocRoute> meetingDocRoutes { get; set; }
        public DbSet<MeetingActionInfo> meetingActionInfos { get; set; }
        public DbSet<MeetingDocAttachment> meetingDocAttachments { get; set; }


        public DbSet<MeetingInfo> meetingInfos { get; set; }
        public DbSet<MeetingDocs> meetingDocs { get; set; }
        public DbSet<MeetingDetail> meetingDetails { get; set; }
        public DbSet<MeetingAttendance> meetingAttendances { get; set; }
        public DbSet<MeetingUser> meetingUsers { get; set; }
        #endregion

        #region HOSPITAL Management

        public DbSet<DiseaseInfo> DiseaseInfos { get; set; }
        public DbSet<PatientHistory> PatientHistories { get; set; }
        public DbSet<PatientInfo> PatientInfos { get; set; }
        public DbSet<PatientRepresentative> PatientRepresentatives { get; set; }
        public DbSet<PatientService> PatientServices { get; set; }
        public DbSet<ProfessionType> ProfessionTypes { get; set; }
        public DbSet<PatientServiceDetail> PatientServiceDetails { get; set; }
       
        #endregion

        #region Change History
        public DbSet<DbChangeHistory> DbChangeHistories { get; set; }

        #endregion

        #region Distributor
        #region MasterData
        public DbSet<SalesLevel> SalesLevels { get; set; }
        public DbSet<SalesTeamDeployment> salesTeamDeployments { get; set; }
        public DbSet<DistributorType> distributorTypes { get; set; }
        #endregion

        #endregion

        #region SEBA

        public DbSet<CustomerProductWarranty> CustomerProductWarranties { get; set; }
        public DbSet<ProblemMaster> ProblemMasters { get; set; }
        public DbSet<ProblemDetail> ProblemDetails { get; set; }

        #endregion

        #region Shagotom

        public DbSet<VisitorEntryMaster> VisitorEntryMasters { get; set; }
        public DbSet<VisitorEntryDetails> VisitorEntryDetails { get; set; }

        #endregion

        #region Production
        public DbSet<BOMMaster> BOMMasters { get; set; }
        public DbSet<BOMDetail> BOMDetails { get; set; }
        public DbSet<BOMOverheadDetail> BOMOverheadDetails { get; set; }
        public DbSet<ProductionMaster> ProductionMasters { get; set; }
        public DbSet<ProductionDetails> ProductionDetails { get; set; }
        public DbSet<ProductionType> ProductionTypes { get; set; }
        public DbSet<ProductionDocumentAttachment> ProductionDocumentAttachments { get; set; }

        //New Table
        public DbSet<ProductionPlan> productionPlans { get; set; }
        public DbSet<ProductionBatch> productionBatches { get; set; }
        public DbSet<OverheadCost> overheadCosts { get; set; }
        public DbSet<ProductionRequsitionMaster> productionRequsitionMasters { get; set; }
        public DbSet<ProductionRequsitionDetails> productionRequsitionDetails { get; set; }
        public DbQuery<ParentIdViewModel> parentIdViewModels { get; set; }

        #endregion

    }
}


