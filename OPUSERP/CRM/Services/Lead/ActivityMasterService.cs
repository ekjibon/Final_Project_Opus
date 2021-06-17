using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMLead.Models;
using OPUSERP.CRM.Data.Entity.Cold;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.CRM.Services.Lead.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Lead
{

    public class ActivityMasterService : IActivityMasterService
    {
        private readonly ERPDbContext _context;

        public ActivityMasterService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveActivityMaster(ActivityMaster ActivityMaster)
        {
            if (ActivityMaster.Id != 0)
                _context.ActivityMasters.Update(ActivityMaster);
            else
                _context.ActivityMasters.Add(ActivityMaster);
            await _context.SaveChangesAsync();
            return ActivityMaster.Id;
        }

        public async Task<IEnumerable<ActivityMaster>> GetAllActivityMaster()
        {
            return await _context.ActivityMasters.Include(x => x.leads).Include(x => x.activityCategory).ToListAsync();
        }
        public async Task<IEnumerable<ActivityMaster>> GetAllActivityMasterbyuser(string aspuser)
        {
            List<int> lstact = _context.ActivityTeams.Where(x => x.team.aspnetuserId == aspuser).Select(x => x.activityMaster.Id).ToList();
            return await _context.ActivityMasters.Where(x => lstact.Contains(x.Id)).Include(x => x.leads).Include(x => x.activityCategory).ToListAsync();
        }

        public async Task<ActivityMaster> GetActivityMasterById(int id)
        {
            return await _context.ActivityMasters.Where(x => x.Id == id).Include(x => x.leads).Include(x => x.activityCategory).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<ActivityMasterCAViewModel>> GetActivityMasterByuserparentId(int id, string username)
        {

            ActivityMaster activityMastern;
            do
            {
                activityMastern = await _context.ActivityMasters.FindAsync(id);
                id = activityMastern.activityMasterId ?? 0;
            }
            while (id != 0);
            id = activityMastern.Id;
            List<ActivityMasterCAViewModel> actlist = new List<ActivityMasterCAViewModel>();
            ActivityMaster lstmaster = await _context.ActivityMasters.Where(x => x.Id == id).Include(x => x.leads).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.activityMaster).Include(x => x.activitySession).Include(x => x.activityType).FirstOrDefaultAsync();
            List<ActivityDetails> lstdetails = await _context.ActivityDetails.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
            List<ActivityTeam> lstTeam = await _context.ActivityTeams.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
            actlist.Add(new ActivityMasterCAViewModel
            {

                activityMasters = lstmaster,
                activityDetails = lstdetails,
                activityTeam = lstTeam

            });

            int sourceId = lstmaster.Id;


            do
            {
                List<ActivityMaster> activityMaster = await _context.ActivityMasters.Where(x => x.activityMasterId == sourceId).Include(x => x.leads).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.activityMaster).Include(x => x.activitySession).Include(x => x.activityType).ToListAsync();
                if (activityMaster.Count() != 0)
                {

                    foreach (ActivityMaster item in activityMaster)
                    {
                        List<ActivityDetails> ActivityDetailss = new List<ActivityDetails>();
                        List<ActivityDetails> lstdetailsfor = await _context.ActivityDetails.Where(x => x.activityMasterId == item.Id).Include(x => x.activityMaster).ToListAsync();

                        foreach (ActivityDetails data in lstdetailsfor)
                        {
                            var resource = _context.Resources.Where(x => x.Id == data.contactsId).FirstOrDefault();
                            ActivityDetailss.Add(new ActivityDetails
                            {
                                activityMasterId = data.activityMasterId,
                                contactsId = data.contactsId,
                                contactName = resource.resourceName

                            });

                        }
                        List<ActivityTeam> lstTeamfor = await _context.ActivityTeams.Where(x => x.activityMasterId == item.Id).Include(x => x.team).ToListAsync();
                        actlist.Add(new ActivityMasterCAViewModel
                        {
                            activityMasters = item,
                            activityDetails = ActivityDetailss,
                            activityTeam = lstTeamfor

                        });

                        sourceId = item.Id;

                    }
                }
                else
                {
                    sourceId = 0;
                }

            }
            while (sourceId != 0);




            return actlist.Where(x => x.activityMasters.createdBy == username);
        }
        public async Task<IEnumerable<ColdActivityMasterCAViewModel>> GetColdActivityMasterByuserparentId(int id, string username)
        {

            ColdActivityMasters activityMastern;
            do
            {
                activityMastern = await _context.ColdActivityMasters.FindAsync(id);
                id = activityMastern.coldActivityMastersId ?? 0;
            }
            while (id != 0);
            id = activityMastern.Id;
            List<ColdActivityMasterCAViewModel> actlist = new List<ColdActivityMasterCAViewModel>();
            ColdActivityMasters lstmaster = await _context.ColdActivityMasters.Where(x => x.Id == id).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.coldActivityMasters).Include(x => x.activitySession).Include(x => x.activityType).FirstOrDefaultAsync();
            List<ColdActivityDetails> lstdetails = await _context.ColdActivityDetails.Where(x => x.coldActivityMastersId == lstmaster.Id).ToListAsync();
            List<ColdActivityTeams> lstTeam = await _context.ColdActivityTeams.Where(x => x.coldActivityMastersId == lstmaster.Id).ToListAsync();
            actlist.Add(new ColdActivityMasterCAViewModel
            {

                activityMasters = lstmaster,
                activityDetails = lstdetails,
                activityTeam = lstTeam

            });

            int sourceId = lstmaster.Id;


            do
            {
                List<ColdActivityMasters> activityMaster = await _context.ColdActivityMasters.Where(x => x.coldActivityMastersId == sourceId).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.coldActivityMasters).Include(x => x.activitySession).Include(x => x.activityType).ToListAsync();
                if (activityMaster.Count() != 0)
                {

                    foreach (ColdActivityMasters item in activityMaster)
                    {
                        List<ColdActivityDetails> ActivityDetailss = new List<ColdActivityDetails>();
                        ActivityDetailss = await _context.ColdActivityDetails.Where(x => x.coldActivityMastersId == item.Id).Include(x => x.coldActivityMasters).ToListAsync();


                        List<ColdActivityTeams> lstTeamfor = await _context.ColdActivityTeams.Where(x => x.coldActivityMastersId == item.Id).Include(x => x.team).ToListAsync();
                        actlist.Add(new ColdActivityMasterCAViewModel
                        {
                            activityMasters = item,
                            activityDetails = ActivityDetailss,
                            activityTeam = lstTeamfor

                        });

                        sourceId = item.Id;

                    }
                }
                else
                {
                    sourceId = 0;
                }

            }
            while (sourceId != 0);




            return actlist.Where(x => x.activityMasters.createdBy == username);
        }
        public async Task<IEnumerable<ActivityReportViewModel>> GetActivityReportViewModels(string FromDate, string ToDate, string TaskOwner, int LeadId, string TaskBy)
        {
            IEnumerable<ActivityReportViewModel> activityReportViewModels = await _context.activityReportViewModels.FromSql($"GetActivityList {FromDate},{ToDate},{TaskOwner},{LeadId},{TaskBy}").AsNoTracking().ToListAsync();
            return activityReportViewModels;

        }
        public async Task<IEnumerable<ActivityMasterCAViewModel>> GetActivityMasterByparentId(int id)
        {
            List<ActivityMasterCAViewModel> actlist = new List<ActivityMasterCAViewModel>();
            ActivityMaster activityMastern;
            do
            {
                activityMastern = await _context.ActivityMasters.FindAsync(id);
                id = activityMastern.activityMasterId ?? 0;
            }
            while (id != 0);
            id = activityMastern.Id;
           List<ActivityMasteIdViewModel> activityMasteIdViewModels=  await _context.activityMasteIdViewModels.FromSql($"getallchildactivity {id}").ToListAsync();
            // var dddata =await GenerateTree(id);
            string fnumber = string.Empty;
            foreach (ActivityMasteIdViewModel fdata in activityMasteIdViewModels)
            {
                if (fdata.activityMasterId == 0)
                {
                    fnumber = "1";
                }
                else
                {
                    ActivityMasterCAViewModel cdata = actlist.Where(x => x.activityMasters.Id == fdata.activityMasterId).FirstOrDefault();
                    List<ActivityMasterCAViewModel>lcdata= actlist.Where(x => x.activityMasters.activityMasterId == fdata.activityMasterId).ToList();
                    if (lcdata.Count() == 0)
                    {
                        fnumber = cdata.number + "." + "1";
                    }
                    else
                    {
                        fnumber = cdata.number + "." + (lcdata.Count()+1).ToString();
                    }
                }
                ActivityMaster lstmaster = await _context.ActivityMasters.Where(x => x.Id == fdata.Id).Include(x => x.leads).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.activityMaster).Include(x => x.activitySession).Include(x => x.activityType).FirstOrDefaultAsync();
                List<ActivityDetails> lstdetails = await _context.ActivityDetails.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
                List<ActivityTeam> lstTeam = await _context.ActivityTeams.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
                actlist.Add(new ActivityMasterCAViewModel
                {
                    number = fnumber,
                    activityMasters = lstmaster,
                    activityDetails = lstdetails,
                    activityTeam = lstTeam

                });

            }


            
            //ActivityMaster lstmaster = await _context.ActivityMasters.Where(x => x.Id == id).Include(x => x.leads).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.activityMaster).Include(x => x.activitySession).Include(x => x.activityType).FirstOrDefaultAsync();
            //List<ActivityDetails> lstdetails = await _context.ActivityDetails.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
            //List<ActivityTeam> lstTeam = await _context.ActivityTeams.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
            //actlist.Add(new ActivityMasterCAViewModel
            //{
            //    number = "1",
            //    activityMasters = lstmaster,
            //    activityDetails = lstdetails,
            //    activityTeam = lstTeam

            //});
            //int i = 0;
            //string rnumber = "1";
            //string rrnumber = "1";
            //List<ActivityMaster> lstofactivity = _context.ActivityMasters.Where(x => x.activityMasterId == id).Include(x => x.leads).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.activityMaster).Include(x => x.activitySession).Include(x => x.activityType).ToList();
            //foreach (ActivityMaster acmaster in lstofactivity)
            //{
            //    rnumber = "1";
            //    rrnumber = "1";
            //    i++;
            //    rnumber = rnumber + "." + i.ToString();
            //    rrnumber = rrnumber + "." + i.ToString();
            //    lstmaster = await _context.ActivityMasters.Where(x => x.Id == acmaster.Id).Include(x => x.leads).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.activityMaster).Include(x => x.activitySession).Include(x => x.activityType).FirstOrDefaultAsync();
            //    lstdetails = await _context.ActivityDetails.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
            //    lstTeam = await _context.ActivityTeams.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
            //    actlist.Add(new ActivityMasterCAViewModel
            //    {
            //        number = rnumber,
            //        activityMasters = lstmaster,
            //        activityDetails = lstdetails,
            //        activityTeam = lstTeam

            //    });

            //    int sourceId = acmaster.Id;


            //    do
            //    {
            //        List<ActivityMaster> activityMaster = await _context.ActivityMasters.Where(x => x.activityMasterId == sourceId).Include(x => x.leads).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.activityMaster).Include(x => x.activitySession).Include(x => x.activityType).ToListAsync();
            //        if (activityMaster.Count() != 0)
            //        {
            //            int j = 0;


            //            string rnumberf = string.Empty;
                        
            //            foreach (ActivityMaster item in activityMaster)
            //            {
            //                j++;


            //                List<ActivityDetails> ActivityDetailss = new List<ActivityDetails>();
            //                List<ActivityDetails> lstdetailsfor = await _context.ActivityDetails.Where(x => x.activityMasterId == item.Id).Include(x => x.activityMaster).ToListAsync();

            //                foreach (ActivityDetails data in lstdetailsfor)
            //                {
            //                    var resource = _context.Resources.Where(x => x.Id == data.contactsId).FirstOrDefault();
            //                    ActivityDetailss.Add(new ActivityDetails
            //                    {
            //                        activityMasterId = data.activityMasterId,
            //                        contactsId = data.contactsId,
            //                        contactName = resource.resourceName

            //                    });

            //                }
            //                List<ActivityTeam> lstTeamfor = await _context.ActivityTeams.Where(x => x.activityMasterId == item.Id).Include(x => x.team).ToListAsync();
            //                rnumberf = rrnumber + "." + j.ToString();
            //                actlist.Add(new ActivityMasterCAViewModel
            //                {
            //                    number = rrnumber + "." + j.ToString(),
            //                    activityMasters = item,
            //                    activityDetails = ActivityDetailss,
            //                    activityTeam = lstTeamfor

            //                });

            //                sourceId = item.Id;

            //            }
            //            rrnumber = rnumberf;
            //        }

            //        else
            //        {
            //            sourceId = 0;
            //        }

            //    }
            //    while (sourceId != 0);


            //}


            return actlist.OrderBy(x=>x.number);
        }


        public async Task<IEnumerable<ActivityMasterCAViewModel>> GetActivityMasterByleadId(int id)
        {
            List<ActivityMasterCAViewModel> actlist = new List<ActivityMasterCAViewModel>();
            List<ActivityMaster> activityMasters = await _context.ActivityMasters.Where(x => x.leadsId == id && x.activityMasterId == null).ToListAsync();
            ActivityMaster activityMastern;
            int i = 0;
            foreach (ActivityMaster acdata in activityMasters)
            {
                do
                {
                    activityMastern = await _context.ActivityMasters.FindAsync(acdata.Id);
                    id = activityMastern.activityMasterId ?? 0;
                }
                while (id != 0);
                id = activityMastern.Id;
                List<ActivityMasteIdViewModel> activityMasteIdViewModels = await _context.activityMasteIdViewModels.FromSql($"getallchildactivity {id}").ToListAsync();
                // var dddata =await GenerateTree(id);
                string fnumber = string.Empty;
                i++;
                foreach (ActivityMasteIdViewModel fdata in activityMasteIdViewModels)
                {
                    
                    if (fdata.activityMasterId == 0)
                    {
                        List<ActivityMasterCAViewModel> lccdata = actlist.Where(x => x.activityMasters.activityMasterId == 0 && x.activityMasters.leadsId==acdata.leadsId).ToList();
                        if (lccdata.Count() == 0)
                        {
                            fnumber = i.ToString() + "." + "1";
                        }
                        else
                        {
                            fnumber = i.ToString() + "." + (lccdata.Count()+1).ToString();

                        }
                        
                    }
                    else
                    {
                        ActivityMasterCAViewModel cdata = actlist.Where(x => x.activityMasters.Id == fdata.activityMasterId).FirstOrDefault();
                        List<ActivityMasterCAViewModel> lcdata = actlist.Where(x => x.activityMasters.activityMasterId == fdata.activityMasterId).ToList();
                        if (lcdata.Count() == 0)
                        {
                            fnumber =cdata.number + "." + "1";
                        }
                        else
                        {
                            fnumber =cdata.number + "." + (lcdata.Count() + 1).ToString();
                        }
                    }
                    ActivityMaster lstmaster = await _context.ActivityMasters.Where(x => x.Id == fdata.Id).Include(x => x.leads).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.activityMaster).Include(x => x.activitySession).Include(x => x.activityType).FirstOrDefaultAsync();
                    List<ActivityDetails> lstdetails = await _context.ActivityDetails.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
                    List<ActivityTeam> lstTeam = await _context.ActivityTeams.Where(x => x.activityMasterId == lstmaster.Id).ToListAsync();
                    actlist.Add(new ActivityMasterCAViewModel
                    {
                        number = fnumber,
                        activityMasters = lstmaster,
                        activityDetails = lstdetails,
                        activityTeam = lstTeam

                    });

                }

            }






            return actlist.OrderBy(x => x.number);
        }



        public async Task<IEnumerable<ColdActivityMasterCAViewModel>> GetColdActivityMasterByparentId(int id)
        {

            ColdActivityMasters activityMastern;
            do
            {
                activityMastern = await _context.ColdActivityMasters.FindAsync(id);
                id = activityMastern.coldActivityMastersId ?? 0;
            }
            while (id != 0);
            id = activityMastern.Id;
            List<ColdActivityMasterCAViewModel> actlist = new List<ColdActivityMasterCAViewModel>();
            ColdActivityMasters lstmaster = await _context.ColdActivityMasters.Where(x => x.Id == id).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.coldActivityMasters).Include(x => x.activitySession).Include(x => x.activityType).FirstOrDefaultAsync();
            List<ColdActivityDetails> lstdetails = await _context.ColdActivityDetails.Where(x => x.coldActivityMastersId == lstmaster.Id).ToListAsync();
            List<ColdActivityTeams> lstTeam = await _context.ColdActivityTeams.Where(x => x.coldActivityMastersId == lstmaster.Id).ToListAsync();
            actlist.Add(new ColdActivityMasterCAViewModel
            {

                activityMasters = lstmaster,
                activityDetails = lstdetails,
                activityTeam = lstTeam

            });

            int sourceId = lstmaster.Id;


            do
            {
                List<ColdActivityMasters> activityMaster = await _context.ColdActivityMasters.Where(x => x.coldActivityMastersId == sourceId).Include(x => x.activityCategory).Include(x => x.activityStatus).Include(x => x.coldActivityMasters).Include(x => x.activitySession).Include(x => x.activityType).ToListAsync();
                if (activityMaster.Count() != 0)
                {

                    foreach (ColdActivityMasters item in activityMaster)
                    {
                        List<ColdActivityDetails> ActivityDetailss = new List<ColdActivityDetails>();
                        ActivityDetailss = await _context.ColdActivityDetails.Where(x => x.coldActivityMastersId == item.Id).Include(x => x.coldActivityMasters).ToListAsync();

                        //foreach (ColdActivityDetails data in lstdetailsfor)
                        //{

                        //    ActivityDetailss.Add(new ActivityDetails
                        //    {
                        //        activityMasterId = data.coldActivityMastersId,
                        //        contactsId = data.contactsId,
                        //        contactName =data.contactName

                        //    });

                        //}

                        List<ColdActivityTeams> lstTeamfor = await _context.ColdActivityTeams.Where(x => x.coldActivityMastersId == item.Id).Include(x => x.team).ToListAsync();
                        actlist.Add(new ColdActivityMasterCAViewModel
                        {
                            activityMasters = item,
                            activityDetails = ActivityDetailss,
                            activityTeam = lstTeamfor

                        });

                        sourceId = item.Id;

                    }
                }
                else
                {
                    sourceId = 0;
                }

            }
            while (sourceId != 0);




            return actlist;
        }
        public async Task<IEnumerable<ActivityMaster>> GetActivityMasterByLeadId(int id)
        {
            return await _context.ActivityMasters.Where(x => x.leadsId == id).Include(x => x.leads).Include(x => x.activityCategory).ToListAsync();
        }
        public async Task<IEnumerable<ActivityMasterCViewModel>> GetActivityMasterCByLeadId(int id)
        {
            List<ActivityMasterCViewModel> lstviewdata = new List<ActivityMasterCViewModel>();
            List<ActivityMaster> lstmaster = await _context.ActivityMasters.Where(x => x.leadsId == id && x.activityMasterId==null).Include(x => x.leads).Include(x => x.activityCategory).ToListAsync();
            foreach (ActivityMaster data in lstmaster)
            {

                //int sourceId = data.Id;
                //int masterid=0;
                //int count = 1;
                //do
                //{
                //    List<ActivityMaster> activityMaster = await _context.ActivityMasters.Where(x => x.activityMasterId == sourceId).ToListAsync();
                //    if (activityMaster.Count() != 0)
                //    {
                //        foreach (ActivityMaster item in activityMaster)
                //        {
                //            if (item.activityMasterId == null)
                //            {
                //                masterid = item.Id;
                //            }
                //            count++;
                //            sourceId = item.Id;

                //        }
                //    }
                //    else
                //    {
                //        sourceId = 0;
                //    }

                //}
                //while (sourceId != 0);
                List<ActivityMasteIdViewModel> activityMasteIdViewModels = await _context.activityMasteIdViewModels.FromSql($"getallchildactivity {data.Id}").ToListAsync();
                lstviewdata.Add(new ActivityMasterCViewModel
                {
                    activityMasters = data,
                    countchild = activityMasteIdViewModels.Count()
                });

            }
            return lstviewdata; //await _context.ActivityMasters.Where(x => x.leadsId == id).Include(x => x.leads).Include(x => x.activityCategory).ToListAsync();
        }
        public async Task<int> getchildcount(int Id)
        {

            int count = 1;
            List<ActivityMaster> activityMaster = new List<ActivityMaster>();
            do
            {
                activityMaster = await _context.ActivityMasters.Where(x => x.activityMasterId == Id).ToListAsync();

                foreach (ActivityMaster item in activityMaster)
                {
                    count++;
                    Id = (int)item.activityMasterId;

                }

            }
            while (Id != 0);
            //  int a = 10;
            return count;


            //  List<ActivityMaster> data = _context.ActivityMasters.Where(x => x.parentId == Id).ToList();


        }


        public async Task<bool> DeleteActivityMasterById(int id)
        {
            _context.ActivityMasters.RemoveRange(_context.ActivityMasters.Where(x => x.Id == id));
            return 1 == await _context.SaveChangesAsync();
        }


    }
}
