using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using OPUSERP.HRPMS.Services.TrainingNew.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.TrainingNew
{
    public class TrainingNewService: ITrainingNewService
    {
        private readonly ERPDbContext _context;

        public TrainingNewService(ERPDbContext context)
        {
            _context = context;
        }

        //ApplicationForm
        public async Task<bool> DeleteTrainingInfoNewById(int id)
        {
            _context.trainingInfoNews.Remove(_context.trainingInfoNews.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingInfoNew>> GetTrainingInfoNew()
        {
            return await _context.trainingInfoNews.Include(x => x.employeeType).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TrainingInfoNew>> GetTrainingInfoNewByType(int type,string org)
        {
            return await _context.trainingInfoNews.Include(x => x.employeeType).Where(x => x.trainingType == type).Where(x => x.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TrainingInfoNew>> GetTrainingInfoNewByStatus(int statu)
        {
            return await _context.trainingInfoNews.Include(x => x.employeeType).Where(x => x.status == statu).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TrainingInfoNew>> GetTrainingInfoNewByStatusandType(int statu, int type,string org)
        {
            return await _context.trainingInfoNews.Include(x => x.employeeType).Where(x => x.status == statu).Where(x => x.trainingType == type).Where(x => x.orgType == org).AsNoTracking().ToListAsync();
        }

        public async Task<TrainingInfoNew> GetTrainingInfoNewById(int id)
        {
            return await _context.trainingInfoNews.Include(x => x.employeeType).Include(x => x.country).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> UpdateTrainingInfoNewById(TrainingInfoNew trainingInfoNew)
        {
            TrainingInfoNew trainingInfoNew1 = _context.trainingInfoNews.Find(trainingInfoNew.Id);
            trainingInfoNew1.startDateActual = trainingInfoNew.startDateActual;
            trainingInfoNew1.endDateActual = trainingInfoNew.endDateActual;
            trainingInfoNew1.noOfParticipantsActual = trainingInfoNew.noOfParticipantsActual;
            trainingInfoNew1.status = trainingInfoNew.status;
            trainingInfoNew1.amountActual = trainingInfoNew.amountActual;
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<int> SaveTrainingInfoNew(TrainingInfoNew trainingInfoNew)
        {
            if (trainingInfoNew.Id != 0)
                _context.trainingInfoNews.Update(trainingInfoNew);
            else
                _context.trainingInfoNews.Add(trainingInfoNew);
            await _context.SaveChangesAsync();
            return trainingInfoNew.Id;
        }

        //For DashBoard
        public async Task<IEnumerable<TrainingInfoNew>> AllTrainingOfLastYearByOrg(string org)
        {
            DateTime to = DateTime.Now;
            DateTime frm = to.AddYears(-1);
            return await _context.trainingInfoNews.Where(x => x.orgType == org && x.startDate >= frm && x.startDate <= to).AsNoTracking().ToListAsync();
        }
    }
}
