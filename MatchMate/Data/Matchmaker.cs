using MatchMate.Class;

namespace MatchMate.Data
{
    class Matchmaker
    {
        private readonly List<Person> _people;
        private readonly Random _random = new Random();

        public Matchmaker(List<Person> people)
        {
            _people = people;
        }

        public List<Tuple<Person, Person>> MakeMatches()
        {
            var sameSexPeople = _people;
            if (sameSexPeople.Count < 2) return null;

            var matches = new List<Tuple<Person, Person>>();
            
            while (sameSexPeople.Count >= 2)
            {
                int index1 = _random.Next(sameSexPeople.Count);
                int index2 = _random.Next(sameSexPeople.Count);

                while (index1 == index2 || sameSexPeople[index1].Sex != sameSexPeople[index2].Sex || matches.Any(match => match.Item1 == sameSexPeople[index1] || match.Item2 == sameSexPeople[index2]))
                {
                    if (matches.Count == _people.Count / 2)
                        return matches;
                    index1 = _random.Next(sameSexPeople.Count);
                    index2 = _random.Next(sameSexPeople.Count);
                }
                var match = new Tuple<Person, Person>(sameSexPeople[index1], sameSexPeople[index2]);
                matches.Add(match);
                sameSexPeople.RemoveAt(index1);
                if (index2 >= index1) index2--;
                sameSexPeople.RemoveAt(index2);
            }
            return matches;
        }

        public List<List<Person>> MakeMatches(int maxMatchedPeople)
        {
            var sameSexPeople = _people.FindAll(person => person.Sex == "male" || person.Sex == "female");
            if (sameSexPeople.Count < maxMatchedPeople) return null;
            var matches = new List<List<Person>>();
            var matchedPeople = new List<Person>();
            var random = new Random();
            int i = 0;
            while (sameSexPeople.Count > 0)
            {
                var group = new List<Person>();
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
