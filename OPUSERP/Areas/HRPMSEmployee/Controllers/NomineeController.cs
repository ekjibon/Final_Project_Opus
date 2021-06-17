using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.HRPMSEmployee.Models;
using OPUSERP.Data.Entity.AttachmentComment;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.HRPMS.Data.Entity.Employee;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Authorize]
    [Area("HRPMSEmployee")]
    public class NomineeController : Controller
    {
        private readonly IPersonalInfoService personalInfoService;
        private readonly IPhotographService photographService;
        private readonly INomineeService nomineeService;
        private readonly INomineeDetailService nomineeDetailService;
        private readonly INomineeFundService nomineeFundService;
        public readonly IERPModuleService iERPModuleService;
        public readonly IAttachmentCommentService attachmentCommentService;
        private readonly IHostingEnvironment _hostingEnvironment;

        public NomineeController(IPersonalInfoService personalInfoService, IPhotographService photographService, INomineeService nomineeService, INomineeDetailService nomineeDetailService, INomineeFundService nomineeFundService, IERPModuleService iERPModuleService, IAttachmentCommentService attachmentCommentService, IHostingEnvironment hostingEnvironment)
        {
            this.personalInfoService = personalInfoService;
            this.photographService = photographService;
            this.nomineeService = nomineeService;
            this.nomineeDetailService = nomineeDetailService;
            this.nomineeFundService = nomineeFundService;
            this.iERPModuleService = iERPModuleService;
            this.attachmentCommentService = attachmentCommentService;
            this._hostingEnvironment = hostingEnvironment;
        }

        #region Nominee

        public async Task<IActionResult> Index(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new NomineeViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                nomineeFunds = await nomineeFundService.GetNomineeFund(),
                nominees = await nomineeService.GetNomineeByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] NomineeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.employeeID = model.employeeID;
                model.nomineeFunds = await nomineeFundService.GetNomineeFund();
                model.nominees = await nomineeService.GetNomineeByEmpId((int)model.employeeID);
                model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
                return View(model);
            }

            Nominee data = new Nominee
            {
                Id = model.nomineeID ?? 0,
                employeeID = (int)model.employeeID,
                name = model.name,
                relation = model.relation,
                address = model.address,
                contact = model.contact,
                guardianName = model.guardianName,
                witnessName = model.witnessName,
                nomineeDate=model.nomineeDate
            };

            int maxId = await nomineeService.SaveNominee(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);
            await nomineeDetailService.DeleteNomineeDetailByNomineeId(maxId);
            for (int i = 0; i < model.fundTypeList.Length; i++)
            {
                NomineeDetail nomineeDetail = new NomineeDetail
                {
                    Id = 0,
                    nomineeId = maxId,
                    nomineeFundId = model.fundTypeList[i],
                    persentence = model.fundValueList[i]
                };
                await nomineeDetailService.SaveNomineeDetail(nomineeDetail);
            }
            
            return RedirectToAction("Index", "Nominee", new
            {
                id = (int)model.employeeID
            });
        }

        public async Task<IActionResult> Delete(int id, int empId)
        {
            await nomineeDetailService.DeleteNomineeDetailByNomineeId(id);
            await nomineeService.DeleteNomineeById(id);
            return RedirectToAction("Index", "Nominee", new
            {
                id = empId
            });
        }

        #endregion

        #region Employee Insurance

        public async Task<IActionResult> EmployeeInsurance(int id)
        {
            ViewBag.employeeID = id.ToString();
            var model = new EmployeeInsuranceViewModel
            {
                employeeID = id,
                photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                employeeInfo = await personalInfoService.GetEmployeeInfoById(id),
                employeeInsurances = await nomineeService.GetEmployeeInsuranceByEmpId(id),
                employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeeInsurance([FromForm] EmployeeInsuranceViewModel model)
        {
            //if (!ModelState.IsValid)
            //{
            //    ViewBag.employeeID = model.employeeID;                
            //    model.employeeInsurances = await nomineeService.GetEmployeeInsuranceByEmpId((int)model.employeeID);
            //    model.employeeNameCode = await personalInfoService.GetEmployeeNameCodeById((int)model.employeeID);
            //    return View(model);
            //}

            //string fileName = String.Empty;
            //string fileNameMain = String.Empty;
            //string message = FileSave.SaveEmpAttachment(out fileName, model.fileUrl);

            //if (message == "success")
            //{
            //    fileNameMain = fileName;
            //}

            EmployeeInsurance data = new EmployeeInsurance
            {
                Id = model.insuranceID ?? 0,
                employeeInfoId = (int)model.employeeID,
                name = model.name,
                relation = model.relation,
                NID = model.NID,
                gender = model.gender,
                dateOfBirth = model.dateOfBirth,
                insuranceDate = model.insuranceDate
                //imageUrl = ""
            };

            int insuranceId = await nomineeService.SaveEmployeeInsurance(data);
            await personalInfoService.UpdateEmployeeinfoById((int)model.employeeID);

            #region Attachment

            var module = await iERPModuleService.GetERPModuleByModuleName("HRM");
            int moduleId = module.Id;

            if (model.imagePath_Challan != null)
            {
                string userName = User.Identity.Name;
                string documentType = "photo";
                var filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/Photo";
                var fileName = ContentDispositionHeaderValue.Parse(model.imagePath_Challan.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = insuranceId + "_" + documentType + "_" + fileName;
                string fileLocation = "/Upload/Photo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await model.imagePath_Challan.CopyToAsync(stream);
                }

                DocumentPhotoAttachment dataF = new DocumentPhotoAttachment
                {
                    Id = 0,
                    actionType = "EmpInsurance",
                    actionTypeId = insuranceId,
                    subject = "",
                    fileName = NewFileName,
                    filePath = fileLocation,
                    fileType = fileType,
                    description = "",
                    documentType = documentType,
                    moduleId = moduleId,
                    createdBy = userName
                };
                await attachmentCommentService.SaveDocumentAttachment(dataF);
            }

            #endregion

            return RedirectToAction("EmployeeInsurance", "Nominee", new
            {
                id = (int)model.employeeID
            });
        }
        

        public async Task<IActionResult> DeleteEmployeeInsuranceById(int Id, int empId)
        {
            await nomineeService.DeleteEmployeeInsuranceById(Id);
            return RedirectToAction("EmployeeInsurance", "Nominee", new
            {
                id = empId
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetInsurancePhotoById(int Id)
        {
            var module = await iERPModuleService.GetERPModuleByModuleName("HRM");
            int moduleId = module.Id;

            return Json(await attachmentCommentService.GetDocumentAttachmentByActionIdContact(Id, "EmpInsurance", "photo", moduleId));
        }

        #endregion

        #region API Section
        [Route("global/api/getNomineeDetailByNomineeId/{nomineeId}")]
        [HttpGet]
        public async Task<IActionResult> GetNomineeDetailByNomineeId(int nomineeId)
        {
            return Json(await nomineeDetailService.GetNomineeDetailByNomineeId(nomineeId));
        }

        [Route("global/api/getNomineeRemainingFunByEmpIdAndFundId/{empId}/{fundId}")]
        [HttpGet]
        public async Task<IActionResult> GetNomineeRemainingFunByEmpIdAndFundId(int empId, int fundId)
        {
            return Json(await nomineeDetailService.GetNomineeRemainingFunByEmpIdAndFundId(empId, fundId));
        }
        #endregion
    }
}