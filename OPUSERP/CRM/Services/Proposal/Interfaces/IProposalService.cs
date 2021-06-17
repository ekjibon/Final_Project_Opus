using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Proposal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Proposal.Interfaces
{
    public interface IProposalService
    {
        #region Product

        Task<bool> SaveProduct(Product product);
        Task<IEnumerable<Product>> GetAllProduct();
        Task<Product> GetProductById(int id);
        Task<bool> DeleteProductById(int id);

        #endregion

        #region Proposal Type

        Task<bool> SaveProposalType(ProposalType proposalType);
        Task<IEnumerable<ProposalType>> GetAllProposalType();
        Task<ProposalType> GetProposalTypeById(int id);
        Task<bool> DeleteProposalTypeById(int id);

        #endregion

        #region Proposal Master

        Task<int> SaveProposalMaster(ProposalMaster proposalMaster);
        Task<IEnumerable<ProposalMaster>> GetAllProposalMaster();
        Task<ProposalMaster> GetProposalMasterById(int id);
        Task<bool> DeleteProposalMasterById(int id);
        Task<IEnumerable<ProposalMaster>> GetProposalMasterByLeadId(int leadId);
        #endregion

        #region Proposal Detail

        Task<bool> SaveProposalDetail(ProposalDetail proposalDetail);
        Task<IEnumerable<ProposalDetail>> GetAllProposalDetail();
        Task<ProposalDetail> GetProposalDetailById(int id);
        Task<bool> DeleteProposalDetailByMasterId(int masterId);
        Task<IEnumerable<ProposalDetail>> GetProposalDetailByMasterId(int masterId);
        #endregion

        #region Relation Product Proposal

        Task<bool> SaveRelProductProposal(RelProductProposal relProductProposal);
        Task<IEnumerable<RelProductProposal>> GetAllRelProductProposal();
        Task<RelProductProposal> GetRelProductProposalById(int id);
        Task<bool> DeleteRelProductProposalByMasterId(int masterId);
        Task<IEnumerable<RelProductProposal>> GetAllProductProposalByMasterId(int masterId);
        #endregion

        #region Proposal Particulars

        Task<bool> SaveProposalParticulars(ProposalParticulars proposalParticulars);
        Task<IEnumerable<ProposalParticulars>> GetAllProposalParticulars();
        Task<ProposalParticulars> GetProposalParticularsById(int id);
        Task<bool> DeleteProposalParticularsById(int id);

        #endregion

        #region Proposal Status

        Task<bool> SaveProposalStatus(ProposalStatus proposalStatus);
        Task<IEnumerable<ProposalStatus>> GetAllProposalStatus();
        Task<ProposalStatus> GetProposalStatusById(int id);
        Task<bool> DeleteProposalStatusById(int id);

        #endregion

    }
}
