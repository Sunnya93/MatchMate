using MatchMate.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MatchMate.Data
{
    public class MatchService
    {
        public List<Matched> Matcheds { get; set; }
        public List<Place> Places { get; set; }

        public Task<List<Matched>> GetMatchAsync(List<People> peoples, int MaxPeopleCount)
        {
            Matchmaker maker = new Matchmaker();
            return Task.FromResult(maker.MakeMatches(peoples, Places.Where(i => i.MaxTeam != 0).ToList(), MaxPeopleCount));
        }

        public async Task<List<People>> GetPeopleAsync()
        {
            string json;
            using (FileStream fs = File.OpenRead(@"D:\develop\MatchMate\MatchMate\wwwroot\data\Peoples.json"))

            using (StreamReader reader = new StreamReader(fs))
            {
                json = await reader.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<List<People>>(json);
        }
    }
}
