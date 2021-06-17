using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OPUSERP.VMS.Data;
using OPUSERP.VMS.Data.Entity.Inspection;
using OPUSERP.VMS.Data.Entity.Master;
using OPUSERP.VMS.Data.Entity.ServiceVehicle;
using OPUSERP.VMS.Data.Entity.VehicleInfo;
using OPUSERP.VMS.Services.VehicleService.Interfaces;
using OPUSERP.Data;

namespace OPUSERP.VMS.Services.VehicleService
{
    public class VehicleServiceHistoryService : IVehicleServiceHistoryService
    {
        private readonly ERPDbContext _context;

        public VehicleServiceHistoryService(ERPDbContext context)
        {
            _context = context;
        }
        #region VehicleServiceHistory
        public async Task<int> SaveVehicleServiceHistory(VehicleServiceHistory  vehicleServiceHistory)
        {
            if (vehicleServiceHistory.Id != 0)
            {
                vehicleServiceHistory.updatedBy = vehicleServiceHistory.createdBy;
                vehicleServiceHistory.updatedAt = DateTime.Now;
                _context.VehicleServiceHistories.Update(vehicleServiceHistory);
            }
            else
            {
                vehicleServiceHistory.createdAt = DateTime.Now;
                _context.VehicleServiceHistories.Add(vehicleServiceHistory);
            }

            await _context.SaveChangesAsync();
            return vehicleServiceHistory.Id;
        }

        public async Task<IEnumerable<VehicleServiceHistory>> GetVehicleServiceHistory()
        {
            return await _context.VehicleServiceHistories.AsNoTracking().Include(x => x.vehicleInformation).Include(x=>x.supplier).ToListAsync();
        }
        public async Task<IEnumerable<VehicleServiceHistory>> GetVehicleServiceHistoryByserviceId(int serviceId)
        {
            return await _context.VehicleServiceHistories.AsNoTracking().Include(x => x.vehicleInformation).Include(x => x.supplier).Where(x => x.Id == serviceId).ToListAsync();
        }

