using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.Recruitment.Models;
using OPUSERP.HRPMS.Services.MasterData.Interfaces;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;

namespace OPUSERP.Areas.Recruitment.Controllers
{
    [Area("Recruitment")]
    public class JobRequisitionController : Controller
    {
        private readonly IJobRequisitionService jobRequisitionService;
        private readonly IDesignationDepartmentService  designationDepartmentService;

        public JobRequisitionController(IJobRequisitionService jobRequisitionService, IDesignationDepartmentService designationDepartmentService)
        {
            this.jobRequisitionService = jobRequisitionService;
            this.designationDepartmentService = designationDepartmentService;
        } 
        
        public async Task<IActionResult> Index()
        {
            JobRequisitionViewModel model = new JobRequisitionViewModel
            {
                jobRequisitions = await jobRequisitionService.GetAllJobRequisition(),
                departments = await designationDepartmentService.GetDepartment(),
                designations = await designationDepartmentService.GetDesignations()
            };
            return View(model);
        }

        // POST: JobRequisition/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index([FromForm] JobRequisitionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.jobRequisitions = await jobRequisitionService.GetAllJobRequisition();
                return View(model);
            }

            JobRequisition data = new JobRequisition
            {
                Id = (int)model.jobRequisitionId,
                jobNo = model.jobNo,
                jobTitle = model.jobTitle,
                requisitionDate = model.requisitionDate,
                appointmentDate = model.appointmentDate,
                justification = model.justification,
                designationId = model.designationId,
                departmentId = model.departmentId,
                //jobSourceId = model.jobSourceId,
                vacancy = model.vacancy,
                jobLocation = model.jobLocation,
                salary = model.salary,
                employmentStatus = model.employmentStatus,
                jobContext = model.jobContext,
                reqExperience = model.reqExperience,
                reqEducationalQ = model.reqEducationalQ,
                jobResponsibility = model.jobResponsibility,
                addRequirements = model.addRequirements,
                compensationOthers = model.compensationOthers
            };

            await jobRequisitionService.SaveJobRequisition(data);

            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> JobGridView()
        {
            //int id = 0;
            JobRequisitionViewModel model = new JobRequisitionViewModel
            {
                //jobPosts = await jobRequisitionService.GetAllJobPostByRequisition()
                jobRequisitions = await jobRequisitionService.GetAllJobRequisition()
            };
            return View(model);
        }
    }
}