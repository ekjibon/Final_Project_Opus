using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data.Entity.Inspection;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;

namespace OPUSERP.VMS.Services.VehicleService.Interfaces
{
   public interface IVehicleServiceHistoryService
    {
        #region VehicleServiceHistory
        Task<int> SaveVehicleServiceHistory(VehicleServiceHistory vehicleServiceHistory);
        Task<IEnumerable<VehicleServiceHistory>> GetVehicleServiceHistory();
        Task<IEnumerable<VehicleServiceHistory>> GetVehicleServiceHistoryByserviceId(int serviceId);
        Task<VehicleServiceHistory> GetVehicleServiceHistoryById(int id);
        Task<bool> DeleteVehicleServiceHistoryById(int id);
        #endregion

        #region VehicleLineItem
        Task<int> SaveVehicleLineItem(VehicleLineItem vehicleLineItem);
        Task<IEnumerable<VehicleLineItem>> GetVehicleLineItemByserviceId(int serviceId);
        Task<VehicleLineItem> GetVehicleLineItemById(int id);
        Task<bool> DeleteVehicleLineItemById(int id);
        Task<bool> DeleteVehicleLineItemByserviceId(int serviceId);
        #endregion

        #region VehicleServiceComment
        Task<int> SaveVehicleServiceComment(VehicleServiceComment vehicleServiceComment);
        Task<IEnumerable<VehicleServiceComment>> GetVehicleServiceComment();
        Task<IEnumerable<VehicleServiceComment>> GetVehicleServiceCommentByserviceId(int serviceId);
        Task<VehicleServiceComment> GetVehicleServiceCommentById(int id);
        Task<bool> DeleteVehicleServiceCommentById(int id);
        #endregion

        #region Supplier
        Task<int> SaveSupplier(VMSSupplier supplier);
        Task<IEnumerable<VMSSupplier>> GetSupplier();
        Task<VMSSupplier> GetSupplierById(int id);
        Task<bool> DeleteSupplierById(int id);
        #endregion

        #region LabelType
        Task<int> SaveLabelType(LabelType labelType);
        Task<IEnumerable<LabelType>> GetLabelType();
        Task<LabelType> GetLabelTypeById(int id);
        Task<bool> DeleteLabelTypeById(int id);
        #endregion

        #region ServiceLabel
        Task<int> SaveServiceLabel(ServiceLabel serviceLabel);
        Task<IEnumerable<ServiceLabel>> GetServiceLabel();
        Task<IEnumerable<ServiceLabel>> GetServiceLabelByvehicleServiceHistoryId(int vehicleServiceHistoryId);
        Task<ServiceLabel> GetServiceLabelById(int id);
        Task<bool> DeleteServiceLabelById(int id);
        #endregion

        #region ServiceTask
        Task<int> SaveServiceTask(ServiceTask serviceTask);
        Task<IEnumerable<ServiceTask>> GetServiceTask();
        Task<ServiceTask> GetServiceTaskById(int id);
        Task<bool> DeleteServiceTaskById(int id);
        #endregion
        #region VehicleServiceIssue
        Task<int> SaveVehicleServiceIssue(VehicleServiceIssue vehicleServiceIssue);
        Task<IEnumerable<VehicleServiceIssue>> GetVehicleServiceIssue();
        Task<IEnumerable<VehicleServiceIssue>> GetVehicleServiceIssueByVehicleId(int vid);
        Task<VehicleServiceIssue> GetVehicleServiceIssueById(int id);
        Task<bool> DeleteVehicleServiceIssuesById(int id);
        #endregion

        #region Vehicle Service Reminder

        Task<int> SaveVehicleServiceReminder(VehicleServiceReminder vehicleServiceReminder);
        Task<IEnumerable<VehicleServiceReminder>> GetVehicleServiceReminder();
        Task<IEnumerable<VehicleServiceReminder>> GetVehicleServiceReminderByVehicleId(int VehicleId);
        Task<VehicleServiceReminder> GetVehicleServiceReminderById(int id);
        Task<bool> DeleteVehicleServiceReminderById(int id);

        #endregion

        #region Reminder Schedule

        Task<int> SaveReminderSchedule(ReminderSchedule reminderSchedule);
        Task<IEnumerable<ReminderSchedule>> GetReminderScheduleByServiceId(int serviceId);
        Task<ReminderSchedule> GetReminderScheduleById(int id);
        Task<bool> DeleteReminderScheduleById(int id);
        Task<bool> DeleteReminderScheduleByServiceId(int serviceId);
        #endregion

        #region VehicleServiceIssueComment
        Task<int> SaveVehicleServiceIssueComment(VehicleServiceIssueComment vehicleServiceIssueComment);
        Task<IEnumerable<VehicleServiceIssueComment>> GetVehicleServiceIssueComment();
        Task<IEnumerable<VehicleServiceIssueComment>> GetVehicleServiceIssueCommentByIssueId(int serviceId);
        Task<VehicleServiceIssueComment> GetVehicleServiceIssueCommentById(int id);
        Task<bool> DeleteVehicleServiceIssueCommentById(int id);
        #endregion

