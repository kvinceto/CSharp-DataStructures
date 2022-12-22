using System;
using System.Collections.Generic;

namespace VaccOps.Models
{
    public class Doctor
    {
        public Doctor(string name, int popularity)
        {
            this.Name = name;
            this.Popularity = popularity;
            this.Patients = new Dictionary<string, Patient>();
        }

        public string Name { get; set; }
        public int Popularity { get; set; }
        public Dictionary<string, Patient> Patients { get; set; }

        public bool Equals(object other)
        {
            return Name == ((Doctor)other).Name;
        }
    }
}
