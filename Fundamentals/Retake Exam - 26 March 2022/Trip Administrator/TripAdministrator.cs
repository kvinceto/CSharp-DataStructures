using System;
using System.Collections.Generic;
using System.Linq;

namespace TripAdministrations
{
    public class TripAdministrator : ITripAdministrator
    {
        private Dictionary<string, Trip> tripsById;
        private Dictionary<string, Company> companyByName;

        public TripAdministrator()
        {
            this.tripsById = new Dictionary<string, Trip>();
            companyByName = new Dictionary<string, Company>();
        }

        public void AddCompany(Company c)
        {
            if (this.Exist(c))
            {
                throw new ArgumentException();
            }

            companyByName.Add(c.Name, c);
        }

        public void AddTrip(Company c, Trip t)
        {
            if (!this.Exist(c))
            {
                throw new ArgumentException();
            }

            if (c.Trips.Count == c.TripOrganizationLimit)
            {
                return;
            }
            this.tripsById.Add(c.Name, t);
            this.companyByName[c.Name].Trips.Add(t.Id, t);
        }

        public bool Exist(Company c)
        => this.companyByName.ContainsKey(c.Name);

        public bool Exist(Trip t)
        => this.tripsById.ContainsKey(t.Id);

        public void RemoveCompany(Company c)
        {
            if (!this.Exist(c))
            {
                throw new ArgumentException();
            }

            this.companyByName.Remove(c.Name);
            foreach (var trip in c.Trips.Keys)
            {
                this.tripsById.Remove(trip);
            }
        }

        public IEnumerable<Company> GetCompanies()
            => this.companyByName.Values.AsEnumerable();

        public IEnumerable<Trip> GetTrips()
        => this.tripsById.Values.AsEnumerable();

        public void ExecuteTrip(Company c, Trip t)
        {
            if (!this.Exist(c) || !this.Exist(t))
            {
                throw new ArgumentException();
            }

            if (!c.Trips.ContainsKey(t.Id))
                throw new ArgumentException();
            c.Trips.Remove(t.Id);
        }

        public IEnumerable<Company> GetCompaniesWithMoreThatNTrips(int n)
            => this.companyByName.Values
                .Where(c => c.Trips.Count > n)
                .AsEnumerable();

        public IEnumerable<Trip> GetTripsWithTransportationType(Transportation t)
            => this.tripsById.Values.Where(t => t.Transportation.Equals(t));

        public IEnumerable<Trip> GetAllTripsInPriceRange(int lo, int hi)
            => this.tripsById.Values
                .Where(t => t.Price >= lo && t.Price <= hi)
                .AsEnumerable();
    }
}
