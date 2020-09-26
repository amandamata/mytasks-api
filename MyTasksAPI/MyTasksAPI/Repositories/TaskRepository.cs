using MyTasksAPI.Database;
using MyTasksAPI.Models;
using MyTasksAPI.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyTasksAPI.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly MyTasksContext _database;
        public TaskRepository(MyTasksContext database)
        {
            database = _database
        }

        public List<Task> Restore(ApplicationUser user, DateTime lastSincDate)
        {
            var query = _database.Tasks.Where(a => a.IdUser == user.Id).AsQueryable();
            if (lastSincDate != null)
            {
                query.Where(a => a.Created >= lastSincDate || a.Updated >= lastSincDate);
            }
            return query.ToList();
        }

        public List<Task> Sinc(List<Task> tasks)
        {
            throw new NotImplementedException();
        }
    }
}
