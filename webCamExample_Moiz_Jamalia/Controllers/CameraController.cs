using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using webCamExample_Moiz_Jamalia.Data;
using webCamExample_Moiz_Jamalia.Models;

namespace webCamExample_Moiz_Jamalia.Controllers
{
    public class CameraController : Controller
    {
        private readonly IWebHostEnvironment env;
        private readonly AppDbContext context;

        public CameraController(IWebHostEnvironment env, AppDbContext context)
        {
            this.env = env;
            this.context = context;
        }

        public IActionResult Capture()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CapturePhoto()
        {
            try
            {
                var files = HttpContext.Request.Form.Files;
                if(files != null)
                {
                    foreach(var file in files)
                    {
                        if(file.Length > 0)
                        {
                            var fileName = file.FileName;
                            var myUniqueFileName = Convert.ToString(Guid.NewGuid());
                            var fileExtension = Path.GetExtension(fileName);
                            var newFileName = string.Concat(myUniqueFileName, fileExtension);
                            var filePath = Path.Combine(this.env.WebRootPath, "CameraPhotos") + $@"\{newFileName}";
                            if(!string.IsNullOrEmpty(filePath))
                            {
                                StoreInFolder(file, filePath);
                            }
                            var imageBytes = System.IO.File.ReadAllBytes(filePath);
                            if(imageBytes != null)
                            {
                                StoreInDatabase(imageBytes);
                            }
                        }
                    }
                    return Json(true);
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
        private static void StoreInFolder(IFormFile file, string fileName)
        {
            using FileStream fs = System.IO.File.Create(fileName);
            file.CopyTo(fs);
            fs.Flush();
        }

        private void StoreInDatabase(byte[] imageBytes)
        {
            try
            {
                if (imageBytes != null)
                {
                    string base64String = Convert.ToBase64String(imageBytes, 0, imageBytes.Length);
                    string imageUrl = string.Concat("data:image/jpg;base64,", base64String);
                    ImageStore imageStore = new()
                    {
                        CreateDate = DateTime.Now,
                        ImageBase64String = imageUrl,
                    };
                    this.context.ImageStores.Add(imageStore);
                    this.context.SaveChanges();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
