using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.TrainingNew;
using OPUSERP.HRPMS.Services.TrainingNew.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.TrainingNew
{
    public class TrainingResourcePersonService: ITrainingResourcePersonService
    {
        private readonly ERPDbContext _context;

        public TrainingResourcePersonService(ERPDbContext context)
        {
            _context = context;
        }

        //TrainingResourcePerson
        public async Task<bool> DeleteTrainingResourcePersonById(int id)
        {
            _context.trainingResourcePersons.Remove(_context.trainingResourcePersons.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingResourcePerson>> GetTrainingResourcePerson()
        {
            return await _context.trainingResourcePersons.AsNoTracking().ToListAsync();
        }

        public async Task<TrainingResourcePerson> GetTrainingResourcePersonById(int id)
        {
            return await _context.trainingResourcePersons.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<TrainingResourcePerson>> GetTrainingResourcePersonByTrainingId(int id)
        {
            return await _context.trainingResourcePersons.Where(x => x.trainingInfoNewId == id).Include(x=>x.resourcePerson).AsNoTracking().ToListAsync();
        }

        public async Task<int> SaveTrainingResourcePerson(TrainingResourcePerson trainingResourcePerson)
        {
            if (trainingResourcePerson.Id != 0)
                _context.trainingResourcePersons.Update(trainingResourcePerson);
            else
                _context.trainingResourcePersons.Add(trainingResourcePerson);
            await _context.SaveChangesAsync();
            return trainingResourcePerson.Id;
        }
    }
}
