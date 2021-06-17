using OPUSERP.Data;
using OPUSERP.HRPMS.Services.Training.Interfaces;

namespace OPUSERP.HRPMS.Services.Training
{
    public class TrainingScheduleService : ITrainingScheduleService
    {
        //private readonly ERPDbContext _context;

        //public TrainingScheduleService(ERPDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task<bool> SaveTrainingSchedule(TrainingSchedule trainingSchedule)
        //{
        //    if (trainingSchedule.Id != 0)
        //        _context.trainingSchedules.Update(trainingSchedule);
        //    else
        //        _context.trainingSchedules.Add(trainingSchedule);
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<TrainingSchedule>> GetAllTrainingSchedule()
        //{
        //    return await _context.trainingSchedules.ToListAsync();
        //}

        //public async Task<TrainingSchedule> GetTrainingScheduleById(int id)
        //{
        //    return await _context.trainingSchedules.FindAsync(id);
        //}

        //public async Task<bool> DeleteTrainingScheduleById(int id)
        //{
        //    _context.trainingSchedules.Remove(_context.trainingSchedules.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}

       
    }
}