using OPUSERP.Data.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Data.Member
{
    [Table("MemberPublication", Schema = "Club")]
    public class MemberPublication : Base
    {
        //Foreign Reliation
        public int employeeId { get; set; }
        public MemberInfo employee { get; set; }

        public string publicationName { get; set; }

        public string publicationNubmer { get; set; }

        public string publicationType { get; set; }

        public string publicationPlace { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? publicationDate { get; set; }
    }
}
