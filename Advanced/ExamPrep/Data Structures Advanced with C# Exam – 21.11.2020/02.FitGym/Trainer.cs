namespace _02.FitGym
{
    using System.Collections.Generic;

    public class Trainer
    {
        public Trainer(int id, string name, int popularity)
        {
            this.Id = id;
            this.Name = name;
            this.Popularity = popularity;
            Members = new Dictionary<int, Member>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }

        public Dictionary<int, Member> Members { get; set; }

        public bool Equals(Trainer other)
        {
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}