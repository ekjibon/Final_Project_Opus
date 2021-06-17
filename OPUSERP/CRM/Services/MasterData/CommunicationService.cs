using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Distribution.Models;
using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.CRM.Services.MasterData.Interfaces;
using OPUSERP.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CRM.Services.MasterData
{
    public class CommunicationService : ICommunicationService
    {
        private readonly ERPDbContext _context;

        public CommunicationService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveCommunicationType(CommunicationType  communicationType)
        {
            if (communicationType.Id != 0)
                _context.CommunicationTypes.Update(communicationType);
            else
                _context.CommunicationTypes.Add(communicationType);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<CommunicationType>> GetAllCommunicationType()
        {
            return await _context.CommunicationTypes.AsNoTracking().ToListAsync();
        }

        public async Task<CommunicationType> GetCommunicationTypeById(int id)
        {
            return await _context.CommunicationTypes.FindAsync(id);
        }

        public async Task<bool> DeleteCommunicationTypeById(int id)
        {
            _context.CommunicationTypes.Remove(_context.CommunicationTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        // Area
        public async Task<bool> SaveArea(Area area)
        {
            if (area.Id != 0)
                _context.Areas.Update(area);
            else
                _context.Areas.Add(area);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Area>> GetAllArea()
        {
            return await _context.Areas.Include(x=>x.salesLevel).Include(x=>x.area).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<Area>> GetAllAreabysaleslevelid(int Id)
        {
            return await _context.Areas.Where(x=>x.salesLevelId==Id).Include(x => x.salesLevel).Include(x => x.area).AsNoTracking().ToListAsync();
        }

        public async Task<Area> GetAreaById(int id)
        {
            return await _context.Areas.FindAsync(id);
        }

        public async Task<IEnumerable<AreaTeamViewModel>> GetMenusByParrentspId(int parrentId)
        {
            return await _context.areaTeamViewModels.FromSql($"Distribution.saleteamdeployedinfo {parrentId}").AsNoTracking().ToListAsync();

        }
        public async Task<IEnumerable<Area>> GetMenusByParrentId(int parrentId)
        {
            List<Area> data = new List<Area>();
            if (parrentId == -1)
            {

                parrentId = 0;
            }
            if (parrentId == 0)
            {
                data = await _context.Areas.Where(x => x.areaId == null).ToListAsync();
            }
            else
            {
                data= await _context.Areas.Where(x => x.areaId == parrentId ).ToListAsync();
            }
            return data;
        }

        public async Task<IEnumerable<Area>> GetMenusByParrentdistId(int parrentId,int relId)
        {
            List<int?> lstsalesdeployid = new List<int?>();
            lstsalesdeployid = _context.RelCustomerAreas.Where(x => x.relSupplierCustomerResourseId == relId).Select(x => x.areaId).ToList();
            //lstsalesdeployid.AddRange(_context.relCustomerSalesTeamDeployments.Where(x => x.relSupplierCustomerResourseId == relId).Select(x => x.rsmsalesTeamDeploymentId).ToList());
            //lstsalesdeployid.AddRange(_context.relCustomerSalesTeamDeployments.Where(x => x.relSupplierCustomerResourseId == relId).Select(x => x.tsmsalesTeamDeploymentId).ToList());
            List<Area> data = new List<Area>();
            if (parrentId == -1)
            {

                parrentId = 0;
            }
            if (parrentId == 0)
            {
                data = await _context.Areas.Where(x => x.areaId == null && lstsalesdeployid.Contains(x.Id)).ToListAsync();
            }
            else
            {
                data = await _context.Areas.Where(x => x.areaId == parrentId&& lstsalesdeployid.Contains(x.Id)).ToListAsync();
            }
            return data;
        }
        public async Task<bool> DeleteAreaById(int id)
        {
            _context.Areas.Remove(_context.Areas.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
