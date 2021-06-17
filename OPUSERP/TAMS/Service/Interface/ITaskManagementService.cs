using OPUSERP.Areas.TAMS.Models;
using OPUSERP.TAMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Service.Interface
{
   public interface ITaskManagementService
    {
        #region TaskProject
        Task<int> SaveTaskProject(TaskProject taskProject);
        Task<IEnumerable<TaskProject>> GetAllTaskProject();
        Task<TaskProject> GetTaskProjectById(int id);
        Task<bool> DeleteTaskProjectById(int id);

        Task<ProjectViewModel> GetAllProjectViewModel(int id);
        #endregion

        #region TaskSection
        Task<int> SaveTaskSection(TaskSection taskProject);
        Task<IEnumerable<TaskSection>> GetAllTaskSection();
        Task<IEnumerable<TaskSection>> GetAllTaskSectionByProjectId(int projectId);
        Task<TaskSection> GetTaskSectionById(int id);
        Task<bool> DeleteTaskSectionById(int id);
        #endregion

        #region TaskInformation
        Task<int> SaveTaskInformation(TaskInformation taskProject);
        Task<IEnumerable<TaskInformation>> GetAllTaskInformation();
        Task<IEnumerable<TaskInformation>> GetAllTaskInformationBySectorId(int sectorId);
        Task<TaskInformation> GetTaskInformationById(int id);
        Task<bool> DeleteTaskInformationById(int id);
        Task<IEnumerable<System.Object>> GetTaskInformationList();
        Task<SingleTaskViewModel> GetSingleTaskViewModelById(int id);
        Task<IEnumerable<TaskInformationHistory>> GetAllTaskProjectByEmp(int id);
        Task<IEnumerable<TaskInformationHistory>> GetAllTaskProjectByEmpFollowup(int id);
        #endregion

        #region TaskFollower
        Task<int> SaveTaskFollower(TaskFollower taskProject);
        Task<IEnumerable<TaskFollower>> GetAllTaskFollower();
        Task<IEnumerable<TaskFollower>> GetAllTaskFollowerByTaskId(int taskIs);
        Task<TaskFollower> GetTaskFollowerById(int id);
        Task<bool> DeleteTaskFollowerById(int id);
        #endregion


        #region TaskTag
        Task<int> SaveTaskTag(TaskTag taskProject);
        Task<IEnumerable<TaskTag>> GetAllTaskTag();
        Task<IEnumerable<TaskTag>> GetAllTaskTagByTaskId(int taskIs);
        Task<TaskTag> GetTaskTagById(int id);
        Task<bool> DeleteTaskTagById(int id);
        #endregion

        #region TaskAttachment
        Task<int> SaveTaskAttachment(TaskAttachment taskProject);
        Task<IEnumerable<TaskAttachment>> GetAllTaskAttachment();
        Task<IEnumerable<TaskAttachment>> GetAllTaskAttachmentByTaskId(int taskIs);
        Task<TaskAttachment> GetTaskAttachmentById(int id);
        Task<bool> DeleteTaskAttachmentById(int id);
        #endregion

        #region TaskComment
        Task<int> SaveTaskComment(TaskComment taskProject);
        Task<IEnumerable<TaskComment>> GetAllTaskComment();
        Task<IEnumerable<TaskComment>> GetAllTaskCommentByTaskId(int taskIs);
        Task<TaskComment> GetTaskCommentById(int id);
        Task<bool> DeleteTaskCommentById(int id);
        #endregion


        #region TaskSubtask
        Task<int> SaveTaskSubtask(TaskSubtask taskProject);
        Task<IEnumerable<TaskSubtask>> GetAllTaskSubtask();
        Task<IEnumerable<TaskSubtask>> GetAllTaskSubtaskByTaskId(int taskIs);
        Task<TaskSubtask> GetTaskSubtaskById(int id);
        Task<bool> DeleteTaskSubtaskById(int id);
        void UpdateSubtask(int Id, int Status);
        #endregion

        #region TaskHours
        Task<int> SaveTaskHours(TaskHours taskProject);
        Task<IEnumerable<TaskHours>> GetAllTaskHours();
        Task<IEnumerable<TaskHours>> GetAllTaskHoursByTaskId(int taskIs);
        Task<TaskHours> GetTaskHoursById(int id);
        Task<bool> DeleteTaskHoursById(int id);
        #endregion

        #region TaskFollowUp
        Task<int> SaveTaskFollowUp(TaskFollowUp taskProject);
        Task<IEnumerable<TaskFollowUp>> GetAllTaskFollowUp();
        Task<IEnumerable<TaskFollowUp>> GetAllTaskFollowUpByTaskId(int taskIs);
        Task<TaskFollowUp> GetTaskFollowUpById(int id);
        Task<bool> DeleteTaskFollowUpById(int id);
        #endregion

        #region TaskStatusList
        Task<int> SaveTaskStatusList(TaskStatusList taskProject);
        Task<IEnumerable<TaskStatusList>> GetAllTaskStatusList();
        Task<IEnumerable<TaskStatusList>> GetAllTaskStatusListByTaskId(int taskIs);
        Task<TaskStatusList> GetTaskStatusListById(int id);
        Task<bool> DeleteTaskStatusListById(int id);
        #endregion



    }
}
