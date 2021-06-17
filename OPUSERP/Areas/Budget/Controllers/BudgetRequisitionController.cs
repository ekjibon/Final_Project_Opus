using DinkToPdf.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Accounting.Services.AccountingSettings.Interfaces;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.Areas.Budget.Models.Lang;
using OPUSERP.Areas.SCMMatrix.Models;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Budget.Controllers
{
    [Authorize]
    [Area("Budget")]
    public class BudgetRequisitionController : Controller
    {
        private readonly LangGenerate<BudgetRequisitionLn> _lang;
        private readonly LangGenerate<BudgetRequisitionExcelLn> _lang1;
        private readonly IBudgetRequsitionMasterService budgetRequsitionMasterService;
        private readonly IBudgetHeadService budgetHeadService;
        private readonly IUserInfoes userInfo;
        private readonly IApprovalLogService approvalLogService;
        private readonly IApprovalMatrixService approvalMatrixService;
        private readonly IERPCompanyService eRPCompanyService;
        private readonly ILedgerService ledgerService;
        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public BudgetRequisitionController(IHostingEnvironment hostingEnvironment, ILedgerService ledgerService, IERPCompanyService eRPCompanyService, IConverter converter, IBudgetRequsitionMasterService budgetRequsitionMasterService, IUserInfoes userInfo, IBudgetHeadService budgetHeadService, IApprovalLogService approvalLogService, IApprovalMatrixService approvalMatrixService)
        {
            _lang = new LangGenerate<BudgetRequisitionLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new LangGenerate<BudgetRequisitionExcelLn>(hostingEnvironment.ContentRootPath);
            this.budgetRequsitionMasterService = budgetRequsitionMasterService;
            this.budgetHeadService = budgetHeadService;
            this.userInfo = userInfo;
            this.approvalLogService = approvalLogService;
            this.approvalMatrixService = approvalMatrixService;
            this.eRPCompanyService = eRPCompanyService;
            this.ledgerService = ledgerService;
            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }
        [AllowAnonymous]
        public IActionResult RequisitionPreviewPdf(int id)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Budget/BudgetRequisition/RequisitionPreview?id=" + id;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }
        [AllowAnonymous]
        public IActionResult RequisitionPreviewPPdf(int id)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Budget/BudgetRequisition/RequisitionPreviewP?id=" + id;

            string status = myPDF.GenerateLandscapePDF(out fileName, url);

            if (status != "done")
            {
                return Content("<h1>Something Went Wrong</h1>");
            }

            var stream = new FileStream(rootPath + "/wwwroot/pdf/" + fileName, FileMode.Open);
            return new FileStreamResult(stream, "application/pdf");
        }

        [AllowAnonymous]
        public async Task<IActionResult> RequisitionPreview(int id)
        {
            IEnumerable<BudgetRequsitionDetail> details = new List<BudgetRequsitionDetail>();
            BudgetRequsitionMaster masterInfoes = new BudgetRequsitionMaster();

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var master = await budgetRequsitionMasterService.GetBudgetRequsitionMasterById(Convert.ToInt32(id));
            masterInfoes.Id = master.Id;
            masterInfoes.requsitionNo = master.requsitionNo;
            masterInfoes.budgetBranchId = master.budgetBranchId;
            masterInfoes.requsitionDate = master.requsitionDate;
            masterInfoes.fiscalYearId = master.fiscalYearId;
            masterInfoes.status = master.status;
            string branchname = master?.budgetBranch?.branchUnitName;
            details = await budgetRequsitionMasterService.GetBudgetRequsitionDetailBymasterId(Convert.ToInt32(id));
            FiscalYear fiscalYear = new FiscalYear();
            fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(Convert.ToInt32(master.fiscalYearId));

            int reqBy = Convert.ToInt32(master.RequsitionBy);

            IEnumerable<ApproverPanelViewModel> lstApproverPanel=await approvalMatrixService.GetPRApproverPanelByUserId(reqBy,id);

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                flang = _lang.PerseLang("Budget/BudgetRequisitionEN.json", "Budget/BudgetRequisitionBN.json", Request.Cookies["lang"]),
                reqId = masterInfoes.Id,
                Number = masterInfoes.requsitionNo,
                Date = masterInfoes.requsitionDate,
                year = masterInfoes.fiscalYearId,
                branchName= branchname,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                budgetRequsitionDetails = details,
                fiscalYear = fiscalYear,
                approerPanelList=lstApproverPanel,
                companies = await eRPCompanyService.GetAllCompany()
            };
            return View(model);
        }

        [AllowAnonymous]
        public async Task<IActionResult> RequisitionPreviewP(int id)
        {
            IEnumerable<BudgetRequsitionDetailFin> details = new List<BudgetRequsitionDetailFin>();
            BudgetRequsitionMasterFin masterInfoes = new BudgetRequsitionMasterFin();

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var master = await budgetRequsitionMasterService.GetBudgetRequsitionMasterFinById(Convert.ToInt32(id));
            masterInfoes.Id = master.Id;
            masterInfoes.requsitionNo = master.requsitionNo;
            masterInfoes.budgetBranchId = master.budgetBranchId;
            masterInfoes.requsitionDate = master.requsitionDate;
            masterInfoes.fiscalYearId = master.fiscalYearId;
            masterInfoes.status = master.status;
            masterInfoes.StartDate = master.StartDate;
            masterInfoes.EndDate = master.EndDate;
            masterInfoes.status = master.status;
            string branchname = master?.budgetBranch?.branchUnitName;
            details = await budgetRequsitionMasterService.GetBudgetRequsitionDetailFinBymasterId(Convert.ToInt32(id));
            FiscalYear fiscalYear = new FiscalYear();
            fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(Convert.ToInt32(master.fiscalYearId));

            int reqBy = Convert.ToInt32(master.RequsitionBy);

            IEnumerable<ApproverPanelViewModel> lstApproverPanel = await approvalMatrixService.GetPRApproverPanelByUserId(reqBy, id);

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                flang = _lang.PerseLang("Budget/BudgetRequisitionEN.json", "Budget/BudgetRequisitionBN.json", Request.Cookies["lang"]),
                reqId = masterInfoes.Id,
                Number = masterInfoes.requsitionNo,
                Date = masterInfoes.requsitionDate,
                year = masterInfoes.fiscalYearId,
                StartDate=masterInfoes.StartDate,
                EndDate=masterInfoes.EndDate,
                branchName = branchname,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                budgetRequsitionDetailFins = details,
                fiscalYear = fiscalYear,
                approerPanelList = lstApproverPanel,
                companies = await eRPCompanyService.GetAllCompany()
            };
            return View(model);
        }
        public async Task<IActionResult> Index(int? id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMaster();
            string productionNo = ("BR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();
            IEnumerable<BudgetRequsitionDetail> details = new List<BudgetRequsitionDetail>();
            BudgetRequsitionMaster masterInfoes = new BudgetRequsitionMaster();
            FiscalYear fiscalYear = new FiscalYear();
            if (id > 0)
            {
                var master = await budgetRequsitionMasterService.GetBudgetRequsitionMasterById(Convert.ToInt32(id));
                masterInfoes.Id = master.Id;
                masterInfoes.requsitionNo = master.requsitionNo;
                masterInfoes.budgetBranchId = master.budgetBranchId;
                masterInfoes.requsitionDate = master.requsitionDate;
                masterInfoes.fiscalYearId = master.fiscalYearId;
                masterInfoes.status = master.status;
                details = await budgetRequsitionMasterService.GetBudgetRequsitionDetailBymasterId(Convert.ToInt32(id));
                fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(Convert.ToInt32(master.fiscalYearId));
            }
            else
            {
                masterInfoes.Id = 0;
                masterInfoes.requsitionNo = productionNo;
                masterInfoes.requsitionDate = DateTime.Now;
                details=new List<BudgetRequsitionDetail>();
                fiscalYear = new FiscalYear();
            }

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                flang = _lang.PerseLang("Budget/BudgetRequisitionEN.json", "Budget/BudgetRequisitionBN.json", Request.Cookies["lang"]),
                reqId = masterInfoes.Id,
                Number = masterInfoes.requsitionNo,
                Date = masterInfoes.requsitionDate,
                year = masterInfoes.fiscalYearId,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                budgetRequsitionDetails=details,
                fiscalYear=fiscalYear,
            };
            return View(model);
        }

        public async Task<IActionResult> FinIndex(int? id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMasterFin();
            string productionNo = ("FBR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();
            IEnumerable<BudgetRequsitionDetailFin> details = new List<BudgetRequsitionDetailFin>();
            BudgetRequsitionMasterFin masterInfoes = new BudgetRequsitionMasterFin();
            FiscalYear fiscalYear = new FiscalYear();
            if (id > 0)
            {
                var master = await budgetRequsitionMasterService.GetBudgetRequsitionMasterFinById(Convert.ToInt32(id));
                masterInfoes.Id = master.Id;
                masterInfoes.requsitionNo = master.requsitionNo;
                masterInfoes.budgetBranchId = master.budgetBranchId;
                masterInfoes.requsitionDate = master.requsitionDate;
                masterInfoes.fiscalYearId = master.fiscalYearId;
                masterInfoes.StartDate = master.StartDate;
                masterInfoes.EndDate = master.EndDate;
                masterInfoes.status = master.status;
                details = await budgetRequsitionMasterService.GetBudgetRequsitionDetailFinBymasterId(Convert.ToInt32(id));
                fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(Convert.ToInt32(master.fiscalYearId));
            }
            else
            {
                masterInfoes.Id = 0;
                masterInfoes.requsitionNo = productionNo;
                masterInfoes.requsitionDate = DateTime.Now;
                masterInfoes.StartDate = DateTime.Now;
                masterInfoes.EndDate = DateTime.Now;
                details = new List<BudgetRequsitionDetailFin>();
                fiscalYear = new FiscalYear();
            }

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                flang = _lang.PerseLang("Budget/BudgetRequisitionEN.json", "Budget/BudgetRequisitionBN.json", Request.Cookies["lang"]),
                reqId = masterInfoes.Id,
                Number = masterInfoes.requsitionNo,
                Date = masterInfoes.requsitionDate,
                year = masterInfoes.fiscalYearId,
                StartDate = masterInfoes.StartDate,
                EndDate = masterInfoes.EndDate,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                budgetRequsitionDetailFins = details,
                fiscalYear = fiscalYear,
            };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index([FromForm] BudgetRequisitionViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);
          
            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMaster();
            string productionNo = ("BR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            if (model.reqId > 0)
            {
                productionNo = model.Number;
            }

            if (model.heads == null)
            {
                ModelState.AddModelError(string.Empty, "Have to Add minimum 1 Budget Head");
                model.reqId = 0;
                model.Number = productionNo;
                model.Date = DateTime.Now;
                return View(model);
            }
            BudgetRequsitionMaster master = new BudgetRequsitionMaster
            {
                Id = Convert.ToInt32(model.reqId),
                requsitionNo = productionNo,
                requsitionDate = model.Date,
                fiscalYearId=model.year,
                budgetBranchId=userInfos.projectId,
                status = 1,
                isProcess =0,
                RequsitionBy = userInfos.UserId,
                grandTotal=model.amounts.Sum()
            };
            int masterId = await budgetRequsitionMasterService.SaveBudgetRequsitionMaster(master);

            if (model.reqId > 0)
            {
                await budgetRequsitionMasterService.DeleteBudgetRequsitionDetailBymasterId(Convert.ToInt32(model.reqId));
            }
            for (int i = 0; i < model.heads.Length; i++)
            {
                BudgetRequsitionDetail details = new BudgetRequsitionDetail
                {
                    Id = 0,
                    budgetRequsitionMasterId = masterId,
                    budgetHeadId=model.heads[i],
                    firstMonth=model.col1[i],
                    secondMonth=model.col2[i],
                    thirdMonth=model.col3[i],
                    fourthMonth=model.col4[i],
                    fifthMonth=model.col5[i],
                    sixthMonth=model.col6[i],
                    seventhMonth=model.col7[i],
                    eighthMonth=model.col8[i],
                    ninethMonth=model.col9[i],
                    tenthMonth=model.col10[i],
                    eleventhMonth=model.col11[i],
                    twelvethMonth=model.col12[i],
                    subTotal=model.amounts[i]
                };
                await budgetRequsitionMasterService.SaveBudgetRequsitionDetail(details);
            }
            IEnumerable<ApproverPanelViewModel> lstApproverPanel  = await approvalMatrixService.GetPRApproverPanelList(userName, 8, 1);
            if (model.reqId == 0 && lstApproverPanel.Count() > 0)
            {
                int i = 0;
                string notes = "";
                foreach (ApproverPanelViewModel panels in lstApproverPanel)
                {
                    int isactive = 0;
                    int? nextAppId = 0;
                    if (i == 1)
                    {
                        isactive = 1;
                        nextAppId = 0;
                        notes = "";
                    }
                    else if (i == 0)
                    {
                        isactive = 0;
                        nextAppId = panels.nextApproverId;
                        notes = "Created";
                    }
                    else
                    {
                        isactive = 0;
                        nextAppId = 0;
                        notes = "";
                    }
                    ApprovalLog appLog = new ApprovalLog
                    {
                        masterId = masterId,
                        matrixTypeId = 1,
                        userId = Convert.ToInt32(panels.nextApproverId),
                        sequenceNo = Convert.ToInt32(panels.sequenceNo),
                        isActive = isactive,
                        notes = notes,
                        nextApproverId = nextAppId
                    };
                    i= i+ 1;
                    await approvalLogService.SaveApproverLog(appLog);
                }
            }
            
            return RedirectToAction(nameof(BudgetRequisitionList));
        }

        [HttpPost]
        public async Task<IActionResult> FinIndex([FromForm] BudgetRequisitionViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMasterFin();
            string productionNo = ("FBR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            if (model.reqId > 0)
            {
                productionNo = model.Number;
            }

            if (model.heads == null)
            {
                ModelState.AddModelError(string.Empty, "Have to Add minimum 1 Budget Head");
                model.reqId = 0;
                model.Number = productionNo;
                model.Date = DateTime.Now;
                return View(model);
            }
            BudgetRequsitionMasterFin master = new BudgetRequsitionMasterFin
            {
                Id = Convert.ToInt32(model.reqId),
                requsitionNo = productionNo,
                requsitionDate = model.Date,
                fiscalYearId = model.year,
                StartDate=model.StartDate,
                EndDate=model.EndDate,
                budgetBranchId = userInfos.projectId,
                status = 1,
                isProcess = 0,
                RequsitionBy = userInfos.UserId,
                grandTotal = model.amounts.Sum()
            };
            int masterId = await budgetRequsitionMasterService.SaveBudgetRequsitionMasterFin(master);

            if (model.reqId > 0)
            {
                await budgetRequsitionMasterService.DeleteBudgetRequsitionDetailFinBymasterId(Convert.ToInt32(model.reqId));
            }
            for (int i = 0; i < model.heads.Length; i++)
            {
                var reldata = await ledgerService.GetledgerRelationByLedgerSubledgerId((int)model.Mheads[i], (int)model.heads[i]);
                BudgetRequsitionDetailFin details = new BudgetRequsitionDetailFin
                {
                    Id = 0,
                    budgetRequsitionMasterId = masterId,
                    budgetHeadId = model.heads[i],
                    partnerId = model.Mheads[i],
                    ledgerRelationId=reldata.Id,
                    firstMonth = model.col1[i],
                    secondMonth = model.col2[i],
                    thirdMonth = model.col3[i],
                    fourthMonth = model.col4[i],
                    fifthMonth = model.col5[i],
                    sixthMonth = model.col6[i],
                    seventhMonth = model.col7[i],
                    eighthMonth = model.col8[i],
                    ninethMonth = model.col9[i],
                    tenthMonth = model.col10[i],
                    eleventhMonth = model.col11[i],
                    twelvethMonth = model.col12[i],
                    subTotal = model.amounts[i]
                };
                await budgetRequsitionMasterService.SaveBudgetRequsitionDetailFin(details);
            }
            IEnumerable<ApproverPanelViewModel> lstApproverPanel = await approvalMatrixService.GetPRApproverPanelList(userName, 9, 1);
            if (model.reqId == 0 && lstApproverPanel.Count() > 0)
            {
                int i = 0;
                string notes = "";
                foreach (ApproverPanelViewModel panels in lstApproverPanel)
                {
                    int isactive = 0;
                    int? nextAppId = 0;
                    if (i == 1)
                    {
                        isactive = 1;
                        nextAppId = 0;
                        notes = "";
                    }
                    else if (i == 0)
                    {
                        isactive = 0;
                        nextAppId = panels.nextApproverId;
                        notes = "Created";
                    }
                    else
                    {
                        isactive = 0;
                        nextAppId = 0;
                        notes = "";
                    }
                    ApprovalLog appLog = new ApprovalLog
                    {
                        masterId = masterId,
                        matrixTypeId = 9,
                        userId = Convert.ToInt32(panels.nextApproverId),
                        sequenceNo = Convert.ToInt32(panels.sequenceNo),
                        isActive = isactive,
                        notes = notes,
                        nextApproverId = nextAppId
                    };
                    i = i + 1;
                    await approvalLogService.SaveApproverLog(appLog);
                }
            }

            return RedirectToAction(nameof(BudgetRequisitionListFin));
        }

        public async Task<IActionResult> BudgetRequisitionList()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                budgetRequsitionMasters = await budgetRequsitionMasterService.GetBudgetRequsitionMasterByuserId((int)userInfos.UserId)
            };
            return View(model);
        }

        public async Task<IActionResult> BudgetRequisitionListFin()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                budgetRequsitionMasterFins = await budgetRequsitionMasterService.GetBudgetRequsitionMasterFinByuserId((int)userInfos.UserId)
            };
            return View(model);
        }

        public async Task<IActionResult> IndexByExcel()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMaster();
            string productionNo = ("BR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            BudgetRequsitionMaster productionPlan = new BudgetRequsitionMaster();
            productionPlan.Id = 0;
            productionPlan.requsitionNo = productionNo;
            productionPlan.requsitionDate = DateTime.Now;

            BudgetRequisitionViewModel model = new BudgetRequisitionViewModel
            {
                flang1 = _lang1.PerseLang("Budget/BudgetRequisitionExcelEN.json", "Budget/BudgetRequisitionExcelBN.json", Request.Cookies["lang"]),
                reqId = productionPlan.Id,
                Number = productionPlan.requsitionNo,
                Date = productionPlan.requsitionDate,
                year = productionPlan.fiscalYearId,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> IndexByExcel([FromForm] BudgetRequisitionViewModel model)
        {
            //return Json(model);
            string userName = HttpContext.User.Identity.Name;
            var userInfos = 58; // await userInfo.GetUserInfoByUser(userName);

            var plan = await budgetRequsitionMasterService.GetBudgetRequsitionMaster();
            string productionNo = ("BR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            if (model.reqId > 0)
            {
                productionNo = model.Number;
            }

            if (model.headName == null)
            {
                ModelState.AddModelError(string.Empty, "Have to Add minimum 1 Budget Head");
                model.reqId = 0;
                model.Number = productionNo;
                model.Date = DateTime.Now;
                return View(model);
            }
            BudgetRequsitionMaster master = new BudgetRequsitionMaster
            {
                Id = model.reqId ?? 0,
                requsitionNo = productionNo,
                requsitionDate = model.Date,
                status = 1,
                RequsitionBy = userInfos,
            };
            int masterId = await budgetRequsitionMasterService.SaveBudgetRequsitionMaster(master);

            if (model.reqId > 0)
            {
                await budgetRequsitionMasterService.DeleteBudgetRequsitionDetailBymasterId(Convert.ToInt32(model.reqId));
            }

            //for (int i = 0; i < model.heads.Length; i++)
            //{
            //    BudgetRequsitionDetail details = new BudgetRequsitionDetail
            //    {
            //        Id = 0,
            //        budgetRequsitionMasterId = masterId,
            //        budgetSubHeadId = model.heads[i],
            //        //Amount = model.amounts[i],
            //    };
            //    await budgetRequsitionMasterService.SaveBudgetRequsitionDetail(details);
            //}
            StringBuilder insertQuery = new StringBuilder("INSERT INTO Budget.BudgetRequsitionDetail (budgetRequsitionMasterId,budgetHeadId,");
            for (int c = 1; c < model.dbField.Length; c++)
            {
                insertQuery.Append(model.dbField[c]);
                if (c + 1 < model.dbField.Length)
                {
                    insertQuery.Append(",");
                }
            }
            insertQuery.Append(") VALUES");


            for (int i = 0; i < model.headName.Length; i++)
            {
                var headId = await budgetHeadService.GetBudgetHeadByCode(model.headName[i]);
                BudgetRequsitionDetail details = new BudgetRequsitionDetail();
                insertQuery.Append("(" + masterId + "," + headId.Id + "," + "'" + model.col1[i] + "'" + "," + "'" + model.col2[i] + "'" + "," + "'" + model.col3[i] + "'" + "," + "'" + model.col4[i] + "'"
                     + "," + "'" + model.col5[i] + "'" + "," + "'" + model.col6[i] + "'" + "," + "'" + model.col7[i] + "'" + "," + "'" + model.col8[i] + "'"
                      + "," + "'" + model.col9[i] + "'" + "," + "'" + model.col10[i] + "'" + "," + "'" + model.col11[i] + "'" + "," + "'" + model.col12[i] + "'" + ")");
                if (i + 1 < model.headName.Length)
                {
                    insertQuery.Append(",");
                }
            }
            string dtLine = insertQuery.ToString();
            DbActionByScript.ActionByScript(dtLine);
            //var a = 1;
            return RedirectToAction(nameof(BudgetRequisitionList));
        }

        [HttpPost]
        public JsonResult LoadFile()
        {
            if (Request.Form.Files.Count > 0)
            {
                try
                {
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    List<ExeclField> lstHead = new List<ExeclField>();
                    if (Request.Form.Files[0].Length > 0)
                    {
                        string fileExtension = Path.GetExtension(Request.Form.Files[0].FileName);

                        if (fileExtension == ".xls" || fileExtension == ".xlsx")
                        {
                            int _min = 10000;
                            int _max = 99999;
                            Random _rdm = new Random();
                            int rnd = _rdm.Next(_min, _max);

                            string filePath = string.Empty;
                            string fileName = string.Empty;
                            string fileType = string.Empty;

                            IFormFile file = Request.Form.Files[0];
                            fileType = file.ContentType;
                            fileName = rnd + file.FileName;
                            filePath = "wwwroot/Upload/CS/" + fileName;

                            var fileD = Path.Combine(Directory.GetCurrentDirectory(), filePath);
                            using (var fileSrteam = new FileStream(fileD, FileMode.Create))
                            {
                                file.CopyTo(fileSrteam);
                            }

                            string excelConnectionString = string.Empty;
                            excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            //connection String for xls file format.
                            if (fileExtension == ".xls")
                            {
                                excelConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                            }
                            //connection String for xlsx file format.
                            else if (fileExtension == ".xlsx")
                            {
                                excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                            }
                            //Create Connection to Excel work book and add oledb namespace
                            OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
                            excelConnection.Open();

                            dt = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                            if (dt == null)
                            {
                                return null;
                            }

                            String[] excelSheets = new String[dt.Rows.Count];
                            int t = 0;
                            //excel data saves in temp file here.
                            foreach (DataRow row in dt.Rows)
                            {
                                excelSheets[t] = row["TABLE_NAME"].ToString();
                                t++;
                            }
                            OleDbConnection excelConnection1 = new OleDbConnection(excelConnectionString);

                            string query = string.Format("Select * from [{0}]", excelSheets[0]);
                            using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(query, excelConnection1))
                            {
                                dataAdapter.Fill(ds);
                                excelConnection.Close();
                            }

                        }


                        
                        ExeclField column = new ExeclField();
                        for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                        {
                            var columnName = ds.Tables[0].Columns[i].ColumnName.ToString();
                            if (i == 0) { column.col0 = columnName; }
                            else if (i == 1) { column.col1 = columnName; }
                            else if (i == 2) { column.col2 = columnName; }
                            else if (i == 3) { column.col3 = columnName; }
                            else if (i == 4) { column.col4 = columnName; }
                            else if (i == 5) { column.col5 = columnName; }
                            else if (i == 6) { column.col6 = columnName; }
                            else if (i == 7) { column.col7 = columnName; }
                            else if (i == 8) { column.col8 = columnName; }
                            else if (i == 9) { column.col9 = columnName; }
                            else if (i == 10) { column.col10 = columnName; }
                            else if (i == 11) { column.col11 = columnName; }
                            else if (i == 12) { column.col12 = columnName; }
                        }
                        lstHead.Add(column);

                        for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                        {
                            ExeclField head = new ExeclField();
                            for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                            {
                                var columnName = ds.Tables[0].Rows[i][j].ToString() != "" ? ds.Tables[0].Rows[i][j].ToString() : "-";
                                if (j == 0) { head.col0 = columnName; }
                                else if (j == 1) { head.col1 = columnName; }
                                else if (j == 2) { head.col2 = columnName; }
                                else if (j == 3) { head.col3 = columnName; }
                                else if (j == 4) { head.col4 = columnName; }
                                else if (j == 5) { head.col5 = columnName; }
                                else if (j == 6) { head.col6 = columnName; }
                                else if (j == 7) { head.col7 = columnName; }
                                else if (j == 8) { head.col8 = columnName; }
                                else if (j == 9) { head.col9 = columnName; }
                                else if (j == 10) { head.col10 = columnName; }
                                else if (j == 11) { head.col11 = columnName; }
                                else if (j == 12) { head.col12 = columnName; }
                            }
                            lstHead.Add(head);
                        }
                    }
                    return Json(lstHead);
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }

        public class ExeclField
        {
            public string col0 { get; set; }
            public string col1 { get; set; }
            public string col2 { get; set; }
            public string col3 { get; set; }
            public string col4 { get; set; }
            public string col5 { get; set; }
            public string col6 { get; set; }
            public string col7 { get; set; }
            public string col8 { get; set; }
            public string col9 { get; set; }
            public string col10 { get; set; }
            public string col11 { get; set; }
            public string col12 { get; set; }
        }


        [Route("global/api/GetBudgetRequsitionDetailBymasterId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetBudgetRequsitionDetailBymasterId(int id)
        {
            return Json(await budgetRequsitionMasterService.GetBudgetRequsitionDetailBymasterId(id));
        }

        [Route("global/api/getAllColumnBySp/")]
        [HttpGet]
        public async Task<IActionResult> GetAllColumnBySp()
        {
            return Json(await budgetRequsitionMasterService.GetAllColumnBySp());
        }

        [Route("global/api/GetFiscalYearById/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetFiscalYearById(int id)
        {
            return Json(await budgetRequsitionMasterService.GetFiscalYearById(id));
        }

    }

}