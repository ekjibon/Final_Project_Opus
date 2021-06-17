using Microsoft.EntityFrameworkCore;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Services.Proposal.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using OPUSERP.Areas.CRMLead.Models;

namespace OPUSERP.CRM.Services.Proposal
{
    public class ProposalService : IProposalService
    {
        private readonly ERPDbContext _context;

        public ProposalService(ERPDbContext context)
        {
            _context = context;
        }

        #region Product

        public async Task<bool> SaveProduct(Product product)
        {
            if (product.Id != 0)
                _context.Products.Update(product);
            else
                _context.Products.Add(product);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllProduct()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> DeleteProductById(int id)
        {
            _context.Products.Remove(_context.Products.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Proposal Type

        public async Task<bool> SaveProposalType(ProposalType proposalType)
        {
            if (proposalType.Id != 0)
                _context.ProposalTypes.Update(proposalType);
            else
                _context.ProposalTypes.Add(proposalType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProposalType>> GetAllProposalType()
        {
            return await _context.ProposalTypes.ToListAsync();
        }

        public async Task<ProposalType> GetProposalTypeById(int id)
        {
            return await _context.ProposalTypes.FindAsync(id);
        }

        public async Task<bool> DeleteProposalTypeById(int id)
        {
            _context.ProposalTypes.Remove(_context.ProposalTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Proposal Master

        public async Task<int> SaveProposalMaster(ProposalMaster proposalMaster)
        {
            if (proposalMaster.Id != 0)
                _context.ProposalMasters.Update(proposalMaster);
            else
                _context.ProposalMasters.Add(proposalMaster);
            await _context.SaveChangesAsync();
            return proposalMaster.Id;
        }

        public async Task<IEnumerable<ProposalMaster>> GetAllProposalMaster()
        {
            return await _context.ProposalMasters.Include(a => a.proposalType).Include(a => a.leads).ToListAsync();
        }

        public async Task<ProposalMaster> GetProposalMasterById(int id)
        {
            return await _context.ProposalMasters.Include(a => a.proposalType).Include(a => a.leads).Include(a => a.proposalStatus).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProposalMasterById(int id)
        {
            _context.ProposalMasters.Remove(_context.ProposalMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProposalMaster>> GetProposalMasterByLeadId(int leadId)
        {
            return await _context.ProposalMasters.Include(a => a.proposalType).Include(a => a.leads).Where(a => a.leadsId == leadId).OrderByDescending(a=>a.Id).ToListAsync();
        }

        #endregion

        #region Proposal Detail

        public async Task<bool> SaveProposalDetail(ProposalDetail proposalDetail)
        {
            if (proposalDetail.Id != 0)
                _context.ProposalDetails.Update(proposalDetail);
            else
                _context.ProposalDetails.Add(proposalDetail);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProposalDetail>> GetAllProposalDetail()
        {
            return await _context.ProposalDetails.ToListAsync();
        }

        public async Task<ProposalDetail> GetProposalDetailById(int id)
        {
            return await _context.ProposalDetails.FindAsync(id);
        }

        public async Task<bool> DeleteProposalDetailByMasterId(int masterId)
        {
            _context.ProposalDetails.RemoveRange(_context.ProposalDetails.Where(x => x.proposalMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProposalDetail>> GetProposalDetailByMasterId(int masterId)
        {

            return await _context.ProposalDetails.Include(x => x.proposalMaster).Include(x => x.proposalParticulars).Where(x => x.proposalMasterId == masterId).Select(e => new ProposalDetail()
            {
                Id = e.Id,
                specificationName = e.proposalMaster.proposalType.proposalTypeName,
                proposalParticularsName = e.proposalParticulars.proposalParticularsName,
                proposalParticularsId = e.proposalParticulars.Id,
                particularsValue = e.particularsValue
            }).ToListAsync();           
        }

        #endregion

        #region Relation Product Proposal

        public async Task<bool> SaveRelProductProposal(RelProductProposal relProductProposal)
        {
            if (relProductProposal.Id != 0)
                _context.RelProductProposals.Update(relProductProposal);
            else
                _context.RelProductProposals.Add(relProductProposal);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RelProductProposal>> GetAllRelProductProposal()
        {
            return await _context.RelProductProposals.ToListAsync();
        }

        public async Task<RelProductProposal> GetRelProductProposalById(int id)
        {
            return await _context.RelProductProposals.FindAsync(id);
        }

        public async Task<bool> DeleteRelProductProposalByMasterId(int masterId)
        {
            _context.RelProductProposals.RemoveRange(_context.RelProductProposals.Where(x => x.proposalMasterId == masterId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RelProductProposal>> GetAllProductProposalByMasterId(int masterId)
        {
            return await _context.RelProductProposals.Include(a => a.product).Include(a => a.proposalMaster).Where(a => a.proposalMasterId == masterId)
            .Select(e => new RelProductProposal()
            {               
                proposalMasterId = e.proposalMasterId,
                productId = e.productId,
                productName=e.product.productName,
                productDescription=e.product.productDescription
            }).ToListAsync();
         

            //List<RelProductProposal> RelProductProposals = new List<RelProductProposal>();
            //IEnumerable<RelProductProposal> RelProductProposallist = await _context.RelProductProposals.Include(a => a.product).Where(x => x.proposalMasterId == masterId).ToListAsync();

            //List<Product> Product = _context.Products.ToList();

            //foreach (RelProductProposal data in RelProductProposallist)
            //{
            //    var product = Product.FirstOrDefault();

            //    RelProductProposals.Add(new RelProductProposal
            //    {
            //        proposalMasterId = data.proposalMasterId,
            //        productId = data.productId,
            //        productName = product.productName

            //    });

            //}

            //return RelProductProposals;
        }
        #endregion

        #region Proposal Particulars

        public async Task<bool> SaveProposalParticulars(ProposalParticulars proposalParticulars)
        {
            if (proposalParticulars.Id != 0)
                _context.ProposalParticulars.Update(proposalParticulars);
            else
                _context.ProposalParticulars.Add(proposalParticulars);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProposalParticulars>> GetAllProposalParticulars()
        {
            return await _context.ProposalParticulars.ToListAsync();
        }

        public async Task<ProposalParticulars> GetProposalParticularsById(int id)
        {
            return await _context.ProposalParticulars.FindAsync(id);
        }

        public async Task<bool> DeleteProposalParticularsById(int id)
        {
            _context.ProposalParticulars.Remove(_context.ProposalParticulars.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Proposal Status

        public async Task<bool> SaveProposalStatus(ProposalStatus proposalStatus)
        {
            if (proposalStatus.Id != 0)
                _context.ProposalStatuses.Update(proposalStatus);
            else
                _context.ProposalStatuses.Add(proposalStatus);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProposalStatus>> GetAllProposalStatus()
        {
            return await _context.ProposalStatuses.ToListAsync();
        }

        public async Task<ProposalStatus> GetProposalStatusById(int id)
        {
            return await _context.ProposalStatuses.FindAsync(id);
        }

        public async Task<bool> DeleteProposalStatusById(int id)
        {
            _context.ProposalStatuses.Remove(_context.ProposalStatuses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

    }
}