        public async Task<VehicleServiceHistory> GetVehicleServiceHistoryById(int id)
        {
            return await _context.VehicleServiceHistories.Include(x=>x.supplier).Where(x=>x.Id== id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteVehicleServiceHistoryById(int id)
        {
            _context.VehicleServiceHistories.Remove(_context.VehicleServiceHistories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region VehicleLineItem
        public async Task<int> SaveVehicleLineItem(VehicleLineItem  vehicleLineItem)
        {
            if (vehicleLineItem.Id != 0)
            {
                vehicleLineItem.updatedBy = vehicleLineItem.createdBy;
                vehicleLineItem.updatedAt = DateTime.Now;
                _context.VehicleLineItems.Update(vehicleLineItem);
            }
            else
            {
                vehicleLineItem.createdAt = DateTime.Now;
                _context.VehicleLineItems.Add(vehicleLineItem);
            }

            await _context.SaveChangesAsync();
            return vehicleLineItem.Id;
        }

        public async Task<IEnumerable<VehicleLineItem>> GetVehicleLineItemByserviceId(int serviceId)
        {
            return await _context.VehicleLineItems.AsNoTracking().Include(x => x.spareParts).Where(x => x.vehicleServiceHistoryId == serviceId).AsNoTracking().ToListAsync();
        }

        public async Task<VehicleLineItem> GetVehicleLineItemById(int id)
        {
            return await _context.VehicleLineItems.FindAsync(id);
        }

        public async Task<bool> DeleteVehicleLineItemById(int id)
        {
            _context.VehicleLineItems.Remove(_context.VehicleLineItems.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteVehicleLineItemByserviceId(int serviceId)
        {
            _context.VehicleLineItems.RemoveRange(_context.VehicleLineItems.Where(x => x.vehicleServiceHistoryId == serviceId));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region VehicleServiceComment
        public async Task<int> SaveVehicleServiceComment(VehicleServiceComment vehicleServiceComment)
        {
            if (vehicleServiceComment.Id != 0)
            {
                vehicleServiceComment.updatedBy = vehicleServiceComment.createdBy;
                vehicleServiceComment.updatedAt = DateTime.Now;
                _context.VehicleServiceComments.Update(vehicleServiceComment);
            }
            else
            {
                vehicleServiceComment.createdAt = DateTime.Now;
                _context.VehicleServiceComments.Add(vehicleServiceComment);
            }

            await _context.SaveChangesAsync();
            return vehicleServiceComment.Id;
        }

        public async Task<IEnumerable<VehicleServiceComment>> GetVehicleServiceComment()
        {
            return await _context.VehicleServiceComments.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<VehicleServiceComment>> GetVehicleServiceCommentByserviceId(int serviceId)
        {
            return await _context.VehicleServiceComments.AsNoTracking().Where(x => x.vehicleServiceHistoryId == serviceId).ToListAsync();
        }

        public async Task<VehicleServiceComment> GetVehicleServiceCommentById(int id)
        {
            return await _context.VehicleServiceComments.FindAsync(id);
        }

        public async Task<bool> DeleteVehicleServiceCommentById(int id)
        {
            _context.VehicleServiceComments.Remove(_context.VehicleServiceComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Supplier
        public async Task<int> SaveSupplier(VMSSupplier supplier)
        {
            if (supplier.Id != 0)
            {
                supplier.updatedBy = supplier.createdBy;
                supplier.updatedAt = DateTime.Now;
                _context.VMSSuppliers.Update(supplier);
            }
            else
            {
                supplier.createdAt = DateTime.Now;
                _context.VMSSuppliers.Add(supplier);
            }

            await _context.SaveChangesAsync();
            return supplier.Id;
        }

        public async Task<IEnumerable<VMSSupplier>> GetSupplier()
        {
            return await _context.VMSSuppliers.AsNoTracking().ToListAsync();
        }

        public async Task<VMSSupplier> GetSupplierById(int id)
        {
            return await _context.VMSSuppliers.FindAsync(id);
        }

        public async Task<bool> DeleteSupplierById(int id)
        {
            _context.VMSSuppliers.Remove(_context.VMSSuppliers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region LabelType
        public async Task<int> SaveLabelType(LabelType labelType)
        {
            if (labelType.Id != 0)
            {
                labelType.updatedBy = labelType.createdBy;
                labelType.updatedAt = DateTime.Now;
                _context.LabelTypes.Update(labelType);
            }
            else
            {
                labelType.createdAt = DateTime.Now;
                _context.LabelTypes.Add(labelType);
            }

            await _context.SaveChangesAsync();
            return labelType.Id;
        }

        public async Task<IEnumerable<LabelType>> GetLabelType()
        {
            return await _context.LabelTypes.AsNoTracking().ToListAsync();
        }

        public async Task<LabelType> GetLabelTypeById(int id)
        {
            return await _context.LabelTypes.FindAsync(id);
        }

        public async Task<bool> DeleteLabelTypeById(int id)
        {
            _context.LabelTypes.Remove(_context.LabelTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region ServiceLabel
        public async Task<int> SaveServiceLabel(ServiceLabel  serviceLabel)
        {
            if (serviceLabel.Id != 0)
            {
                serviceLabel.updatedBy = serviceLabel.createdBy;
                serviceLabel.updatedAt = DateTime.Now;
                _context.ServiceLabels.Update(serviceLabel);
            }
            else
            {
                serviceLabel.createdAt = DateTime.Now;
                _context.ServiceLabels.Add(serviceLabel);
            }

            await _context.SaveChangesAsync();
            return serviceLabel.Id;
        }

        public async Task<IEnumerable<ServiceLabel>> GetServiceLabel()
        {
            return await _context.ServiceLabels.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<ServiceLabel>> GetServiceLabelByvehicleServiceHistoryId( int vehicleServiceHistoryId)
        {
            return await _context.ServiceLabels.AsNoTracking().Include(x=>x.labelType).Where(x=>x.vehicleServiceHistoryId== vehicleServiceHistoryId).ToListAsync();
        }

        public async Task<ServiceLabel> GetServiceLabelById(int id)
        {
            return await _context.ServiceLabels.FindAsync(id);
        }

        public async Task<bool> DeleteServiceLabelById(int id)
        {
            _context.ServiceLabels.Remove(_context.ServiceLabels.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region ServiceTask
        public async Task<int> SaveServiceTask(ServiceTask  serviceTask)
        {
            if (serviceTask.Id != 0)
            {
                serviceTask.updatedBy = serviceTask.createdBy;
                serviceTask.updatedAt = DateTime.Now;
                _context.ServiceTasks.Update(serviceTask);
            }
            else
            {
                serviceTask.createdAt = DateTime.Now;
                _context.ServiceTasks.Add(serviceTask);
            }

            await _context.SaveChangesAsync();
            return serviceTask.Id;
        }

        public async Task<IEnumerable<ServiceTask>> GetServiceTask()
        {
            return await _context.ServiceTasks.AsNoTracking().ToListAsync();
        }

        public async Task<ServiceTask> GetServiceTaskById(int id)
        {
            return await _context.ServiceTasks.FindAsync(id);
        }

        public async Task<bool> DeleteServiceTaskById(int id)
        {
            _context.ServiceTasks.Remove(_context.ServiceTasks.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region VehicleServiceIssue
        public async Task<int> SaveVehicleServiceIssue(VehicleServiceIssue vehicleServiceIssue)
        {
            if (vehicleServiceIssue.Id != 0)
            {
                vehicleServiceIssue.updatedBy = vehicleServiceIssue.createdBy;
                vehicleServiceIssue.updatedAt = DateTime.Now;
                _context.VehicleServiceIssues.Update(vehicleServiceIssue);
            }
            else
            {
                vehicleServiceIssue.createdAt = DateTime.Now;
                _context.VehicleServiceIssues.Add(vehicleServiceIssue);
            }

            await _context.SaveChangesAsync();
            return vehicleServiceIssue.Id;
        }

        public async Task<IEnumerable<VehicleServiceIssue>> GetVehicleServiceIssue()
        {
            return await _context.VehicleServiceIssues.Include(x=>x.vehicleInformation).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<VehicleServiceIssue>> GetVehicleServiceIssueByVehicleId(int vid)
        {
            return await _context.VehicleServiceIssues.Include(x => x.vehicleInformation).AsNoTracking().Where(x=>x.vehicleInformationId == vid).ToListAsync();
        }

        public async Task<VehicleServiceIssue> GetVehicleServiceIssueById(int id)
        {
            return await _context.VehicleServiceIssues.Where(x => x.Id == id).Include(x=>x.vehicleInformation).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteVehicleServiceIssuesById(int id)
        {
            _context.VehicleServiceIssues.Remove(_context.VehicleServiceIssues.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Vehicle Service Reminder
        public async Task<int> SaveVehicleServiceReminder(VehicleServiceReminder vehicleServiceReminder)
        {
            if (vehicleServiceReminder.Id != 0)
            {
                vehicleServiceReminder.updatedBy = vehicleServiceReminder.createdBy;
                vehicleServiceReminder.updatedAt = DateTime.Now;
                _context.VehicleServiceReminders.Update(vehicleServiceReminder);
            }
            else
            {
                vehicleServiceReminder.createdAt = DateTime.Now;
                _context.VehicleServiceReminders.Add(vehicleServiceReminder);
            }

            await _context.SaveChangesAsync();
            return vehicleServiceReminder.Id;
        }

        public async Task<IEnumerable<VehicleServiceReminder>> GetVehicleServiceReminder()
        {
            return await _context.VehicleServiceReminders.Include(a=>a.vehicleInformation).Include(a => a.spareParts).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<VehicleServiceReminder>> GetVehicleServiceReminderByVehicleId( int VehicleId)
        {
            return await _context.VehicleServiceReminders.Include(a => a.vehicleInformation).Include(a => a.spareParts).AsNoTracking().Where(x=>x.vehicleInformationId == VehicleId).ToListAsync();
        }

        public async Task<VehicleServiceReminder> GetVehicleServiceReminderById(int id)
        {
            return await _context.VehicleServiceReminders.FindAsync(id);
        }

        public async Task<bool> DeleteVehicleServiceReminderById(int id)
        {
            _context.VehicleServiceReminders.Remove(_context.VehicleServiceReminders.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Reminder Schedule
        public async Task<int> SaveReminderSchedule(ReminderSchedule reminderSchedule)
        {
            if (reminderSchedule.Id != 0)
            {
                reminderSchedule.updatedBy = reminderSchedule.createdBy;
                reminderSchedule.updatedAt = DateTime.Now;
                _context.ReminderSchedules.Update(reminderSchedule);
            }
            else
            {
                reminderSchedule.createdAt = DateTime.Now;
                _context.ReminderSchedules.Add(reminderSchedule);
            }

            await _context.SaveChangesAsync();
            return reminderSchedule.Id;
        }

        public async Task<IEnumerable<ReminderSchedule>> GetReminderScheduleByServiceId(int serviceId)
        {
            return await _context.ReminderSchedules.Include(a => a.vehicleServiceReminder).Where(a => a.vehicleServiceReminderId == serviceId).AsNoTracking().ToListAsync();
        }

        public async Task<ReminderSchedule> GetReminderScheduleById(int id)
        {
            return await _context.ReminderSchedules.FindAsync(id);
        }

        public async Task<bool> DeleteReminderScheduleById(int id)
        {
            _context.ReminderSchedules.Remove(_context.ReminderSchedules.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteReminderScheduleByServiceId(int serviceId)
        {
            _context.ReminderSchedules.RemoveRange(_context.ReminderSchedules.Where(x => x.vehicleServiceReminderId == serviceId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region VehicleServiceIssueComment
        public async Task<int> SaveVehicleServiceIssueComment(VehicleServiceIssueComment vehicleServiceIssueComment)
        {
            if (vehicleServiceIssueComment.Id != 0)
            {
                vehicleServiceIssueComment.updatedBy = vehicleServiceIssueComment.createdBy;
                vehicleServiceIssueComment.updatedAt = DateTime.Now;
                _context.vehicleServiceIssueComments.Update(vehicleServiceIssueComment);
            }
            else
            {
                vehicleServiceIssueComment.createdAt = DateTime.Now;
                _context.vehicleServiceIssueComments.Add(vehicleServiceIssueComment);
            }

            await _context.SaveChangesAsync();
            return vehicleServiceIssueComment.Id;
        }

        public async Task<IEnumerable<VehicleServiceIssueComment>> GetVehicleServiceIssueComment()
        {
            return await _context.vehicleServiceIssueComments.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<VehicleServiceIssueComment>> GetVehicleServiceIssueCommentByIssueId(int serviceId)
        {
            return await _context.vehicleServiceIssueComments.AsNoTracking().Where(x => x.vehicleServiceIssueId == serviceId).ToListAsync();
        }

        public async Task<VehicleServiceIssueComment> GetVehicleServiceIssueCommentById(int id)
        {
            return await _context.vehicleServiceIssueComments.FindAsync(id);
        }

        public async Task<bool> DeleteVehicleServiceIssueCommentById(int id)
        {
            _context.vehicleServiceIssueComments.Remove(_context.vehicleServiceIssueComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region Inspection
        #region Inspection Master
        public async Task<int> SaveinspectionMaster(InspectionMaster inspectionMaster)
        {
            if (inspectionMaster.Id != 0)
            {
                inspectionMaster.updatedBy = inspectionMaster.createdBy;
                inspectionMaster.createdAt = inspectionMaster.createdAt;
                inspectionMaster.updatedAt = DateTime.Now;
                _context.InspectionMasters.Update(inspectionMaster);
            }
            else
            {
                inspectionMaster.createdAt = DateTime.Now;
                _context.InspectionMasters.Add(inspectionMaster);
            }

            await _context.SaveChangesAsync();
            return inspectionMaster.Id;
        }

        public async Task<IEnumerable<InspectionMaster>> GetinspectionMaster()
        {
            return await _context.InspectionMasters.Include(x=>x.vehicleInformation).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<InspectionMaster>> GetinspectionMasterByvehicleId(int serviceId)
        {
            return await _context.InspectionMasters.AsNoTracking().Where(x => x.vehicleInformationId == serviceId).ToListAsync();
        }

        public async Task<InspectionMaster> GetinspectionMasterById(int id)
        {
            return await _context.InspectionMasters.Where(x => x.Id == id).Include(x=>x.vehicleInformation).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteinspectionMasterById(int id)
        {
            _context.InspectionMasters.Remove(_context.InspectionMasters.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region Inspection Detail
        public async Task<int> SaveinspectionDetail(InspectionDetail inspectionDetail)
        {
            if (inspectionDetail.Id != 0)
            {
                inspectionDetail.updatedBy = inspectionDetail.createdBy;
                inspectionDetail.updatedAt = DateTime.Now;
                _context.InspectionDetails.Update(inspectionDetail);
            }
            else
            {
                inspectionDetail.createdAt = DateTime.Now;
                _context.InspectionDetails.Add(inspectionDetail);
            }

            await _context.SaveChangesAsync();
            return inspectionDetail.Id;
        }

        public async Task<IEnumerable<InspectionDetail>> GetinspectionDetail()
        {
            return await _context.InspectionDetails.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<InspectionDetail>> GetinspectionDetailByMasterId(int serviceId)
        {
            return await _context.InspectionDetails.AsNoTracking().Where(x => x.inspectionMasterId == serviceId).Include(x=>x.inspectionCheckListCategory).ToListAsync();
        }

        public async Task<InspectionDetail> GetinspectionDetailById(int id)
        {
            return await _context.InspectionDetails.FindAsync(id);
        }

        public async Task<bool> DeleteinspectionDetailById(int id)
        {
            _context.InspectionDetails.Remove(_context.InspectionDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeleteInspectionDetailBymasterId(int fuelId)
        {
            _context.InspectionDetails.RemoveRange(_context.InspectionDetails.Where(x => x.inspectionMasterId == fuelId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region Inspection Category
        public async Task<int> SaveinspectionCheckLIstCategory(InspectionCheckLIstCategory inspectionCheckLIstCategory)
        {
            if (inspectionCheckLIstCategory.Id != 0)
            {
                inspectionCheckLIstCategory.updatedBy = inspectionCheckLIstCategory.createdBy;
                inspectionCheckLIstCategory.updatedAt = DateTime.Now;
                _context.InspectionCheckLIstCategories.Update(inspectionCheckLIstCategory);
            }
            else
            {
                inspectionCheckLIstCategory.createdAt = DateTime.Now;
                _context.InspectionCheckLIstCategories.Add(inspectionCheckLIstCategory);
            }

            await _context.SaveChangesAsync();
            return inspectionCheckLIstCategory.Id;
        }

        public async Task<IEnumerable<InspectionCheckLIstCategory>> GetInspectionCheckLIstCategory()
        {
            return await _context.InspectionCheckLIstCategories.AsNoTracking().ToListAsync();
        }
       
        public async Task<InspectionCheckLIstCategory> GetInspectionCheckLIstCategoryById(int id)
        {
            return await _context.InspectionCheckLIstCategories.FindAsync(id);
        }

        public async Task<bool> DeleteInspectionCheckLIstCategoriesById(int id)
        {
            _context.InspectionCheckLIstCategories.Remove(_context.InspectionCheckLIstCategories.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #endregion

        #region maintenance
        #region maintenance master
        public async Task<int> SavevehicleMaintenanceLimit(VehicleMaintenanceLimit vehicleMaintenanceLimit)
        {
            if (vehicleMaintenanceLimit.Id != 0)
            {
                vehicleMaintenanceLimit.updatedBy = vehicleMaintenanceLimit.createdBy;
                vehicleMaintenanceLimit.createdAt = vehicleMaintenanceLimit.createdAt;
                vehicleMaintenanceLimit.updatedAt = DateTime.Now;
                _context.VehicleMaintenanceLimits.Update(vehicleMaintenanceLimit);
            }
            else
            {
                vehicleMaintenanceLimit.createdAt = DateTime.Now;
                _context.VehicleMaintenanceLimits.Add(vehicleMaintenanceLimit);
            }

            await _context.SaveChangesAsync();
            return vehicleMaintenanceLimit.Id;
        }

        public async Task<IEnumerable<VehicleMaintenanceLimit>> GetvehicleMaintenanceLimit()
        {
            return await _context.VehicleMaintenanceLimits.Include(x => x.vehicleInformation).AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<VehicleMaintenanceLimit>> GetvehicleMaintenanceLimitByvehicleId(int serviceId)
        {
            return await _context.VehicleMaintenanceLimits.AsNoTracking().Where(x => x.vehicleInformationId == serviceId).Include(x=>x.vehicleInformation).ToListAsync();
        }

        public async Task<VehicleMaintenanceLimit> GetvehicleMaintenanceLimitById(int id)
        {
            return await _context.VehicleMaintenanceLimits.Where(x => x.Id == id).Include(x => x.vehicleInformation).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<bool> DeletevehicleMaintenanceLimitById(int id)
        {
            _context.VehicleMaintenanceLimits.Remove(_context.VehicleMaintenanceLimits.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region maintenance Detail
        public async Task<int> SavevehicleMaintenanceLimitDetail(VehicleMaintenanceLimitDetail vehicleMaintenanceLimitDetail)
        {
            if (vehicleMaintenanceLimitDetail.Id != 0)
            {
                vehicleMaintenanceLimitDetail.updatedBy = vehicleMaintenanceLimitDetail.createdBy;
                vehicleMaintenanceLimitDetail.updatedAt = DateTime.Now;
                _context.VehicleMaintenanceLimitDetails.Update(vehicleMaintenanceLimitDetail);
            }
            else
            {
                vehicleMaintenanceLimitDetail.createdAt = DateTime.Now;
                _context.VehicleMaintenanceLimitDetails.Add(vehicleMaintenanceLimitDetail);
            }

            await _context.SaveChangesAsync();
            return vehicleMaintenanceLimitDetail.Id;
        }

        public async Task<IEnumerable<VehicleMaintenanceLimitDetail>> GetvehicleMaintenanceLimitDetail()
        {
            return await _context.VehicleMaintenanceLimitDetails.AsNoTracking().ToListAsync();
        }
        public async Task<IEnumerable<VehicleMaintenanceLimitDetail>> GetvehicleMaintenanceLimitDetailByMasterId(int serviceId)
        {
            return await _context.VehicleMaintenanceLimitDetails.AsNoTracking().Where(x => x.vehicleMaintenanceLimitId == serviceId).Include(x=>x.fuelType).Include(x => x.vehicleMaintenanceLimit).Include(x=>x.limitPeriodType).Include(x=>x.limitAmountType).ToListAsync();
        }

        public async Task<VehicleMaintenanceLimitDetail> GetvehicleMaintenanceLimitDetailById(int id)
        {
            return await _context.VehicleMaintenanceLimitDetails.FindAsync(id);
        }

        public async Task<bool> DeletevehicleMaintenanceLimitDetailById(int id)
        {
            _context.VehicleMaintenanceLimitDetails.Remove(_context.VehicleMaintenanceLimitDetails.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        public async Task<bool> DeletevehicleMaintenanceLimitDetailBymasterId(int fuelId)
        {
            _context.VehicleMaintenanceLimitDetails.RemoveRange(_context.VehicleMaintenanceLimitDetails.Where(x => x.vehicleMaintenanceLimitId == fuelId));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region limit Amount Type 
        public async Task<int> SavelimitAmountType(LimitAmountType limitAmountType)
        {
            if (limitAmountType.Id != 0)
            {
                limitAmountType.updatedBy = limitAmountType.createdBy;
                limitAmountType.updatedAt = DateTime.Now;
                _context.LimitAmountTypes.Update(limitAmountType);
            }
            else
            {
                limitAmountType.createdAt = DateTime.Now;
                _context.LimitAmountTypes.Add(limitAmountType);
            }

            await _context.SaveChangesAsync();
            return limitAmountType.Id;
        }

        public async Task<IEnumerable<LimitAmountType>> GetlimitAmountType()
        {
            return await _context.LimitAmountTypes.AsNoTracking().ToListAsync();
        }

        public async Task<LimitAmountType> GetlimitAmountTypeById(int id)
        {
            return await _context.LimitAmountTypes.FindAsync(id);
        }

        public async Task<bool> DeletelimitAmountTypeById(int id)
        {
            _context.LimitAmountTypes.Remove(_context.LimitAmountTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #region limit Period Type 
        public async Task<int> SavelimitPeriodType(LimitPeriodType limitPeriodType)
        {
            if (limitPeriodType.Id != 0)
            {
                limitPeriodType.updatedBy = limitPeriodType.createdBy;
                limitPeriodType.updatedAt = DateTime.Now;
                _context.LimitPeriodTypes.Update(limitPeriodType);
            }
            else
            {
                limitPeriodType.createdAt = DateTime.Now;
                _context.LimitPeriodTypes.Add(limitPeriodType);
            }

            await _context.SaveChangesAsync();
            return limitPeriodType.Id;
        }

        public async Task<IEnumerable<LimitPeriodType>> GetlimitPeriodType()
        {
            return await _context.LimitPeriodTypes.AsNoTracking().ToListAsync();
        }

        public async Task<LimitPeriodType> GetlimitPeriodTypeById(int id)
        {
            return await _context.LimitPeriodTypes.FindAsync(id);
        }

        public async Task<bool> DeletelimitPeriodTypeById(int id)
        {
            _context.LimitPeriodTypes.Remove(_context.LimitPeriodTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
        #endregion

        #region VehicleRenewalReminder
        public async Task<int> SaveVehicleRenewalReminder(VehicleRenewalReminder  vehicleRenewalReminder)
        {
            if (vehicleRenewalReminder.Id != 0)
            {
                vehicleRenewalReminder.updatedBy = vehicleRenewalReminder.createdBy;
                vehicleRenewalReminder.updatedAt = DateTime.Now;
                _context.VehicleRenewalReminders.Update(vehicleRenewalReminder);
            }
            else
            {
                vehicleRenewalReminder.createdAt = DateTime.Now;
                _context.VehicleRenewalReminders.Add(vehicleRenewalReminder);
            }

            await _context.SaveChangesAsync();
            return vehicleRenewalReminder.Id;
        }

        public async Task<IEnumerable<VehicleRenewalReminder>> GetVehicleRenewalReminder()
        {
            return await _context.VehicleRenewalReminders.AsNoTracking().Include(x => x.vehicleInformation).Include(x => x.renewalType).ToListAsync();
        }
        public async Task<IEnumerable<VehicleRenewalReminder>> GetVehicleRenewalReminderById(int Id)
        {
            return await _context.VehicleRenewalReminders.AsNoTracking().Include(x => x.vehicleInformation).Include(x => x.renewalType).Where(x => x.Id == Id).ToListAsync();
        }

       
        public async Task<bool> DeleteVehicleRenewalReminderById(int id)
        {
            _context.VehicleRenewalReminders.Remove(_context.VehicleRenewalReminders.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region RenewalType
        public async Task<int> SaveRenewalType(RenewalType  renewalType)
        {
            if (renewalType.Id != 0)
            {
                renewalType.updatedBy = renewalType.createdBy;
                renewalType.updatedAt = DateTime.Now;
                _context.RenewalTypes.Update(renewalType);
            }
            else
            {
                renewalType.createdAt = DateTime.Now;
                _context.RenewalTypes.Add(renewalType);
            }

            await _context.SaveChangesAsync();
            return renewalType.Id;
        }

        public async Task<IEnumerable<RenewalType>> GetRenewalType()
        {
            return await _context.RenewalTypes.AsNoTracking().ToListAsync();
        }
       
        public async Task<bool> DeleteRenewalTypeById(int id)
        {
            _context.RenewalTypes.Remove(_context.RenewalTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

        #region ContactRenewalReminder
        public async Task<int> SaveContactRenewalReminder(ContactRenewalReminder contactRenewalReminder)
        {
            if (contactRenewalReminder.Id != 0)
            {
                contactRenewalReminder.updatedBy = contactRenewalReminder.createdBy;
                contactRenewalReminder.updatedAt = DateTime.Now;
                _context.ContactRenewalReminders.Update(contactRenewalReminder);
            }
            else
            {
                contactRenewalReminder.createdAt = DateTime.Now;
                _context.ContactRenewalReminders.Add(contactRenewalReminder);
            }

            await _context.SaveChangesAsync();
            return contactRenewalReminder.Id;
        }

        public async Task<IEnumerable<ContactRenewalReminder>> GetContactRenewalReminder()
        {
            return await _context.ContactRenewalReminders.AsNoTracking().Include(x => x.supplier).Include(x => x.renewalType).ToListAsync();
        }
        public async Task<IEnumerable<ContactRenewalReminder>> GetContactRenewalReminderById(int Id)
        {
            return await _context.ContactRenewalReminders.AsNoTracking().Include(x => x.supplier).Include(x => x.renewalType).Where(x => x.Id == Id).ToListAsync();
        }
        public async Task<bool> DeleteContactRenewalReminderById(int id)
        {
            _context.ContactRenewalReminders.Remove(_context.ContactRenewalReminders.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        #endregion

    }
}
