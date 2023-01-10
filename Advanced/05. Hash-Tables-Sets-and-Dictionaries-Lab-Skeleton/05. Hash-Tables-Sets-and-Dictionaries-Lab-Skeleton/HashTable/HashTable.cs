namespace HashTable
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
    {
        private const int DefaultCapacity = 4;
        private const float LoadFactor = 0.75f;

        private LinkedList<KeyValue<TKey, TValue>>[] slots;

        public int Count { get; private set; }

        public int Capacity => this.slots.Length;

        public HashTable() : this(DefaultCapacity) { }

        public HashTable(int capacity)
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        }

        public void Add(TKey key, TValue value)
        {
            this.GrowIfNeeded();

            int index = Math.Abs(key.GetHashCode()) % this.Capacity;

            if (this.slots[index] == null)
            {
                this.slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.slots[index])
            {
                if (element.Key.Equals(key))
                {
                    throw new ArgumentException();
                }
            }

            var item = new KeyValue<TKey, TValue>(key, value);
            this.slots[index].AddLast(item);
            this.Count++;
        }

        private void GrowIfNeeded()
        {
            if ((float)(this.Count + 1) / this.Capacity >= LoadFactor)
            {
                var newTable = new HashTable<TKey, TValue>(this.Capacity * 2);
                foreach (var element in this)
                {
                    newTable.Add(element.Key, element.Value);
                }

                this.slots = newTable.slots;
                this.Count = newTable.Count;
            }
        }

        public bool AddOrReplace(TKey key, TValue value)
        {

            this.GrowIfNeeded();

            int index = Math.Abs(key.GetHashCode()) % this.Capacity;

            if (this.slots[index] == null)
            {
                this.slots[index] = new LinkedList<KeyValue<TKey, TValue>>();
            }

            foreach (var element in this.slots[index])
            {
                if (element.Key.Equals(key))
                {
                    element.Value = value;
                    return true;
                }
            }

            var item = new KeyValue<TKey, TValue>(key, value);
            this.slots[index].AddLast(item);
            this.Count++;

            return false;
        }

        public TValue Get(TKey key)
        {
            var element = this.Find(key);

            if (element == null)
            {
                throw new KeyNotFoundException();
            }

            return element.Value;
        }

        public TValue this[TKey key]
        {
            get
            {
                int index = Math.Abs(key.GetHashCode()) % this.Capacity;
                if (this.slots[index] != null)
                {
                    foreach (var element in slots[index])
                    {
                        if (element.Key.Equals(key))
                        {
                            return element.Value;
                        }
                    }
                }

                throw new KeyNotFoundException();
            }
            set
            {
                this.AddOrReplace(key, value);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            var element = this.Find(key);

            if (element != null)
            {
                value = element.Value;
                return true;
            }

            value = default;
            return false;
        }

        public KeyValue<TKey, TValue> Find(TKey key)
        {
            int index = Math.Abs(key.GetHashCode()) % this.Capacity;

            if (this.slots[index] != null)
            {
                foreach (var element in slots[index])
                {
                    if (element.Key.Equals(key))
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        public bool ContainsKey(TKey key)
        {
            return this.Find(key) != null;
        }

        public bool Remove(TKey key)
        {
            int index = Math.Abs(key.GetHashCode()) % this.Capacity;

            if (this.slots[index] != null)
            {
                var current = this.slots[index].First;
                while (current != null)
                {
                    if (current.Value.Key.Equals(key))
                    {
                        this.slots[index].Remove(current);
                        this.Count--;
                        return true;
                    }
                    current = current.Next;
                }
            }

            return false;
        }

        public void Clear()
        {
            this.slots = new LinkedList<KeyValue<TKey, TValue>>[DefaultCapacity];
            this.Count = 0;
        }

        public IEnumerable<TKey> Keys => this.Select(kvp => kvp.Key);

        public IEnumerable<TValue> Values
        {
            get
            {
                var result = new List<TValue>();

                foreach (var slot in slots)
                {
                    if (slot != null)
                    {
                        foreach (var element in slot)
                        {
                            result.Add(element.Value);
                        }
                    }
                }
                return result;
            }
        }

        public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
        {
            foreach (var slot in this.slots)
            {
                if (slot != null)
                {
                    foreach (var element in slot)
                    {
                        yield return element;
                    }
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}
