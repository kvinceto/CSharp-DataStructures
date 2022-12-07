using System.Linq;

namespace VaccOps
{
    using Models;
    using Interfaces;
    using System;
    using System.Collections.Generic;

    public class VaccDb : IVaccOps
    {
        private Dictionary<string, Doctor> doctorsByName;
        private Dictionary<string, Patient> patientsByName;

        public VaccDb()
        {
            this.doctorsByName = new Dictionary<string,Doctor>();
            this.patientsByName = new Dictionary<string,Patient>();
        }

        public void AddDoctor(Doctor doctor)
        {
            if (this.doctorsByName.ContainsKey(doctor.Name))
            {
                throw new ArgumentException();
            }
            this.doctorsByName.Add(doctor.Name, doctor);
        }

        public void AddPatient(Doctor doctor, Patient patient)
        {
            if (!this.Exist(doctor))
            {
                throw new ArgumentException();
            }
            this.patientsByName.Add(patient.Name, patient);
            doctor.Patients.Add(patient.Name, patient);
            patient.Doctor = doctor;
        }

        public IEnumerable<Doctor> GetDoctors()
        => this.doctorsByName.Values.AsEnumerable();

        public IEnumerable<Patient> GetPatients()
        => this.patientsByName.Values.AsEnumerable();

        public bool Exist(Doctor doctor)
            => this.doctorsByName.ContainsKey(doctor.Name);

        public bool Exist(Patient patient)
        => this.patientsByName.ContainsKey(patient.Name);

        public Doctor RemoveDoctor(string name)
        {
            if (!this.doctorsByName.ContainsKey(name))
            {
                throw new ArgumentException();
            }

            var doctor = this.doctorsByName[name];
            this.doctorsByName.Remove(name);
            foreach (var kvp in doctor.Patients)
            {
                this.patientsByName.Remove(kvp.Key);
            }

            return doctor;
        }

        public void ChangeDoctor(Doctor oldDoctor, Doctor newDoctor, Patient patient)
        {
            if (!this.doctorsByName.ContainsKey(oldDoctor.Name) ||
                !this.doctorsByName.ContainsKey(newDoctor.Name) ||
                !this.patientsByName.ContainsKey(patient.Name))
            {
                throw new ArgumentException();
            }
            patient.Doctor = newDoctor;
            oldDoctor.Patients.Remove(patient.Name);
            newDoctor.Patients.Add(patient.Name, patient);
        }

        public IEnumerable<Doctor> GetDoctorsByPopularity(int populariry)
            => this.doctorsByName.Values.Where(d => d.Popularity == populariry);

        public IEnumerable<Patient> GetPatientsByTown(string town)
            => this.patientsByName.Values.Where(p => p.Town == town);

        public IEnumerable<Patient> GetPatientsInAgeRange(int lo, int hi)
            => this.patientsByName.Values.Where(p => p.Age >= lo && p.Age <= hi);

        public IEnumerable<Doctor> GetDoctorsSortedByPatientsCountDescAndNameAsc()
            => this.doctorsByName.Values
                .OrderByDescending(d => d.Patients.Count)
                .ThenBy(d => d.Name);

        public IEnumerable<Patient> GetPatientsSortedByDoctorsPopularityAscThenByHeightDescThenByAge()
            => this.patientsByName.Values
                .OrderBy(p => p.Doctor.Popularity)
                .ThenByDescending(p => p.Height)
                .ThenBy(p => p.Age);
    }
}
