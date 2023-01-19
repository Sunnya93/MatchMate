using MatchMate.Class;

namespace MatchMate.Data
{
    class Matchmaker
    {
        private readonly List<People> _people;
        private readonly Random _random = new Random();

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

        public List<List<People>> MakeMatches(int maxMatchedPeople)
        {
            var sameSexPeople = _people.FindAll(person => person.Gender == "male" || person.Gender == "female");
            if (sameSexPeople.Count < maxMatchedPeople) return null;
            var matches = new List<List<People>>();
            var matchedPeople = new List<People>();
            var random = new Random();
            int i = 0;
            while (sameSexPeople.Count > 0)
            {
                var group = new List<People>();
                int j = 0;
                while (j < maxMatchedPeople && sameSexPeople.Count > 0)
                {
                    int index = random.Next(sameSexPeople.Count);
                    if (!matchedPeople.Contains(sameSexPeople[index]))
                    {
                        matchedPeople.Add(sameSexPeople[index]);
                        group.Add(sameSexPeople[index]);
                        sameSexPeople.RemoveAt(index);
                        j++;
                    }
                }
                matches.Add(group);
                i += maxMatchedPeople;
            }
            return matches;
        }
    }
}
