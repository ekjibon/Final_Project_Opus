using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.Data.Entity;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    public class CVCollectionController : Controller
    {
        private readonly ICandidateInfoService candidateInfoService;
        private readonly IUpdateCandidateInfoService updateCandidateInfoService;
        private readonly IJobRequisitionService jobRequisitionService;

        public CVCollectionController(ICandidateInfoService candidateInfoService, IUpdateCandidateInfoService updateCandidateInfoService, IJobRequisitionService jobRequisitionService, UserManager<ApplicationUser> userManager)
        {
            this.candidateInfoService = candidateInfoService;
            this.updateCandidateInfoService = updateCandidateInfoService;
            this.jobRequisitionService = jobRequisitionService;
        }
        public async Task<IActionResult> Index(int id)
        {
            ViewBag.candidateId = id;
            CandidateInfoViewModel model = new CandidateInfoViewModel
            {
                candidateId = id,
                candidateInfo = await candidateInfoService.GetCandidateInfoById(id),
                candidateInfos = await candidateInfoService.GetCandidateInfo()
            };
            return View(model);
        }

        // POST: CandidateInfo/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] CandidateInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.candidateId = model.candidateId;
                model.candidateInfos = await candidateInfoService.GetCandidateInfo();
                return View(model);
            }

            CandidateInfo data = new CandidateInfo
            {
                candidateCode = model.referenceJobNo,
                candidateName = model.candidateName,
                nationalID = model.nationalID,
                nationality = model.nationality,
                dateOfBirth = Convert.ToDateTime(model.dateOfBirth),
                placeOfBirth = model.placeOfBirth,
                gender = model.gender,
                maritalStatus = model.maritalStatus,
                religion = model.religion,
                department = model.department,
                designation = model.designation,
                bloodGroup = model.bloodGroup,
                email = model.email,
                mobile = model.mobile
            };

            await candidateInfoService.SaveCandidateInfo(data);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Create(int id)
        {

            CandidateInfoViewModel model = new CandidateInfoViewModel
            {
                candidateInfos = await candidateInfoService.GetCandidateInfo(),
                jobRequisitions = await jobRequisitionService.GetAllJobRequisition()
            };
            return View(model);
        }

        // POST: CandidateInfo/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] CandidateInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.candidateId = model.candidateId;
                model.candidateInfos = await candidateInfoService.GetCandidateInfo();
                return View(model);
            }

            CandidateInfo data = new CandidateInfo
            {
                //candidateCode = Convert.ToString(model.JobRequisition.Id),
                candidateName = model.candidateName,
                nationalID = model.nationalID,
                nationality = model.nationality,
                dateOfBirth = Convert.ToDateTime(model.dateOfBirth),
                placeOfBirth = model.placeOfBirth,
                gender = model.gender,
                maritalStatus = model.maritalStatus,
                religion = model.religion,
                department = model.department,
                designation = model.designation,
                bloodGroup = model.bloodGroup,
                email = model.email,
                mobile = model.mobile
            };

            await candidateInfoService.SaveCandidateInfo(data);

            return RedirectToAction(nameof(AllCVList));
        }

        public IActionResult AllCVList()
        {
            return View();
        }

        // GET: Address
        public async Task<IActionResult> AddressInfo(int id)
        {
            ViewBag.candidateId = id.ToString();
            var model = new AddressInfoViewModel
            {
                candidateId = id,
                candidateInfo = await candidateInfoService.GetCandidateInfoById(id),
                permanent = await updateCandidateInfoService.GetAddressByTypeAndCandidateId(id, "present"),
                present = await updateCandidateInfoService.GetAddressByTypeAndCandidateId(id, "permanent")
                //employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            if (model.permanent == null) model.permanent = new RCRTAddress();
            if (model.present == null) model.present = new RCRTAddress();
            model.present.divisionId = model.present.divisionId ?? 0;
            model.present.districtId = model.present.districtId ?? 0;

            model.permanent.divisionId = model.permanent.divisionId ?? 0;
            model.permanent.districtId = model.permanent.districtId ?? 0;

            //model.permanent.division = new Division();
            //model.permanent.district = new District();

            //model.permanent.division.Id = model.present.division.Id ?? 0;
            //model.permanent.district.Id = model.present.district.Id ?? 0;

            return View(model);
        }

        // POST: Address/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressInfo([FromForm] AddressInfoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.candidateId = model.candidateId;
                model.candidateInfo = await candidateInfoService.GetCandidateInfoById(model.candidateId);
                model.present = await updateCandidateInfoService.GetAddressByTypeAndCandidateId(model.candidateId, "present");
                model.permanent = await updateCandidateInfoService.GetAddressByTypeAndCandidateId(model.candidateId, "permanent");
                if (model.permanent == null) model.permanent = new RCRTAddress();
                if (model.present == null) model.present = new RCRTAddress();

                model.present.divisionId = model.present.divisionId ?? 0;
                model.present.districtId = model.present.districtId ?? 0;
                model.permanent.divisionId = model.permanent.divisionId ?? 0;
                model.permanent.districtId = model.permanent.districtId ?? 0;

                return View(model);
            }

            RCRTAddress presentdata = new RCRTAddress
            {
                Id = model.presentAddressID,
                candidateId = model.candidateId,
                countryId = 1,
                divisionId = Int32.Parse(model.presentDivision),
                districtId = Int32.Parse(model.presentDistrict),
                thanaId = Int32.Parse(model.presentUpazila),
                union = model.presentUnion,
                postOffice = model.presentPostOffice,
                postCode = model.presentPostCode,
                blockSector = model.presentBlockSector,
                houseVillage = model.presentHouseVillage,
                roadNumber = model.presentRoadNo,
                type = "present"
            };
            await updateCandidateInfoService.SaveAddress(presentdata);

            RCRTAddress permanentdata = new RCRTAddress
            {
                Id = model.permanentAddressID,
                candidateId = model.candidateId,
                countryId = 1,
                divisionId = Int32.Parse(model.permanentDivision),
                districtId = Int32.Parse(model.permanentDistrict),
                thanaId = Int32.Parse(model.permanentUpazila),
                union = model.permanentUnion,
                postOffice = model.permanentPostOffice,
                postCode = model.permanentPostCode,
                blockSector = model.permanentBlockSector,
                houseVillage = model.permanentHouseVillage,
                roadNumber = model.permanentRoadNo,
                type = "permanent"
            };
            await updateCandidateInfoService.SaveAddress(permanentdata);

            return RedirectToAction(nameof(Create));
        }

        public async Task<IActionResult> EducationInfo(int id)
        {
            ViewBag.candidateId = id.ToString();
            var model = new EducationInfoViewModel
            {
                candidateId = id,
                candidateInfo = await candidateInfoService.GetCandidateInfoById(id),
                //employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            return View(model);
        }

        // POST: CandidateInfo/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EducationInfo([FromForm] EducationInfoViewModel model)
        {
            ViewBag.candidateId = model.candidateId;
            if (!ModelState.IsValid)
            {
                ViewBag.candidateId = model.candidateId;
                model.candidateInfo = await candidateInfoService.GetCandidateInfoById(model.candidateId);
                return View(model);
            }

            RCRTEducation data = new RCRTEducation
            {
                Id = model.educationId,
                candidateId = model.candidateId,
                institution = model.institution,
                resultId = model.resultId,
                grade = model.grade,
                passingYear = model.passingYear,
                degreeId = model.degreeId,
                organizationId = model.organizationId,
                reldegreesubjectId = model.reldegreesubjectId
            };

            await updateCandidateInfoService.SaveRCRTEducation(data);

            return RedirectToAction(nameof(EducationInfo));
        }

        public IActionResult JobExperience()
        {
            return View();
        }

        public async Task<IActionResult> UpdateCandidateInfo(int id)
        {
            ViewBag.cadidateId = id.ToString();
            CandidatePhotographViewModel model = new CandidatePhotographViewModel
            {
                candidateInfo = await candidateInfoService.GetCandidateInfoById(id)
                //photograph = await photographService.GetPhotographByEmpIdAndType(id, "profile"),
                //candidateInfo = await candidateInfoService.GetCandidateInfoById(id),
                //employeeNameCode = await personalInfoService.GetEmployeeNameCodeById(id)
            };
            //if (model.photograph == null) model.photograph = new CandidatePhoto();
            return View(model);
        }

        public async Task<IActionResult> UpdateCVStatus(int id)
        {
            string userName = HttpContext.User.Identity.Name;
            int status = 1;
            string updateBy = userName;
            await candidateInfoService.UpdateCandidateInfo(id, status, updateBy);
            return RedirectToAction("Index", "CVFiltering", new { Area = "Recruitment" });
        }
        
        #region CVCollection Api
        [AllowAnonymous]
        [Route("global/api/allCandidateList")]
        [HttpGet]
        public async Task<IActionResult> AllCandidateList()
        {
            //ApplicationUser user = await _userManager.GetUserAsync(HttpContext.User);
            return Json(await candidateInfoService.GetCandidateInfoAsQueryAble());
            //return Json(await candidateInfoService.GetCandidateInfo());
            //var model = new CandidateInfoViewModel
            //{
            //    candidateInfos = await candidateInfoService.GetCandidateInfoAsQueryAble(queryString)
            //};
            //return View(model);
        }
        #endregion
    }
}