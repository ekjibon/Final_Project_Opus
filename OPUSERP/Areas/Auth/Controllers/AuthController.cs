using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data.Entity;
using OPUSERP.ERPServices.AuthService.Interfaces;
using OPUSERP.Helpers.Errors;
using OPUSERP.HRPMS.Services.Employee.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtFactoryService _jwtFactory;
        private readonly IPersonalInfoService personalInfoService;

        public AuthController(UserManager<ApplicationUser> userManager, IJwtFactoryService jwtFactory, IPersonalInfoService personalInfoService)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
            this.personalInfoService = personalInfoService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(_userManager);
        }

        private IActionResult Json(UserManager<ApplicationUser> userManager)
        {
            throw new NotImplementedException();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody]LogInViewAPPModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userManager.FindByNameAsync(model.ID);

            if (user != null && (await _userManager.CheckPasswordAsync(user, model.Password)))
            {
                var roles = await _userManager.GetRolesAsync(user);
                string id = await personalInfoService.GetEmployeeIDByAuthID(user.Id);
                var response = new
                {
                    id = id,
                    auth_token = await _jwtFactory.GenerateToken(user.UserName, id, roles)
                };

                var jwt = JsonConvert.SerializeObject(response);
                return new OkObjectResult(jwt);

            }

            return BadRequest(Errors.AddErrorToModelState("login_failure", "Invalid username or password.", ModelState));
        }
    }
}
