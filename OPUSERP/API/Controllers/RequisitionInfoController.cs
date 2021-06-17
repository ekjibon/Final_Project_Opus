using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.SCMRequisition.Models;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.ERPService.AuthService.Interfaces;
using OPUSERP.SCM.Data.Entity.Requisition;
using OPUSERP.SCM.Services.Requisition.Interfaces;

namespace OPUSERP.API.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class RequisitionInfoController : ControllerBase
    {
        private readonly IRequisitionService requisitionService;
        private readonly IUserInfoes userInfoes;

        public RequisitionInfoController(IRequisitionService requisitionService, IUserInfoes userInfoes)
        {
            this.requisitionService = requisitionService;
            this.userInfoes = userInfoes;
        }

        [HttpGet("{userName}")]
        [AllowAnonymous]
        public async Task<IEnumerable<GetRequisitionListForApprovedViewModel>> ReqApprovelistApi(string userName)
        {
            var userInfo = await userInfoes.GetUserInfoByUser(userName);

            var result = await requisitionService.GetRequisitionApproveList(userInfo.UserId);
            return result;
        }

        [HttpGet("{userName}")]
        [AllowAnonymous]
        public async Task<IEnumerable<RequisitionMaster>> ReturnRequisitionApi(string userName)
        {
            var userInfo = await userInfoes.GetUserInfoByUser(userName);

            var result = await requisitionService.GetRequisitionMasterListByPRStatus(userInfo.UserId, 4);

            return result;
        }

        [HttpGet("{userName}")]
        [AllowAnonymous]
        public async Task<IEnumerable<RequisitionMaster>> RejectRequisitionApi(string userName)
        {
            var userInfo = await userInfoes.GetUserInfoByUser(userName);

            var result = await requisitionService.GetRequisitionMasterListByPRStatus(userInfo.UserId, 4);

            return result;
        }
    }
}