using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Budget.Models;
using OPUSERP.Budget.Data.Entity;
using OPUSERP.Budget.Service.Interface;
using OPUSERP.ERPService.AuthService.Interfaces;
using Microsoft.AspNetCore.Http;
using OPUSERP.Helpers;
using System.Dynamic;
using OPUSERP.Areas.Budget.Models.Lang;
using Microsoft.AspNetCore.Hosting;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using OPUSERP.Areas.SCMMatrix.Models;
using DinkToPdf.Contracts;

namespace OPUSERP.Areas.Budget.Controllers
{
    [Authorize]
    [Area("Budget")]
    public class HOBudgetRequisitionController : Controller
    {
        private readonly LangGenerate<BudgetRequisitionLn> _lang;
        private readonly LangGenerate<BudgetRequisitionExcelLn> _lang1;
        private readonly IHOBudgetRequsitionService hOBudgetRequsitionService;
        private readonly IBudgetRequsitionMasterService budgetRequsitionMasterService;
        private readonly IBudgetHeadService budgetHeadService;
        private readonly IUserInfoes userInfo;
        private readonly IApprovalLogService approvalLogService;
        private readonly IApprovalMatrixService approvalMatrixService;

        private readonly string rootPath;
        private readonly MyPDF myPDF;

        public HOBudgetRequisitionController(IHostingEnvironment hostingEnvironment, IConverter converter, IHOBudgetRequsitionService hOBudgetRequsitionService, IUserInfoes userInfo, IBudgetHeadService budgetHeadService, IApprovalLogService approvalLogService, IApprovalMatrixService approvalMatrixService, IBudgetRequsitionMasterService budgetRequsitionMasterService)
        {
            _lang = new LangGenerate<BudgetRequisitionLn>(hostingEnvironment.ContentRootPath);
            _lang1 = new LangGenerate<BudgetRequisitionExcelLn>(hostingEnvironment.ContentRootPath);
            this.hOBudgetRequsitionService = hOBudgetRequsitionService;
            this.budgetHeadService = budgetHeadService;
            this.userInfo = userInfo;
            this.approvalLogService = approvalLogService;
            this.approvalMatrixService = approvalMatrixService;
            this.budgetRequsitionMasterService = budgetRequsitionMasterService;

            myPDF = new MyPDF(hostingEnvironment, converter);
            rootPath = hostingEnvironment.ContentRootPath;
        }

