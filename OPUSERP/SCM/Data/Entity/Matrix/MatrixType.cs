using System.ComponentModel.DataAnnotations.Schema;

namespace OPUSERP.Data.Entity.Matrix
{
    [Table("MatrixType", Schema = "SCM")]
    public class MatrixType:Base
    {
        public string matrixTypeName { get; set; }
        public int? shortOrder { get; set; }
    }
}
