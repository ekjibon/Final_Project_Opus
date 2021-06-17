using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Helpers.Errors;

namespace OPUSERP.Areas.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PersonalInfoController : ControllerBase
    {
        private readonly IPersonalInfoService personalInfo;

        public PersonalInfoController(IPersonalInfoService personalInfo)
        {
            this.personalInfo = personalInfo;
        }

        // GET: api/PersonalInfo/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> GetAsync(int id)
        {
            return new OkObjectResult(await personalInfo.GetEmployeeInfoById(id));
        }

        // POST: api/PersonalInfo
        [HttpPost]
        public async Task<IActionResult> post ([FromBody] OPUSERP.CLUB.Data.Member.MemberInfo model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await personalInfo.SaveEmployeeInfo(model);

            if (result==0)
                return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));

            return new OkObjectResult(new { Message = "Personal Information Updated." });
        }

        // PUT: api/PersonalInfo/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
