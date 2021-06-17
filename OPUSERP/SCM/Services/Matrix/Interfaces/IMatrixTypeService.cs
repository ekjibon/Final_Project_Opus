using OPUSERP.Data.Entity.Matrix;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix.Interfaces
{
    public interface IMatrixTypeService
    {
        Task<int> SaveMatrixType(MatrixType matrixType);
        Task<IEnumerable<MatrixType>> GetMatrixTypeList();
        Task<MatrixType> GetMatrixTypeById(int id);
        Task<bool> DeleteIMatrixTypeById(int id);
    }
}
