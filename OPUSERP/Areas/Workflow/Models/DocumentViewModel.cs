using Microsoft.AspNetCore.Http;
using OPUSERP.Areas.Auth.Models;
using OPUSERP.VIMS.Data;
using OPUSERP.Workflow.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Areas.Workflow.Models
{
    public class DocumentViewModel
    {
        public string number { get; set; }

        public string subject { get; set; }

        public string content { get; set; }
        
        public IFormFile[] attachments { get; set; }

        public int? employeeId { get; set; }

        public int?[] ids { get; set; }

        public int docId { get; set; }

        public string[] fileTitle { get; set; }

        public string[] fileSubject { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        public int? isClose { get; set; }

        public int? isInitial { get; set; }

        public int? ReviewId { get; set; }

        public string noteType { get; set; }

        public Doc docs { get; set; }
        public DocWithSgnature doc { get; set; }
        public IEnumerable<DocAttachment> docAttachment { get; set; }
        public IEnumerable<ReviewRoute> reviewRoutes { get; set; }
        public IEnumerable<DocRoute>  docRoutes { get; set; }
        public DocRoute  docCheck{ get; set; }
        public IEnumerable<RouteWithSignatureforDoc>  docRoute { get; set; }

        public IEnumerable<Doc> createdNote { get; set; }
        public IEnumerable<DocWithReviewIdModel> docWithReviewIdModels { get; set; }
    }
}
