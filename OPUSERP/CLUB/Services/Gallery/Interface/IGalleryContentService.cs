using OPUSERP.CLUB.Data.Gallery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OPUSERP.CLUB.Services.Gallery.Interface
{
    public interface IGalleryContentService
    {
        Task<bool> SaveGalleryContent(GalleryContent galleryContent);
        Task<IEnumerable<GalleryContent>> GetGalleryContent();
        Task<IEnumerable<GalleryContent>> GetGalleryContentByType(int type);
        Task<IEnumerable<GalleryContent>> GetGalleryContentByTitleId(int titleId);
        Task<GalleryContent> GetGalleryContentById(int id);
        Task<bool> DeleteGalleryContentById(int id);
    }
}
