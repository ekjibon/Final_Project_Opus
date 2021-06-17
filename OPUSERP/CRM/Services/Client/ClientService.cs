using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.CRMClient.Models;
using OPUSERP.CRM.Data.Entity.Client;
using OPUSERP.CRM.Services.Client.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.Client
{
    public class ClientService : IClientService
    {
        private readonly ERPDbContext _context;

        public ClientService(ERPDbContext context)
        {
            _context = context;
        }

        #region Clients
        public async Task<int> SaveClients(Clients Clients)
        {
            if (Clients.Id != 0)
                _context.Clients.Update(Clients);
            else
                _context.Clients.Add(Clients);
            
            await _context.SaveChangesAsync();
            return Clients.Id;
        }
        public async Task<bool> UpdateClient(int Id)
        {
            var Agreement = _context.Clients.Find(Id);
            Agreement.isconverted = 0;
            Agreement.isactive = 0;
          
            _context.Entry(Agreement).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateReClient(int Id)
        {
            var Agreement = _context.Clients.Find(Id);
            Agreement.isconverted = 1;
            Agreement.isactive = 1;

            _context.Entry(Agreement).State = EntityState.Modified;
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Clients>> GetAllClients()
        {
            return await _context.Clients.Include(x=>x.leads.ownerShipType).Include(x=>x.leads.sector).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetClientInfoListViewModel>> GetClientsByOwner(string ownername)
        {
            return await _context.getClientInfoListViewModels.FromSql($"SP_GetClientInfoList {ownername}").AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<GetClientInfoListViewModel>> GetClientsByOwnerfilter(string ownername,string Teamleader,string FaName,string BD,string LeadId)
        {
            return await _context.getClientInfoListViewModels.FromSql($"SP_GetClientInfoListfilter {ownername},{Teamleader},{FaName},{BD},{LeadId}").AsNoTracking().ToListAsync();
        }


        public async Task<Clients> GetClientsById(int id)
        {
            return await _context.Clients.FindAsync(id);
        }
        public async Task<Clients> GetClientsByLeadId(int id)
        {
            return await _context.Clients.Where(x=>x.leadsId==id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeletClientsById(int id)
        {
            _context.Clients.Remove(_context.Clients.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletClientsByleadId(int id)
        {
            var data = _context.Agreements.Find(id);
            _context.Clients.Remove(_context.Clients.Where(x=>x.leadsId== data.leadsId).FirstOrDefault());
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletClientsleadsByleadId(int id)
        {
           // var client = _context.Clients.Where(x => x.leadsId == id).FirstOrDefault();
            _context.Clients.Remove(_context.Clients.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

    }
}
