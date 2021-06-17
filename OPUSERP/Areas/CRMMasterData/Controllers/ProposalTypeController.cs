using Microsoft.AspNetCore.Mvc;
using OPUSERP.Areas.CRMMasterData.Models;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using System.Threading.Tasks;

namespace OPUSERP.Areas.CRMMasterData.Controllers
{
    [Area("CRMMasterData")]
    public class ProposalTypeController : Controller
    {
        private readonly IProposalService proposalService;

        public ProposalTypeController(IProposalService proposalService)
        {
            this.proposalService = proposalService;
        }

        #region Proposal Type

        public async Task<IActionResult> ProposalType()
        {
            ProposalTypeViewModel model = new ProposalTypeViewModel()
            {
                proposalTypes = await proposalService.GetAllProposalType()
            };
            return View(model);
        }

        // POST: Proposal Type/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProposalType([FromForm] ProposalTypeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.proposalTypes = await proposalService.GetAllProposalType();
                return View(model);
            }

            ProposalType data = new ProposalType
            {
                Id = model.proposalTypeId,
                proposalTypeName = model.proposalTypeName
            };

            await proposalService.SaveProposalType(data);
            return RedirectToAction(nameof(ProposalType));
        }

        public async Task<IActionResult> DeleteProposalTypeById(int id)
        {
            await proposalService.DeleteProposalTypeById(id);
            return RedirectToAction("ProposalType", "ProposalType", new { Area = "CRMMasterData" });
        }

        #endregion

        #region Product

        public async Task<IActionResult> Product()
        {
            ProductViewModel model = new ProductViewModel()
            {
                products = await proposalService.GetAllProduct()
            };
            return View(model);
        }

        // POST: Proposal Type/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Product([FromForm] ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.products = await proposalService.GetAllProduct();
                return View(model);
            }

            Product data = new Product
            {
                Id = model.productId,
                productName = model.productName,
                productDescription = model.productDescription
            };

            await proposalService.SaveProduct(data);
            return RedirectToAction(nameof(Product));
        }

        public async Task<IActionResult> DeleteProductById(int id)
        {
            await proposalService.DeleteProductById(id);
            return RedirectToAction("Product", "ProposalType", new { Area = "CRMMasterData" });
        }

        #endregion

        #region Proposal Particulars

        public async Task<IActionResult> ProposalParticulars()
        {
            ProposalParticularsViewModel model = new ProposalParticularsViewModel()
            {
                proposalParticulars = await proposalService.GetAllProposalParticulars()
            };
            return View(model);
        }

        // POST: Proposal Particulars/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProposalParticulars([FromForm] ProposalParticularsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.proposalParticulars = await proposalService.GetAllProposalParticulars();
                return View(model);
            }

            ProposalParticulars data = new ProposalParticulars
            {
                Id = model.proposalParticularsId,
                proposalParticularsName = model.proposalParticularsName
            };

            await proposalService.SaveProposalParticulars(data);
            return RedirectToAction(nameof(ProposalParticulars));
        }

        public async Task<IActionResult> DeleteProposalParticularsById(int id)
        {
            await proposalService.DeleteProposalParticularsById(id);
            return RedirectToAction("ProposalParticulars", "ProposalType", new { Area = "CRMMasterData" });
        }

        #endregion

        #region Proposal Status

        public async Task<IActionResult> ProposalStatus()
        {
            ProposalStatusViewModel model = new ProposalStatusViewModel()
            {
                proposalStatuses = await proposalService.GetAllProposalStatus()
            };
            return View(model);
        }

        // POST: Proposal Particulars/save/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProposalStatus([FromForm] ProposalStatusViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.proposalStatuses = await proposalService.GetAllProposalStatus();
                return View(model);
            }

            ProposalStatus data = new ProposalStatus
            {
                Id = model.proposalStatusId,
                proposalStatusName = model.proposalStatusName
            };

            await proposalService.SaveProposalStatus(data);
            return RedirectToAction(nameof(ProposalStatus));
        }

        public async Task<IActionResult> DeleteProposalStatusById(int id)
        {
            await proposalService.DeleteProposalStatusById(id);
            return RedirectToAction("ProposalStatus", "ProposalType", new { Area = "CRMMasterData" });
        }

        #endregion

    }
}