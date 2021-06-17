using Microsoft.EntityFrameworkCore;
using OPUSERP.Areas.TAMS.Models;
using OPUSERP.Data;
using OPUSERP.TAMS.Data.Entity;
using OPUSERP.TAMS.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.TAMS.Service
{
    public class TaskManagementService : ITaskManagementService
    {

        private readonly ERPDbContext _context;

        public TaskManagementService(ERPDbContext context)
        {
            _context = context;
        }


        #region TaskProject
        public async Task<int> SaveTaskProject(TaskProject doc)
        {
            if (doc.Id != 0)
                _context.taskProjects.Update(doc);
            else
                _context.taskProjects.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskProject>> GetAllTaskProject()
        {
            return await _context.taskProjects.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskInformationHistory>> GetAllTaskProjectByEmp(int id)
        {
            List<TaskInformation> taskInformation = await _context.taskInformations.Where(x => x.employeeAssignId == id).ToListAsync();
            List<TaskInformationHistory> TaskInformationHistory = new List<TaskInformationHistory>();

            foreach (var taskInfo in taskInformation)
            {
                TaskInformationHistory.Add(new TaskInformationHistory
                {
                    taskInformation = await _context.taskInformations.Where(x => x.Id == taskInfo.Id).Include(x => x.employeeAssign).Include(x => x.employee).AsNoTracking().FirstOrDefaultAsync(),
                    followers = await _context.taskFollowers.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    files = await _context.taskAttachments.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    Tags = await _context.taskTags.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    Comments = await _context.taskComments.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    Following = await _context.taskFollowUps.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    doneSubtusk = _context.taskSubtasks.Where(x => x.taskInformationId == taskInfo.Id && x.status == 1).Count(),
                    unDoneSubtusk = _context.taskSubtasks.Where(x => x.taskInformationId == taskInfo.Id).Count(),
                });
            }

            return TaskInformationHistory;
        }

        public async Task<IEnumerable<TaskInformationHistory>> GetAllTaskProjectByEmpFollowup(int id)
        {
            List<TaskInformation> taskInformation = await _context.taskInformations.Where(x => x.employeeId == id).ToListAsync();
            List<TaskInformationHistory> TaskInformationHistory = new List<TaskInformationHistory>();

            foreach (var taskInfo in taskInformation)
            {
                TaskInformationHistory.Add(new TaskInformationHistory
                {
                    taskInformation = await _context.taskInformations.Where(x => x.Id == taskInfo.Id).Include(x => x.employeeAssign).AsNoTracking().FirstOrDefaultAsync(),
                    followers = await _context.taskFollowers.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    files = await _context.taskAttachments.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    Tags = await _context.taskTags.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    Comments = await _context.taskComments.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    Following = await _context.taskFollowUps.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                    doneSubtusk = _context.taskSubtasks.Where(x => x.taskInformationId == taskInfo.Id && x.status == 1).Count(),
                    unDoneSubtusk = _context.taskSubtasks.Where(x => x.taskInformationId == taskInfo.Id).Count(),
                });
            }

            return TaskInformationHistory;
        }

        public async Task<ProjectViewModel> GetAllProjectViewModel(int id)
        {
            var project = await _context.taskProjects.Where(x => x.Id == id).AsNoTracking().FirstOrDefaultAsync();
            List<SectorViewModel> SectorViewModel = new List<SectorViewModel>();
            List<TaskSection> TaskSection = await _context.taskSections.Where(x => x.taskProjectId == id).AsNoTracking().ToListAsync();

            foreach (var data in TaskSection)
            {
                List<TaskInformation> taskInformation = await _context.taskInformations.Where(x => x.taskSectionId == data.Id).ToListAsync();
                List<TaskInformationHistory> TaskInformationHistory = new List<TaskInformationHistory>();


                foreach (var taskInfo in taskInformation)
                {
                    string aspnetId = await _context.employeeInfos.Where(x => x.Id == taskInfo.employeeAssignId).Select(x => x.ApplicationUserId).FirstOrDefaultAsync();
                    var team = await _context.Teams.Where(x => x.aspnetuserId == aspnetId).FirstOrDefaultAsync();
                    int? leader = 0;
                    if (team.teamId != null)
                    {
                        var leaderteam = await _context.Teams.Where(x => x.Id == team.teamId).FirstOrDefaultAsync();
                        leader = await _context.employeeInfos.Where(x => x.ApplicationUserId == leaderteam.aspnetuserId).Select(x => x.Id).FirstOrDefaultAsync();
                    }
                    
                    TaskInformationHistory.Add(new TaskInformationHistory
                    {
                        taskInformation = await _context.taskInformations.Where(x => x.Id == taskInfo.Id).Include(x => x.employeeAssign).AsNoTracking().FirstOrDefaultAsync(),
                        followers = await _context.taskFollowers.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                        files = await _context.taskAttachments.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                        Tags = await _context.taskTags.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                        Comments = await _context.taskComments.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                        Following = await _context.taskFollowUps.Where(x => x.taskInformationId == taskInfo.Id).CountAsync(),
                        doneSubtusk = _context.taskSubtasks.Where(x => x.taskInformationId == taskInfo.Id && x.status == 1).Count(),
                        unDoneSubtusk = _context.taskSubtasks.Where(x => x.taskInformationId == taskInfo.Id).Count(),
                        taskFollowers = await _context.taskFollowers.Where(x => x.taskInformationId == taskInfo.Id).Select(x => x.employeeId).ToListAsync(),
                        LeaderID = leader,
                    });
                }
                SectorViewModel.Add(new SectorViewModel
                {
                    taskSection = await _context.taskSections.Where(x => x.Id == data.Id).AsNoTracking().FirstOrDefaultAsync(),
                    taskInformationHistories = TaskInformationHistory,
                });

            }

            ProjectViewModel projectViewModels = new ProjectViewModel
            {
                taskProject = project,
                sectorViewModels = SectorViewModel,
            };

            return projectViewModels;
        }

        public async Task<TaskProject> GetTaskProjectById(int id)
        {
            return await _context.taskProjects.FindAsync(id);
        }

        public async Task<bool> DeleteTaskProjectById(int id)
        {
            _context.taskProjects.Remove(_context.taskProjects.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


        #region TaskSection
        public async Task<int> SaveTaskSection(TaskSection doc)
        {
            if (doc.Id != 0)
                _context.taskSections.Update(doc);
            else
                _context.taskSections.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskSection>> GetAllTaskSection()
        {
            return await _context.taskSections.Include(x => x.taskProject).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskSection>> GetAllTaskSectionByProjectId(int projectId)
        {
            return await _context.taskSections.Where(x => x.taskProjectId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskSection> GetTaskSectionById(int id)
        {
            return await _context.taskSections.FindAsync(id);
        }

        public async Task<bool> DeleteTaskSectionById(int id)
        {
            _context.taskSections.Remove(_context.taskSections.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


        #region TaskInformation
        public async Task<int> SaveTaskInformation(TaskInformation doc)
        {
            if (doc.Id != 0)
                _context.taskInformations.Update(doc);
            else
                _context.taskInformations.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskInformation>> GetAllTaskInformation()
        {
            return await _context.taskInformations.Include(a => a.problemMaster).Include(a => a.employeeAssign).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskInformation>> GetAllTaskInformationBySectorId(int projectId)
        {
            return await _context.taskInformations.Where(x => x.taskSectionId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<SingleTaskViewModel> GetSingleTaskViewModelById(int id)
        {
            var task = await _context.taskInformations.Include(x => x.taskSection.taskProject).Include(x => x.employeeAssign).Include(x => x.activityCategory).Include(x => x.activitySession).Include(x => x.activityStatus).Where(x => x.Id == id).FirstOrDefaultAsync();

            List<TaskComment> taskComments = await _context.taskComments.Where(x => x.taskInformationId == id).Include(x => x.employee).AsNoTracking().ToListAsync();
            List<TaskCommentWithPhoto> comments = new List<TaskCommentWithPhoto>();
            foreach (var data in taskComments)
            {
                var photo = await _context.photographs.Where(x => x.employeeId == data.employeeId).FirstOrDefaultAsync();
                var url = photo?.url;
                if (url == null)
                {
                    url = "images/cuser.jpg";
                }
                comments.Add(new TaskCommentWithPhoto
                {
                    name = data.employee.nameEnglish,
                    photo = url,
                    comments = data.comments
                });
            }

            SingleTaskViewModel singleTaskViewModel = new SingleTaskViewModel
            {
                taskInformation = task,
                followers = await _context.taskFollowers.Where(x => x.taskInformationId == id).Include(x => x.employee).AsNoTracking().ToListAsync(),
                files = await _context.taskAttachments.Where(x => x.taskInformationId == id).AsNoTracking().ToListAsync(),
                Tags = await _context.taskTags.Where(x => x.taskInformationId == id).AsNoTracking().ToListAsync(),
                taskSubtasks = await _context.taskSubtasks.Where(x => x.taskInformationId == id).AsNoTracking().ToListAsync(),
                taskHours = await _context.taskHours.Where(x => x.taskInformationId == id).AsNoTracking().ToListAsync(),
                taskStatusLists = await _context.taskStatusLists.Where(x => x.taskInformationId == id).AsNoTracking().ToListAsync(),
                Comments = comments,
                doneSubtusk = _context.taskSubtasks.Where(x => x.taskInformationId == id && x.status == 1).Count(),
                unDoneSubtusk = _context.taskSubtasks.Where(x => x.taskInformationId == id && x.status != 1).Count(),
                taskFollowUps = await _context.taskFollowUps.Where(x => x.taskInformationId == id).Include(x => x.activityCategory).Include(x => x.activitySession).Include(x => x.activityStatus).Include(x => x.activityType).AsNoTracking().ToListAsync(),
            };
            return singleTaskViewModel;
        }

        public async Task<TaskInformation> GetTaskInformationById(int id)
        {
            //return await _context.taskInformations.Include(a=>a.employeeAssign).Include(a => a.taskSection).Include(a => a.taskSection.taskProject).Include(a => a.activityCategory).Include(a => a.activitySession).Include(a => a.activityStatus).Include(a => a.activityType).Where(x => x.Id == id).FirstOrDefaultAsync();
            return await _context.taskInformations.Include(a => a.employeeAssign).Include(a => a.taskSection).Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTaskInformationById(int id)
        {
            _context.taskInformations.Remove(_context.taskInformations.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<System.Object>> GetTaskInformationList()
        {
            try
            {
                var result = (from T in _context.taskInformations
                              join P in _context.ProblemMasters on T.problemMasterId equals P.Id into SS
                              from prob in SS.DefaultIfEmpty()
                              join HR in _context.employeeInfos on T.employeeAssignId equals HR.Id
                              select new
                              {
                                  T.Id,
                                  T.problemMasterId,
                                  T.name,
                                  prob.tokenNo,
                                  HR.nameEnglish,
                                  leadId = (from lp in _context.ProblemDetails
                                            join cpw in _context.CustomerProductWarranties on lp.customerProductWarrantyId equals cpw.Id
                                            where prob.Id == lp.problemMasterId
                                            select cpw.leadsId).FirstOrDefault(),
                                  leadName = (from lp in _context.ProblemDetails
                                              join cpw in _context.CustomerProductWarranties on lp.customerProductWarrantyId equals cpw.Id
                                              join ld in _context.Leads on cpw.leadsId equals ld.Id
                                              where prob.Id == lp.problemMasterId
                                              select ld.leadName).FirstOrDefault(),
                                  itemName = (from lp in _context.ProblemDetails
                                              join cpw in _context.CustomerProductWarranties on lp.customerProductWarrantyId equals cpw.Id
                                              join itsp in _context.ItemSpecifications on cpw.itemSpecificationId equals itsp.Id
                                              join itm in _context.Items on itsp.itemId equals itm.Id
                                              where prob.Id == lp.problemMasterId
                                              select itm.itemName).FirstOrDefault(),
                              }).OrderByDescending(x => x.Id)
                       .AsNoTracking().ToListAsync();
                return await result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion


        #region TaskFollower
        public async Task<int> SaveTaskFollower(TaskFollower doc)
        {
            if (doc.Id != 0)
                _context.taskFollowers.Update(doc);
            else
                _context.taskFollowers.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskFollower>> GetAllTaskFollower()
        {
            return await _context.taskFollowers.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskFollower>> GetAllTaskFollowerByTaskId(int projectId)
        {
            return await _context.taskFollowers.Where(x => x.taskInformationId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskFollower> GetTaskFollowerById(int id)
        {
            return await _context.taskFollowers.FindAsync(id);
        }

        public async Task<bool> DeleteTaskFollowerById(int id)
        {
            _context.taskFollowers.Remove(_context.taskFollowers.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion


        #region TaskTag
        public async Task<int> SaveTaskTag(TaskTag doc)
        {
            if (doc.Id != 0)
                _context.taskTags.Update(doc);
            else
                _context.taskTags.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskTag>> GetAllTaskTag()
        {
            return await _context.taskTags.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskTag>> GetAllTaskTagByTaskId(int projectId)
        {
            return await _context.taskTags.Where(x => x.taskInformationId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskTag> GetTaskTagById(int id)
        {
            return await _context.taskTags.FindAsync(id);
        }

        public async Task<bool> DeleteTaskTagById(int id)
        {
            _context.taskTags.Remove(_context.taskTags.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region TaskAttachment
        public async Task<int> SaveTaskAttachment(TaskAttachment doc)
        {
            if (doc.Id != 0)
                _context.taskAttachments.Update(doc);
            else
                _context.taskAttachments.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskAttachment>> GetAllTaskAttachment()
        {
            return await _context.taskAttachments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskAttachment>> GetAllTaskAttachmentByTaskId(int projectId)
        {
            return await _context.taskAttachments.Where(x => x.taskInformationId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskAttachment> GetTaskAttachmentById(int id)
        {
            return await _context.taskAttachments.FindAsync(id);
        }

        public async Task<bool> DeleteTaskAttachmentById(int id)
        {
            _context.taskAttachments.Remove(_context.taskAttachments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region TaskComment
        public async Task<int> SaveTaskComment(TaskComment doc)
        {
            if (doc.Id != 0)
                _context.taskComments.Update(doc);
            else
                _context.taskComments.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskComment>> GetAllTaskComment()
        {
            return await _context.taskComments.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskComment>> GetAllTaskCommentByTaskId(int projectId)
        {
            return await _context.taskComments.Where(x => x.taskInformationId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskComment> GetTaskCommentById(int id)
        {
            return await _context.taskComments.FindAsync(id);
        }

        public async Task<bool> DeleteTaskCommentById(int id)
        {
            _context.taskComments.Remove(_context.taskComments.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region TaskSubtask
        public async Task<int> SaveTaskSubtask(TaskSubtask doc)
        {
            if (doc.Id != 0)
                _context.taskSubtasks.Update(doc);
            else
                _context.taskSubtasks.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskSubtask>> GetAllTaskSubtask()
        {
            return await _context.taskSubtasks.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskSubtask>> GetAllTaskSubtaskByTaskId(int projectId)
        {
            return await _context.taskSubtasks.Where(x => x.taskInformationId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskSubtask> GetTaskSubtaskById(int id)
        {
            return await _context.taskSubtasks.FindAsync(id);
        }

        public async Task<bool> DeleteTaskSubtaskById(int id)
        {
            _context.taskSubtasks.Remove(_context.taskSubtasks.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }

        public void UpdateSubtask(int Id, int Status)
        {
            var user = _context.taskSubtasks.Find(Id);
            user.status = Status;
            user.updatedAt = DateTime.Now;
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        #endregion

        #region TaskHours
        public async Task<int> SaveTaskHours(TaskHours doc)
        {
            if (doc.Id != 0)
                _context.taskHours.Update(doc);
            else
                _context.taskHours.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskHours>> GetAllTaskHours()
        {
            return await _context.taskHours.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskHours>> GetAllTaskHoursByTaskId(int projectId)
        {
            return await _context.taskHours.Where(x => x.taskInformationId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskHours> GetTaskHoursById(int id)
        {
            return await _context.taskHours.FindAsync(id);
        }

        public async Task<bool> DeleteTaskHoursById(int id)
        {
            _context.taskHours.Remove(_context.taskHours.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region TaskFollowUp
        public async Task<int> SaveTaskFollowUp(TaskFollowUp doc)
        {
            if (doc.Id != 0)
                _context.taskFollowUps.Update(doc);
            else
                _context.taskFollowUps.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskFollowUp>> GetAllTaskFollowUp()
        {
            return await _context.taskFollowUps.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskFollowUp>> GetAllTaskFollowUpByTaskId(int projectId)
        {
            return await _context.taskFollowUps.Where(x => x.taskInformationId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskFollowUp> GetTaskFollowUpById(int id)
        {
            return await _context.taskFollowUps.Where(x => x.Id == id).Include(x => x.activityCategory).Include(x => x.activitySession).Include(x => x.activityStatus).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTaskFollowUpById(int id)
        {
            _context.taskFollowUps.Remove(_context.taskFollowUps.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion

        #region TaskStatusList
        public async Task<int> SaveTaskStatusList(TaskStatusList doc)
        {
            if (doc.Id != 0)
                _context.taskStatusLists.Update(doc);
            else
                _context.taskStatusLists.Add(doc);
            await _context.SaveChangesAsync();
            return doc.Id;
        }

        public async Task<IEnumerable<TaskStatusList>> GetAllTaskStatusList()
        {
            return await _context.taskStatusLists.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskStatusList>> GetAllTaskStatusListByTaskId(int projectId)
        {
            return await _context.taskStatusLists.Where(x => x.taskInformationId == projectId).AsNoTracking().ToListAsync();
        }

        public async Task<TaskStatusList> GetTaskStatusListById(int id)
        {
            return await _context.taskStatusLists.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteTaskStatusListById(int id)
        {
            _context.taskStatusLists.Remove(_context.taskStatusLists.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
        #endregion
    }
}
