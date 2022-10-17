using System;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace RestFileManager.Services
{
    public static class FileService
    {
        public static void SaveFile(IFormFile file)
        {
            if (file.Length <= 0) throw new Exception("Empty file!");
            using var fileStream = new FileStream(file.FileName, FileMode.Create);
            file.CopyTo(fileStream);
        }
    }
}
