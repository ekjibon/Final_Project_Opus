using System.Threading.Tasks;
using OPUSERP.CLUB.Data.Member;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.CLUB.Services.Member.Interfaces;
using OPUSERP.Helpers;
using OPUSERP.Helpers.Errors;
using OPUSERP.Areas.MemberInfo.Models;

namespace OPUSERP.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PhotographController : ControllerBase
    {
        private readonly IPhotographService photographService;

        public PhotographController(IPhotographService photographService)
        {
            this.photographService = photographService;
        }

        // GET: api/Notice
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return new OkObjectResult(await photographService.GetPhotographByEmpIdAndType(id, "profile"));
        }

        // POST: api/Education
        [HttpPost]
        public async Task<IActionResult> post([FromBody] PhotographViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            string fileName;
            string message = FileSave.SaveImage(out fileName, model.empPhoto);

            if (message != "success")
            {
                return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));
            }

            MemberPhotograph data = new MemberPhotograph
            {
                Id = model.photographID,
                employeeId = model.employeeID,
                url = fileName,
                type = "profile"
            };

            var result = await photographService.SavePhotograph(data);

            if (!result)
                return BadRequest(Errors.AddErrorToModelState("Message", "Something Went Wrong!! Date not saved!!", ModelState));

            return new OkObjectResult(new { Message = "Children Updated." });
        }
    }
}