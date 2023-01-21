namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Microsystems : IMicrosystem
    {
        private Dictionary<int, Computer> computersByNumber;

        private Dictionary<Brand, HashSet<Computer>> computersByBrands;

        public Microsystems()
        {
            computersByNumber = new Dictionary<int, Computer>();
            computersByBrands = new Dictionary<Brand, HashSet<Computer>>
            {
                { Brand.HP, new HashSet<Computer>()},
                { Brand.DELL, new HashSet<Computer>()},
                { Brand.ACER, new HashSet<Computer>()},
                { Brand.ASUS, new HashSet<Computer>()}
            };
        }

        public void CreateComputer(Computer computer)
        {
            if (this.Contains(computer.Number))
            {
                throw new ArgumentException();
            }

            computersByNumber.Add(computer.Number, computer);
            computersByBrands[computer.Brand].Add(computer);
        }

        public bool Contains(int number)
        {
            return this.computersByNumber.ContainsKey(number);
        }

        public int Count()
        {
            return computersByNumber.Count;
        }

        public Computer GetComputer(int number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            return computersByNumber[number];
        }

        public void Remove(int number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            computersByBrands[computersByNumber[number].Brand].Remove(computersByNumber[number]);
            computersByNumber.Remove(number);
        }

        public void RemoveWithBrand(Brand brand)
        {
            if (computersByBrands[brand].Count == 0)
            {
                throw new ArgumentException();
            }

            var list = computersByBrands[brand];
            computersByBrands[brand] = new HashSet<Computer>();
            foreach (var computer in list)
            {
                computersByNumber.Remove(computer.Number);
            }
        }

        public void UpgradeRam(int ram, int number)
        {
            if (!this.Contains(number))
            {
                throw new ArgumentException();
            }

            var com = computersByNumber[number];
            if (com.RAM < ram)
            {
                com.RAM = ram;
            }
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            return computersByBrands[brand].OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            return computersByNumber.Values
                .Where(c => c.ScreenSize == screenSize)
                .OrderByDescending(c => c.Number);
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            return computersByNumber.Values
                .Where(c => c.Color == color)
                .OrderByDescending(c => c.Price);
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
           return computersByNumber.Values
               .Where(c => c.Price >= minPrice && c.Price <= maxPrice)
               .OrderByDescending(c => c.Price);
        }
    }
}
