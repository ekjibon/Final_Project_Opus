using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.Areas.MasterData.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.Client.Interfaces;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.EmailService.interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMLead.Controllers
{
    [Area("CRMLead")]
    public class AgreementController : Controller
    {
        private IMemoryCache _cache;
        private readonly IAgreementService agreementService;
        private readonly ILeadsService leadsService;
        private readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IProposalService proposalService;
        private readonly IContactsService contactsService;
        private readonly IAgreementCategoryService agreementCategoryService;
        private readonly IUserInfoes userInfoes;
        private readonly IEmailSenderService emailSenderService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly IRatingCategoryService ratingCategoryService;
        private readonly IAddressesService addressesService;
        private readonly IBankBranchService bankBranchService;
        private readonly IPersonalInfoService employeeInfoService;
        private readonly IClientService clientService;
        private ERPDbContext _db;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public AgreementController(IAgreementService agreementService, IMemoryCache _cache, IClientService clientService, IPersonalInfoService employeeInfoService, IEmailSenderService emailSenderService, IBankBranchService bankBranchService, IAddressesService addressesService, ERPDbContext db, IRatingCategoryService ratingCategoryService, ILeadsService leadsService, IAttachmentCommentService attachmentCommentService, IHostingEnvironment hostingEnvironment, IProposalService proposalService, IContactsService contactsService, IAgreementCategoryService agreementCategoryService, IUserInfoes userInfoes, IConverter converter, IERPCompanyService eRPCompanyService)
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
            this.ratingCategoryService = ratingCategoryService;
            this.addressesService = addressesService;
            this.bankBranchService = bankBranchService;
            this.employeeInfoService = employeeInfoService;
            this.emailSenderService = emailSenderService;
            this.clientService = clientService;
            this._cache = _cache;
            _db = db;
            //For PDF
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        #region Lead Agreement New

        public async Task<IActionResult> Index(int? id, string leadName)
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
            var agreementTypes = await agreementService.GetAllAgreementType();
            if (agreementTypes == null)
            {
                agreementTypes = new List<AgreementType>();
            }
            AgreementViewModel model = new AgreementViewModel()
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
                ratingCategories = await ratingCategoryService.GetAllRatingCategory(),
                employeeInfos = await employeeInfoService.GetEmployeeInfoList()
            };
            ViewBag.leadName = leadName;
            ViewBag.masterId = id;
            ViewBag.leadId = leadId;
            return View(model);
        }

        // POST: Agreement Master/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] AgreementViewModel model)
        {
            var masterId = 0;
            //if (!ModelState.IsValid)
            //{
            //    model.agreements = await agreementService.GetAgreementbyLeadId(Convert.ToInt32(model.leadsId));
            //    model.agreementTypes = await agreementService.GetAllAgreementType();
            //    ViewBag.leadName = model.leadName;
            //    ViewBag.leadId = Convert.ToInt32(model.leadsId);
            //    return View(model);
            //}
            var purchase = await agreementService.GetAllAgreement();
            int Cpurchase = 0;
            Cpurchase = purchase.Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            if (model.agreementId > 0)
            {
                agreementNo = model.agreementNo;
            }
            Agreement data = new Agreement
            {
                Id = model.agreementId,
                agreementTypeId = model.agreementTypeId,
                leadsId = model.leadsId,
                agreementNo = agreementNo,
                agreementOwner = HttpContext.User.Identity.Name,
                agreementDate = model.agreementDate,
                sendingDate = model.sendingDate,
                signingDate = model.signingDate,
                expiredDate = model.expiredDate,
                agreementCategoryId = model.agreementCategoryId,
                noOfReports = model.noOfReports,
                expectedReportDate = model.expectedReportDate,
                loanLimitAmount = model.loanLimitAmount,
                loanLimitUnit = model.loanLimitUnit,
                priority = model.priority,
                remarks = model.remarks,
                companySignatory = model.companySignatory,
                companyWitness = model.companyWitness,
                contactSignatoryId = model.contactSignatoryId,
                contactWitnessId = model.contactWitnessId,
                agreementStatusId = model.agreementStatusId,
                ratingCategoryId = model.ratingCategoryTypeId

            };

            masterId = await agreementService.SaveAgreement(data);

            if (model.ratingYearId != null)
            {
                if (model.ratingYearId.Length > 0)
                {
                    int AgreementDetailId = 0;
                    for (int i = 0; i < model.ratingYearId.Length; i++)
                    {
                        if (model.agreementId > 0)
                        {
                            var AgreementDetails = await agreementService.GetAgreementDetailsbyAgreementRatingYearId(Convert.ToInt32(masterId), Convert.ToInt32(model.ratingYearId[i]));
                            if (AgreementDetails.Count() > 0)
                            {
                                AgreementDetailId = AgreementDetails.FirstOrDefault().Id;
                            }
                            else
                            {
                                AgreementDetailId = 0;
                            }
                        }
                        AgreementDetails Details = new AgreementDetails();

                        Details.Id = AgreementDetailId;
                        Details.agreementId = Convert.ToInt32(masterId);
                        Details.ratingYearId = Convert.ToInt32(model.ratingYearId[i]);
                        Details.vatCategoryId = Convert.ToInt32(model.vatCategoryId[i]);
                        Details.taxCategoryId = Convert.ToInt32(model.taxCategoryId[i]);
                        Details.ratingDate = model.ratingDate[i];
                        Details.agreementAmount = model.agreementAmount[i];
                        Details.ratingAmount = model.ratingAmount[i];
                        Details.vatAmount = model.vatAmount[i];
                        Details.taxAmount = model.taxAmount[i];
                        Details.totalAmount = model.totalAmount[i];
                        Details.isProposed = model.isProposed[i];

                        await agreementService.SaveAgreementDetails(Details);
                        Details = new AgreementDetails();
                    }

                }
            }

            #region Save History
            string actDetailss = string.Empty;
            if (model.agreementId == 0)
            {
                actDetailss = "Agreement " + model.agreementNo + " is Created.";
            }
            else
            {
                actDetailss = "Agreement " + model.agreementNo + " is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(model.leadsId),
                actionName = "Agreement",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            return RedirectToAction(nameof(AgreementList));
            //return RedirectToAction(nameof(Index), new
            //{
            //    id = Convert.ToInt32(model.leadsId),
            //    leadName = model.leadName
            //});
        }

        [HttpGet]
        public async Task<IActionResult> GetRelProductAgreementByagreementId(int agreementId)
        {
            return Json(await agreementService.GetRelProductAgreementByagreementId(agreementId));
        }

        [HttpGet]
        public async Task<IActionResult> GetAgreementbyLeadId(int masterId)
        {
            return Json(await agreementService.GetAgreementbyLeadId(masterId));
        }
        [HttpGet]
        public async Task<IActionResult> GetAllContactsByLeadsId(int LeadsId)
        {
            return Json(await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(LeadsId)));
        }
        [HttpGet]
        public async Task<IActionResult> GetAgreementDetailsbyAgreementId(int AgreementId)
        {
            return Json(await agreementService.GetAgreementDetailsbyAgreementId(Convert.ToInt32(AgreementId)));
        }
        [HttpGet]
        public async Task<IActionResult> GetAgreementDetailsbyAgreementIdpre(int AgreementId)
        {
            var agreementlist = await agreementService.GetAgreementbyLeadId(AgreementId);
            int pram = 0;
            if (agreementlist.Count()>0)
            {
                pram = agreementlist.Where(x => x.agreementType?.agreementTypeName != "ReAgreement").LastOrDefault().Id;
            }

            var agreementdetailspre = await agreementService.GetAgreementDetailsbyAgreementId(pram);
            return Json(agreementdetailspre);
        }
        [HttpGet]
        public async Task<IActionResult> GetAgreementByAgreementId(int AgreementId)
        {
            return Json(await agreementService.GetAgreementByagreementId(Convert.ToInt32(AgreementId)));
        }
        [HttpGet]
        public async Task<ActionResult> UpdateForAgreementVerification(int Id, int statusId)
        {

            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfoes.GetUserInfoByUser(userName);
                var EmpInfo = await employeeInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
                int empId = 0;
                string email = "";
                string name = "";
                if (EmpInfo != null)
                {
                    empId = EmpInfo.Id;
                    email = EmpInfo.emailAddress;
                    name = EmpInfo.nameEnglish;
                }

                var aggrementDetails = await agreementService.GetAgreementDetailsByaggId(Id);
                await agreementService.UpdateForAgreementVerification(Id, statusId, "");

                #region Save History
                string actDetailss = string.Empty;
                var model = await agreementService.GetAgreementDetailsByaggId(Id);

                actDetailss = "Agreement " + model.agreement.agreementNo + " for rating year " + model.ratingYear.ratingYearName + " is verified.";
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

                string html = "<div><strong>Requested to verify</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is requested to verify."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' >  " + aggrementDetails.agreement.leads.leadOwner + " </p></div>"
                        + " <br/>";

                if (email != null)
                {
                    await emailSenderService.SendEmailWithFrom("lubna@emergingrating.com", aggrementDetails.agreement.leads.leadOwner, "Requested to verify for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                    await emailSenderService.SendEmailWithFrom("almasud@emergingrating.com", aggrementDetails.agreement.leads.leadOwner, "Requested to verify for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                }
                return RedirectToAction("AgreementList");
            }
            catch (Exception)
            {
                return RedirectToAction("AgreementList");
            }

        }
        [HttpGet]
        public async Task<ActionResult> UpdateForAgreementVerificationLead(int Id, int statusId, int leadId, string leadName)
        {

            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfoes.GetUserInfoByUser(userName);
                var EmpInfo = await employeeInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
                int empId = 0;
                string email = "";
                string name = "";
                if (EmpInfo != null)
                {
                    empId = EmpInfo.Id;
                    email = EmpInfo.emailAddress;
                    name = EmpInfo.nameEnglish;
                }

                var aggrementDetails = await agreementService.GetAgreementDetailsByaggId(Id);
                await agreementService.UpdateForAgreementVerification(Id, statusId, "");
                #region Save History
                string actDetailss = string.Empty;
               // var model = await agreementService.GetAgreementDetailsByaggId(Id);

                actDetailss = "Agreement " + aggrementDetails.agreement.agreementNo + " for rating year " + aggrementDetails.ratingYear.ratingYearName + " is verified.";
                LeadsHistory leadsData = new LeadsHistory
                {
                    Id = 0,
                    leadsId = Convert.ToInt32(aggrementDetails.agreement.leadsId),
                    actionName = "Agreement",
                    actionDetails = actDetailss
                };
                await leadsService.SaveLeadsHistory(leadsData);
                #endregion
                #region updateclient
               
                if (aggrementDetails.agreement.agreementTypeId != 1)
                {
                    var client = await clientService.GetClientsByLeadId((int)aggrementDetails.agreement.leadsId);
                    if (client != null)
                    {
                        await clientService.UpdateClient(client.Id);
                    }
                }

                #endregion
                //return Json(true);
                //await agreementService.UpdateAgreementDetailsPropose(Id, statusId, remarks);

                string html = "<div><strong>Requested to verify</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is requested to verify."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' >  " + aggrementDetails.agreement.leads.leadOwner + " </p></div>"
                        + " <br/>";

                if (email != null)
                {
                    await emailSenderService.SendEmailWithFrom("lubna@emergingrating.com", aggrementDetails.agreement.leads.leadOwner, "Requested to verify for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                    await emailSenderService.SendEmailWithFrom("almasud@emergingrating.com", aggrementDetails.agreement.leads.leadOwner, "Requested to verify for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                }

                return RedirectToAction(nameof(AgreementListForLead), new
                {
                    id = Convert.ToInt32(leadId),
                    leadName = leadName
                });
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(AgreementListForLead), new
                {
                    id = Convert.ToInt32(leadId),
                    leadName = leadName
                });
            }

        }
        [HttpGet]
        public async Task<ActionResult> UpdateForAgreementVerificationClient(int Id, int statusId, int leadId, string leadName)
        {
            await agreementService.UpdateForAgreementVerification(Id, statusId, "");
            return RedirectToAction(nameof(AgreementListForClient), new
            {
                id = Convert.ToInt32(leadId),
                leadName = leadName
            });

        }
        [HttpGet]
        public async Task<ActionResult> UpdateForAgreementVerify(int Id, int statusId, string remarks)
        {
            var verify = await agreementService.GetAgreementDetailsbyAgreementId(Id);
            if (verify.Count() > 0)
            {
                ApprovedforCro data = new ApprovedforCro
                {
                    Id = 0,
                    agreementDetailsId = verify.FirstOrDefault().Id,
                    approvedDate = DateTime.Now,
                    isRated = 0,

                };
                await agreementService.SaveApprovedforCro(data);
                await agreementService.UpdateAgreementDetailsPropose(verify.FirstOrDefault().Id, 2, "");
            }

            await agreementService.UpdateForAgreementVerification(Id, statusId, remarks);

            var datag = await agreementService.GetAgreementById(Id);
            string actDetailss = string.Empty;
            actDetailss = "Agreement " + datag.agreementNo + " is verified.";
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(datag.leadsId),
                actionName = "Agreement",
                actionDetails = actDetailss
            };

            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfoes.GetUserInfoByUser(userName);
                var EmpInfo = await employeeInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
                int empId = 0;
                string email = "";
                string name = "";
                if (EmpInfo != null)
                {
                    empId = EmpInfo.Id;
                    email = EmpInfo.emailAddress;
                    name = EmpInfo.nameEnglish;
                }

                var aggrementDetails = await agreementService.GetAgreementDetailsByaggId(Id);

                string html = "<div><strong>Aggrement verified</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is verified."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' >  " + name + " </p></div>"
                        + " <br/>";

                if (email != null)
                {
                    await emailSenderService.SendEmailWithFrom(aggrementDetails.agreement.leads.leadOwner, email, "verified for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("AgreementVerificationList");
            }

            return RedirectToAction("AgreementVerificationList");

        }
        [HttpGet]
        public async Task<ActionResult> UpdateForAgreementReview(int Id, int statusId, string remarks)
        {
            var data = await agreementService.GetAgreementById(Id);
            await agreementService.UpdateForAgreementVerification(Id, statusId, remarks);

            #region Save History

            string actDetailss = string.Empty;
            actDetailss = "Agreement " + data.agreementNo + " is sent to Review.";
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(data.leadsId),
                actionName = "Agreement",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfoes.GetUserInfoByUser(userName);
                var EmpInfo = await employeeInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
                int empId = 0;
                string email = "";
                string name = "";
                if (EmpInfo != null)
                {
                    empId = EmpInfo.Id;
                    email = EmpInfo.emailAddress;
                    name = EmpInfo.nameEnglish;
                }

                var aggrementDetails = await agreementService.GetAgreementDetailsByaggId(Id);

                string html = "<div><strong>Aggrement Review</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is sent to review for " + remarks+"."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' >  " + name + " </p></div>"
                        + " <br/>";

                if (email != null)
                {
                    await emailSenderService.SendEmailWithFrom(aggrementDetails.agreement.leads.leadOwner, email, "Reviewed for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("AgreementVerificationList");
            }


            return RedirectToAction("AgreementVerificationList");

        }
        [HttpGet]
        public async Task<ActionResult> UpdateForAgreementClose(int Id, int statusId, string remarks)
        {
            await agreementService.UpdateForAgreementVerification(Id, statusId, remarks);
            var data = await agreementService.GetAgreementById(Id);
            string actDetailss = string.Empty;
            actDetailss = "Agreement " + data.agreementNo + " is closed.";
            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(data.leadsId),
                actionName = "Agreement",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);


            try
            {
                string userName = HttpContext.User.Identity.Name;
                var userInfos = await userInfoes.GetUserInfoByUser(userName);
                var EmpInfo = await employeeInfoService.GetEmployeeInfoByCode(userInfos?.EmpCode);
                int empId = 0;
                string email = "";
                string name = "";
                if (EmpInfo != null)
                {
                    empId = EmpInfo.Id;
                    email = EmpInfo.emailAddress;
                    name = EmpInfo.nameEnglish;
                }

                var aggrementDetails = await agreementService.GetAgreementDetailsByaggId(Id);

                string html = "<div><strong>Aggrement Closed</strong></div>"
                        + " <br/>"
                        + "<p>Dear Sir,</p>"
                        + " <br/>"
                        + " This is to inform you that " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName + " is Closed."
                        + "<br/>"
                        + "<div><p> Thank You & Best Regards</p><p style = 'font-weight:bold' >  " + name + " </p></div>"
                        + " <br/>";

                if (email != null)
                {
                    await emailSenderService.SendEmailWithFrom(aggrementDetails.agreement.leads.leadOwner, email, "Closed for " + aggrementDetails.ratingYear.ratingTypeName + " of lead : " + aggrementDetails.agreement.leads.leadName, html);
                }
            }
            catch (Exception)
            {
                return RedirectToAction("AgreementVerificationList");
            }


            return RedirectToAction("AgreementVerificationList");

        }

        #endregion

        #region Agreement List
        public async Task<IActionResult> AgreementList()
        {
            string Owner = HttpContext.User.Identity.Name;
            AgreementViewModel model = new AgreementViewModel()
            {
                agreements = await agreementService.GetAgreementbyOwner(Owner),

            };

            return View(model);
        }

        public async Task<IActionResult> AgreementListReview()
        {
            string Owner = HttpContext.User.Identity.Name;
            var userId = _db.Users.Where(x => x.UserName == Owner).Select(x => x.Id).FirstOrDefault();
            var adminrole = _db.UserRoles.Where(x => x.UserId == userId).First();
            ViewBag.rolename = adminrole.RoleId;
            var lstleadBankInfo = await bankBranchService.GetLeadsBankInfo();
            AgreementViewModel model = new AgreementViewModel()
            {
                agreements = await agreementService.GetAgreementbyOwner(Owner),
                employeeInfos = await employeeInfoService.GetEmployeeInfo(),
                leadsBankInfos = lstleadBankInfo.Where(x => x.bankType == "Primary")

            };

            return View(model);
        }
        //public async Task<IActionResult> AgreementListBDReview()
        //{
        //    string Owner = HttpContext.User.Identity.Name;
        //    AgreementViewModel model = new AgreementViewModel()
        //    {
        //        agreements = await agreementService.GetAgreementbyOwner(Owner),

        //    };

        //    return View(model);
        //}

        public async Task<IActionResult> AgreementListVerified()
        {
            string Owner = HttpContext.User.Identity.Name;
            var userId = _db.Users.Where(x => x.UserName == Owner).Select(x => x.Id).FirstOrDefault();
            var adminrole = _db.UserRoles.Where(x => x.UserId == userId).First();
            ViewBag.rolename = adminrole.RoleId;
            var data = await agreementService.GetAgreementbyOwner(Owner);
            var lstleadBankInfo = await bankBranchService.GetLeadsBankInfo();
            //if (!_cache.TryGetValue(data, out data))
            //{
            //    // Key not in cache, so get data.
            //   // cacheEntry = DateTime.Now;

            //    // Set cache options.
            //    var cacheEntryOptions = new MemoryCacheEntryOptions()
            //        // Keep in cache for this time, reset time if accessed.
            //        .SetSlidingExpiration(TimeSpan.FromSeconds(3));

            //    // Save data in cache.
            //    _cache.Set(data, data, cacheEntryOptions);
            //}

            AgreementViewModel model = new AgreementViewModel()
            {
                agreements = data,// await agreementService.GetAgreementbyOwner(Owner),
                employeeInfos = await employeeInfoService.GetEmployeeInfo(),
                leadsBankInfos = lstleadBankInfo.Where(x => x.bankType == "Primary")

            };

            return View(model);
        }
        #endregion

        #region Agreement List for Lead
        public async Task<IActionResult> AgreementListForLead(int id, string leadName)
        {
            string Owner = HttpContext.User.Identity.Name;
            var userId = _db.Users.Where(x => x.UserName == Owner).Select(x => x.Id).FirstOrDefault();
            var adminrole = _db.UserRoles.Where(x => x.UserId == userId).First();
            ViewBag.rolename = adminrole.RoleId;
            AgreementViewModel model = new AgreementViewModel()
            {
                agreements = await agreementService.GetAgreementbyLeadId(id),

            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }

        public async Task<IActionResult> AgreementListForLeadPersonal(int id, string leadName)
        {
            string Owner = HttpContext.User.Identity.Name;
            AgreementViewModel model = new AgreementViewModel()
            {
                agreements = await agreementService.GetAgreementbyLeadId(id),

            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }


        public async Task<IActionResult> AgreementNewLead(int? id, int? leadId, string leadName)
        {
            var purchase = await agreementService.GetAgreementdesc();
            int Cpurchase = 0;
            Cpurchase = purchase.Id;
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            //var leadId = 0;

            if (Convert.ToInt32(id) != 0)
            {
                var data = await agreementService.GetAgreementById(Convert.ToInt32(id));
                agreementNo = data.agreementNo;
                leadId = data.leadsId;
            }
            ViewBag.agreementNo = agreementNo;
            var aspdata = await userInfoes.GetAllActiveEmployeeInfo();




            //var aspdatafil= aspdata.Select(x=>x.EmpCode).Select(x => )
            AgreementViewModel model = new AgreementViewModel()
            {
                agreements = await agreementService.GetAgreementbyLeadId(Convert.ToInt32(leadId)),
                agreementTypes = await agreementService.GetAllAgreementType(),
                // products = await proposalService.GetAllProduct(),
                agreementCategories = await agreementCategoryService.GetAllAgreementCategory(),
                contacts = await contactsService.GetAllContactsByLeadsId(Convert.ToInt32(leadId)),
                aspNetUsers = await userInfoes.GetUserInfoByModule(2),
                ratingYears = await agreementService.GetAllRatingYear(),
                vatCategories = await agreementService.GetAllVatCategory(),
                taxCategories = await agreementService.GetAllTaxCategory(),
                aspNetUsersViewModels = await userInfoes.GetAllActiveEmployeeInfo(),
                employeeInfos = await employeeInfoService.GetEmployeeInfoList(),

            };
            var lead = await leadsService.GetLeadDetailsById(Convert.ToInt32(leadId));
            if (lead != null)
            {
                ViewBag.leadName = lead.leadName;
            }
            ViewBag.masterId = id;
            ViewBag.leadId = leadId;
            return View(model);
        }

        public async Task<IActionResult> AgreementNewLeadPersonal(int? id, int? leadId, string leadName)
        {
            var purchase = await agreementService.GetAllAgreement();
            int Cpurchase = 0;
            Cpurchase = purchase.Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            //var leadId = 0;

            if (Convert.ToInt32(id) != 0)
            {
                var data = await agreementService.GetAgreementById(Convert.ToInt32(id));
                agreementNo = data.agreementNo;
                leadId = data.leadsId;
            }
            ViewBag.agreementNo = agreementNo;

            AgreementViewModel model = new AgreementViewModel()
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
            var lead = await leadsService.GetLeadDetailsById(Convert.ToInt32(leadId));
            if (lead != null)
            {
                ViewBag.leadName = lead.leadName;
            }
            ViewBag.masterId = id;
            ViewBag.leadId = leadId;
            return View(model);
        }

        // POST: Agreement Master/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAgreementNewLead([FromForm] AgreementViewModel model)
        {
            var masterId = 0;
            //if (!ModelState.IsValid)
            //{
            //    model.agreements = await agreementService.GetAgreementbyLeadId(Convert.ToInt32(model.leadsId));
            //    model.agreementTypes = await agreementService.GetAllAgreementType();
            //    ViewBag.leadName = model.leadName;
            //    ViewBag.leadId = Convert.ToInt32(model.leadsId);
            //    return View(model);
            //}
            //var purchase = await agreementService.GetAllAgreement();
            var purchase = await agreementService.GetAgreementdesc();
            int Cpurchase = 0;
            Cpurchase = purchase.Id;
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            if (model.agreementId > 0)
            {
                agreementNo = model.agreementNo;
            }
            var leaddata = await leadsService.GetLeadsById(model.leadsId);
            Agreement data = new Agreement
            {
                Id = model.agreementId,
                agreementTypeId = model.agreementTypeId,
                leadsId = model.leadsId,
                agreementNo = agreementNo,
                agreementOwner = leaddata.leadOwner,
                agreementDate = model.agreementDate,
                sendingDate = model.sendingDate,
                signingDate = model.signingDate,
                expiredDate = model.expiredDate,
                agreementCategoryId = model.agreementCategoryId,
                noOfReports = model.noOfReports,
                expectedReportDate = model.expectedReportDate,
                loanLimitAmount = model.loanLimitAmount,
                loanLimitUnit = model.loanLimitUnit,
                priority = model.priority,
                remarks = model.remarks,
                companySignatory = model.companySignatory,
                companyWitness = model.companyWitness,
                contactSignatoryId = model?.contactSignatoryId,
                contactWitnessId = model?.contactWitnessId,
                agreementStatusId = model.agreementStatusId,
                companySignatoryDesignation = model.cosigdesignationName,
                companyWitnessDesignation = model.cowitdesignationName,
                contactSignatoryDesignation = model.consigdesignationName,
                contactWitnessDesignation = model.conwitdesignationName,
                reviewremarks=model.revremarks



            };

            masterId = await agreementService.SaveAgreement(data);

            if (model.ratingYearId != null)
            {
                if (model.ratingYearId.Length > 0)
                {
                    int AgreementDetailId = 0;
                    for (int i = 0; i < model.ratingYearId.Length; i++)
                    {
                        if (model.agreementId > 0)
                        {
                            var AgreementDetails = await agreementService.GetAgreementDetailsbyAgreementRatingYearId(Convert.ToInt32(masterId), Convert.ToInt32(model.ratingYearId[i]));
                            if (AgreementDetails.Count() > 0)
                            {
                                AgreementDetailId = AgreementDetails.FirstOrDefault().Id;
                            }
                            else
                            {
                                AgreementDetailId = 0;
                            }
                        }
                        AgreementDetails Details = new AgreementDetails();

                        Details.Id = AgreementDetailId;
                        Details.agreementId = Convert.ToInt32(masterId);
                        Details.ratingYearId = Convert.ToInt32(model.ratingYearId[i]);
                        Details.vatCategoryId = Convert.ToInt32(model.vatCategoryId[i]);
                        Details.taxCategoryId = Convert.ToInt32(model.taxCategoryId[i]);
                        Details.ratingDate = model.ratingDate[i];
                        Details.agreementAmount = model.agreementAmount[i];
                        Details.ratingAmount = model.ratingAmount[i];
                        Details.vatAmount = model.vatAmount[i];
                        Details.taxAmount = model.taxAmount[i];
                        Details.totalAmount = model.totalAmount[i];
                        Details.isProposed = model.isProposed[i];

                        await agreementService.SaveAgreementDetails(Details);
                        Details = new AgreementDetails();
                    }

                }
            }

            #region Save History
            string actDetailss = string.Empty;
            if (model.agreementId == 0)
            {
                actDetailss = "Agreement " + model.agreementNo + " is Created.";
            }
            else
            {
                actDetailss = "Agreement " + model.agreementNo + " is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(model.leadsId),
                actionName = "Agreement",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            

            if (model.isPersonal == 1)
            {
                return RedirectToAction(nameof(AgreementListForLeadPersonal), new
                {
                    id = Convert.ToInt32(model.leadsId),
                    leadName = model.leadName
                });
            }
            else
            {
                return RedirectToAction(nameof(AgreementListForLead), new
                {
                    id = Convert.ToInt32(model.leadsId),
                    leadName = model.leadName
                });
            }
        }

        #endregion

        #region Agreement Pdf Report



        [AllowAnonymous]
        public IActionResult AgreementWordPdfAction(int AgreementId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/CRMLead/Agreement/AgreementPdf?AgreementId=" + AgreementId;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }


            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".doc");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToWord(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }
        [AllowAnonymous]
        public IActionResult AgreementPdfAction(int AgreementId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/CRMLead/Agreement/AgreementPdf?AgreementId=" + AgreementId;

            string fileName;
            string status = myPDF.GeneratePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult AgreementCWORDPdfAction(int AgreementId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/CRMLead/Agreement/AgreementCPdf?AgreementId=" + AgreementId;

            string fileName;
            string status = myPDF.GenerateCPDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            //var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            //return new FileStreamResult(stream, "application/pdf");
            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".doc");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToWord(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }

        [AllowAnonymous]
        public IActionResult AgreementCPdfAction(int AgreementId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/CRMLead/Agreement/AgreementCPdf?AgreementId=" + AgreementId;

            string fileName;
            string status = myPDF.GenerateCPDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult AgreementSPdfAction(int AgreementId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/CRMLead/Agreement/AgreementSPdf?AgreementId=" + AgreementId;

            string fileName;
            string status = myPDF.GenerateSPDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult AgreementSWORDPdfAction(int AgreementId)
        {
            string scheme = Request.Scheme;
            var host = Request.Host;

            string url = scheme + "://" + host + "/CRMLead/Agreement/AgreementSPdf?AgreementId=" + AgreementId;

            string fileName;
            string status = myPDF.GenerateSPDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            //var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            //return new FileStreamResult(stream, "application/pdf");
            string pdfpath = rootPath + @"\wwwroot\pdf\" + fileName;
            string wordpath = fileName.Replace(".pdf", ".doc");
            SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            f.OpenPdf(rootPath + @"\wwwroot\pdf\" + fileName);
            f.ToWord(rootPath + @"\wwwroot\pdf\" + wordpath);
            //  var stream = new FileStream(rootPath + "/wwwroot/pdf/" + wordpath, FileMode.Open);
            var model = wordpath;
            return Json(model);
        }


        [AllowAnonymous]
        public async Task<ActionResult> AgreementPdf(int AgreementId)
        {
            
            var agreement = await agreementService.GetAgreementById(AgreementId);
            var leaddata = await agreementService.GetAgreementbyLeadId((int)agreement.leadsId);
            leaddata = leaddata.Where(x => x.Id != AgreementId).ToList();
            var data = new Agreement();
            if (leaddata.Count() != 0)
            {
                data = await agreementService.GetAgreementByagreementId(leaddata.FirstOrDefault().Id);

            }
            if (data == null)
            {
                data = new Agreement();

            }
            AgreementViewModel model = new AgreementViewModel()
            {
                getAgreementDeatilsById = await agreementService.GetAgreementByagreementId(AgreementId),
                getAgreementReportByAgreementId = await agreementService.GetAgreementReportByAgreementId(AgreementId),
                addresses = await addressesService.GetAddressesByLeadId((int)agreement.leadsId),
                companies = await eRPCompanyService.GetAllCompany(),
                getAgreementDeatilsBypreId=data //await agreementService.GetAgreementByagreementId(leaddata.FirstOrDefault().Id),
            };
            //await agreementService.UpdateForAgreementPrint(AgreementId);

            return View(model);
        }

        [AllowAnonymous]
        public async Task<ActionResult> AgreementCPdf(int AgreementId)
        {
            var aggrement = await agreementService.GetAgreementById(AgreementId);
            var leaddata = await agreementService.GetAgreementbyLeadId((int)aggrement.leadsId);
            leaddata = leaddata.Where(x => x.Id != AgreementId).ToList();
            var data = new Agreement();
            if(leaddata.Count()!=0)
            {
                data = await agreementService.GetAgreementByagreementId(leaddata.FirstOrDefault().Id);

            }
            if (data == null)
            {
                data = new Agreement(); 

            }
            AgreementViewModel model = new AgreementViewModel()
            {
                getAgreementDeatilsById = await agreementService.GetAgreementByagreementId(AgreementId),
                getAgreementReportByAgreementId = await agreementService.GetAgreementReportByAgreementId(AgreementId),
                companies = await eRPCompanyService.GetAllCompany(),
                addresses = await addressesService.GetAddressesByLeadId((int)aggrement.leadsId),
                 getAgreementDeatilsBypreId = data// await agreementService.GetAgreementByagreementId(leaddata.FirstOrDefault().Id),
            };
            //await agreementService.UpdateForAgreementPrint(AgreementId);

            return View(model);
        }
        [AllowAnonymous]
        public async Task<ActionResult> AgreementSPdf(int AgreementId)
        {
            var aggrement = await agreementService.GetAgreementById(AgreementId);
            var leaddata = await agreementService.GetAgreementbyLeadId((int)aggrement.leadsId);
            leaddata = leaddata.Where(x => x.Id != AgreementId).ToList();
            var data = new Agreement();
            if (leaddata.Count() != 0)
            {
                data = await agreementService.GetAgreementByagreementId(leaddata.FirstOrDefault().Id);

            }
            if (data == null)
            {
                data = new Agreement();

            }
            AgreementViewModel model = new AgreementViewModel()
            {
                getAgreementDeatilsById = await agreementService.GetAgreementByagreementId(AgreementId),
                getAgreementReportByAgreementId = await agreementService.GetAgreementReportByAgreementId(AgreementId),
                companies = await eRPCompanyService.GetAllCompany(),
                addresses = await addressesService.GetAddressesByLeadId((int)aggrement.leadsId),
                getAgreementDeatilsBypreId = data// await agreementService.GetAgreementByagreementId(leaddata.FirstOrDefault().Id),
            };
            //await agreementService.UpdateForAgreementPrint(AgreementId);

            return View(model);
        }
        #endregion

        #region Agreement Verification List
        public async Task<IActionResult> AgreementVerificationList()
        {
            AgreementViewModel model = new AgreementViewModel()
            {
                // agreements = await agreementService.GetAllAgreement(),
                getLeadInfoVerificationListViewModels = await leadsService.GetLeadInfoVerification(HttpContext.User.Identity.Name),

            };

            return View(model);
        }
        public async Task<IActionResult> AgreementPrintList()
        {
            AgreementViewModel model = new AgreementViewModel()
            {
                // agreements = await agreementService.GetAllAgreement(),
                getLeadInfoPrintListViewModels = await leadsService.GetLeadInfoPrint(HttpContext.User.Identity.Name)

            };

            return View(model);
        }
    
        public async Task<IActionResult> AgreementBackView(int? id)
        {
            var agreementdata = await agreementService.GetAgreementById((int)id);
            var clientdata = await clientService.GetClientsByLeadId((int)agreementdata.leadsId);
            await agreementService.UpdateForAgreementBackToLead((int)id);
            if (clientdata != null)
            {
                await clientService.UpdateReClient(clientdata.Id);
            }
            //await 
            return RedirectToAction(nameof(AgreementListReview));
            
        }
        public async Task<IActionResult> AgreementView(int? id)
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

            AgreementViewModel model = new AgreementViewModel()
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
                addresses = await addressesService.GetAddressesByLeadId(leadId),
                leadsBankInfos = await bankBranchService.GetLeadsBankInfoByLeadId(leadId)
            };

            ViewBag.masterId = id;
            ViewBag.leadId = leadId;
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> UpdateForAgreementPrint(int Id)
        {
            await agreementService.UpdateForAgreementPrint(Id);
            return Json(true);
        }
        #endregion


        #region Lead Agreement Details

        public async Task<IActionResult> AgreementDetails(int Id, int? leadid, string leadName)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Agreement", 2);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Agreement", "photo", 2);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Agreement", "Document", 2);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new AgreementViewModel
                {
                    getAgreementDeatilsById = await agreementService.GetAgreementByagreementId(Id),
                    relProductAgreement = await agreementService.GetRelProductAgreementByagreementId(Id),
                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };

                ViewBag.leadName = leadName;
                ViewBag.leadId = leadid;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> SaveComment([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "Agreement",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = 2,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("AgreementDetails", "Agreement", new { id = model.actionTypeId, Area = "CRMLead" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> DeleteComments(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("AgreementDetails", "Agreement", new { id = actionId, Area = "CRMLead" });
        }

        [HttpPost]
        public async Task<IActionResult> SaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile docformFile)
        {
            try
            {
                if (docformFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(docformFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await docformFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("AgreementDetails", "Agreement", new { id = model.actionTypeId, Area = "CRMLead" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> SavePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("AgreementDetails", "Agreement", new { id = model.actionTypeId, Area = "CRMLead" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> DeletePhoto(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("AgreementDetails", "Agreement", new { id = actionId, Area = "CRMLead" });
        }

        #endregion


        #region Client Agreement New

        public async Task<IActionResult> AgreementListForClient(int id, string leadName)
        {
            string Owner = HttpContext.User.Identity.Name;
            AgreementViewModel model = new AgreementViewModel()
            {
                agreements = await agreementService.GetAgreementbyLeadId(id),

            };
            ViewBag.leadName = leadName;
            ViewBag.leadId = id;
            return View(model);
        }

        public async Task<IActionResult> ClientAgreement(int? id, int? leadId, string leadName)
        {
            var purchase = await agreementService.GetAllAgreement();
            int Cpurchase = 0;
            Cpurchase = purchase.Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            //var leadId = 0;

            if (Convert.ToInt32(id) != 0)
            {
                var data = await agreementService.GetAgreementById(Convert.ToInt32(id));
                agreementNo = data.agreementNo;
                leadId = data.leadsId;
            }
            ViewBag.agreementNo = agreementNo;

            AgreementViewModel model = new AgreementViewModel()
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
            var lead = await leadsService.GetLeadDetailsById(Convert.ToInt32(leadId));
            ViewBag.leadName = lead.leadName;
            ViewBag.masterId = id;
            ViewBag.leadId = leadId;
            return View(model);
        }

        // POST: Agreement Master/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAgreementNewClient([FromForm] AgreementViewModel model)
        {
            var masterId = 0;
            //if (!ModelState.IsValid)
            //{
            //    model.agreements = await agreementService.GetAgreementbyLeadId(Convert.ToInt32(model.leadsId));
            //    model.agreementTypes = await agreementService.GetAllAgreementType();
            //    ViewBag.leadName = model.leadName;
            //    ViewBag.leadId = Convert.ToInt32(model.leadsId);
            //    return View(model);
            //}
            var purchase = await agreementService.GetAllAgreement();
            int Cpurchase = 0;
            Cpurchase = purchase.Count();
            string idate = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd");
            string agreementNo = "Agreement" + '-' + idate + '-' + (Cpurchase + 1);
            if (model.agreementId > 0)
            {
                agreementNo = model.agreementNo;
            }
            Agreement data = new Agreement
            {
                Id = model.agreementId,
                agreementTypeId = model.agreementTypeId,
                leadsId = model.leadsId,
                agreementNo = agreementNo,
                agreementOwner = HttpContext.User.Identity.Name,
                agreementDate = model.agreementDate,
                sendingDate = model.sendingDate,
                signingDate = model.signingDate,
                expiredDate = model.expiredDate,
                agreementCategoryId = model.agreementCategoryId,
                noOfReports = model.noOfReports,
                expectedReportDate = model.expectedReportDate,
                loanLimitAmount = model.loanLimitAmount,
                loanLimitUnit = model.loanLimitUnit,
                priority = model.priority,
                remarks = model.remarks,
                companySignatory = model.companySignatory,
                companyWitness = model.companyWitness,
                contactSignatoryId = model.contactSignatoryId,
                contactWitnessId = model.contactWitnessId,
                agreementStatusId = model.agreementStatusId

            };

            masterId = await agreementService.SaveAgreement(data);

            if (model.ratingYearId != null)
            {
                if (model.ratingYearId.Length > 0)
                {
                    int AgreementDetailId = 0;
                    for (int i = 0; i < model.ratingYearId.Length; i++)
                    {
                        if (model.agreementId > 0)
                        {
                            var AgreementDetails = await agreementService.GetAgreementDetailsbyAgreementRatingYearId(Convert.ToInt32(masterId), Convert.ToInt32(model.ratingYearId[i]));
                            if (AgreementDetails.Count() > 0)
                            {
                                AgreementDetailId = AgreementDetails.FirstOrDefault().Id;
                            }
                            else
                            {
                                AgreementDetailId = 0;
                            }
                        }
                        AgreementDetails Details = new AgreementDetails();

                        Details.Id = AgreementDetailId;
                        Details.agreementId = Convert.ToInt32(masterId);
                        Details.ratingYearId = Convert.ToInt32(model.ratingYearId[i]);
                        Details.vatCategoryId = Convert.ToInt32(model.vatCategoryId[i]);
                        Details.taxCategoryId = Convert.ToInt32(model.taxCategoryId[i]);
                        Details.ratingDate = model.ratingDate[i];
                        Details.agreementAmount = model.agreementAmount[i];
                        Details.ratingAmount = model.ratingAmount[i];
                        Details.vatAmount = model.vatAmount[i];
                        Details.taxAmount = model.taxAmount[i];
                        Details.totalAmount = model.totalAmount[i];
                        Details.isProposed = model.isProposed[i];

                        await agreementService.SaveAgreementDetails(Details);
                        Details = new AgreementDetails();
                    }

                }
            }

            #region Save History
            string actDetailss = string.Empty;
            if (model.agreementId == 0)
            {
                actDetailss = "Agreement " + model.agreementNo + " is Created.";
            }
            else
            {
                actDetailss = "Agreement " + model.agreementNo + " is Updated.";
            }

            LeadsHistory leadsData = new LeadsHistory
            {
                Id = 0,
                leadsId = Convert.ToInt32(model.leadsId),
                actionName = "Agreement",
                actionDetails = actDetailss
            };
            await leadsService.SaveLeadsHistory(leadsData);
            #endregion

            //return RedirectToAction(nameof(AgreementList));
            return RedirectToAction(nameof(AgreementListForClient), new
            {
                id = Convert.ToInt32(model.leadsId),
                leadName = model.leadName
            });
        }


        #endregion


        #region Client Agreement Details

        public async Task<IActionResult> ClientAgreementDetails(int Id, int? leadid, string leadName)
        {
            try
            {
                ViewBag.masterId = Id;
                var CommentInfo = await attachmentCommentService.GetCommentByActionTypeId(Id, "Agreement", 2);
                if (CommentInfo == null)
                {
                    CommentInfo = new List<Comment>();
                }
                var photoInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Agreement", "photo", 2);
                if (photoInfo == null)
                {
                    photoInfo = new List<DocumentPhotoAttachment>();
                }
                var docInfo = await attachmentCommentService.GetDocumentAttachmentByActionId(Id, "Agreement", "Document", 2);
                if (docInfo == null)
                {
                    docInfo = new List<DocumentPhotoAttachment>();
                }

                var model = new AgreementViewModel
                {
                    getAgreementDeatilsById = await agreementService.GetAgreementByagreementId(Id),
                    relProductAgreement = await agreementService.GetRelProductAgreementByagreementId(Id),
                    comments = CommentInfo,
                    photoes = photoInfo,
                    documents = docInfo,
                };

                ViewBag.leadName = leadName;
                ViewBag.leadId = leadid;
                return View(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<ActionResult> ClientSaveComment([FromForm] CommentsViewModel model)
        {
            try
            {
                Comment entityComment = new Comment
                {
                    Id = Convert.ToInt32(model.commentsId),
                    actionType = "Agreement",
                    actionTypeId = model.actionTypeId,
                    title = model.title,
                    comment = model.comment,
                    moduleId = 2,
                    createdBy = HttpContext.User.Identity.Name,
                };
                int commentsId = await attachmentCommentService.SaveComment(entityComment);
                return RedirectToAction("ClientAgreementDetails", "Agreement", new { id = model.actionTypeId, Area = "CRMLead" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpGet]
        public async Task<IActionResult> ClientDeleteComments(int Id, int actionId)
        {
            await attachmentCommentService.DeleteCommentById(Id);
            return RedirectToAction("ClientAgreementDetails", "Agreement", new { id = actionId, Area = "CRMLead" });
        }

        [HttpPost]
        public async Task<IActionResult> ClientSaveDoc([FromForm] DocumentAttachmentViewModel model, IFormFile docformFile)
        {
            try
            {
                if (docformFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "Document";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(docformFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await docformFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("ClientAgreementDetails", "Agreement", new { id = model.actionTypeId, Area = "CRMLead" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> ClientSavePhoto([FromForm] DocumentAttachmentViewModel model, IFormFile formFile)
        {
            try
            {
                if (formFile != null)
                {
                    string userName = User.Identity.Name;
                    string documentType = "photo";
                    var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                    var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                    string fileType = Path.GetExtension(fileName);
                    fileName = fileName.Contains("\\")
                        ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                        : fileName.Trim('"');

                    string NewFileName = model.actionTypeId + "_" + documentType + "_" + fileName;
                    string fileLocation = "/Upload/Photo/" + NewFileName;
                    var fullFilePath = Path.Combine(filesPath, NewFileName);

                    using (var stream = new FileStream(fullFilePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    DocumentPhotoAttachment data = new DocumentPhotoAttachment
                    {
                        Id = Convert.ToInt32(model.documentId),
                        actionType = model.actionType,
                        actionTypeId = model.actionTypeId,
                        subject = model.subject,
                        fileName = NewFileName,
                        filePath = fileLocation,
                        fileType = fileType,
                        description = model.description,
                        documentType = documentType,
                        moduleId = 2,
                        createdBy = userName
                    };
                    await attachmentCommentService.SaveDocumentAttachment(data);
                }

                return RedirectToAction("ClientAgreementDetails", "Agreement", new { id = model.actionTypeId, Area = "CRMLead" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public async Task<IActionResult> ClientDeletePhoto(int actionId, int photoId)
        {
            var data = await attachmentCommentService.GetDocumentAttachmentById(photoId);
            var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";

            var fullFilePath = Path.Combine(filesPath, data.fileName);
            if (System.IO.File.Exists(fullFilePath))
                System.IO.File.Delete(fullFilePath);
            await attachmentCommentService.DeleteDocumentAttachmentById(photoId);
            return RedirectToAction("ClientAgreementDetails", "Agreement", new { id = actionId, Area = "CRMLead" });
        }

        [HttpGet]
        public async Task<IActionResult> getcontactdetailbyId(int id)
        {
            return Json(await contactsService.GetContactsById(id));
        }
        [HttpGet]
        public async Task<IActionResult> getemployeebyempcode(string empCode)
        {
            return Json(await userInfoes.Getemployeebyname(empCode));
        }
        [HttpGet]
        public async Task<IActionResult> getemployee()
        {
            return Json(await employeeInfoService.GetEmployeeInfoList());
        }

        #endregion
    }
}