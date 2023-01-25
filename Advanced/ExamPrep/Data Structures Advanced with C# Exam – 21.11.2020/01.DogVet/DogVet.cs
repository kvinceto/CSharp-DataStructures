namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DogVet : IDogVet
    {
        private Dictionary<string, Dog> dogsById;
        private Dictionary<string, Owner> ownersById;

        public DogVet()
        {
            dogsById = new Dictionary<string, Dog>();
            ownersById = new Dictionary<string, Owner>();
        }

        public int Size => dogsById.Count;

        public void AddDog(Dog dog, Owner owner)
        {
            if (dogsById.ContainsKey(dog.Id))
            {
                throw new ArgumentException();
            }

            if (!ownersById.ContainsKey(owner.Id))
            {
                ownersById.Add(owner.Id, owner);
            }

            if (owner.dogsByName.ContainsKey(dog.Name))
            {
                throw new ArgumentException();
            }

            dogsById.Add(dog.Id, dog);
            ownersById[owner.Id].dogsByName.Add(dog.Name, dog);
            dog.Owner = owner;
        }

        public bool Contains(Dog dog)
        {
            return dogsById.ContainsKey(dog.Id);
        }

        public Dog GetDog(string name, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            if (!ownersById[ownerId].dogsByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            return ownersById[ownerId].dogsByName[name];
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            if (!ownersById[ownerId].dogsByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            Dog dog = ownersById[ownerId].dogsByName[name];
            ownersById[ownerId].dogsByName.Remove(name);
            dogsById.Remove(dog.Id);
            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            return ownersById[ownerId].dogsByName.Values;
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            if (dogsById.Count == 0)
            {
                throw new ArgumentException();
            }

            return dogsById.Values.Where(d => d.Breed.Equals(breed));
        }

        public void Vaccinate(string name, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            if (!ownersById[ownerId].dogsByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            ownersById[ownerId].dogsByName[name].Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            if (!ownersById.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            if (!ownersById[ownerId].dogsByName.ContainsKey(oldName))
            {
                throw new ArgumentException();
            }

            Dog dog = ownersById[ownerId].dogsByName[oldName];

            if (ownersById[ownerId].dogsByName.ContainsKey(newName))
            {
                throw new ArgumentException();
            }

            ownersById[ownerId].dogsByName.Remove(oldName);
            dog.Name = newName;
            ownersById[ownerId].dogsByName.Add(dog.Name, dog);
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            
            var list = new List<Dog>();

            foreach (var dog in dogsById)
            {
                if (dog.Value.Age == age)
                {
                    list.Add(dog.Value);
                }
            }

            if (list.Count == 0)
            {
                throw new ArgumentException();
            }

            return list;
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            var list = dogsById.Values.Where(d => d.Age >= lo && d.Age <= hi);
            
            return list;
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {
            return dogsById.Values
                .OrderBy(d => d.Age)
                .ThenBy(d => d.Name)
                .ThenBy(d => d.Owner.Name);
        }
    }
}