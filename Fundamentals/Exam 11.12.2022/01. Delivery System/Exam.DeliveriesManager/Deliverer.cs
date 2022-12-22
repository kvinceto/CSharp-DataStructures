using System.Collections.Generic;

namespace Exam.DeliveriesManager
{
    public class Deliverer
    {
        public Deliverer(string id, string name)
        {
            Id = id;
            Name = name;
            this.Packages = new LinkedList<Package>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public LinkedList<Package> Packages { get; set; }

        public override bool Equals(object obj)
        {
            return this.Id == ((Deliverer)obj).Id;
        }
    }
}
