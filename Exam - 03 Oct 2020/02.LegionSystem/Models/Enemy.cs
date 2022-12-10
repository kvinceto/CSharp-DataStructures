namespace _02.LegionSystem.Models
{
    using System;
    using _02.LegionSystem.Interfaces;

    public class Enemy : IEnemy
    {
        public Enemy(int attackSpeed, int health)
        {
            this.AttackSpeed = attackSpeed;
            this.Health = health;
        }

        public int AttackSpeed { get; set; }

        public int Health { get; set; }

        public int CompareTo(object obj)
        {
            IEnemy other = (IEnemy)obj;
            if (this.AttackSpeed > other.AttackSpeed)
                return 1;
            if (this.AttackSpeed == other.AttackSpeed)
                return 0;
            return -1;
        }

        public bool Equals(object otherObj)
        {
            Enemy other = (Enemy)otherObj;
            return AttackSpeed == other.AttackSpeed && Health == other.Health;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (AttackSpeed * 397) ^ Health;
            }
        }
    }
}
