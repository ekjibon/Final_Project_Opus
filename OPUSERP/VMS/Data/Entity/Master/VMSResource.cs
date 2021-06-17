using OPUSERP.Data.Entity;
using OPUSERP.HRPMS.Data.Entity.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.VMS.Data.Entity.Master
{
    [Table("VMSResource", Schema = "VMS")]
    public class VMSResource:Base
    {

        [MaxLength(50)]
        public string resourceCode { get; set; }

        [MaxLength(100)]
        public string nationalID { get; set; }

        [MaxLength(100)]
        public string birthIdentificationNo { get; set; }

        public string nameEnglish { get; set; }

        public string nameBangla { get; set; }

        public string motherNameEnglish { get; set; }

        public string motherNameBangla { get; set; }

        public string fatherNameEnglish { get; set; }

        public string fatherNameBangla { get; set; }

        public string nationality { get; set; }

        public string disability { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? dateOfBirth { get; set; }

        public string gender { get; set; }

        public string birthPlace { get; set; }

        public string maritalStatus { get; set; }

        public int? religionId { get; set; }
        public Religion religion { get; set; }

        public int? resourceTypeId { get; set; }
        public VMSResourceType resourceType { get; set; }

        public string bloodGroup { get; set; }

        public bool freedomFighter { get; set; }

        public string freedomFighterNo { get; set; }

        public string telephoneResidence { get; set; }

        public string pabx { get; set; }

        public string emailAddress { get; set; }

        public string emailAddressPersonal { get; set; } // Next generated not planned

        [MaxLength(50)]
        public string mobileNumberPersonal { get; set; }

        public string specialSkill { get; set; }

        public string homeDistrict { get; set; }


        public string phone { get; set; }

        public string mobile { get; set; }

        public string email { get; set; }
        public string fax { get; set; }


        //public string url { get; set; }

        public int departmentID { get; set; }
        public Department department { get; set; }

        public int designationID { get; set; }
        public Designation designation { get; set; }

        public string imagePath { get; set; }
    }
}
