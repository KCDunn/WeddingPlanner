using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WeddingPlanner.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace WeddingPlanner.Controllers
{
    public class HomeController : Controller
    {
        private WeddingContext _context;

        public HomeController(WeddingContext context)
        {
            _context = context;
        }

        // GET: /Home/
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Register")]
        public IActionResult Register(RegisterUser regUser)
        {
            // Check uniqueness of user's email
            if(_context.users.Where(user => user.email == regUser.email).ToList().Count() > 0)
            {
                ModelState.AddModelError("email", "Email already exists");
            }

            if(ModelState.IsValid)
            {
                PasswordHasher<RegisterUser> hasher = new PasswordHasher<RegisterUser>();

                User NewPerson = new User
                {
                    first_name = regUser.first_name,
                    last_name = regUser.last_name,
                    email = regUser.email,
                    password = hasher.HashPassword(regUser, regUser.password)
                };
            
                _context.Add(NewPerson);
                // or _context.User.Add(NewPerson);
                _context.SaveChanges();
                int UserId = _context.users.Last().userId;
                
                // Console.WriteLine("User Id: " + UserId);
                HttpContext.Session.SetInt32("UserId", UserId);
                
                return RedirectToAction("Index", "Wedding");
            }
            return View("Index");
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult Login(LoginUser logUser)
        {
            User thisUser = _context.users.SingleOrDefault(user => user.email == logUser.logEmail);
            
            PasswordHasher<LoginUser> hasher = new PasswordHasher<LoginUser>();

            if(_context.users.Where(u => u.email == logUser.logEmail).ToList().Count() == 0)
            {
                ModelState.AddModelError("logEmail", "Invalid Email/Password");
            }
            else
            {
                if(hasher.VerifyHashedPassword(logUser, thisUser.password, logUser.logPassword) == 0)
                {
                    ModelState.AddModelError("logEmail", "Invalid Email/Password");
                }
            }
            
            if(ModelState.IsValid)
            {
                HttpContext.Session.SetInt32("UserId", (int)thisUser.userId);
                return RedirectToAction("Index", "Wedding");
            }
            return View("Index");
        }

        [Route("logout")]
        IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
        

        // [HttpGet]
        // [Route("dashboard")]
        // public IActionResult Dashboard()
        // {
        //     return View();
        // }

        
        // [HttpGet]
        // [Route("plan")]
        // public IActionResult Planner()
        // {
        //     return View();
        // }

        // [HttpGet]
        // [Route("show")]
        // public IActionResult Show()
        // {
        //     return View();
        // }
    }
}
