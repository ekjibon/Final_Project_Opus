using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMClient.Models;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.SPModel;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{
   
    public class LeadsService : ILeadsService
    {
        private readonly ERPDbContext _context;

        public LeadsService(ERPDbContext context)
        {
            _context = context;
        }

        #region Lead

        public async Task<int> SaveLeads(Leads Leads)
        {
            if (Leads.Id != 0)
                _context.Leads.Update(Leads);
            else
                _context.Leads.Add(Leads);
            await _context.SaveChangesAsync();
            return Leads.Id;
        }
        public async Task<bool> UpdateLeads(int? id, int? leadprogressstatus,string reamrks, string updateBy)
        {
            var VoucherMasters = _context.Leads.Find(id);
            VoucherMasters.leadProgressStatusId = leadprogressstatus;
            VoucherMasters.updatedBy = updateBy;
            VoucherMasters.updatedAt = DateTime.Now;
            VoucherMasters.remarks = reamrks;
            _context.Entry(VoucherMasters).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Leads>> GetAllLeads()
        { 
            return await _context.Leads.Include(x => x.sector).ToListAsync();       
        }
        public async Task<Leads> GetLeadDetailsById(int Id)
        {
            return await _context.Leads.Include(x => x.sector).Where(x => x.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<GetLeadsForConversionListViewModel>> GetLeadsForConversion(string ownername)
        {
            return await _context.getLeadsForConversionListViewModels.FromSql($"SP_GetLeadsForConversionList {ownername}").AsNoTracking().ToListAsync();
            
        }
       
        public async Task<IEnumerable<Leads>> GetLeadsForCollection()
        {
            List<int?> leadids = await _context.BillGenerates.Include(x => x.approvedforCro.agreementDetails.agreement).Select(x => x.approvedforCro.agreementDetails.agreement.leadsId).ToListAsync();
            return await _context.Leads.Where(x => leadids.Contains(x.Id)).AsNoTracking().ToListAsync();

        }

        public async Task<IEnumerable<Leads>> GetLeadInfoByOrg(string org)
        {
            return await _context.Leads.Where(x => x.leadName == org).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Leads>> GetLeadInfoByfil(string org)
        {
            return await _context.Leads.Include(x=>x.sector).Where(x => x.leadName.Contains(org)).AsNoTracking().ToListAsync();
        }
        public async Task<Leads> GetLeadsById(int id)
        {
            return await _context.Leads.Include(x => x.ownerShipType).Include(x => x.companyGroup).Where(x=>x.Id==id).AsNoTracking().FirstAsync();
        }
        public async Task<IEnumerable<Leads>> GetLeadslistById(int id)
        {
            return await _context.Leads.Include(x => x.ownerShipType).Include(x => x.companyGroup).Where(x => x.Id == id).AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteLeadsById(int id)
        {
            _context.Leads.Remove(_context.Leads.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }      

        public async Task<IEnumerable<AllLeadJson>> GetLeadInfoAsQueryAblebyOwner(string LeadOwner)
        {
            IEnumerable<Leads> data = await _context.Leads.ToListAsync();
            List<AllLeadJson> filteredData = new List<AllLeadJson>();

            #region Result Process

            foreach (Leads employeeInfo in data)
            {
                filteredData.Add(new AllLeadJson
                {
                    leadName = employeeInfo.leadName,
                    leadOwner = employeeInfo.leadOwner,
                    action = $"<a class='btn btn-success' data-toggle='tooltip' title='Edit' target='_blank' href='/LeadManagement/LeadManagement/LeadInfo/?Id={employeeInfo.Id}&leadName={employeeInfo.leadName}'><i class='fa fa-edit'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='View' target='_blank' href='/Employee/Info/GridView/{employeeInfo.Id}'><i class='fa fa-search'></i></a> <a class='btn btn-success' data-toggle='tooltip' title='Preview' target='_blank' href='/Employee/InfoView/Index/{employeeInfo.Id}'><i class='fas fa-eye'></i></a> <a class='btn btn-info' data-toggle='tooltip' title='Print' target='_blank' href='/Employee/InfoView/pdspdf/{employeeInfo.Id}'><i class='fa fa-print'></i></a>"
                });
            }
            #endregion

            return filteredData;
        }
        
        public async Task<IEnumerable<GetLeadInfoListViewModel>> GetLeadInfoAsQueryAble(string ownername)
        {
            return await _context.getLeadInfoListViewModels.FromSql($"SP_GetLeadInfoList {ownername}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetLeadInfoListViewModel>> GetLeadInfofilterAsQueryAble(string ownername,string bank,string branch,string BD,string leadId)
        {
            return await _context.getLeadInfoListViewModels.FromSql($"SP_GetLeadInfoListfilter {ownername},{bank},{branch},{BD},{leadId}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GetLeadInfoVerificationListViewModel>> GetLeadInfoVerification(string ownername)
        {
            return await _context.getLeadInfoVerificationListViewModels.FromSql($"SP_GetClientInfoListforVerification {ownername}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetLeadInfoPrintListViewModel>> GetLeadInfoPrint(string ownername)
        {
            return await _context.getLeadInfoPrintListViewModels.FromSql($"SP_GetClientInfoListforPrint {ownername}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Leads>> GetAllLeadsByuser(string user)
        {
            var data= await _context.Leads.Include(x=>x.sector).Where(x => x.leadOwner.ToLower() == user.ToLower()).ToListAsync();
            //var result= data.Where(x => x.leadOwner.ToLower() == user.ToLower());
            return data;
        }

        public async Task<IEnumerable<AllClientViewModel>> GetAllLeadsByuserSP(string ownername)
        {
            return await _context.allClientViewModels.FromSql($"getallclientforviewbybd {ownername}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AllClientViewModel>> GetAllLeadsByNameSP(string ownername)
        {
            return await _context.allClientViewModels.FromSql($"getallclientforviewbylead {ownername}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AllClientViewModel>> GetAllLeadsListSP()
        {
            return await _context.allClientViewModels.FromSql($"getallclientforviewbyleadall").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<AllClientCViewModel>> GetAllLeadsCByuserSP(string ownername)
        {
            return await _context.allClientCViewModels.FromSql($"getallclientCforviewbybd {ownername}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AllClientCViewModel>> GetAllLeadsCByNameSP(string ownername)
        {
            return await _context.allClientCViewModels.FromSql($"getallclientCforviewbylead {ownername}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<AllClientCViewModel>> GetAllLeadCsListSP()
        {
            return await _context.allClientCViewModels.FromSql($"getallclientCforviewbyleadall").AsNoTracking().ToListAsync();
        }
        //end of wahid

        public async Task<IEnumerable<Leads>> GetLeadsByLeadOwner(string LeadOwner)
        {
            return await _context.Leads.Include(x => x.ownerShipType).Include(x => x.companyGroup).Where(x => x.leadOwner == LeadOwner).ToListAsync();
        }

        public async Task<LeadAutoNumber> GetLeadAutoNumberBySp()
        {
            var data = _context.GetLeadAutoNumberBySp.FromSql("SP_AutoLeadNo");
            return await data.FirstOrDefaultAsync();
        }

        public Task<Leads> GetTotalLead()
        {
            return _context.Leads.FirstOrDefaultAsync();
        }

        #endregion

        #region Lead History

        public async Task<bool> SaveLeadsHistory(LeadsHistory leadsHistory)
        {
            if (leadsHistory.Id != 0)
                _context.LeadsHistories.Update(leadsHistory);
            else
                _context.LeadsHistories.Add(leadsHistory);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<LeadsHistory>> GetLeadsHistoryByLeadId(int leadId)
        {
            return await _context.LeadsHistories.Include(a => a.leads).Where(a => a.leadsId == leadId).OrderByDescending(a => a.Id).ToListAsync();
        }
        public async Task<IEnumerable<LeadsHistory>> GetAllLeadsHistory()
        {
            return await _context.LeadsHistories.Include(a => a.leads).OrderByDescending(a => a.Id).ToListAsync();
        }
        public async Task<IEnumerable<ActivityLogShowViewModel>> GetAllLeadsHistorySP()
        {
            return await _context.activityLogShowViewModels.FromSql($"getactivityhistory").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ActivityLogShowViewModel>> GetAllLeadsHistorySPfilter(string BD,string LeadId)
        {
            return await _context.activityLogShowViewModels.FromSql($"getactivityhistoryfilter {BD},{LeadId}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ActivityLogShowViewModel>> GetAllLeadsHistorySurSP()
        {
            return await _context.activityLogShowViewModels.FromSql($"getactivityhistorysurvellience").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ChangeOwnerLogShowViewModel>> GetchangeOwnerLogShowViewModelsSP()
        {
            return await _context.changeOwnerLogShowViewModels.FromSql($"getownerchangehistory").AsNoTracking().ToListAsync();
        }

        #endregion

        #region Others Lead service
        public async Task<IEnumerable<GetOwnerChangeViewModel>> SaveChangeOwnerUserOneToOne(string previousOwner, string previousOwnerUser, string newOwner, string newOwnerUser, string user)
        {
            return await _context.getOwnerChangeViewModels.FromSql($"spUpdateChangeOwnerUserOneToOne {previousOwner},{previousOwnerUser},{newOwner},{newOwnerUser},{user}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetOwnerChangeViewModel>> SaveChangeOwnerSingleUser(int? leadsId, string previousOwner, string previousOwnerUser, string newOwner, string newOwnerUser, string user)
        {
            return await _context.getOwnerChangeViewModels.FromSql($"spUpdateChangeOwnerSingleUser {leadsId},{previousOwner},{previousOwnerUser},{newOwner},{newOwnerUser},{user}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetOwnerChangeViewModel>> SaveChangeActivity(int? leadsId, int?isactive, string user)
        {
            return await _context.getOwnerChangeViewModels.FromSql($"spUpdateActivity {leadsId},{isactive},{user}").AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<System.Object>> GetLeadContactAddress(int leadId)
        {
            try
            {
                var result = (from l in _context.Leads
                              select new
                              {
                                  leadsId=l.Id,
                                  leadsName = l.leadName,
                                  mobile = (from cn in _context.Contact
                                            join r in _context.Resources on cn.resourceId equals r.Id
                                            where l.Id == cn.leadsId
                                            select r.mobile).FirstOrDefault(),
                                  address = (from ad in _context.Address
                                             join dv in _context.Divisions on ad.divisionId equals dv.Id
                                             join dst in _context.Districts on ad.districtId equals dst.Id
                                             join th in _context.Thanas on ad.thanaId equals th.Id
                                             where l.Id == ad.leadsId
                                             select ad.maillingAddress + ", " + th.thanaName + ", " + dst.districtName + ", " + dv.divisionName).FirstOrDefault(),
                              }).Where(a => a.leadsId == leadId).OrderByDescending(x => x.leadsId).AsNoTracking().ToListAsync();

                return await result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<System.Object>> GetLeadPatientInfoByLeadId(int leadId)
        {
            try
            {
                var result = (from l in _context.Leads
                              join c in _context.Contact on l.Id equals c.leadsId into CC
                              from con in CC.DefaultIfEmpty()
                              where con.isLead == 1
                              join R in _context.Resources on con.resourceId equals R.Id into RR
                              from res in RR.DefaultIfEmpty()
                              select new
                              {
                                  leadsId = l.Id,                                  
                                  patientName = l.leadName,
                                  res.fatherName,
                                  res.motherName,
                                  ageInYears = res.age,
                                  res.ageInMonths,
                                  res.ageInDays,
                                  res.bloodGroup,
                                  res.maritalStatus,
                                  res.gender,
                                  res.nationalId,
                                  patientMobile = res.mobile,
                                  patientPhone = res.phone,
                                  patientEmail = res.email,                                  
                                  resourceId = res.Id,
                                  contactId = con.Id

                              }).Where(a => a.leadsId == leadId).OrderByDescending(x => x.leadsId).AsNoTracking().ToListAsync();

                return await result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<string> GetLeadNameAndCodeById(int? Id)
        {
            Leads data = await _context.Leads.FindAsync(Id);
            return data.leadName + " (" + data.leadNumber + ")";
        }

        #endregion

        #region Report

        public async Task<IEnumerable<LeadReportViewModel>> GetLeadReport(int? sectorId, int? fitypeId, int? sourceId, string leadOwner)
        {
            return await _context.leadReportViewModels.FromSql($"SP_RPT_LeadInfo {sectorId},{fitypeId},{sourceId},{leadOwner}").AsNoTracking().ToListAsync();
        }

        #endregion
    }
}
