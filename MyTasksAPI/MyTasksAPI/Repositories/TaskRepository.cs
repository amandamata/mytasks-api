using MyTasks.Database;
using MyTasks.Models;
using MyTasks.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasks.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MyTasksContext _database;
        public TaskRepository(MyTasksContext database)
        {
            _database = database;
        }

        public List<Task> Restore(ApplicationUser user, DateTime lastSyncDate)
        {
            var query = _database.Tasks.Where(a => a.IdUser == user.Id).AsQueryable();
            if (lastSyncDate != null)
            {
                query.Where(a => a.Created >= lastSyncDate || a.Updated >= lastSyncDate);
            }
            return query.ToList();
        }

        public List<Task> Sync(List<Task> tasks)
        {
            var newTasks = tasks.Where(a => a.IdTaskApi == 0);

            if(newTasks.Count()> 0)
            {
                foreach (var task in newTasks)
                {
                    _database.Tasks.Add(task);
                }
            }

            var updatedExcludedTasks  = tasks.Where(a => a.IdTaskApi != 0);
            if (updatedExcludedTasks.Count() > 0)
            {
                foreach(var task in updatedExcludedTasks)
                {
                    _database.Update(task);
                }
            }
            _database.SaveChanges();
            return newTasks.ToList();
        }
    }
}
