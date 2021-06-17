using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRO.Models;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRO.Data.Entity.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob;
using OPUSERP.CRO.Services.DistributeJob.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.CRO.Controllers
{
    [Area("CRO")]
    public class JobArchiveController : Controller
    {
        private readonly IDistributeJobService distributeJobService;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IBankBranchService bankBranchService;
        private readonly IBankBranchService lbankBranchService;
        public JobArchiveController(IDistributeJobService distributeJobService, IBankBranchService lbankBranchService, IPersonalInfoService personalInfoService, IBankBranchService bankBranchService)
        {
            this.distributeJobService = distributeJobService;
            this.personalInfoService = personalInfoService;
            this.bankBranchService = bankBranchService;
            this.lbankBranchService = lbankBranchService;
        }


        public IActionResult ArchivePreview(int id)
        {
         
            return View();
        }


        public async Task<IActionResult> Index()
        {   
            JobArchiveViewModel model = new JobArchiveViewModel
            {
                operationMasters = await distributeJobService.GetAllOperationMaster(),
                iRCRatings = await distributeJobService.GetAllIRCRating(),
                archiveFloors = await distributeJobService.GetAllArchiveFloor(),
                employeeInfos=await personalInfoService.GetEmployeeInfo(),
                leadsBankInfos = await bankBranchService.GetLeadsBankInfo()


            };
            return View(model);
        }

        [HttpPost]
        public async Task<JsonResult> Index([FromForm] JobArchiveViewModel model)
        {

            string userName = User.Identity.Name;
            int jobStatusId = 48;
            if (model.archieveId==1)
            {
                jobStatusId = 2054;
            }
            if (model.softCopyId != null)
            {
                if (model.softCopyId.Length > 0)
                {
                    for (int i = 0; i < model.softCopyId.Length; i++)
                    {
                        await distributeJobService.UpdateAttachmentStatusArchieved(Convert.ToInt32(model.softCopyId[i]), 1);

                    }

                }
            }

            if (model.operationMasterId != null)
            {
                
                if (model.archieveId == 1)
                {
                    await distributeJobService.UpdateOperationMasterArchieved(Convert.ToInt32(model.operationMasterId), jobStatusId);

                    StatusLog log = new StatusLog();
                    log.Id = 0;
                    log.operationMasterId = Convert.ToInt32(model.operationMasterId);
                    log.jobStatusId = jobStatusId;
                    log.remarks = "";
                    log.currentUser = userName;
                    log.createdAt = DateTime.Now;
                    log.createdBy = userName;
                    await distributeJobService.SaveStatusLog(log);
                    log = new StatusLog();
                }

                var archivelist = await distributeJobService.GetArchiveByOperationMasterId(Convert.ToInt32(model.operationMasterId));
                int archiveId = 0;
                if(archivelist.Count()>0)
                {
                    archiveId = archivelist.FirstOrDefault().Id;
                }
                Archive archive = new Archive();
                archive.Id = archiveId;
                archive.operationMasterId = Convert.ToInt32(model.operationMasterId);
                archive.reportPrintDate = model.reportPrintDate ;
                archive.archiveDate = model.archiveDate;
                archive.ratingDate = model.ratingDate;
                archive.ratingValidTillDate = model.ratingValidTillDate;
                archive.ratingValidDays = model.ratingValidDays;
                archive.archiveLocationSoftcopy = model.archiveLocationSoftcopy;
                archive.archiveShelfId = model.archiveShelfId;
                archive.hardCopyNewFileNo = model.hardCopyNewFileNo;
                archive.hardCopyOldFileNo = model.hardCopyOldFileNo;
                archive.hardWorkingFile = model.hardWorkingFile;
                archive.softWorkingFile = model.softWorkingFile;
                archive.archiveFloorId = model.archiveFloorId;
                archive.createdAt = DateTime.Now;
                archive.createdBy = userName;
                await distributeJobService.SaveArchive(archive);
                archive = new Archive();
                
            }


            return Json(1);

        }
        [HttpPost]
        public async Task<JsonResult> saveHardCopy([FromForm] JobArchiveViewModel model)
        {

            string userName = User.Identity.Name;
            int jobStatusId = 48;
            if (model.archieveId == 1)
            {
                jobStatusId = 2054;
            }
            if (model.hardCopyId != null)
            {
                if (model.hardCopyId.Length > 0)
                {
                    for (int i = 0; i < model.hardCopyId.Length; i++)
                    {
                        await distributeJobService.UpdateReceiveDocumentArchieved(Convert.ToInt32(model.hardCopyId[i]), 1);

                    }

                }
            }

            if (model.operationMasterId != null)
            {

                if (model.archieveId == 1)
                {
                    await distributeJobService.UpdateOperationMasterArchieved(Convert.ToInt32(model.operationMasterId), jobStatusId);

                    StatusLog log = new StatusLog();
                    log.Id = 0;
                    log.operationMasterId = Convert.ToInt32(model.operationMasterId);
                    log.jobStatusId = jobStatusId;
                    log.remarks = "";
                    log.currentUser = userName;
                    log.createdAt = DateTime.Now;
                    log.createdBy = userName;
                    await distributeJobService.SaveStatusLog(log);
                    log = new StatusLog();
                }

                var archivelist = await distributeJobService.GetArchiveByOperationMasterId(Convert.ToInt32(model.operationMasterId));
                int archiveId = 0;
                if (archivelist.Count() > 0)
                {
                    archiveId = archivelist.FirstOrDefault().Id;
                }
                Archive archive = new Archive();
                archive.Id = archiveId;
                archive.operationMasterId = Convert.ToInt32(model.operationMasterId);
                archive.reportPrintDate = model.reportPrintDate;
                archive.archiveDate = model.archiveDate;
                archive.ratingDate = model.ratingDate;
                archive.ratingValidTillDate = model.ratingValidTillDate;
                archive.ratingValidDays = model.ratingValidDays;
                archive.archiveLocationSoftcopy = model.archiveLocationSoftcopy;
                archive.archiveShelfId = model.archiveShelfId;
                archive.hardCopyNewFileNo = model.hardCopyNewFileNo;
                archive.hardCopyOldFileNo = model.hardCopyOldFileNo;
                archive.hardWorkingFile = model.hardWorkingFile;
                archive.softWorkingFile = model.softWorkingFile;
                archive.archiveFloorId = model.archiveFloorId;
                archive.createdAt = DateTime.Now;
                archive.createdBy = userName;
                await distributeJobService.SaveArchive(archive);
                archive = new Archive();

            }


            return Json(1);

        }
        [HttpGet]
        public async Task<IActionResult> GetArchiveShelfByarchiveFloorId(int Id)
        {
            return Json(await distributeJobService.GetArchiveShelfByarchiveFloorId(Id));
        }

        [HttpGet]
        public async Task<IActionResult> GetOperationmasterdata()
        {
           string fromdate = string.Empty;
           string todate = string.Empty;
            return Json(await distributeJobService.OperationMasterSPViewModels(fromdate, todate));
        }
        [Route("global/api/GetOperationmasterdata/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> GetOperationmasterdata(string fromdate,string todate)
        {
            if (fromdate != null)
            {
                fromdate = Convert.ToDateTime(fromdate).ToString("yyyyMMdd");
                todate = Convert.ToDateTime(todate).ToString("yyyyMMdd");
            }
            else
            {
                fromdate = string.Empty;
                todate = string.Empty;
            }
            var data = await distributeJobService.OperationMasterSPViewModels(fromdate, todate);
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetOperationmasterdatacom()
        {
           string fromdate = string.Empty;
           string todate = string.Empty;
            return Json(await distributeJobService.OperationMasterSPViewModelscom(fromdate, todate));
        }
        [Route("global/api/GetOperationmasterdatacom/{fromDate}/{toDate}")]
        [HttpGet]
        public async Task<IActionResult> GetOperationmasterdatacom(string fromdate,string todate)
        {
            if (fromdate != null)
            {
                fromdate = Convert.ToDateTime(fromdate).ToString("yyyyMMdd");
                todate = Convert.ToDateTime(todate).ToString("yyyyMMdd");
            }
            else
            {
                fromdate = string.Empty;
                todate = string.Empty;
            }
            var data = await distributeJobService.OperationMasterSPViewModelscom(fromdate, todate);
            return Json(data);
        }
        [HttpGet]
        public async Task<IActionResult> GetArchiveByOperationMasterId(int Id)
        {
            return Json(await distributeJobService.GetArchiveByOperationMasterId(Id));
        }
        public JsonResult DateDifference(string ratingDateF, string ratingTillDateT)
        {
            int days = 0;
            if (ratingDateF != "" && ratingTillDateT != "")
            {
                DateTime ratingDate = DateTime.Parse(ratingDateF);
                DateTime ratingTillDate = DateTime.Parse(ratingTillDateT);

                TimeSpan ts = ratingTillDate - ratingDate;
                days = ts.Days;
            }
            return Json(days);

        }
        public async Task<IActionResult> ArchiveCheck()
        {

            JobArchiveViewModel model = new JobArchiveViewModel
            {
                operationMasters = await distributeJobService.GetAllOperationMaster(),
                iRCRatings = await distributeJobService.GetAllIRCRating(),
                archiveFloors = await distributeJobService.GetAllArchiveFloor(),
                employeeInfos = await personalInfoService.GetEmployeeInfo(),
                leadsBankInfos = await bankBranchService.GetLeadsBankInfo()

            };
            return View(model);

        }
        public async Task<IActionResult> ArchiveView()
        {

            JobArchiveViewModel model = new JobArchiveViewModel
            {
                operationMasters = await distributeJobService.GetAllOperationMaster(),
                iRCRatings = await distributeJobService.GetAllIRCRating(),
                archiveFloors = await distributeJobService.GetAllArchiveFloor(),
                leadsBankInfos = await lbankBranchService.GetLeadsBankInfo(),
                employeeInfos = await personalInfoService.GetEmployeeInfo()

            };
            return View(model);

        }
        [HttpPost]
        public async Task<JsonResult> ArchiveCheched([FromForm] JobArchiveViewModel model)
        {

            string userName = User.Identity.Name;
            int jobStatusId = 2059;
           

            if (model.operationMasterId != null)
            {

               
                    await distributeJobService.UpdateOperationMasterArchieved(Convert.ToInt32(model.operationMasterId), jobStatusId);

                    StatusLog log = new StatusLog();
                    log.Id = 0;
                    log.operationMasterId = Convert.ToInt32(model.operationMasterId);
                    log.jobStatusId = jobStatusId;
                    log.remarks = "";
                    log.currentUser = userName;
                    log.createdAt = DateTime.Now;
                    log.createdBy = userName;
                    await distributeJobService.SaveStatusLog(log);
                    log = new StatusLog();
             

            }


            return Json(1);

        }

    }
}