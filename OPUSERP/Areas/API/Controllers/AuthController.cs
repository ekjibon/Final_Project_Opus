using OPUSERP.CLUB.Services.Member.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Security.Claims;
using System.Threading.Tasks;
using OPUSERP.Data.Entity;
using OPUSERP.Helpers.Errors;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.CLUB.Services.jwt.Interfaces;
using OPUSERP.Areas.API.Models;

namespace OPUSERP.CLUB.Areas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromBody]loginViewModel model)
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

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePsswordViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var data = await _userManager.ChangePasswordAsync(await _userManager.FindByNameAsync(User.FindFirst(ClaimTypes.NameIdentifier)?.Value), model.OldPassword, model.Password);
            return new OkObjectResult(new {Message =  data.ToString()});
        }
    }
}