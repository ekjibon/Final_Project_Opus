using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMClient.Models.Dashboard;
using OPUSERP.CRM.Data.Entity.Client;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Data.Entity.Proposal;
using OPUSERP.CRM.Services.Dashboard.Interfaces;
using OPUSERP.Data;
using OPUSERP.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Employee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Dashboard
{
    public class CRMDashboardService : ICRMDashboardService
    {
        private readonly ERPDbContext _context;

        public CRMDashboardService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<CRMDataCountViewModel> GetRMDataCountViewModel()
        {
            List<CRMDataCountViewModel> models = new List<CRMDataCountViewModel>();
            try
            {
                DateTime date = DateTime.Now;
                var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                int monthno = DateTime.Now.Month;
                DateTime premonth = DateTime.Now.AddMonths(monthno * -1);
                List<DateTime> months = new List<DateTime>();
                List<string> monthshow = new List<string>();
                List<int> activityCompletePercent = new List<int>();
                List<int> activityInprogressPercent = new List<int>();
                List<string> leadsprogessstatus = new List<string>();
                List<int> leadsprogessstatuscount = new List<int>();
                List<string> productsname = new List<string>();
                List<int> productscount = new List<int>();
                List<string> sourcesname = new List<string>();
                List<int> sourcescount = new List<int>();
                List<string> teams = new List<string>();
                List<int> leadgenerationcount = new List<int>();
                List<int> leadproposalcount = new List<int>();
                List<int> leadagreementcount = new List<int>();
                List<int> leadconversationcount = new List<int>();
                List<string> ownerships = new List<string>();
                List<int> ownershipscount = new List<int>();

                List<int> technicalproposalcount = new List<int>();
                List<int> financialproposalcount = new List<int>();
                List<int> financialtechnicalproposalcount = new List<int>();
                List<LeadProgressStatus> leadProgressStatuses = _context.leadProgressStatuses.ToList();
                
                List<Leads> leads = _context.Leads.ToList();
                List<Clients> clients = _context.Clients.ToList();
                List<ActivityMaster> activityMasters = _context.ActivityMasters.Include(x => x.activityStatus).ToList();
                List<Product> products = _context.Products.ToList();
                List<RelProductAgreement> relProductAgreements = _context.RelProductAgreements.ToList();
                List<Source> sources = _context.Sources.ToList();
                List<Team> datateams = _context.Teams.ToList();
                List<EmployeeInfo> employeeInfos = _context.employeeInfos.Include(x => x.ApplicationUser).ToList();
                List<OwnerShipType> ownerShipTypes = _context.OwnerShipTypes.ToList();
                // List<ProposalType> proposalTypes = _context.ProposalTypes.ToList();
                List<ProposalMaster> proposalMasters = _context.ProposalMasters.Include(x => x.proposalType).Include(x=>x.leads).Include(x => x.leads.leadProgressStatus).ToList();
                for (int i = 0; i <= monthno; i++)
                {
                    monthshow.Add(Convert.ToDateTime(premonth).ToString("MMMM"));
                    int complete = activityMasters.Where(x => x.activityStatus.status == "Completed" && Convert.ToDateTime(x.createdAt).ToString("yyyMM") == Convert.ToDateTime(premonth).ToString("yyyMM")).Count() * 100 / activityMasters.Count();
                    int Incomplete = activityMasters.Where(x => x.activityStatus.status != "Completed" && Convert.ToDateTime(x.createdAt).ToString("yyyMM") == Convert.ToDateTime(premonth).ToString("yyyMM")).Count() * 100 / activityMasters.Count();
                    activityCompletePercent.Add(complete);
                    activityInprogressPercent.Add(complete);
                    premonth = premonth.AddMonths(1);
                }
                foreach (LeadProgressStatus l in leadProgressStatuses)
                {

                    leadsprogessstatus.Add(l.name);
                    int leadcount = leads.Where(x => x.leadProgressStatusId == l.Id).Count();
                    leadsprogessstatuscount.Add(leadcount);
                }
                foreach (Product p in products)
                {
                    productsname.Add(p.productName);
                    int pcount = relProductAgreements.Where(x => x.productId == p.Id).Count();
                    productscount.Add(pcount);
                }
                foreach (Source p in sources.Take(5))
                {
                    sourcesname.Add(p.sourceName);
                    int pcount = leads.Where(x => x.sourceId == p.Id).Count();
                    sourcescount.Add(pcount);
                }
                foreach (Team p in datateams.Distinct())
                {
                    string empName = employeeInfos. Where(x => x.ApplicationUserId == p.aspnetuserId).Select(x => x.nameEnglish).FirstOrDefault();
                    teams.Add(empName);
                    int gcount = leads.Where(x => x.leadOwner == p.aspnetuser.UserName && x.leadProgressStatus.name == "Lead Generation").Count();
                    leadgenerationcount.Add(gcount);
                    int pcount = leads.Where(x => x.leadOwner == p.aspnetuser.UserName && x.leadProgressStatus.name == "Proposal Send").Count();
                    leadproposalcount.Add(pcount);
                    int acount = leads.Where(x => x.leadOwner == p.aspnetuser.UserName && x.leadProgressStatus.name == "Agreement").Count();
                    leadagreementcount.Add(acount);
                    int ccount = leads.Where(x => x.leadOwner == p.aspnetuser.UserName && x.leadProgressStatus.name == "Lead Converstion").Count();
                    leadconversationcount.Add(ccount);
                    int techcount = proposalMasters.Where(x => x.leads.leadOwner == p.aspnetuser.UserName).Count();
                    technicalproposalcount.Add(techcount);
                    int finaccount = proposalMasters.Where(x => x.leads.leadOwner == p.aspnetuser.UserName && x.leads.leadProgressStatus.name == "Lead Converstion").Count();
                    financialproposalcount.Add(finaccount);
                    //int techfincount = proposalMasters.Where(x => x.leads.leadOwner == p.aspnetuser.UserName && x.proposalType.proposalTypeName == "Technical & Financial").Count();
                    //financialtechnicalproposalcount.Add(techfincount);

                }
                foreach (OwnerShipType p in ownerShipTypes)
                {
                    ownerships.Add(p.ownerShipTypeName);
                    int pcount = leads.Where(x => x.ownerShipTypeId == p.Id).Count();
                    ownershipscount.Add(pcount);
                }
                //foreach (ProposalType p in proposalTypes)
                //{
                //    ownerships.Add(p.ownerShipTypeName);
                //    int pcount = leads.Where(x => x.ownerShipTypeId == p.Id).Count();
                //    ownershipscount.Add(pcount);
                //}
                models.Add(new CRMDataCountViewModel
                {
                    TotalLead = leads.Count(),
                    TotalClient = clients.Count(),
                    ThisMonthTotalClient = clients.Where(x => Convert.ToDateTime(x.createdAt).Date >= Convert.ToDateTime(firstDayOfMonth).Date && Convert.ToDateTime(x.createdAt).Date <= Convert.ToDateTime(lastDayOfMonth).Date).Count(),
                    ThisMonthTotalLead = leads.Where(x => Convert.ToDateTime(x.createdAt).Date >= Convert.ToDateTime(firstDayOfMonth).Date && Convert.ToDateTime(x.createdAt).Date <= Convert.ToDateTime(lastDayOfMonth).Date).Count(),
                    clientpercent = clients.Count() * 100 / leads.Count(),
                    notClientpercent = 100 - clients.Count() * 100 / leads.Count(),
                    months = monthshow,
                    activityCompletePercent = activityCompletePercent,
                    activityInprogressPercent = activityInprogressPercent,
                    leadprogressstatus = leadsprogessstatus,
                    leadprogressstatuscount = leadsprogessstatuscount,
                    products = productsname,
                    productscount = productscount,
                    sources = sourcesname,
                    sourcescount = sourcescount,
                    teams = teams,
                    countleadagrement = leadagreementcount,
                    countleadconversion = leadconversationcount,
                    countleadgeneration = leadgenerationcount,
                    countleadproposal = leadproposalcount,
                    OwnerShipTypes = ownerships,
                    countOwnerShipTypes = ownershipscount,
                    countfinacial = financialproposalcount,
                    counttechnical = technicalproposalcount,
                    countfinacialtechnical = financialtechnicalproposalcount



                });
            }
            catch (Exception ex)
            {
                 ex.Message.ToString();
            }
           
            return models.FirstOrDefault();
        }
    }
}
