using Microsoft.AspNetCore.Mvc;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class RatingController : Controller
    {
        private readonly ICommunicationService communicationService;

        public RatingController(ICommunicationService communicationService)
        {
            this.communicationService = communicationService;
        }
    }
}