using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ChatMVC.Controllers
{
    [Route("User")]
    public class UserController : Controller
    {
        public readonly LoginService service;

        public UserController(LoginService service)
        {
            this.service = service;
        }

        public IActionResult Index()
        {
            return View("Login");
        }
        [HttpPost("login")]
        public IActionResult Login(string login,string password )
        {
            var user = service.Login(login, password);
            if (String.IsNullOrEmpty(user.ApiKey))
            {
                ViewBag.Message = "Invalid login username/password. Please try again";
                return View();
            }
            return RedirectToAction("Index", "Home", user);
        }
        [HttpGet("register")]
        public IActionResult RegisterView()
        {
            return View("Register");
        }
        [HttpPost("register")]
        public IActionResult Register(string login,string password)
        {
            var newUser = service.Register(login, password);
            if (String.IsNullOrEmpty(newUser.UserName))
            {
                ViewBag.Message = "Username already token.Please try a new one.";
                return View("Register");
            }
            return Login(login, password);
        }
        [HttpGet("update")]
        public IActionResult UpdateView()
        {
            ViewBag.Message = TempData["msg"];
            return View("Update");
        }
        [HttpPost("update")]
        public IActionResult Update(string username,string avatarurl)
        {
            if (service.Update(username, avatarurl))
            {
                TempData["msg"] = "Update Successful";
                return RedirectToAction("UpdateView","User", ViewBag.Message = "UpdateSuccessful");
            }
            TempData["msg"] = "Update failed";
            return RedirectToAction("UpdateView","User");
            
        }
        
        [HttpGet("logout")]
        public IActionResult Logout()
        {
            var apiKey = service.Logout();
            if (String.IsNullOrEmpty(apiKey))
            {
                return RedirectToAction("Index");
            }
            TempData["message"] = "Unable to log out";
            return RedirectToAction("Index", "Home", new { apiKey,hasMessage = true });
        }
    }
}
