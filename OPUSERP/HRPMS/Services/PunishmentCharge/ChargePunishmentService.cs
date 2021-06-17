using OPUSERP.Areas.HRPMSPunishment.Models;
using OPUSERP.Data;
using OPUSERP.HRPMS.Data.Entity.PunishmentCharge;
using OPUSERP.HRPMS.Services.PunishmentCharge.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.HRPMS.Services.PunishmentCharge
{
    public class ChargePunishmentService : ICPService
    {
        private readonly ERPDbContext _context;

        public ChargePunishmentService(ERPDbContext context)
        {
            _context = context;
        }

        //Change Service

        public async Task<int> SaveCharge(Charge charge)
        {
            if (charge.Id != 0)
                _context.charges.Update(charge);
            else
                _context.charges.Add(charge);

            await _context.SaveChangesAsync();
            return charge.Id;
        }
        public async Task<IEnumerable<Charge>> GetChargeByEmpId(int empId)
        {
            return await _context.charges.Where(x => x.employeeId == empId).OrderBy(x => x.chargeDate).AsNoTracking().ToListAsync();
        }

        //public async Task<IEnumerable<ChargeViewModel>> GetChargeByEmpId(int empId)
        //{
        //    return await _context.GetCharges.FromSql($"SP_GETChargeList @p0",empId).ToListAsync();
        //}

        public async Task<Charge> GetChargeById(int id)
        {
            return await _context.charges.Where(x => x.Id == id).FirstAsync();
        }
        public async Task<bool> DeleteChargeById(int id)
        {
            _context.charges.Remove(_context.charges.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeletePunishmentById(int id)
        {
            _context.punishments.Remove(_context.punishments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        //Punishment Service

        public async Task<IEnumerable<Punishment>> GetPunishmentByEmpId(int empId)
        {
            return await _context.punishments.Where(x=>x.employeeId==empId).OrderBy(x=>x.punishmentDate).AsNoTracking().ToListAsync();
        }

        public async Task<Punishment> GetPunishmentById(int id)
        {
            return await _context.punishments.Where(x => x.Id == id).FirstAsync();
        }

        public async Task<int> SavePunishment(Punishment punishment)
        {
            if (punishment.Id != 0)
                _context.punishments.Update(punishment);
            else
                _context.punishments.Add(punishment);

            await _context.SaveChangesAsync();
            return punishment.Id;
        }
    }
}
