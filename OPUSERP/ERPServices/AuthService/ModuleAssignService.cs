using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Auth;
using OPUSERP.ERPServices.Interfaces;
using OPUSERP.ERPServices.MasterData.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.ERPServices.AuthService
{
    public class ModuleAssignService:IModuleAssignService
    {
        private readonly ERPDbContext _context;

        public ModuleAssignService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveModuleAccessPage(ModuleAccessPage userAccessPage)
        {
            try
            {
                if (userAccessPage.Id != 0)
                {
                    userAccessPage.updatedBy = userAccessPage.createdBy;
                    userAccessPage.updatedAt = DateTime.Now;
                    _context.ModuleAccessPages.Update(userAccessPage);
                }
                else
                {
                    userAccessPage.createdAt = DateTime.Now;
                    _context.ModuleAccessPages.Add(userAccessPage);
                }

                await _context.SaveChangesAsync();
                return userAccessPage.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
        public async Task<IEnumerable<ModuleAccessPage>> GetModuleAccessPages()
        {
            return await _context.ModuleAccessPages.Include(x=>x.eRPModule).ToListAsync();
        }

        public async Task<IEnumerable<ModuleAccessPageViewModel>> GetModuleAccessPagesactive(string UserTypeId)
        {
            List<ERPModule> eRPModules= _context.ERPModules.ToList();
            List<ModuleAccessPage> lstModuleAccess = _context.ModuleAccessPages.Where(x => x.applicationRoleId == UserTypeId).ToList();
            List<ModuleAccessPageViewModel> data = new List<ModuleAccessPageViewModel>();
            foreach (ERPModule d in eRPModules)
            {
                var Mdata = lstModuleAccess.Where(x => x.eRPModuleId == d.Id).ToList();
                int active = 0;
                if (Mdata.Count() > 0)
                {
                    active = 1;
                }
                data.Add(new ModuleAccessPageViewModel {
                   eRPModuleId=d.Id,
                   eRPModuleName=d.moduleName,
                   active= active

                });
            }
            return data;
        }

        public async Task<IEnumerable<ERPModule>> GetERPModules()
        {
            return await _context.ERPModules.ToListAsync();
        }
        public async Task<IEnumerable<ERPModule>> GetERPModulesForTeam()
        {
            return await _context.ERPModules.Where(x=>x.isTeam== "Yes").AsNoTracking().ToListAsync();
        }

        public async Task<bool> DeleteModuleAccesspageByUserTypeId(string userTypeid)
        {
            _context.ModuleAccessPages.RemoveRange(_context.ModuleAccessPages.Where(x => x.applicationRoleId == userTypeid));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
