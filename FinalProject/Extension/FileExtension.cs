using FinalProject.ViewModel.Admin;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FinalProject.Extension
{
    public static class FileExtension
    {
        public static bool IsImage(this IFormFile file)
        {
            bool isImage = !file.ContentType.Contains("image/"); ;
            return isImage;
        }
        public static bool CheckImageSize(this IFormFile file, int size)
        {
            return file.Length / 1024 > size;

        }


        public static string SaveImage(this IFormFile file, IWebHostEnvironment webHostEnvironment, string folder)
        {
            if (file != null)
            {
                string newImageName = Guid.NewGuid() + file.FileName;

                string path = Path.Combine(webHostEnvironment.WebRootPath, folder, newImageName);
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fileStream);


                }
                return newImageName;
            }
            return null;
        }
    }

}