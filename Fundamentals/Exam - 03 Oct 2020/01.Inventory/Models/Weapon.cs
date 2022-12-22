namespace _01.Inventory.Models
{
    using _01.Inventory.Interfaces;

    public abstract class Weapon : IWeapon
    {
        public Weapon(int id, int maxCapacity, int ammunition)
        {
            this.Id = id;
            this.MaxCapacity = maxCapacity;
            this.Ammunition = ammunition;
        }

        public int Id { get; private set; }
        public int Ammunition { get; set; }
        public int MaxCapacity { get; set; }
        public Category Category { get; set; }

        protected bool Equals(object otherObj)
        {
            Weapon other = (Weapon)otherObj;
            return Id == other.Id && Ammunition == other.Ammunition && MaxCapacity == other.MaxCapacity && Category == other.Category;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ Ammunition;
                hashCode = (hashCode * 397) ^ MaxCapacity;
                hashCode = (hashCode * 397) ^ (int)Category;
                return hashCode;
            }
        }
    }
}
