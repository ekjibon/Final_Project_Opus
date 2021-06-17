using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead.Interfaces
{
    public interface IAgreementService
    {
        #region Agreement
        Task<int> SaveAgreement(Agreement agreement);
        Task<IEnumerable<Agreement>> GetAllAgreement();
        Task<Agreement> GetAgreementdesc();
        Task<IEnumerable<Agreement>> GetAgreementbyLeadId(int LeadId);
        Task<IEnumerable<Agreement>> GetAgreementbyOwner(string Owner);
        Task<Agreement> GetAgreementById(int id);
        Task<bool> DeleteAgreementById(int id);
        Task<Agreement> GetAgreementByagreementId(int agreementId);
        Task<bool> UpdateForAgreementVerification(int Id, int statusId, string remarks);
        Task<bool> UpdateForAgreementPrint(int Id);
        Task<GetAgreementReportViewModel> GetAgreementReportByAgreementId(int AgreementId);
        Task<IEnumerable<Agreement>> GetAgreementForCRO();
        Task<bool> UpdateAgreementDetailsProposePro(int Id, int statusId, string remarks);
        Task<IEnumerable<GetAgreementDetailsbyRatingDateViewModel>> GetAgreementDetailsbyRatingDatefilter(string FDate, string TDate, string Owner, string TeamLeader, string Fa, string BD, string LeadId);

        #endregion

        #region Agreement Type
        Task<int> SaveAgreementType(AgreementType agreementType);
        Task<IEnumerable<AgreementType>> GetAllAgreementType();
        Task<AgreementType> GetAgreementTypeById(int id);
        Task<bool> DeleteAgreementTypeById(int id);

        #endregion

        #region Relation Product Agreement
        Task<bool> SaveRelProductAgreement(RelProductAgreement relProductAgreement);
        Task<IEnumerable<RelProductAgreement>> GetAllRelProductAgreement();
        Task<RelProductAgreement> GetRelProductAgreementById(int id);
        Task<bool> DeleteRelProductAgreementByagreementId(int agreementId);
        Task<IEnumerable<RelProductAgreement>> GetRelProductAgreementByagreementId(int agreementId);

        #endregion

        #region Agreement Details
        Task<int> SaveAgreementDetails(AgreementDetails agreementDetails);
        Task<IEnumerable<AgreementDetails>> GetAllAgreementDetails();
        Task<IEnumerable<AgreementDetails>> GetAgreementDetailsbyAgreementId(int AgreementId);
        Task<IEnumerable<AgreementDetails>> GetAgreementDetailsbyAgreementRatingYearId(int AgreementId, int RatingYearId);
        Task<IEnumerable<AgreementDetails>> GetAgreementDetailsbyAgreementIdRatingDate(int AgreementId, string FDate, string TDate);
        Task<AgreementDetails> GetAgreementDetailsById(int id);
        Task<AgreementDetails> GetAgreementDetailsByaggId(int id);
        Task<bool> DeleteAgreementDetailsById(int id);
        Task<bool> DeleteAgreementDetailsbyAgreementId(int AgreementId);
        Task<IEnumerable<AgreementDetails>> GetAgreementDetailsbyleadId(int AgreementId);
        Task<IEnumerable<GetAgreementDetailsbyRatingDateViewModel>> GetAgreementDetailsbyRatingDate(string FDate, string TDate, string Owner);
        Task<bool> UpdateAgreementDetailsPropose(int Id, int statusId,string remarks);
        Task<IEnumerable<GetAgreementDetailsbyRatingDateSApprovalViewModel>> GetAgreementDetailsbyRatingDateSApproval(string FDate, string TDate, string Owner);
        Task<IEnumerable<AgreementDetails>> GetApprovedSurveillencebyOwner(string Owner);
        Task<IEnumerable<AgreementDetails>> GetApprovedSurveillencebyOwnerDate(string Owner, DateTime? Fdate, DateTime? TDate);
        Task<IEnumerable<AgreementDetails>> GetReviewedSurveillencebyOwnerDate(string Owner, DateTime? Fdate, DateTime? TDate);
        Task<bool> UpdateForAgreementBackToLead(int Id);

        #endregion

        #region rating Year
        Task<int> SaveRatingYear(RatingYear ratingYear);
        Task<IEnumerable<RatingYear>> GetAllRatingYear();
        #endregion

        #region Vat Category
        Task<IEnumerable<VatCategory>> GetAllVatCategory();
        #endregion

        #region Tax Category
        Task<IEnumerable<TaxCategory>> GetAllTaxCategory();
        #endregion

        #region Approved for Cro
        Task<int> SaveApprovedforCro(ApprovedforCro approvedforCro);
        Task<IEnumerable<ApprovedforCro>> GetAllApprovedforCro();
        Task<ApprovedforCro> GetApprovedforCroById(int id);
        Task<bool> DeleteApprovedforCroById(int id);
        //Task<IEnumerable<ApprovedforCro>> GetAgreementForCROrating();
        Task<IEnumerable<GetAgreementForCROratingViewModel>> GetAgreementForCROrating(string empCode);
        
        #endregion
    }
}
