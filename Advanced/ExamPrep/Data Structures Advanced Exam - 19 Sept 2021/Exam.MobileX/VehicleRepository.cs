using System;
using System.Collections.Generic;

namespace Exam.MobileX
{
    using System.Linq;

    public class VehicleRepository : IVehicleRepository
    {
        private Dictionary<string, HashSet<Vehicle>> vehiclesBySeller;
        private Dictionary<string, Vehicle> vehiclesById;

        public VehicleRepository()
        {
            this.vehiclesBySeller = new Dictionary<string, HashSet<Vehicle>>();
            this.vehiclesById = new Dictionary<string, Vehicle>();
        }

        public int Count => this.vehiclesById.Count;

        public void AddVehicleForSale(Vehicle vehicle, string sellerName)
        {
            if (!this.vehiclesBySeller.ContainsKey(sellerName))
            {
                this.vehiclesBySeller.Add(sellerName, new HashSet<Vehicle>());
            }

            vehicle.SellerName = sellerName;

            this.vehiclesBySeller[sellerName].Add(vehicle);
            this.vehiclesById.Add(vehicle.Id, vehicle);
        }

        public Vehicle BuyCheapestFromSeller(string sellerName)
        {
            if (!this.vehiclesBySeller.ContainsKey(sellerName))
            {
                throw new ArgumentException();
            }

            if (this.vehiclesBySeller[sellerName].Count == 0)
            {
                throw new ArgumentException();
            }

            Vehicle vehicle = this.vehiclesBySeller[sellerName].OrderBy(v => v.Price).First();
            this.vehiclesById.Remove(vehicle.Id);
            this.vehiclesBySeller[sellerName].Remove(vehicle);
            return vehicle;
        }

        public bool Contains(Vehicle vehicle)
        {
            return this.vehiclesById.ContainsKey(vehicle.Id);
        }

        public Dictionary<string, List<Vehicle>> GetAllVehiclesGroupedByBrand()
        {
            if (this.vehiclesById.Count == 0)
            {
                throw new ArgumentException();
            }

            Dictionary<string, List<Vehicle>> result = new Dictionary<string, List<Vehicle>>();
            var list = this.vehiclesById.Values.OrderBy(v => v.Price);

            foreach ( var vehicle in list)
            {
                if (!result.ContainsKey(vehicle.Brand))
                {
                    result.Add(vehicle.Brand, new List<Vehicle>());
                }

                result[vehicle.Brand].Add(vehicle);
            }

            return result;
        }

        public IEnumerable<Vehicle> GetAllVehiclesOrderedByHorsepowerDescendingThenByPriceThenBySellerName()
        {
            return this.vehiclesById.Values
                .OrderByDescending(v => v.Horsepower)
                .ThenBy(v => v.Price)
                .ThenBy(v => v.SellerName);
        }

        public IEnumerable<Vehicle> GetVehicles(List<string> keywords)
        {
            HashSet<string> set = new HashSet<string>();
            foreach (string keyword in keywords)
            {
                set.Add(keyword);
            }

            return this.vehiclesById.Values
                .Where(v => set.Contains(v.Brand) && set.Contains(v.Model) &&
                            set.Contains(v.Location) && set.Contains(v.Color))
                .OrderByDescending(v => v.IsVIP)
                .ThenBy(v => v.Price);
        }

        public IEnumerable<Vehicle> GetVehiclesBySeller(string sellerName)
        {
            if (!this.vehiclesBySeller.ContainsKey(sellerName))
            {
                throw new ArgumentException();
            }

            return this.vehiclesBySeller[sellerName];
        }

        public IEnumerable<Vehicle> GetVehiclesInPriceRange(double lowerBound, double upperBound)
        {
            return this.vehiclesById.Values
                .Where(v => v.Price >= lowerBound && v.Price <= upperBound)
                .OrderByDescending(v => v.Horsepower);
        }

        public void RemoveVehicle(string vehicleId)
        {
            if (!this.vehiclesById.ContainsKey(vehicleId))
            {
                throw new ArgumentException();
            }

            var vehicle = this.vehiclesById[vehicleId];
            this.vehiclesById.Remove(vehicleId);
            this.vehiclesBySeller[vehicle.SellerName].Remove(vehicle);
        }
    }
}
