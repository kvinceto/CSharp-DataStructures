namespace _01.DogVet
{
    using System.Collections.Generic;

    public class Owner
    {
        public Owner(string id, string name)
        {
            this.Id = id;
            this.Name = name;
            dogsByName = new Dictionary<string, Dog>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public Dictionary<string, Dog> dogsByName { get; set; }

        public bool Equals(Owner other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Id != null ? Id.GetHashCode() : 0) * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }
    }
}