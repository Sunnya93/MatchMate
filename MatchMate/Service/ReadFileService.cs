using MatchMate.Page.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMate.Service
{
    public class ReadFileService : IReadFileService
    {
        public async Task<string> ReadFile(string FilePath)
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync(FilePath);
            using var reader = new StreamReader(stream);
            return await reader.ReadToEndAsync();
        }
    }
}
