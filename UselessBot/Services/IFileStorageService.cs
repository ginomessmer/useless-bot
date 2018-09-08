using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UselessBot.Services
{
    public interface IFileStorageService
    {
        Task SaveFileAsync(FileStream stream, string name);
    }
}
