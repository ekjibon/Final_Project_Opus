using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.HRPMSReport.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using OPUSERP.HRPMS.Services.TrainingNew.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.TrainingNew
{
    public class EnrollTraineeService: IEnrollTraineeService
    {
        private readonly ERPDbContext _context;

        public EnrollTraineeService(ERPDbContext context)
        {
            _context = context;
        }

        //ApplicationForm
        public async Task<bool> DeleteEnrolledTraineeById(int id)
        {
            _context.enrolledTrainees.Remove(_context.enrolledTrainees.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EnrolledTrainee>> GetEnrolledTrainee()
        {
            return await _context.enrolledTrainees.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<EnrolledTrainee>> GetEnrolledTraineeByPlannId(int id)
        {
            return await _context.enrolledTrainees.Include(x=>x.employee).Include(x => x.trainingInfoNew).Where(x=>x.trainingInfoNewId == id).AsNoTracking().ToListAsync();
        }

        public async Task<EnrolledTrainee> GetEnrolledTraineeById(int id)
        {
            return await _context.enrolledTrainees.Include(x => x.employee).Include(x => x.trainingInfoNew).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<int> SaveEnrolledTrainee(EnrolledTrainee enrollTraineeService)
        {
            if (enrollTraineeService.Id != 0)
                _context.enrolledTrainees.Update(enrollTraineeService);
            else
                _context.enrolledTrainees.Add(enrollTraineeService);
            await _context.SaveChangesAsync();
            return enrollTraineeService.Id;
        }

        public async Task<bool> UpdatePositionEnrolledTrainee(EnrolledTrainee enrolledTrainee)
        {
            EnrolledTrainee enrolledTraineeInfo = await _context.enrolledTrainees.FindAsync(enrolledTrainee.Id);
            enrolledTraineeInfo.positon = enrolledTrainee.positon;
            enrolledTraineeInfo.completionStatus = enrolledTrainee.completionStatus;
            enrolledTraineeInfo.remarks = enrolledTrainee.remarks;

            return 1 == await _context.SaveChangesAsync();            
        }

        public async Task<IEnumerable<EmployeeTraining>> GetTrainingListByEmployeeId(int employeeId)
        {
            IQueryable<EnrolledTrainee> queryData = _context.enrolledTrainees.Include(x=>x.trainingInfoNew).Where(x => x.employeeId == employeeId);

            //List<int?> ids = await _context.enrolledTrainees.Where(x => x.employeeId == employeeId).Select(x => x.trainingInfoNewId).ToListAsync();

            //queryData = queryData.Where(x => ids.Contains(x.trainingInfoNewId));

            return await queryData.Select(x => new EmployeeTraining
            {
                course = x.trainingInfoNew.course,
                year = x.trainingInfoNew.year,
                startDateActual = x.trainingInfoNew.startDateActual.HasValue?x.trainingInfoNew.startDateActual.Value.ToString("MM/dd/yyyy") : String.Empty,
                endDateActual = x.trainingInfoNew.endDateActual.HasValue ?x.trainingInfoNew.endDateActual.Value.ToString("MM/dd/yyyy") : String.Empty,
                budget = x.trainingInfoNew.budget,
                location = x.trainingInfoNew.location,
                status = x.completionStatus,
                action = "<a data-toggle='tooltip' title='Report' target='_blank' class='btn btn-success' href='../../TrainingNew/TrainingImplementation/DetailsView/" + x.trainingInfoNew.Id + "'><i class='fa fa-eye'></i></a>"
            }).ToListAsync();
        }

        //xxxxxxxxxxxxxxxxxxxxx
    }
}
