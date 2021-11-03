using Autofac;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace SocialNetwork.Areas.Admin.Models.PhotoModels
{
    public class PhotoFileOperationModel
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public PhotoFileOperationModel()
        {
            _webHostEnvironment = Startup.AutofacContainer.Resolve<IWebHostEnvironment>();
        }

        public string GetUniqueFileName(IFormFile photoFile)
        {
            string UniqueFileName = null;

            if (photoFile != null)
            {
                UniqueFileName = Guid.NewGuid().ToString() + "_" + photoFile.FileName;
            }
            else
                throw new NullReferenceException("File is Empty");

            return UniqueFileName;
        }

        public void Upload(IFormFile photoFile, string fileName)
        {
            if (photoFile != null)
            {
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                var filePath = Path.Combine(folderPath, fileName);

                using var fileStream = new FileStream(filePath, FileMode.Create);
                photoFile.CopyTo(fileStream);
            }
            else
                throw new NullReferenceException("File is Empty");
        }

        public IFormFile ConverToPhotoFile(string fileName)
        {
            var filepath = Path.Combine(_webHostEnvironment.WebRootPath, @"images\" + fileName);
            using var stream = File.OpenRead($"{filepath}");

            var PhotoFile = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));

            return PhotoFile;
        }
    }
}
