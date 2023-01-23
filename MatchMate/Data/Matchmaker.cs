using MatchMate.Class;

namespace MatchMate.Data
{
    class Matchmaker
    {
        private readonly List<People> _people;
        private readonly Random _random = new Random();

        public Matchmaker()
        {
        }

        public Matchmaker(List<People> people)
        {
            _people = people;
        }

        public List<Tuple<People, People>> MakeMatches()
        {
            var sameSexPeople = _people;
            if (sameSexPeople.Count < 2) return null;

            var matches = new List<Tuple<People, People>>();
            
            while (sameSexPeople.Count >= 2)
            {
                int index1 = _random.Next(sameSexPeople.Count);
                int index2 = _random.Next(sameSexPeople.Count);

                while (index1 == index2 || sameSexPeople[index1].Gender != sameSexPeople[index2].Gender || matches.Any(match => match.Item1 == sameSexPeople[index1] || match.Item2 == sameSexPeople[index2]))
                {
                    if (matches.Count == _people.Count / 2)
                        return matches;
                    index1 = _random.Next(sameSexPeople.Count);
                    index2 = _random.Next(sameSexPeople.Count);
                }
                var match = new Tuple<People, People>(sameSexPeople[index1], sameSexPeople[index2]);
                matches.Add(match);
                sameSexPeople.RemoveAt(index1);
                if (index2 >= index1) index2--;
                sameSexPeople.RemoveAt(index2);
            }
            return matches;
        }

        public List<Matched> MakeMatches(List<People> selectedPeople, List<Place> places, int maxMatchedPeople)
        {
            List<Matched> matcheds = new List<Matched>();
            var malePeople = selectedPeople.FindAll(person => person.Gender == "Male");
            var femalePeople = selectedPeople.FindAll(person => person.Gender == "Female");

            // Create a new Random object outside of the loops to improve performance
            var random = new Random();

            // Loop through the places
            foreach (Place place in places)
            {
                if (malePeople.Count == 0) return matcheds;

                var matches = new List<List<People>>();
                var matchedPeople = new List<People>();

                int i = 0;
                while (malePeople.Count >= 0 && matches.Count < place.MaxTeam)
                {
                    var group = new List<People>();
                    int j = 0;
                    while (j < maxMatchedPeople && malePeople.Count > 0)
                    {
                        int index = random.Next(malePeople.Count);
                        if (!matchedPeople.Contains(malePeople[index]))
                        {
                            matchedPeople.Add(malePeople[index]);
                            group.Add(malePeople[index]);
                            malePeople.RemoveAt(index);
                            j++;
                        }
                    }

                    matches.Add(group);
                    i += maxMatchedPeople;

                    if (malePeople.Count == 0)
                    {
                        malePeople = femalePeople;
                        continue;
                    }
                }

                matcheds.Add(new Matched
                {
                    Place = place,
                    MatchedPerson = matches
                });
            }

            return matcheds;
        }
    }
}
