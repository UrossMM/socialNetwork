using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using socialNetwork.Models;
using socialNetwork.Models.ViewModels;
using socialNetwork.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace socialNetwork.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IJWTManagerService tokenService;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public UsersController(IJWTManagerService repository, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            tokenService = repository;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public List<string> Get()
        {
            var users = new List<string> 
            {
                "Satinder singh",
                "Amit Sarna",
                "David John"
            };

            return users;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("authenticate")]
        public async Task<IActionResult> LogIn(UserVM userdata)
        {
            var result = await signInManager.PasswordSignInAsync(userdata.Email, userdata.Password, userdata.RememberMe, lockoutOnFailure: true);
            Console.WriteLine(result);
            if (result.Succeeded)
            {
                //_logger.LogInformation("User logged in.");
                var token = tokenService.Authenticate(userdata);

                if (token == null)
                {
                    return Unauthorized();
                }

                return Ok(token);
            }

            return NotFound();
           
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody]UserVM model)
        {
            var user = new User { UserName = model.Email, Email = model.Email, Password = model.Password, RememberMe = model.RememberMe, Name = model.Name };
            var result = await userManager.CreateAsync(user);

            //await signInManager.SignInAsync(user, isPersistent: false);
            return Ok();
        }
    }
}
