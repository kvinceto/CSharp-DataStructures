using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class AirlinesManager : IAirlinesManager
    {
        private Dictionary<string, Airline> airlinesById;
        private Dictionary<string, Flight> flightsById;

        public AirlinesManager()
        {
            this.airlinesById = new Dictionary<string, Airline>();
            this.flightsById = new Dictionary<string, Flight>();
        }

        public void AddAirline(Airline airline)
        {
            this.airlinesById.Add(airline.Id, airline);
        }

        public void AddFlight(Airline airline, Flight flight)
        {
            if (!this.airlinesById.ContainsKey(airline.Id))
                throw new ArgumentException();
            this.airlinesById[airline.Id].Flights.Add(flight.Id, flight);
            this.flightsById.Add(flight.Id, flight);
        }

        public bool Contains(Airline airline)
        {
            return this.airlinesById.ContainsKey(airline.Id);
        }

        public bool Contains(Flight flight)
        {
            return this.flightsById.ContainsKey(flight.Id);
        }

        public void DeleteAirline(Airline airline)
        {
            if (!this.airlinesById.ContainsKey(airline.Id))
                throw new ArgumentException();

            foreach (var kvp in this.airlinesById[airline.Id].Flights)
            {
                this.flightsById.Remove(kvp.Key);
            }
            this.airlinesById.Remove(airline.Id);
        }

        public IEnumerable<Airline> GetAirlinesOrderedByRatingThenByCountOfFlightsThenByName()
        {
            return  this.airlinesById.Values
                .OrderByDescending(a => a.Rating)
                .ThenByDescending(a => a.Flights.Count)
                .ThenBy(a => a.Name);
        }

        public IEnumerable<Airline> GetAirlinesWithFlightsFromOriginToDestination(string origin, string destination)
        {
            return this.airlinesById.Values
                .Where(a => a.Flights.Values.Any(f => !f.IsCompleted && f.Origin == origin && f.Destination == destination))
                .ToList();

        }

        public IEnumerable<Flight> GetAllFlights()
        {
            return this.flightsById.Values;
        }

        public IEnumerable<Flight> GetCompletedFlights()
        {
            return this.flightsById.Values.Where(f => f.IsCompleted);
        }

        public IEnumerable<Flight> GetFlightsOrderedByCompletionThenByNumber()
        {
            return this.flightsById.Values
                .OrderBy(f => f.IsCompleted)
                .ThenBy(f => f.Number);
        }

        public Flight PerformFlight(Airline airline, Flight flight)
        {
            if (!this.airlinesById.ContainsKey(airline.Id))
                throw new ArgumentException();

            if (!this.airlinesById[airline.Id].Flights.ContainsKey(flight.Id))
                throw new ArgumentException();

            Flight result = this.airlinesById[airline.Id].Flights[flight.Id];
            flight.IsCompleted = true;
            return result;
        }
    }
}
