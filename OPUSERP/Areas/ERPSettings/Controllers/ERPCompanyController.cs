using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.ERPSettings.Models;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;

namespace OPUSERP.Areas.ERPSettings.Controllers
{
    [Area("ERPSettings")]
    public class ERPCompanyController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IERPCompanyService iERPCompanyService;
        private readonly IUserInfoes userInfoes;
        private readonly IERPModuleAssignService eRPModuleAssignService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public ERPCompanyController(IHostingEnvironment hostingEnvironment, IERPCompanyService iERPCompanyService, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IERPModuleAssignService eRPModuleAssignService, IUserInfoes userInfoes)
        {
            this.iERPCompanyService = iERPCompanyService;
            this._hostingEnvironment = hostingEnvironment;
            this.eRPModuleAssignService = eRPModuleAssignService;
            this.userInfoes = userInfoes;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            ERPCompanyViewModel model = new ERPCompanyViewModel
            {
                companies=await iERPCompanyService.GetAllCompany()
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromForm] ERPCompanyViewModel model)
        {
            string userName = User.Identity.Name;
            string filesPath = $"{_hostingEnvironment.WebRootPath}/Upload/logo";
            string fileName = string.Empty;
            string fileLocation= string.Empty;


            Company company = new Company
            {
                Id = Convert.ToInt32(model.companyId),
                companyName = model.companyName,
                tradeLicense = model.tradeLicense,
                ownerName = model.ownerName,
                officeTelephone = model.officeTelephone,
                vatNo = model.vatNo,
                tinNo = model.tinNo,
                companyEmail = model.companyEmail,
                alternetEmail = model.alternetEmail,
                fileName = fileName,
                filePath = fileLocation,
                addressLine = model.addressLine
            };
           int companyId= await iERPCompanyService.SaveERPCompany(company);

            if (model.logo != null)
            {
                //int _min = 1000;
                //int _max = 9999;
                //Random _rdm = new Random();
                //int uniqNo = _rdm.Next(_min, _max);
                fileName = ContentDispositionHeaderValue.Parse(model.logo.ContentDisposition).FileName;
                string fileType = Path.GetExtension(fileName);
                fileName = fileName.Contains("\\")
                    ? fileName.Trim('"').Substring(fileName.LastIndexOf("\\", StringComparison.Ordinal) + 1)
                    : fileName.Trim('"');

                string NewFileName = companyId + "_logo." + fileName.Split(".")[1];
                fileLocation = "/Upload/logo/" + NewFileName;
                var fullFilePath = Path.Combine(filesPath, NewFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await model.logo.CopyToAsync(stream);
                }

                iERPCompanyService.UpdateCompanyLogoById(companyId, NewFileName, fileLocation);
            }

            int maxUserId = await userInfoes.GetMaxUserId()+1;
            var user = new ApplicationUser { UserName = model.loginId, Email = model.companyEmail,userId=maxUserId };
            var result = await _userManager.CreateAsync(user, model.password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "ERPAdmin");
                //IdentityUser temp = await _userManager.FindByNameAsync(model.loginId);
            }

            ERPModuleAssign eRPModuleAssign = new ERPModuleAssign
            {
                companyId = companyId,
                roleId = "4734bf4e-4ee6-4606-9d4c-90ca3fe7c57a"
            };
            await eRPModuleAssignService.SaveERPModuleAssign(eRPModuleAssign);

            return RedirectToAction(nameof(Index));
        }
    }
}