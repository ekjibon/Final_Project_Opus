using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Recruitment.Data.Entity;
using OPUSERP.Recruitment.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Recruitment.Services
{
    public class RcrtTrainingService : IRcrtTrainingService
    {
        private readonly ERPDbContext _context;

        public RcrtTrainingService(ERPDbContext context)
        {
            _context = context;
        }

        //public async Task<bool> DeleteTraningHistoryById(int id)
        //{
        //    _context.traningLogs.Remove(_context.traningLogs.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<TraningLog>> GetTraningHistory()
        //{
        //    return await _context.traningLogs.AsNoTracking().ToListAsync();
        //}

        //public async Task<IEnumerable<TraningLog>> GetTraningHistoryByEmpId(int empId)
        //{
        //    return await _context.traningLogs.Where(x => x.employeeId == empId).Include(x => x.employee).Include(x => x.country).Include(x => x.trainingCategory).Include(x => x.trainingInstitute).AsNoTracking().ToListAsync();
        //}

        //public async Task<TraningLog> GetTraningHistoryById(int id)
        //{
        //    return await _context.traningLogs.FindAsync(id);
        //}

        public async Task<bool> SaveRcrtTraning(RCRTTrainingLog rCRTTrainingLog)
        {
            if (rCRTTrainingLog.Id != 0)
                _context.RCRTTrainingLogs.Update(rCRTTrainingLog);
            else
                _context.RCRTTrainingLogs.Add(rCRTTrainingLog);

            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RCRTTrainingLog>> GetTraningLogListById(int? candidateId)
        {
            return await _context.RCRTTrainingLogs.AsNoTracking().Where(x => x.candidateId == candidateId).ToListAsync();
        }
    }
}
