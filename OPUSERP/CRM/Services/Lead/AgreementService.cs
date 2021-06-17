using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{
   
    public class AgreementService : IAgreementService
    {
        private readonly ERPDbContext _context;

        public AgreementService(ERPDbContext context)
        {
            _context = context;
        }
        #region Agreement
        public async Task<int> SaveAgreement(Agreement agreement)
        {
            if (agreement.Id != 0)
                _context.Agreements.Update(agreement);
            else
                _context.Agreements.Add(agreement);
            await _context.SaveChangesAsync();
            return agreement.Id;
        }

        public async Task<IEnumerable<Agreement>> GetAllAgreement()
        {
            return await _context.Agreements.AsNoTracking().Include(x => x.agreementCategory).Include(x => x.agreementStatus).Include(x => x.leads).Include(x => x.agreementType).Include(x => x.contactSignatory).Include(x => x.contactWitness).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
        public async Task<Agreement> GetAgreementdesc()
        {
            return await _context.Agreements.AsNoTracking().Include(x => x.agreementCategory).Include(x => x.agreementStatus).Include(x => x.leads).Include(x => x.agreementType).Include(x => x.contactSignatory).Include(x => x.contactWitness).OrderByDescending(x => x.Id).AsNoTracking().OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Agreement>> GetAgreementbyLeadId(int LeadId)
        {
            return await _context.Agreements.Where(x => x.leadsId == LeadId).Include(x => x.agreementCategory).Include(x => x.agreementStatus).Include(x => x.leads).Include(x => x.agreementType).Include(x => x.contactSignatory).Include(x => x.contactWitness).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Agreement>> GetAgreementbyOwner(string Owner)
        {
            var aspnetuser = _context.Users.Where(x => x.UserName == Owner)?.FirstOrDefault()?.Id;
            var roleId = _context.UserRoles.Where(x => x.UserId == aspnetuser)?.FirstOrDefault()?.RoleId;
            var role = _context.Roles.Where(x => x.Id == roleId)?.FirstOrDefault()?.Name;
            if (role== "HR CRM CRO Admin" || role == "Accounts" || role == "admin"|| role == "Manager")
            {
                return await _context.Agreements.Include(x => x.agreementCategory).Include(x => x.leads).Include(x => x.agreementType).Include(x => x.contactSignatory).Include(x => x.contactWitness).Include(x => x.agreementStatus).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.Agreements.Where(x => x.agreementOwner == Owner).Include(x => x.agreementCategory).Include(x => x.leads).Include(x => x.agreementType).Include(x => x.contactSignatory).Include(x => x.contactWitness).Include(x => x.agreementStatus).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            }


        }
        public async Task<IEnumerable<AgreementDetails>> GetApprovedSurveillencebyOwner(string Owner)
        {
            var aspnetuser = _context.Users.Where(x => x.UserName == Owner)?.FirstOrDefault()?.Id;
            var roleId = _context.UserRoles.Where(x => x.UserId == aspnetuser)?.FirstOrDefault()?.RoleId;
            var role = _context.Roles.Where(x => x.Id == roleId)?.FirstOrDefault()?.Name;
            if (role== "HR CRM CRO Admin" || role == "Accounts" || role == "admin" || role == "Manager")
            {
                //return await _context.Agreements.Include(x => x.agreementCategory).Include(x => x.leads).Include(x => x.agreementType).Include(x => x.contactSignatory).Include(x => x.contactWitness).Include(x => x.agreementStatus).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
                return await _context.AgreementDetails.Include(x => x.agreement.agreementCategory).Include(x => x.agreement.leads).Include(x => x.agreement.agreementType).Include(x => x.agreement.ratingCategory).Include(x => x.agreement.contactSignatory).Include(x => x.agreement.contactWitness).Include(x => x.agreement.agreementStatus).Where(x=>x.isProposed==2).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.AgreementDetails.Where(x => x.agreement.agreementOwner == Owner).Include(x => x.agreement.agreementCategory).Include(x => x.agreement.leads).Include(x => x.agreement.agreementType).Include(x => x.agreement.ratingCategory).Include(x => x.agreement.contactSignatory).Include(x => x.agreement.contactWitness).Include(x => x.agreement.agreementStatus).Where(x => x.isProposed == 2).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            }


        }
        public async Task<IEnumerable<AgreementDetails>> GetApprovedSurveillencebyOwnerDate(string Owner,DateTime? Fdate,DateTime? TDate)
        {
            var aspnetuser = _context.Users.Where(x => x.UserName == Owner)?.FirstOrDefault()?.Id;
            var roleId = _context.UserRoles.Where(x => x.UserId == aspnetuser)?.FirstOrDefault()?.RoleId;
            var role = _context.Roles.Where(x => x.Id == roleId)?.FirstOrDefault()?.Name;
            if (role== "HR CRM CRO Admin" || role == "Accounts" || role == "admin" || role == "Manager")
            {
                //return await _context.Agreements.Include(x => x.agreementCategory).Include(x => x.leads).Include(x => x.agreementType).Include(x => x.contactSignatory).Include(x => x.contactWitness).Include(x => x.agreementStatus).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
                return await _context.AgreementDetails.Include(x => x.agreement.agreementCategory).Include(x => x.agreement.leads).Include(x => x.agreement.agreementType).Include(x => x.agreement.contactSignatory).Include(x => x.agreement.ratingCategory).Include(x=>x.ratingYear).Include(x => x.agreement.contactWitness).Include(x => x.agreement.agreementStatus).Where(x=>x.isProposed==2 && x.ratingYearId!=1 && x.ratingDate>=Fdate&& x.ratingDate <= TDate).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.AgreementDetails.Where(x => x.agreement.agreementOwner == Owner).Include(x => x.agreement.agreementCategory).Include(x => x.agreement.leads).Include(x => x.agreement.agreementType).Include(x => x.agreement.ratingCategory).Include(x => x.agreement.contactSignatory).Include(x => x.agreement.contactWitness).Include(x => x.agreement.agreementStatus).Where(x => x.isProposed == 2 && x.ratingYearId != 1 && x.ratingDate >= Fdate && x.ratingDate <= TDate).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            }


        }
        public async Task<IEnumerable<AgreementDetails>> GetReviewedSurveillencebyOwnerDate(string Owner, DateTime? Fdate, DateTime? TDate)
        {
            var aspnetuser = _context.Users.Where(x => x.UserName == Owner)?.FirstOrDefault()?.Id;
            var roleId = _context.UserRoles.Where(x => x.UserId == aspnetuser)?.FirstOrDefault()?.RoleId;
            var role = _context.Roles.Where(x => x.Id == roleId)?.FirstOrDefault()?.Name;
            var q = from t in _context.Agreements
                    group t by t.leadsId
                        into g
                    select new
                    {
                        leadsId = g.Key,
                        Id = (from t2 in g select t2.Id).Max()
                    };
            List<int> leadsIds = _context.Clients.Where(x => x.isactive == 1).Select(x => x.leadsId).ToList();
            List<int> agreementIds = q.Where(x=>leadsIds.Contains((int)x.leadsId)).Select(x => x.Id).ToList();
           
            if (role == "HR CRM CRO Admin" || role == "Accounts" || role == "admin"||role== "Manager")
            {
                //return await _context.Agreements.Include(x => x.agreementCategory).Include(x => x.leads).Include(x => x.agreementType).Include(x => x.contactSignatory).Include(x => x.contactWitness).Include(x => x.agreementStatus).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
                return await _context.AgreementDetails.Include(x => x.agreement.agreementCategory).Include(x => x.agreement.leads).Include(x => x.agreement.agreementType).Include(x => x.agreement.contactSignatory).Include(x => x.agreement.ratingCategory).Include(x => x.ratingYear).Include(x => x.agreement.contactWitness).Include(x => x.agreement.agreementStatus).Include(x => x.ratingYear).Where(x => x.isProposed == 6 && agreementIds.Contains((int)x.agreementId) ).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
                //return await _context.AgreementDetails.Include(x => x.agreement.agreementCategory).Include(x => x.agreement.leads).Include(x => x.agreement.agreementType).Include(x => x.agreement.contactSignatory).Include(x => x.agreement.ratingCategory).Include(x => x.ratingYear).Include(x => x.agreement.contactWitness).Include(x => x.agreement.agreementStatus).Include(x => x.ratingYear).Where(x => x.isProposed == 6 && x.ratingDate >= Fdate && x.ratingDate <= TDate).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            }
            else
            {
                return await _context.AgreementDetails.Where(x => x.agreement.leads.leadOwner == Owner).Include(x => x.agreement.agreementCategory).Include(x => x.agreement.leads).Include(x => x.agreement.agreementType).Include(x => x.agreement.ratingCategory).Include(x => x.agreement.contactSignatory).Include(x => x.agreement.contactWitness).Include(x => x.agreement.agreementStatus).Include(x => x.ratingYear).Where(x => x.isProposed == 6 && agreementIds.Contains((int)x.agreementId)).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
                //return await _context.AgreementDetails.Where(x => x.agreement.agreementOwner == Owner).Include(x => x.agreement.agreementCategory).Include(x => x.agreement.leads).Include(x => x.agreement.agreementType).Include(x => x.agreement.ratingCategory).Include(x => x.agreement.contactSignatory).Include(x => x.agreement.contactWitness).Include(x => x.agreement.agreementStatus).Include(x => x.ratingYear).Where(x => x.isProposed == 6 && x.ratingDate >= Fdate && x.ratingDate <= TDate).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
            }


        }
        public async Task<Agreement> GetAgreementByagreementId(int agreementId)
        {
            return await _context.Agreements.Include(x => x.agreementCategory).Include(x => x.leads.ownerShipType).Include(x => x.leads.companyGroup).Include(x => x.agreementType).Include(x => x.contactSignatory.resource.crmdesignations).Include(x => x.contactWitness.resource.crmdesignations).Where(x => x.Id == agreementId).FirstOrDefaultAsync(); 
        }


        public async Task<Agreement> GetAgreementById(int id)
        {
            return await _context.Agreements.Include(x=>x.ratingCategory).Include(x => x.agreementType).Include(x=>x.leads.companyGroup).Include(x => x.leads.ownerShipType).Include(x => x.agreementCategory).Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAgreementById(int id)
        {
            _context.Agreements.Remove(_context.Agreements.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateForAgreementVerification(int Id, int statusId,string remarks)
        {
            var Agreement = _context.Agreements.Find(Id);
            Agreement.agreementStatusId = statusId;
            if(statusId==4)
            {
                Agreement.reviewremarks = remarks;
            }
           
            _context.Entry(Agreement).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateForAgreementPrint(int Id)
        {
            var Agreement = _context.Agreements.Find(Id);
            Agreement.isPrint = 1;
            Agreement.printDate = DateTime.Now;
            _context.Entry(Agreement).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateForAgreementBackToLead(int Id)
        {
            var Agreement = _context.Agreements.Find(Id);
            Agreement.agreementStatusId = 1;
     
            _context.Entry(Agreement).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<GetAgreementReportViewModel> GetAgreementReportByAgreementId(int AgreementId)
        {
            return await _context.getAgreementReportViewModels.FromSql($"SP_RPT_CLIENT_AGREEMENT {AgreementId}").AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Agreement>> GetAgreementForCRO()
        {
            List<int?> operationMaster = _context.operationMasters.Select(x => x.agreementId).ToList();
            return await _context.Agreements.Where(x => x.agreementStatusId == 3 && !operationMaster.Contains(x.Id)).Include(x => x.agreementCategory).Include(x => x.leads).Include(x => x.agreementType).Include(x => x.contactSignatory).Include(x => x.contactWitness).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
        #endregion

        #region Agreement Type
        public async Task<int> SaveAgreementType(AgreementType  agreementType)
        {
            if (agreementType.Id != 0)
                _context.AgreementTypes.Update(agreementType);
            else
                _context.AgreementTypes.Add(agreementType);
            await _context.SaveChangesAsync();
            return agreementType.Id;
        }

        public async Task<IEnumerable<AgreementType>> GetAllAgreementType()
        {
            return await _context.AgreementTypes.AsNoTracking().ToListAsync();
        }
   
        public async Task<AgreementType> GetAgreementTypeById(int id)
        {
            return await _context.AgreementTypes.FindAsync(id);
        }

        public async Task<bool> DeleteAgreementTypeById(int id)
        {
            _context.AgreementTypes.Remove(_context.AgreementTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Relation Product Agreement

        public async Task<bool> SaveRelProductAgreement(RelProductAgreement relProductAgreement)
        {
            if (relProductAgreement.Id != 0)
                _context.RelProductAgreements.Update(relProductAgreement);
            else
                _context.RelProductAgreements.Add(relProductAgreement);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RelProductAgreement>> GetAllRelProductAgreement()
        {
            return await _context.RelProductAgreements.ToListAsync();
        }

        public async Task<RelProductAgreement> GetRelProductAgreementById(int id)
        {
            return await _context.RelProductAgreements.FindAsync(id);
        }

        public async Task<bool> DeleteRelProductAgreementByagreementId(int agreementId)
        {
            _context.RelProductAgreements.RemoveRange(_context.RelProductAgreements.Where(x => x.agreementId == agreementId));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RelProductAgreement>> GetRelProductAgreementByagreementId(int agreementId)
        {
            return await _context.RelProductAgreements.Include(a => a.product).Include(a => a.agreement).Where(a => a.agreementId == agreementId)
            .Select(e => new RelProductAgreement()
            {
                agreementId = e.agreementId,
                productId = e.productId,
                productName = e.product.productName,
                productDescription = e.product.productDescription
            }).ToListAsync();

        }
        #endregion

        #region Agreement Details
        public async Task<int> SaveAgreementDetails(AgreementDetails agreementDetails)
        {
            if (agreementDetails.Id != 0)
                _context.AgreementDetails.Update(agreementDetails);
            else
                _context.AgreementDetails.Add(agreementDetails);
            await _context.SaveChangesAsync();
            return agreementDetails.Id;
        }

        public async Task<IEnumerable<AgreementDetails>> GetAllAgreementDetails()
        {
            return await _context.AgreementDetails.Include(x => x.agreement).Include(x => x.vatCategory).Include(x => x.taxCategory).Include(x => x.ratingYear).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AgreementDetails>> GetAgreementDetailsbyAgreementId(int AgreementId)
        {
            return await _context.AgreementDetails.Where(x => x.agreementId == AgreementId).Include(x => x.agreement.leads.ownerShipType).Include(x => x.vatCategory).Include(x => x.taxCategory).Include(x => x.ratingYear).AsNoTracking().OrderBy(x=>x.ratingYearId).ToListAsync();
        }
        public async Task<IEnumerable<AgreementDetails>> GetAgreementDetailsbyAgreementRatingYearId(int AgreementId,int RatingYearId)
        {
            return await _context.AgreementDetails.Where(x => x.agreementId == AgreementId && x.ratingYearId == RatingYearId).Include(x => x.agreement).Include(x => x.vatCategory).Include(x => x.taxCategory).Include(x => x.ratingYear).AsNoTracking().ToListAsync();
        }
        //public async Task<IEnumerable<AgreementDetails>> GetAgreementDetailsbyRatingDate(string FDate, string TDate,string Owner)
        //{
        //    List<int?> agreementDetailsIds = _context.ApprovedforCros.Select(x => x.agreementDetailsId).ToList();
        //    return await _context.AgreementDetails.Where(x =>x.agreement.agreementOwner== Owner && !agreementDetailsIds.Contains(x.Id) && x.ratingDate >= Convert.ToDateTime(FDate) && x.ratingDate <= Convert.ToDateTime(TDate) && x.ratingYearId!=1).Include(x => x.agreement.leads).Include(x => x.vatCategory).Include(x => x.taxCategory).Include(x => x.ratingYear).AsNoTracking().ToListAsync();
        //}
        public async Task<IEnumerable<GetAgreementDetailsbyRatingDateViewModel>> GetAgreementDetailsbyRatingDate(string FDate, string TDate, string Owner)
        {
            return await _context.getAgreementDetailsbyRatingDateViewModels.FromSql($"SP_GetAgreementDetailsbyRatingDate {Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")},{Owner}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetAgreementDetailsbyRatingDateViewModel>> GetAgreementDetailsbyRatingDatefilter(string FDate, string TDate, string Owner, string TeamLeader, string Fa, string BD, string LeadId)
        {
            return await _context.getAgreementDetailsbyRatingDateViewModels.FromSql($"SP_GetAgreementDetailsbyRatingDatefilter {Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")},{Owner},{TeamLeader},{Fa},{BD},{LeadId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetAgreementDetailsbyRatingDateSApprovalViewModel>> GetAgreementDetailsbyRatingDateSApproval(string FDate, string TDate, string Owner)
        {
            return await _context.getAgreementDetailsbyRatingDateSApprovalViewModels.FromSql($"SP_GetAgreementDetailsbyRatingDateSApproval {Convert.ToDateTime(FDate).ToString("yyyyMMdd")},{Convert.ToDateTime(TDate).ToString("yyyyMMdd")},{Owner}").AsNoTracking().ToListAsync();
            
        }
        public async Task<IEnumerable<AgreementDetails>> GetAgreementDetailsbyAgreementIdRatingDate(int AgreementId, string FDate, string TDate)
        {
            List<int?> agreementDetailsIds = _context.ApprovedforCros.Select(x => x.agreementDetailsId).ToList();
            return await _context.AgreementDetails.Where(x => x.agreementId== AgreementId && !agreementDetailsIds.Contains(x.Id) && x.ratingDate >= Convert.ToDateTime(FDate) && x.ratingDate <= Convert.ToDateTime(TDate) && x.ratingYearId != 1).Include(x => x.agreement.leads).Include(x => x.vatCategory).Include(x => x.taxCategory).Include(x => x.ratingYear).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AgreementDetails>> GetAgreementDetailsbyleadId(int AgreementId)
        {
            return await _context.AgreementDetails.Include(x => x.agreement.leads).Include(x => x.vatCategory).Include(x => x.taxCategory).Include(x => x.ratingYear).Where(x => x.agreement.leadsId == AgreementId).AsNoTracking().ToListAsync();
        }
        public async Task<AgreementDetails> GetAgreementDetailsById(int id)
        {
            return await _context.AgreementDetails.Include(x => x.ratingYear).Include(x => x.agreement.leads).Where(x=>x.Id==id).FirstOrDefaultAsync();
        }
        public async Task<AgreementDetails> GetAgreementDetailsByaggId(int id)
        {
            return await _context.AgreementDetails.Include(x=>x.ratingYear).Include(x=>x.agreement.leads).Where(x=>x.agreement.Id==id).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteAgreementDetailsById(int id)
        {
            _context.AgreementDetails.Remove(_context.AgreementDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteAgreementDetailsbyAgreementId(int AgreementId)
        {
            _context.AgreementDetails.RemoveRange(_context.AgreementDetails.Where(x => x.agreementId == AgreementId));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateAgreementDetailsPropose(int Id, int statusId,string remarks)
        {
            var Agreement = _context.AgreementDetails.Find(Id);
            Agreement.isProposed = statusId;
            Agreement.remarks = remarks;
            _context.Entry(Agreement).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdateAgreementDetailsProposePro(int Id, int statusId, string remarks)
        {
            var Agreement = _context.AgreementDetails.Find(Id);
            Agreement.isProposed = statusId;
            //Agreement.revremarks = remarks;
            _context.Entry(Agreement).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region rating Year
        public async Task<int> SaveRatingYear(RatingYear ratingYear)
        {
            if (ratingYear.Id != 0)
                _context.RatingYears.Update(ratingYear);
            else
                _context.RatingYears.Add(ratingYear);
            await _context.SaveChangesAsync();
            return ratingYear.Id;
        }

        public async Task<IEnumerable<RatingYear>> GetAllRatingYear()
        {
            return await _context.RatingYears.AsNoTracking().ToListAsync();
        }

        #endregion

        #region Vat Category

        public async Task<IEnumerable<VatCategory>> GetAllVatCategory()
        {
            return await _context.VatCategories.OrderByDescending(x=>x.Id).AsNoTracking().ToListAsync();
        }
        #endregion

        #region Tax Category
        public async Task<IEnumerable<TaxCategory>> GetAllTaxCategory()
        {
            return await _context.TaxCategories.OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
        #endregion

        #region Approved for Cro
        public async Task<int> SaveApprovedforCro(ApprovedforCro approvedforCro)
        {
            if (approvedforCro.Id != 0)
                _context.ApprovedforCros.Update(approvedforCro);
            else
                _context.ApprovedforCros.Add(approvedforCro);
            await _context.SaveChangesAsync();
            return approvedforCro.Id;
        }

        public async Task<IEnumerable<ApprovedforCro>> GetAllApprovedforCro()
        {
            return await _context.ApprovedforCros.AsNoTracking().Include(x => x.agreementDetails.agreement.leads).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        }
        //public async Task<IEnumerable<ApprovedforCro>> GetAgreementForCROrating()
        //{
        //    List<int?> operationMaster = _context.operationMasters.Select(x => x.approvedforCroId).ToList();
        //    return await _context.ApprovedforCros.Where(x => x.agreementDetails.agreement.agreementStatusId == 3 && x.isRated==0 && !operationMaster.Contains(x.Id)).Include(x => x.agreementDetails.agreement.agreementCategory).Include(x => x.agreementDetails.agreement.leads).Include(x => x.agreementDetails.agreement.agreementType).Include(x => x.agreementDetails.ratingYear).Include(x => x.agreementDetails.agreement.contactSignatory).Include(x => x.agreementDetails.agreement.contactWitness).OrderByDescending(x => x.Id).AsNoTracking().ToListAsync();
        //}

        public async Task<IEnumerable<GetAgreementForCROratingViewModel>> GetAgreementForCROrating(string empCode)
        {
            return await _context.getAgreementForCROratingViewModels.FromSql($"SP_GetAgreementForCROrating {empCode}").AsNoTracking().ToListAsync();
        }
        public async Task<ApprovedforCro> GetApprovedforCroById(int id)
        {
            return await _context.ApprovedforCros.Include(x => x.agreementDetails.ratingYear).Include(x=>x.agreementDetails.agreement.leads).Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteApprovedforCroById(int id)
        {
            _context.ApprovedforCros.Remove(_context.ApprovedforCros.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        
        #endregion

    }
}
