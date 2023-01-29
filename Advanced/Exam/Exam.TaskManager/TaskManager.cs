using System;
using System.Collections.Generic;

namespace Exam.TaskManager
{
    using System.Linq;

    public class TaskManager : ITaskManager
    {
        private Dictionary<string, Task> tasksById;
        private Queue<Task> tasksByOrder;
        private Queue<Task> executedTasks;
        private HashSet<Task> removedTasks;

        public TaskManager()
        {
            this.tasksById = new Dictionary<string, Task>();
            this.tasksByOrder = new Queue<Task>();
            this.executedTasks = new Queue<Task>();
            this.removedTasks = new HashSet<Task>();
        }

        public void AddTask(Task task)
        {
            this.tasksByOrder.Enqueue(task);
            this.tasksById[task.Id] = task;
        }

        public bool Contains(Task task)
        {
            return this.tasksById.ContainsKey(task.Id);
        }
        
        public void DeleteTask(string taskId)
        {
            if (!this.tasksById.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            Task task = this.tasksById[taskId];
            this.tasksById.Remove(taskId);
            this.removedTasks.Add(task);
        }

        public Task ExecuteTask()
        {
            bool isCorrect = false;
            Task task = null;

            while (!isCorrect)
            {
                if (this.tasksByOrder.Count == 0)
                {
                    throw new ArgumentException();
                }

                task = this.tasksByOrder.Dequeue();
                if (!this.removedTasks.Contains(task))
                {
                    isCorrect = true;
                }
            }

            task.Executed = true;
            this.executedTasks.Enqueue(task);

            return task;
        }

        public IEnumerable<Task> GetAllTasksOrderedByEETThenByName()
        {
            return this.tasksById.Values
                .OrderByDescending(t => t.EstimatedExecutionTime)
                .ThenBy(t => t.Name.Length);
        }

        public IEnumerable<Task> GetDomainTasks(string domain)
        {
            var list =  this.tasksById.Values
                .Where(t => t.Domain == domain && t.Executed == false);
            if (list.Count() == 0)
            {
                throw new ArgumentException();
            }

            return list;
        }

        public Task GetTask(string taskId)
        {
            if (!this.tasksById.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            return this.tasksById[taskId];
        }

        public IEnumerable<Task> GetTasksInEETRange(int lowerBound, int upperBound)
        {
           var list = new List<Task>();
           foreach (var task in this.tasksByOrder)
           {
               if (task.EstimatedExecutionTime >= lowerBound && task.EstimatedExecutionTime <= upperBound)
               {
                   list.Add(task);
               }
           }

           return list;
        }

        public void RescheduleTask(string taskId)
        {
            if (!this.tasksById.ContainsKey(taskId))
            {
                throw new ArgumentException();
            }

            Task task = this.tasksById[taskId];
            if (!task.Executed)
            {
                throw new ArgumentException();
            }

            this.AddTask(task);
        }

        public int Size()
        {
            return this.tasksById.Count;
        }
    }
}
