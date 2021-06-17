using OPUSERP.Areas.OtherSales.Models;
using OPUSERP.Areas.POS.Models;
using OPUSERP.Areas.Rental.Models;
using OPUSERP.CRM.Data.Entity.Lead;
using OPUSERP.Distribution.Data.Entity.MasterData;
using OPUSERP.HRPMS.Data.Entity.Master;
using OPUSERP.OtherSales.Data.Entity.Sales;
using OPUSERP.POS.Data.Entity;
using OPUSERP.SCM.Data.Entity.MasterData;
using OPUSERP.SCM.Data.Entity.Supplier;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.OtherSales.Services.Sales.Interfaces
{
    public interface ICustomerService
    {
        #region RoleTypes
        Task<bool> SaveRoleType(RoleType roleType);
        Task<IEnumerable<RoleType>> GetAllRoleType();
        Task<RoleType> GetRoleTypeIdByName(string name);
        Task<bool> DeleteRoleTypeById(int id);
        #endregion

        #region Organizations
        Task<int> SaveOrganization(HRPMS.Data.Entity.Master.Organization organization);
        Task<IEnumerable<HRPMS.Data.Entity.Master.Organization>> GetAllOrganization();
        //Task<IEnumerable<Organization>> GetAllOrganizationByTypeId(int orgTypeId);
        Task<HRPMS.Data.Entity.Master.Organization> GetOrganizationById(int id);
        Task<bool> DeleteOrganizationsById(int id);
        //Task<bool> DeleteAddressByCompanyId(int id);
        //Task<bool> DeleteContactByCompanyId(int id);
        Task<bool> UpdateSupplierLedgerId(int id, int ledgerId);
        Task<bool> UpdateCustomerLedgerId(int id, int ledgerId);
        #endregion

        #region Resources
        Task<int> SaveResource(Resource resource);
        Task<IEnumerable<Resource>> GetAllResource();
        //Task<IEnumerable<Resource>> GetAllResourceByOrganizationId(int orgId);
        Task<Resource> GetResourceById(int id);
        Task<bool> DeleteResourceById(int id);
        #endregion
        #region Rel Customer Area
        Task<int> SaveRelCustomerArea(RelCustomerArea relCustomerArea);
        Task<IEnumerable<RelCustomerArea>> GetAllRelCustomerArea();
        //Task<IEnumerable<Resource>> GetAllResourceByOrganizationId(int orgId);
        Task<RelCustomerArea> GetRelCustomerAreaById(int id);
        Task<bool> DeleteRelCustomerAreaById(int id);
        Task<bool> DeleteRelCustomerAreaByrelId(int id);
        Task<IEnumerable<RelCustomerArea>> GetAllRelCustomerArearesourceId(int Id);
        #endregion

        #region RelSupplierCustomerResourses
        Task<int> SaveRelSupplierCustomerResourse(RelSupplierCustomerResourse relSupplierCustomerResourse);
        Task<IEnumerable<RelSupplierCustomerResourse>> GetAllRelSupplierCustomerResourse();
        Task<IEnumerable<RelSupplierCustomerResourse>> GetAllRelSupplierCustomerResourseByRoleId(int roletypeId);
        Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseById(int id);
        Task<bool> DeleteRelSupplierCustomerResourseById(int id);
        Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseByResourceId(int id);
        Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseByOrganizationId(int id);
        Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseBysupplier(string SupplierName);
        Task<IEnumerable<RelSupplierCustomerResourse>> GetAllRelSupplierCustomerResourseByRoleDisId(int roletypeId);
        Task<IEnumerable<RelSupplierCustomerResourse>> GetRelSupplierCustomerResourseByResourceshow();
        Task<RelSupplierCustomerResourse> GetRelSupplierCustomerResourseByLeadId(int id);
        Task<IEnumerable<RelSupplierCustomerResourseViewModel>> GetRelSupplierCustomerResourseByRoleId(int roletypeId);
        Task<IEnumerable<CustomerInfoViewModel>> GetCustomerInfoByRoleId(int roletypeId);
        #endregion

        #region Contacts
        Task<int> Savecontact(Contact contact);
        Task<IEnumerable<Contact>> Getcontact();
        Task<Contact> GetcontactById(int id);
        Task<bool> DeletecontactById(int id);
        Task<IEnumerable<Contact>> GetcontactbyResourceid(int Id);
        Task<IEnumerable<Contact>> GetcontactbyOrganizationId(int Id);
        //Task<IEnumerable<Contact>> GetcontactbyCompanyId(int Id);
        #endregion

        #region Customer Item Info
        //Task<int> SaveCustomerItemInfo(CustomerItemInfo customerItemInfo);
        //Task<IEnumerable<CustomerItemInfo>> GetCustomerItemInfo();
        //Task<IEnumerable<CustomerItemInfo>> GetCustomerItemInfobyResourceId(int Id);
        //Task<CustomerItemInfo> GetCustomerItemInfoById(int id);
        //Task<bool> DeleteCustomerItemInfoById(int id);
        //Task<bool> DeleteCustomerItemInfoByResourceId(int id);
        Task<bool> DeleteAddressByResourceId(int id);
        Task<bool> DeleteContactByResourceId(int id);
        //Task<IEnumerable<CustomerItemInfo>> GetCustomerItemInfobyOrganizationId(int Id);

        //Task<bool> DeleteCustomerItemInfoByOrganizationId(int id);
        //Task<bool> DeleteAddressByOrganizationId(int id);
        //Task<bool> DeleteContactByOrganizationId(int id);
        #endregion

        #region Rel Customer Sales Team
        Task<int> SaverelCustomerSalesTeamDeployment(RelCustomerSalesTeamDeployment relCustomerSalesTeamDeployment);
        Task<IEnumerable<RelCustomerSalesTeamDeployment>> GetAllrelCustomerSalesTeamDeployment();
        //Task<IEnumerable<Resource>> GetAllResourceByOrganizationId(int orgId);
        Task<IEnumerable<RelCustomerSalesTeamDeployment>> GetAllrelCustomerSalesTeamDeploymentresourceId(int Id);
        Task<RelCustomerSalesTeamDeployment> GetRelCustomerSalesTeamDeploymentById(int id);
        Task<bool> DeleteRelCustomerSalesTeamDeploymentById(int id);
        Task<bool> DeleteRelCustomerSalesTeamDeploymentByrelId(int id);
        #endregion

    }
}
