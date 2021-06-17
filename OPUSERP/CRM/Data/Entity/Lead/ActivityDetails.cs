using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("ActivityDetails", Schema = "CRM")]
    public class ActivityDetails : Base
    {

        public int activityMasterId { get; set; }
        public ActivityMaster activityMaster { get; set; }

        public int contactsId { get; set; }
        //public Contacts contacts { get; set; }
        [NotMapped]
        public string contactName { get; set; }


    }
}
