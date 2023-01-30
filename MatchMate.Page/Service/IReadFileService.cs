using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMate.Page.Service
{
    public interface IReadFileService
    {
        Task<string> ReadFile(string filePath);
    }
}
