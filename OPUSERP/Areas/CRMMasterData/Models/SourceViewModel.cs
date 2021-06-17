using OPUSERP.CRM.Data.Entity.MasterData;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OPUSERP.Areas.CRMMasterData.Models
{
    public class SourceViewModel
    {
        public int sourceId { get; set; }
        public string sourceName { get; set; }

        public IEnumerable<Source> sources { get; set; }
    }
}
