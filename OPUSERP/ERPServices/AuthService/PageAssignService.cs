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
    public class PageAssignService:IPageAssignService
    {
        private readonly ERPDbContext _context;

        public PageAssignService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveUserAccessPage(UserAccessPage userAccessPage)
        {
            try
            {
                if (userAccessPage.Id != 0)
                {
                    userAccessPage.updatedBy = userAccessPage.createdBy;
                    userAccessPage.updatedAt = DateTime.Now;
                    _context.UserAccessPages.Update(userAccessPage);
                }
                else
                {
                    userAccessPage.createdAt = DateTime.Now;
                    _context.UserAccessPages.Add(userAccessPage);
                }

                await _context.SaveChangesAsync();
                return userAccessPage.Id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<IEnumerable<NavbarViewModel>> GetNavbarDataByUser(string userName, string lang)
        {
            return await _context.navbarViewModels.FromSql($"SP_GetuserMenu {userName},{lang}").ToListAsync();
        }

        public async Task<IEnumerable<Navbar>> GetNavbars(string userName)
        {
            return await _context.Navbars.Include(x=>x.module).Where(x=>x.status==true).OrderBy(x=>x.displayOrder).ToListAsync();
        }

        public async Task<IEnumerable<ERPModule>> GetERPModules()
        {
            return await _context.ERPModules.OrderBy(x=>x.shortOrder).ToListAsync();
        }
       
        public async Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserType(string userTypeId)
        {
            try
            {
                var result= await _context.userAccessPageViewModels.FromSql($"Sp_GetUserMenuAccess {userTypeId}").ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeByModule(string userTypeId,int ModuleId)
        {
            try
            {
                var result= await _context.userAccessPageViewModels.FromSql($"Sp_GetUserMenuAccessByModule {userTypeId},{ModuleId}").ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeByMenu(string userTypeId,int ModuleId)
        {
            try
            {
                var result= await _context.userAccessPageViewModels.FromSql($"Sp_GetUserMenuAccessByMenu {userTypeId},{ModuleId}").ToListAsync();
                return result;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        public async Task<IEnumerable<UserAccessPageViewModel>> GetUserMenuAccessByUserTypeModule(string userTypeId, string userTypeIdM)
        {
            return await _context.userAccessPageViewModels.FromSql($"Sp_GetUserMenuAccessModule {userTypeId},{userTypeIdM}").ToListAsync();
        }


        public async Task<bool> DeleteUserAccesspageByUserTypeId(string userTypeid)
        {
            _context.UserAccessPages.RemoveRange(_context.UserAccessPages.Where(x => x.applicationRoleId == userTypeid));
            return 1 == await _context.SaveChangesAsync();
        }

    }
}