        public async Task<IActionResult> Index(int? id)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var plan = await hOBudgetRequsitionService.GetBudgetRequsitionMaster();
            string productionNo = ("HOBR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();
            
            HOBudgetRequsitionMaster masterInfoes = new HOBudgetRequsitionMaster();
            FiscalYear fiscalYear = new FiscalYear();
            if (id > 0)
            {
                var master = await hOBudgetRequsitionService.GetBudgetRequsitionMasterById(Convert.ToInt32(id));
                masterInfoes.Id = master.Id;
                masterInfoes.requsitionNo = master.requsitionNo;
                masterInfoes.requsitionDate = master.requsitionDate;
                masterInfoes.fiscalYearId = master.fiscalYearId;
                masterInfoes.status = master.status;
               
                fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(Convert.ToInt32(masterInfoes.fiscalYearId));
            }
            else
            {
                masterInfoes.Id = 0;
                masterInfoes.requsitionNo = productionNo;
                masterInfoes.requsitionDate = DateTime.Now;
                fiscalYear = new FiscalYear();
            }

            HOBudgetRequsitionViewModel model = new HOBudgetRequsitionViewModel
            {
                flang = _lang.PerseLang("Budget/BudgetRequisitionEN.json", "Budget/BudgetRequisitionBN.json", Request.Cookies["lang"]),
                reqId = masterInfoes.Id,
                Number = masterInfoes.requsitionNo,
                Date = masterInfoes.requsitionDate,
                year = masterInfoes.fiscalYearId,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                fiscalYear=fiscalYear,
                budgetRequsitionMasters = await budgetRequsitionMasterService.GetBudgetRequsitionMaster(),
                hOBudgetRequsitionDetails = await hOBudgetRequsitionService.GetBudgetRequsitionDetailBymasterId(Convert.ToInt32(id))
        };
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Index([FromForm] HOBudgetRequsitionViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            //return Json(model);
          
            var plan = await hOBudgetRequsitionService.GetBudgetRequsitionMaster();
            string productionNo = ("HOBR/" + DateTime.Now.Month + "/" + DateTime.Now.Year + "/" + (plan.Count() + 1)).ToString();

            if (model.reqId > 0)
            {
                productionNo = model.Number;
            }
        
            HOBudgetRequsitionMaster master = new HOBudgetRequsitionMaster
            {
                Id = Convert.ToInt32(model.reqId),
                requsitionNo = productionNo,
                requsitionDate = model.Date,
                fiscalYearId=model.year,
                status = 1,
                RequsitionBy = userInfos.UserId
            };
            int masterId = await hOBudgetRequsitionService.SaveBudgetRequsitionMaster(master);

            //await hOBudgetRequsitionService.PROCESS_HOBudgetRequsitionDetails(Convert.ToInt32(model.year), masterId);

            if (model.reqId > 0)
            {
                await hOBudgetRequsitionService.DeleteBudgetRequsitionDetailBymasterId(Convert.ToInt32(model.reqId));
            }
            for (int i = 0; i < model.heads.Length; i++)
            {
                HOBudgetRequsitionDetail details = new HOBudgetRequsitionDetail
                {
                    Id = 0,
                    hOBudgetRequsitionMasterId = masterId,
                    budgetHeadId = model.heads[i],
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
                await hOBudgetRequsitionService.SaveBudgetRequsitionDetail(details);
            }

            budgetRequsitionMasterService.UpdateBudgetRequsitionIsProcessByYear(Convert.ToInt32(model.year));

            IEnumerable<ApproverPanelViewModel> lstApproverPanel  = await approvalMatrixService.GetPRApproverPanelList(userName, 2, 1);
            if (model.reqId == 0 && lstApproverPanel.Count() > 0)
            {
                int i = 0;
                //string notes = "";
                foreach (ApproverPanelViewModel panels in lstApproverPanel)
                {
                    int isactive = 0;
                    int? nextAppId = 0;
                    if (i == 1)
                    {
                        isactive = 1;
                        nextAppId = 0;
                        //notes = "";
                    }
                    else if (i == 0)
                    {
                        isactive = 0;
                        nextAppId = panels.nextApproverId;
                        //notes = "Created";
                    }
                    else
                    {
                        isactive = 0;
                        nextAppId = 0;
                        //notes = "";
                    }
                    ApprovalLog appLog = new ApprovalLog
                    {
                        masterId = masterId,
                        matrixTypeId = 2,
                        userId = Convert.ToInt32(panels.nextApproverId),
                        sequenceNo = Convert.ToInt32(panels.sequenceNo),
                        isActive = isactive,
                        nextApproverId = nextAppId
                    };
                    i= i+ 1;
                    await approvalLogService.SaveApproverLog(appLog);
                }
            }
            
            return RedirectToAction(nameof(BudgetRequisitionList));
        }

        public async Task<IActionResult> BudgetRequisitionList()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            HOBudgetRequsitionViewModel model = new HOBudgetRequsitionViewModel
            {
                hOBudgetRequsitionMasters = await hOBudgetRequsitionService.GetBudgetRequsitionMasterByUserId((int)userInfos.UserId)
            };
            return View(model);
        }



        //[Route("global/api/GetBudgetRequsitionDetailBymasterId/{id}")]
        //[HttpGet]
        //public async Task<IActionResult> GetBudgetRequsitionDetailBymasterId(int id)
        //{
        //    return Json(await budgetRequsitionMasterService.GetBudgetRequsitionDetailBymasterId(id));
        //}

        //[Route("global/api/getAllColumnBySp/")]
        //[HttpGet]
        //public async Task<IActionResult> GetAllColumnBySp()
        //{
        //    return Json(await budgetRequsitionMasterService.GetAllColumnBySp());
        //}

        [Route("global/api/PROCESSHOBudgetRequsition/{id}")]
        [HttpGet]
        public async Task<IActionResult> PROCESSHOBudgetRequsition(int id)
        {
            return Json(await hOBudgetRequsitionService.PROCESSHOBudgetRequsition(id));
        }

        [AllowAnonymous]
        public IActionResult RequisitionPreviewPdf(int id)
        {
            string fileName;
            string host = HttpContext.Request.Host.ToString();
            string scheme = Request.Scheme;
            string url = string.Empty;
            url = $"" + scheme + "://" + host + "/Budget/HOBudgetRequisition/RequisitionPreview?id=" + id;

            string status = myPDF.GeneratePDF(out fileName, url);

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
            IEnumerable<HOBudgetRequsitionDetail> details = new List<HOBudgetRequsitionDetail>();
            HOBudgetRequsitionMaster masterInfoes = new HOBudgetRequsitionMaster();

            string userName = HttpContext.User.Identity.Name;
            var userInfos = await userInfo.GetUserInfoByUser(userName);

            var master = await hOBudgetRequsitionService.GetBudgetRequsitionMasterById(Convert.ToInt32(id));
            masterInfoes.Id = master.Id;
            masterInfoes.requsitionNo = master.requsitionNo;
            masterInfoes.requsitionDate = master.requsitionDate;
            masterInfoes.fiscalYearId = master.fiscalYearId;
            masterInfoes.status = master.status;
            details = await hOBudgetRequsitionService.GetBudgetRequsitionDetailBymasterId(Convert.ToInt32(id));
            FiscalYear fiscalYear = new FiscalYear();
            fiscalYear = await budgetRequsitionMasterService.GetFiscalYearById(Convert.ToInt32(master.fiscalYearId));

            int reqBy = Convert.ToInt32(master.RequsitionBy);

            IEnumerable<ApproverPanelViewModel> lstApproverPanel = await approvalMatrixService.GetPRApproverPanelByUserId(reqBy, id);

            HOBudgetRequsitionViewModel model = new HOBudgetRequsitionViewModel
            {
                flang = _lang.PerseLang("Budget/BudgetRequisitionEN.json", "Budget/BudgetRequisitionBN.json", Request.Cookies["lang"]),
                reqId = masterInfoes.Id,
                Number = masterInfoes.requsitionNo,
                Date = masterInfoes.requsitionDate,
                year = masterInfoes.fiscalYearId,
                fiscalYears = await budgetRequsitionMasterService.GetFiscalYear(),
                budgetRequsitionDetails = details,
                fiscalYear = fiscalYear,
                approerPanelList = lstApproverPanel
            };
            return View(model);
        }

    }

}