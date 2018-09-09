using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UselessBot.Services
{
    public interface IFileStorageService
    {
        Task SaveFileAsync(FileStream stream, string relativeName);
        Task<FileInfo> DownloadFileAsync(string url, string relativeName);
        Task<FileInfo> ListAllFilesAsync(string relativePath);
        Task RemoveFileAsync(string relativeName);
    }
}
