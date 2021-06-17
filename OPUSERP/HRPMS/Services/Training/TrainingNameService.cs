using OPUSERP.Data;
using OPUSERP.HRPMS.Services.Training.Interfaces;

namespace OPUSERP.HRPMS.Services.Training
{
    public class TrainingNameService : ITrainingNameService
    {
        //private readonly ERPDbContext _context;

        //public TrainingNameService(ERPDbContext context)
        //{
        //    _context = context;
        //}

        //public async Task<bool> SaveTrainingName(TrainingName trainingName)
        //{
        //    if (trainingName.Id != 0)
        //        _context.trainingNames.Update(trainingName);
        //    else
        //        _context.trainingNames.Add(trainingName);
        //    //_context.employeePunchCardInfos.Add(employeePunchCardInfo);
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<TrainingName>> GetAllTrainingName()
        //{
        //    return await _context.trainingNames.AsNoTracking().ToListAsync();
        //}

        //public async Task<TrainingName> GetTrainingNameById(int id)
        //{
        //    return await _context.trainingNames.FindAsync(id);
        //}

        //public async Task<bool> DeleteTrainingNameById(int id)
        //{
        //    //_context.myquery.FromSql("");
        //    _context.trainingNames.Remove(_context.trainingNames.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}

        
    }
}