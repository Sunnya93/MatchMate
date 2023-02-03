namespace MatchMate.Class
{
    public class People : IEquatable<People>
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public Place MatchedPlace { get; set; } = new Place();
        public bool Equals(People? other)
        {
            if (other is null) return false;
            return Name == other.Name;
        }

    }
}