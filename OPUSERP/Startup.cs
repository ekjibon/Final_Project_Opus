using DinkToPdf;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using OPUSERP.Accounting.Services.AccountingSettings;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Accounting.Services.FDR;
using OPUSERP.Accounting.Services.FDR.Interface;
using OPUSERP.Accounting.Services.MasterData;
using OPUSERP.Accounting.Services.MasterData.Interfaces;
using OPUSERP.Accounting.Services.Voucher;
using OPUSERP.Accounting.Services.Voucher.Interfaces;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Budget.Service;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.CLUB.Services.Bulk;
using OPUSERP.CLUB.Services.Bulk.Interface;
using OPUSERP.CLUB.Services.Channel;
using OPUSERP.CLUB.Services.Channel.Interfaces;
using OPUSERP.CLUB.Services.Event;
using OPUSERP.CLUB.Services.Event.Interface;
using OPUSERP.CLUB.Services.Event.Interfaces;
using OPUSERP.CLUB.Services.Finance;
using OPUSERP.CLUB.Services.Finance.Interface;
using OPUSERP.CLUB.Services.Forum;
using OPUSERP.CLUB.Services.Forum.Interface;
using OPUSERP.CLUB.Services.Gallery;
using OPUSERP.CLUB.Services.Gallery.Interface;
using OPUSERP.CLUB.Services.Hall_Rent;
using OPUSERP.CLUB.Services.Hall_Rent.Interfaces;
using OPUSERP.CLUB.Services.Master;
using OPUSERP.CLUB.Services.Master.Interface;
using OPUSERP.CLUB.Services.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.CLUB.Services.News;
using OPUSERP.CLUB.Services.News.Interfaces;
using OPUSERP.CLUB.Services.Notice;
using OPUSERP.CLUB.Services.Notice.Interfaces;
using OPUSERP.CRM.Services.Budget;
using OPUSERP.CRM.Services.Budget.Interfaces;
using OPUSERP.CRM.Services.Client;
using OPUSERP.CRM.Services.Client.Interfaces;
using OPUSERP.CRM.Services.Cold;
using OPUSERP.CRM.Services.Cold.Interfaces;
using OPUSERP.CRM.Services.Dashboard;
using OPUSERP.CRM.Services.Dashboard.Interfaces;
using OPUSERP.CRM.Services.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRM.Services.Notes;
using OPUSERP.CRM.Services.Notes.Interfaces;
using OPUSERP.CRM.Services.Proposal;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using OPUSERP.CRO.Services.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity;
using OPUSERP.Distribution.Services.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using OPUSERP.Distribution.Services.Requisition;
using OPUSERP.Distribution.Services.Requisition.Interfaces;
using OPUSERP.DMS.Services.Document;
using OPUSERP.DMS.Services.Document.Interfaces;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPService.MasterData;
using OPUSERP.ERPService.MasterData.Interfaces;
using OPUSERP.ERPServices.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.AuthService;
using OPUSERP.ERPServices.AuthService.Interfaces;
using OPUSERP.ERPServices.EmailService;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.ERPServices.Interfaces;
using OPUSERP.ERPServices.MasterData;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.FixedAsset.Services.AssetRegister;
using OPUSERP.FixedAsset.Services.AssetRegister.Interfaces;
using OPUSERP.FixedAsset.Services.Dashboard;
using OPUSERP.FixedAsset.Services.Dashboard.Interfaces;
using OPUSERP.FixedAsset.Services.MasterData;
using OPUSERP.FixedAsset.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.ACR;
using OPUSERP.HRPMS.Services.ACR.Interfaces;
using OPUSERP.HRPMS.Services.Assignment;
using OPUSERP.HRPMS.Services.Assignment.Interfaces;
using OPUSERP.HRPMS.Services.Attendance;
using OPUSERP.HRPMS.Services.Attendance.Interfaces;
using OPUSERP.HRPMS.Services.Auth;
using OPUSERP.HRPMS.Services.Auth.Interfaces;
using OPUSERP.HRPMS.Services.AwardPublication;
using OPUSERP.HRPMS.Services.AwardPublication.Interfaces;
using OPUSERP.HRPMS.Services.Dashboard;
using OPUSERP.HRPMS.Services.Dashboard.interfaces;
using OPUSERP.HRPMS.Services.DisciplineInvestigation;
using OPUSERP.HRPMS.Services.DisciplineInvestigation.Interfaces;
using OPUSERP.HRPMS.Services.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.Gratuity;
using OPUSERP.HRPMS.Services.Gratuity.Interfaces;
using OPUSERP.HRPMS.Services.Leave;
using OPUSERP.HRPMS.Services.Leave.Interfaces;
using OPUSERP.HRPMS.Services.Library;
using OPUSERP.HRPMS.Services.Library.Interfaces;
using OPUSERP.HRPMS.Services.MasterData;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Movement;
using OPUSERP.HRPMS.Services.Organogram;
using OPUSERP.HRPMS.Services.Organogram.Interfaces;
using OPUSERP.HRPMS.Services.PromotionAndTransfer;
using OPUSERP.HRPMS.Services.PromotionAndTransfer.Interfaces;
using OPUSERP.HRPMS.Services.PunishmentCharge;
using OPUSERP.HRPMS.Services.PunishmentCharge.Interfaces;
using OPUSERP.HRPMS.Services.Recruitment;
using OPUSERP.HRPMS.Services.Recruitment.Interfaces;
using OPUSERP.HRPMS.Services.Report;
using OPUSERP.HRPMS.Services.RetirementAndTermination;
using OPUSERP.HRPMS.Services.RetirementAndTermination.Interface;
using OPUSERP.HRPMS.Services.RewardInfo;
using OPUSERP.HRPMS.Services.RewardInfo.Interfaces;
using OPUSERP.HRPMS.Services.Training;
using OPUSERP.HRPMS.Services.Training.Interfaces;
using OPUSERP.HRPMS.Services.TrainingNew;
using OPUSERP.HRPMS.Services.TrainingNew.Interfaces;
using OPUSERP.HRPMS.Services.Travel;
using OPUSERP.HRPMS.Services.Travel.Interface;
using OPUSERP.HRPMS.Services.VehicleInfo;
using OPUSERP.HRPMS.Services.VehicleInfo.Interfaces;
using OPUSERP.HRPMS.Services.Visitor;
//using OPUSERP.Payroll.Services.PF.Interfaces;
//using OPUSERP.Payroll.Services.PF;
using OPUSERP.HRPMS.Services.Visitor.Interface;
using OPUSERP.Meeting.Service;
using OPUSERP.Meeting.Service.Interface;
using OPUSERP.MessageBox.Service;
using OPUSERP.MessageBox.Service.Interface;
using OPUSERP.OtherSales.Services.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.Patient.Services;
using OPUSERP.Patient.Services.Interfaces;
using OPUSERP.Payroll.Services.Dashboard;
using OPUSERP.Payroll.Services.Dashboard.Interfaces;
using OPUSERP.Payroll.Services.FinalSettlements;
using OPUSERP.Payroll.Services.FinalSettlements.Interfaces;
using OPUSERP.Payroll.Services.IncomeTax;
using OPUSERP.Payroll.Services.IncomeTax.Interfaces;
using OPUSERP.Payroll.Services.PF;
using OPUSERP.Payroll.Services.PF.Interfaces;
using OPUSERP.Payroll.Services.Salary;
using OPUSERP.Payroll.Services.Salary.Interfaces;
using OPUSERP.Production.Services;
using OPUSERP.Production.Services.Interfaces;
using OPUSERP.Programs.Service;
using OPUSERP.Programs.Service.Interface;
using OPUSERP.Purchase.Service;
using OPUSERP.Purchase.Service.Interfaces;
using OPUSERP.Purchase.Services;
using OPUSERP.Purchase.Services.Interfaces;
using OPUSERP.Recruitment.Services;
using OPUSERP.Recruitment.Services.Interfaces;
using OPUSERP.REMS.Services;
using OPUSERP.REMS.Services.Interfaces;
using OPUSERP.Rental.Services.Sales;
using OPUSERP.Rental.Services.Sales.Interfaces;
using OPUSERP.Sales.Services.Collection;
using OPUSERP.Sales.Services.Collection.Interface;
using OPUSERP.Sales.Services.Collection.Interfaces;
using OPUSERP.Sales.Services.Sales;
using OPUSERP.Sales.Services.Sales.Interface;
using OPUSERP.Sales.Services.Sales.Interfaces;
using OPUSERP.SCM.Helpers;
using OPUSERP.SCM.Services.Dashboard;
using OPUSERP.SCM.Services.Dashboard.Interfaces;
using OPUSERP.SCM.Services.Disbarse;
using OPUSERP.SCM.Services.Disbarse.Interface;
using OPUSERP.SCM.Services.IOU;
using OPUSERP.SCM.Services.IOU.Interface;
using OPUSERP.SCM.Services.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;
using OPUSERP.SCM.Services.Matrix;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.SCM.Services.ProjectStatus;
using OPUSERP.SCM.Services.ProjectStatus.Interface;
using OPUSERP.SCM.Services.PurchaseOrder;
using OPUSERP.SCM.Services.PurchaseOrder.Interface;
using OPUSERP.SCM.Services.PurchaseProcess;
using OPUSERP.SCM.Services.PurchaseProcess.Interfaces;
using OPUSERP.SCM.Services.Report;
using OPUSERP.SCM.Services.Report.Interface;
using OPUSERP.SCM.Services.Requisition;
using OPUSERP.SCM.Services.Requisition.Interfaces;
using OPUSERP.SCM.Services.SCMMail;
using OPUSERP.SCM.Services.SCMMail.interfaces;
using OPUSERP.SCM.Services.Stock;
using OPUSERP.SCM.Services.Stock.Interface;
using OPUSERP.SCM.Services.Supplier;
using OPUSERP.SCM.Services.Supplier.Interface;
using OPUSERP.SEBA.Services;
using OPUSERP.SEBA.Services.Interfaces;
using OPUSERP.Shagotom.Services.MasterData;
using OPUSERP.Shagotom.Services.MasterData.Interfaces;
using OPUSERP.Shagotom.Services.Visitor;
using OPUSERP.Shagotom.Services.Visitor.Interfaces;
using OPUSERP.TAMS.Service;
using OPUSERP.TAMS.Service.Interface;
using OPUSERP.VEMS.Services.MasterData;
using OPUSERP.VEMS.Services.MasterData.Interface;
using OPUSERP.VEMS.Services.Registration;
using OPUSERP.VEMS.Services.Registration.Interface;
using OPUSERP.VMS.Services.Agreement;
//using OPUSERP.SCM.Services.SCMMail;
//using OPUSERP.HRPMS.Services.Travel.Interface;
//using OPUSERP.HRPMS.Services.Travel;
//using OPUSERP.Recruitment.Services.Interfaces;
//using OPUSERP.Recruitment.Services;
//using OPUSERP.HRPMS.Services.RetirementAndTermination.Interface;
using OPUSERP.VMS.Services.Agreement.Interfaces;
using OPUSERP.VMS.Services.CarManagement;
using OPUSERP.VMS.Services.CarManagement.Interfaces;
using OPUSERP.VMS.Services.FuelInfo;
using OPUSERP.VMS.Services.FuelInfo.Interfaces;
using OPUSERP.VMS.Services.FuelStation;
using OPUSERP.VMS.Services.FuelStation.Interfaces;
using OPUSERP.VMS.Services.Inventory;
using OPUSERP.VMS.Services.Inventory.interfaces;
using OPUSERP.VMS.Services.MasterData;
using OPUSERP.VMS.Services.MasterData.Interfaces;
using OPUSERP.VMS.Services.Requisition;
using OPUSERP.VMS.Services.Requisition.Interfaces;
using OPUSERP.VMS.Services.Resources;
using OPUSERP.VMS.Services.Resources.Interfaces;
using OPUSERP.VMS.Services.SOR;
using OPUSERP.VMS.Services.SOR.Interfaces;
using OPUSERP.VMS.Services.VehicleInfo;
using OPUSERP.VMS.Services.VehicleInfo.Interfaces;
using OPUSERP.VMS.Services.VehicleService;
using OPUSERP.VMS.Services.VehicleService.Interfaces;
using OPUSERP.Workflow.Service;
using OPUSERP.Workflow.Service.Interface;
using System;
using System.Text;
using AddressEducationPhotoService = OPUSERP.HRPMS.Services.Employee.AddressEducationPhotoService;
using AwardPublicationLanguageService = OPUSERP.HRPMS.Services.Employee.AwardPublicationLanguageService;
using DesignationDepartmentService = OPUSERP.HRPMS.Services.MasterData.DesignationDepartmentService;
using DrivingLicenseInfoService = OPUSERP.HRPMS.Services.Employee.DrivingLicenseInfoService;
using IAwardPublicationLanguageService = OPUSERP.HRPMS.Services.Employee.Interfaces.IAwardPublicationLanguageService;
using IDesignationDepartmentService = OPUSERP.HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService;
using IDrivingLicenseService = OPUSERP.HRPMS.Services.Employee.Interfaces.IDrivingLicenseService;
using IEmployeeOrganogramService = OPUSERP.HRPMS.Services.Employee.Interfaces.IEmployeeOrganogramService;
using IMembershipService = OPUSERP.HRPMS.Services.Employee.Interfaces.IMembershipService;
using IPassportInfoService = OPUSERP.HRPMS.Services.Employee.Interfaces.IPassportInfoService;
using IPersonalInfoService = OPUSERP.HRPMS.Services.Employee.Interfaces.IPersonalInfoService;
using IPhotographService = OPUSERP.HRPMS.Services.Employee.Interfaces.IPhotographService;
using IServiceHistoryService = OPUSERP.HRPMS.Services.Employee.Interfaces.IServiceHistoryService;
using ISpouseChildrenService = OPUSERP.HRPMS.Services.Employee.Interfaces.ISpouseChildrenService;
using ITraningHistoryService = OPUSERP.HRPMS.Services.Employee.Interfaces.ITraningHistoryService;
using MembershipService = OPUSERP.HRPMS.Services.Employee.MembershipService;
using PassportInfoService = OPUSERP.HRPMS.Services.Employee.PassportInfoService;
using PersonalInfoService = OPUSERP.HRPMS.Services.Employee.PersonalInfoService;
using ServiceHistoryService = OPUSERP.HRPMS.Services.Employee.ServiceHistoryService;
using SpouseChildrenService = OPUSERP.HRPMS.Services.Employee.SpouseChildrenService;
using TraningHistoryService = OPUSERP.HRPMS.Services.Employee.TraningHistoryService;
//using ISpouseChildrenService = OPUSERP.CLUB.Services.Member.Interfaces.ISpouseChildrenService;
//using SpouseChildrenService = OPUSERP.CLUB.Services.Member.SpouseChildrenService;
//using IPersonalInfoService = OPUSERP.CLUB.Services.Member.Interfaces.IPersonalInfoService;
//using PersonalInfoService = OPUSERP.CLUB.Services.Member.PersonalInfoService;
//using IDrivingLicenseService = OPUSERP.CLUB.Services.Member.Interfaces.IDrivingLicenseService;
//using DrivingLicenseInfoService = OPUSERP.CLUB.Services.Member.DrivingLicenseInfoService;
//using IPassportInfoService = OPUSERP.CLUB.Services.Member.Interfaces.IPassportInfoService;
//using PassportInfoService = OPUSERP.CLUB.Services.Member.PassportInfoService;
//using IAwardPublicationLanguageService = OPUSERP.CLUB.Services.Member.Interfaces.IAwardPublicationLanguageService;
//using AwardPublicationLanguageService = OPUSERP.CLUB.Services.Member.AwardPublicationLanguageService;
//using IEmployeeOrganogramService = OPUSERP.CLUB.Services.Member.Interfaces.IEmployeeOrganogramService;
//using IMembershipService = OPUSERP.CLUB.Services.Member.Interfaces.IMembershipService;
//using MembershipService = OPUSERP.CLUB.Services.Member.MembershipService;
//using ITraningHistoryService = OPUSERP.CLUB.Services.Member.Interfaces.ITraningHistoryService;
//using TraningHistoryService = OPUSERP.CLUB.Services.Member.TraningHistoryService;
//using ServiceHistoryService = OPUSERP.CLUB.Services.Member.ServiceHistoryService;
//using IServiceHistoryService = OPUSERP.CLUB.Services.Member.Interfaces.IServiceHistoryService;
//using IPhotographService = OPUSERP.CLUB.Services.Member.Interfaces.IPhotographService;
//using AddressEducationPhotoService = OPUSERP.HRPMS.Services.Employee.AddressEducationPhotoService;

