using BlogApp.Data;
using BlogApp.Models;
using BlogApp.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
        public async Task<IActionResult> Create(AccountRegisterViewModel obj)
        {
            var usernameChck = _db.Users.Select(u => u.Username == obj.Username).First();

            if(ModelState.IsValid && usernameChck == false)
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
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, obj.Username));
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("index", "home");
            }
            if (usernameChck)
            {
                TempData["Error"] = "This username is taken";
            }
            return View("Register");
        }
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
