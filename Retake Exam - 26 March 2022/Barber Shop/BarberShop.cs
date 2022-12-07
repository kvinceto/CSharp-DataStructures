using System;
using System.Collections.Generic;
using System.Linq;

namespace BarberShop
{
    public class BarberShop : IBarberShop
    {
        private Dictionary<string, Barber> barbersByName;
        private Dictionary<string, Client> clientsByName;

        public BarberShop()
        {
            this.barbersByName = new Dictionary<string, Barber>();
            this.clientsByName = new Dictionary<string, Client>();
        }

        public void AddBarber(Barber b)
        {
            if (this.barbersByName.ContainsKey(b.Name))
            {
                throw new ArgumentException();
            }

            this.barbersByName.Add(b.Name, b);
        }

        public void AddClient(Client c)
        {
            if (this.clientsByName.ContainsKey(c.Name))
            {
                throw new ArgumentException();
            }

            this.clientsByName.Add(c.Name, c);
        }

        public bool Exist(Barber b)
        => this.barbersByName.ContainsKey(b.Name);

        public bool Exist(Client c)
        => this.clientsByName.ContainsKey(c.Name);

        public IEnumerable<Barber> GetBarbers()
        => this.barbersByName.Values.AsEnumerable();

        public IEnumerable<Client> GetClients()
        => this.clientsByName.Values.AsEnumerable();

        public void AssignClient(Barber b, Client c)
        {
            if (!this.Exist(b) || !this.Exist(c))
            {
                throw new ArgumentException();
            }

            b.Clients.Add(c.Name, c);
            c.Barber = b;
        }

        public void DeleteAllClientsFrom(Barber b)
        {
            if (!this.Exist(b))
            {
                throw new ArgumentException();
            }

            b.Clients = new Dictionary<string, Client>();
        }

        public IEnumerable<Client> GetClientsWithNoBarber()
            => this.clientsByName.Values.Where(c => c.Barber == null);

        public IEnumerable<Barber> GetAllBarbersSortedWithClientsCountDesc()
            => this.barbersByName.Values.OrderByDescending(b => b.Clients.Count).AsEnumerable();

        public IEnumerable<Barber> GetAllBarbersSortedWithStarsDecsendingAndHaircutPriceAsc()
            => this.barbersByName.Values
                .OrderByDescending(b => b.Stars)
                .ThenBy(b => b.HaircutPrice)
                .AsEnumerable();

        public IEnumerable<Client> GetClientsSortedByAgeDescAndBarbersStarsDesc()
            => this.clientsByName.Values
                .Where(c => c.Barber != null)
                .OrderByDescending(c => c.Age)
                .ThenByDescending(c => c.Barber.Stars)
                .AsEnumerable();
    }
}
