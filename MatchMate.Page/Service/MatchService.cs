using MatchMate.Class;
using MatchMate.Page.Logics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MatchMate.Page.Service
{
    public class MatchService
    {
        public List<Matched>? Matcheds { get; set; }
        public List<Place>? Places { get; set; }

        public Task<List<Matched>> GetMatchAsync(List<People> peoples, int MaxPeopleCount)
        {
            Matchmaker maker = new Matchmaker(peoples);
            List<Matched> matcheds = maker.MakeMatches(peoples, Places!.Where(i => i.MaxTeam != 0).ToList(), MaxPeopleCount);
            Places = matcheds.Select(i => i.Place).ToList()!;
            return Task.FromResult(matcheds);
        }

        public List<People> GetPeopleAsync()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "wwwroot/data/Peoples.json");

            using (var sr = new StreamReader(path))
            {
                string jsonData = sr.ReadToEnd();
                return JsonSerializer.Deserialize<List<People>>(jsonData)!;
            }
        }

        public List<Place> GetInitPlace()
        {
            List<Place> places = new List<Place>()
            {
                new Place{ Name = "맥도날드", Color = "#f6b910", MaxTeam = 0 },
                new Place{ Name = "올리브영", Color = "#0bf249", MaxTeam = 0 },
                new Place{ Name = "가락115동", Color = "#1cdbef", MaxTeam = 0 },
                new Place{ Name = "삼성빌딩", Color = "#0497f8", MaxTeam = 0 },
                new Place{ Name = "프라임병원", Color = "#f70505", MaxTeam = 0}
            };

            return places;
        }

        public Tuple<string, List<People>> SetMessageAndContact()
        {
            StringBuilder Text = new StringBuilder();
            List<People> people = new List<People>();

            Text.Append($"{DateTime.Now.ToString("MM/dd")} 전시대 팀배정\r\n");

            foreach (Matched matched in Matcheds!)
            {
                Text.Append($"{matched.Place!.Name}-");
                int OrderNumber = 0;
                foreach (List<People> peoples in matched.MatchedPerson!)
                {
                    Text.Append($"{++OrderNumber}{string.Join(",", peoples.Select(i => i.Name))}");
                    people.AddRange(peoples);
                }

                Text.Append("\r\n");
            }

            return new Tuple<string, List<People>>(Text.ToString(), people);
        }
    }
}
