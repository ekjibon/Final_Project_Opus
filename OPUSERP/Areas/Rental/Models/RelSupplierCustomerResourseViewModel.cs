namespace OPUSERP.Areas.Rental.Models
{
    public class RelSupplierCustomerResourseViewModel
    {        
        public int relId { get; set; }
        public int? leadId { get; set; }
        public string customerName { get; set; }
        public string presentAddress { get; set; }
        public string permanentAddress { get; set; }
        public string mobile { get; set; }        
    }
}
