using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using OPUSERP.Data;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.CRM.Services.Client.Interfaces;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.ERPServices.AttachmentComment.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using IDesignationDepartmentService = OPUSERP.HRPMS.Services.MasterData.Interfaces.IDesignationDepartmentService;
using OPUSERP.ERPService.MasterData.Interfaces;
using OPUSERP.HRPMS.Services.Employee.Interfaces;

namespace OPUSERP.Areas.HRPMSEmployee.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ApiEmployeeController : ControllerBase
    {
        private ERPDbContext _db;
        private readonly IPersonalInfoService personalInfoService;
        private readonly IWagesPersonalInfoService wagesPersonalInfoService;
        private readonly IWagesReferenceService wagesReferenceService;
        private readonly IReferenceService referenceService;
        private readonly IOfficeAssignService officeAssignService;


        public ApiEmployeeController( ERPDbContext db, IHostingEnvironment hostingEnvironment, IOfficeAssignService officeAssignService, IReferenceService referenceService, IPersonalInfoService personalInfoService, IWagesPersonalInfoService wagesPersonalInfoService, IWagesReferenceService wagesReferenceService)
        {
            this.personalInfoService = personalInfoService;
            this.referenceService = referenceService;
            this.wagesPersonalInfoService = wagesPersonalInfoService;
            this.wagesReferenceService = wagesReferenceService;
            this.officeAssignService = officeAssignService;
            _db = db;
        }
       
        #region Contacts
       

   
        [HttpGet("{id}")]
        public async Task<IActionResult> getdeskinfobyemployeId(int id)
        {
            return new OkObjectResult(await officeAssignService.GetOfficeAssignByEmpId(id));
        }

       

        #endregion


    }
}