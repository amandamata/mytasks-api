using MyTasksAPI.Models;
using System;
using System.Collections.Generic;

namespace MyTasksAPI.Repositories.Contracts
{
    public interface ITaskRepository
    {
        List<Task> Sync(List<Task> tasks);
        List<Task> Restore(ApplicationUser user, DateTime lastSincDate);
    }
}
