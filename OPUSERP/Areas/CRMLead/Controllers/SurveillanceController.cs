using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class SurveillanceController : Controller
    {
        private readonly IAgreementService agreementService;
        private readonly ILeadsService leadsService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProposalService proposalService;
        private readonly IContactsService contactsService; 
        private readonly IAgreementCategoryService  agreementCategoryService;
        private readonly IUserInfoes userInfoes;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IEmailSenderService emailSenderService;
        private readonly IBillCollectionService billCollectionService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public SurveillanceController(IAgreementService agreementService,IBillCollectionService billCollectionService, IPersonalInfoService personalInfoService, IEmailSenderService emailSenderService, ILeadsService leadsService, IAttachmentCommentService attachmentCommentService, IHostingEnvironment hostingEnvironment, IProposalService proposalService, IContactsService contactsService, IAgreementCategoryService agreementCategoryService, IUserInfoes userInfoes,  IConverter converter, IERPCompanyService eRPCompanyService)
       
        {
            this.agreementService = agreementService;
            this.leadsService = leadsService;
            this.attachmentCommentService = attachmentCommentService;
            this._hostingEnvironment = hostingEnvironment;
            this.proposalService = proposalService;
            this.contactsService = contactsService;
            this.agreementCategoryService = agreementCategoryService;
            this.userInfoes = userInfoes;
            this.eRPCompanyService = eRPCompanyService;
            this.personalInfoService = personalInfoService;
            this.emailSenderService = emailSenderService;
            this.billCollectionService = billCollectionService;

            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }



        #region Surveillance Propose
        public IActionResult Index()
        {
            string Owner = HttpContext.User.Identity.Name;
            AgreementViewModel model = new AgreementViewModel()
            {
                //agreements = await agreementService.GetAgreementbyOwner(Owner),

            };

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> GetAgreementDetailsbyRatingDate(string FDate, string TDate)
        {
            string Owner = HttpContext.User.Identity.Name;
            return Json(await agreementService.GetAgreementDetailsbyRatingDate(FDate, TDate, Owner));
        }
        [HttpGet]
        public async Task<IActionResult> GetAgreementDetailsbyRatingDateS(string FDate, string TDate, string TeamLeader, string Fa, string BD, string LeadId)
        {
            string Owner = HttpContext.User.Identity.Name;
      
            if (TeamLeader == "NoData")
            {
                TeamLeader = "";
            }
            if (Fa == "NoData")
            {
                Fa = "";
            }
            if (BD == "NoData")
            {
                BD = "";
            }
            if (LeadId == "NoData")
            {
                LeadId = "";
            }
            var data = await agreementService.GetAgreementDetailsbyRatingDatefilter(FDate, TDate, Owner,TeamLeader,Fa,BD,LeadId);
          
            return Json(data);
        }
        #endregion
        #region Surveillance Proposal Edit
        public async Task<IActionResult> SurveillanceProposal(int? id, string FDate, string TDate)
        {
            var purchase = await agreementService.GetAllAgreement();
            int Cpurchase = 0;
            Cpurchase = purchase.Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            var leadId = 0;

            if (Convert.ToInt32(id) != 0)
            {
                var data = await agreementService.GetAgreementById(Convert.ToInt32(id));
                agreementNo = data.agreementNo;
                leadId =Convert.ToInt32(data.leadsId);
            }
            ViewBag.agreementNo = agreementNo;
            var billcollection = await billCollectionService.GetBillCollectionbyLeadId(leadId);
            var agreementdetails = await agreementService.GetAgreementDetailsbyAgreementId((int)id);
            ViewBag.totalcollection = billcollection.Where(x => x.billGenerate.agreementDetailss.Id == agreementdetails.Where(y => y.isProposed == 2).LastOrDefault().Id).Sum(z => z?.totalAmount);
            SurveillanceViewModel model = new SurveillanceViewModel()
            {
                agreements = await agreementService.GetAgreementbyLeadId(Convert.ToInt32(leadId)),
                agreementTypes = await agreementService.GetAllAgreementType(),
                products = await proposalService.GetAllProduct(),
                agreementCategories = await agreementCategoryService.GetAllAgreementCategory(),
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(leadId)),
                aspNetUsers = await userInfoes.GetUserInfoByModule(2),
                ratingYears = await agreementService.GetAllRatingYear(),
                vatCategories = await agreementService.GetAllVatCategory(),
                taxCategories = await agreementService.GetAllTaxCategory(),
                aspNetUsersViewModels = await userInfoes.GetAllActiveEmployeeInfo(),
            };
            //ViewBag.leadName = leadName;
            ViewBag.masterId = id;
            ViewBag.leadId = leadId;
            ViewBag.fdate = FDate;
            ViewBag.tdate = TDate;
            return View(model);
        }
      
        [HttpPost]
        public async Task<JsonResult> EditSurveillanceProposal([FromForm] SurveillanceViewModel model)
        {
            AgreementDetails Details = new AgreementDetails();

            Details.Id = Convert.ToInt32(model.agreementDetailId) ;
            Details.agreementId = Convert.ToInt32(model.agreementId);
            Details.ratingYearId = Convert.ToInt32(model.ratingYear);
            Details.vatCategoryId = Convert.ToInt32(model.vatType);
            Details.taxCategoryId = Convert.ToInt32(model.taxType);
            Details.ratingDate = model.rDate;
            Details.agreementAmount = model.agreementAmounts;
            Details.ratingAmount = model.ratingAmounts;
            Details.vatAmount = model.vatAmounts;
            Details.taxAmount = model.taxAmounts;
            Details.totalAmount = model.totalAmounts;
            Details.isProposed = model.isProposed;
            Details.remarks = model.finremarks;
            Details.revremarks = model.revremarks;

          int aggrementdetailid=  await agreementService.SaveAgreementDetails(Details);
            Details = new AgreementDetails();
                    

                
            #region Save History

            string actDetailss = string.Empty;
            var models = await agreementService.GetAgreementDetailsByaggId(model.agreementId);
            actDetailss = "Agreement " + models.agreement.agreementNo + " for rating year " + models.ratingYear.ratingYearName + " is proposed.";

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(models.agreement.leadsId),
                actionName = "Agreement",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return Json(1);
        }

        public async Task<IActionResult> SurveillanceProposalReviewed(int? id, string FDate, string TDate)
        {
            var purchase = await agreementService.GetAllAgreement();
            int Cpurchase = 0;
            Cpurchase = purchase.Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            var leadId = 0;

            if (Convert.ToInt32(id) != 0)
            {
                var data = await agreementService.GetAgreementById(Convert.ToInt32(id));
                agreementNo = data.agreementNo;
                leadId = Convert.ToInt32(data.leadsId);
            }
            ViewBag.agreementNo = agreementNo;
            var billcollection = await billCollectionService.GetBillCollectionbyLeadId(leadId);
            var agreementdetails = await agreementService.GetAgreementDetailsbyAgreementId((int)id);
            ViewBag.totalcollection = billcollection.Where(x => x.billGenerate.agreementDetailss.Id == agreementdetails.Where(y => y.isProposed == 2).LastOrDefault().Id).Sum(z=>z?.totalAmount);
            SurveillanceViewModel model = new SurveillanceViewModel()
            {
                agreements = await agreementService.GetAgreementbyLeadId(Convert.ToInt32(leadId)),
                agreementTypes = await agreementService.GetAllAgreementType(),
                products = await proposalService.GetAllProduct(),
                agreementCategories = await agreementCategoryService.GetAllAgreementCategory(),
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(leadId)),
                aspNetUsers = await userInfoes.GetUserInfoByModule(2),
                ratingYears = await agreementService.GetAllRatingYear(),
                vatCategories = await agreementService.GetAllVatCategory(),
                taxCategories = await agreementService.GetAllTaxCategory(),
                aspNetUsersViewModels = await userInfoes.GetAllActiveEmployeeInfo(),
            };
            //ViewBag.leadName = leadName;
            ViewBag.masterId = id;
            ViewBag.leadId = leadId;
            ViewBag.fdate = FDate;
            ViewBag.tdate = TDate;
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> EditSurveillanceProposalReviewed([FromForm] SurveillanceViewModel model)
        {
            AgreementDetails Details = new AgreementDetails();

            Details.Id = Convert.ToInt32(model.agreementDetailId);
            Details.agreementId = Convert.ToInt32(model.agreementId);
            Details.ratingYearId = Convert.ToInt32(model.ratingYear);
            Details.vatCategoryId = Convert.ToInt32(model.vatType);
            Details.taxCategoryId = Convert.ToInt32(model.taxType);
            Details.ratingDate = model.rDate;
            Details.agreementAmount = model.agreementAmounts;
            Details.ratingAmount = model.ratingAmounts;
            Details.vatAmount = model.vatAmounts;
            Details.taxAmount = model.taxAmounts;
            Details.totalAmount = model.totalAmounts;
            Details.isProposed = model.isProposed;

            int aggrementdetailid = await agreementService.SaveAgreementDetails(Details);
            Details = new AgreementDetails();



            #region Save History

            string actDetailss = string.Empty;
            var models = await agreementService.GetAgreementDetailsByaggId(model.agreementId);
            actDetailss = "Agreement " + models.agreement.agreementNo + " for rating year " + models.ratingYear.ratingYearName + " is created.";

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(models.agreement.leadsId),
                actionName = "Agreement",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return Json(1);
        }
        [HttpGet]
        public async Task<IActionResult> GetAgreementDetailsbyAgreementIdRatingDate(int AgreementId, string FDate, string TDate)
        {
            return Json(await agreementService.GetAgreementDetailsbyAgreementIdRatingDate(AgreementId, FDate, TDate));
        }
        [HttpPost]
        public async Task<JsonResult> UpdateAgreementDetailsPropose(int Id, int statusId,string remarks)
        {
            try
            {
                var aggrementDetails = await agreementService.GetAgreementDetailsById(Id);
                await agreementService.UpdateAgreementDetailsPropose(Id, statusId, remarks);
                #region Save History
                string actDetailss = string.Empty;
                var model = await agreementService.GetAgreementDetailsById(Id);

                actDetailss = "Agreement " + model.agreement.agreementNo + " for rating year " + model.ratingYear.ratingYearName + " is proposed.";
                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = Convert.ToInt32(model.agreement.leadsId),
                    actionName = "Agreement",
                    actionDetails = actDetailss
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion
                //return Json(true);
                //await agreementService.UpdateAgreementDetailsPropose(Id, statusId, remarks);

                string html = "<div><strong>Propose Mail.</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is proposed for your verification."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > " + aggrementDetails.agreement.leads.leadOwner + " </p></div>"
                        + " <br/>";

                if (aggrementDetails.agreement.leads.leadOwner != null)
                {
                    await emailSenderService.SendEmailWithFrom("lubna@emergingrating.com", aggrementDetails.agreement.leads.leadOwner, "Propose for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                    await emailSenderService.SendEmailWithFrom("almasud@emergingrating.com", aggrementDetails.agreement.leads.leadOwner, "Propose for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                }

                return Json(true);
            }
            catch(Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateAgreementDetailsProposePre(int Id, int statusId, string remarks)
        {
            try
            {
                var aggrementDetails = await agreementService.GetAgreementDetailsById(Id);
                await agreementService.UpdateAgreementDetailsPropose(Id, statusId, remarks);
                #region Save History
                string actDetailss = string.Empty;
                var model = await agreementService.GetAgreementDetailsById(Id);

                actDetailss = "Agreement " + model.agreement.agreementNo + " for rating year " + model.ratingYear.ratingYearName + " is proposed.";
                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = Convert.ToInt32(model.agreement.leadsId),
                    actionName = "Agreement",
                    actionDetails = actDetailss
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion
                //return Json(true);
                //await agreementService.UpdateAgreementDetailsPropose(Id, statusId, remarks);

                string html = "<div><strong>Review Mail.</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is send for review for "+remarks+"."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > " + aggrementDetails.agreement.leads.leadOwner + " </p></div>"
                        + " <br/>";

                if (aggrementDetails.agreement.leads.leadOwner != null)
                {
                    string mail = HttpContext.User.Identity.Name;
                    if (mail == "lubna@emergingrating.com")
                    {
                        await emailSenderService.SendEmailWithFrom(model.agreement.leads.leadOwner, "lubna@emergingrating.com", "Review  " + model.ratingYear.ratingTypeName + " of lead : " + model.agreement.leads.leadName, html);
                        await emailSenderService.SendEmailWithFrom("almasud@emergingrating.com", "lubna@emergingrating.com", "Review  " + model.ratingYear.ratingTypeName + " of lead : " + model.agreement.leads.leadName, html);
                    }
                    else
                    {
                        await emailSenderService.SendEmailWithFrom(model.agreement.leads.leadOwner, "almasud@emergingrating.com", "Review  " + model.ratingYear.ratingTypeName + " of lead : " + model.agreement.leads.leadName, html);
                        await emailSenderService.SendEmailWithFrom("lubna@emergingrating.com", "almasud@emergingrating.com", "Review  " + model.ratingYear.ratingTypeName + " of lead : " + model.agreement.leads.leadName, html);
                    }
                }

                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }

        [HttpPost]
        public async Task<JsonResult> UpdateAgreementDetailsReviewed(int Id, int statusId, string remarks)
        {
            try
            {
                var aggrementDetails = await agreementService.GetAgreementDetailsById(Id);
                await agreementService.UpdateAgreementDetailsProposePro(Id, statusId, remarks);
                #region Save History
                string actDetailss = string.Empty;
                var model = await agreementService.GetAgreementDetailsById(Id);

                actDetailss = "Agreement " + model.agreement.agreementNo + " for rating year " + model.ratingYear.ratingYearName + " is proposed.";
                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = Convert.ToInt32(model.agreement.leadsId),
                    actionName = "Agreement",
                    actionDetails = actDetailss
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion
                //return Json(true);
                //await agreementService.UpdateAgreementDetailsPropose(Id, statusId, remarks);

                string html = "<div><strong>Reviewed Mail.</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is reviewed and send for your verification."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > " + aggrementDetails.agreement.leads.leadOwner + " </p></div>"
                        + " <br/>";

                if (aggrementDetails.agreement.leads.leadOwner != null)
                {
                    await emailSenderService.SendEmailWithFrom("lubna@emergingrating.com", aggrementDetails.agreement.leads.leadOwner, "Reviewed for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                    await emailSenderService.SendEmailWithFrom("almasud@emergingrating.com", aggrementDetails.agreement.leads.leadOwner, "Reviewed for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                }

                return Json(true);
            }
            catch (Exception ex)
            {
                return Json(ex);
            }
        }


        #endregion

        #region Surveillance Approval List
        public IActionResult SurveillanceApprovalList()
        {
            string Owner = HttpContext.User.Identity.Name;
            AgreementViewModel model = new AgreementViewModel()
            {
                //agreements = await agreementService.GetAgreementbyOwner(Owner),

            };

            return View(model);
        }
        public async Task<IActionResult> ApprovedSurveillance()
        {
            string Owner = HttpContext.User.Identity.Name;
            AgreementViewModel model = new AgreementViewModel()
            {
                agreementDetails = await agreementService.GetApprovedSurveillencebyOwner(Owner),

            };

            return View(model);
        }

        public async Task<IActionResult> ReviewedSurveillance()
        {
            string Owner = HttpContext.User.Identity.Name;
            AgreementViewModel model = new AgreementViewModel()
            {
                agreementDetails = await agreementService.GetApprovedSurveillencebyOwner(Owner),

            };

            return View(model);
        }

        #endregion
        #region Surveillance Proposal Approve 
        public async Task<IActionResult> SurveillanceProposalApprove(int? id, string FDate, string TDate)
        {
            var purchase = await agreementService.GetAllAgreement();
            int Cpurchase = 0;
            Cpurchase = purchase.Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            var leadId = 0;

            if (Convert.ToInt32(id) != 0)
            {
                var data = await agreementService.GetAgreementById(Convert.ToInt32(id));
                agreementNo = data.agreementNo;
                leadId = Convert.ToInt32(data.leadsId);
            }
            ViewBag.agreementNo = agreementNo;
            var billcollection = await billCollectionService.GetBillCollectionbyLeadId(leadId);
            var agreementdetails = await agreementService.GetAgreementDetailsbyAgreementId((int)id);
            ViewBag.totalcollection = billcollection.Where(x => x.billGenerate.agreementDetailss.Id == agreementdetails.Where(y => y.isProposed == 2).LastOrDefault().Id).Sum(z => z?.totalAmount);
            SurveillanceViewModel model = new SurveillanceViewModel()
            {
                agreements = await agreementService.GetAgreementbyLeadId(Convert.ToInt32(leadId)),
                agreementTypes = await agreementService.GetAllAgreementType(),
                products = await proposalService.GetAllProduct(),
                agreementCategories = await agreementCategoryService.GetAllAgreementCategory(),
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(leadId)),
                aspNetUsers = await userInfoes.GetUserInfoByModule(2),
                ratingYears = await agreementService.GetAllRatingYear(),
                vatCategories = await agreementService.GetAllVatCategory(),
                taxCategories = await agreementService.GetAllTaxCategory(),
                aspNetUsersViewModels = await userInfoes.GetAllActiveEmployeeInfo(),
            };
            //ViewBag.leadName = leadName;
            ViewBag.masterId = id;
            ViewBag.leadId = leadId;
            ViewBag.fdate = FDate;
            ViewBag.tdate = TDate;
            return View(model);
        }
        public async Task<IActionResult> SurveillanceProposalApproved(int? id, string FDate, string TDate)
        {
            var purchase = await agreementService.GetAllAgreement();
            int Cpurchase = 0;
            Cpurchase = purchase.Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            var leadId = 0;

            if (Convert.ToInt32(id) != 0)
            {
                var data = await agreementService.GetAgreementById(Convert.ToInt32(id));
                agreementNo = data.agreementNo;
                leadId = Convert.ToInt32(data.leadsId);
            }
            ViewBag.agreementNo = agreementNo;

            SurveillanceViewModel model = new SurveillanceViewModel()
            {
                agreements = await agreementService.GetAgreementbyLeadId(Convert.ToInt32(leadId)),
                agreementTypes = await agreementService.GetAllAgreementType(),
                products = await proposalService.GetAllProduct(),
                agreementCategories = await agreementCategoryService.GetAllAgreementCategory(),
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(leadId)),
                aspNetUsers = await userInfoes.GetUserInfoByModule(2),
                ratingYears = await agreementService.GetAllRatingYear(),
                vatCategories = await agreementService.GetAllVatCategory(),
                taxCategories = await agreementService.GetAllTaxCategory(),
                aspNetUsersViewModels = await userInfoes.GetAllActiveEmployeeInfo(),
            };
            //ViewBag.leadName = leadName;
            ViewBag.masterId = id;
            ViewBag.leadId = leadId;
            ViewBag.fdate = FDate;
            ViewBag.tdate = TDate;
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateAgreementDetailsVerify(int Id, int statusId)
        {
            await agreementService.UpdateAgreementDetailsPropose(Id, statusId,"");
            ApprovedforCro data = new ApprovedforCro
            {
                Id = 0,
                agreementDetailsId = Id,
                approvedDate = DateTime.Now,
                isRated = 0,

            };
            await agreementService.SaveApprovedforCro(data);
         
            #region Save History
            string actDetailss = string.Empty;
            var model = await agreementService.GetAgreementDetailsById(Id);

            actDetailss = "Agreement " + model.agreement.agreementNo + " for rating year " + model.ratingYear.ratingYearName + " is approved.";
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(model.agreement.leadsId),
                actionName = "Agreement",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion
            //return Json(true);
            //await agreementService.UpdateAgreementDetailsPropose(Id, statusId, remarks);

            string html = "<div><strong>Approval Mail.</strong></div>"
                    + " <br/>"
                    + "<p>Dear Sir,</p>"
                    + " <br/>"
                    + " This is to inform you that " + model.ratingYear.ratingTypeName + " of lead : " + model.agreement.leads.leadName + " is verified."
                    + "<br/>"
                    + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' > " + model.agreement.leads.leadOwner + " </p></div>"
                    + " <br/>";

            if (model.agreement.leads.leadOwner != null)
            {
                string mail = HttpContext.User.Identity.Name;
                if (mail == "lubna@emergingrating.com")
                {
                    await emailSenderService.SendEmailWithFrom(model.agreement.leads.leadOwner, "lubna@emergingrating.com", "Verified for " + model.ratingYear.ratingTypeName + " of lead : " + model.agreement.leads.leadName, html);
                    await emailSenderService.SendEmailWithFrom("almasud@emergingrating.com", "lubna@emergingrating.com", "Verified for " + model.ratingYear.ratingTypeName + " of lead : " + model.agreement.leads.leadName, html);
                }
                else
                {
                    await emailSenderService.SendEmailWithFrom(model.agreement.leads.leadOwner, "almasud@emergingrating.com", "Verified for " + model.ratingYear.ratingTypeName + " of lead : " + model.agreement.leads.leadName, html);
                    await emailSenderService.SendEmailWithFrom("lubna@emergingrating.com", "almasud@emergingrating.com", "Verified for " + model.ratingYear.ratingTypeName + " of lead : " + model.agreement.leads.leadName, html);
                }
               
            }

            return Json(true);
        }
        [HttpGet]
        public async Task<IActionResult> GetAgreementDetailsbyRatingDateSApproval(string FDate, string TDate)
        {
            string Owner = HttpContext.User.Identity.Name;
            return Json(await agreementService.GetAgreementDetailsbyRatingDateSApproval(FDate, TDate, Owner));
        }
        [HttpGet]
        public async Task<IActionResult> GetApprovedSurveillancebyRatingDate(DateTime? FDate, DateTime? TDate)
        {
            string Owner = HttpContext.User.Identity.Name;
            return Json(await agreementService.GetApprovedSurveillencebyOwnerDate(Owner,FDate, TDate));
        }
        [HttpGet]
        public async Task<IActionResult> GetReviewedSurveillancebyRatingDate(DateTime? FDate, DateTime? TDate)
        {
            string Owner = HttpContext.User.Identity.Name;
            return Json(await agreementService.GetReviewedSurveillencebyOwnerDate(Owner, FDate, TDate));
        }
        [HttpGet]
        public async Task<IActionResult> GetApprovedSurveillancebyOwner()
        {
            string Owner = HttpContext.User.Identity.Name;
            return Json(await agreementService.GetApprovedSurveillencebyOwner(Owner));
        }
        #endregion


    }
}