namespace OPUSERP
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });


            #region SCM Master Data
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IFundSourceService, FundSourceService>();
            services.AddScoped<IProjectStatusService, ProjcetStatusService>();
            //services.AddScoped<IItemsService,ItemsService>();
            #endregion

            #region SCM Matrix
            services.AddScoped<IMatrixTypeService, MatrixTypeService>();
            services.AddScoped<IApproverTypeService, ApproverTypeService>();
            services.AddScoped<IApprovalMatrixService, ApprovalMatrixService>();
            services.AddScoped<IApprovalMatrixlogService, ApprovalMatrixlogService>();
            services.AddScoped<IApprovalLogService, ApprovalLogService>();
            services.AddScoped<IStatusLogService, StatusLogService>();
            services.AddScoped<IStatusInfoService, StatusInfoService>();
            #endregion

            #region Budget
            services.AddScoped<IUnitOfTakaService, UnitOfTakaService>();
            services.AddScoped<IBudgetRequsitionMasterService, BudgetRequsitionMasterService>();
            services.AddScoped<IBudgetBranchService, BudgetBranchService>();
            services.AddScoped<IBudgetHeadService, BudgetHeadService>();
            services.AddScoped<IBudgetDisbursmentMasterService, BudgetDisbursmentMasterService>();
            services.AddScoped<IHOBudgetRequsitionService, HOBudgetRequsitionService>();

            #endregion

            #region Gratuity
            services.AddScoped<IGratuityPolicyService, GratuityPolicyService>();
            services.AddScoped<IHRManualService, HRManualService>();
            #endregion

            #region Document Upload
            services.AddScoped<IDocumentUploadService, DocumentUploadService>();
            #endregion

            #region Master Data
            services.AddScoped<ERPServices.MasterData.Interfaces.IAddressService, ERPServices.MasterData.AddressService>();
            services.AddScoped<IDesignationDepartmentService, DesignationDepartmentService>();
            services.AddScoped<IAddressTypeService, AddressTypeService>();
            services.AddScoped<IPageAssignService, PageAssignService>();
            services.AddScoped<IModuleAssignService, ModuleAssignService>();
            services.AddScoped<IUserInfoes, UserInfoes>();
            services.AddScoped<IAttachmentCommentService, AttachmentCommentService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IERPCompanyService, ERPCompanyService>();
            services.AddScoped<IERPModuleAssignService, ERPModuleAssignService>();
            services.AddScoped<IERPModuleService, EPRModuleService>();
            services.AddScoped<INavbarService, NavbarService>();
            services.AddScoped<IDbChangeService, DbChangeService>();
            #endregion

            #region Supplier
            services.AddScoped<ISupplierItemInfoService, SupplierItemInfoService>();
            services.AddScoped<SCM.Services.Supplier.Interface.IOrganizationService, SCM.Services.Supplier.OrganizationService>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<SCM.Services.Supplier.Interface.IAddressService, SCM.Services.Supplier.AddressService>();
            #endregion

            #region SCM Master Data
            services.AddScoped<IItemsService, ItemsService>();
            services.AddScoped<ISCMTeamService, SCMTeamService>();
            services.AddScoped<ISCMDashboardService, SCMDashboardService>();
            #endregion

            #region PDF
            services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));
            #endregion

            #region Requisition
            services.AddScoped<SCM.Services.Requisition.Interfaces.IRequisitionService, RequisitionService>();
            services.AddScoped< IRequisition, RRequisitionService >();
            services.AddScoped<RequisitionStatusHistory>();
            services.AddScoped<IExitInterviewService, ExitInterviewService>();
            #endregion

            #region IOU
            services.AddScoped<IIOUService, IOUService>();
            services.AddScoped<IDisbarseMasterService, DisbarseMasterService>();
            #endregion

            #region Report
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IItemSummaryService, ItemSummaryService>();

            #endregion

            #region SCM Purchase Process
            services.AddScoped<IPurchaseProcessService, PurchaseProcessService>();
            services.AddScoped<IProcurementService, ProcurementService>();
            #endregion

            #region SCM Purchase Order
            services.AddScoped<IPurchaseOrderService, PurchaseOrderService>();
            services.AddScoped<IPODismissService, PODismissService>();
            #endregion

            #region SCM Mail
            services.AddScoped<ISCMMailService, SCMMailService>();
            #endregion

            #region Stock
            services.AddScoped<IStockService, StockService>();
            #endregion

            #region ERP Database Settings
            services.AddDbContext<ERPDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ERPConnection")));

            services.AddMemoryCache();
            //.AddDefaultTokenProviders();
            services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ERPDbContext>().AddDefaultTokenProviders();
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ERPDbContext>();
            #endregion

            #region Auth JWT
            services.AddSingleton<IJwtFactoryService, JwtFactoryService>();
            var jwtAppsettingsOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtAppsettingsOptions["SecreatKey"]));

            services.Configure<JwtIssuerOptions>(Options =>
            {
                Options.Issuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)];
                Options.Audience = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Audience)];
                Options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppsettingsOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
            #endregion

            #region Auth Related Settings
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 4;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);

                options.LoginPath = "/Auth/Account/Login";
                options.AccessDeniedPath = "/Auth/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            #endregion

            #region Areas Config
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.AreaViewLocationFormats.Clear();
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/{1}/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/areas/{2}/Views/Shared/{0}.cshtml");
                options.AreaViewLocationFormats.Add("/Views/Shared/{0}.cshtml");
            });
            #endregion

            #region CRM
            #region Communication

            services.AddScoped<ICommunicationService, CommunicationService>();
            services.AddScoped<ICRMDashboardService, CRMDashboardService>();
            #endregion

            #region MasterData DI

            services.AddScoped<IActivityCategoryService, ActivityCategoryService>();
            services.AddScoped<IActivityTypeService, ActivityTypeService>();
            services.AddScoped<IActivityNatureService, ActivityNatureService>();
            services.AddScoped<IActivitySessionService, ActivitySessionService>();
            services.AddScoped<IAgreementCategoryService, AgreementCategoryService>();
            services.AddScoped<IActivityStatusService, ActivityStatusService>();
            services.AddScoped<IUserTypeService, UserTypeService>();
            services.AddScoped<IBankBranchService, BankBranchService>();
            services.AddScoped<IIndustryService, IndustryService>();
            services.AddScoped<IRatingCategoryService, RatingCategoryService>();
            #endregion

            #region Sector
            services.AddScoped<ISectorService, SectorService>();
            #endregion

            #region Team
            //services.AddScoped<ITeamService, TeamService>();
            #endregion

            #region Company Group
            services.AddScoped<ICompanyGroupService, CompanyGroupService>();
            #endregion

            #region Ownership Type
            services.AddScoped<IOwnershipService, OwnershipService>();
            #endregion

            #region Source
            services.AddScoped<ISourceService, SourceService>();
            #endregion

            #region Designation Department Service
            services.AddScoped<IDesignationDepartmentService, DesignationDepartmentService>();
            #endregion

            #region Leads
            services.AddScoped<IResourceService, ResourceService>();
            services.AddScoped<IAddressesService, AddressesService>();
            services.AddScoped<ILeadsService, LeadsService>();
            services.AddScoped<IContactsService, ContactsService>();
            services.AddScoped<IActivityMasterService, ActivityMasterService>();
            services.AddScoped<IActivityDetailsService, ActivityDetailsService>();
            services.AddScoped<IActivityTeamService, ActivityTeamService>();
            services.AddScoped<IColdService, ColdService>();
            services.AddScoped<IActivityStatusProgressService, ActivityStatusProgressService>();
            services.AddScoped<ICRMDesignationDepartmentService, CRMDesignationDepartmentService>();

            #endregion

            #region Leads
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IBillGenerateService, BillGenerateService>();
            services.AddScoped<IBillCollectionService, BillCollectionService>();
            #endregion

            #region Address
            services.AddScoped<ERPServices.MasterData.Interfaces.IAddressService, ERPServices.MasterData.AddressService>();
            services.AddScoped<IAddressTypeService, AddressTypeService>();
            #endregion

            #region CRM Proposal

            services.AddScoped<IProposalService, ProposalService>();
            services.AddScoped<INotesService, NotesService>();


            #endregion

            #region CRM Proposal
            services.AddScoped<IAgreementService, AgreementService>();
            #endregion
            #region Budget
            services.AddScoped<ITeamBudgetService, TeamBudgetService>();
            services.AddScoped<ICompanyBudgetService, CompanyBudgetService>();
            #endregion

            #endregion

            #region FAMS

            services.AddScoped<IRegistrationTypeService, RegistrationTypeService>();

            services.AddScoped<IAssetRegistrationService, AssetRegistrationService>();
            services.AddScoped<IDepriciationInfoService, DepriciationInfoService>();
            services.AddScoped<IPurchaseInfoService, PurchaseInfoService>();
            services.AddScoped<IAssetAssignService, AssetAssignService>();
            services.AddScoped<IDepriciationPeriodService, DepriciationPeriodService>();
            services.AddScoped<IDepriciationRateService, DepriciationRateService>();
            services.AddScoped<IAssetWarrentyService, AssetWarrentyService>();
            services.AddScoped<IAssetDepreciationService, AssetDepreciationService>();
            services.AddScoped<IFAMSDashboardService, FAMSDashboardService>();
            #endregion

            #region REMS
            services.AddScoped<IClaimRegisterService, ClaimRegisterService>();
            services.AddScoped<IRepairTransactionLogService, RepairTransactionLogService>();
            services.AddScoped<IClaimAssignService, ClaimAssignService>();
            services.AddScoped<IClaimAttachmentService, ClaimAttachmentService>();
            services.AddScoped<IClaimBillInformationService, ClaimBillInformationService>();
            #endregion

            #region RCRT
            services.AddScoped<IJobRequisitionService, JobRequisitionService>();
            services.AddScoped<ICandidateInfoService, CandidateInfoService>();
            services.AddScoped<IUpdateCandidateInfoService, UpdateCandidateInfoService>();
            services.AddScoped<IDesignationDepartmentService, DesignationDepartmentService>();
            services.AddScoped<IInterviewCallingService, InterviewCallingService>();
            services.AddScoped<IMarksEntryService, MarksEntryService>();

            services.AddScoped<IRcrtTrainingService, RcrtTrainingService>();
            services.AddScoped<IRcrtQualificationsService, RcrtQualificationsService>();
            services.AddScoped<IRcrtPublicationService, RcrtPublicationService>();
            services.AddScoped<IRcrtPrevEmploymentService, RcrtPrevEmploymentService>();
            #endregion

            #region VEMS Master Data
            services.AddScoped<IMasterDataServices, MasterDataServices>();
            #endregion

            #region VEMS RegisterForm
            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IAuthorizedPersonService, AuthorizedPersonService>();
            services.AddScoped<IBankInformationService, BankInformationService>();
            services.AddScoped<ICompanyInformationService, CompanyInformationService>();
            services.AddScoped<IInterestedAreaService, InterestedAreaService>();
            services.AddScoped<ITopOfficialService, TopOfficialService>();
            services.AddScoped<IVendorAddressService, VendorAddressService>();
            services.AddScoped<IVendorAttachmentService, VendorAttachmentService>();
            #endregion

            #region Payroll
            services.AddScoped<ISalaryService, SalaryService>();
            services.AddScoped<IIncomeTaxSlabService, IncomeTaxSlabService>();
            services.AddScoped<IFinalSettlementService, FinalSettlementService>();
            services.AddScoped<IPayrollDashboardService, PayrollDashboardService>();
            #endregion

            #region Accounting

            services.AddScoped<IAccountGroupService, AccountGroupService>();
            services.AddScoped<IAccountModeService, AccountModeService>();
            services.AddScoped<IGroupNatureService, GroupNatureService>();
            services.AddScoped<ILedgerService, LedgerService>();
            services.AddScoped<IOpeningBalanceService, OpeningBalanceService>();

            services.AddScoped<ICostCentreService, CostCentreService>();
            services.AddScoped<IVoucherTypeService, VoucherTypeService>();
            services.AddScoped<IRateTypeService, RateTypeService>();
            services.AddScoped<IVatTaxRateService, VatTaxRateService>();

            services.AddScoped<IAccountingReportService, AccountingReportService>();
            services.AddScoped<IVoucherService, VoucherService>();
            services.AddScoped<IDailyBillReceiveService, DailyBillReceiveService>();
            services.AddScoped<IBankChargeMasterService, BankChargeMasterService>();
            services.AddScoped<IFDRInvestmentService, FDRInvestmentService>();
            services.AddScoped<ISignatureService, SignatureService>();


            #endregion

            #region Sales
            services.AddScoped<ISalesInvoiceMasterService, SalesInvoiceMasterService>();
            services.AddScoped<ISalesInvoiceDetailService, SalesInvoiceDetailService>();
            services.AddScoped<ISalesCollectionService, SalesCollectionService>();
            services.AddScoped<ISalesCollectionDetailsService, SalesCollectionDetailsService>();
            #endregion
            #region Rental
            services.AddScoped<IRentInvoiceMasterService, RentInvoiceMasterService>();
            services.AddScoped<IRentInvoiceDetailService, RentInvoiceDetailService>();
            services.AddScoped<IHallRentalServices, HallRentalServices>();
            #endregion
            #region Other Sales
            services.AddScoped<IOSalesInvoiceMasterService, OSalesInvoiceMasterService>();
            services.AddScoped<IOSalesInvoiceDetailService, OSalesInvoiceDetailService>();
            services.AddScoped<IOSalesCollectionService, OSalesCollectionService>();
            services.AddScoped<IOSalesCollectionDetailsService, OSalesCollectionDetailsService>();
            services.AddScoped<IItemPriceFixingService, ItemPriceFixingService>();
            services.AddScoped<IBarCodeService, BarCodeInformationService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IAddressCategoryService, AddressCategoryService>();
            services.AddScoped<IPaymentModeService, PaymentModeService>();



            #endregion

            #region PF
            services.AddScoped<IPFService, PFService>();
            #endregion

            #region HRPMS
            #region MasterData DI
            services.AddScoped<ITypeService, EmployeeTypeService>();
            services.AddScoped<IMemberTypeService, MemberTypeService>();
            services.AddScoped<HRPMS.Services.MasterData.Interfaces.IAddressService, HRPMS.Services.MasterData.AddressService>();
            services.AddScoped<ILevelofEducationService, LevelofEducationService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IDegreeService, DegreeService>();
            services.AddScoped<HRPMS.Services.MasterData.Interfaces.IOrganizationService, HRPMS.Services.MasterData.OrganizationService>();
            services.AddScoped<HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService, HRPMS.Services.MasterData.DesignationDepartmentService>();
            services.AddScoped<ISalaryGradeService, SalaryGradeService>();
            services.AddScoped<ITrainingService, TrainingService>();
            services.AddScoped<ITravelService, TravelVehicleService>();
            services.AddScoped<IRelationService, RelationService>();
            services.AddScoped<IMembershipLanguageService, MembershipLanguageService>();
            services.AddScoped<IOccupationCadreService, OccupationCadreService>();
            services.AddScoped<IBookAwardService, BookAwardService>();
            services.AddScoped<ERPServices.MasterData.Interfaces.IDesignationDepartmentService, ERPServices.MasterData.DesignationDepartmentService>();
            services.AddScoped<IReligionMunicipalityService, ReligionMunicipalityService>();
            services.AddScoped<IStatusService, StatusService>();
            services.AddScoped<IHolidayService, HolidayService>();
            services.AddScoped<IRelDegreeSubjectService, RelDegreeSubjectService>();
            services.AddScoped<IYearCourseTitleService, YearCourseTitleService>();
            services.AddScoped<IOfficeAssignService, OfficeAssignService>();
            //New 
            services.AddScoped<IQualificationHeadService, QualificationHeadService>();
            services.AddScoped<INomineeFundService, NomineeFundService>();
            services.AddScoped<IHRPMSOrganizationTypeService, HRPMSOrganizationTypeService>();
            services.AddScoped<IHRPMSActivityTypeService, HRPMSActivityTypeService>();
            services.AddScoped<IActivityNameService, ActivityNameService>();
            services.AddScoped<IOtherQualificationHeadService, OtherQualificationHeadService>();
            services.AddScoped<ISpecialBranchUnitService, SpecialBranchUnitService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IFunctionsInfoService, FunctionsInfoService>();
            services.AddScoped<IPNSService, PNSService>();
            services.AddScoped<IRlnSBUPNSService, RlnSBUPNSService>();

            //travel module
            services.AddScoped<IHRDonerSevice, HRDonerSevice>();
            services.AddScoped<IHRActivityService, HRActivityService>();
            services.AddScoped<ITravelVehicleTypeService, TravelVehicleTypeService>();
            services.AddScoped<IHRProjectService, HRProjectService>();
            services.AddScoped<IBelongingsItemService, BelongingsItemService>();
            services.AddScoped<IAttachmentCategoryService, AttachmentCategoryService>();

            services.AddScoped<IApprovalService, ApprovalService>();

            // Employee Exit Interview

            services.AddScoped<IEmployeeExitInterviewService, EmployeeExitInterviewService>();
            services.AddScoped<ILoggedinUser, LoggedinUser>();
            #endregion

            #region Employee DI
            services.AddScoped<ISpouseChildrenService, SpouseChildrenService>();
            services.AddScoped<IPersonalInfoService, PersonalInfoService>();
            services.AddScoped<IDrivingLicenseService, DrivingLicenseInfoService>();
            services.AddScoped<IPassportInfoService, PassportInfoService>();
            services.AddScoped<IAwardPublicationLanguageService, AwardPublicationLanguageService>();
            services.AddScoped<IEmployeeInfoService, AddressEducationPhotoService>();
            services.AddScoped<IEmployeeContratInfoService, EmployeeContractInfoService>();
            services.AddScoped<IShiftGroupMasterService, ShiftGroupMasterService>();
            services.AddScoped<IShiftGroupDetailService, ShiftGroupDetailService>();
            services.AddScoped<IMembershipService, MembershipService>();
            services.AddScoped<IPromotionService, PromotionService>();
            services.AddScoped<ITravelInfoService, TravelInfoService>();
            services.AddScoped<IServiceHistoryService, ServiceHistoryService>();
            services.AddScoped<ITraningHistoryService, TraningHistoryService>();
            services.AddScoped<IPromotionLogService, PromotionLogService>();
            services.AddScoped<IPhotographService, PhotographService>();
            services.AddScoped<IBankInfoService, BankInfoService>();
            services.AddScoped<IDisciplinaryService, DisciplinaryService>();
            services.AddScoped<ILeaveLogService, LeaveLogService>();
            services.AddScoped<IEmployeeOrganogramService, EmployeeOrganogramService>();
            services.AddScoped<IFreedomFighterService, FreedomFighterService>();
            services.AddScoped<IOtherActivityService, OtherActivityService>();
            services.AddScoped<IOtherQualificationService, OtherQualificationService>();
            services.AddScoped<IProfessionalQualificationsService, ProfessionalQualificationsService>();
            services.AddScoped<IPriviousEmploymentService, PriviousEmploymentService>();
            services.AddScoped<IReferenceService, ReferenceService>();
            services.AddScoped<ISupervisorService, SupervisorService>();
            services.AddScoped<IEmergencyContactService, EmergencyContactService>();
            services.AddScoped<INomineeDetailService, NomineeDetailService>();
            services.AddScoped<INomineeService, NomineeService>();
            services.AddScoped<IBelongingsService, BelongingsService>();
            services.AddScoped<IEmployeeProjectActivityService, EmployeeProjectActivityService>();
            services.AddScoped<IFinanceCodeService, FinanceCodeService>();
            services.AddScoped<IPIMSVisitorService, PIMSVisitorService>();
            services.AddScoped<IHRPMSAttachmentService, HRPMSAttachmentService>();
            services.AddScoped<IHrmDashboardService, HrmDashboardService>();

            #endregion

            #region CLUB Member

            services.AddScoped<OPUSERP.CLUB.Services.Member.Interfaces.ISpouseChildrenService, OPUSERP.CLUB.Services.Member.SpouseChildrenService>();
            services.AddScoped<OPUSERP.CLUB.Services.Member.Interfaces.IPersonalInfoService, OPUSERP.CLUB.Services.Member.PersonalInfoService>();
            services.AddScoped<OPUSERP.CLUB.Services.Member.Interfaces.IDrivingLicenseService, OPUSERP.CLUB.Services.Member.DrivingLicenseInfoService>();
            services.AddScoped<OPUSERP.CLUB.Services.Member.Interfaces.IPassportInfoService, OPUSERP.CLUB.Services.Member.PassportInfoService>();
            services.AddScoped<OPUSERP.CLUB.Services.Member.Interfaces.IAwardPublicationLanguageService, OPUSERP.CLUB.Services.Member.AwardPublicationLanguageService>();
            services.AddScoped<OPUSERP.CLUB.Services.Member.Interfaces.IServiceHistoryService, OPUSERP.CLUB.Services.Member.ServiceHistoryService>();
            services.AddScoped<OPUSERP.CLUB.Services.Member.Interfaces.ITraningHistoryService, OPUSERP.CLUB.Services.Member.TraningHistoryService>();
            services.AddScoped<OPUSERP.CLUB.Services.Member.Interfaces.IMembershipService, OPUSERP.CLUB.Services.Member.MembershipService>();
            services.AddScoped<OPUSERP.CLUB.Services.Member.Interfaces.IPhotographService, OPUSERP.CLUB.Services.Member.Photographservice>();
            services.AddScoped<IMemberInfoService, OPUSERP.CLUB.Services.Member.AddressEducationPhotoService>();
            #endregion

            #region Bulk DI
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IRlnGroupMemberService, RlnGroupMemberService>();
            #endregion

            #region SMSEMAIL DI
            services.AddScoped<ISMSMailService, SMSMailService>();
            #endregion

            #region Forum
            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ISponsorshipService, SponsorshipService>();
            #endregion

            #region Event DI
            services.AddScoped<IEventInfoService, EventInfoService>();
            services.AddScoped<IEventRegisterService, EventRegisterService>();
            services.AddScoped<IEventParticipantHeadService, EventParticipantHeadService>();
            services.AddScoped<IEventCategoryService, EventCategoryService>();
            services.AddScoped<IPaymentHeadService, PaymentHeadService>();
            services.AddScoped<IParticipantHeadService, ParticipantHeadService>();

            #endregion

            #region Fees DI
            services.AddScoped<IMemberFeesService, MemberFeesService>();
            services.AddScoped<IBalanceService, BalanceService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<ICollectionService, CollectionService>();
            services.AddScoped<ITrMasterService, TrMasterService>();
            #endregion

            #region NewsDI
            services.AddScoped<INewsInfoService, NewsInfoService>();
            #endregion

            #region Gallery DI
            services.AddScoped<IGalleryTitleService, GalleryTitleService>();
            services.AddScoped<IGalleryContentService, GalleryContentService>();
            #endregion

            #region Notice DI
            services.AddScoped<INoticeInfoService, NoticeInfoService>();
            services.AddScoped<INoticeAuthorService, NoticeAuthorService>();
            services.AddScoped<IRlnNoticeAuthorService, RlnNoticeAuthorService>();
            services.AddScoped<INoticeTypeService, NoticeTypeService>();
            #endregion

            #region PromotionAndTransfer
            services.AddScoped<IPromotionTransferEntryService, PromotionTransferEntryService>();
            #endregion

            #region Assignment
            services.AddScoped<IAssignmentService, AssignmentService>();
            #endregion

            #region ACR
            services.AddScoped<IACRService, ACRService>();
            services.AddScoped<IAcrInfoService, AcrInfoService>();
            #endregion

            #region Attendance
            services.AddScoped<IEmployeePunchCardInfoService, EmployeePunchCardInfoService>();
            services.AddScoped<IAttendanceProcessService, AttendanceProcessService>();
            services.AddScoped<IReports, ReportsService>();
            #endregion

            #region Movement
            services.AddScoped<IMovementService, MovementService>();
            #endregion

            #region Wages
            services.AddScoped<IWagesBankInfoService, WagesBankInfoService>();
            services.AddScoped<IWagesEmergencyContactService, WagesEmergencyContactService>();
            services.AddScoped<IWagesPersonalInfoService, WagesPersonalInfoService>();
            services.AddScoped<IWagesPhotographService, WagesPhotographService>();
            services.AddScoped<IWagesEmployeeInfoService, WagesAddressEducationPhotoService>();
            services.AddScoped<IWagesReferenceService, WagesReferenceService>();
            services.AddScoped<IWagesPunchCardInfoService, WagesPunchCardInfoService>();
            services.AddScoped<IWagesPriviousEmploymentService, WagesPriviousEmploymentService>();
            services.AddScoped<IWagesLeftDetailsService, WagesLeftDetailsService>();
            services.AddScoped<IWagesOTSetupMasterService, WagesOTSetupMasterService>();
            services.AddScoped<IWagesLeaveRegisterService, WagesLeaveRegisterService>();
            #endregion

            #region Recruitment
            services.AddScoped<IApplicationFormService, ApplicationFormService>();
            #endregion

            #region Leave
            services.AddScoped<ILeaveTypeService, LeaveTypeService>();
            services.AddScoped<ILeaveRegisterService, LeaveRegisterService>();
            services.AddScoped<ILeaveApprovalService, LeaveApprovalService>();
            services.AddScoped<ILeavePolicyService, LeavePolicyService>();
            services.AddScoped<ILeaveRouteService, LeaveRouteService>();
            services.AddScoped<ILeaveStatusLogService, LeaveStatusLogService>();
            #endregion

            #region Training

            services.AddScoped<ITrainingNameService, TrainingNameService>();
            services.AddScoped<ITrainingPlanService, TrainingPlanService>();
            services.AddScoped<ITrainerInfoService, TrainerInfoService>();
            services.AddScoped<ITrainingInfoService, TrainingInfoService>();
            services.AddScoped<ITrainingLaunchService, TrainingLaunchService>();
            services.AddScoped<ITrainingScheduleService, TrainingScheduleService>();
            services.AddScoped<ITrainingEvaluationQuestionTraineeService, TrainingEvaluationQuestionTraineeService>();
            services.AddScoped<ITrainingEvaluationQuestionTrainerService, TrainingEvaluationQuestionTrainerService>();
            services.AddScoped<ITrainingPlanDetailsService, TrainingPlanDetailsService>();
            services.AddScoped<IDailyAttendanceService, DailyAttendanceService>();
            services.AddScoped<ITraningExamService, TraningExamService>();
            services.AddScoped<IExamResultService, ExamResultService>();
            services.AddScoped<ITrainingMiscallenousService, TrainingMiscallenousService>();
            services.AddScoped<ITrainingDeliverablesService, TrainingDeliverablesService>();
            services.AddScoped<ITrainingInfoService, TrainingInfoService>();
            services.AddScoped<IEvaluationFectorsService, EvaluationFectorsService>();
            services.AddScoped<ITrainingInfoDetailsService, TrainingInfoDetailsService>();
            services.AddScoped<ITrainingOfferService, TrainingOfferService>();
            services.AddScoped<IRlnTrainingTrainerService, RlnTrainingTrainerService>();
            services.AddScoped<ITrainingCategoryService, TrainingCategoryService>();
            services.AddScoped<ITrainingAllocationService, TrainingAllocationService>();
            #endregion

            #region Training New 
            services.AddScoped<ITrainingNewService, TrainingNewService>();
            services.AddScoped<IEnrollTraineeService, EnrollTraineeService>();
            services.AddScoped<IResourcePersonService, ResourcePersonService>();
            services.AddScoped<ITrainingResourcePersonService, TrainingResourcePersonService>();

            #endregion

            #region Library
            services.AddScoped<IBookEntryService, BookEntryService>();
            services.AddScoped<IBorrowerInfoService, BorrowerInfoService>();
            #endregion

            #region Vehicle
            services.AddScoped<IVehicleInfoService, VehicleInfoService>();
            services.AddScoped<IDriverAssMajorOverhaulingService, DriverAssMajorOverhaulingService>();
            #endregion

            #region Punishment Charge
            services.AddScoped<ICPService, ChargePunishmentService>();
            #endregion

            #region Reward
            services.AddScoped<IRewardService, RewardService>();
            #endregion

            #region Travel
            services.AddScoped<ITravellerInfoService, TravellerInfoService>();
            services.AddScoped<ITravellingInfoService, TravellingInfoService>();
            services.AddScoped<ITravelMasterService, TravelMasterService>();
            services.AddScoped<ITravelStatusLogService, TravelStatusLogService>();
            services.AddScoped<ITravelRouteService, TravelRouteService>();
            #endregion

            #region DisciplineryInvestigaion
            services.AddScoped<IDisciplineInvestigation, DisciplineInvestigationService>();
            #endregion         

            #region RetirementAndTermination
            services.AddScoped<IPRLEntryService, PRLEntryService>();
            services.AddScoped<IResignInformationService, ResignInformationService>();
            #endregion

            #region Award and Publication
            services.AddScoped<IAwardPublicationService, AwardPublicationService>();
            #endregion

            #region Organogram
            services.AddScoped<IOrganizationPostService, OrganizationPostService>();
            services.AddScoped<IOrganizationTypeService, OrganizationTypeService>();
            services.AddScoped<IPostingEmploymentTypeService, PostingEmploymentTypeService>();
            #endregion

            #region custtomAuthSUpport
            services.AddScoped<ILoggedinUser, LoggedinUser>();
            #endregion
            #endregion

            #region VMS

            #region Agreement
            services.AddScoped<IVMSAgreementService, VMSAgreementService>();
            #endregion

            #region Requisition
            services.AddScoped<IVMSRequisitionService, VMSRequisitionService>();
            #endregion

            #region SOR
            services.AddScoped<ISORService, SORService>();
            #endregion

            #region Car Management
            services.AddScoped<ICarInfo, CarInfo>();
            services.AddScoped<IVMSVehicleInfoService, VMSVehicleInfoService>();
            services.AddScoped<IFuelStationInfoService, FuelStationInfoService>();
            #endregion

            #region Fuel Information
            services.AddScoped<IFuelInfoService, FuelInfoService>();
            #endregion

            #region Fuel Information
            services.AddScoped<IVehicleServiceHistoryService, VehicleServiceHistoryService>();
            services.AddScoped<IItemStoreInServiceCenterService, ItemStoreInServiceCenterService>();
            #endregion

            #region Master Data
            services.AddScoped<IDesignationService, DesignationService>();
            services.AddScoped<IVMSAddressService, VMSAddressService>();
            services.AddScoped<IVMSResourceService, VMSResourceService>();
            services.AddScoped<IVMSReportService, VMSReportService>();
            #endregion

            #region Inventory
            services.AddScoped<IPurchasePartsService, PurchasePartsService>();
            services.AddScoped<IPartsIssueService, PartsIssueService>();
            #endregion


            #endregion

            #region CRO
            services.AddScoped<IDistributeJobService, DistributeJobService>();
            #endregion

            #region Email Service
            services.AddTransient<IEmailSenderService, EmailSenderService>();
            #endregion

            #region Program

            services.AddScoped<IProgramImpactService, ProgramImpactService>();
            services.AddScoped<IProgramCategoryService, ProgramCategoryService>();
            services.AddScoped<IProgramMasterService, ProgramMasterService>();
            services.AddScoped<IProgramAttachmentService, ProgramAttachmentService>();
            services.AddScoped<IProgramAttendeeService, ProgramAttendeeService>();
            services.AddScoped<IProgramHeadlineService, ProgramHeadlineService>();
            services.AddScoped<IProgramMainCategoryService, ProgramMainCategoryService>();
            services.AddScoped<IProgramPeopleInDiscussionService, ProgramPeopleInDiscussionService>();
            services.AddScoped<IProgramSubjectService, ProgramSubjectService>();
            services.AddScoped<IProgramViewerService, ProgramViewerService>();
            services.AddScoped<IProgramAddressService, ProgramAddressService>();
            services.AddScoped<IProgramReportService, ProgramReportService>();
            services.AddScoped<IProgramPlanService, ProgramPlanService>();
            services.AddScoped<IProgramYearService, ProgramYearService>();
            services.AddScoped<IProgramVideoService, ProgramVideoService>();

            #endregion

            #region Purchase
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<IStoreService, StoreService>();
            services.AddScoped<ITransferService, TransferService>();
            services.AddScoped<IBillPaymentService, BillPaymentService>();

            #endregion

            #region Workflow
            services.AddScoped<IDocService, DocService>();
            services.AddScoped<IMeetingService, MeetingService>();
            services.AddScoped<ITaskManagementService, TaskManagementService>();

            #endregion

            #region Distribution
            #region Master Data
            services.AddScoped<ISalesLevelService, SalesLevelService>();
            services.AddScoped<ISalesTeamDeploymentService, SalesTeamDeploymentService>();
            services.AddScoped<IDistributorTypeService, DistributorTypeService>();
            services.AddScoped<IDisItemPriceFixingService, DisItemPriceFixingService>();
            #endregion
            #region Requisition
            services.AddScoped<IRequsitionMasterService, RequisitionMasterService>();
            services.AddScoped<IRequisitionDetailService, RequisitionDetailService>();
            #endregion
            #endregion

            #region HOSPITAL Management

            services.AddScoped<IHomeCareService, HomeCareService>();


            #endregion

            #region Message
            services.AddScoped<IMessageService, MessageService>();
            #endregion

            #region SEBA
            services.AddScoped<ISebaService, SebaService>();
            services.AddScoped<ISpecialSkillService, SpecialSkillService>();
            #endregion

            #region  Shagotom

            services.AddScoped<IVisitorEntryMasterService, VisitorEntryMasterService>();
            services.AddScoped<IVisitorInfoDetailsService, VisitorInfoDetailsService>();
            services.AddScoped<IEmployeeInfoServiceForShagotom, EmployeeInfoServiceForShagotom>();
            services.AddScoped<IIssueCardService, IssueCardService>();

            #endregion

            #region Production
            services.AddScoped<OPUSERP.Production.Services.AttachmentComment.Interfaces.IAttachmentCommentService, OPUSERP.Production.Services.AttachmentComment.AttachmentCommentService>();
            services.AddScoped<IProductionService, ProductionService>();
            services.AddScoped<IProductionRequisitionService, ProductionRequisitionService>();
            services.AddScoped<IProductionPlanService, ProductionPlanService>();
            services.AddScoped<IBOMService, BOMService>();
            services.AddScoped<IProductionPlanService, ProductionPlanService>();
            #endregion

            #region MemberTransferLogService
            services.AddScoped<IMemberTransferLogService, MemberTransferLogService>();
            #endregion       
            #region StoreDepartmentService
            services.AddScoped<IStoreDepartmentService, StoreDepartmentTypeService>();
            #endregion


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddJsonOptions(options =>
                {
                    var resolver = options.SerializerSettings.ContractResolver;
                    if (resolver != null)
                        (resolver as DefaultContractResolver).NamingStrategy = null;
                });

            services.AddSession(options =>
            {
                options.Cookie.Name = ".AdventureWorks.Session";
                options.IdleTimeout = TimeSpan.FromHours(24);
                options.Cookie.IsEssential = true;
            });

            services.AddHttpContextAccessor();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //app.Run(async (context) =>
            //    {
            //        await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            //    });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseStaticFiles();
            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
