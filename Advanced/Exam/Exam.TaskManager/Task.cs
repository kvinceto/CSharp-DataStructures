namespace Exam.TaskManager
{
    using System;

    public class Task
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int EstimatedExecutionTime { get; set; }

        public string Domain { get; set; }

        public bool Executed { get; set; }

        public Task(string id, string name, int estimatedExecutionTime, string domain)
        {
            Id = id;
            Name = name;
            EstimatedExecutionTime = estimatedExecutionTime;
            Domain = domain;
            Executed = false;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Task))
            {
                return false;
            }
            return this.Id == ((Task)obj).Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, EstimatedExecutionTime, Domain);
        }
    }
}
