namespace Exam.MobileX
{
    using System;

    public class Vehicle
    {
        public Vehicle(string id, string brand, string model, string location, string color, int horsepower, double price, bool isVIP)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Location = location;
            Color = color;
            Horsepower = horsepower;
            Price = price;
            IsVIP = isVIP;
        }

        public string Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Location { get; set; }

        public string Color { get; set; }

        public int Horsepower { get; set; }

        public double Price { get; set; }

        public bool IsVIP { get; set; }

        public string SellerName { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            return this.Id == ((Vehicle)obj).Id;
        }

        public override int GetHashCode()
        {
            var hashCode = new HashCode();
            hashCode.Add(Id);
            hashCode.Add(Brand);
            hashCode.Add(Model);
            hashCode.Add(Location);
            hashCode.Add(Color);
            hashCode.Add(Horsepower);
            hashCode.Add(Price);
            hashCode.Add(IsVIP);
            hashCode.Add(SellerName);
            return hashCode.ToHashCode();
        }
    }
}
