using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Program.Models;
using OPUSERP.Data;
using OPUSERP.Models;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service
{

    public class ProgramReportService : IProgramReportService
    {
        private readonly ERPDbContext _context;

        public ProgramReportService(ERPDbContext context)
        {
            _context = context;
        }
        public async Task<ProgramReportListModel> programReportListModel(int mainCat, int subcat, DateTime FromDate, DateTime ToDate)
        {
            string MainCatName = await _context.programMainCategories.Where(x => x.Id == mainCat).Select(x => x.name).FirstOrDefaultAsync();
            string SubCatName = _context.programCategories.Where(x => x.Id == subcat).Select(x => x.name).FirstOrDefault();
            string fromDate = FromDate.ToString("dd/MM/yy");
            string toDate = ToDate.ToString("dd/MM/yy");
            IEnumerable<ProgramMaster> programMasters = _context.programMasters.Include(x => x.programCategory.programMainCategory).Include(x => x.thana.district.division).Where(x => x.programCategoryId == subcat && x.date >= FromDate && x.date <= ToDate).ToList();
            List<int> programMastersids = programMasters.Select(x => x.Id).ToList();
            IEnumerable<ProgramAddress> programAddresses = _context.programAddresses.Where(x => programMastersids.Contains((int)x.programMasterId)).ToList();
            IEnumerable<ProgramPeopleInDiscussion> programPeopleInDiscussions = _context.programPeopleInDiscussions.Where(x => programMastersids.Contains((int)x.programMasterId)).ToList();
            IEnumerable<ProgramViewer> programViewers = _context.programViewers.Where(x => programMastersids.Contains((int)x.programMasterId)).ToList();
            IEnumerable<ProgramAttachment> programAttachments = _context.programAttachments.Where(x => programMastersids.Contains((int)x.programMasterId)).ToList();
            List<ProgramReportListSubModel> datasub = new List<ProgramReportListSubModel>();
            foreach (ProgramMaster data in programMasters)
            {
                datasub.Add(new ProgramReportListSubModel
                {
                    date = data.date,
                    place = programAddresses.Where(x => x.programMasterId == data.Id).Select(x => x.address).FirstOrDefault(),
                    union = data.place,
                    thana = data.thana.thanaName,
                    district = data.district.districtName,
                    number = "১(এক)টি",
                    url = programAttachments.Where(x => x.programMasterId == data.Id).Select(x => x.fileUrl).FirstOrDefault(),
                    present = (programViewers.Where(x => x.programMasterId == data.Id).Sum(x => x.quantity) + programPeopleInDiscussions.Where(x => x.programMasterId == data.Id).Sum(x => x.quantity)).ToString() + " জন"

                });
            }
            List<ProgramReportListModel> dataF = new List<ProgramReportListModel>();
            dataF.Add(new ProgramReportListModel
            {
                MainCategory = MainCatName,
                SubCategory = SubCatName,
                FromDate = fromDate,
                ToDate = toDate,
                programReportListSubModels = datasub

            });
            return dataF.FirstOrDefault();

        }
        public async Task<ProgramReportListModel> programReportListModelAttcahment(int mainCat, int subcat, DateTime FromDate, DateTime ToDate)
        {
            string MainCatName = await _context.programMainCategories.Where(x => x.Id == mainCat).Select(x => x.name).FirstOrDefaultAsync();
            string SubCatName = _context.programCategories.Where(x => x.Id == subcat).Select(x => x.name).FirstOrDefault();
            string fromDate = FromDate.ToString("dd/MM/yy");
            string toDate = ToDate.ToString("dd/MM/yy");
            IEnumerable<ProgramMaster> programMasters = _context.programMasters.Include(x => x.programCategory.programMainCategory).Include(x => x.thana.district.division).Where(x => x.programCategoryId == subcat && x.date >= FromDate && x.date <= ToDate).ToList();
            List<int> programMastersids = programMasters.Select(x => x.Id).ToList();
            IEnumerable<ProgramAddress> programAddresses = _context.programAddresses.Where(x => programMastersids.Contains((int)x.programMasterId)).ToList();
            IEnumerable<ProgramPeopleInDiscussion> programPeopleInDiscussions = _context.programPeopleInDiscussions.Where(x => programMastersids.Contains((int)x.programMasterId)).ToList();
            IEnumerable<ProgramViewer> programViewers = _context.programViewers.Where(x => programMastersids.Contains((int)x.programMasterId)).ToList();
            List<ProgramReportListSubModel> datasub = new List<ProgramReportListSubModel>();
            foreach (ProgramMaster data in programMasters)
            {
                datasub.Add(new ProgramReportListSubModel
                {
                    date = data.date,
                    place = programAddresses.Where(x => x.programMasterId == data.Id).Select(x => x.address).FirstOrDefault(),
                    union = data.place,
                    thana = data.thana.thanaName,
                    number = "১(এক)টি",
                    present = (programViewers.Where(x => x.programMasterId == data.Id).Sum(x => x.quantity) + programPeopleInDiscussions.Where(x => x.programMasterId == data.Id).Sum(x => x.quantity)).ToString() + " জন"

                });
            }
            List<ProgramReportListModel> dataF = new List<ProgramReportListModel>();
            dataF.Add(new ProgramReportListModel
            {
                MainCategory = MainCatName,
                SubCategory = SubCatName,
                FromDate = fromDate,
                ToDate = toDate,
                programReportListSubModels = datasub

            });
            return dataF.FirstOrDefault();

        }

        public async Task<ProgramDashboardViewModel> GetProgramDashboardViewModel()
        {
            var master = await _context.programMasters.ToListAsync();
            decimal? perticipents = 0;
            decimal? max = 0;
            List<MasterPerticipants> masterList = new List<MasterPerticipants>();
            foreach (var data in master)
            {
                perticipents = (_context.programViewers.Where(x => x.programMasterId == data.Id).Sum(x => x.quantity) + _context.programPeopleInDiscussions.Where(x => x.programMasterId == data.Id).Sum(x => x.quantity));
                masterList.Add(new MasterPerticipants {
                    name = data.number + "(" + data.place + ")",
                    count = Convert.ToInt32(perticipents)
                });
                if (max < perticipents)
                {
                    max = perticipents;
                }
            }
            ProgramDashboardViewModel model = new ProgramDashboardViewModel
            {
                plan = _context.programPlans.Count(),
                program = await _context.programMasters.CountAsync(),
                videos = await _context.programVideos.CountAsync(),
                perticipents = max,
                masterPerticipants = masterList.OrderByDescending(x=>x.count).Take(5)
            };
            return model;
        }
    }
}
