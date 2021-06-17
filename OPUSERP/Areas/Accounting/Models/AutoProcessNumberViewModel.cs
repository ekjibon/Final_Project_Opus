using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Areas.Accounting.Models
{
    [NotMapped]
    public class AutoProcessNumberViewModel
    {
        public string ProcessNo { get; set; }
    }
}
