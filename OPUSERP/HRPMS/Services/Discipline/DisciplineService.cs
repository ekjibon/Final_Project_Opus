using OPUSERP.Data;
using OPUSERP.HRPMS.Services.Discipline.interfaces;

namespace OPUSERP.HRPMS.Services.Discipline
{
    public class DisciplineService:IDisciplineService
    {
        private readonly ERPDbContext _context;

        public DisciplineService(ERPDbContext context)
        {
            _context = context;
        }

        //#region Offense
        //public async Task<bool> SaveOffense(Holiday holiday)
        //{
        //    _context.holidays.Add(holiday);
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<Holiday>> GetAllOffense()
        //{
        //    return await _context.holidays.ToListAsync();
        //}

        //public async Task<Holiday> GetOffenseById(int id)
        //{
        //    return await _context.holidays.FindAsync(id);
        //}

        //public async Task<bool> DeleteOffenseById(int id)
        //{
        //    _context.holidays.Remove(_context.holidays.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}
        //#endregion

        //#region Punishment
        //public async Task<bool> SavePunishment(Holiday holiday)
        //{
        //    _context.holidays.Add(holiday);
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<IEnumerable<Holiday>> GetAllPunishment()
        //{
        //    return await _context.holidays.ToListAsync();
        //}

        //public async Task<Holiday> GetPunishmentById(int id)
        //{
        //    return await _context.holidays.FindAsync(id);
        //}

        //public async Task<bool> DeletePunishmentById(int id)
        //{
        //    _context.holidays.Remove(_context.holidays.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}
        //#endregion

    }
}
