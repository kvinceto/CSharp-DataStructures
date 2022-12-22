using System.Linq;

namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        private SortedDictionary<int, IEnemy> enemies;

        public Legion()
        {
            this.enemies = new SortedDictionary<int, IEnemy>();
        }

        public int Size => this.enemies.Count;

        public bool Contains(IEnemy enemy)
        {
            return this.enemies.ContainsKey(enemy.AttackSpeed);
        }

        public void Create(IEnemy enemy)
        {
            if (this.enemies.ContainsKey(enemy.AttackSpeed))
                return;
            this.enemies.Add(enemy.AttackSpeed, enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            if (this.enemies.ContainsKey(speed))
            {
                return this.enemies[speed];
            }
            return null;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            return this.enemies.Values.Where(e => e.AttackSpeed > speed).ToList();
        }

        public IEnemy GetFastest()
        {
            if (this.Size == 0)
                throw new InvalidOperationException("Legion has no enemies!");
            return this.enemies.Last().Value;
        }

        public IEnemy[] GetOrderedByHealth()
        {
            return this.enemies.Values.OrderByDescending(e => e.Health).ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            return this.enemies.Values.Where(e => e.AttackSpeed < speed).ToList();
        }

        public IEnemy GetSlowest()
        {
            if (this.Size == 0)
                throw new InvalidOperationException("Legion has no enemies!");
            return this.enemies.First().Value;
        }

        public void ShootFastest()
        {
            if (this.Size == 0)
                throw new InvalidOperationException("Legion has no enemies!");
            int n = this.enemies.Last().Key;
            this.enemies.Remove(n);
        }

        public void ShootSlowest()
        {
            if (this.Size == 0)
                throw new InvalidOperationException("Legion has no enemies!");
            int n = this.enemies.First().Key;
            this.enemies.Remove(n);
        }
    }
}
