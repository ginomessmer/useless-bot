using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace UselessBot.Core.Services
{
    public class FileStorageService : IFileStorageService
    {
        public readonly string FileStoragePath;
        private readonly LiteDatabase liteDb;

        public FileStorageService(LiteDatabase liteDb)
        {
            this.liteDb = liteDb;
            this.FileStoragePath = Environment.CurrentDirectory + "/storage/";

            // Ensure file storage directory has been created
            Directory.CreateDirectory(this.FileStoragePath);
        }

        public async Task<FileInfo> DownloadFileAsync(string url, string relativeName)
        {
            using (var client = new HttpClient())
            {
                using (var stream = new FileStream(path: this.FileStoragePath + relativeName, 
                    mode: System.IO.FileMode.OpenOrCreate))
                {
                    await (await client.GetAsync(url)).Content.CopyToAsync(stream);

                    var path = stream.Name;
                    return new FileInfo(path);
                }
            }
        }

        public Task<FileInfo> ListAllFilesAsync(string relativePath)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFileAsync(string relativeName)
        {
            throw new NotImplementedException();
        }

        public Task SaveFileAsync(FileStream stream, string relativeName)
        {
            throw new NotImplementedException();
        }
    }
}
