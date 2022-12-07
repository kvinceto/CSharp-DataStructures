using System;
using System.Collections.Generic;

namespace TripAdministrations
{
    public class Company
    {
        public Company(string name, int tripOrganizationLimit)
        {
            this.Name = name;
            this.TripOrganizationLimit = tripOrganizationLimit;
            this.Trips = new Dictionary<string, Trip>();
        }

        public string Name { get; set; }

        public int TripOrganizationLimit { get; set; }

        public int CurrentTrips { get; set; }

        public Dictionary<string, Trip> Trips { get; set; }
        public override bool Equals(object obj)
        {
            return this.Name == ((Company)obj).Name;
        }
    }
}
