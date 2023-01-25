using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchMate.Page.Service
{
    public class HttpClientService : HttpClient
    {
        public HttpClientService() { 
            BaseAddress = new Uri("https://localhost");
        }
    }
}
