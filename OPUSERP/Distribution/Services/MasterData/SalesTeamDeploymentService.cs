using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.Distribution.Services.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Distribution.Services.MasterData
{
    public class SalesTeamDeploymentService : ISalesTeamDeploymentService
    {
        private readonly ERPDbContext _context;

        public SalesTeamDeploymentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteSalesTeamDeploymentById(int id)
        {
            _context.salesTeamDeployments.Remove(_context.salesTeamDeployments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalesTeamDeployment>> GetAllSalesTeamDeployment()
        {
            return await _context.salesTeamDeployments.Include(x => x.salesLevel).Include(x => x.area).Include(x => x.employeeInfo).AsNoTracking().ToListAsync();
        }

        public async Task<SalesTeamDeployment> GetSalesTeamDeploymentById(int id)
        {
            return await _context.salesTeamDeployments.FindAsync(id);
        }
        public async Task<IEnumerable<SalesTeamDeployment>> GetSalesTeamDeploymentByareaId(int id)
        {
            var data = new List<SalesTeamDeployment>();
            if (id == 0)
            {
                data = await _context.salesTeamDeployments.Where(x => x.areaId == null).Include(x => x.salesLevel).Include(x => x.area).Include(x => x.employeeInfo).AsNoTracking().ToListAsync();
            }
            else
            {
                data = await _context.salesTeamDeployments.Where(x => x.areaId == id).Include(x => x.salesLevel).Include(x => x.area).Include(x => x.employeeInfo).AsNoTracking().ToListAsync();
            }
            return data;

        }
        public async Task<IEnumerable<SalesTeamDeployment>> GetSalesTeamDeploymentByparrentId(int id)
        {
            var data = new List<SalesTeamDeployment>();
            if (id == 0)
            {
                data = await _context.salesTeamDeployments.Include(x=>x.salesTeamDeployment).Include(x => x.salesLevel).Include(x => x.area).Include(x => x.employeeInfo).Where(x => x.salesTeamDeploymentId == null).AsNoTracking().ToListAsync();
            }
            else
            {
                data = await _context.salesTeamDeployments.Include(x => x.salesTeamDeployment).Include(x => x.salesLevel).Include(x => x.area).Include(x => x.employeeInfo).Where(x => x.salesTeamDeploymentId == id).AsNoTracking().ToListAsync();
            }
            return data;

        }
        public async Task<IEnumerable<SalesTeamDeployment>> GetSalesTeamDeploymentByparrentrelId(int id,int relId)
        {
            List<int?> lstsalesdeployid = new List<int?>();
            lstsalesdeployid = _context.relCustomerSalesTeamDeployments.Where(x => x.relSupplierCustomerResourseId == relId).Select(x => x.employeeInfoId).ToList();
            //lstsalesdeployid.AddRange(_context.relCustomerSalesTeamDeployments.Where(x => x.relSupplierCustomerResourseId == relId).Select(x => x.rsmsalesTeamDeploymentId).ToList());
            //lstsalesdeployid.AddRange(_context.relCustomerSalesTeamDeployments.Where(x => x.relSupplierCustomerResourseId == relId).Select(x => x.tsmsalesTeamDeploymentId).ToList());
            //List<int> lstdepid = _context.salesTeamDeployments.Where(x => lstsalesdeployid.Contains(x.salesLevelId)).Select(x => x.Id).ToList();
            var data = new List<SalesTeamDeployment>();
            if (id == 0)
            {
                data = await _context.salesTeamDeployments.Include(x => x.salesTeamDeployment).Include(x => x.salesLevel).Include(x => x.area).Include(x => x.employeeInfo).Where(x => x.salesTeamDeploymentId == null&&lstsalesdeployid.Contains(x.employeeInfoId)).AsNoTracking().ToListAsync();
            }
            else
            {
                data = await _context.salesTeamDeployments.Include(x => x.salesTeamDeployment).Include(x => x.salesLevel).Include(x => x.area).Include(x => x.employeeInfo).Where(x => x.salesTeamDeploymentId == id && lstsalesdeployid.Contains(x.employeeInfoId)).AsNoTracking().ToListAsync();
            }
            return data;

        }
        public async Task<IEnumerable<SalesTeamDeployment>> GetSalesTeamDeploymentByempId(int id)
        {
            return await _context.salesTeamDeployments.Where(x => x.employeeInfoId == id).Include(x => x.salesLevel).Include(x => x.area).Include(x => x.employeeInfo).AsNoTracking().ToListAsync();
        }

        public async Task<bool> SaveSalesTeamDeployment(SalesTeamDeployment salesTeamDeployment)
        {
            if (salesTeamDeployment.Id != 0)
                _context.salesTeamDeployments.Update(salesTeamDeployment);
            else
                _context.salesTeamDeployments.Add(salesTeamDeployment);

            return 1 == await _context.SaveChangesAsync();
        }


    }
}
