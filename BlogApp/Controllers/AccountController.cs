using BlogApp.Data;
using BlogApp.Models;
using BlogApp.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace BlogApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        public AccountController (ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        // POST Create
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(AccountRegisterViewModel obj)
        {
            var usernameChck = _db.Users.Where(u => u.Username == obj.Username).First();

            if(ModelState.IsValid && usernameChck == null)
            {
                var hashpassword = Crypto.HashPassword(obj.Password);
                User newuser = new()
                {
                    Firstname = obj.Firstname,
                    Lastname = obj.Lastname,
                    Username = obj.Username,
                    Password = hashpassword
                };
                _db.Users.Add(newuser);
                _db.SaveChanges();
                return RedirectToAction("index", "home");
            }
            if (usernameChck != null)
            {
                TempData["Error"] = "This username is taken";
            }
            return View("Register");
        }
    }
}
