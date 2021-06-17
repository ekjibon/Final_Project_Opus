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
    public class GalleryTitleService: IGalleryTitleService
    {
        private readonly ERPDbContext _context;

        public GalleryTitleService(ERPDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveGalleryTitle(GalleryTitle galleryTitle)
        {
            if (galleryTitle.Id != 0)
                _context.galleryTitles.Update(galleryTitle);
            else
                _context.galleryTitles.Add(galleryTitle);
            return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<GalleryTitle>> GetGalleryTitle()
        {
            return await _context.galleryTitles.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<GalleryTitle>> GetGalleryTitleByType(int type)
        {
            return await _context.galleryTitles.Where(x=>x.type == type).AsNoTracking().ToListAsync();
        }

        public async Task<GalleryTitle> GetGalleryTitleById(int id)
        {
            return await _context.galleryTitles.FindAsync(id);
        }

        public async Task<bool> DeleteGalleryTitleById(int id)
        {
            _context.galleryTitles.Remove(_context.galleryTitles.Find(id));
            return 1 == await _context.SaveChangesAsync();
        }
    }
}
