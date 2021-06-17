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
    public class OtherMemberShipController : ControllerBase
    {
        private readonly IMembershipService membershipService;

        public OtherMemberShipController(IMembershipService membershipService)
        {
            this.membershipService = membershipService;
        }


        // GET: api/Education/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await membershipService.GetMembershipInfoByEmpId(id));
        }

        // POST: api/Education
        [HttpPost]
        public async Task<IActionResult> post([FromBody] OtherMembership model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await membershipService.SaveMembershipInfo(model);

            if (!result)
                return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));

            return new OkObjectResult(new { Message = "Other Membership Updated." });
        }
    }
}