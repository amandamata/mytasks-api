using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyTasks.Models;
using MyTasks.Repositories.Contracts;
using System.Collections.Generic;

namespace MyTasks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly SignInManager<ApplicationUser> _login;
        private readonly UserManager<ApplicationUser> _userManager;
        public UserController(IUserRepository userRepository, SignInManager<ApplicationUser> login, UserManager<ApplicationUser> userManager)
        {
            _userRepository = userRepository;
            _login = login;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public ActionResult Login([FromBody] UserDTO userDTO)
        {
            ModelState.Remove("PersistPassword");
            ModelState.Remove("Name");

            if (ModelState.IsValid)
            {
                ApplicationUser user = _userRepository.Get(userDTO.Email, userDTO.Password);
                if (user != null)
                {
                    _login.SignInAsync(user, false);
                    return Ok();
                }
                else return NotFound("User not found!");
            }
            else return UnprocessableEntity(ModelState);
        }

        [HttpPost("")]
        public ActionResult Create([FromBody] UserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user = _userRepository.Get(userDTO.Email, userDTO.Password);
                if (user == null)
                {
                    user.FullName = userDTO.Name;
                    user.Email = userDTO.Email;
                    var result = _userManager.CreateAsync(user, userDTO.Password).Result;
                    if (!result.Succeeded)
                    {
                        List<string> errors = new List<string>();
                        foreach (var error in result.Errors)
                        {
                            errors.Add(error.Description);
                        }
                        return UnprocessableEntity(errors);
                    }
                    else return Ok();
                }
                else return UnprocessableEntity("User already exists");
            }
            else return UnprocessableEntity(ModelState);
        }
    }
}
