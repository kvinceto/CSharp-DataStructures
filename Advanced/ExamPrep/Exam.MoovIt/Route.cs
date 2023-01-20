using System.Collections.Generic;

namespace Exam.MoovIt
{
    using System;

    public class Route
    {
        public string Id { get; set; }

        public double Distance { get; set; }

        public int Popularity { get; set; }

        public bool IsFavorite { get; set; }

        public List<string> LocationPoints { get; set; } = new List<string>();

        public Route(string id, double distance, int popularity, bool isFavorite, List<string> locationPoints)
        {
            this.Id = id;
            this.Distance = distance;
            this.Popularity = popularity;
            this.IsFavorite = isFavorite;
            this.LocationPoints = locationPoints;
        }

        public override int GetHashCode()
        {
            return Math.Abs(this.Id.GetHashCode()) + this.Distance.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return this.Id == ((Route)obj).Id && this.Distance == ((Route)obj).Distance;
        }
    }
}
