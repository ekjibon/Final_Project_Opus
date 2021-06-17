using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    public class ResultController : Controller
    {
        private readonly IMarksEntryService marksEntryService;
        public ResultController(IHostingEnvironment hostingEnvironment, IMarksEntryService marksEntryService)
        {
            this.marksEntryService = marksEntryService;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}

        public async Task<IActionResult> MarksEntry()
        {
            MarksEntryViewModel model = new MarksEntryViewModel
            {
                resultMasters = await marksEntryService.GetMarks()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MarksEntry([FromForm] MarksEntryViewModel model)
        {
            //var GRNumber = await stockService.GetGRNumber();
            if (!ModelState.IsValid || model.detailids == null)
            {
                //model.PurchaseOrderMaster = await purchaseOrderService.GetPurchaseOrderMasterById((int)model.purchaseId);
                //model.stockItemViewModels = await stockService.GetDueStockPurchaseDetailsInvoiceList((int)model.purchaseId);
                if (model.detailids == null)
                {
                    ModelState.AddModelError(string.Empty, "You have to Add minimum 1 Item");
                }
                return View(model);
            }

            //return Json(model);

            if (model.detailids != null)
            {
                ResultMaster data = new ResultMaster
                {
                    Id = Convert.ToInt32(model.marksId),
                    candidateInfoId = (int)model.candidateId,
                    jobRequisitionId = (int)model.reqId,
                    examNumber = model.examOrder,
                    remarks = model.remarks,
                };
                var masterId = await marksEntryService.SaveMarks(data);

                for (var i = 0; i < model.detailids.Length; i++)
                {
                    //PurchasedetailsId = (int)model.detailids[i];
                    ResultDetails data1 = new ResultDetails
                    {
                        resultId = masterId,
                        examTypeId = (int)model.examTypeId[i],
                        marks = model.marks[i],
                        percentage = model.percentage[i],
                        status = model.status[i],
                    };
                    await marksEntryService.SaveMarksDetails(data1);
                }

                //var stockMaster = await stockService.GetStockMasterById(masterId);
                //string empNameCode = user.EmpCode + "-" + user.EmpName;
                //await requisitionStatusHistory.SaveRequisitionStatusLog(Convert.ToInt32(stockMaster.purchase.cSMaster.requisitionId), 4, Convert.ToInt32(user.UserTypeId), user.UserId, empNameCode, model.remarks, "", 20, "GRPO", masterId, GRNumber);

            }
            return RedirectToAction(nameof(MarksEntry));
        }
    }
}