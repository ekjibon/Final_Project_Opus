using Microsoft.EntityFrameworkCore;
using OPUSERP.Data;
using OPUSERP.Programs.Data.Entity;
using OPUSERP.Programs.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.Programs.Service
{
    public class ProgramVideoService: IProgramVideoService
    {
        private readonly ERPDbContext _context;

        public ProgramVideoService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveProgramVideo(ProgramVideo programVideo)
        {
            if (programVideo.Id != 0)
            {
                programVideo.updatedBy = programVideo.createdBy;
                programVideo.updatedAt = DateTime.Now;
                _context.programVideos.Update(programVideo);
            }
            else
            {
                programVideo.createdAt = DateTime.Now;
                _context.programVideos.Add(programVideo);
            }
            await _context.SaveChangesAsync();
            return programVideo.Id;
        }

        public async Task<IEnumerable<ProgramVideo>> GetProgramVideo()
        {
            return await _context.programVideos.AsNoTracking().ToListAsync();
        }

        public async Task<ProgramVideo> GetProgramVideoById(int id)
        {
            return await _context.programVideos.Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<bool> DeleteProgramVideoById(int id)
        {
            _context.programVideos.Remove(_context.programVideos.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