        #region VehicleInspection
        #region Inspection Master
        Task<int> SaveinspectionMaster(InspectionMaster inspectionMaster);
        Task<IEnumerable<InspectionMaster>> GetinspectionMaster();
        Task<IEnumerable<InspectionMaster>> GetinspectionMasterByvehicleId(int serviceId);
        Task<InspectionMaster> GetinspectionMasterById(int id);
        Task<bool> DeleteinspectionMasterById(int id);
        #endregion
        #region Inspection Details
        Task<int> SaveinspectionDetail(InspectionDetail inspectionDetail);
        Task<IEnumerable<InspectionDetail>> GetinspectionDetail();
        Task<IEnumerable<InspectionDetail>> GetinspectionDetailByMasterId(int serviceId);
        Task<InspectionDetail> GetinspectionDetailById(int id); 
         Task<bool> DeleteinspectionDetailById(int id);
        Task<bool> DeleteInspectionDetailBymasterId(int id);

        #endregion
        #region Inspection Category
        Task<int> SaveinspectionCheckLIstCategory(InspectionCheckLIstCategory inspectionCheckLIstCategory);
        Task<IEnumerable<InspectionCheckLIstCategory>> GetInspectionCheckLIstCategory();
   
        Task<InspectionCheckLIstCategory> GetInspectionCheckLIstCategoryById(int id);
        Task<bool> DeleteInspectionCheckLIstCategoriesById(int id);
        #endregion
        #endregion
        #region maintenance
        #region maintenance master
        Task<int> SavevehicleMaintenanceLimit(VehicleMaintenanceLimit vehicleMaintenanceLimit);
        Task<IEnumerable<VehicleMaintenanceLimit>> GetvehicleMaintenanceLimit();
        Task<IEnumerable<VehicleMaintenanceLimit>> GetvehicleMaintenanceLimitByvehicleId(int serviceId);
        Task<VehicleMaintenanceLimit> GetvehicleMaintenanceLimitById(int id);
        Task<bool> DeletevehicleMaintenanceLimitById(int id);
        #endregion
        #region maintenance Detail
        Task<int> SavevehicleMaintenanceLimitDetail(VehicleMaintenanceLimitDetail vehicleMaintenanceLimitDetail);
        Task<IEnumerable<VehicleMaintenanceLimitDetail>> GetvehicleMaintenanceLimitDetail();
        Task<IEnumerable<VehicleMaintenanceLimitDetail>> GetvehicleMaintenanceLimitDetailByMasterId(int serviceId);
        Task<VehicleMaintenanceLimitDetail> GetvehicleMaintenanceLimitDetailById(int id);
        Task<bool> DeletevehicleMaintenanceLimitDetailById(int id);
        Task<bool> DeletevehicleMaintenanceLimitDetailBymasterId(int id);

        #endregion
        #region limit Amount Type 
        Task<int> SavelimitAmountType(LimitAmountType limitAmountType);
        Task<IEnumerable<LimitAmountType>> GetlimitAmountType();

        Task<LimitAmountType> GetlimitAmountTypeById(int id);
        Task<bool> DeletelimitAmountTypeById(int id);
        #endregion
        #region limit Period Type 
        Task<int> SavelimitPeriodType(LimitPeriodType limitPeriodType);
        Task<IEnumerable<LimitPeriodType>> GetlimitPeriodType();

        Task<LimitPeriodType> GetlimitPeriodTypeById(int id);
        Task<bool> DeletelimitPeriodTypeById(int id);
        #endregion
        #endregion

        #region VehicleRenewalReminder
        Task<int> SaveVehicleRenewalReminder(VehicleRenewalReminder vehicleRenewalReminder);
        Task<IEnumerable<VehicleRenewalReminder>> GetVehicleRenewalReminder();
        Task<IEnumerable<VehicleRenewalReminder>> GetVehicleRenewalReminderById(int Id);
        Task<bool> DeleteVehicleRenewalReminderById(int id);
        #endregion
        #region RenewalType
        Task<int> SaveRenewalType(RenewalType renewalType);
        Task<IEnumerable<RenewalType>> GetRenewalType();
        Task<bool> DeleteRenewalTypeById(int id);
        #endregion

        #region ContactRenewalReminder
        Task<int> SaveContactRenewalReminder(ContactRenewalReminder contactRenewalReminder);
        Task<IEnumerable<ContactRenewalReminder>> GetContactRenewalReminder();
        Task<IEnumerable<ContactRenewalReminder>> GetContactRenewalReminderById(int Id);
        Task<bool> DeleteContactRenewalReminderById(int id);
        #endregion
    }
}
