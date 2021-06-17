using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OPUSERP.Data;

namespace OPUSERP.Areas.Shagotom.Controllers
{
    [Area("Shagotom")]
    public class CameraController : Controller
    {
        private readonly ERPDbContext _context;
        private readonly IHostingEnvironment _environment;
        public CameraController(IHostingEnvironment hostingEnvironment, ERPDbContext context)
        {
            _environment = hostingEnvironment;
            _context = context;
        }

        [HttpPost]
        public IActionResult Capture(string name)
        {
            var filePath = "";
            try
            {
                var files = HttpContext.Request.Form.Files;
                if (files != null)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {

                            var fileName = file.FileName;
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var fileExtension = Path.GetExtension(fileName);
                            var newFileName = string.Concat(myUniqueFileName, fileExtension);
                            var filepath = Path.Combine(_environment.WebRootPath, "CameraPhotos") + $@"\{newFileName}";

                            if (!string.IsNullOrEmpty(filepath))
                            {
                                filepath = filepath;
                                StoreInFolder(file, filepath);

                            }

                            //var imageBytes = System.IO.File.ReadAllBytes(filepath);
                            //if (imageBytes != null)
                            //{
                            //    StoreInDatabase(imageBytes);
                            //}

                        }
                    }
                    return Json(filePath);
                }
                else
                {
                    return Json(false);
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void StoreInFolder(IFormFile file, string fileName)
        {
            using (FileStream fs = System.IO.File.Create(fileName))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        //private void StoreInDatabase(byte[] imageBytes)
        //{
        //    try
        //    {
        //        if (imageBytes != null)
        //        {
        //            string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
        //            string imageUrl = string.Concat("data:image/jpg;base64,", base64String);

        //            ImageStore imageStore = new ImageStore()
        //            {
        //                CreateDate = DateTime.Now,
        //                ImageBase64String = imageUrl,
        //                ImageId = 0
        //            };

        //            _context.ImageStores.Add(imageStore);
        //            _context.SaveChanges();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //}
    }
}