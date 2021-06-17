using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMMasterData.Models;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.SCMMasterData.Controllers
{
    [Area("SCMMasterData")]
    public class SCMFeatureItemController : Controller
    {
        public readonly IItemsService itemsService;
        
        private readonly IUserInfoes userInfoes;

        public SCMFeatureItemController(IItemsService itemsService,  IUserInfoes userInfoes)
        {
            this.itemsService = itemsService;
            
            this.userInfoes = userInfoes;
        }
        [HttpGet]
        public async Task<IActionResult>  Index()
        {
            SCMFeatureItemViewModel item = new SCMFeatureItemViewModel
            {
                featureItems = await itemsService.AllFeatureItem()
            };
            return View(item);
        }
        
       
        [HttpPost]
        public async Task<IActionResult> SaveFeatureItem(SCMFeatureItemViewModel model)
        {
            string userName = HttpContext.User.Identity.Name;
            var userInfo = await userInfoes.GetUserInfoByUser(userName);

            var featureItem = new FeatureItem
            {
                
                itemSpecificationId = model.itemSpecificationId,
                userId = userInfo.Id,
                date = DateTime.Now,
                statusId = 1,
                shortOrder = 1
            };
            await itemsService.SaveFeatureItem(featureItem);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditFeatureItem(SCMFeatureItemViewModel model)
        {
            return View();
        }
    }
}