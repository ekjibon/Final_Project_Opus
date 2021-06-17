using OPUSERP.TAMS.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.TAMS.Models
{
    public class SingleTaskViewModel
    {
        public TaskInformation taskInformation { get; set; }

        public IEnumerable<TaskFollower> followers { get; set; }

        public IEnumerable<TaskTag> Tags { get; set; }

        public IEnumerable<TaskAttachment> files { get; set; }

        public IEnumerable<TaskSubtask> taskSubtasks { get; set; }

        public IEnumerable<TaskHours> taskHours { get; set; }

        public IEnumerable<TaskStatusList> taskStatusLists { get; set; }

        public IEnumerable<TaskFollowUp> taskFollowUps { get; set; }

        public IEnumerable<TaskCommentWithPhoto> Comments { get; set; }

        public int? doneSubtusk { get; set; }

        public int? unDoneSubtusk { get; set; }
    }
}
