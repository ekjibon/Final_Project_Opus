using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Data.Entity.Matrix;
using OPUSERP.SCM.Data;
using OPUSERP.SCM.Services.Matrix.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OPUSERP.SCM.Services.Matrix
{
    public class MatrixTypeService:IMatrixTypeService
    {
        private readonly ERPDbContext _context;

        public MatrixTypeService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveMatrixType(MatrixType matrixType)
        {
            if (matrixType.Id != 0)
            {
                matrixType.updatedBy = matrixType.createdBy;
                matrixType.updatedAt = DateTime.Now;
                _context.MatrixTypes.Update(matrixType);
            }
            else
            {
                matrixType.createdAt = DateTime.Now;
                _context.MatrixTypes.Add(matrixType);
            }
                
             await _context.SaveChangesAsync();
            return matrixType.Id;
        }

        public async Task<IEnumerable<MatrixType>> GetMatrixTypeList()
        {
            return await _context.MatrixTypes.AsNoTracking().ToListAsync();
        }

        public async Task<MatrixType> GetMatrixTypeById(int id)
        {
            return await _context.MatrixTypes.FindAsync(id);
        }

        public async Task<bool> DeleteIMatrixTypeById(int id)
        {
            _context.MatrixTypes.Remove(_context.MatrixTypes.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
