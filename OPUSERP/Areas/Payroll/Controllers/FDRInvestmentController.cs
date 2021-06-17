using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Payroll.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.Payroll.Data.Entity.PF;
using OPUSERP.Payroll.Data.Entity.Salary;
using OPUSERP.Payroll.Services.PF.Interfaces;
using OPUSERP.Payroll.Services.Salary.Interfaces;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OPUSERP.Areas.Payroll.Controllers
{
    [Area("Payroll")]
    public class FDRInvestmentController : Controller
    {
        // GET: /<controller>/
        private readonly IPFService pFService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IAttachmentCommentService attachmentCommentService;
        private readonly ISalaryService salaryService;
        private readonly IPersonalInfoService personalInfoService;
        public FDRInvestmentController(ISalaryService salaryService,IPFService pFService, IHostingEnvironment hostingEnvironment, IAttachmentCommentService attachmentCommentService, IPersonalInfoService personalInfoService)
        {
            this.pFService = pFService;
            this._hostingEnvironment = hostingEnvironment;
            this.attachmentCommentService = attachmentCommentService;
            this.salaryService = salaryService;
            this.personalInfoService = personalInfoService;
        }
        #region PFloan
        public async Task<ActionResult> PFLoan()
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            PFLoanViewModel model = new PFLoanViewModel
            {
                pFLoans =await pFService.GetAllPFLoan(),
                salaryHeads= salaryHead.Where(a => a.isAdvance == "Yes"),
                salaryPeriods=await salaryService.GetAllSalaryPeriod(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
            };
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PFLoan([FromForm] PFLoanViewModel model)
        {

            var loandata = await pFService.GetAllPFLoan();
            var count = loandata.Count() + 1;
            string LoanNo = "PFLoan/" + Convert.ToDateTime(DateTime.Now).ToString("dd-MM-yyyy") + "/" +count.ToString();
            if (model.PfloanId > 0)
            {
                LoanNo = model.PfLoanNo;
            }
            PFLoan data = new PFLoan
            {
                Id = model.PfloanId,
                employeeInfoId = model.employeeInfoId,
                salaryPeriodId = model.salaryPeriodId,
                salaryHeadId = model.salaryHeadId,
                advanceAmount = model.advanceAmount,
                installmentAmount = model.installmentAmount,
                noOfInstallment = model.noOfInstallment,
                isFromSalary = model.isFromSalary,
                PFLoanNo = LoanNo,
                loanDate=model.loanDate,
                purpose = model.purpose
            };

           int pfid= await pFService.SavePFAdvance(data);
            try
            {
                await pFService.DeletePFLoanScheduleByPFId(pfid);
                int periodid = model.salaryPeriodId;
                for (int i = 1; i <= model.noOfInstallment; i++)
                {
                    PFLoanSchedule datas = new PFLoanSchedule
                    {
                        Id = 0,
                        pFLoanId = pfid,
                        salaryPeriodId = periodid,
                        installmentAmount = model.installmentAmount,
                        noOfInstallment = i,
                        restAmount=model.advanceAmount-model.installmentAmount*i,
                        isComplete=0

                    };
                    await pFService.SavePFLoanSchedule(datas);
                    periodid++;
                }
            }
            catch (Exception ex){
                throw ex;
            }
            
            return RedirectToAction(nameof(PFLoan));
        }

        #endregion
        #region PFloanPayment
        public async Task<ActionResult> PFLoanPayment()
        {
            var salaryHead = await salaryService.GetAllSalaryHeadByFilter(string.Empty);
            PFLoanPaymentViewModel model = new PFLoanPaymentViewModel
            {
                pFLoanPayments = await pFService.GetAllPFLoanPayment(),
                visualEmpCodeName = await personalInfoService.GetEmpCodeNameVisualData()
                //salaryHeads = salaryHead.Where(a => a.isAdvance == "Yes"),
                //salaryPeriods = await salaryService.GetAllSalaryPeriod()

            };
            return View(model);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PFLoanPayment([FromForm] PFLoanPaymentViewModel model)
        {


            PFLoanPayment data = new PFLoanPayment
            {
                Id = model.PfloanpaymentId,
                employeeInfoId = model.employeeInfoId,
                paymentAmount = model.paymentAmount,
                paymentDate = model.paymentDate,
                pFLoanId=model.pFLoanId
               
            };

            int pfid = await pFService.SavePFLoanPayment(data);
            try
            {

                await pFService.UpdatePFScheduleByPaymentId(pfid);
               await pFService.UpdatePFSchedulePaymentByPaymentId((int)model.pFLoanId,(int)model.installmentno,pfid);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return RedirectToAction(nameof(PFLoanPayment));
        }

        #endregion
        public async Task<ActionResult> Index()
        {
            var docInfo = await attachmentCommentService.GetDocumentAttachmentList("FDRInvestment", "ChecqueFile", 10);
            if (docInfo == null)
            {
                docInfo = new List<DocumentPhotoAttachment>();
            }
            FDRInvestmentViewModel model = new FDRInvestmentViewModel
            {

                fdrInvestments=await pFService.GetAllFdrInvestment(),
                documentPhotoAttachments=docInfo
            };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] FDRInvestmentViewModel model)
        {


            FdrInvestment data = new FdrInvestment
            {
                Id = (int)model.FdrInvestmentId,
                FromBank = model.FromBank,
                FromBranch = model.FromBranch,
                AccountNumber = model.AccountNumber,
                ToBank=model.ToBank,
                ToBranch=model.ToBranch,
                ChequeNo=model.ChequeNo,
                OpenDate=model.OpenDate,
                MaturityDate=model.MaturityDate,
                Amount=model.Amount,
                Rate=model.Rate,
                Interest=model.Interest,
                Tenure=model.Tenure
            };

          int fdrid=  await pFService.SaveFdrInvestment(data);

            if (model.ChequeFile != null)
            {

                string userName = User.Identity.Name;
                string documentType = "ChecqueFile";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(model.ChequeFile.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = fdrid + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await model.ChequeFile.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataF = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "FDRInvestment",
                    actionTypeId = fdrid,
                    subject = "",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "",
                    documentType = documentType,
                    moduleId = 10,
                    createdBy = userName
                };
                await attachmentCommentService.SaveDocumentAttachment(dataF);

            }

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> GetContactphotoByContactId(int Id)
        {
            return Json(await attachmentCommentService.GetDocumentAttachmentByActionIdContact(Id, "FDRInvestment", "ChecqueFile", 10));
        }
        [Route("global/api/GetAllScheduleByPFloanId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllScheduleByPFloanId(int id)
        {
            return Json(await pFService.GetLoanScheduleViewModel(id));
        }
        [Route("global/api/GetAllPFLoanByPFEmployeeId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetAllPFLoanByPFEmployeeId(int id)
        {
            return Json(await pFService.GetPFLoanByemployeeId(id));
        }
        [Route("global/api/GetPFLoanInfoByPFId/{id}")]
        [HttpGet]
        public async Task<IActionResult> GetPFLoanInfoByPFId(int id)
        {
            return Json(await pFService.GetPFLoanById(id));
        }
    }
}
