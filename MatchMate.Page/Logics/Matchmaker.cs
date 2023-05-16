using MatchMate.Class;

namespace MatchMate.Page.Logics
{
    public class Matchmaker
    {
        private readonly List<People> _people;
        private readonly Random _random = new Random();

        public Matchmaker(List<People> people)
        {
            _people = people;
        }

        public List<Tuple<People, People>>? MakeMatches()
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

        public List<Matched> MakeMatches(List<People> selectedPeople, List<Place> places, int maxMatchedPeople, List<Matched> Matcheds)
        {
            List<Matched> matcheds = Matcheds;

            // Create a new Random object outside of the loops to improve performance
            var random = new Random();

            // Loop through the places
            foreach (Place place in places)
            {
                if (selectedPeople.Count == 0) return matcheds;

                var matches = new List<List<People>>();
                var matchedPeople = new List<People>();
                //bool SetBrother = false;

                while (matches.Count < place.MaxTeam)
                {
                    int j = 0;
                    var group = new List<People>();

                    while (j < maxMatchedPeople && j < selectedPeople.Count)
                    {
                        int index = random.Next(selectedPeople.Count);
                        var selectedPerson = selectedPeople[index];

                        // Add condition to check if the person's gender is the same as the previous matched people
                        if (!matchedPeople.Any() || selectedPerson.Gender == matchedPeople[0].Gender)
                        {
                            selectedPeople[index].MatchedPlace = place;
                            matchedPeople.Add(selectedPeople[index]);
                            group.Add(selectedPeople[index]);
                            selectedPeople.Remove(selectedPeople[index]);
                            j++;
                        }
                    }
                    matches.Add(group);
                    matchedPeople = new List<People>();
                    #region 형제 배정 후 자매 배정 (2023-05-16 해당 로직 삭제)
                    //    var malePeople = selectedPeople.FindAll(person => person.Gender == "형제");

                    //    //형제 짝 먼저 배정
                    //    if ((malePeople.Count > 0 && !SetBrother) || selectedPeople.FindAll(person => person.Gender == "자매").Count == 0)
                    //    {
                    //        while (j < maxMatchedPeople && j < malePeople.Count)
                    //        {
                    //            int index = random.Next(malePeople.Count);
                    //            if (!matchedPeople.Contains(malePeople[index]))
                    //            {
                    //                malePeople[index].MatchedPlace = place;
                    //                matchedPeople.Add(malePeople[index]);
                    //                group.Add(malePeople[index]);
                    //                selectedPeople.Remove(malePeople[index]);
                    //                j++;
                    //            }
                    //        }
                    //        matches.Add(group);
                    //        SetBrother = true;
                    //    }

                    //   //자매 짝 배정
                    //    group = new List<People>();
                    //    j = 0;
                    //    var femalePeople = selectedPeople.FindAll(person => person.Gender == "자매");

                    //    if (femalePeople.Count > 0)
                    //    {
                    //        while (j < maxMatchedPeople && j < femalePeople.Count)
                    //        {
                    //            int index = random.Next(femalePeople.Count);
                    //            if (!matchedPeople.Contains(femalePeople[index]))
                    //            {
                    //                femalePeople[index].MatchedPlace = place;
                    //                matchedPeople.Add(femalePeople[index]);
                    //                group.Add(femalePeople[index]);
                    //                selectedPeople.Remove(femalePeople[index]);
                    //                j++;
                    //            }
                    //        }
                    //        matches.Add(group);
                    //    }
                    //}

                    #endregion
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
