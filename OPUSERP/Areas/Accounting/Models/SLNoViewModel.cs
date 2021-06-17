using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Areas.Accounting.Models
{
    [NotMapped]
    public class AutoNumberModel
    {
        public string autoNumber { get; set; }
    }
}
