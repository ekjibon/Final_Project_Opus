using OPUSERP.CLUB.Services.Gallery.Interface;
using Microsoft.EntityFrameworkCore;
using OPUSERP.CLUB.Data.Gallery;
using OPUSERP.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Gallery
{
    public class GalleryContentService: IGalleryContentService
    {
        private readonly ERPDbContext _context;

        public GalleryContentService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveGalleryContent(GalleryContent galleryContent)
        {
            if (galleryContent.Id != 0)
                _context.galleryContents.Update(galleryContent);
            else
                _context.galleryContents.Add(galleryContent);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GalleryContent>> GetGalleryContent()
        {
            return await _context.galleryContents.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GalleryContent>> GetGalleryContentByType(int type)
        {
            return await _context.galleryContents.Where(x=>x.galleryTitle.type == type).Include(x => x.galleryTitle).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GalleryContent>> GetGalleryContentByTitleId(int titleId)
        {
            return await _context.galleryContents.Where(x=>x.galleryTitleId == titleId).Include(x => x.galleryTitle).AsNoTracking().ToListAsync();
        }

        public async Task<GalleryContent> GetGalleryContentById(int id)
        {
            return await _context.galleryContents.FindAsync(id);
        }

        public async Task<bool> DeleteGalleryContentById(int id)
        {
            _context.galleryContents.Remove(_context.galleryContents.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
