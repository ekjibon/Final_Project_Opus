using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Areas.Rental.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Data;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.OtherSales.Services.Sales.Interfaces;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales
{
    public class CustomerService : ICustomerService
    {
        private readonly ERPDbContext _context;

        public CustomerService(ERPDbContext context)
        {
            _context = context;
        }

        #region Role Type
        public async Task<bool> SaveRoleType(RoleType roleType)
        {
            if (roleType.Id != 0)
            {
                _context.RoleTypes.Update(roleType);
            }
            else
            {
                _context.RoleTypes.Add(roleType);
            }
            return roleType.Id == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RoleType>> GetAllRoleType()
        {
            return await _context.RoleTypes.AsNoTracking().ToListAsync();
        }

        public async Task<RoleType> GetRoleTypeIdByName(string name)
        {
            return await _context.RoleTypes.Where(x => x.name == name).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteRoleTypeById(int id)
        {
            _context.RoleTypes.Remove(_context.RoleTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region organizations
        public async Task<int> SaveOrganization(HRPMS.Data.Entity.Master.Organization organization)
        {
            if (organization.Id != 0)
            {
                _context.organizations.Update(organization);
            }
            else
            {
                _context.organizations.Add(organization);
            }
            await _context.SaveChangesAsync();
            return organization.Id;
        }

        public async Task<IEnumerable<HRPMS.Data.Entity.Master.Organization>> GetAllOrganization()
        {
            return await _context.organizations.AsNoTracking().OrderBy(a=>a.Id).ToListAsync();
        }

        //public async Task<IEnumerable<HRPMS.Data.Entity.Master.Organization>> GetAllOrganizationByTypeId(int orgTypeId)
        //{
        //    return await _context.organizations.AsNoTracking().Where(x => x.organizationTypeId == orgTypeId).ToListAsync();
        //}

        public async Task<HRPMS.Data.Entity.Master.Organization> GetOrganizationById(int id)
        {
            return await _context.organizations.Where(x=>x.Id==id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteOrganizationsById(int id)
        {
            _context.organizations.Remove(_context.organizations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> UpdateSupplierLedgerId(int id,int ledgerId)
        {
            HRPMS.Data.Entity.Master.Organization salesInvoiceMaster = await _context.organizations.FindAsync(id);
            if (salesInvoiceMaster != null)
            {
                //salesInvoiceMaster.ledgerId = ledgerId;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }
        public async Task<bool> UpdateCustomerLedgerId(int id, int ledgerId)
        {
            RelSupplierCustomerResourse salesInvoiceMaster = await _context.RelSupplierCustomerResourses.FindAsync(id);
            if (salesInvoiceMaster != null)
            {
                salesInvoiceMaster.ledgerId = ledgerId;
                return 1 == await _context.SaveChangesAsync();
            }
            else
            {
                return 1 == 0;
            }
        }

        #endregion

        #region Resource
        public async Task<int> SaveResource(Resource resource)
        {            
            if (resource.Id != 0)
            {
                resource.updatedBy = resource.updatedBy;
                resource.updatedAt = DateTime.Now;
                _context.Resources.Update(resource);
            }
            else
            {
                resource.createdBy = resource.createdBy;
                resource.createdAt = DateTime.Now;
                _context.Resources.Add(resource);
            }
            //return resource.Id == await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return resource.Id;
        }

        public async Task<IEnumerable<Resource>> GetAllResource()
        {
            return await _context.Resources.AsNoTracking().ToListAsync();
        }

        //public async Task<IEnumerable<Resource>> GetAllResourceByOrganizationId(int orgId)
        //{
        //    return await _context.Resources.AsNoTracking().Where(x => x.organizationId == orgId).ToListAsync();
        //}

        public async Task<Resource> GetResourceById(int id)
        {
            return await _context.Resources.FindAsync(id);
        }

        public async Task<bool> DeleteResourceById(int id)
        {
            _context.Resources.Remove(_context.Resources.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion
        #region area
        public async Task<int> SaveRelCustomerArea(RelCustomerArea relCustomerArea)
        {
            if (relCustomerArea.Id != 0)
            {
                relCustomerArea.updatedBy = relCustomerArea.updatedBy;
                relCustomerArea.updatedAt = DateTime.Now;
                _context.RelCustomerAreas.Update(relCustomerArea);
            }
            else
            {
                relCustomerArea.createdBy = relCustomerArea.createdBy;
                relCustomerArea.createdAt = DateTime.Now;
                _context.RelCustomerAreas.Add(relCustomerArea);
            }
            //return resource.Id == await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return relCustomerArea.Id;
        }

        public async Task<IEnumerable<RelCustomerArea>> GetAllRelCustomerArea()
        {
            return await _context.RelCustomerAreas.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<RelCustomerArea>> GetAllRelCustomerArearesourceId(int Id)
        {
            return await _context.RelCustomerAreas.Include(x=>x.area).Where(x=>x.relSupplierCustomerResourse.resourceId==Id).AsNoTracking().ToListAsync();
        }



        public async Task<RelCustomerArea> GetRelCustomerAreaById(int id)
        {
            return await _context.RelCustomerAreas.FindAsync(id);
        }

        public async Task<bool> DeleteRelCustomerAreaById(int id)
        {
            _context.RelCustomerAreas.Remove(_context.RelCustomerAreas.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteRelCustomerAreaByrelId(int id)
        {
            _context.RelCustomerAreas.RemoveRange(_context.RelCustomerAreas.Where(x=>x.relSupplierCustomerResourseId==id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region Sales Team
        public async Task<int> SaverelCustomerSalesTeamDeployment(RelCustomerSalesTeamDeployment relCustomerSalesTeamDeployment)
        {
            if (relCustomerSalesTeamDeployment.Id != 0)
            {
                relCustomerSalesTeamDeployment.updatedBy = relCustomerSalesTeamDeployment.updatedBy;
                relCustomerSalesTeamDeployment.updatedAt = DateTime.Now;
                _context.relCustomerSalesTeamDeployments.Update(relCustomerSalesTeamDeployment);
            }
            else
            {
                relCustomerSalesTeamDeployment.createdBy = relCustomerSalesTeamDeployment.createdBy;
                relCustomerSalesTeamDeployment.createdAt = DateTime.Now;
                _context.relCustomerSalesTeamDeployments.Add(relCustomerSalesTeamDeployment);
            }
            //return resource.Id == await _context.SaveChangesAsync();
            await _context.SaveChangesAsync();
            return relCustomerSalesTeamDeployment.Id;
        }

        public async Task<IEnumerable<RelCustomerSalesTeamDeployment>> GetAllrelCustomerSalesTeamDeployment()
        {
            return await _context.relCustomerSalesTeamDeployments.Include(x => x.employeeInfo).Include(x => x.salesLevel).Include(x => x.area).Include(x => x.tsmsalesTeamDeployment.salesLevel).Include(x => x.tsmsalesTeamDeployment.employeeInfo).Include(x => x.tsmsalesTeamDeployment.area).Include(x => x.rsmsalesTeamDeployment.salesLevel).Include(x => x.rsmsalesTeamDeployment.employeeInfo).Include(x => x.rsmsalesTeamDeployment.area).Include(x => x.nsmsalesTeamDeployment.salesLevel).Include(x => x.nsmsalesTeamDeployment.employeeInfo).Include(x => x.nsmsalesTeamDeployment.area).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<RelCustomerSalesTeamDeployment>> GetAllrelCustomerSalesTeamDeploymentresourceId(int Id)
        {
            List<RelCustomerSalesTeamDeployment> data = new List<RelCustomerSalesTeamDeployment>();
            data= await _context.relCustomerSalesTeamDeployments.Include(x => x.employeeInfo).Include(x => x.salesLevel).Include(x => x.area).Where(x => x.relSupplierCustomerResourseId == Id).AsNoTracking().ToListAsync();
            return data;
        }



        public async Task<RelCustomerSalesTeamDeployment> GetRelCustomerSalesTeamDeploymentById(int id)
        {
            return await _context.relCustomerSalesTeamDeployments.FindAsync(id);
        }

        public async Task<bool> DeleteRelCustomerSalesTeamDeploymentById(int id)
        {
            _context.relCustomerSalesTeamDeployments.Remove(_context.relCustomerSalesTeamDeployments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteRelCustomerSalesTeamDeploymentByrelId(int id)
        {
            _context.relCustomerSalesTeamDeployments.RemoveRange(_context.relCustomerSalesTeamDeployments.Where(x => x.relSupplierCustomerResourseId == id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region RelSupplierCustomerResourse
        public async Task<int> SaveRelSupplierCustomerResourse(RelSupplierCustomerResourse relSupplierCustomerResourse)
        {
            if (relSupplierCustomerResourse.Id != 0)
            {
                relSupplierCustomerResourse.updatedBy = relSupplierCustomerResourse.updatedBy;
                relSupplierCustomerResourse.updatedAt = DateTime.Now;
                _context.RelSupplierCustomerResourses.Update(relSupplierCustomerResourse);
            }
            else
            {
                relSupplierCustomerResourse.createdBy = relSupplierCustomerResourse.createdBy;
                relSupplierCustomerResourse.createdAt = DateTime.Now;
                _context.RelSupplierCustomerResourses.Add(relSupplierCustomerResourse);
            }
            await _context.SaveChangesAsync();
            return relSupplierCustomerResourse.Id;
        }

        public async Task<IEnumerable<RelSupplierCustomerResourse>> GetAllRelSupplierCustomerResourse()
        {
            return await _context.RelSupplierCustomerResourses.Include(x => x.Leads).Include(x => x.roleType).Include(x => x.distributorType).Include(a => a.resource).Include(a => a.organization).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RelSupplierCustomerResourse>> GetAllRelSupplierCustomerResourseByRoleId(int roletypeId)
        {
            var data = await _context.RelSupplierCustomerResourses.Include(x => x.Leads).Include(x => x.roleType).Include(x => x.distributorType).Include(a => a.resource).Include(a => a.organization).AsNoTracking().ToListAsync();
            //var data = await _context.RelSupplierCustomerResourses.Include(x => x.Leads).Include(x => x.roleType).Include(x => x.distributorType).Include(a => a.resource).Include(a => a.organization).AsNoTracking().Where(x => x.roleTypeId == roletypeId).ToListAsync();
            return data;
        }
        //public async Task<IEnumerable<RelSupplierCustomerResourseViewModel>> GetRelSupplierCustomerResourseByRoleId(int roletypeId)
        //{
        //    var data = await (from RSCR in _context.RelSupplierCustomerResourses
        //                        join L in _context.Leads on RSCR.LeadsId equals L.Id
        //                        join RT in _context.RoleTypes on RSCR.roleTypeId equals RT.Id
        //                        join DT in _context.distributorTypes on RSCR.distributorTypeId equals DT.Id
        //                        join R in _context.Resources on RSCR.resourceId equals R.Id
        //                        join O in _context.Organizations on RSCR.organizationId equals O.Id
        //                        join PA in _context.Address on L.Id equals PA.leadsId where PA.addressTypeId==1
        //                        join A in _context.Address on L.Id equals A.leadsId where A.addressTypeId==2
        //                        //join aa in (from a1 in _context.ApprovalLogs
        //                        //            where a1.matrixTypeId == 2
        //                        //            group a1 by a1.masterId into g
        //                        //            select new { masterId = g.Key, sequenceNo = g.Max(x => x.sequenceNo) }) on m.Id equals aa.masterId
        //                        //join a in _context.ApprovalLogs.Where(x => x.matrixTypeId == 2) on new { aa.masterId, aa.sequenceNo } equals new { a.masterId, a.sequenceNo }
        //                        //join fap in _context.Users on a.userId equals fap.userId
        //                        //join ap in _context.employeeInfos on fap.Id equals ap.ApplicationUserId
        //                        where RSCR.roleTypeId == roletypeId
        //                        select new RelSupplierCustomerResourseViewModel
        //                        {
        //                            relSupplierCustomerResourceId = RSCR.Id,
        //                            organizationId = RSCR.organizationId,
        //                            resourceId = RSCR.resourceId,
        //                            roleTypeId = RSCR.roleTypeId,
        //                            ledgerId = RSCR.ledgerId,
        //                            distributorTypeId = RSCR.distributorTypeId,
        //                            LeadsId = RSCR.LeadsId,
        //                            presentAddress=A.maillingAddress,
        //                            permanentAddress=PA.maillingAddress,
        //                            phoneNo = R.phone
        //                        }).ToListAsync();

        //    return data;

        //}

        public async Task<IEnumerable<RelSupplierCustomerResourseViewModel>> GetRelSupplierCustomerResourseByRoleId(int roletypeId)
        {
            return await _context.relSupplierCustomerResourseViewModels.FromSql(@"SP_GetRelSupplierCustomerResourseByRoleId @p0", roletypeId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<CustomerInfoViewModel>> GetCustomerInfoByRoleId(int roletypeId)
        {
            return await _context.customerInfoViewModels.FromSql(@"SP_GetCustomerInfoByRoleId @p0", roletypeId).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<RelSupplierCustomerResourse>> GetAllRelSupplierCustomerResourseByRoleDisId(int roletypeId)
        {
            var data = await _context.RelSupplierCustomerResourses.Include(x => x.roleType).Include(x => x.distributorType).Include(a => a.resource).Include(a => a.organization).AsNoTracking().Where(x => x.roleTypeId == roletypeId && x.distributorTypeId!=null).ToListAsync();
            return data;
        }

        public async Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseById(int id)
        {
            return await _context.RelSupplierCustomerResourses.Where(x=>x.Id==id).Include(x=>x.resource).Include(x => x.Leads).FirstOrDefaultAsync();
        }
        public async Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseByLeadId(int id)
        {
            return await _context.RelSupplierCustomerResourses.Where(x => x.LeadsId == id).Include(x => x.resource).Include(x => x.organization).FirstOrDefaultAsync();
        }
        public async Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseBysupplier(string SupplierName)
        {
            return await _context.RelSupplierCustomerResourses.Where(x => x.roleTypeId == 1&& x.organization.organizationName==SupplierName).Include(x => x.resource).Include(x => x.organization).FirstOrDefaultAsync();
        }

        public async Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseByResourceId(int id)
        {
            return await _context.RelSupplierCustomerResourses.Include(x=>x.roleType).Include(x=>x.distributorType).Where(x=>x.resourceId == id).FirstOrDefaultAsync();
        }

        public async Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseByOrganizationId(int id)
        {
            return await _context.RelSupplierCustomerResourses.Where(x => x.organizationId == id).Include(x=>x.organization).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<RelSupplierCustomerResourse>> GetRelSupplierCustomerResourseByResourceshow()
        {
            return await _context.RelSupplierCustomerResourses.Include(x=>x.resource).Include(x => x.roleType).Include(x => x.distributorType).Where(x => x.roleTypeId==3&&x.distributorTypeId!=null).ToListAsync();
        }

        public async Task<bool> DeleteRelSupplierCustomerResourseById(int id)
        {
            _context.RelSupplierCustomerResourses.Remove(_context.RelSupplierCustomerResourses.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Contact
        public async Task<int> Savecontact(Contact contact)
        {
            if (contact.Id != 0)
            {
                contact.updatedBy = contact.createdBy;
                contact.updatedAt = DateTime.Now;
                _context.Contacts.Update(contact);
            }
            else
            {
                contact.createdAt = DateTime.Now;
                _context.Contacts.Add(contact);
            }

            await _context.SaveChangesAsync();
            return contact.Id;
        }

        public async Task<IEnumerable<Contact>> Getcontact()
        {
            return await _context.Contacts.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetcontactbyResourceid(int Id)
        {
            return await _context.Contacts.AsNoTracking().Where(x => x.resourceId == Id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Contact>> GetcontactbyOrganizationId(int Id)
        {
            return await _context.Contacts.AsNoTracking().Where(x=>x.organizationId==Id).AsNoTracking().ToListAsync();
        }

        //public async Task<IEnumerable<Contact>> GetcontactbyCompanyId(int Id)
        //{
        //    return await _context.Contacts.AsNoTracking().Where(x=>x.companyId==Id).AsNoTracking().ToListAsync();
        //}

        public async Task<Contact> GetcontactById(int id)
        {
            return await _context.Contacts.FindAsync(id);
        }

        public async Task<bool> DeletecontactById(int id)
        {
            _context.Contacts.Remove(_context.Contacts.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Customer Item Info
        //public async Task<int> SaveCustomerItemInfo(CustomerItemInfo customerItemInfo)
        //{
        //    if (customerItemInfo.Id != 0)
        //    {
        //        customerItemInfo.updatedBy = customerItemInfo.createdBy;
        //        customerItemInfo.updatedAt = DateTime.Now;
        //        _context.CustomerItemInfos.Update(customerItemInfo);
        //    }
        //    else
        //    {
        //        customerItemInfo.createdAt = DateTime.Now;
        //        _context.CustomerItemInfos.Add(customerItemInfo);
        //    }

        //    await _context.SaveChangesAsync();
        //    return customerItemInfo.Id;
        //}

        //public async Task<IEnumerable<CustomerItemInfo>> GetCustomerItemInfo()
        //{
        //    return await _context.CustomerItemInfos.AsNoTracking().AsNoTracking().ToListAsync();
        //}

        //public async Task<CustomerItemInfo> GetCustomerItemInfoById(int id)
        //{
        //    return await _context.CustomerItemInfos.FindAsync(id);
        //}
        //public async Task<IEnumerable<CustomerItemInfo>> GetCustomerItemInfobyResourceId(int Id)
        //{
        //    return await _context.CustomerItemInfos.Where(x => x.resourceId == Id).Include(x => x.item).AsNoTracking().ToListAsync();
        //}
        //public async Task<IEnumerable<CustomerItemInfo>> GetCustomerItemInfobyOrganizationId(int Id)
        //{
        //    return await _context.CustomerItemInfos.Where(x => x.organizationId == Id).Include(x => x.item).AsNoTracking().ToListAsync();
        //}

        //public async Task<bool> DeleteCustomerItemInfoById(int id)
        //{
        //    _context.CustomerItemInfos.Remove(_context.CustomerItemInfos.Find(id));
        //    return 1 == await _context.SaveChangesAsync();
        //}
        //public async Task<bool> DeleteCustomerItemInfoByResourceId(int id)
        //{
        //    IEnumerable<CustomerItemInfo> stationFuelInfos = await _context.CustomerItemInfos.Where(x => x.resourceId == id).AsNoTracking().ToListAsync();
        //    foreach (CustomerItemInfo data in stationFuelInfos)
        //    {
        //        _context.CustomerItemInfos.Remove(_context.CustomerItemInfos.Find(data.Id));
        //    }
        //    return 1 == await _context.SaveChangesAsync();
        //}

        public async Task<bool> DeleteAddressByResourceId(int id)
        {
            IEnumerable<CRM.Data.Entity.Lead.Address> addresses = await _context.Address.Where(x => x.resourceId == id).AsNoTracking().ToListAsync();
            foreach (CRM.Data.Entity.Lead.Address data in addresses)
            {
                _context.Address.Remove(_context.Address.Find(data.Id));
            }
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteContactByResourceId(int id)
        {
            IEnumerable<Contact> contacts = await _context.Contacts.Where(x => x.resourceId == id).AsNoTracking().ToListAsync();
            foreach (Contact data in contacts)
            {
                _context.Contacts.Remove(_context.Contacts.Find(data.Id));
            }
            return 1 == await _context.SaveChangesAsync();
        }


        //public async Task<bool> DeleteCustomerItemInfoByOrganizationId(int id)
        //{
        //    IEnumerable<CustomerItemInfo> stationFuelInfos = await _context.CustomerItemInfos.Where(x => x.organizationId == id).AsNoTracking().ToListAsync();
        //    foreach (CustomerItemInfo data in stationFuelInfos)
        //    {
        //        _context.CustomerItemInfos.Remove(_context.CustomerItemInfos.Find(data.Id));
        //    }
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteAddressByOrganizationId(int id)
        //{
        //    IEnumerable<Address> addresses = await _context.Addresses.Where(x => x.organizationId == id).AsNoTracking().ToListAsync();
        //    foreach (Address data in addresses)
        //    {
        //        _context.Addresses.Remove(_context.Addresses.Find(data.Id));
        //    }
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteAddressByCompanyId(int id)
        //{
        //    IEnumerable<Address> addresses = await _context.Addresses.Where(x => x.companyId == id).AsNoTracking().ToListAsync();
        //    foreach (Address data in addresses)
        //    {
        //        _context.Addresses.Remove(_context.Addresses.Find(data.Id));
        //    }
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteContactByOrganizationId(int id)
        //{
        //    IEnumerable<Contact> contacts = await _context.Contacts.Where(x => x.organizationId == id).AsNoTracking().ToListAsync();
        //    foreach (Contact data in contacts)
        //    {
        //        _context.Contacts.Remove(_context.Contacts.Find(data.Id));
        //    }
        //    return 1 == await _context.SaveChangesAsync();
        //}

        //public async Task<bool> DeleteContactByCompanyId(int id)
        //{
        //    IEnumerable<Contact> contacts = await _context.Contacts.Where(x => x.companyId == id).AsNoTracking().ToListAsync();
        //    foreach (Contact data in contacts)
        //    {
        //        _context.Contacts.Remove(_context.Contacts.Find(data.Id));
        //    }
        //    return 1 == await _context.SaveChangesAsync();
        //}



        #endregion


    }
}