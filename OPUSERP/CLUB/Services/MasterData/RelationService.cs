using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLUB.Data;
using CLUB.Data.Entity.Master;
using CLUB.Services.MasterData.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CLUB.Services.MasterData
{
    public class RelationService : IRelationService
    {
        private readonly ApplicationDbContext _context;

        public RelationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Relation>> GetEmployeeRelations()
        {
            return await _context.relations.AsNoTracking().ToListAsync();
        }

        public async Task<Relation> GetEmployeeRelationById(int id)
        {
            return await _context.relations.FindAsync(id);
        }

        public async Task<bool> SaveEmployeeRelation(Relation relation)
        {
            if(relation.Id != 0)
                _context.relations.Update(relation);
            else 
                _context.relations.Add(relation);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteEmployeeRelationById(int id)
        {
            _context.relations.Remove(_context.relations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
