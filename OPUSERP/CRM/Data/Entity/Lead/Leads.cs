using OPUSERP.CRM.Data.Entity.MasterData;
using OPUSERP.Data.Entity;
using OPUSERP.Data.Entity.Master;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.CRM.Data.Entity.Lead
{
    [Table("Leads", Schema = "CRM")]
    public class Leads : Base
    {
        public int? sourceId { get; set; }
        public Source source { get; set; }

        public int? sectorId { get; set; }
        public Sector sector { get; set; }

        public int? companyGroupId { get; set; }
        public CompanyGroup companyGroup { get; set; }

        public int? ownerShipTypeId { get; set; }
        public OwnerShipType ownerShipType { get; set; }
        [MaxLength(350)]
        public string leadOwner { get; set; }
        [MaxLength(250)]
        public string leadName { get; set; }
        [MaxLength(100)]
        public string leadShortName { get; set; }
        [MaxLength(50)]
        public string leadNumber { get; set; }     
        public string sourceDescription { get; set; }
        [DefaultValue(0)]
        public int? isClient { get; set; } //0=No, 1=yes

        public int? totalemployee  { get; set; }

        public string companyName { get; set; }

        public int? isPersonal  { get; set; }

        public int? fITypeId { get; set; }
        public FIType fIType { get; set; }

        public int? leadProgressStatusId { get; set; }
        public LeadProgressStatus leadProgressStatus { get; set; }
        public string remarks { get; set; }
        
    }
}
