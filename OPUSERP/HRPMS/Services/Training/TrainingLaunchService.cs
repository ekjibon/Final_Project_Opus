using OPUSERP.Data;
using OPUSERP.HRPMS.Services.Training.Interfaces;

namespace OPUSERP.HRPMS.Services.Training
{
    public class TrainingLaunchService : ITrainingLaunchService
    {
        //private readonly ERPDbContext _context;

        //public TrainingLaunchService(ERPDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task<bool> SaveTrainingLaunch(TrainingLaunch trainingLaunch)
        //{
        //    if (trainingLaunch.Id != 0)
        //        _context.trainingLaunches.Update(trainingLaunch);
        //    else
        //        _context.trainingLaunches.Add(trainingLaunch);
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<TrainingLaunch>> GetAllTrainingLaunch()
        //{
        //    return await _context.trainingLaunches.AsNoTracking().ToListAsync();
        //}

        //public async Task<TrainingLaunch> GetTrainingLaunchById(int id)
        //{
        //    return await _context.trainingLaunches.FindAsync(id);
        //}

        //public async Task<bool> DeleteTrainingLaunchById(int id)
        //{
        //    _context.trainingLaunches.Remove(_context.trainingLaunches.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}

        

      
    }
}