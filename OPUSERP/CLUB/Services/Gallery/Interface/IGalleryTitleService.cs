using OPUSERP.CLUB.Data.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Gallery.Interface
{
   public interface IGalleryTitleService
    {
        Task<bool> SaveGalleryTitle(GalleryTitle galleryTitle);
        Task<IEnumerable<GalleryTitle>> GetGalleryTitle();
        Task<IEnumerable<GalleryTitle>> GetGalleryTitleByType(int type);
        Task<GalleryTitle> GetGalleryTitleById(int id);
        Task<bool> DeleteGalleryTitleById(int id);
    }
}
