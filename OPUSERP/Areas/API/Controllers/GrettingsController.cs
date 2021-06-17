using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GrettingsController : ControllerBase
    {
        private readonly IPersonalInfoService personalInfoService;

        public GrettingsController(IPersonalInfoService personalInfoService)
        {
            this.personalInfoService = personalInfoService;
        }

        // GET: api/Notice
        //[HttpGet()]
        //public async Task<IActionResult> Get()
        //{
        //    //return new OkObjectResult(await personalInfoService.GetEmployeeGrettings());
        //}
    }
}