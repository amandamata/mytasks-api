using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTasksAPI.Models;
using MyTasksAPI.Repositories;
using System;
using System.Collections.Generic;

namespace MyTasksAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskRepository _taskRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        public TaskController(TaskRepository taskRepository, UserManager<ApplicationUser> userManager)
        {
            _taskRepository = taskRepository;
            _userManager = userManager;
        }

        public ActionResult Sync([FromBody] List<Task> tasks)
        {
            return Ok(_taskRepository.Sync(tasks));
        }

        public ActionResult Restore(DateTime lastSyncDate)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            return Ok(_taskRepository.Restore(user, lastSyncDate));
        }
    }
}
