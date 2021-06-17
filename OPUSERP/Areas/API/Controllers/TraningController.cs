using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.CLUB.Data.Member;
using OPUSERP.Helpers.Errors;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TraningController : ControllerBase
    {
        private readonly ITraningHistoryService traningHistoryService;

        public TraningController(ITraningHistoryService traningHistoryService)
        {
            this.traningHistoryService = traningHistoryService;
        }


        // GET: api/Education/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await traningHistoryService.GetTraningHistoryByEmpId(id));
        }

        // POST: api/Education
        [HttpPost]
        public async Task<IActionResult> post([FromBody] MemberTraningLog model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await traningHistoryService.SaveTraningHistory(model);

            if (!result)
                return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));

            return new OkObjectResult(new { Message = "Training History Updated." });
        }
    }
}