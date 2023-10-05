//using MatchMate.Page.Service;
//namespace MatchMate.Wasm.Service
//{

//    public class ReadFileService : IReadFileService
//    {

//        private readonly HttpClient httpClient;

//        public ReadFileService(HttpClient httpClient)
//        {
//            this.httpClient = httpClient;
//        }

//        public async Task<string> ReadFile(string FilePath)
//        {
//            return await this.httpClient.GetStringAsync(FilePath.Replace("wwwroot", ""));
//        }
//    }
//} 
