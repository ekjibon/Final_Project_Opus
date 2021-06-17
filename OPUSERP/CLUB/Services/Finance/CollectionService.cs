using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Finance;
using OPUSERP.CLUB.Services.Finance.Interface;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Finance
{
    public class CollectionService: ICollectionService
    {
        private readonly ERPDbContext _context;

        public CollectionService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveCollection(Collection collection)
        {
            if (collection.Id != 0)
                _context.collections.Update(collection);
            else
                _context.collections.Add(collection);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Collection>> GetCollection()
        {
            return await _context.collections.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Collection>> GetCollectionByTrMasterId(int id)
        {
            return await _context.collections.Where(x=>x.trMasterId==id).Include(x=>x.paymentHead).AsNoTracking().ToListAsync();
        }

        public async Task<Collection> GetCollectionById(int id)
        {
            return await _context.collections.FindAsync(id);
        }

        public async Task<bool> DeleteCollectionById(int id)
        {
            _context.collections.Remove(_context.collections.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
