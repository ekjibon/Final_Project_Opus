using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.REMS.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.REMS.Data.Entity;
using OPUSERP.REMS.Services.Interfaces;

namespace OPUSERP.Areas.REMS.Controllers
{
    [Area("REMS")]
    public class ReturnFromVendorController : Controller
    {
        private readonly IClaimRegisterService claimRegisterService;
        private readonly IClaimAssignService claimAssignService;
        private readonly IClaimAttachmentService claimAttachmentService;
        private readonly IClaimBillInformationService claimBillInformationService;
        private readonly IRepairTransactionLogService repairTransactionLogService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IUserInfoes userInfoes;

        public ReturnFromVendorController(IHostingEnvironment hostingEnvironment, IClaimRegisterService claimRegisterService, IClaimBillInformationService claimBillInformationService, IClaimAttachmentService claimAttachmentService, IUserInfoes userInfoes, IClaimAssignService claimAssignService, IRepairTransactionLogService repairTransactionLogService)
        {
            this._hostingEnvironment = hostingEnvironment;
            this.claimRegisterService = claimRegisterService;
            this.claimAttachmentService = claimAttachmentService;
            this.claimBillInformationService = claimBillInformationService;
            this.userInfoes = userInfoes;
            this.claimAssignService = claimAssignService;
            this.repairTransactionLogService = repairTransactionLogService;
        }

        public async Task<IActionResult> Index()
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);
            ClaimRegisterViewModel model = new ClaimRegisterViewModel
            {
                claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(1),
                claimAssignWarrentyViewModels = await claimAssignService.GetClaimAssignListByAssignTypeNew(userInfo.UserId, 5, 5),
                claimAssignNonWarrentyViewModels = await claimAssignService.GetClaimAssignListByAssignTypeNew(userInfo.UserId, 7, 7),
                claimReturnFromVendorViewModel = await claimAssignService.GetClaimAssignListByAssignTypeNew(userInfo.UserId, 8, 8),
            };
            return View(model);
        }

        // POST: RepairHead/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] ClaimRegisterViewModel model, IFormFile formFile)
        {
            if (!ModelState.IsValid)
            {
                model.claimRegisters = await claimRegisterService.GetClaimRegisterListByStatus(1);
                model.claimAssigns = await claimAssignService.GetClaimAssign();
                return View(model);
            }
            var user = await userInfoes.GetUserInfoByUser(User.Identity.Name);

            //return Json(model);

            ClaimAssign data = new ClaimAssign
            {
                claimId = model.claimId,
                problemDescription = model.ProblemDescription,
                assignUserId = model.AssignToId,
                assignTypeId = 8,
                assignDate = model.AssignDate,
                organizationId = model.assignToVendorId,
                remarks = "Return From Vendor After Repaire"
            };
            await claimAssignService.SaveClaimAssign(data);

            claimRegisterService.UpdateClaimRegister((int)model.claimId, 8);

            RepairTransactionLog repairTransactionLog = new RepairTransactionLog
            {
                claimId = (int)model.claimId,
                statusInfoId = 8,
                description = "Return From Vendor After Repaire"
            };
            await repairTransactionLogService.SaveRepairTransactionLog(repairTransactionLog);

            for (int i = 0; i < model.purposeList.Length; i++)
            {
                ClaimBillInformation billInformation = new ClaimBillInformation
                {
                    Id = 0,
                    claimId = model.claimId,
                    paymentHead = model.purposeList[i],
                    amount = model.amountList[i]
                };
                await claimBillInformationService.SaveClaimBillInformation(billInformation);
            }

            if (formFile != null)
            {
                string userName = User.Identity.Name;
                string documentType = "ClaimAttachment";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(formFile.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = documentType + "_" + model.claimId + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                ClaimAttachment attachment = new ClaimAttachment
                {
                    Id = 0,
                    claimId = model.claimId,
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                };
                await claimAttachmentService.SaveClaimAttachment(attachment);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}