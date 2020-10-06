using Microsoft.AspNetCore.Identity;
using MyTasks.Models;
using MyTasks.Repositories.Contracts;
using System;
using System.Text;

namespace MyTasks.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        
        public ApplicationUser Get(string email, string password)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (user != null)
                if (_userManager.CheckPasswordAsync(user, password).Result) return user;
            return new ApplicationUser();
        }
        
        public void Register(ApplicationUser user, string password)
        {
            var result = _userManager.CreateAsync(user, password).Result;
            if (!result.Succeeded)
            {
                StringBuilder sb = new StringBuilder();
                foreach(var error in result.Errors)
                {
                    sb.Append(error.Description);
                }
                throw new Exception($"User not registered! {sb.ToString()}");
            }
        }
    }
}
