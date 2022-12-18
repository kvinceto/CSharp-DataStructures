using System;
using System.Collections.Generic;

namespace Exam.DeliveriesManager
{
    public class Airline
    {
        public Airline(string id, string name, double rating)
        {
            Id = id;
            Name = name;
            Rating = rating;
            this.Flights = new Dictionary<string, Flight>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public double Rating { get; set; }

        public Dictionary<string, Flight> Flights { get; set; }

        public bool Equals(object otherObj)
        {
            Airline other = (Airline)otherObj;
            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Rating);
        }
    }
}
