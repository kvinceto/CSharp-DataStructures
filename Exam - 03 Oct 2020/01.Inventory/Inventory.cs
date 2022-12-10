using System.IO.Compression;
using System.Linq;

namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        Dictionary<int, IWeapon> weapons;

        public Inventory()
        {
            this.weapons = new Dictionary<int, IWeapon>();
        }

        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon)
        {
            if (!this.weapons.ContainsKey(weapon.Id))
            {
                this.weapons.Add(weapon.Id, weapon);
            }
        }

        public void Clear()
        {
            this.weapons = new Dictionary<int, IWeapon>();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.weapons.ContainsKey(weapon.Id);
        }

        public void EmptyArsenal(Category category)
        {
            IEnumerable<IWeapon> weaponsToEmpty = this.weapons.Values
                .Where(w => (int)w.Category == (int)category);
            foreach (var weapon in weaponsToEmpty)
            {
                weapon.Ammunition = 0;
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            if (!this.weapons.ContainsKey(weapon.Id))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            IWeapon weaponToFire = this.weapons[weapon.Id];
            if (weaponToFire.Ammunition < ammunition)
                return false;
            weaponToFire.Ammunition -= ammunition;
            return true;
        }

        public IWeapon GetById(int id)
        {
            return this.weapons[id];
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var weapon in this.weapons.Values)
            {
                yield return weapon;
            }
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            if (!this.weapons.ContainsKey(weapon.Id))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            IWeapon weaponToRefill = this.weapons[weapon.Id];
            if (weaponToRefill.Ammunition + ammunition > weaponToRefill.MaxCapacity)
            {
                weaponToRefill.Ammunition = weaponToRefill.MaxCapacity;
            }
            else
            {
                weaponToRefill.Ammunition += ammunition;
            }

            return weaponToRefill.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            if (!this.weapons.ContainsKey(id))
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            IWeapon result = this.weapons[id];
            this.weapons.Remove(id);
            return result;
        }

        public int RemoveHeavy()
        {
           IEnumerable<IWeapon> weaponsToRemove = this.weapons.Values.Where(w => (int)w.Category == (int)Category.Heavy);
           int result = weaponsToRemove.Count();
           foreach (IWeapon weapon in weaponsToRemove)
           {
               this.RemoveById(weapon.Id);
           }

           return result;
        }

        public List<IWeapon> RetrieveAll()
        {
            return this.weapons.Values.ToList();
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            return this.weapons.Values.Where(w => (int)w.Category >= (int)lower && (int)w.Category <= (int)upper).ToList();
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            if (!this.weapons.ContainsKey(firstWeapon.Id) || !this.weapons.ContainsKey(secondWeapon.Id))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
            if((int)firstWeapon.Category != (int)secondWeapon.Category)
                return;

            List<IWeapon> list = this.weapons.Values.ToList();
            int indexOfFirst = list.IndexOf(firstWeapon);
            int indexOfSecond = list.IndexOf(secondWeapon);
            list[indexOfFirst] = secondWeapon;
            list[indexOfSecond] = firstWeapon;
            this.weapons = new Dictionary<int, IWeapon>();
            foreach (var weapon in list)
            {
                this.weapons.Add(weapon.Id, weapon);
            }
        }
    }
}
