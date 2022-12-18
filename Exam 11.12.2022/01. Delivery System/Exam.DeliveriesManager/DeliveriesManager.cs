using System;
using System.Collections.Generic;
using System.Linq;

namespace Exam.DeliveriesManager
{
    public class DeliveriesManager : IDeliveriesManager
    {
        private LinkedList<Package> _packages;
        private LinkedList<Deliverer> _delives;
        private Dictionary<string, Deliverer> _delivesDictionary;
        private Dictionary<string, Package> _packagesDictionary;

        public DeliveriesManager()
        {
            _packages = new LinkedList<Package>();
            _delives = new LinkedList<Deliverer>();
            _packagesDictionary = new Dictionary<string,Package>();
            _delivesDictionary = new Dictionary<string,Deliverer>();
        }
        public void AddDeliverer(Deliverer deliverer)
        {
            _delives.AddLast(deliverer);
            _delivesDictionary[deliverer.Id] = deliverer;
        }

        public void AddPackage(Package package)
        {
            _packages.AddLast(package);
            _packagesDictionary[package.Id] = package;
        }

        public bool Contains(Deliverer deliverer)
        {
            return _delivesDictionary.ContainsKey(deliverer.Id);
        }

        public bool Contains(Package package)
        {
           return _packagesDictionary.ContainsKey(package.Id);
        }

        public IEnumerable<Deliverer> GetDeliverers()
        {
            return _delives;
        }

        public IEnumerable<Package> GetPackages()
        {
            return _packages;
        }

        public void AssignPackage(Deliverer deliverer, Package package)
        {
            if (!this.Contains(deliverer) || !this.Contains(package))
                throw new ArgumentException();

            deliverer.Packages.AddLast(package);
            package.Deliverer = deliverer;
        }

        public IEnumerable<Package> GetUnassignedPackages()
        {
            return _packages.Where(p => p.Deliverer == null);
        }

        public IEnumerable<Package> GetPackagesOrderedByWeightThenByReceiver()
        {
            return _packages
                .OrderByDescending(p => p.Weight)
                .ThenBy(p => p.Receiver)
                .AsEnumerable();
        }

        public IEnumerable<Deliverer> GetDeliverersOrderedByCountOfPackagesThenByName()
        {
            return _delives
                .OrderByDescending(d => d.Packages.Count)
                .ThenBy(d => d.Name)
                .AsEnumerable();
        }
    }

}
